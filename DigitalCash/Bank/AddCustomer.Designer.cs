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
            this.submitBtn.Location = new System.Drawing.Point(121, 229);
            this.submitBtn.Name = "submitBtn";
            this.submitBtn.Size = new System.Drawing.Size(75, 23);
            this.submitBtn.TabIndex = 13;
            this.submitBtn.Text = "Submit";
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.Click += new System.EventHandler(this.SubmitBtn_Click);
            // 
            // customerBalanceLbl
            // 
            this.customerBalanceLbl.AutoSize = true;
            this.customerBalanceLbl.Location = new System.Drawing.Point(66, 170);
            this.customerBalanceLbl.Name = "customerBalanceLbl";
            this.customerBalanceLbl.Size = new System.Drawing.Size(105, 15);
            this.customerBalanceLbl.TabIndex = 12;
            this.customerBalanceLbl.Text = "Balance                 $";
            // 
            // customerPwdLbl
            // 
            this.customerPwdLbl.AutoSize = true;
            this.customerPwdLbl.Location = new System.Drawing.Point(57, 115);
            this.customerPwdLbl.Name = "customerPwdLbl";
            this.customerPwdLbl.Size = new System.Drawing.Size(57, 15);
            this.customerPwdLbl.TabIndex = 11;
            this.customerPwdLbl.Text = "Password";
            // 
            // customerNameLbl
            // 
            this.customerNameLbl.AutoSize = true;
            this.customerNameLbl.Location = new System.Drawing.Point(75, 58);
            this.customerNameLbl.Name = "customerNameLbl";
            this.customerNameLbl.Size = new System.Drawing.Size(39, 15);
            this.customerNameLbl.TabIndex = 10;
            this.customerNameLbl.Text = "Name";
            // 
            // customerBalanceTxtbx
            // 
            this.customerBalanceTxtbx.Location = new System.Drawing.Point(170, 167);
            this.customerBalanceTxtbx.Name = "customerBalanceTxtbx";
            this.customerBalanceTxtbx.Size = new System.Drawing.Size(100, 23);
            this.customerBalanceTxtbx.TabIndex = 9;
            // 
            // customerPwdTxtbx
            // 
            this.customerPwdTxtbx.Location = new System.Drawing.Point(170, 112);
            this.customerPwdTxtbx.Name = "customerPwdTxtbx";
            this.customerPwdTxtbx.PasswordChar = '*';
            this.customerPwdTxtbx.Size = new System.Drawing.Size(100, 23);
            this.customerPwdTxtbx.TabIndex = 8;
            // 
            // customerNameTxtbx
            // 
            this.customerNameTxtbx.Location = new System.Drawing.Point(170, 55);
            this.customerNameTxtbx.Name = "customerNameTxtbx";
            this.customerNameTxtbx.Size = new System.Drawing.Size(100, 23);
            this.customerNameTxtbx.TabIndex = 7;
            // 
            // AddCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 315);
            this.Controls.Add(this.submitBtn);
            this.Controls.Add(this.customerBalanceLbl);
            this.Controls.Add(this.customerPwdLbl);
            this.Controls.Add(this.customerNameLbl);
            this.Controls.Add(this.customerBalanceTxtbx);
            this.Controls.Add(this.customerPwdTxtbx);
            this.Controls.Add(this.customerNameTxtbx);
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