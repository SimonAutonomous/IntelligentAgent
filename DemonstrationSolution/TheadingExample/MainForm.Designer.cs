namespace TheadingExample
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.runInSingleThreadButton = new System.Windows.Forms.ToolStripButton();
            this.runMultiThreadedButton = new System.Windows.Forms.ToolStripButton();
            this.progressListBox = new System.Windows.Forms.ListBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runInSingleThreadButton,
            this.runMultiThreadedButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(494, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // runInSingleThreadButton
            // 
            this.runInSingleThreadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.runInSingleThreadButton.Image = ((System.Drawing.Image)(resources.GetObject("runInSingleThreadButton.Image")));
            this.runInSingleThreadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runInSingleThreadButton.Name = "runInSingleThreadButton";
            this.runInSingleThreadButton.Size = new System.Drawing.Size(116, 22);
            this.runInSingleThreadButton.Text = "Run in single thread";
            this.runInSingleThreadButton.Click += new System.EventHandler(this.runInSingleThreadButton_Click);
            // 
            // runMultiThreadedButton
            // 
            this.runMultiThreadedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.runMultiThreadedButton.Image = ((System.Drawing.Image)(resources.GetObject("runMultiThreadedButton.Image")));
            this.runMultiThreadedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runMultiThreadedButton.Name = "runMultiThreadedButton";
            this.runMultiThreadedButton.Size = new System.Drawing.Size(110, 22);
            this.runMultiThreadedButton.Text = "Run multithreaded";
            this.runMultiThreadedButton.Click += new System.EventHandler(this.runMultiThreadedButton_Click);
            // 
            // sumListBox
            // 
            this.progressListBox.BackColor = System.Drawing.Color.Black;
            this.progressListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressListBox.ForeColor = System.Drawing.Color.Lime;
            this.progressListBox.FormattingEnabled = true;
            this.progressListBox.Location = new System.Drawing.Point(0, 25);
            this.progressListBox.Name = "sumListBox";
            this.progressListBox.Size = new System.Drawing.Size(494, 164);
            this.progressListBox.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 189);
            this.Controls.Add(this.progressListBox);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainForm";
            this.Text = "Threading example";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton runInSingleThreadButton;
        private System.Windows.Forms.ToolStripButton runMultiThreadedButton;
        private System.Windows.Forms.ListBox progressListBox;
    }
}

