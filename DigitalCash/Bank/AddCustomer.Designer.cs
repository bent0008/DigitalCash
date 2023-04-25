namespace Bank
{
    partial class AddCustomer
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
            this.submitBtn = new System.Windows.Forms.Button();
            this.customerBalanceLbl = new System.Windows.Forms.Label();
            this.customerPwdLbl = new System.Windows.Forms.Label();
            this.customerNameLbl = new System.Windows.Forms.Label();
            this.customerBalanceTxtbx = new System.Windows.Forms.TextBox();
            this.customerPwdTxtbx = new System.Windows.Forms.TextBox();
            this.customerNameTxtbx = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // submitBtn
            // 
            this.submitBtn.Location = new System.Drawing.Point(138, 305);
            this.submitBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.submitBtn.Name = "submitBtn";
            this.submitBtn.Size = new System.Drawing.Size(86, 31);
            this.submitBtn.TabIndex = 4;
            this.submitBtn.Text = "Submit";
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.Click += new System.EventHandler(this.SubmitBtn_Click);
            // 
            // customerBalanceLbl
            // 
            this.customerBalanceLbl.AutoSize = true;
            this.customerBalanceLbl.Location = new System.Drawing.Point(75, 227);
            this.customerBalanceLbl.Name = "customerBalanceLbl";
            this.customerBalanceLbl.Size = new System.Drawing.Size(61, 20);
            this.customerBalanceLbl.TabIndex = 12;
            this.customerBalanceLbl.Text = "Balance";
            // 
            // customerPwdLbl
            // 
            this.customerPwdLbl.AutoSize = true;
            this.customerPwdLbl.Location = new System.Drawing.Point(65, 153);
            this.customerPwdLbl.Name = "customerPwdLbl";
            this.customerPwdLbl.Size = new System.Drawing.Size(70, 20);
            this.customerPwdLbl.TabIndex = 11;
            this.customerPwdLbl.Text = "Password";
            // 
            // customerNameLbl
            // 
            this.customerNameLbl.AutoSize = true;
            this.customerNameLbl.Location = new System.Drawing.Point(86, 77);
            this.customerNameLbl.Name = "customerNameLbl";
            this.customerNameLbl.Size = new System.Drawing.Size(49, 20);
            this.customerNameLbl.TabIndex = 10;
            this.customerNameLbl.Text = "Name";
            // 
            // customerBalanceTxtbx
            // 
            this.customerBalanceTxtbx.Location = new System.Drawing.Point(194, 223);
            this.customerBalanceTxtbx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.customerBalanceTxtbx.Name = "customerBalanceTxtbx";
            this.customerBalanceTxtbx.Size = new System.Drawing.Size(114, 27);
            this.customerBalanceTxtbx.TabIndex = 3;
            // 
            // customerPwdTxtbx
            // 
            this.customerPwdTxtbx.Location = new System.Drawing.Point(194, 149);
            this.customerPwdTxtbx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.customerPwdTxtbx.Name = "customerPwdTxtbx";
            this.customerPwdTxtbx.PasswordChar = '*';
            this.customerPwdTxtbx.Size = new System.Drawing.Size(114, 27);
            this.customerPwdTxtbx.TabIndex = 2;
            // 
            // customerNameTxtbx
            // 
            this.customerNameTxtbx.Location = new System.Drawing.Point(194, 73);
            this.customerNameTxtbx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.customerNameTxtbx.Name = "customerNameTxtbx";
            this.customerNameTxtbx.Size = new System.Drawing.Size(114, 27);
            this.customerNameTxtbx.TabIndex = 1;
            // 
            // AddCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 420);
            this.Controls.Add(this.submitBtn);
            this.Controls.Add(this.customerBalanceLbl);
            this.Controls.Add(this.customerPwdLbl);
            this.Controls.Add(this.customerNameLbl);
            this.Controls.Add(this.customerBalanceTxtbx);
            this.Controls.Add(this.customerPwdTxtbx);
            this.Controls.Add(this.customerNameTxtbx);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AddCustomer";
            this.Text = "AddCustomer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button submitBtn;
        private Label customerBalanceLbl;
        private Label customerPwdLbl;
        private Label customerNameLbl;
        private TextBox customerBalanceTxtbx;
        private TextBox customerPwdTxtbx;
        private TextBox customerNameTxtbx;
    }
}