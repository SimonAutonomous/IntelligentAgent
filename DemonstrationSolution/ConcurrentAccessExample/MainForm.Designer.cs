namespace ConcurrentAccessExample
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.runWithoutLockButton = new System.Windows.Forms.ToolStripButton();
            this.runWithLockButton = new System.Windows.Forms.ToolStripButton();
            this.informationListBox = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(434, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runWithoutLockButton,
            this.runWithLockButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(434, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // runWithoutLockButton
            // 
            this.runWithoutLockButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.runWithoutLockButton.Image = ((System.Drawing.Image)(resources.GetObject("runWithoutLockButton.Image")));
            this.runWithoutLockButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runWithoutLockButton.Name = "runWithoutLockButton";
            this.runWithoutLockButton.Size = new System.Drawing.Size(101, 22);
            this.runWithoutLockButton.Text = "Run without lock";
            this.runWithoutLockButton.Click += new System.EventHandler(this.runWithoutLockButton_Click);
            // 
            // runWithLockButton
            // 
            this.runWithLockButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.runWithLockButton.Image = ((System.Drawing.Image)(resources.GetObject("runWithLockButton.Image")));
            this.runWithLockButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runWithLockButton.Name = "runWithLockButton";
            this.runWithLockButton.Size = new System.Drawing.Size(83, 22);
            this.runWithLockButton.Text = "Run with lock";
            this.runWithLockButton.Click += new System.EventHandler(this.runWithLockButton_Click);
            // 
            // informationListBox
            // 
            this.informationListBox.BackColor = System.Drawing.Color.Black;
            this.informationListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.informationListBox.ForeColor = System.Drawing.Color.Lime;
            this.informationListBox.FormattingEnabled = true;
            this.informationListBox.Location = new System.Drawing.Point(0, 49);
            this.informationListBox.Name = "informationListBox";
            this.informationListBox.Size = new System.Drawing.Size(434, 304);
            this.informationListBox.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 353);
            this.Controls.Add(this.informationListBox);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton runWithoutLockButton;
        private System.Windows.Forms.ToolStripButton runWithLockButton;
        private System.Windows.Forms.ListBox informationListBox;
    }
}

