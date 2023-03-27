namespace Customer
{
    partial class Encrypt
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
            this.encryptLbl = new System.Windows.Forms.Label();
            this.EncryptBtn = new System.Windows.Forms.Button();
            this.DecryptBtn = new System.Windows.Forms.Button();
            this.decryptLbl = new System.Windows.Forms.Label();
            this.GenKeysBtn = new System.Windows.Forms.Button();
            this.TestBtn = new System.Windows.Forms.Button();
            this.BasicBtn = new System.Windows.Forms.Button();
            this.NewBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // encryptLbl
            // 
            this.encryptLbl.AutoSize = true;
            this.encryptLbl.Location = new System.Drawing.Point(87, 202);
            this.encryptLbl.Name = "encryptLbl";
            this.encryptLbl.Size = new System.Drawing.Size(0, 15);
            this.encryptLbl.TabIndex = 1;
            // 
            // EncryptBtn
            // 
            this.EncryptBtn.Location = new System.Drawing.Point(335, 151);
            this.EncryptBtn.Name = "EncryptBtn";
            this.EncryptBtn.Size = new System.Drawing.Size(75, 23);
            this.EncryptBtn.TabIndex = 2;
            this.EncryptBtn.Text = "Encrypt";
            this.EncryptBtn.UseVisualStyleBackColor = true;
            this.EncryptBtn.Click += new System.EventHandler(this.EncryptBtn_Click);
            // 
            // DecryptBtn
            // 
            this.DecryptBtn.Location = new System.Drawing.Point(335, 246);
            this.DecryptBtn.Name = "DecryptBtn";
            this.DecryptBtn.Size = new System.Drawing.Size(75, 23);
            this.DecryptBtn.TabIndex = 3;
            this.DecryptBtn.Text = "Decrypt";
            this.DecryptBtn.UseVisualStyleBackColor = true;
            this.DecryptBtn.Click += new System.EventHandler(this.DecryptBtn_Click);
            // 
            // decryptLbl
            // 
            this.decryptLbl.AutoSize = true;
            this.decryptLbl.Location = new System.Drawing.Point(352, 296);
            this.decryptLbl.Name = "decryptLbl";
            this.decryptLbl.Size = new System.Drawing.Size(0, 15);
            this.decryptLbl.TabIndex = 4;
            // 
            // GenKeysBtn
            // 
            this.GenKeysBtn.Location = new System.Drawing.Point(578, 229);
            this.GenKeysBtn.Name = "GenKeysBtn";
            this.GenKeysBtn.Size = new System.Drawing.Size(75, 23);
            this.GenKeysBtn.TabIndex = 5;
            this.GenKeysBtn.Text = "New Keys";
            this.GenKeysBtn.UseVisualStyleBackColor = true;
            this.GenKeysBtn.Click += new System.EventHandler(this.GenKeysBtn_Click);
            // 
            // TestBtn
            // 
            this.TestBtn.Location = new System.Drawing.Point(175, 209);
            this.TestBtn.Name = "TestBtn";
            this.TestBtn.Size = new System.Drawing.Size(75, 23);
            this.TestBtn.TabIndex = 6;
            this.TestBtn.Text = "Test";
            this.TestBtn.UseVisualStyleBackColor = true;
            this.TestBtn.Click += new System.EventHandler(this.TestBtn_Click);
            // 
            // BasicBtn
            // 
            this.BasicBtn.Location = new System.Drawing.Point(205, 77);
            this.BasicBtn.Name = "BasicBtn";
            this.BasicBtn.Size = new System.Drawing.Size(75, 23);
            this.BasicBtn.TabIndex = 8;
            this.BasicBtn.Text = "Basic";
            this.BasicBtn.UseVisualStyleBackColor = true;
            this.BasicBtn.Click += new System.EventHandler(this.BasicBtn_Click);
            // 
            // NewBtn
            // 
            this.NewBtn.Location = new System.Drawing.Point(316, 371);
            this.NewBtn.Name = "NewBtn";
            this.NewBtn.Size = new System.Drawing.Size(75, 23);
            this.NewBtn.TabIndex = 9;
            this.NewBtn.Text = "New";
            this.NewBtn.UseVisualStyleBackColor = true;
            this.NewBtn.Click += new System.EventHandler(this.NewBtn_Click);
            // 
            // Encrypt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.NewBtn);
            this.Controls.Add(this.BasicBtn);
            this.Controls.Add(this.TestBtn);
            this.Controls.Add(this.GenKeysBtn);
            this.Controls.Add(this.decryptLbl);
            this.Controls.Add(this.DecryptBtn);
            this.Controls.Add(this.EncryptBtn);
            this.Controls.Add(this.encryptLbl);
            this.Name = "Encrypt";
            this.Text = "Encrypt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label encryptLbl;
        private Button EncryptBtn;
        private Button DecryptBtn;
        private Label decryptLbl;
        private Button GenKeysBtn;
        private Button TestBtn;
        private Button BasicBtn;
        private Button NewBtn;
    }
}