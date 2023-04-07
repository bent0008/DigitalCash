namespace Customer
{
    partial class CustomerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.moneyOrderBtn = new System.Windows.Forms.Button();
            this.CustomerLoginBtn = new System.Windows.Forms.Button();
            this.EncryptBtn = new System.Windows.Forms.Button();
            this.balanceAmountLbl = new System.Windows.Forms.Label();
            this.usernameLbl = new System.Windows.Forms.Label();
            this.balanceLbl = new System.Windows.Forms.Label();
            this.userLbl = new System.Windows.Forms.Label();
            this.UnblindBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // moneyOrderBtn
            // 
            this.moneyOrderBtn.Location = new System.Drawing.Point(286, 118);
            this.moneyOrderBtn.Name = "moneyOrderBtn";
            this.moneyOrderBtn.Size = new System.Drawing.Size(94, 23);
            this.moneyOrderBtn.TabIndex = 1;
            this.moneyOrderBtn.Text = "Money Order";
            this.moneyOrderBtn.UseVisualStyleBackColor = true;
            this.moneyOrderBtn.Click += new System.EventHandler(this.MoneyOrderBtn_Click);
            // 
            // CustomerLoginBtn
            // 
            this.CustomerLoginBtn.Location = new System.Drawing.Point(12, 12);
            this.CustomerLoginBtn.Name = "CustomerLoginBtn";
            this.CustomerLoginBtn.Size = new System.Drawing.Size(94, 23);
            this.CustomerLoginBtn.TabIndex = 2;
            this.CustomerLoginBtn.Text = "Login";
            this.CustomerLoginBtn.UseVisualStyleBackColor = true;
            this.CustomerLoginBtn.Click += new System.EventHandler(this.CustomerLoginBtn_Click);
            // 
            // EncryptBtn
            // 
            this.EncryptBtn.Location = new System.Drawing.Point(286, 195);
            this.EncryptBtn.Name = "EncryptBtn";
            this.EncryptBtn.Size = new System.Drawing.Size(94, 23);
            this.EncryptBtn.TabIndex = 3;
            this.EncryptBtn.Text = "Encrypt";
            this.EncryptBtn.UseVisualStyleBackColor = true;
            this.EncryptBtn.Click += new System.EventHandler(this.EncryptBtn_Click);
            // 
            // balanceAmountLbl
            // 
            this.balanceAmountLbl.AutoSize = true;
            this.balanceAmountLbl.Location = new System.Drawing.Point(453, 15);
            this.balanceAmountLbl.Name = "balanceAmountLbl";
            this.balanceAmountLbl.Size = new System.Drawing.Size(0, 15);
            this.balanceAmountLbl.TabIndex = 12;
            // 
            // usernameLbl
            // 
            this.usernameLbl.AutoSize = true;
            this.usernameLbl.Location = new System.Drawing.Point(193, 15);
            this.usernameLbl.Name = "usernameLbl";
            this.usernameLbl.Size = new System.Drawing.Size(0, 15);
            this.usernameLbl.TabIndex = 11;
            // 
            // balanceLbl
            // 
            this.balanceLbl.AutoSize = true;
            this.balanceLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.balanceLbl.Location = new System.Drawing.Point(395, 9);
            this.balanceLbl.Name = "balanceLbl";
            this.balanceLbl.Padding = new System.Windows.Forms.Padding(5);
            this.balanceLbl.Size = new System.Drawing.Size(135, 27);
            this.balanceLbl.TabIndex = 10;
            this.balanceLbl.Text = "Balance:                        ";
            // 
            // userLbl
            // 
            this.userLbl.AutoSize = true;
            this.userLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userLbl.Location = new System.Drawing.Point(154, 9);
            this.userLbl.Name = "userLbl";
            this.userLbl.Padding = new System.Windows.Forms.Padding(5);
            this.userLbl.Size = new System.Drawing.Size(117, 27);
            this.userLbl.TabIndex = 9;
            this.userLbl.Text = "User:                        ";
            // 
            // UnblindBtn
            // 
            this.UnblindBtn.Location = new System.Drawing.Point(286, 253);
            this.UnblindBtn.Name = "UnblindBtn";
            this.UnblindBtn.Size = new System.Drawing.Size(94, 23);
            this.UnblindBtn.TabIndex = 13;
            this.UnblindBtn.Text = "Unblind Order";
            this.UnblindBtn.UseVisualStyleBackColor = true;
            this.UnblindBtn.Click += new System.EventHandler(this.UnblindBtn_Click);
            // 
            // CustomerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 415);
            this.Controls.Add(this.UnblindBtn);
            this.Controls.Add(this.balanceAmountLbl);
            this.Controls.Add(this.usernameLbl);
            this.Controls.Add(this.balanceLbl);
            this.Controls.Add(this.userLbl);
            this.Controls.Add(this.EncryptBtn);
            this.Controls.Add(this.CustomerLoginBtn);
            this.Controls.Add(this.moneyOrderBtn);
            this.Name = "CustomerForm";
            this.Text = "Customer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button moneyOrderBtn;
        private Button CustomerLoginBtn;
        private Button EncryptBtn;
        private Label balanceAmountLbl;
        private Label usernameLbl;
        private Label balanceLbl;
        private Label userLbl;
        private Button UnblindBtn;
    }
}