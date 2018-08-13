namespace OOPExample
{
    partial class OOPMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OOPMainForm));
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.runExampleButton = new System.Windows.Forms.ToolStripButton();
            this.classExampleTextBox = new System.Windows.Forms.TextBox();
            this.exitButton = new System.Windows.Forms.ToolStripButton();
            this.mainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runExampleButton,
            this.exitButton});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(558, 25);
            this.mainToolStrip.TabIndex = 2;
            this.mainToolStrip.Text = "Exit";
            // 
            // runExampleButton
            // 
            this.runExampleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.runExampleButton.ForeColor = System.Drawing.Color.Black;
            this.runExampleButton.Image = ((System.Drawing.Image)(resources.GetObject("runExampleButton.Image")));
            this.runExampleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runExampleButton.Name = "runExampleButton";
            this.runExampleButton.Size = new System.Drawing.Size(79, 22);
            this.runExampleButton.Text = "Run example";
            this.runExampleButton.Click += new System.EventHandler(this.runExampleButton_Click);
            // 
            // classExampleTextBox
            // 
            this.classExampleTextBox.BackColor = System.Drawing.Color.Black;
            this.classExampleTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.classExampleTextBox.ForeColor = System.Drawing.Color.Lime;
            this.classExampleTextBox.Location = new System.Drawing.Point(0, 25);
            this.classExampleTextBox.Multiline = true;
            this.classExampleTextBox.Name = "classExampleTextBox";
            this.classExampleTextBox.ReadOnly = true;
            this.classExampleTextBox.Size = new System.Drawing.Size(558, 116);
            this.classExampleTextBox.TabIndex = 3;
            // 
            // exitButton
            // 
            this.exitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exitButton.ForeColor = System.Drawing.Color.Black;
            this.exitButton.Image = ((System.Drawing.Image)(resources.GetObject("exitButton.Image")));
            this.exitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(29, 22);
            this.exitButton.Text = "Exit";
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 141);
            this.Controls.Add(this.classExampleTextBox);
            this.Controls.Add(this.mainToolStrip);
            this.ForeColor = System.Drawing.Color.Lime;
            this.Name = "MainForm";
            this.Text = "Class example";
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton runExampleButton;
        private System.Windows.Forms.TextBox classExampleTextBox;
        private System.Windows.Forms.ToolStripButton exitButton;

    }
}

