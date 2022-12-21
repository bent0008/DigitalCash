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
            this.customerNameTxtbx = new System.Windows.Forms.TextBox();
            this.customerPwdTxtbx = new System.Windows.Forms.TextBox();
            this.customerBalanceTxtbx = new System.Windows.Forms.TextBox();
            this.customerNameLbl = new System.Windows.Forms.Label();
            this.customerPwdLbl = new System.Windows.Forms.Label();
            this.customerBalanceLbl = new System.Windows.Forms.Label();
            this.submitBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // customerNameTxtbx
            // 
            this.customerNameTxtbx.Location = new System.Drawing.Point(262, 43);
            this.customerNameTxtbx.Name = "customerNameTxtbx";
            this.customerNameTxtbx.Size = new System.Drawing.Size(100, 23);
            this.customerNameTxtbx.TabIndex = 0;
            // 
            // customerPwdTxtbx
            // 
            this.customerPwdTxtbx.Location = new System.Drawing.Point(262, 100);
            this.customerPwdTxtbx.Name = "customerPwdTxtbx";
            this.customerPwdTxtbx.PasswordChar = '*';
            this.customerPwdTxtbx.Size = new System.Drawing.Size(100, 23);
            this.customerPwdTxtbx.TabIndex = 1;
            // 
            // customerBalanceTxtbx
            // 
            this.customerBalanceTxtbx.Location = new System.Drawing.Point(262, 155);
            this.customerBalanceTxtbx.Name = "customerBalanceTxtbx";
            this.customerBalanceTxtbx.Size = new System.Drawing.Size(100, 23);
            this.customerBalanceTxtbx.TabIndex = 2;
            // 
            // customerNameLbl
            // 
            this.customerNameLbl.AutoSize = true;
            this.customerNameLbl.Location = new System.Drawing.Point(167, 46);
            this.customerNameLbl.Name = "customerNameLbl";
            this.customerNameLbl.Size = new System.Drawing.Size(39, 15);
            this.customerNameLbl.TabIndex = 3;
            this.customerNameLbl.Text = "Name";
            // 
            // customerPwdLbl
            // 
            this.customerPwdLbl.AutoSize = true;
            this.customerPwdLbl.Location = new System.Drawing.Point(149, 103);
            this.customerPwdLbl.Name = "customerPwdLbl";
            this.customerPwdLbl.Size = new System.Drawing.Size(57, 15);
            this.customerPwdLbl.TabIndex = 4;
            this.customerPwdLbl.Text = "Password";
            // 
            // customerBalanceLbl
            // 
            this.customerBalanceLbl.AutoSize = true;
            this.customerBalanceLbl.Location = new System.Drawing.Point(158, 158);
            this.customerBalanceLbl.Name = "customerBalanceLbl";
            this.customerBalanceLbl.Size = new System.Drawing.Size(105, 15);
            this.customerBalanceLbl.TabIndex = 5;
            this.customerBalanceLbl.Text = "Balance                 $";
            // 
            // submitBtn
            // 
            this.submitBtn.Location = new System.Drawing.Point(213, 217);
            this.submitBtn.Name = "submitBtn";
            this.submitBtn.Size = new System.Drawing.Size(75, 23);
            this.submitBtn.TabIndex = 6;
            this.submitBtn.Text = "Submit";
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.Click += new System.EventHandler(this.SubmitBtn_Click);
            // 
            // AddCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 284);
            this.Controls.Add(this.submitBtn);
            this.Controls.Add(this.customerBalanceLbl);
            this.Controls.Add(this.customerPwdLbl);
            this.Controls.Add(this.customerNameLbl);
            this.Controls.Add(this.customerBalanceTxtbx);
            this.Controls.Add(this.customerPwdTxtbx);
            this.Controls.Add(this.customerNameTxtbx);
            this.Location = new System.Drawing.Point(50, 50);
            this.Name = "AddCustomer";
            this.Text = "Add Customer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox customerNameTxtbx;
        private TextBox customerPwdTxtbx;
        private TextBox customerBalanceTxtbx;
        private Label customerNameLbl;
        private Label customerPwdLbl;
        private Label customerBalanceLbl;
        private Button submitBtn;
    }
}