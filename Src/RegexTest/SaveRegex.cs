using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace RegexTest
{
    /// <summary>
    ///     Summary description for SaveRegex.
    /// </summary>
    public class SaveRegex : Form
    {
        private static string _lastDirectory = "library";
        private Button _browseButton;
        private Button _cancelButton;

        /// <summary>
        ///     Required designer variable.
        /// </summary>
        private readonly Container _components = null;

        public TextBox Filename;
        private Label _label2;
        private Button _okButton;

        public SaveRegex()
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
            this.Filename = new System.Windows.Forms.TextBox();
            this._okButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._label2 = new System.Windows.Forms.Label();
            this._browseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // filename
            // 
            this.Filename.Location = new System.Drawing.Point(88, 16);
            this.Filename.Name = "Filename";
            this.Filename.Size = new System.Drawing.Size(328, 20);
            this.Filename.TabIndex = 1;
            this.Filename.Text = "Library\\";
            // 
            // okButton
            // 
            this._okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._okButton.Location = new System.Drawing.Point(136, 64);
            this._okButton.Name = "_okButton";
            this._okButton.TabIndex = 2;
            this._okButton.Text = "OK";
            this._okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(272, 64);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.TabIndex = 3;
            this._cancelButton.Text = "Cancel";
            // 
            // label2
            // 
            this._label2.Location = new System.Drawing.Point(8, 16);
            this._label2.Name = "_label2";
            this._label2.Size = new System.Drawing.Size(64, 23);
            this._label2.TabIndex = 4;
            this._label2.Text = "Filename:";
            // 
            // browseButton
            // 
            this._browseButton.Location = new System.Drawing.Point(432, 16);
            this._browseButton.Name = "_browseButton";
            this._browseButton.TabIndex = 5;
            this._browseButton.Text = "Browse";
            this._browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // SaveRegex
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(528, 106);
            this.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this._browseButton,
                this._label2,
                this._cancelButton,
                this._okButton,
                this.Filename
            });
            this.Name = "SaveRegex";
            this.Text = "SaveRegex";
            this.ResumeLayout(false);
        }

        #endregion

        private void browseButton_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.InitialDirectory = _lastDirectory;
            dialog.DefaultExt = "Regex";
            dialog.AddExtension = true;
            dialog.Filter = "Regex files (*.regex)|*.regex";
            if (dialog.ShowDialog() == DialogResult.OK)
                Filename.Text = dialog.FileName;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            var fileInfo = new FileInfo(Filename.Text);
            _lastDirectory = fileInfo.DirectoryName;
        }
    }
}