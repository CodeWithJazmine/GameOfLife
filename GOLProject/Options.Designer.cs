namespace GOLProject
{
    partial class Options
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
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.widthOfCellsLabel = new System.Windows.Forms.Label();
            this.heightOfCellsLabel = new System.Windows.Forms.Label();
            this.widthUpDown = new System.Windows.Forms.NumericUpDown();
            this.heightUpDown = new System.Windows.Forms.NumericUpDown();
            this.timerIntervalLabel = new System.Windows.Forms.Label();
            this.timerUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timerUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(46, 118);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(127, 118);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // widthOfCellsLabel
            // 
            this.widthOfCellsLabel.AutoSize = true;
            this.widthOfCellsLabel.Location = new System.Drawing.Point(32, 49);
            this.widthOfCellsLabel.Name = "widthOfCellsLabel";
            this.widthOfCellsLabel.Size = new System.Drawing.Size(128, 13);
            this.widthOfCellsLabel.TabIndex = 2;
            this.widthOfCellsLabel.Text = "Width of Universe in Cells";
            // 
            // heightOfCellsLabel
            // 
            this.heightOfCellsLabel.AutoSize = true;
            this.heightOfCellsLabel.Location = new System.Drawing.Point(29, 75);
            this.heightOfCellsLabel.Name = "heightOfCellsLabel";
            this.heightOfCellsLabel.Size = new System.Drawing.Size(131, 13);
            this.heightOfCellsLabel.TabIndex = 3;
            this.heightOfCellsLabel.Text = "Height of Universe in Cells";
            // 
            // widthUpDown
            // 
            this.widthUpDown.Location = new System.Drawing.Point(166, 47);
            this.widthUpDown.Name = "widthUpDown";
            this.widthUpDown.Size = new System.Drawing.Size(53, 20);
            this.widthUpDown.TabIndex = 4;
            // 
            // heightUpDown
            // 
            this.heightUpDown.Location = new System.Drawing.Point(166, 73);
            this.heightUpDown.Name = "heightUpDown";
            this.heightUpDown.Size = new System.Drawing.Size(53, 20);
            this.heightUpDown.TabIndex = 5;
            // 
            // timerIntervalLabel
            // 
            this.timerIntervalLabel.AutoSize = true;
            this.timerIntervalLabel.Location = new System.Drawing.Point(18, 23);
            this.timerIntervalLabel.Name = "timerIntervalLabel";
            this.timerIntervalLabel.Size = new System.Drawing.Size(142, 13);
            this.timerIntervalLabel.TabIndex = 6;
            this.timerIntervalLabel.Text = "Timer Interval in Milliseconds";
            // 
            // timerUpDown
            // 
            this.timerUpDown.Location = new System.Drawing.Point(166, 21);
            this.timerUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.timerUpDown.Name = "timerUpDown";
            this.timerUpDown.Size = new System.Drawing.Size(53, 20);
            this.timerUpDown.TabIndex = 7;
            // 
            // Options
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(248, 166);
            this.Controls.Add(this.timerUpDown);
            this.Controls.Add(this.timerIntervalLabel);
            this.Controls.Add(this.heightUpDown);
            this.Controls.Add(this.widthUpDown);
            this.Controls.Add(this.heightOfCellsLabel);
            this.Controls.Add(this.widthOfCellsLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            ((System.ComponentModel.ISupportInitialize)(this.widthUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timerUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label widthOfCellsLabel;
        private System.Windows.Forms.Label heightOfCellsLabel;
        private System.Windows.Forms.NumericUpDown widthUpDown;
        private System.Windows.Forms.NumericUpDown heightUpDown;
        private System.Windows.Forms.Label timerIntervalLabel;
        private System.Windows.Forms.NumericUpDown timerUpDown;
    }
}