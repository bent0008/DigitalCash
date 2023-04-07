namespace Merchant
{
    partial class Merchant
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
            this.CustomerLoginBtn = new System.Windows.Forms.Button();
            this.userLbl = new System.Windows.Forms.Label();
            this.balanceLbl = new System.Windows.Forms.Label();
            this.usernameLbl = new System.Windows.Forms.Label();
            this.balanceAmountLbl = new System.Windows.Forms.Label();
            this.itemList = new System.Windows.Forms.ListView();
            this.itemHeader = new System.Windows.Forms.ColumnHeader();
            this.priceHeader = new System.Windows.Forms.ColumnHeader();
            this.BuyBtn = new System.Windows.Forms.Button();
            this.RevealBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CustomerLoginBtn
            // 
            this.CustomerLoginBtn.Location = new System.Drawing.Point(12, 12);
            this.CustomerLoginBtn.Name = "CustomerLoginBtn";
            this.CustomerLoginBtn.Size = new System.Drawing.Size(75, 23);
            this.CustomerLoginBtn.TabIndex = 0;
            this.CustomerLoginBtn.Text = "Login";
            this.CustomerLoginBtn.UseVisualStyleBackColor = true;
            this.CustomerLoginBtn.Click += new System.EventHandler(this.CustomerLoginBtn_Click);
            // 
            // userLbl
            // 
            this.userLbl.AutoSize = true;
            this.userLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userLbl.Location = new System.Drawing.Point(182, 12);
            this.userLbl.Name = "userLbl";
            this.userLbl.Padding = new System.Windows.Forms.Padding(5);
            this.userLbl.Size = new System.Drawing.Size(117, 27);
            this.userLbl.TabIndex = 1;
            this.userLbl.Text = "User:                        ";
            // 
            // balanceLbl
            // 
            this.balanceLbl.AutoSize = true;
            this.balanceLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.balanceLbl.Location = new System.Drawing.Point(423, 12);
            this.balanceLbl.Name = "balanceLbl";
            this.balanceLbl.Padding = new System.Windows.Forms.Padding(5);
            this.balanceLbl.Size = new System.Drawing.Size(135, 27);
            this.balanceLbl.TabIndex = 2;
            this.balanceLbl.Text = "Balance:                        ";
            // 
            // usernameLbl
            // 
            this.usernameLbl.AutoSize = true;
            this.usernameLbl.Location = new System.Drawing.Point(221, 18);
            this.usernameLbl.Name = "usernameLbl";
            this.usernameLbl.Size = new System.Drawing.Size(0, 15);
            this.usernameLbl.TabIndex = 3;
            // 
            // balanceAmountLbl
            // 
            this.balanceAmountLbl.AutoSize = true;
            this.balanceAmountLbl.Location = new System.Drawing.Point(481, 18);
            this.balanceAmountLbl.Name = "balanceAmountLbl";
            this.balanceAmountLbl.Size = new System.Drawing.Size(0, 15);
            this.balanceAmountLbl.TabIndex = 4;
            // 
            // itemList
            // 
            this.itemList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.itemHeader,
            this.priceHeader});
            this.itemList.Location = new System.Drawing.Point(444, 81);
            this.itemList.Name = "itemList";
            this.itemList.Size = new System.Drawing.Size(204, 255);
            this.itemList.TabIndex = 5;
            this.itemList.UseCompatibleStateImageBehavior = false;
            this.itemList.View = System.Windows.Forms.View.Details;
            this.itemList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.itemList_ItemSelectionChanged);
            // 
            // itemHeader
            // 
            this.itemHeader.Text = "Item";
            this.itemHeader.Width = 100;
            // 
            // priceHeader
            // 
            this.priceHeader.Text = "Price";
            this.priceHeader.Width = 100;
            // 
            // BuyBtn
            // 
            this.BuyBtn.Location = new System.Drawing.Point(510, 373);
            this.BuyBtn.Name = "BuyBtn";
            this.BuyBtn.Size = new System.Drawing.Size(75, 23);
            this.BuyBtn.TabIndex = 6;
            this.BuyBtn.Text = "Buy";
            this.BuyBtn.UseVisualStyleBackColor = true;
            this.BuyBtn.Click += new System.EventHandler(this.BuyBtn_Click);
            // 
            // RevealBtn
            // 
            this.RevealBtn.Location = new System.Drawing.Point(155, 184);
            this.RevealBtn.Name = "RevealBtn";
            this.RevealBtn.Size = new System.Drawing.Size(101, 23);
            this.RevealBtn.TabIndex = 7;
            this.RevealBtn.Text = "Reveal Order";
            this.RevealBtn.UseVisualStyleBackColor = true;
            this.RevealBtn.Click += new System.EventHandler(this.RevealBtn_Click);
            // 
            // Merchant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.RevealBtn);
            this.Controls.Add(this.BuyBtn);
            this.Controls.Add(this.itemList);
            this.Controls.Add(this.balanceAmountLbl);
            this.Controls.Add(this.usernameLbl);
            this.Controls.Add(this.balanceLbl);
            this.Controls.Add(this.userLbl);
            this.Controls.Add(this.CustomerLoginBtn);
            this.Name = "Merchant";
            this.Text = "Merchant";
            this.Load += new System.EventHandler(this.Merchant_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button CustomerLoginBtn;
        private Label userLbl;
        private Label balanceLbl;
        private Label usernameLbl;
        private Label balanceAmountLbl;
        private ListView itemList;
        private ColumnHeader itemHeader;
        private ColumnHeader priceHeader;
        private Button BuyBtn;
        private Button RevealBtn;
    }
}