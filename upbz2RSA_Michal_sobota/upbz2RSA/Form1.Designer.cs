namespace upbz2RSA
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.crtAsymKeyBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.EncBtn = new System.Windows.Forms.Button();
            this.SlctFileBtn = new System.Windows.Forms.Button();
            this.slctFileTxt = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dcrtBtn = new System.Windows.Forms.Button();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.exprtPubKeyBtn = new System.Windows.Forms.Button();
            this.ImprtPubKey = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.stcFileDtxt = new System.Windows.Forms.TextBox();
            this.slcFileDbtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.exprtPrivKey = new System.Windows.Forms.Button();
            this.imprPrivKeyBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.passtxt = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // crtAsymKeyBtn
            // 
            this.crtAsymKeyBtn.Location = new System.Drawing.Point(179, 10);
            this.crtAsymKeyBtn.Name = "crtAsymKeyBtn";
            this.crtAsymKeyBtn.Size = new System.Drawing.Size(113, 23);
            this.crtAsymKeyBtn.TabIndex = 0;
            this.crtAsymKeyBtn.Text = "Create asymetric key";
            this.crtAsymKeyBtn.UseVisualStyleBackColor = true;
            this.crtAsymKeyBtn.Click += new System.EventHandler(this.crtAsymKeyBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 349);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 1;
            // 
            // EncBtn
            // 
            this.EncBtn.Location = new System.Drawing.Point(6, 53);
            this.EncBtn.Name = "EncBtn";
            this.EncBtn.Size = new System.Drawing.Size(75, 23);
            this.EncBtn.TabIndex = 2;
            this.EncBtn.Text = "Encrypt";
            this.EncBtn.UseVisualStyleBackColor = true;
            this.EncBtn.Click += new System.EventHandler(this.EncBtn_Click);
            // 
            // SlctFileBtn
            // 
            this.SlctFileBtn.Location = new System.Drawing.Point(324, 27);
            this.SlctFileBtn.Name = "SlctFileBtn";
            this.SlctFileBtn.Size = new System.Drawing.Size(75, 23);
            this.SlctFileBtn.TabIndex = 3;
            this.SlctFileBtn.Text = "Select File";
            this.SlctFileBtn.UseVisualStyleBackColor = true;
            this.SlctFileBtn.Click += new System.EventHandler(this.SlctFileBtn_Click);
            // 
            // slctFileTxt
            // 
            this.slctFileTxt.Location = new System.Drawing.Point(6, 27);
            this.slctFileTxt.Name = "slctFileTxt";
            this.slctFileTxt.Size = new System.Drawing.Size(301, 20);
            this.slctFileTxt.TabIndex = 4;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // dcrtBtn
            // 
            this.dcrtBtn.Location = new System.Drawing.Point(15, 66);
            this.dcrtBtn.Name = "dcrtBtn";
            this.dcrtBtn.Size = new System.Drawing.Size(75, 23);
            this.dcrtBtn.TabIndex = 5;
            this.dcrtBtn.Text = "Decrypt";
            this.dcrtBtn.UseVisualStyleBackColor = true;
            this.dcrtBtn.Click += new System.EventHandler(this.dcrtBtn_Click);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // exprtPubKeyBtn
            // 
            this.exprtPubKeyBtn.Location = new System.Drawing.Point(27, 41);
            this.exprtPubKeyBtn.Name = "exprtPubKeyBtn";
            this.exprtPubKeyBtn.Size = new System.Drawing.Size(113, 23);
            this.exprtPubKeyBtn.TabIndex = 6;
            this.exprtPubKeyBtn.Text = "Export Public Key";
            this.exprtPubKeyBtn.UseVisualStyleBackColor = true;
            this.exprtPubKeyBtn.Click += new System.EventHandler(this.exprtPubKeyBtn_Click);
            // 
            // ImprtPubKey
            // 
            this.ImprtPubKey.Location = new System.Drawing.Point(26, 99);
            this.ImprtPubKey.Name = "ImprtPubKey";
            this.ImprtPubKey.Size = new System.Drawing.Size(113, 23);
            this.ImprtPubKey.TabIndex = 7;
            this.ImprtPubKey.Text = "Import Public Key";
            this.ImprtPubKey.UseVisualStyleBackColor = true;
            this.ImprtPubKey.Click += new System.EventHandler(this.ImprtPubKey_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(27, 132);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(468, 148);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.Controls.Add(this.slctFileTxt);
            this.tabPage1.Controls.Add(this.SlctFileBtn);
            this.tabPage1.Controls.Add(this.EncBtn);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(460, 122);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Encrypt";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.Controls.Add(this.stcFileDtxt);
            this.tabPage2.Controls.Add(this.slcFileDbtn);
            this.tabPage2.Controls.Add(this.dcrtBtn);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(460, 122);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Decrypt";
            // 
            // stcFileDtxt
            // 
            this.stcFileDtxt.Location = new System.Drawing.Point(15, 35);
            this.stcFileDtxt.Name = "stcFileDtxt";
            this.stcFileDtxt.Size = new System.Drawing.Size(318, 20);
            this.stcFileDtxt.TabIndex = 7;
            // 
            // slcFileDbtn
            // 
            this.slcFileDbtn.Location = new System.Drawing.Point(339, 35);
            this.slcFileDbtn.Name = "slcFileDbtn";
            this.slcFileDbtn.Size = new System.Drawing.Size(75, 23);
            this.slcFileDbtn.TabIndex = 6;
            this.slcFileDbtn.Text = "Select File";
            this.slcFileDbtn.UseVisualStyleBackColor = true;
            this.slcFileDbtn.Click += new System.EventHandler(this.slcFileDbtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(298, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "It will create a pair of asymetric key";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(146, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "It will export public key into file";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(145, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "It will load public key from file";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(80, 301);
            this.progressBar1.Maximum = 350;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(350, 23);
            this.progressBar1.TabIndex = 14;
            // 
            // exprtPrivKey
            // 
            this.exprtPrivKey.Location = new System.Drawing.Point(301, 41);
            this.exprtPrivKey.Name = "exprtPrivKey";
            this.exprtPrivKey.Size = new System.Drawing.Size(102, 23);
            this.exprtPrivKey.TabIndex = 15;
            this.exprtPrivKey.Text = "Export Private Key";
            this.exprtPrivKey.UseVisualStyleBackColor = true;
            this.exprtPrivKey.Click += new System.EventHandler(this.exprtPrivKey_Click);
            // 
            // imprPrivKeyBtn
            // 
            this.imprPrivKeyBtn.Location = new System.Drawing.Point(301, 99);
            this.imprPrivKeyBtn.Name = "imprPrivKeyBtn";
            this.imprPrivKeyBtn.Size = new System.Drawing.Size(102, 23);
            this.imprPrivKeyBtn.TabIndex = 16;
            this.imprPrivKeyBtn.Text = "Import Private Key";
            this.imprPrivKeyBtn.UseVisualStyleBackColor = true;
            this.imprPrivKeyBtn.Click += new System.EventHandler(this.imprPrivKeyBtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(410, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "It will export private key";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(409, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "It will import private key";
            // 
            // passtxt
            // 
            this.passtxt.Location = new System.Drawing.Point(31, 12);
            this.passtxt.Name = "passtxt";
            this.passtxt.Size = new System.Drawing.Size(142, 20);
            this.passtxt.TabIndex = 19;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 430);
            this.Controls.Add(this.passtxt);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.imprPrivKeyBtn);
            this.Controls.Add(this.exprtPrivKey);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.ImprtPubKey);
            this.Controls.Add(this.exprtPubKeyBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.crtAsymKeyBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button crtAsymKeyBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button EncBtn;
        private System.Windows.Forms.Button SlctFileBtn;
        private System.Windows.Forms.TextBox slctFileTxt;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button dcrtBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Button exprtPubKeyBtn;
        private System.Windows.Forms.Button ImprtPubKey;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox stcFileDtxt;
        private System.Windows.Forms.Button slcFileDbtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button exprtPrivKey;
        private System.Windows.Forms.Button imprPrivKeyBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox passtxt;
    }
}

