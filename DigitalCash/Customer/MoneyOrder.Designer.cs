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
            this.ProgressBar = new System.Windows.Forms.StatusStrip();
            this.moneyProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.ProgressBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // cheatChkbx
            // 
            this.cheatChkbx.AutoSize = true;
            this.cheatChkbx.Location = new System.Drawing.Point(271, 12);
            this.cheatChkbx.Name = "cheatChkbx";
            this.cheatChkbx.Size = new System.Drawing.Size(57, 19);
            this.cheatChkbx.TabIndex = 0;
            this.cheatChkbx.Text = "Cheat";
            this.cheatChkbx.UseVisualStyleBackColor = true;
            // 
            // SubmitBtn
            // 
            this.SubmitBtn.Location = new System.Drawing.Point(262, 243);
            this.SubmitBtn.Name = "SubmitBtn";
            this.SubmitBtn.Size = new System.Drawing.Size(75, 23);
            this.SubmitBtn.TabIndex = 1;
            this.SubmitBtn.Text = "Submit";
            this.SubmitBtn.UseVisualStyleBackColor = true;
            this.SubmitBtn.Click += new System.EventHandler(this.SubmitBtn_Click);
            // 
            // moneyAmountTxtbx
            // 
            this.moneyAmountTxtbx.Location = new System.Drawing.Point(296, 49);
            this.moneyAmountTxtbx.Name = "moneyAmountTxtbx";
            this.moneyAmountTxtbx.Size = new System.Drawing.Size(100, 23);
            this.moneyAmountTxtbx.TabIndex = 2;
            // 
            // moneyAmountLbl
            // 
            this.moneyAmountLbl.AutoSize = true;
            this.moneyAmountLbl.Location = new System.Drawing.Point(239, 52);
            this.moneyAmountLbl.Name = "moneyAmountLbl";
            this.moneyAmountLbl.Size = new System.Drawing.Size(51, 15);
            this.moneyAmountLbl.TabIndex = 3;
            this.moneyAmountLbl.Text = "Amount";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moneyProgressBar});
            this.ProgressBar.Location = new System.Drawing.Point(0, 289);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(584, 22);
            this.ProgressBar.TabIndex = 4;
            this.ProgressBar.Text = "Progress Bar";
            // 
            // moneyProgressBar
            // 
            this.moneyProgressBar.Name = "moneyProgressBar";
            this.moneyProgressBar.Size = new System.Drawing.Size(570, 16);
            // 
            // MoneyOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 311);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.moneyAmountLbl);
            this.Controls.Add(this.moneyAmountTxtbx);
            this.Controls.Add(this.SubmitBtn);
            this.Controls.Add(this.cheatChkbx);
            this.Location = new System.Drawing.Point(50, 50);
            this.Name = "MoneyOrder";
            this.Text = "Money Order";
            this.ProgressBar.ResumeLayout(false);
            this.ProgressBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox cheatChkbx;
        private Button SubmitBtn;
        private TextBox moneyAmountTxtbx;
        private Label moneyAmountLbl;
        private StatusStrip ProgressBar;
        private ToolStripProgressBar moneyProgressBar;
    }
}