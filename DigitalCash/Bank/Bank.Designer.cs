namespace Bank
{
    partial class Bank
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
            this.CheatedBtn = new System.Windows.Forms.Button();
            this.SignBtn = new System.Windows.Forms.Button();
            this.LoginBtn = new System.Windows.Forms.Button();
            this.balanceAmountLbl = new System.Windows.Forms.Label();
            this.usernameLbl = new System.Windows.Forms.Label();
            this.balanceLbl = new System.Windows.Forms.Label();
            this.userLbl = new System.Windows.Forms.Label();
            this.DepositBtn = new System.Windows.Forms.Button();
            this.NewCustBtn = new System.Windows.Forms.Button();
            this.GenKeysBtn = new System.Windows.Forms.Button();
            this.RevealBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CheatedBtn
            // 
            this.CheatedBtn.Location = new System.Drawing.Point(403, 95);
            this.CheatedBtn.Name = "CheatedBtn";
            this.CheatedBtn.Size = new System.Drawing.Size(100, 23);
            this.CheatedBtn.TabIndex = 1;
            this.CheatedBtn.Text = "Check Cheated";
            this.CheatedBtn.UseVisualStyleBackColor = true;
            this.CheatedBtn.Click += new System.EventHandler(this.CheatedBtn_Click);
            // 
            // SignBtn
            // 
            this.SignBtn.Location = new System.Drawing.Point(403, 195);
            this.SignBtn.Name = "SignBtn";
            this.SignBtn.Size = new System.Drawing.Size(100, 23);
            this.SignBtn.TabIndex = 2;
            this.SignBtn.Text = "Sign Order";
            this.SignBtn.UseVisualStyleBackColor = true;
            this.SignBtn.Click += new System.EventHandler(this.SignBtn_Click);
            // 
            // LoginBtn
            // 
            this.LoginBtn.Location = new System.Drawing.Point(12, 12);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(97, 23);
            this.LoginBtn.TabIndex = 3;
            this.LoginBtn.Text = "Login";
            this.LoginBtn.UseVisualStyleBackColor = true;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // balanceAmountLbl
            // 
            this.balanceAmountLbl.AutoSize = true;
            this.balanceAmountLbl.Location = new System.Drawing.Point(496, 15);
            this.balanceAmountLbl.Name = "balanceAmountLbl";
            this.balanceAmountLbl.Size = new System.Drawing.Size(0, 15);
            this.balanceAmountLbl.TabIndex = 8;
            // 
            // usernameLbl
            // 
            this.usernameLbl.AutoSize = true;
            this.usernameLbl.Location = new System.Drawing.Point(236, 15);
            this.usernameLbl.Name = "usernameLbl";
            this.usernameLbl.Size = new System.Drawing.Size(0, 15);
            this.usernameLbl.TabIndex = 7;
            // 
            // balanceLbl
            // 
            this.balanceLbl.AutoSize = true;
            this.balanceLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.balanceLbl.Location = new System.Drawing.Point(438, 9);
            this.balanceLbl.Name = "balanceLbl";
            this.balanceLbl.Padding = new System.Windows.Forms.Padding(5);
            this.balanceLbl.Size = new System.Drawing.Size(135, 27);
            this.balanceLbl.TabIndex = 6;
            this.balanceLbl.Text = "Balance:                        ";
            // 
            // userLbl
            // 
            this.userLbl.AutoSize = true;
            this.userLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userLbl.Location = new System.Drawing.Point(197, 9);
            this.userLbl.Name = "userLbl";
            this.userLbl.Padding = new System.Windows.Forms.Padding(5);
            this.userLbl.Size = new System.Drawing.Size(117, 27);
            this.userLbl.TabIndex = 5;
            this.userLbl.Text = "User:                        ";
            // 
            // DepositBtn
            // 
            this.DepositBtn.Location = new System.Drawing.Point(217, 195);
            this.DepositBtn.Name = "DepositBtn";
            this.DepositBtn.Size = new System.Drawing.Size(97, 23);
            this.DepositBtn.TabIndex = 9;
            this.DepositBtn.Text = "Deposit";
            this.DepositBtn.UseVisualStyleBackColor = true;
            this.DepositBtn.Click += new System.EventHandler(this.DepositBtn_Click);
            // 
            // NewCustBtn
            // 
            this.NewCustBtn.Location = new System.Drawing.Point(12, 41);
            this.NewCustBtn.Name = "NewCustBtn";
            this.NewCustBtn.Size = new System.Drawing.Size(97, 23);
            this.NewCustBtn.TabIndex = 11;
            this.NewCustBtn.Text = "New Customer";
            this.NewCustBtn.UseVisualStyleBackColor = true;
            this.NewCustBtn.Click += new System.EventHandler(this.NewCustBtn_Click);
            // 
            // GenKeysBtn
            // 
            this.GenKeysBtn.Location = new System.Drawing.Point(12, 415);
            this.GenKeysBtn.Name = "GenKeysBtn";
            this.GenKeysBtn.Size = new System.Drawing.Size(75, 23);
            this.GenKeysBtn.TabIndex = 12;
            this.GenKeysBtn.Text = "New Keys";
            this.GenKeysBtn.UseVisualStyleBackColor = true;
            this.GenKeysBtn.Click += new System.EventHandler(this.GenKeysBtn_Click);
            // 
            // RevealBtn
            // 
            this.RevealBtn.Location = new System.Drawing.Point(403, 145);
            this.RevealBtn.Name = "RevealBtn";
            this.RevealBtn.Size = new System.Drawing.Size(100, 23);
            this.RevealBtn.TabIndex = 13;
            this.RevealBtn.Text = "Reveal Orders";
            this.RevealBtn.UseVisualStyleBackColor = true;
            this.RevealBtn.Click += new System.EventHandler(this.RevealBtn_Click);
            // 
            // Bank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.RevealBtn);
            this.Controls.Add(this.GenKeysBtn);
            this.Controls.Add(this.NewCustBtn);
            this.Controls.Add(this.DepositBtn);
            this.Controls.Add(this.balanceAmountLbl);
            this.Controls.Add(this.usernameLbl);
            this.Controls.Add(this.balanceLbl);
            this.Controls.Add(this.userLbl);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.SignBtn);
            this.Controls.Add(this.CheatedBtn);
            this.Name = "Bank";
            this.Text = "Bank";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button UnblindBtn;
        private Button CheatedBtn;
        private Button SignBtn;
        private Button LoginBtn;
        private Label balanceAmountLbl;
        private Label usernameLbl;
        private Label balanceLbl;
        private Label userLbl;
        private Button DepositBtn;
        private Button NewCustBtn;
        private Button GenKeysBtn;
        private Button RevealBtn;
    }
}