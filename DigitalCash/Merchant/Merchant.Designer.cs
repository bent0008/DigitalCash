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
            this.HashBtn = new System.Windows.Forms.Button();
            this.MerchantChkBx = new System.Windows.Forms.CheckBox();
            this.CustomerChkBx = new System.Windows.Forms.CheckBox();
            this.updateBox = new System.Windows.Forms.RichTextBox();
            this.ToBankBtn = new System.Windows.Forms.Button();
            this.VerifyHashesBtn = new System.Windows.Forms.Button();
            this.RefreshBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CustomerLoginBtn
            // 
            this.CustomerLoginBtn.Location = new System.Drawing.Point(14, 16);
            this.CustomerLoginBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CustomerLoginBtn.Name = "CustomerLoginBtn";
            this.CustomerLoginBtn.Size = new System.Drawing.Size(86, 31);
            this.CustomerLoginBtn.TabIndex = 1;
            this.CustomerLoginBtn.Text = "Login";
            this.CustomerLoginBtn.UseVisualStyleBackColor = true;
            this.CustomerLoginBtn.Click += new System.EventHandler(this.CustomerLoginBtn_Click);
            // 
            // userLbl
            // 
            this.userLbl.AutoSize = true;
            this.userLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userLbl.Location = new System.Drawing.Point(208, 16);
            this.userLbl.Name = "userLbl";
            this.userLbl.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.userLbl.Size = new System.Drawing.Size(151, 36);
            this.userLbl.TabIndex = 1;
            this.userLbl.Text = "User:                        ";
            // 
            // balanceLbl
            // 
            this.balanceLbl.AutoSize = true;
            this.balanceLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.balanceLbl.Location = new System.Drawing.Point(483, 16);
            this.balanceLbl.Name = "balanceLbl";
            this.balanceLbl.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.balanceLbl.Size = new System.Drawing.Size(174, 36);
            this.balanceLbl.TabIndex = 2;
            this.balanceLbl.Text = "Balance:                        ";
            // 
            // usernameLbl
            // 
            this.usernameLbl.AutoSize = true;
            this.usernameLbl.Location = new System.Drawing.Point(253, 24);
            this.usernameLbl.Name = "usernameLbl";
            this.usernameLbl.Size = new System.Drawing.Size(0, 20);
            this.usernameLbl.TabIndex = 3;
            // 
            // balanceAmountLbl
            // 
            this.balanceAmountLbl.AutoSize = true;
            this.balanceAmountLbl.Location = new System.Drawing.Point(550, 24);
            this.balanceAmountLbl.Name = "balanceAmountLbl";
            this.balanceAmountLbl.Size = new System.Drawing.Size(0, 20);
            this.balanceAmountLbl.TabIndex = 4;
            // 
            // itemList
            // 
            this.itemList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.itemHeader,
            this.priceHeader});
            this.itemList.Location = new System.Drawing.Point(507, 108);
            this.itemList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.itemList.Name = "itemList";
            this.itemList.Size = new System.Drawing.Size(233, 269);
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
            this.BuyBtn.Location = new System.Drawing.Point(585, 385);
            this.BuyBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BuyBtn.Name = "BuyBtn";
            this.BuyBtn.Size = new System.Drawing.Size(86, 31);
            this.BuyBtn.TabIndex = 6;
            this.BuyBtn.Text = "Buy";
            this.BuyBtn.UseVisualStyleBackColor = true;
            this.BuyBtn.Click += new System.EventHandler(this.BuyBtn_Click);
            // 
            // RevealBtn
            // 
            this.RevealBtn.Location = new System.Drawing.Point(165, 108);
            this.RevealBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RevealBtn.Name = "RevealBtn";
            this.RevealBtn.Size = new System.Drawing.Size(156, 31);
            this.RevealBtn.TabIndex = 2;
            this.RevealBtn.Text = "Reveal Order";
            this.RevealBtn.UseVisualStyleBackColor = true;
            this.RevealBtn.Click += new System.EventHandler(this.RevealBtn_Click);
            // 
            // HashBtn
            // 
            this.HashBtn.Location = new System.Drawing.Point(165, 157);
            this.HashBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.HashBtn.Name = "HashBtn";
            this.HashBtn.Size = new System.Drawing.Size(156, 31);
            this.HashBtn.TabIndex = 3;
            this.HashBtn.Text = "Reveal Hashes";
            this.HashBtn.UseVisualStyleBackColor = true;
            this.HashBtn.Click += new System.EventHandler(this.HashBtn_Click);
            // 
            // MerchantChkBx
            // 
            this.MerchantChkBx.AutoSize = true;
            this.MerchantChkBx.Location = new System.Drawing.Point(560, 429);
            this.MerchantChkBx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MerchantChkBx.Name = "MerchantChkBx";
            this.MerchantChkBx.Size = new System.Drawing.Size(135, 24);
            this.MerchantChkBx.TabIndex = 7;
            this.MerchantChkBx.Text = "Merchant Cheat";
            this.MerchantChkBx.UseVisualStyleBackColor = true;
            // 
            // CustomerChkBx
            // 
            this.CustomerChkBx.AutoSize = true;
            this.CustomerChkBx.Location = new System.Drawing.Point(560, 461);
            this.CustomerChkBx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CustomerChkBx.Name = "CustomerChkBx";
            this.CustomerChkBx.Size = new System.Drawing.Size(136, 24);
            this.CustomerChkBx.TabIndex = 8;
            this.CustomerChkBx.Text = "Customer Cheat";
            this.CustomerChkBx.UseVisualStyleBackColor = true;
            // 
            // updateBox
            // 
            this.updateBox.Location = new System.Drawing.Point(50, 300);
            this.updateBox.Name = "updateBox";
            this.updateBox.Size = new System.Drawing.Size(375, 185);
            this.updateBox.TabIndex = 11;
            this.updateBox.Text = "";
            // 
            // ToBankBtn
            // 
            this.ToBankBtn.Location = new System.Drawing.Point(165, 250);
            this.ToBankBtn.Name = "ToBankBtn";
            this.ToBankBtn.Size = new System.Drawing.Size(156, 29);
            this.ToBankBtn.TabIndex = 9;
            this.ToBankBtn.Text = "Send To Bank";
            this.ToBankBtn.UseVisualStyleBackColor = true;
            this.ToBankBtn.Click += new System.EventHandler(this.ToBankBtn_Click);
            // 
            // VerifyHashesBtn
            // 
            this.VerifyHashesBtn.Location = new System.Drawing.Point(165, 205);
            this.VerifyHashesBtn.Name = "VerifyHashesBtn";
            this.VerifyHashesBtn.Size = new System.Drawing.Size(156, 29);
            this.VerifyHashesBtn.TabIndex = 4;
            this.VerifyHashesBtn.Text = "Verify Hashes";
            this.VerifyHashesBtn.UseVisualStyleBackColor = true;
            this.VerifyHashesBtn.Click += new System.EventHandler(this.VerifyHashesBtn_Click);
            // 
            // RefreshBtn
            // 
            this.RefreshBtn.Location = new System.Drawing.Point(710, 20);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(68, 29);
            this.RefreshBtn.TabIndex = 10;
            this.RefreshBtn.Text = "Refresh";
            this.RefreshBtn.UseVisualStyleBackColor = true;
            this.RefreshBtn.Click += new System.EventHandler(this.RefreshBtn_Click);
            // 
            // Merchant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 515);
            this.Controls.Add(this.RefreshBtn);
            this.Controls.Add(this.VerifyHashesBtn);
            this.Controls.Add(this.ToBankBtn);
            this.Controls.Add(this.updateBox);
            this.Controls.Add(this.CustomerChkBx);
            this.Controls.Add(this.MerchantChkBx);
            this.Controls.Add(this.HashBtn);
            this.Controls.Add(this.RevealBtn);
            this.Controls.Add(this.BuyBtn);
            this.Controls.Add(this.itemList);
            this.Controls.Add(this.balanceAmountLbl);
            this.Controls.Add(this.usernameLbl);
            this.Controls.Add(this.balanceLbl);
            this.Controls.Add(this.userLbl);
            this.Controls.Add(this.CustomerLoginBtn);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
        private Button HashBtn;
        private CheckBox MerchantChkBx;
        private CheckBox CustomerChkBx;
        private RichTextBox updateBox;
        private Button ToBankBtn;
        private Button VerifyHashesBtn;
        private Button RefreshBtn;
    }
}