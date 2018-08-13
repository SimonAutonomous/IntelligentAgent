namespace TestApplication
{
    partial class MainForm
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
            this.inputStringTextBox = new System.Windows.Forms.TextBox();
            this.checkMatchButton = new System.Windows.Forms.Button();
            this.patternDefinitionTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // inputStringTextBox
            // 
            this.inputStringTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputStringTextBox.Location = new System.Drawing.Point(62, 35);
            this.inputStringTextBox.Name = "inputStringTextBox";
            this.inputStringTextBox.Size = new System.Drawing.Size(355, 20);
            this.inputStringTextBox.TabIndex = 1;
            this.inputStringTextBox.TextChanged += new System.EventHandler(this.inputStringTextBox_TextChanged);
            // 
            // checkMatchButton
            // 
            this.checkMatchButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkMatchButton.Location = new System.Drawing.Point(62, 61);
            this.checkMatchButton.Name = "checkMatchButton";
            this.checkMatchButton.Size = new System.Drawing.Size(355, 23);
            this.checkMatchButton.TabIndex = 2;
            this.checkMatchButton.Text = "Check match";
            this.checkMatchButton.UseVisualStyleBackColor = true;
            this.checkMatchButton.Click += new System.EventHandler(this.checkMatchButton_Click);
            // 
            // patternDefinitionTextBox
            // 
            this.patternDefinitionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.patternDefinitionTextBox.Location = new System.Drawing.Point(62, 9);
            this.patternDefinitionTextBox.Name = "patternDefinitionTextBox";
            this.patternDefinitionTextBox.Size = new System.Drawing.Size(355, 20);
            this.patternDefinitionTextBox.TabIndex = 0;
            this.patternDefinitionTextBox.TextChanged += new System.EventHandler(this.patternDefinitionTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Pattern:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Sample:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 101);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.patternDefinitionTextBox);
            this.Controls.Add(this.checkMatchButton);
            this.Controls.Add(this.inputStringTextBox);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pattern matching tester application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputStringTextBox;
        private System.Windows.Forms.Button checkMatchButton;
        private System.Windows.Forms.TextBox patternDefinitionTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

