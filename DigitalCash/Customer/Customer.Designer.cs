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
            this.addCustomerBtn = new System.Windows.Forms.Button();
            this.moneyOrderBtn = new System.Windows.Forms.Button();
            this.CustomerLoginBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addCustomerBtn
            // 
            this.addCustomerBtn.Location = new System.Drawing.Point(105, 85);
            this.addCustomerBtn.Name = "addCustomerBtn";
            this.addCustomerBtn.Size = new System.Drawing.Size(94, 23);
            this.addCustomerBtn.TabIndex = 0;
            this.addCustomerBtn.Text = "New Customer";
            this.addCustomerBtn.UseVisualStyleBackColor = true;
            this.addCustomerBtn.Click += new System.EventHandler(this.AddCustomerBtn_Click);
            // 
            // moneyOrderBtn
            // 
            this.moneyOrderBtn.Location = new System.Drawing.Point(122, 313);
            this.moneyOrderBtn.Name = "moneyOrderBtn";
            this.moneyOrderBtn.Size = new System.Drawing.Size(94, 23);
            this.moneyOrderBtn.TabIndex = 1;
            this.moneyOrderBtn.Text = "Money Order";
            this.moneyOrderBtn.UseVisualStyleBackColor = true;
            this.moneyOrderBtn.Click += new System.EventHandler(this.MoneyOrderBtn_Click);
            // 
            // CustomerLoginBtn
            // 
            this.CustomerLoginBtn.Location = new System.Drawing.Point(105, 125);
            this.CustomerLoginBtn.Name = "CustomerLoginBtn";
            this.CustomerLoginBtn.Size = new System.Drawing.Size(94, 23);
            this.CustomerLoginBtn.TabIndex = 2;
            this.CustomerLoginBtn.Text = "Login";
            this.CustomerLoginBtn.UseVisualStyleBackColor = true;
            this.CustomerLoginBtn.Click += new System.EventHandler(this.CustomerLoginBtn_Click);
            // 
            // CustomerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CustomerLoginBtn);
            this.Controls.Add(this.moneyOrderBtn);
            this.Controls.Add(this.addCustomerBtn);
            this.Name = "CustomerForm";
            this.Text = "Customer";
            this.ResumeLayout(false);

        }

        #endregion

        private Button addCustomerBtn;
        private Button moneyOrderBtn;
        private Button CustomerLoginBtn;
    }
}