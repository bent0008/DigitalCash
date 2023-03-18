namespace Bank
{
    partial class Login
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
            this.SubmitBtn = new System.Windows.Forms.Button();
            this.passwordTxtbx = new System.Windows.Forms.TextBox();
            this.usernameTxtbx = new System.Windows.Forms.TextBox();
            this.passwordLbl = new System.Windows.Forms.Label();
            this.usernameLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SubmitBtn
            // 
            this.SubmitBtn.Location = new System.Drawing.Point(153, 200);
            this.SubmitBtn.Name = "SubmitBtn";
            this.SubmitBtn.Size = new System.Drawing.Size(75, 23);
            this.SubmitBtn.TabIndex = 9;
            this.SubmitBtn.Text = "Submit";
            this.SubmitBtn.UseVisualStyleBackColor = true;
            this.SubmitBtn.Click += new System.EventHandler(this.SubmitBtn_Click);
            // 
            // passwordTxtbx
            // 
            this.passwordTxtbx.Location = new System.Drawing.Point(183, 133);
            this.passwordTxtbx.Name = "passwordTxtbx";
            this.passwordTxtbx.PasswordChar = '*';
            this.passwordTxtbx.Size = new System.Drawing.Size(100, 23);
            this.passwordTxtbx.TabIndex = 8;
            // 
            // usernameTxtbx
            // 
            this.usernameTxtbx.Location = new System.Drawing.Point(183, 72);
            this.usernameTxtbx.Name = "usernameTxtbx";
            this.usernameTxtbx.Size = new System.Drawing.Size(100, 23);
            this.usernameTxtbx.TabIndex = 7;
            // 
            // passwordLbl
            // 
            this.passwordLbl.AutoSize = true;
            this.passwordLbl.Location = new System.Drawing.Point(103, 136);
            this.passwordLbl.Name = "passwordLbl";
            this.passwordLbl.Size = new System.Drawing.Size(57, 15);
            this.passwordLbl.TabIndex = 6;
            this.passwordLbl.Text = "Password";
            // 
            // usernameLbl
            // 
            this.usernameLbl.AutoSize = true;
            this.usernameLbl.Location = new System.Drawing.Point(121, 75);
            this.usernameLbl.Name = "usernameLbl";
            this.usernameLbl.Size = new System.Drawing.Size(39, 15);
            this.usernameLbl.TabIndex = 5;
            this.usernameLbl.Text = "Name";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 313);
            this.Controls.Add(this.SubmitBtn);
            this.Controls.Add(this.passwordTxtbx);
            this.Controls.Add(this.usernameTxtbx);
            this.Controls.Add(this.passwordLbl);
            this.Controls.Add(this.usernameLbl);
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button SubmitBtn;
        private TextBox passwordTxtbx;
        private TextBox usernameTxtbx;
        private Label passwordLbl;
        private Label usernameLbl;
    }
}