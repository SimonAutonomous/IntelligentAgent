namespace EventHandlerExample
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
            this.runButton = new System.Windows.Forms.ToolStripButton();
            this.progressListBox = new System.Windows.Forms.ListBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(450, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // runButton
            // 
            this.runButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.runButton.Image = ((System.Drawing.Image)(resources.GetObject("runButton.Image")));
            this.runButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(32, 22);
            this.runButton.Text = "Run";
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // progressListBox
            // 
            this.progressListBox.BackColor = System.Drawing.Color.Black;
            this.progressListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressListBox.ForeColor = System.Drawing.Color.Lime;
            this.progressListBox.FormattingEnabled = true;
            this.progressListBox.Location = new System.Drawing.Point(0, 25);
            this.progressListBox.Name = "progressListBox";
            this.progressListBox.Size = new System.Drawing.Size(450, 236);
            this.progressListBox.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 261);
            this.Controls.Add(this.progressListBox);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainForm";
            this.Text = "Event handler example";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton runButton;
        private System.Windows.Forms.ListBox progressListBox;
    }
}

