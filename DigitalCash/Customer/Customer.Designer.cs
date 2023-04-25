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
            this.balanceAmountLbl = new System.Windows.Forms.Label();
            this.usernameLbl = new System.Windows.Forms.Label();
            this.balanceLbl = new System.Windows.Forms.Label();
            this.userLbl = new System.Windows.Forms.Label();
            this.UnblindBtn = new System.Windows.Forms.Button();
            this.updateBox = new System.Windows.Forms.RichTextBox();
            this.ToBankBtn = new System.Windows.Forms.Button();
            this.ToMerchantBtn = new System.Windows.Forms.Button();
            this.RefreshBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // moneyOrderBtn
            // 
            this.moneyOrderBtn.Location = new System.Drawing.Point(195, 106);
            this.moneyOrderBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.moneyOrderBtn.Name = "moneyOrderBtn";
            this.moneyOrderBtn.Size = new System.Drawing.Size(107, 31);
            this.moneyOrderBtn.TabIndex = 2;
            this.moneyOrderBtn.Text = "Money Order";
            this.moneyOrderBtn.UseVisualStyleBackColor = true;
            this.moneyOrderBtn.Click += new System.EventHandler(this.MoneyOrderBtn_Click);
            // 
            // CustomerLoginBtn
            // 
            this.CustomerLoginBtn.Location = new System.Drawing.Point(14, 16);
            this.CustomerLoginBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CustomerLoginBtn.Name = "CustomerLoginBtn";
            this.CustomerLoginBtn.Size = new System.Drawing.Size(107, 31);
            this.CustomerLoginBtn.TabIndex = 1;
            this.CustomerLoginBtn.Text = "Login";
            this.CustomerLoginBtn.UseVisualStyleBackColor = true;
            this.CustomerLoginBtn.Click += new System.EventHandler(this.CustomerLoginBtn_Click);
            // 
            // balanceAmountLbl
            // 
            this.balanceAmountLbl.AutoSize = true;
            this.balanceAmountLbl.Location = new System.Drawing.Point(518, 20);
            this.balanceAmountLbl.Name = "balanceAmountLbl";
            this.balanceAmountLbl.Size = new System.Drawing.Size(0, 20);
            this.balanceAmountLbl.TabIndex = 12;
            // 
            // usernameLbl
            // 
            this.usernameLbl.AutoSize = true;
            this.usernameLbl.Location = new System.Drawing.Point(221, 20);
            this.usernameLbl.Name = "usernameLbl";
            this.usernameLbl.Size = new System.Drawing.Size(0, 20);
            this.usernameLbl.TabIndex = 11;
            // 
            // balanceLbl
            // 
            this.balanceLbl.AutoSize = true;
            this.balanceLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.balanceLbl.Location = new System.Drawing.Point(451, 12);
            this.balanceLbl.Name = "balanceLbl";
            this.balanceLbl.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.balanceLbl.Size = new System.Drawing.Size(174, 36);
            this.balanceLbl.TabIndex = 10;
            this.balanceLbl.Text = "Balance:                        ";
            // 
            // userLbl
            // 
            this.userLbl.AutoSize = true;
            this.userLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userLbl.Location = new System.Drawing.Point(176, 12);
            this.userLbl.Name = "userLbl";
            this.userLbl.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.userLbl.Size = new System.Drawing.Size(151, 36);
            this.userLbl.TabIndex = 9;
            this.userLbl.Text = "User:                        ";
            // 
            // UnblindBtn
            // 
            this.UnblindBtn.Location = new System.Drawing.Point(480, 106);
            this.UnblindBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UnblindBtn.Name = "UnblindBtn";
            this.UnblindBtn.Size = new System.Drawing.Size(107, 31);
            this.UnblindBtn.TabIndex = 4;
            this.UnblindBtn.Text = "Unblind Order";
            this.UnblindBtn.UseVisualStyleBackColor = true;
            this.UnblindBtn.Click += new System.EventHandler(this.UnblindBtn_Click);
            // 
            // updateBox
            // 
            this.updateBox.BackColor = System.Drawing.SystemColors.Window;
            this.updateBox.Location = new System.Drawing.Point(48, 234);
            this.updateBox.Name = "updateBox";
            this.updateBox.ReadOnly = true;
            this.updateBox.Size = new System.Drawing.Size(624, 167);
            this.updateBox.TabIndex = 14;
            this.updateBox.Text = "";
            // 
            // ToBankBtn
            // 
            this.ToBankBtn.Location = new System.Drawing.Point(195, 154);
            this.ToBankBtn.Name = "ToBankBtn";
            this.ToBankBtn.Size = new System.Drawing.Size(107, 29);
            this.ToBankBtn.TabIndex = 3;
            this.ToBankBtn.Text = "To Bank";
            this.ToBankBtn.UseVisualStyleBackColor = true;
            this.ToBankBtn.Click += new System.EventHandler(this.ToBankBtn_Click);
            // 
            // ToMerchantBtn
            // 
            this.ToMerchantBtn.Location = new System.Drawing.Point(480, 154);
            this.ToMerchantBtn.Name = "ToMerchantBtn";
            this.ToMerchantBtn.Size = new System.Drawing.Size(107, 29);
            this.ToMerchantBtn.TabIndex = 5;
            this.ToMerchantBtn.Text = "To Merchant";
            this.ToMerchantBtn.UseVisualStyleBackColor = true;
            this.ToMerchantBtn.Click += new System.EventHandler(this.ToMerchantBtn_Click);
            // 
            // RefreshBtn
            // 
            this.RefreshBtn.Location = new System.Drawing.Point(640, 16);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(68, 29);
            this.RefreshBtn.TabIndex = 6;
            this.RefreshBtn.Text = "Refresh";
            this.RefreshBtn.UseVisualStyleBackColor = true;
            this.RefreshBtn.Click += new System.EventHandler(this.RefreshBtn_Click);
            // 
            // CustomerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 450);
            this.Controls.Add(this.RefreshBtn);
            this.Controls.Add(this.ToMerchantBtn);
            this.Controls.Add(this.ToBankBtn);
            this.Controls.Add(this.updateBox);
            this.Controls.Add(this.UnblindBtn);
            this.Controls.Add(this.balanceAmountLbl);
            this.Controls.Add(this.usernameLbl);
            this.Controls.Add(this.balanceLbl);
            this.Controls.Add(this.userLbl);
            this.Controls.Add(this.CustomerLoginBtn);
            this.Controls.Add(this.moneyOrderBtn);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CustomerForm";
            this.Text = "Customer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button moneyOrderBtn;
        private Button CustomerLoginBtn;
        private Label balanceAmountLbl;
        private Label usernameLbl;
        private Label balanceLbl;
        private Label userLbl;
        private Button UnblindBtn;
        private RichTextBox updateBox;
        private Button ToBankBtn;
        private Button ToMerchantBtn;
        private Button RefreshBtn;
    }
}