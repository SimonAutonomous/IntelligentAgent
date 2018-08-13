namespace SerializationExample
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectListBox = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(475, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadObjectToolStripMenuItem,
            this.saveObjectToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadObjectToolStripMenuItem
            // 
            this.loadObjectToolStripMenuItem.Name = "loadObjectToolStripMenuItem";
            this.loadObjectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadObjectToolStripMenuItem.Text = "Load object";
            this.loadObjectToolStripMenuItem.Click += new System.EventHandler(this.loadObjectToolStripMenuItem_Click);
            // 
            // saveObjectToolStripMenuItem
            // 
            this.saveObjectToolStripMenuItem.Name = "saveObjectToolStripMenuItem";
            this.saveObjectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveObjectToolStripMenuItem.Text = "Save object";
            this.saveObjectToolStripMenuItem.Click += new System.EventHandler(this.saveObjectToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // objectListBox
            // 
            this.objectListBox.BackColor = System.Drawing.Color.Black;
            this.objectListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListBox.ForeColor = System.Drawing.Color.Lime;
            this.objectListBox.FormattingEnabled = true;
            this.objectListBox.Location = new System.Drawing.Point(0, 24);
            this.objectListBox.Name = "objectListBox";
            this.objectListBox.Size = new System.Drawing.Size(475, 183);
            this.objectListBox.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 207);
            this.Controls.Add(this.objectListBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Serialization example";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadObjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveObjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ListBox objectListBox;
    }
}

