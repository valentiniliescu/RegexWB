using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RegexTest
{
    /// <summary>
    ///     Summary description for Form1.
    /// </summary>
    public class Form1 : Form
    {
        private MenuItem _about;
        private MenuItem _addItem;
        private MenuItem _backReferenceMenuItem;
        private RegexBuffer _buffer;
        private bool _bufferDirty = true;
        private Button _button1;
        private SizeF _characterSize;
        private CheckBox _compiled;
        private TextBox _compileTime;
        private IContainer components;
        private ContextMenu _contextMenu1;

        private readonly MenuItem _contextMenuAddItems;
        private readonly MenuItem _contextMenuBackreferences;
        private MenuItem _copyAsCSharp;
        private MenuItem _copyAsVb;
        private MenuItem _copyMenuItem;
        private readonly string _currentDirectory = Environment.CurrentDirectory;
        private MenuItem _cutMenuItem;
        private MenuItem _deleteMenuItem;
        private TextBox _description;
        private TextBox _elapsed;
        private CheckBox _explicitCapture;
        private GroupBox _groupBox1;
        private GroupBox _groupBox2;
        private CheckBox _hideGroupZero;
        private CheckBox _hoverInterpret;
        private CheckBox _ignoreCase;
        private CheckBox _ignoreWhitespace;
        private Button _interpret;
        private TextBox _iterations;
        private Label _label1;
        private Label _label2;
        private Label _label3;
        private Label _label33;
        private Label _label4;
        private Label _label5;
        private Label _label6;
        private Label _label7;
        private MenuItem _library;
        private MainMenu _mainMenu1;
        private MenuItem _makeAssemblyItem;
        private CheckBox _matchEvaluator;
        private MenuItem _menuItem1;
        private MenuItem _menuItem10;
        private MenuItem _menuItem11;
        private MenuItem _menuItem12;
        private MenuItem _menuItem13;
        private MenuItem _menuItem14;
        private MenuItem _menuItem15;
        private MenuItem _menuItem16;
        private MenuItem _menuItem17;
        private MenuItem _menuItem18;
        private MenuItem _menuItem19;
        private MenuItem _menuItem2;
        private MenuItem _menuItem20;
        private MenuItem _menuItem21;
        private MenuItem _menuItem22;
        private MenuItem _menuItem23;
        private MenuItem _menuItem24;
        private MenuItem _menuItem25;
        private MenuItem _menuItem26;
        private MenuItem _menuItem27;
        private MenuItem _menuItem28;
        private MenuItem _menuItem29;
        private MenuItem _menuItem3;
        private MenuItem _menuItem30;
        private MenuItem _menuItem31;
        private MenuItem _menuItem32;
        private MenuItem _menuItem33;
        private MenuItem _menuItem34;
        private MenuItem _menuItem35;
        private MenuItem _menuItem36;
        private MenuItem _menuItem37;
        private MenuItem _menuItem38;
        private MenuItem _menuItem39;
        private MenuItem _menuItem4;
        private MenuItem _menuItem40;
        private MenuItem _menuItem41;
        private MenuItem _menuItem42;
        private MenuItem _menuItem43;
        private MenuItem _menuItem44;
        private MenuItem _menuItem45;
        private MenuItem _menuItem46;
        private MenuItem _menuItem47;
        private MenuItem _menuItem48;
        private MenuItem _menuItem49;
        private MenuItem _menuItem5;
        private MenuItem _menuItem50;
        private MenuItem _menuItem51;
        private MenuItem _menuItem52;
        private MenuItem _menuItem53;
        private MenuItem _menuItem54;
        private MenuItem _menuItem55;
        private MenuItem _menuItem56;
        private MenuItem _menuItem57;
        private MenuItem _menuItem58;
        private MenuItem _menuItem59;
        private MenuItem _menuItem6;
        private MenuItem _menuItem60;
        private MenuItem _menuItem61;
        private MenuItem _menuItem62;
        private MenuItem _menuItem63;
        private MenuItem _menuItem64;
        private MenuItem _menuItem65;
        private MenuItem _menuItem66;
        private MenuItem _menuItem67;
        private MenuItem _menuItem68;
        private MenuItem _menuItem69;
        private MenuItem _menuItem7;
        private MenuItem _menuItem70;
        private MenuItem _menuItem71;
        private MenuItem _menuItem72;
        private MenuItem _menuItem73;
        private MenuItem _menuItem78;
        private MenuItem _menuItem8;
        private MenuItem _menuItem80;
        private MenuItem _menuItem9;
        private MenuItem _menuReleaseNotes;
        private CheckBox _multiline;
        private CheckBox _oneString;
        private TextBox _output;
        private Panel _panel1;
        private Panel _panel2;
        private Panel _panel3;
        private Panel _panel4;
        private Panel _panel5;
        private Panel _panel6;
        private Panel _panel7;
        private Panel _panel8;
        private Panel _panel9;
        private MenuItem _pasteFromCSharp;
        private MenuItem _pasteMenuItem;

        private int _regexInsertionPoint = -1;
        private TextBoxHoverable _regexText;
        private Button _replace;
        private TextBox _replaceString;
        private MenuItem _saveRegex;
        private MenuItem _selectAllMenuItem;
        private CheckBox _singleline;
        private Button _split;
        private TextBox _strings;
        private ToolTip _toolTip1;
        private MenuItem _undoMenuItem;

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            EventHandler menuHandler = addItem_Click;
            foreach (MenuItem mainItem in _addItem.MenuItems)
            foreach (MenuItem subItem in mainItem.MenuItems)
                subItem.Click += menuHandler;

            // create the context menu for the regex box. To get
            // the ordering we want, we have to create a new context
            // menu, clone over the add items menu items, and then
            // copy over the ones from the orignal context menu
            var newContextMenu = new ContextMenu();
            _contextMenuAddItems = _addItem.CloneMenu();
            newContextMenu.MenuItems.Add(_contextMenuAddItems);
            foreach (MenuItem contextItem in _contextMenu1.MenuItems)
                newContextMenu.MenuItems.Add(contextItem.CloneMenu());
            _regexText.ContextMenu = newContextMenu;

            // Find backreferences entry so we can use it for later
            // fixups...
            foreach (MenuItem subItem in _contextMenuAddItems.MenuItems)
                if (subItem.Text.IndexOf("Backreference") != -1)
                    _contextMenuBackreferences = subItem;
        }

        /// <summary>
        ///     Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            var resources = new System.Resources.ResourceManager(typeof(Form1));
            this._regexText = new RegexTest.TextBoxHoverable();
            this._contextMenu1 = new System.Windows.Forms.ContextMenu();
            this._menuItem80 = new System.Windows.Forms.MenuItem();
            this._undoMenuItem = new System.Windows.Forms.MenuItem();
            this._menuItem72 = new System.Windows.Forms.MenuItem();
            this._cutMenuItem = new System.Windows.Forms.MenuItem();
            this._copyMenuItem = new System.Windows.Forms.MenuItem();
            this._pasteMenuItem = new System.Windows.Forms.MenuItem();
            this._deleteMenuItem = new System.Windows.Forms.MenuItem();
            this._menuItem78 = new System.Windows.Forms.MenuItem();
            this._selectAllMenuItem = new System.Windows.Forms.MenuItem();
            this._label1 = new System.Windows.Forms.Label();
            this._strings = new System.Windows.Forms.TextBox();
            this._label2 = new System.Windows.Forms.Label();
            this._ignoreWhitespace = new System.Windows.Forms.CheckBox();
            this._button1 = new System.Windows.Forms.Button();
            this._label3 = new System.Windows.Forms.Label();
            this._output = new System.Windows.Forms.TextBox();
            this._ignoreCase = new System.Windows.Forms.CheckBox();
            this._compiled = new System.Windows.Forms.CheckBox();
            this._explicitCapture = new System.Windows.Forms.CheckBox();
            this._singleline = new System.Windows.Forms.CheckBox();
            this._multiline = new System.Windows.Forms.CheckBox();
            this._label4 = new System.Windows.Forms.Label();
            this._elapsed = new System.Windows.Forms.TextBox();
            this._label5 = new System.Windows.Forms.Label();
            this._iterations = new System.Windows.Forms.TextBox();
            this._interpret = new System.Windows.Forms.Button();
            this._split = new System.Windows.Forms.Button();
            this._compileTime = new System.Windows.Forms.TextBox();
            this._label33 = new System.Windows.Forms.Label();
            this._oneString = new System.Windows.Forms.CheckBox();
            this._menuItem2 = new System.Windows.Forms.MenuItem();
            this._menuItem4 = new System.Windows.Forms.MenuItem();
            this._menuItem5 = new System.Windows.Forms.MenuItem();
            this._menuItem6 = new System.Windows.Forms.MenuItem();
            this._menuItem7 = new System.Windows.Forms.MenuItem();
            this._menuItem8 = new System.Windows.Forms.MenuItem();
            this._menuItem9 = new System.Windows.Forms.MenuItem();
            this._menuItem10 = new System.Windows.Forms.MenuItem();
            this._menuItem11 = new System.Windows.Forms.MenuItem();
            this._menuItem12 = new System.Windows.Forms.MenuItem();
            this._menuItem13 = new System.Windows.Forms.MenuItem();
            this._menuItem3 = new System.Windows.Forms.MenuItem();
            this._menuItem14 = new System.Windows.Forms.MenuItem();
            this._menuItem15 = new System.Windows.Forms.MenuItem();
            this._menuItem68 = new System.Windows.Forms.MenuItem();
            this._menuItem16 = new System.Windows.Forms.MenuItem();
            this._menuItem17 = new System.Windows.Forms.MenuItem();
            this._menuItem18 = new System.Windows.Forms.MenuItem();
            this._menuItem19 = new System.Windows.Forms.MenuItem();
            this._menuItem20 = new System.Windows.Forms.MenuItem();
            this._menuItem21 = new System.Windows.Forms.MenuItem();
            this._menuItem22 = new System.Windows.Forms.MenuItem();
            this._menuItem23 = new System.Windows.Forms.MenuItem();
            this._menuItem24 = new System.Windows.Forms.MenuItem();
            this._menuItem25 = new System.Windows.Forms.MenuItem();
            this._menuItem26 = new System.Windows.Forms.MenuItem();
            this._menuItem27 = new System.Windows.Forms.MenuItem();
            this._menuItem28 = new System.Windows.Forms.MenuItem();
            this._menuItem29 = new System.Windows.Forms.MenuItem();
            this._menuItem69 = new System.Windows.Forms.MenuItem();
            this._menuItem30 = new System.Windows.Forms.MenuItem();
            this._menuItem31 = new System.Windows.Forms.MenuItem();
            this._menuItem32 = new System.Windows.Forms.MenuItem();
            this._menuItem33 = new System.Windows.Forms.MenuItem();
            this._menuItem34 = new System.Windows.Forms.MenuItem();
            this._menuItem35 = new System.Windows.Forms.MenuItem();
            this._menuItem36 = new System.Windows.Forms.MenuItem();
            this._menuItem37 = new System.Windows.Forms.MenuItem();
            this._menuItem38 = new System.Windows.Forms.MenuItem();
            this._menuItem39 = new System.Windows.Forms.MenuItem();
            this._menuItem40 = new System.Windows.Forms.MenuItem();
            this._menuItem41 = new System.Windows.Forms.MenuItem();
            this._menuItem42 = new System.Windows.Forms.MenuItem();
            this._menuItem43 = new System.Windows.Forms.MenuItem();
            this._menuItem44 = new System.Windows.Forms.MenuItem();
            this._menuItem45 = new System.Windows.Forms.MenuItem();
            this._menuItem46 = new System.Windows.Forms.MenuItem();
            this._menuItem47 = new System.Windows.Forms.MenuItem();
            this._menuItem53 = new System.Windows.Forms.MenuItem();
            this._menuItem48 = new System.Windows.Forms.MenuItem();
            this._menuItem49 = new System.Windows.Forms.MenuItem();
            this._menuItem50 = new System.Windows.Forms.MenuItem();
            this._menuItem51 = new System.Windows.Forms.MenuItem();
            this._menuItem52 = new System.Windows.Forms.MenuItem();
            this._menuItem54 = new System.Windows.Forms.MenuItem();
            this._menuItem55 = new System.Windows.Forms.MenuItem();
            this._menuItem56 = new System.Windows.Forms.MenuItem();
            this._menuItem57 = new System.Windows.Forms.MenuItem();
            this._menuItem58 = new System.Windows.Forms.MenuItem();
            this._menuItem59 = new System.Windows.Forms.MenuItem();
            this._menuItem60 = new System.Windows.Forms.MenuItem();
            this._menuItem61 = new System.Windows.Forms.MenuItem();
            this._menuItem62 = new System.Windows.Forms.MenuItem();
            this._menuItem63 = new System.Windows.Forms.MenuItem();
            this._menuItem64 = new System.Windows.Forms.MenuItem();
            this._menuItem65 = new System.Windows.Forms.MenuItem();
            this._menuItem66 = new System.Windows.Forms.MenuItem();
            this._menuItem67 = new System.Windows.Forms.MenuItem();
            this._backReferenceMenuItem = new System.Windows.Forms.MenuItem();
            this._toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this._hoverInterpret = new System.Windows.Forms.CheckBox();
            this._description = new System.Windows.Forms.TextBox();
            this._hideGroupZero = new System.Windows.Forms.CheckBox();
            this._replaceString = new System.Windows.Forms.TextBox();
            this._replace = new System.Windows.Forms.Button();
            this._matchEvaluator = new System.Windows.Forms.CheckBox();
            this._mainMenu1 = new System.Windows.Forms.MainMenu();
            this._menuItem70 = new System.Windows.Forms.MenuItem();
            this._saveRegex = new System.Windows.Forms.MenuItem();
            this._makeAssemblyItem = new System.Windows.Forms.MenuItem();
            this._menuItem73 = new System.Windows.Forms.MenuItem();
            this._copyAsCSharp = new System.Windows.Forms.MenuItem();
            this._copyAsVb = new System.Windows.Forms.MenuItem();
            this._pasteFromCSharp = new System.Windows.Forms.MenuItem();
            this._addItem = new System.Windows.Forms.MenuItem();
            this._library = new System.Windows.Forms.MenuItem();
            this._menuItem1 = new System.Windows.Forms.MenuItem();
            this._about = new System.Windows.Forms.MenuItem();
            this._groupBox1 = new System.Windows.Forms.GroupBox();
            this._label6 = new System.Windows.Forms.Label();
            this._groupBox2 = new System.Windows.Forms.GroupBox();
            this._panel1 = new System.Windows.Forms.Panel();
            this._panel2 = new System.Windows.Forms.Panel();
            this._panel3 = new System.Windows.Forms.Panel();
            new System.Windows.Forms.PageSetupDialog();
            this._panel4 = new System.Windows.Forms.Panel();
            this._panel5 = new System.Windows.Forms.Panel();
            this._panel6 = new System.Windows.Forms.Panel();
            this._label7 = new System.Windows.Forms.Label();
            this._panel7 = new System.Windows.Forms.Panel();
            this._panel8 = new System.Windows.Forms.Panel();
            this._panel9 = new System.Windows.Forms.Panel();
            this._menuItem71 = new System.Windows.Forms.MenuItem();
            this._menuReleaseNotes = new System.Windows.Forms.MenuItem();
            this._groupBox1.SuspendLayout();
            this._groupBox2.SuspendLayout();
            this._panel1.SuspendLayout();
            this._panel2.SuspendLayout();
            this._panel4.SuspendLayout();
            this._panel5.SuspendLayout();
            this._panel7.SuspendLayout();
            this._panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // RegexText
            // 
            this._regexText.Anchor =
            ((System.Windows.Forms.AnchorStyles)
            (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
              | System.Windows.Forms.AnchorStyles.Right)));
            this._regexText.ContextMenu = this._contextMenu1;
            this._regexText.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
            this._regexText.Location = new System.Drawing.Point(144, 32);
            this._regexText.Multiline = true;
            this._regexText.Name = "_regexText";
            this._regexText.Size = new System.Drawing.Size(856, 184);
            this._regexText.TabIndex = 0;
            this._regexText.Text = "\\((?<AreaCode>\\d{3})\\)";
            this._regexText.Leave += new System.EventHandler(this.RegexText_Leave);
            this._regexText.HoverDetail += new RegexTest.HoverDetailEventHandler(this.RegexText_HoverDetail);
            this._regexText.TextChanged += new System.EventHandler(this.RegexText_TextChanged_1);
            this._regexText.Enter += new System.EventHandler(this.RegexText_Enter);
            // 
            // contextMenu1
            // 
            this._contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
            {
                this._menuItem80,
                this._undoMenuItem,
                this._menuItem72,
                this._cutMenuItem,
                this._copyMenuItem,
                this._pasteMenuItem,
                this._deleteMenuItem,
                this._menuItem78,
                this._selectAllMenuItem
            });
            this._contextMenu1.Popup += new System.EventHandler(this.contextMenu1_Popup);
            // 
            // menuItem80
            // 
            this._menuItem80.Index = 0;
            this._menuItem80.Text = "----";
            // 
            // undoMenuItem
            // 
            this._undoMenuItem.Index = 1;
            this._undoMenuItem.Text = "Undo";
            this._undoMenuItem.Click += new System.EventHandler(this.undoMenuItem_Click);
            // 
            // menuItem72
            // 
            this._menuItem72.Index = 2;
            this._menuItem72.Text = "-------";
            // 
            // cutMenuItem
            // 
            this._cutMenuItem.Index = 3;
            this._cutMenuItem.Text = "Cut";
            this._cutMenuItem.Click += new System.EventHandler(this.cutMenuItem_Click);
            // 
            // copyMenuItem
            // 
            this._copyMenuItem.Index = 4;
            this._copyMenuItem.Text = "Copy";
            this._copyMenuItem.Click += new System.EventHandler(this.copyMenuItem_Click);
            // 
            // pasteMenuItem
            // 
            this._pasteMenuItem.Index = 5;
            this._pasteMenuItem.Text = "Paste";
            this._pasteMenuItem.Click += new System.EventHandler(this.pasteMenuItem_Click);
            // 
            // deleteMenuItem
            // 
            this._deleteMenuItem.Index = 6;
            this._deleteMenuItem.Text = "Delete";
            this._deleteMenuItem.Click += new System.EventHandler(this.deleteMenuItem_Click);
            // 
            // menuItem78
            // 
            this._menuItem78.Index = 7;
            this._menuItem78.Text = "----";
            // 
            // selectAllMenuItem
            // 
            this._selectAllMenuItem.Index = 8;
            this._selectAllMenuItem.Text = "Select All";
            this._selectAllMenuItem.Click += new System.EventHandler(this.selectAllMenuItem_Click);
            // 
            // label1
            // 
            this._label1.Location = new System.Drawing.Point(144, 8);
            this._label1.Name = "_label1";
            this._label1.Size = new System.Drawing.Size(40, 23);
            this._label1.TabIndex = 1;
            this._label1.Text = "Regex:";
            // 
            // Strings
            // 
            this._strings.Anchor =
            ((System.Windows.Forms.AnchorStyles)
            (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
              | System.Windows.Forms.AnchorStyles.Right)));
            this._strings.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
            this._strings.Location = new System.Drawing.Point(144, 232);
            this._strings.Multiline = true;
            this._strings.Name = "_strings";
            this._strings.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._strings.Size = new System.Drawing.Size(856, 144);
            this._strings.TabIndex = 2;
            this._strings.Text = "(425) 123-4567\r\n(111) 555-1212";
            this._toolTip1.SetToolTip(this._strings, "Strings to use to test the regex");
            // 
            // label2
            // 
            this._label2.Location = new System.Drawing.Point(96, 232);
            this._label2.Name = "_label2";
            this._label2.Size = new System.Drawing.Size(48, 23);
            this._label2.TabIndex = 3;
            this._label2.Text = "Strings:";
            // 
            // IgnoreWhitespace
            // 
            this._ignoreWhitespace.Location = new System.Drawing.Point(8, 16);
            this._ignoreWhitespace.Name = "_ignoreWhitespace";
            this._ignoreWhitespace.Size = new System.Drawing.Size(116, 24);
            this._ignoreWhitespace.TabIndex = 4;
            this._ignoreWhitespace.Text = "IgnoreWhitespace";
            this._toolTip1.SetToolTip(this._ignoreWhitespace, "Ignore any whitespace or comments in the text");
            this._ignoreWhitespace.CheckedChanged += new System.EventHandler(this.IgnoreWhitespace_CheckedChanged);
            // 
            // button1
            // 
            this._button1.Location = new System.Drawing.Point(32, 312);
            this._button1.Name = "_button1";
            this._button1.TabIndex = 5;
            this._button1.Text = "Execute";
            this._toolTip1.SetToolTip(this._button1, "Execute this regex");
            this._button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this._label3.Location = new System.Drawing.Point(88, 512);
            this._label3.Name = "_label3";
            this._label3.Size = new System.Drawing.Size(48, 23);
            this._label3.TabIndex = 6;
            this._label3.Text = "Output:";
            // 
            // Output
            // 
            this._output.Anchor =
            ((System.Windows.Forms.AnchorStyles)
            ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
               | System.Windows.Forms.AnchorStyles.Left)
              | System.Windows.Forms.AnchorStyles.Right)));
            this._output.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
            this._output.Location = new System.Drawing.Point(144, 504);
            this._output.Multiline = true;
            this._output.Name = "_output";
            this._output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._output.Size = new System.Drawing.Size(856, 336);
            this._output.TabIndex = 7;
            this._output.Text = "";
            this._toolTip1.SetToolTip(this._output, "Output window");
            // 
            // IgnoreCase
            // 
            this._ignoreCase.Location = new System.Drawing.Point(8, 40);
            this._ignoreCase.Name = "_ignoreCase";
            this._ignoreCase.Size = new System.Drawing.Size(112, 24);
            this._ignoreCase.TabIndex = 8;
            this._ignoreCase.Text = "IgnoreCase";
            this._toolTip1.SetToolTip(this._ignoreCase, "Ignore the case of letters");
            // 
            // Compiled
            // 
            this._compiled.Location = new System.Drawing.Point(8, 64);
            this._compiled.Name = "_compiled";
            this._compiled.TabIndex = 9;
            this._compiled.Text = "Compiled";
            this._toolTip1.SetToolTip(this._compiled, "Compile to a custom engine");
            // 
            // ExplicitCapture
            // 
            this._explicitCapture.Location = new System.Drawing.Point(8, 88);
            this._explicitCapture.Name = "_explicitCapture";
            this._explicitCapture.TabIndex = 10;
            this._explicitCapture.Text = "ExplicitCapture";
            this._toolTip1.SetToolTip(this._explicitCapture, "Capture only with (?<name>) syntax");
            this._explicitCapture.CheckedChanged += new System.EventHandler(this.ExplicitCapture_CheckedChanged);
            // 
            // Singleline
            // 
            this._singleline.Location = new System.Drawing.Point(8, 136);
            this._singleline.Name = "_singleline";
            this._singleline.TabIndex = 11;
            this._singleline.Text = "Singleline";
            this._toolTip1.SetToolTip(this._singleline, "\".\" matches \"\\n\"");
            // 
            // Multiline
            // 
            this._multiline.Location = new System.Drawing.Point(8, 112);
            this._multiline.Name = "_multiline";
            this._multiline.TabIndex = 12;
            this._multiline.Text = "Multiline";
            this._toolTip1.SetToolTip(this._multiline, "^ and $ match beginning and end of any line");
            // 
            // label4
            // 
            this._label4.Location = new System.Drawing.Point(16, 72);
            this._label4.Name = "_label4";
            this._label4.Size = new System.Drawing.Size(48, 23);
            this._label4.TabIndex = 13;
            this._label4.Text = "Time:";
            // 
            // Elapsed
            // 
            this._elapsed.Location = new System.Drawing.Point(16, 88);
            this._elapsed.Name = "_elapsed";
            this._elapsed.TabIndex = 14;
            this._elapsed.Text = "";
            this._toolTip1.SetToolTip(this._elapsed, "Time it took to do the match");
            // 
            // label5
            // 
            this._label5.Location = new System.Drawing.Point(16, 24);
            this._label5.Name = "_label5";
            this._label5.TabIndex = 15;
            this._label5.Text = "Iterations";
            // 
            // Iterations
            // 
            this._iterations.Location = new System.Drawing.Point(16, 48);
            this._iterations.Name = "_iterations";
            this._iterations.TabIndex = 16;
            this._iterations.Text = "1";
            this._toolTip1.SetToolTip(this._iterations, "Number of iterations to use when timing ");
            // 
            // Interpret
            // 
            this._interpret.Location = new System.Drawing.Point(32, 280);
            this._interpret.Name = "_interpret";
            this._interpret.TabIndex = 17;
            this._interpret.Text = "Interpret";
            this._toolTip1.SetToolTip(this._interpret, "Interpret what this regex means");
            this._interpret.Click += new System.EventHandler(this.Interpret_Click);
            // 
            // Split
            // 
            this._split.Location = new System.Drawing.Point(32, 344);
            this._split.Name = "_split";
            this._split.TabIndex = 18;
            this._split.Text = "Split";
            this._toolTip1.SetToolTip(this._split, "Call Regex.Split()");
            this._split.Click += new System.EventHandler(this.Split_Click);
            // 
            // CompileTime
            // 
            this._compileTime.Location = new System.Drawing.Point(16, 128);
            this._compileTime.Name = "_compileTime";
            this._compileTime.TabIndex = 22;
            this._compileTime.Text = "";
            this._toolTip1.SetToolTip(this._compileTime, "Time it took to create the regex");
            // 
            // label33
            // 
            this._label33.Location = new System.Drawing.Point(16, 112);
            this._label33.Name = "_label33";
            this._label33.Size = new System.Drawing.Size(80, 23);
            this._label33.TabIndex = 21;
            this._label33.Text = "Compile Time:";
            // 
            // OneString
            // 
            this._oneString.Location = new System.Drawing.Point(16, 248);
            this._oneString.Name = "_oneString";
            this._oneString.Size = new System.Drawing.Size(120, 24);
            this._oneString.TabIndex = 23;
            this._oneString.Text = "Treat as one string";
            this._toolTip1.SetToolTip(this._oneString, "Pass strings in one block rather than as separate calls to regex");
            // 
            // menuItem2
            // 
            this._menuItem2.Index = 0;
            this._menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
            {
                this._menuItem4,
                this._menuItem5,
                this._menuItem6,
                this._menuItem7,
                this._menuItem8,
                this._menuItem9,
                this._menuItem10,
                this._menuItem11,
                this._menuItem12,
                this._menuItem13
            });
            this._menuItem2.Text = "Character Shortcuts";
            // 
            // menuItem4
            // 
            this._menuItem4.Index = 0;
            this._menuItem4.Text = "Bell - \\a";
            // 
            // menuItem5
            // 
            this._menuItem5.Index = 1;
            this._menuItem5.Text = "Tab - \\t";
            // 
            // menuItem6
            // 
            this._menuItem6.Index = 2;
            this._menuItem6.Text = "Carriage Return - \\r";
            // 
            // menuItem7
            // 
            this._menuItem7.Index = 3;
            this._menuItem7.Text = "Vertical Tab - \\v";
            // 
            // menuItem8
            // 
            this._menuItem8.Index = 4;
            this._menuItem8.Text = "Form Feed - \\f";
            // 
            // menuItem9
            // 
            this._menuItem9.Index = 5;
            this._menuItem9.Text = "Newline - \\n";
            // 
            // menuItem10
            // 
            this._menuItem10.Index = 6;
            this._menuItem10.Text = "Escape - \\e";
            // 
            // menuItem11
            // 
            this._menuItem11.Index = 7;
            this._menuItem11.Text = "ASCII Value - \\x<nn>";
            // 
            // menuItem12
            // 
            this._menuItem12.Index = 8;
            this._menuItem12.Text = "CTRL Char - \\C<letter>";
            // 
            // menuItem13
            // 
            this._menuItem13.Index = 9;
            this._menuItem13.Text = "Unicode Char - \\u<XXXX>";
            // 
            // menuItem3
            // 
            this._menuItem3.Index = 1;
            this._menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
            {
                this._menuItem14,
                this._menuItem15,
                this._menuItem68,
                this._menuItem16,
                this._menuItem17,
                this._menuItem18,
                this._menuItem19,
                this._menuItem20,
                this._menuItem21,
                this._menuItem22
            });
            this._menuItem3.Text = "Character Classes";
            // 
            // menuItem14
            // 
            this._menuItem14.Index = 0;
            this._menuItem14.Text = "Group - [<chars>]";
            // 
            // menuItem15
            // 
            this._menuItem15.Index = 1;
            this._menuItem15.Text = "Negated group - [^<chars>]";
            // 
            // menuItem68
            // 
            this._menuItem68.Index = 2;
            this._menuItem68.Text = "-";
            // 
            // menuItem16
            // 
            this._menuItem16.Index = 3;
            this._menuItem16.Text = "Any - .";
            // 
            // menuItem17
            // 
            this._menuItem17.Index = 4;
            this._menuItem17.Text = "Word [a-zA-Z_0-9] - \\w";
            // 
            // menuItem18
            // 
            this._menuItem18.Index = 5;
            this._menuItem18.Text = "Non-Word - \\W";
            // 
            // menuItem19
            // 
            this._menuItem19.Index = 6;
            this._menuItem19.Text = "Whitespace [\\f\\n\\r\t\\v] - \\s";
            // 
            // menuItem20
            // 
            this._menuItem20.Index = 7;
            this._menuItem20.Text = "Non-Whitespace - \\S";
            // 
            // menuItem21
            // 
            this._menuItem21.Index = 8;
            this._menuItem21.Text = "Decimal digit - \\d";
            // 
            // menuItem22
            // 
            this._menuItem22.Index = 9;
            this._menuItem22.Text = "Non-decimal digit - \\D";
            // 
            // menuItem23
            // 
            this._menuItem23.Index = 2;
            this._menuItem23.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
            {
                this._menuItem24,
                this._menuItem25,
                this._menuItem26,
                this._menuItem27,
                this._menuItem28,
                this._menuItem29,
                this._menuItem69,
                this._menuItem30,
                this._menuItem31,
                this._menuItem32,
                this._menuItem33,
                this._menuItem34,
                this._menuItem35
            });
            this._menuItem23.Text = "Quantifiers";
            // 
            // menuItem24
            // 
            this._menuItem24.Index = 0;
            this._menuItem24.Text = "Zero or more - *";
            // 
            // menuItem25
            // 
            this._menuItem25.Index = 1;
            this._menuItem25.Text = "One or more - +";
            // 
            // menuItem26
            // 
            this._menuItem26.Index = 2;
            this._menuItem26.Text = "Zero or one - ?";
            // 
            // menuItem27
            // 
            this._menuItem27.Index = 3;
            this._menuItem27.Text = "From n to m - {<n>,<m>}";
            // 
            // menuItem28
            // 
            this._menuItem28.Index = 4;
            this._menuItem28.Text = "At least n - {<n>,}";
            // 
            // menuItem29
            // 
            this._menuItem29.Index = 5;
            this._menuItem29.Text = "Exactly n - {<n>}";
            // 
            // menuItem69
            // 
            this._menuItem69.Index = 6;
            this._menuItem69.Text = "-";
            // 
            // menuItem30
            // 
            this._menuItem30.Index = 7;
            this._menuItem30.Text = "Zero or more non greedy - *?";
            // 
            // menuItem31
            // 
            this._menuItem31.Index = 8;
            this._menuItem31.Text = "One or more non greedy - +?";
            // 
            // menuItem32
            // 
            this._menuItem32.Index = 9;
            this._menuItem32.Text = "Zero or one non greedy - ??";
            // 
            // menuItem33
            // 
            this._menuItem33.Index = 10;
            this._menuItem33.Text = "From n to m non greedy - {<n>,<m>}?";
            // 
            // menuItem34
            // 
            this._menuItem34.Index = 11;
            this._menuItem34.Text = "At least n non greedy - {<n>,}?";
            // 
            // menuItem35
            // 
            this._menuItem35.Index = 12;
            this._menuItem35.Text = "Exactly n non greedy - {<n>}?";
            // 
            // menuItem36
            // 
            this._menuItem36.Index = 3;
            this._menuItem36.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
            {
                this._menuItem37,
                this._menuItem38,
                this._menuItem39,
                this._menuItem40,
                this._menuItem41,
                this._menuItem42,
                this._menuItem43
            });
            this._menuItem36.Text = "Anchors";
            // 
            // menuItem37
            // 
            this._menuItem37.Index = 0;
            this._menuItem37.Text = "Beginning of string - ^";
            // 
            // menuItem38
            // 
            this._menuItem38.Index = 1;
            this._menuItem38.Text = "Beginning, multiline - \\A";
            // 
            // menuItem39
            // 
            this._menuItem39.Index = 2;
            this._menuItem39.Text = "End of string - $";
            // 
            // menuItem40
            // 
            this._menuItem40.Index = 3;
            this._menuItem40.Text = "End, multiline - \\Z";
            // 
            // menuItem41
            // 
            this._menuItem41.Index = 4;
            this._menuItem41.Text = "End, multiline -  \\z";
            // 
            // menuItem42
            // 
            this._menuItem42.Index = 5;
            this._menuItem42.Text = "Word boundary - \\b";
            // 
            // menuItem43
            // 
            this._menuItem43.Index = 6;
            this._menuItem43.Text = "Non-word boundary - \\B";
            // 
            // menuItem44
            // 
            this._menuItem44.Index = 4;
            this._menuItem44.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
            {
                this._menuItem45,
                this._menuItem46,
                this._menuItem47,
                this._menuItem53
            });
            this._menuItem44.Text = "Grouping";
            // 
            // menuItem45
            // 
            this._menuItem45.Index = 0;
            this._menuItem45.Text = "Capture - (<exp>)";
            // 
            // menuItem46
            // 
            this._menuItem46.Index = 1;
            this._menuItem46.Text = "Named capture - (?<<name>>x)";
            // 
            // menuItem47
            // 
            this._menuItem47.Index = 2;
            this._menuItem47.Text = "Non-capture - (?:<exp>)";
            // 
            // menuItem53
            // 
            this._menuItem53.Index = 3;
            this._menuItem53.Text = "Alternation - (<x>|<y>)";
            // 
            // menuItem48
            // 
            this._menuItem48.Index = 5;
            this._menuItem48.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
            {
                this._menuItem49,
                this._menuItem50,
                this._menuItem51,
                this._menuItem52
            });
            this._menuItem48.Text = "Zero-Width";
            // 
            // menuItem49
            // 
            this._menuItem49.Index = 0;
            this._menuItem49.Text = "Positive Lookahead - (?=<x>)";
            // 
            // menuItem50
            // 
            this._menuItem50.Index = 1;
            this._menuItem50.Text = "Negative Lookahead - (?!<x>)";
            // 
            // menuItem51
            // 
            this._menuItem51.Index = 2;
            this._menuItem51.Text = "Positive Lookbehind - (?<=<x>)";
            // 
            // menuItem52
            // 
            this._menuItem52.Index = 3;
            this._menuItem52.Text = "Negative Lookbehind - (?<!<x>)";
            // 
            // menuItem54
            // 
            this._menuItem54.Index = 6;
            this._menuItem54.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
            {
                this._menuItem55,
                this._menuItem56
            });
            this._menuItem54.Text = "Conditionals";
            // 
            // menuItem55
            // 
            this._menuItem55.Index = 0;
            this._menuItem55.Text = "Expression - (?(<exp>)yes|no)";
            // 
            // menuItem56
            // 
            this._menuItem56.Index = 1;
            this._menuItem56.Text = "Named - (?(<name>)yes|no)";
            // 
            // menuItem57
            // 
            this._menuItem57.Index = 7;
            this._menuItem57.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
            {
                this._menuItem58,
                this._menuItem59,
                this._menuItem60,
                this._menuItem61,
                this._menuItem62,
                this._menuItem63,
                this._menuItem64,
                this._menuItem65,
                this._menuItem66,
                this._menuItem67
            });
            this._menuItem57.Text = "Options";
            // 
            // menuItem58
            // 
            this._menuItem58.Index = 0;
            this._menuItem58.Text = "Ignore Case - (?i:)";
            // 
            // menuItem59
            // 
            this._menuItem59.Index = 1;
            this._menuItem59.Text = "Ignore Case off - (?-i:)";
            // 
            // menuItem60
            // 
            this._menuItem60.Index = 2;
            this._menuItem60.Text = "Multline - (?m:)";
            // 
            // menuItem61
            // 
            this._menuItem61.Index = 3;
            this._menuItem61.Text = "Multiline off - (?-m:)";
            // 
            // menuItem62
            // 
            this._menuItem62.Index = 4;
            this._menuItem62.Text = "Explicit Capture - (?n:)";
            // 
            // menuItem63
            // 
            this._menuItem63.Index = 5;
            this._menuItem63.Text = "Explicit Capture off - (?-n:)";
            // 
            // menuItem64
            // 
            this._menuItem64.Index = 6;
            this._menuItem64.Text = "Singleline - (?s:)";
            // 
            // menuItem65
            // 
            this._menuItem65.Index = 7;
            this._menuItem65.Text = "Singleline off - (?-s:)";
            // 
            // menuItem66
            // 
            this._menuItem66.Index = 8;
            this._menuItem66.Text = "Ignore Whitespace - (?x:)";
            // 
            // menuItem67
            // 
            this._menuItem67.Index = 9;
            this._menuItem67.Text = "Ignore Whitespace off - (?-x:)";
            // 
            // BackReferenceMenuItem
            // 
            this._backReferenceMenuItem.Index = 8;
            this._backReferenceMenuItem.Text = "Backreferences";
            // 
            // HoverInterpret
            // 
            this._hoverInterpret.Checked = true;
            this._hoverInterpret.CheckState = System.Windows.Forms.CheckState.Checked;
            this._hoverInterpret.Location = new System.Drawing.Point(16, 8);
            this._hoverInterpret.Name = "_hoverInterpret";
            this._hoverInterpret.TabIndex = 27;
            this._hoverInterpret.Text = "Hover Interpret";
            this._toolTip1.SetToolTip(this._hoverInterpret, "Display interpretation in a popup window");
            // 
            // Description
            // 
            this._description.Location = new System.Drawing.Point(312, 8);
            this._description.Name = "_description";
            this._description.Size = new System.Drawing.Size(688, 20);
            this._description.TabIndex = 29;
            this._description.Text = "";
            this._toolTip1.SetToolTip(this._description, "Description for this regular expression");
            // 
            // HideGroupZero
            // 
            this._hideGroupZero.Location = new System.Drawing.Point(16, 544);
            this._hideGroupZero.Name = "_hideGroupZero";
            this._hideGroupZero.TabIndex = 31;
            this._hideGroupZero.Text = "Hide Groups[0]";
            this._toolTip1.SetToolTip(this._hideGroupZero, "Hide Group[0] in the output window");
            // 
            // ReplaceString
            // 
            this._replaceString.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
            this._replaceString.Location = new System.Drawing.Point(144, 392);
            this._replaceString.Multiline = true;
            this._replaceString.Name = "_replaceString";
            this._replaceString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._replaceString.Size = new System.Drawing.Size(856, 96);
            this._replaceString.TabIndex = 36;
            this._replaceString.Text = "";
            this._toolTip1.SetToolTip(this._replaceString, "Replace text or MatchEvaluator");
            // 
            // Replace
            // 
            this._replace.Location = new System.Drawing.Point(32, 432);
            this._replace.Name = "_replace";
            this._replace.TabIndex = 38;
            this._replace.Text = "Replace";
            this._toolTip1.SetToolTip(this._replace, "Call Regex.Replace");
            this._replace.Click += new System.EventHandler(this.Replace_Click);
            // 
            // MatchEvaluator
            // 
            this._matchEvaluator.Location = new System.Drawing.Point(16, 464);
            this._matchEvaluator.Name = "MatchEvaluator";
            this._matchEvaluator.TabIndex = 39;
            this._matchEvaluator.Text = "MatchEvaluator";
            this._toolTip1.SetToolTip(this._matchEvaluator, "Write an implementation of a MatchEvaluator");
            this._matchEvaluator.CheckedChanged += new System.EventHandler(this.MatchEvaluator_CheckedChanged);
            // 
            // mainMenu1
            // 
            this._mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
            {
                this._menuItem70,
                this._menuItem73,
                this._addItem,
                this._library,
                this._about
            });
            // 
            // menuItem70
            // 
            this._menuItem70.Index = 0;
            this._menuItem70.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
            {
                this._saveRegex,
                this._makeAssemblyItem
            });
            this._menuItem70.Text = "File";
            // 
            // saveRegex
            // 
            this._saveRegex.Index = 0;
            this._saveRegex.Text = "Save Regex";
            this._saveRegex.Click += new System.EventHandler(this.saveRegex_Click);
            // 
            // makeAssemblyItem
            // 
            this._makeAssemblyItem.Index = 1;
            this._makeAssemblyItem.Text = "Make Assembly";
            this._makeAssemblyItem.Click += new System.EventHandler(this.makeAssemblyItem_Click);
            // 
            // menuItem73
            // 
            this._menuItem73.Index = 1;
            this._menuItem73.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
            {
                this._copyAsCSharp,
                this._copyAsVb,
                this._pasteFromCSharp
            });
            this._menuItem73.Text = "Edit";
            // 
            // copyAsCSharp
            // 
            this._copyAsCSharp.Index = 0;
            this._copyAsCSharp.Text = "Copy as C#";
            this._copyAsCSharp.Click += new System.EventHandler(this.copyAsCSharp_Click);
            // 
            // copyAsVB
            // 
            this._copyAsVb.Index = 1;
            this._copyAsVb.Text = "Copy as VB";
            this._copyAsVb.Click += new System.EventHandler(this.copyAsVB_Click);
            // 
            // pasteFromCSharp
            // 
            this._pasteFromCSharp.Index = 2;
            this._pasteFromCSharp.Text = "Paste from C#";
            this._pasteFromCSharp.Click += new System.EventHandler(this.pasteFromCSharp_Click);
            // 
            // addItem
            // 
            this._addItem.Index = 2;
            this._addItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
            {
                this._menuItem2,
                this._menuItem3,
                this._menuItem23,
                this._menuItem36,
                this._menuItem44,
                this._menuItem48,
                this._menuItem54,
                this._menuItem57,
                this._backReferenceMenuItem
            });
            this._addItem.Text = "Add Item";
            this._addItem.Popup += new System.EventHandler(this.addItem_Popup);
            this._addItem.Click += new System.EventHandler(this.addItem_Click);
            // 
            // library
            // 
            this._library.Index = 3;
            this._library.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
            {
                this._menuItem1
            });
            this._library.Text = "Library";
            this._library.Popup += new System.EventHandler(this.library_Popup);
            // 
            // menuItem1
            // 
            this._menuItem1.Index = 0;
            this._menuItem1.Text = "Fake Item";
            // 
            // about
            // 
            this._about.Index = 4;
            this._about.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
            {
                this._menuItem71,
                this._menuReleaseNotes
            });
            this._about.Text = "About";
            // 
            // groupBox1
            // 
            this._groupBox1.Controls.Add(this._ignoreWhitespace);
            this._groupBox1.Controls.Add(this._multiline);
            this._groupBox1.Controls.Add(this._explicitCapture);
            this._groupBox1.Controls.Add(this._compiled);
            this._groupBox1.Controls.Add(this._singleline);
            this._groupBox1.Controls.Add(this._ignoreCase);
            this._groupBox1.Location = new System.Drawing.Point(8, 32);
            this._groupBox1.Name = "_groupBox1";
            this._groupBox1.Size = new System.Drawing.Size(128, 168);
            this._groupBox1.TabIndex = 28;
            this._groupBox1.TabStop = false;
            this._groupBox1.Text = "Options";
            // 
            // label6
            // 
            this._label6.Location = new System.Drawing.Point(232, 8);
            this._label6.Name = "_label6";
            this._label6.Size = new System.Drawing.Size(72, 23);
            this._label6.TabIndex = 30;
            this._label6.Text = "Description:";
            // 
            // groupBox2
            // 
            this._groupBox2.Controls.Add(this._compileTime);
            this._groupBox2.Controls.Add(this._label33);
            this._groupBox2.Controls.Add(this._elapsed);
            this._groupBox2.Controls.Add(this._label5);
            this._groupBox2.Controls.Add(this._iterations);
            this._groupBox2.Controls.Add(this._label4);
            this._groupBox2.Location = new System.Drawing.Point(8, 640);
            this._groupBox2.Name = "_groupBox2";
            this._groupBox2.Size = new System.Drawing.Size(128, 168);
            this._groupBox2.TabIndex = 32;
            this._groupBox2.TabStop = false;
            this._groupBox2.Text = "Timing";
            // 
            // panel1
            // 
            this._panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panel1.Controls.Add(this._panel2);
            this._panel1.Location = new System.Drawing.Point(8, 384);
            this._panel1.Name = "_panel1";
            this._panel1.Size = new System.Drawing.Size(984, 1);
            this._panel1.TabIndex = 33;
            // 
            // panel2
            // 
            this._panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panel2.Controls.Add(this._panel3);
            this._panel2.Location = new System.Drawing.Point(-1, 0);
            this._panel2.Name = "_panel2";
            this._panel2.Size = new System.Drawing.Size(984, 1);
            this._panel2.TabIndex = 34;
            // 
            // panel3
            // 
            this._panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panel3.Location = new System.Drawing.Point(-1, 40);
            this._panel3.Name = "_panel3";
            this._panel3.Size = new System.Drawing.Size(984, 1);
            this._panel3.TabIndex = 34;
            // 
            // panel4
            // 
            this._panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panel4.Controls.Add(this._panel5);
            this._panel4.Location = new System.Drawing.Point(8, 496);
            this._panel4.Name = "_panel4";
            this._panel4.Size = new System.Drawing.Size(984, 1);
            this._panel4.TabIndex = 34;
            // 
            // panel5
            // 
            this._panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panel5.Controls.Add(this._panel6);
            this._panel5.Location = new System.Drawing.Point(-1, 0);
            this._panel5.Name = "_panel5";
            this._panel5.Size = new System.Drawing.Size(984, 1);
            this._panel5.TabIndex = 34;
            // 
            // panel6
            // 
            this._panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panel6.Location = new System.Drawing.Point(-1, 40);
            this._panel6.Name = "_panel6";
            this._panel6.Size = new System.Drawing.Size(984, 1);
            this._panel6.TabIndex = 34;
            // 
            // label7
            // 
            this._label7.Location = new System.Drawing.Point(80, 400);
            this._label7.Name = "_label7";
            this._label7.Size = new System.Drawing.Size(56, 16);
            this._label7.TabIndex = 35;
            this._label7.Text = "Replace:";
            // 
            // panel7
            // 
            this._panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panel7.Controls.Add(this._panel8);
            this._panel7.Location = new System.Drawing.Point(8, 224);
            this._panel7.Name = "_panel7";
            this._panel7.Size = new System.Drawing.Size(984, 1);
            this._panel7.TabIndex = 37;
            // 
            // panel8
            // 
            this._panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panel8.Controls.Add(this._panel9);
            this._panel8.Location = new System.Drawing.Point(-1, 0);
            this._panel8.Name = "_panel8";
            this._panel8.Size = new System.Drawing.Size(984, 1);
            this._panel8.TabIndex = 34;
            // 
            // panel9
            // 
            this._panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panel9.Location = new System.Drawing.Point(-1, 40);
            this._panel9.Name = "_panel9";
            this._panel9.Size = new System.Drawing.Size(984, 1);
            this._panel9.TabIndex = 34;
            // 
            // menuItem71
            // 
            this._menuItem71.Index = 0;
            this._menuItem71.Text = "About Regular Expression Workbench";
            this._menuItem71.Click += new System.EventHandler(this.menuItem71_Click);
            // 
            // menuReleaseNotes
            // 
            this._menuReleaseNotes.Index = 1;
            this._menuReleaseNotes.Text = "Release Notes";
            this._menuReleaseNotes.Click += new System.EventHandler(this.menuReleaseNotes_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1008, 837);
            this.Controls.Add(this._matchEvaluator);
            this.Controls.Add(this._replace);
            this.Controls.Add(this._panel7);
            this.Controls.Add(this._replaceString);
            this.Controls.Add(this._description);
            this.Controls.Add(this._output);
            this.Controls.Add(this._strings);
            this.Controls.Add(this._regexText);
            this.Controls.Add(this._label7);
            this.Controls.Add(this._panel4);
            this.Controls.Add(this._panel1);
            this.Controls.Add(this._groupBox2);
            this.Controls.Add(this._hideGroupZero);
            this.Controls.Add(this._label6);
            this.Controls.Add(this._groupBox1);
            this.Controls.Add(this._hoverInterpret);
            this.Controls.Add(this._oneString);
            this.Controls.Add(this._split);
            this.Controls.Add(this._interpret);
            this.Controls.Add(this._button1);
            this.Controls.Add(this._label1);
            this.Controls.Add(this._label2);
            this.Controls.Add(this._label3);
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.Menu = this._mainMenu1;
            this.Name = "Form1";
            this.Text = "Regular Expression Workbench";
            this.Load += new System.EventHandler(this.Form1_Load);
            this._groupBox1.ResumeLayout(false);
            this._groupBox2.ResumeLayout(false);
            this._panel1.ResumeLayout(false);
            this._panel2.ResumeLayout(false);
            this._panel4.ResumeLayout(false);
            this._panel5.ResumeLayout(false);
            this._panel7.ResumeLayout(false);
            this._panel8.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.Run(new Form1());
        }

        private void SaveValues()
        {
            SaveRegex(Environment.GetFolderPath(Environment.SpecialFolder.Personal) +
                      "\\regex.xml");
        }


        private RegexOptions CreateRegexOptions()
        {
            var regOp = new RegexOptions();
            if (_ignoreWhitespace.Checked)
                regOp |= RegexOptions.IgnorePatternWhitespace;
            if (_ignoreCase.Checked)
                regOp |= RegexOptions.IgnoreCase;
            if (_compiled.Checked)
                regOp |= RegexOptions.Compiled;
            if (_explicitCapture.Checked)
                regOp |= RegexOptions.ExplicitCapture;
            if (_singleline.Checked)
                regOp |= RegexOptions.Singleline;
            if (_multiline.Checked)
                regOp |= RegexOptions.Multiline;

            return regOp;
        }

        private Regex CreateRegex()
        {
            var regOp = CreateRegexOptions();
            return new Regex(_regexText.Text, regOp);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveValues();

            Regex regex;
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                regex = CreateRegex();
                stopwatch.Stop();
                _compileTime.Text = stopwatch.Elapsed.TotalSeconds.ToString();
            }
            catch (Exception ex)
            {
                _output.Text = ex.ToString();
                return;
            }

            var groupNames = regex.GetGroupNames();

            string[] strings;
            // if checked, pass all lines as a single block
            if (_oneString.Checked)
            {
                strings = new string[1];
                strings[0] = _strings.Text;
            }
            else
            {
                strings = Regex.Split(_strings.Text, @"\r\n");
                //strings = Strings.Text.Split('\n\r');
            }

            var outString = new StringBuilder();
            foreach (var s in strings)
            {
                outString.Append(string.Format("Matching: {0}\r\n", s));

                _elapsed.Text = "";
                var iterations = Convert.ToInt32(_iterations.Text);
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                Match m;
                for (var i = 0; i < iterations; i++)
                {
                    m = regex.Match(s);
                    while (m.Success)
                        m = m.NextMatch();
                }
                stopwatch.Stop();
                _elapsed.Text = string.Format("{0:f2}", stopwatch.Elapsed.TotalSeconds);

                m = regex.Match(s);
                var noMatch = true;
                while (m.Success)
                {
                    noMatch = false;

                    var groupNumber = 0;
                    foreach (Group group in m.Groups)
                    {
                        foreach (Capture capture in group.Captures)
                            if (_hideGroupZero.Checked == false ||
                                groupNames[groupNumber] != "0")
                                outString.Append(string.Format("    {0} => {1}\r\n", groupNames[groupNumber], capture));
                        groupNumber++;
                    }

                    m = m.NextMatch();
                }
                if (noMatch)
                    outString.Append("    No Match\r\n");
            }
            _output.Text = outString.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var filename =
                string.Format(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\regex.xml");

            if (File.Exists(filename))
                LoadRegex(filename);

            var g = Graphics.FromHwnd(Handle);
            _characterSize = g.MeasureString(_regexText.Text, _regexText.Font);
            _characterSize.Width /= _regexText.Text.Length;
        }

        // Go from commented regex version to non-commented version

        private void Interpret_Click(object sender, EventArgs e)
        {
            SaveValues();

            _buffer = new RegexBuffer(_regexText.Text, CreateRegexOptions());
            try
            {
                var exp = new RegexExpression(_buffer);

                _output.Text = exp.ToString(0);
            }
            catch (Exception ex)
            {
                if (_buffer.ErrorLocation != -1)
                {
                    _regexText.SelectionStart = _buffer.ErrorLocation;
                    _regexText.SelectionLength = _buffer.ErrorLength;
                }
                _output.Text =
                    "Error intepreting regex\r\n" + ex.Message;
                _regexText.Focus();
            }
        }

        private void Split_Click(object sender, EventArgs e)
        {
            SaveValues();
            Regex regex;
            try
            {
                regex = CreateRegex();
            }
            catch (Exception ex)
            {
                _output.Text = ex.ToString();
                return;
            }

            string[] strings;
            // if checked, pass all lines as a single block
            if (_oneString.Checked)
            {
                strings = new string[1];
                strings[0] = _strings.Text;
            }
            else
            {
                strings = Regex.Split(_strings.Text, @"\r\n");
                //strings = Strings.Text.Split('\n\r');
            }

            var outString = new StringBuilder();
            foreach (var s in strings)
            {
                outString.Append(string.Format("Splitting: {0}\r\n", s));

                var arr = regex.Split(s);

                var index = 0;
                foreach (var split in arr)
                {
                    outString.Append(string.Format("    [{0}] => {1}\r\n", index, split));
                    index++;
                }
            }
            _output.Text = outString.ToString();
        }

        private void Replace_Click(object sender, EventArgs e)
        {
            SaveValues();
            Regex regex;
            try
            {
                regex = CreateRegex();
            }
            catch (Exception ex)
            {
                _output.Text = ex.ToString();
                return;
            }

            string[] strings;
            // if checked, pass all lines as a single block
            if (_oneString.Checked)
            {
                strings = new string[1];
                strings[0] = _strings.Text;
            }
            else
            {
                strings = Regex.Split(_strings.Text, @"\r\n");
                //strings = Strings.Text.Split('\n\r');
            }

            ReplaceMatchEvaluator replacer = null;

            if (_matchEvaluator.Checked)
            {
                replacer = new ReplaceMatchEvaluator(_replaceString.Text);
                var output = replacer.CreateAndLoadClass();
                if (output != null)
                {
                    _output.Text = output;
                    return;
                }
            }

            var outString = new StringBuilder();
            var replace = _replaceString.Text;
            foreach (var s in strings)
            {
                outString.Append(string.Format("Replacing: {0}\r\n", s));

                string output;
                if (_matchEvaluator.Checked)
                {
                    outString.Append("  with a custom MatchEvaluator\r\n");
                    output = regex.Replace(s, replacer.MatchEvaluator);
                }
                else
                {
                    outString.Append(string.Format("  with: {0}\r\n", replace));
                    output = regex.Replace(s, replace);
                }
                outString.Append(string.Format("  result: {0}\r\n", output));
            }
            _output.Text = outString.ToString();
        }

        private void makeAssemblyItem_Click(object sender, EventArgs e)
        {
            var dialog = new MakeAssemblyDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                RegexCompilationInfo[] regexCompInfo =
                {
                    new RegexCompilationInfo(
                        _regexText.Text,
                        CreateRegexOptions(), dialog.TypeName.Text, dialog.Namespace.Text, dialog.CreatePublic.Checked)
                };

                var assemblyName = new AssemblyName();
                assemblyName.Name = dialog.AssemblyName.Text;
                Regex.CompileToAssembly(regexCompInfo, assemblyName);
            }
        }

        private string MakeCSharpString()
        {
            var s =
                "Regex regex = new Regex(@\"\r\n" +
                _regexText.Text.Replace("\"", "\"\"") +
                "\", \r\n";

            var optionStrings = new ArrayList();
            var options = CreateRegexOptions();

            foreach (RegexOptions option in Enum.GetValues(typeof(RegexOptions)))
                if ((options & option) != 0)
                    optionStrings.Add("RegexOptions." + Enum.GetName(typeof(RegexOptions), option));

            if (optionStrings.Count != 0)
                s += string.Join(" | \r\n", (string[]) optionStrings.ToArray(typeof(string)));
            else
                s += " (RegexOptions) 0";
            s += ");";

            s = s.Replace("\n", "\n\t\t\t\t");
            return s;
#if fred
Regex r = new Regex(
				@"(?<Field>         # capture to field
				[^,""]+              # one or more not , or ""
				|                 # or
				""[^""]+              # "" and one or more not , or ""
				""),               # end capture", 
				RegexOptions.IgnorePatternWhitespace);
			
			#endif
        }

        private void RegexText_Leave(object sender, EventArgs e)
        {
            _regexInsertionPoint = _regexText.SelectionStart;
        }

        // this routine is called by both the Add Item menu and the
        // pick on the context menu.
        // Rather than figure out which, we just update both. 
        // (yes, I know, it's inefficient)
        private void addItem_Popup(object sender, EventArgs e)
        {
            UpdateBackreferences(_addItem, _backReferenceMenuItem);
            UpdateBackreferences(_contextMenuAddItems, _contextMenuBackreferences);
        }

        private void UpdateBackreferences(MenuItem addItem, MenuItem backReferenceMenuItem)
        {
            // BUG:
            // If you clear a menu item and then add items back into
            // it, the newly added items don't show up. If you
            // remove it from the context menu and then add it back
            // it works fine. 
            addItem.MenuItems.Remove(backReferenceMenuItem);

            backReferenceMenuItem.MenuItems.Clear();

            var regexCapture = new Regex(
                @"\(\?<
				(?<Name>.+?)
				>.+?\)
				",
                RegexOptions.ExplicitCapture |
                RegexOptions.IgnorePatternWhitespace);

            var m = regexCapture.Match(_regexText.Text);

            while (m.Success)
            {
                var name = m.Groups["Name"].Value;
                var item = string.Format("{0} - \\k<{1}>", name, name);
                var menuItem = new MenuItem(item, addItem_Click);
                backReferenceMenuItem.MenuItems.Add(menuItem);
                m = m.NextMatch();
            }

            // now match any unnamed captures, and add
            // references for them...
            if (!_explicitCapture.Checked)
            {
                var regexUnnamed = new Regex(
                    @"\(            # Opening (
				(?!           # Not followed by
				 (\?<|\?:))   #     ""?<"" or ""?:""s
				",
                    RegexOptions.ExplicitCapture |
                    RegexOptions.IgnorePatternWhitespace);

                m = regexUnnamed.Match(_regexText.Text);

                var number = 1;
                while (m.Success)
                {
                    var item = string.Format("{0} - \\<{1}>", number, number);
                    var menuItem = new MenuItem(item, addItem_Click);
                    backReferenceMenuItem.MenuItems.Add(menuItem);
                    number++;
                    m = m.NextMatch();
                }
            }
            addItem.MenuItems.Add(backReferenceMenuItem);
        }


        private void addItem_Click(object sender, EventArgs e)
        {
            var menuItem = (MenuItem) sender;

            var regexBreak = new Regex(".+ - (?<Placeholder>.+)");
            var match = regexBreak.Match(menuItem.Text);
            if (match.Success)
            {
                var insert = match.Groups["Placeholder"].ToString();
                _regexInsertionPoint = _regexText.SelectionStart;
                string start;
                string end;

                //try
                //{
                start = _regexText.Text.Substring(0, _regexInsertionPoint);
                end = _regexText.Text.Substring(_regexInsertionPoint);
                //}
                //catch (Exception)
                //	{
                //		// anything bad happens, don't worry about the rest of the string...
                //	}
                _regexText.Text = start + insert + end;

                var regexSelect = new Regex("(?<Select><[^<]+?>)");
                match = regexSelect.Match(insert);
                if (match.Success)
                {
                    var g = match.Groups["Select"];
                    _regexText.SelectionStart =
                        _regexInsertionPoint + g.Index;
                    _regexText.SelectionLength = g.Length;
                }
                else
                {
                    _regexText.SelectionStart = _regexInsertionPoint;
                }
                _regexText.Focus();
            }
        }

        private string MakeVbString()
        {
#if fred
   Dim r As Regex
      Dim m As Match
    
      r = New Regex("href\s*=\s*(?:""(?<1>[^""]*)""|(?<1>\S+))", _
         RegexOptions.IgnoreCase Or RegexOptions.Compiled)
    
      m = r.Match(inputString)
      While m.Success
         Console.WriteLine("Found href " & m.Groups(1).Value _
            & " at " & m.Groups(1).Index.ToString())
         m = m.NextMatch()
      End While
   End Sub
#endif
            var s =
                "Dim r as Regex\r\n\r\n" +
                "r = new Regex( _\r\n";

            var splitter = new Regex("\r\n");
            var lines = splitter.Split(_regexText.Text);

            s += "\"";
            s += string.Join("\" + _ \r\n\"", lines);
            s += "\" _\r\n";

            var optionStrings = new ArrayList();
            var options = CreateRegexOptions();

            foreach (RegexOptions option in Enum.GetValues(typeof(RegexOptions)))
                if ((options & option) != 0)
                    optionStrings.Add("RegexOptions." + Enum.GetName(typeof(RegexOptions), option));

            if (optionStrings.Count != 0)
            {
                s += ", ";
                s += string.Join(" Or ", (string[]) optionStrings.ToArray(typeof(string)));
            }

            s += ")";


            return s;
        }


        private void UpdateBuffer()
        {
            if (_bufferDirty)
            {
                _buffer = new RegexBuffer(_regexText.Text, CreateRegexOptions());
                _bufferDirty = false;
            }
        }

        private HoverDetailAction RegexText_HoverDetail(object sender, HoverEventArgs args)
        {
            HoverDetailAction action = null;
            if (!_hoverInterpret.Checked)
                return null;

            UpdateBuffer();
            try
            {
                new RegexExpression(_buffer);
            }
            catch (Exception e)
            {
                _output.Text = e.ToString();
                return null;
            }

            var regexRef = _buffer.ExpressionLookup.MatchLocations(args.CharacterOffset);
            if (regexRef != null)
                action = new HoverDetailAction(regexRef.Start, regexRef.Length, regexRef.StringValue);
            return action;
        }

        private void RegexText_Enter(object sender, EventArgs e)
        {
            _bufferDirty = true;
        }

        private void RegexText_TextChanged_1(object sender, EventArgs e)
        {
            _bufferDirty = true;
        }

        private void IgnoreWhitespace_CheckedChanged(object sender, EventArgs e)
        {
            _bufferDirty = true;
        }

        private void ExplicitCapture_CheckedChanged(object sender, EventArgs e)
        {
            _bufferDirty = true;
        }

        // get the contents of a C# regex, and make it nicer...
        private void pasteFromCSharp_Click(object sender, EventArgs e)
        {
            var clipboard = Clipboard.GetDataObject();
            var value = (string) clipboard.GetData(typeof(string));

            // first, get rid of the "Regex regex line, if it exists"
            var regex2 = new Regex(@"
				^.+?new\ Regex\(@""(?<Rest>.+)
				",
                RegexOptions.Multiline |
                RegexOptions.ExplicitCapture |
                RegexOptions.Singleline |
                RegexOptions.IgnorePatternWhitespace);

            var m = regex2.Match(value);
            if (m.Success)
                value = m.Groups["Rest"].Value;

            // get rid of the leading whitespace on each line...
            var regex = new Regex(@"
				^\s+
				",
                RegexOptions.Multiline |
                RegexOptions.ExplicitCapture |
                RegexOptions.IgnorePatternWhitespace);

            value = regex.Replace(value, "");

            // see if there is a " and options after the string...
            var regex3 = new Regex(@"
				(?<Pattern>.+)^\s*"",(?<Rest>.+)
				",
                RegexOptions.Multiline |
                RegexOptions.ExplicitCapture |
                RegexOptions.Singleline |
                RegexOptions.IgnorePatternWhitespace);

            m = regex3.Match(value);
            if (m.Success)
            {
                value = m.Groups["Pattern"].Value;

                // clear all the patterns, and then set the ones
                // that are on...
                _ignoreCase.Checked = false;
                _ignoreWhitespace.Checked = false;
                _multiline.Checked = false;
                _singleline.Checked = false;
                _compiled.Checked = false;
                _explicitCapture.Checked = false;

                var rest = m.Groups["Rest"].Value;
                if (rest.IndexOf("IgnoreCase") != -1)
                    _ignoreCase.Checked = true;

                if (rest.IndexOf("IgnorePatternWhitespace") != -1)
                    _ignoreWhitespace.Checked = true;

                if (rest.IndexOf("Multiline") != -1)
                    _multiline.Checked = true;

                if (rest.IndexOf("Singleline") != -1)
                    _singleline.Checked = true;

                if (rest.IndexOf("Compiled") != -1)
                    _compiled.Checked = true;

                if (rest.IndexOf("ExplicitCapture") != -1)
                    _explicitCapture.Checked = true;
            }

            // change any double "" to "
            _regexText.Text = value.Replace("\"\"", "\"");
        }

        private void copyAsCSharp_Click(object sender, EventArgs e)
        {
            var csharpSource = MakeCSharpString();
            Clipboard.SetDataObject(csharpSource);
            _output.Text = csharpSource;
        }

        private void copyAsVB_Click(object sender, EventArgs e)
        {
            var vbSource = MakeVbString();
            Clipboard.SetDataObject(vbSource);
            _output.Text = vbSource;
        }

        private void saveRegex_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory("library");
            var saveRegexDialog = new SaveRegex();
            if (saveRegexDialog.ShowDialog() == DialogResult.OK)
                SaveRegex(saveRegexDialog.Filename.Text);
        }

        private void libraryItem_Click(object sender, EventArgs e)
        {
            var menuItem = (LibraryMenuItem) sender;
            LoadRegex(menuItem.Filename);
        }

        private void CreateLibrary(DirectoryInfo dirInfo, Menu.MenuItemCollection collection)
        {
            foreach (var subdir in dirInfo.GetDirectories())
            {
                var subMenuItem = new MenuItem(subdir.Name);
                collection.Add(subMenuItem);
                CreateLibrary(subdir, subMenuItem.MenuItems);
            }

            foreach (var fileInfo in dirInfo.GetFiles())
                if (fileInfo.Extension.ToLower() == ".regex")
                {
                    var fileMenuItem =
                        new LibraryMenuItem(fileInfo.FullName,
                            fileInfo.Name,
                            libraryItem_Click);
                    collection.Add(fileMenuItem);
                }
        }

        private void library_Popup(object sender, EventArgs e)
        {
            _library.MenuItems.Clear();

            var dirInfo = new DirectoryInfo(_currentDirectory + @"\library");
            CreateLibrary(dirInfo, _library.MenuItems);
        }

        private void contextMenu1_Popup(object sender, EventArgs e)
        {
        }

        private void undoMenuItem_Click(object sender, EventArgs e)
        {
            _regexText.Undo();
        }

        private void cutMenuItem_Click(object sender, EventArgs e)
        {
            _regexText.Cut();
        }

        private void copyMenuItem_Click(object sender, EventArgs e)
        {
            _regexText.Copy();
        }

        private void pasteMenuItem_Click(object sender, EventArgs e)
        {
            _regexText.Paste();
        }

        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            _regexText.Clear();
        }

        private void selectAllMenuItem_Click(object sender, EventArgs e)
        {
            _regexText.SelectAll();
        }

        private void MatchEvaluator_CheckedChanged(object sender, EventArgs e)
        {
            if (_matchEvaluator.Checked)
                _replaceString.Text =
                    "static public string Evaluator(Match match) {\r\n" +
                    "    return \"Fred\";\r\n" +
                    "}";
        }

        private void menuItem71_Click(object sender, EventArgs e)
        {
            var about = new About();
            about.ShowDialog();
        }

        private void menuReleaseNotes_Click(object sender, EventArgs e)
        {
            var releaseNotes = new ReleaseNotes();
            releaseNotes.ShowDialog();
        }

        #region SaveRestore

        private void SaveRegex(string filename)
        {
            // write to SOAP (XML)
            using (var streamWrite = File.Create(filename))
            {
                var soapWrite = new SoapFormatter();

                var settings = new Settings();
                settings.RegexText = _regexText.Text;
                settings.Strings = _strings.Text;
                settings.IgnoreWhitespace = _ignoreWhitespace.Checked;
                settings.IgnoreCase = _ignoreCase.Checked;
                settings.Compiled = _compiled.Checked;
                settings.ExplicitCapture = _explicitCapture.Checked;
                settings.Multiline = _multiline.Checked;
                settings.Singleline = _singleline.Checked;
                settings.Iterations = _iterations.Text;
                settings.OneString = _oneString.Checked;
                settings.Description = _description.Text;
                settings.ReplaceString = _replaceString.Text;
                settings.MatchEvaluator = _matchEvaluator.Checked;
                settings.HideGroupZero = _hideGroupZero.Checked;
                soapWrite.Serialize(streamWrite, settings);
            }
        }

        private void LoadRegex(string filename)
        {
            try
            {
                using (var streamRead = File.OpenRead(filename))
                {
                    try
                    {
                        var soapRead = new SoapFormatter();
                        var settings =
                            (Settings) soapRead.Deserialize(streamRead);

                        _regexText.Text = settings.RegexText;
                        _strings.Text = settings.Strings;
                        _ignoreWhitespace.Checked = settings.IgnoreWhitespace;
                        _ignoreCase.Checked = settings.IgnoreCase;
                        _compiled.Checked = settings.Compiled;
                        _explicitCapture.Checked = settings.ExplicitCapture;
                        _multiline.Checked = settings.Multiline;
                        _singleline.Checked = settings.Singleline;
                        _iterations.Text = settings.Iterations;
                        _oneString.Checked = settings.OneString;
                        _description.Text = settings.Description;
                        _matchEvaluator.Checked = settings.MatchEvaluator;
                        _replaceString.Text = settings.ReplaceString;
                        _hideGroupZero.Checked = settings.HideGroupZero;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (FileNotFoundException)
            {
            }
        }

        #endregion
    }
}