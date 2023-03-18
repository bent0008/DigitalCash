namespace Bank
{
    partial class Deposit
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
            this.amountLbl = new System.Windows.Forms.Label();
            this.amountTxtbx = new System.Windows.Forms.TextBox();
            this.DepositBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // amountLbl
            // 
            this.amountLbl.AutoSize = true;
            this.amountLbl.Location = new System.Drawing.Point(71, 76);
            this.amountLbl.Name = "amountLbl";
            this.amountLbl.Size = new System.Drawing.Size(51, 15);
            this.amountLbl.TabIndex = 0;
            this.amountLbl.Text = "Amount";
            // 
            // amountTxtbx
            // 
            this.amountTxtbx.Location = new System.Drawing.Point(143, 73);
            this.amountTxtbx.Name = "amountTxtbx";
            this.amountTxtbx.Size = new System.Drawing.Size(100, 23);
            this.amountTxtbx.TabIndex = 1;
            // 
            // DepositBtn
            // 
            this.DepositBtn.Location = new System.Drawing.Point(115, 152);
            this.DepositBtn.Name = "DepositBtn";
            this.DepositBtn.Size = new System.Drawing.Size(75, 23);
            this.DepositBtn.TabIndex = 2;
            this.DepositBtn.Text = "Deposit";
            this.DepositBtn.UseVisualStyleBackColor = true;
            this.DepositBtn.Click += new System.EventHandler(this.DepositBtn_Click);
            // 
            // Deposit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 252);
            this.Controls.Add(this.DepositBtn);
            this.Controls.Add(this.amountTxtbx);
            this.Controls.Add(this.amountLbl);
            this.Name = "Deposit";
            this.Text = "Deposit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label amountLbl;
        private TextBox amountTxtbx;
        private Button DepositBtn;
    }
}