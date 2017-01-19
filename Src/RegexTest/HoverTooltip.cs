using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace RegexTest
{
    /// <summary>
    ///     Summary description for HoverTooltip.
    /// </summary>
    public class HoverTooltip : Form
    {
        /// <summary>
        ///     Required designer variable.
        /// </summary>
        private readonly Container _components = null;

        private TextBox _textBox1;

        public HoverTooltip()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            ShowInTaskbar = false;

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public string WindowText
        {
            set
            {
                _textBox1.Text = value;
                var g = Graphics.FromHwnd(Handle);
                var size = g.MeasureString(value, _textBox1.Font);
                _textBox1.Size = new Size((int) size.Width, (int) size.Height);
                Size = new Size(_textBox1.Size.Width + 16,
                    _textBox1.Size.Height + 16);
                g.Dispose();
            }
        }

        /// <summary>
        ///     Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (_components != null)
                    _components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this._textBox1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                     | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right);
            this._textBox1.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (255)), ((System.Byte) (255)),
                ((System.Byte) (192)));
            this._textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
            this._textBox1.Location = new System.Drawing.Point(8, 8);
            this._textBox1.Multiline = true;
            this._textBox1.Name = "_textBox1";
            this._textBox1.Size = new System.Drawing.Size(304, 200);
            this._textBox1.TabIndex = 0;
            this._textBox1.Text = "textBox1";
            // 
            // HoverTooltip
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (255)), ((System.Byte) (255)),
                ((System.Byte) (192)));
            this.ClientSize = new System.Drawing.Size(306, 198);
            this.ControlBox = false;
            this.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this._textBox1
            });
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "HoverTooltip";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.ResumeLayout(false);
        }

        #endregion
    }
}