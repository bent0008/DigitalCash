namespace Customer
{
    partial class MoneyOrder
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
            this.cheatChkbx = new System.Windows.Forms.CheckBox();
            this.SubmitBtn = new System.Windows.Forms.Button();
            this.moneyAmountTxtbx = new System.Windows.Forms.TextBox();
            this.moneyAmountLbl = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // cheatChkbx
            // 
            this.cheatChkbx.AutoSize = true;
            this.cheatChkbx.Location = new System.Drawing.Point(211, 214);
            this.cheatChkbx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cheatChkbx.Name = "cheatChkbx";
            this.cheatChkbx.Size = new System.Drawing.Size(69, 24);
            this.cheatChkbx.TabIndex = 3;
            this.cheatChkbx.Text = "Cheat";
            this.cheatChkbx.UseVisualStyleBackColor = true;
            // 
            // SubmitBtn
            // 
            this.SubmitBtn.Location = new System.Drawing.Point(203, 150);
            this.SubmitBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SubmitBtn.Name = "SubmitBtn";
            this.SubmitBtn.Size = new System.Drawing.Size(86, 31);
            this.SubmitBtn.TabIndex = 2;
            this.SubmitBtn.Text = "Submit";
            this.SubmitBtn.UseVisualStyleBackColor = true;
            this.SubmitBtn.Click += new System.EventHandler(this.SubmitBtn_Click);
            // 
            // moneyAmountTxtbx
            // 
            this.moneyAmountTxtbx.Location = new System.Drawing.Point(218, 73);
            this.moneyAmountTxtbx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.moneyAmountTxtbx.Name = "moneyAmountTxtbx";
            this.moneyAmountTxtbx.Size = new System.Drawing.Size(114, 27);
            this.moneyAmountTxtbx.TabIndex = 1;
            // 
            // moneyAmountLbl
            // 
            this.moneyAmountLbl.AutoSize = true;
            this.moneyAmountLbl.Location = new System.Drawing.Point(141, 76);
            this.moneyAmountLbl.Name = "moneyAmountLbl";
            this.moneyAmountLbl.Size = new System.Drawing.Size(62, 20);
            this.moneyAmountLbl.TabIndex = 3;
            this.moneyAmountLbl.Text = "Amount";
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.SystemColors.Control;
            this.progressBar.Location = new System.Drawing.Point(1, 336);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(491, 30);
            this.progressBar.TabIndex = 4;
            this.progressBar.UseWaitCursor = true;
            // 
            // MoneyOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 365);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.moneyAmountLbl);
            this.Controls.Add(this.moneyAmountTxtbx);
            this.Controls.Add(this.SubmitBtn);
            this.Controls.Add(this.cheatChkbx);
            this.Location = new System.Drawing.Point(50, 50);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MoneyOrder";
            this.Text = "Money Order";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox cheatChkbx;
        private Button SubmitBtn;
        private TextBox moneyAmountTxtbx;
        private Label moneyAmountLbl;
        private ProgressBar progressBar;
    }
}