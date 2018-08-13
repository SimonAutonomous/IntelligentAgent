namespace SpeechApplication
{
    partial class SpeechMainForm
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
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.communicationLogTabPage = new System.Windows.Forms.TabPage();
            this.communicationLogListBox = new CustomUserControlsLibrary.ColorListBox();
            this.speechTabPage = new System.Windows.Forms.TabPage();
            this.speechColorListBox = new CustomUserControlsLibrary.ColorListBox();
            this.menuStrip1.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.communicationLogTabPage.SuspendLayout();
            this.speechTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(422, 24);
            this.menuStrip1.TabIndex = 2;
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
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.communicationLogTabPage);
            this.mainTabControl.Controls.Add(this.speechTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 24);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(422, 171);
            this.mainTabControl.TabIndex = 3;
            // 
            // communicationLogTabPage
            // 
            this.communicationLogTabPage.Controls.Add(this.communicationLogListBox);
            this.communicationLogTabPage.Location = new System.Drawing.Point(4, 22);
            this.communicationLogTabPage.Name = "communicationLogTabPage";
            this.communicationLogTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.communicationLogTabPage.Size = new System.Drawing.Size(414, 145);
            this.communicationLogTabPage.TabIndex = 0;
            this.communicationLogTabPage.Text = "Communication log";
            this.communicationLogTabPage.UseVisualStyleBackColor = true;
            // 
            // communicationLogListBox
            // 
            this.communicationLogListBox.BackColor = System.Drawing.Color.Black;
            this.communicationLogListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.communicationLogListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.communicationLogListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.communicationLogListBox.ForeColor = System.Drawing.Color.Lime;
            this.communicationLogListBox.FormattingEnabled = true;
            this.communicationLogListBox.Location = new System.Drawing.Point(3, 3);
            this.communicationLogListBox.Name = "communicationLogListBox";
            this.communicationLogListBox.SelectedItemBackColor = System.Drawing.Color.Empty;
            this.communicationLogListBox.SelectedItemForeColor = System.Drawing.Color.Empty;
            this.communicationLogListBox.Size = new System.Drawing.Size(408, 139);
            this.communicationLogListBox.TabIndex = 0;
            // 
            // speechTabPage
            // 
            this.speechTabPage.Controls.Add(this.speechColorListBox);
            this.speechTabPage.Location = new System.Drawing.Point(4, 22);
            this.speechTabPage.Name = "speechTabPage";
            this.speechTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.speechTabPage.Size = new System.Drawing.Size(414, 145);
            this.speechTabPage.TabIndex = 1;
            this.speechTabPage.Text = "Speech";
            this.speechTabPage.UseVisualStyleBackColor = true;
            // 
            // speechColorListBox
            // 
            this.speechColorListBox.BackColor = System.Drawing.Color.Black;
            this.speechColorListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.speechColorListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.speechColorListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.speechColorListBox.ForeColor = System.Drawing.Color.Lime;
            this.speechColorListBox.FormattingEnabled = true;
            this.speechColorListBox.Location = new System.Drawing.Point(3, 3);
            this.speechColorListBox.Name = "speechColorListBox";
            this.speechColorListBox.SelectedItemBackColor = System.Drawing.Color.Empty;
            this.speechColorListBox.SelectedItemForeColor = System.Drawing.Color.Empty;
            this.speechColorListBox.Size = new System.Drawing.Size(408, 139);
            this.speechColorListBox.TabIndex = 0;
            // 
            // SpeechMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 195);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.menuStrip1);
            this.Name = "SpeechMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Speech";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpeechMainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.communicationLogTabPage.ResumeLayout(false);
            this.speechTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage communicationLogTabPage;
        private CustomUserControlsLibrary.ColorListBox communicationLogListBox;
        private System.Windows.Forms.TabPage speechTabPage;
        private CustomUserControlsLibrary.ColorListBox speechColorListBox;
    }
}

