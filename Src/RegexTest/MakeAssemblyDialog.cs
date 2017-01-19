using System.ComponentModel;
using System.Windows.Forms;

namespace RegexTest
{
    /// <summary>
    ///     Summary description for MakeAssemblyDialog.
    /// </summary>
    public class MakeAssemblyDialog : Form
    {
        public TextBox AssemblyName;
        private Button _cancel;

        /// <summary>
        ///     Required designer variable.
        /// </summary>
        private readonly Container _components = null;

        public CheckBox CreatePublic;
        private Label _label1;
        private Label _label2;
        private Label _label3;
        public TextBox Namespace;
        private Button _ok;
        public TextBox TypeName;

        public MakeAssemblyDialog()
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
            this.Namespace = new System.Windows.Forms.TextBox();
            this._ok = new System.Windows.Forms.Button();
            this._cancel = new System.Windows.Forms.Button();
            this.CreatePublic = new System.Windows.Forms.CheckBox();
            this.TypeName = new System.Windows.Forms.TextBox();
            this._label2 = new System.Windows.Forms.Label();
            this.AssemblyName = new System.Windows.Forms.TextBox();
            this._label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this._label1.Location = new System.Drawing.Point(8, 56);
            this._label1.Name = "_label1";
            this._label1.Size = new System.Drawing.Size(72, 23);
            this._label1.TabIndex = 0;
            this._label1.Text = "Namespace:";
            // 
            // Namespace
            // 
            this.Namespace.Location = new System.Drawing.Point(96, 56);
            this.Namespace.Name = "Namespace";
            this.Namespace.Size = new System.Drawing.Size(296, 20);
            this.Namespace.TabIndex = 1;
            this.Namespace.Text = "textBox1";
            // 
            // OK
            // 
            this._ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._ok.Location = new System.Drawing.Point(120, 136);
            this._ok.Name = "_ok";
            this._ok.TabIndex = 2;
            this._ok.Text = "OK";
            // 
            // Cancel
            // 
            this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancel.Location = new System.Drawing.Point(240, 136);
            this._cancel.Name = "_cancel";
            this._cancel.TabIndex = 3;
            this._cancel.Text = "Cancel";
            // 
            // CreatePublic
            // 
            this.CreatePublic.Checked = true;
            this.CreatePublic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CreatePublic.Location = new System.Drawing.Point(24, 120);
            this.CreatePublic.Name = "CreatePublic";
            this.CreatePublic.TabIndex = 4;
            this.CreatePublic.Text = "Public";
            // 
            // TypeName
            // 
            this.TypeName.Location = new System.Drawing.Point(96, 88);
            this.TypeName.Name = "TypeName";
            this.TypeName.Size = new System.Drawing.Size(296, 20);
            this.TypeName.TabIndex = 6;
            this.TypeName.Text = "textBox1";
            // 
            // label2
            // 
            this._label2.Location = new System.Drawing.Point(8, 88);
            this._label2.Name = "_label2";
            this._label2.Size = new System.Drawing.Size(72, 23);
            this._label2.TabIndex = 5;
            this._label2.Text = "Type Name:";
            // 
            // AssemblyName
            // 
            this.AssemblyName.Location = new System.Drawing.Point(96, 16);
            this.AssemblyName.Name = "AssemblyName";
            this.AssemblyName.Size = new System.Drawing.Size(296, 20);
            this.AssemblyName.TabIndex = 8;
            this.AssemblyName.Text = "Assembly";
            // 
            // label3
            // 
            this._label3.Location = new System.Drawing.Point(8, 16);
            this._label3.Name = "_label3";
            this._label3.Size = new System.Drawing.Size(72, 23);
            this._label3.TabIndex = 7;
            this._label3.Text = "Assembly";
            // 
            // MakeAssemblyDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(400, 186);
            this.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.AssemblyName,
                this._label3,
                this.TypeName,
                this._label2,
                this.CreatePublic,
                this._cancel,
                this._ok,
                this.Namespace,
                this._label1
            });
            this.Name = "MakeAssemblyDialog";
            this.Text = "MakeAssemblyDialog";
            this.ResumeLayout(false);
        }

        #endregion
    }
}