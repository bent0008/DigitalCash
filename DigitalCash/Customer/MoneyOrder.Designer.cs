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
            this.cheatChkbx.Location = new System.Drawing.Point(316, 185);
            this.cheatChkbx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cheatChkbx.Name = "cheatChkbx";
            this.cheatChkbx.Size = new System.Drawing.Size(69, 24);
            this.cheatChkbx.TabIndex = 0;
            this.cheatChkbx.Text = "Cheat";
            this.cheatChkbx.UseVisualStyleBackColor = true;
            // 
            // SubmitBtn
            // 
            this.SubmitBtn.Location = new System.Drawing.Point(299, 324);
            this.SubmitBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SubmitBtn.Name = "SubmitBtn";
            this.SubmitBtn.Size = new System.Drawing.Size(86, 31);
            this.SubmitBtn.TabIndex = 1;
            this.SubmitBtn.Text = "Submit";
            this.SubmitBtn.UseVisualStyleBackColor = true;
            this.SubmitBtn.Click += new System.EventHandler(this.SubmitBtn_Click);
            // 
            // moneyAmountTxtbx
            // 
            this.moneyAmountTxtbx.Location = new System.Drawing.Point(338, 65);
            this.moneyAmountTxtbx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.moneyAmountTxtbx.Name = "moneyAmountTxtbx";
            this.moneyAmountTxtbx.Size = new System.Drawing.Size(114, 27);
            this.moneyAmountTxtbx.TabIndex = 2;
            // 
            // moneyAmountLbl
            // 
            this.moneyAmountLbl.AutoSize = true;
            this.moneyAmountLbl.Location = new System.Drawing.Point(273, 69);
            this.moneyAmountLbl.Name = "moneyAmountLbl";
            this.moneyAmountLbl.Size = new System.Drawing.Size(62, 20);
            this.moneyAmountLbl.TabIndex = 3;
            this.moneyAmountLbl.Text = "Amount";
            // 
            // ProgressBar
            // 
            this.ProgressBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ProgressBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moneyProgressBar});
            this.ProgressBar.Location = new System.Drawing.Point(0, 388);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.ProgressBar.Size = new System.Drawing.Size(667, 27);
            this.ProgressBar.TabIndex = 4;
            this.ProgressBar.Text = "Progress Bar";
            // 
            // moneyProgressBar
            // 
            this.moneyProgressBar.Name = "moneyProgressBar";
            this.moneyProgressBar.Size = new System.Drawing.Size(651, 19);
            // 
            // MoneyOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 415);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.moneyAmountLbl);
            this.Controls.Add(this.moneyAmountTxtbx);
            this.Controls.Add(this.SubmitBtn);
            this.Controls.Add(this.cheatChkbx);
            this.Location = new System.Drawing.Point(50, 50);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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