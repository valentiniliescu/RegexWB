using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace RegexTest
{
    /// <summary>
    ///     Summary description for About.
    /// </summary>
    public class About : Form
    {
        /// <summary>
        ///     Required designer variable.
        /// </summary>
        private readonly Container _components = null;

        private Label _label1;
        private LinkLabel _linkLabel1;
        private TextBox _textBox1;

        public About()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
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
            this._label1 = new System.Windows.Forms.Label();
            this._textBox1 = new System.Windows.Forms.TextBox();
            this._linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this._label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this._label1.ForeColor = System.Drawing.Color.Black;
            this._label1.Location = new System.Drawing.Point(10, 9);
            this._label1.Name = "_label1";
            this._label1.Size = new System.Drawing.Size(488, 93);
            this._label1.TabIndex = 1;
            this._label1.Text = "Regular Expression Workbench";
            // 
            // textBox1
            // 
            this._textBox1.BackColor = System.Drawing.Color.Cornsilk;
            this._textBox1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this._textBox1.ForeColor = System.Drawing.Color.Black;
            this._textBox1.Location = new System.Drawing.Point(28, 105);
            this._textBox1.Multiline = true;
            this._textBox1.Name = "_textBox1";
            this._textBox1.ReadOnly = true;
            this._textBox1.Size = new System.Drawing.Size(470, 83);
            this._textBox1.TabIndex = 2;
            this._textBox1.TabStop = false;
            this._textBox1.Text = "Eric Gunnerson, 2002\r\nMike Lansdaal, 2016\r";
            // 
            // linkLabel1
            // 
            this._linkLabel1.AutoSize = true;
            this._linkLabel1.Location = new System.Drawing.Point(25, 216);
            this._linkLabel1.Name = "_linkLabel1";
            this._linkLabel1.Size = new System.Drawing.Size(460, 17);
            this._linkLabel1.TabIndex = 3;
            this._linkLabel1.TabStop = true;
            this._linkLabel1.Text = "Add New or View Issues : https://github.com/mlansdaal/RegexWB/issues";
            this._linkLabel1.LinkClicked +=
                new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // About
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(520, 265);
            this.Controls.Add(this._linkLabel1);
            this.Controls.Add(this._textBox1);
            this.Controls.Add(this._label1);
            this.Name = "About";
            this.Text = "About Regular Expression Workbench";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/mlansdaal/RegexWB/issues");
        }
    }
}