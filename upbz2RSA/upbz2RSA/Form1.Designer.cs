﻿namespace upbz2RSA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.crtAsymKeyBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SlctFileBtn = new System.Windows.Forms.Button();
            this.slctFileTxt = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.exprtPubKeyBtn = new System.Windows.Forms.Button();
            this.ImprtPubKey = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.stcFileDtxt = new System.Windows.Forms.TextBox();
            this.slcFileDbtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.exprtPrivKey = new System.Windows.Forms.Button();
            this.imprPrivKeyBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.passtxt = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.EncBtn = new System.Windows.Forms.Button();
            this.dcrtBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // crtAsymKeyBtn
            // 
            this.crtAsymKeyBtn.Location = new System.Drawing.Point(285, 10);
            this.crtAsymKeyBtn.Name = "crtAsymKeyBtn";
            this.crtAsymKeyBtn.Size = new System.Drawing.Size(145, 23);
            this.crtAsymKeyBtn.TabIndex = 0;
            this.crtAsymKeyBtn.Text = "Create asymmetric key pair";
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
            // SlctFileBtn
            // 
            this.SlctFileBtn.Location = new System.Drawing.Point(337, 31);
            this.SlctFileBtn.Name = "SlctFileBtn";
            this.SlctFileBtn.Size = new System.Drawing.Size(75, 23);
            this.SlctFileBtn.TabIndex = 3;
            this.SlctFileBtn.Text = "Select File";
            this.SlctFileBtn.UseVisualStyleBackColor = true;
            this.SlctFileBtn.Click += new System.EventHandler(this.SlctFileBtn_Click);
            // 
            // slctFileTxt
            // 
            this.slctFileTxt.ForeColor = System.Drawing.SystemColors.Window;
            this.slctFileTxt.Location = new System.Drawing.Point(19, 31);
            this.slctFileTxt.Name = "slctFileTxt";
            this.slctFileTxt.ReadOnly = true;
            this.slctFileTxt.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.slctFileTxt.Size = new System.Drawing.Size(301, 20);
            this.slctFileTxt.TabIndex = 4;
            this.slctFileTxt.TextChanged += new System.EventHandler(this.slctFileTxt_TextChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // exprtPubKeyBtn
            // 
            this.exprtPubKeyBtn.Location = new System.Drawing.Point(179, 52);
            this.exprtPubKeyBtn.Name = "exprtPubKeyBtn";
            this.exprtPubKeyBtn.Size = new System.Drawing.Size(144, 23);
            this.exprtPubKeyBtn.TabIndex = 6;
            this.exprtPubKeyBtn.Text = "Export Public Key";
            this.exprtPubKeyBtn.UseVisualStyleBackColor = true;
            this.exprtPubKeyBtn.Click += new System.EventHandler(this.exprtPubKeyBtn_Click);
            // 
            // ImprtPubKey
            // 
            this.ImprtPubKey.Location = new System.Drawing.Point(31, 52);
            this.ImprtPubKey.Name = "ImprtPubKey";
            this.ImprtPubKey.Size = new System.Drawing.Size(142, 23);
            this.ImprtPubKey.TabIndex = 7;
            this.ImprtPubKey.Text = "Import Public Key";
            this.ImprtPubKey.UseVisualStyleBackColor = true;
            this.ImprtPubKey.Click += new System.EventHandler(this.ImprtPubKey_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(27, 118);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(468, 148);
            this.tabControl1.TabIndex = 9;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
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
            this.stcFileDtxt.Location = new System.Drawing.Point(19, 31);
            this.stcFileDtxt.Name = "stcFileDtxt";
            this.stcFileDtxt.ReadOnly = true;
            this.stcFileDtxt.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.stcFileDtxt.Size = new System.Drawing.Size(301, 20);
            this.stcFileDtxt.TabIndex = 7;
            // 
            // slcFileDbtn
            // 
            this.slcFileDbtn.Location = new System.Drawing.Point(337, 31);
            this.slcFileDbtn.Name = "slcFileDbtn";
            this.slcFileDbtn.Size = new System.Drawing.Size(75, 23);
            this.slcFileDbtn.TabIndex = 6;
            this.slcFileDbtn.Text = "Select File";
            this.slcFileDbtn.UseVisualStyleBackColor = true;
            this.slcFileDbtn.Click += new System.EventHandler(this.slcFileDbtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(329, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "file \".\\Encrypt\\rsaPublicKey.txt\"";
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
            this.exprtPrivKey.Location = new System.Drawing.Point(179, 81);
            this.exprtPrivKey.Name = "exprtPrivKey";
            this.exprtPrivKey.Size = new System.Drawing.Size(144, 23);
            this.exprtPrivKey.TabIndex = 15;
            this.exprtPrivKey.Text = "Export Public+Private Key";
            this.exprtPrivKey.UseVisualStyleBackColor = true;
            this.exprtPrivKey.Click += new System.EventHandler(this.exprtPrivKey_Click);
            // 
            // imprPrivKeyBtn
            // 
            this.imprPrivKeyBtn.Location = new System.Drawing.Point(31, 81);
            this.imprPrivKeyBtn.Name = "imprPrivKeyBtn";
            this.imprPrivKeyBtn.Size = new System.Drawing.Size(142, 23);
            this.imprPrivKeyBtn.TabIndex = 16;
            this.imprPrivKeyBtn.Text = "Import Public+Private Key";
            this.imprPrivKeyBtn.UseVisualStyleBackColor = true;
            this.imprPrivKeyBtn.Click += new System.EventHandler(this.imprPrivKeyBtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(329, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(162, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "file \".\\Decrypt\\rsaPrivateKey.txt\"";
            // 
            // passtxt
            // 
            this.passtxt.Location = new System.Drawing.Point(125, 12);
            this.passtxt.Name = "passtxt";
            this.passtxt.Size = new System.Drawing.Size(142, 20);
            this.passtxt.TabIndex = 19;
            this.passtxt.TextChanged += new System.EventHandler(this.passtxt_TextChanged);
            // 
            // EncBtn
            // 
            this.EncBtn.Image = global::upbz2RSA.Properties.Resources.icon16;
            this.EncBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.EncBtn.Location = new System.Drawing.Point(15, 66);
            this.EncBtn.Name = "EncBtn";
            this.EncBtn.Size = new System.Drawing.Size(79, 25);
            this.EncBtn.TabIndex = 2;
            this.EncBtn.Text = "    Encrypt";
            this.EncBtn.UseVisualStyleBackColor = true;
            this.EncBtn.Click += new System.EventHandler(this.EncBtn_Click);
            // 
            // dcrtBtn
            // 
            this.dcrtBtn.Image = global::upbz2RSA.Properties.Resources.iconOpen16;
            this.dcrtBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dcrtBtn.Location = new System.Drawing.Point(15, 66);
            this.dcrtBtn.Name = "dcrtBtn";
            this.dcrtBtn.Size = new System.Drawing.Size(79, 25);
            this.dcrtBtn.TabIndex = 5;
            this.dcrtBtn.Text = "    Decrypt";
            this.dcrtBtn.UseVisualStyleBackColor = true;
            this.dcrtBtn.Click += new System.EventHandler(this.dcrtBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Secret password:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(528, 403);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.passtxt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.imprPrivKeyBtn);
            this.Controls.Add(this.exprtPrivKey);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.ImprtPubKey);
            this.Controls.Add(this.exprtPubKeyBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.crtAsymKeyBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "TurboPowerCrypt";
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button exprtPrivKey;
        private System.Windows.Forms.Button imprPrivKeyBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox passtxt;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label4;
    }
}

