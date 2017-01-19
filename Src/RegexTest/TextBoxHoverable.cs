using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

// to do
// provide an event on the hover.
//   on mousemove, 

namespace RegexTest
{
    public enum HoverState
    {
        Ready,
        InHover,
        AfterClick
    }

    public class HoverEventArgs : EventArgs
    {
        public HoverEventArgs(int characterOffset)
        {
            CharacterOffset = characterOffset;
        }

        public int CharacterOffset { get; }
    }

    public class HoverDetailAction
    {
        public HoverDetailAction(int highlightStart, int highlightLength, string tooltipText)
        {
            HighlightStart = highlightStart;
            HighlightLength = highlightLength;
            TooltipText = tooltipText;
        }

        public int HighlightStart { get; }

        public int HighlightLength { get; }

        public string TooltipText { get; }
    }

    public delegate HoverDetailAction HoverDetailEventHandler(object sender, HoverEventArgs args);


    /// <summary>
    ///     A textbox that has advanced hovering support.
    /// </summary>
    public class TextBoxHoverable : TextBox
    {
        private SizeF _characterSize; // estimated character size
        private IContainer components;
        private Graphics _g;
        private HoverState _hoverState = HoverState.Ready;
        private Timer _hoverTimer;
        private readonly HoverTooltip _hoverTooltip = new HoverTooltip();
        private MouseEventArgs _lastLocation;
        private int _mouseMoveIgnore;

        public TextBoxHoverable()
        {
            InitializeComponent();
        }

        public event HoverDetailEventHandler HoverDetail;

        protected override void OnMouseMove(MouseEventArgs args)
        {
            if (args.Button == MouseButtons.None)
            {
                if (_mouseMoveIgnore > 0)
                {
                    _mouseMoveIgnore--;
                    return;
                }
                _lastLocation = args;

                if (_hoverState == HoverState.InHover)
                    DoHover();
                else
                    try
                    {
                        _hoverTimer.Enabled = false;
                        _hoverTimer.Enabled = true;
                        _hoverTimer.Start();
                        _hoverState = HoverState.Ready;
                    }
                    catch (InvalidOperationException)
                    {
                        // eat this here...
                    }
            }

            base.OnMouseMove(args);
        }

        private void InitializeComponent()
        {
            components = new Container();
            _hoverTimer = new Timer(components);
            // 
            // hoverTimer
            // 
            _hoverTimer.Tick += hoverTimer_Tick;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
            base.OnPaint(pe);
        }

        private void hoverTimer_Tick(object sender, EventArgs e)
        {
            DoHover();
        }

        private void DoHover()
        {
            // if the user has clicked and hasn't moved the mouse, 
            // don't do anything...
            if (_hoverState == HoverState.AfterClick)
                return;

            _hoverTimer.Enabled = false;

            // first time - create the Graphics object, and get the
            // height of the characters...
            if (_g == null)
            {
                _g = Graphics.FromHwnd(Handle);
                _characterSize = _g.MeasureString("X", Font);
            }

            // Figure out what character we're hovering over. 
            // Start by identifying the line

            var line = (int) (_lastLocation.Y / _characterSize.Height);
            if (line >= Lines.Length)
            {
                DoneHover();
                return;
            }

            // Do this with a brute-force algorithm, by measuring
            // the string until we find the character that puts us
            // to the right of the mouse location.

            var s = Lines[line];
            var size = _g.MeasureString(s, Font);
            if (_lastLocation.X > size.Width) // past end of line
            {
                DoneHover();
                return;
            }

            while (size.Width > _lastLocation.X)
            {
                s = s.Substring(0, s.Length - 1);
                size = _g.MeasureString(s, Font);
            }

            // figure out the character offset for this line...
            var offset = 0;
            for (var lineIndex = 0; lineIndex < line; lineIndex++)
                offset += Lines[lineIndex].Length + 2; // add 2 to cover /r/n at the end of each line

            var action = OnHoverDetail(s.Length + offset);
            if (action != null)
            {
                SelectionStart = action.HighlightStart;
                SelectionLength = action.HighlightLength;
                if (SelectionLength < 0)
                    SelectionLength = 0;

                _hoverTooltip.WindowText = action.TooltipText;

                _hoverTooltip.Location =
                    PointToScreen(new Point(50,
                        Font.Height * (line + 2)));
                _hoverTooltip.Show();
                Debug.WriteLine(action.TooltipText);
                if (!Focused)
                    Focus();
                _hoverState = HoverState.InHover;
            }

            _hoverTimer.Enabled = false;
        }

        protected override void OnClick(EventArgs e)
        {
            _hoverTimer.Enabled = false;
            _hoverState = HoverState.AfterClick;
            _hoverTooltip.Hide();
            _mouseMoveIgnore = 1;
            base.OnClick(e);
        }

        private void DoneHover()
        {
            if (_hoverState == HoverState.InHover)
            {
                _hoverTooltip.Hide();
                SelectionLength = 0;
                _hoverState = HoverState.Ready;
                _hoverTimer.Enabled = false;
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            DoneHover();
            base.OnLostFocus(e);
        }

        private HoverDetailAction OnHoverDetail(int offset)
        {
            if (HoverDetail != null)
                return HoverDetail(this, new HoverEventArgs(offset));
            return null;
        }

        protected override void OnLeave(EventArgs e)
        {
            DoneHover();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            DoneHover();
            base.OnMouseLeave(e);
        }
    }
}