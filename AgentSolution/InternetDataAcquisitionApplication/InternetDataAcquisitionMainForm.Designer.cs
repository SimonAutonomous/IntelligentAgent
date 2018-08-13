namespace InternetDataAcquisitionApplication
{
    partial class InternetDataAcquisitionMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InternetDataAcquisitionMainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startRSSReaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.acquiredDataTabPage = new System.Windows.Forms.TabPage();
            this.acquiredDataColorListBox = new CustomUserControlsLibrary.ColorListBox();
            this.searchRequestsTabPage = new System.Windows.Forms.TabPage();
            this.searchRequestsColorListBox = new CustomUserControlsLibrary.ColorListBox();
            this.searchRequestToolStrip = new System.Windows.Forms.ToolStrip();
            this.sendSearchRequestButton = new System.Windows.Forms.ToolStripButton();
            this.searchRequestTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.communicationLogTabPage = new System.Windows.Forms.TabPage();
            this.communicationLogColorListBox = new CustomUserControlsLibrary.ColorListBox();
            this.menuStrip1.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.acquiredDataTabPage.SuspendLayout();
            this.searchRequestsTabPage.SuspendLayout();
            this.searchRequestToolStrip.SuspendLayout();
            this.communicationLogTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.testsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(449, 24);
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
            // testsToolStripMenuItem
            // 
            this.testsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startRSSReaderToolStripMenuItem});
            this.testsToolStripMenuItem.Name = "testsToolStripMenuItem";
            this.testsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.testsToolStripMenuItem.Text = "Tests";
            // 
            // startRSSReaderToolStripMenuItem
            // 
            this.startRSSReaderToolStripMenuItem.Name = "startRSSReaderToolStripMenuItem";
            this.startRSSReaderToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.startRSSReaderToolStripMenuItem.Text = "Start RSS reader";
            this.startRSSReaderToolStripMenuItem.Click += new System.EventHandler(this.startRSSReaderToolStripMenuItem_Click);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.acquiredDataTabPage);
            this.mainTabControl.Controls.Add(this.searchRequestsTabPage);
            this.mainTabControl.Controls.Add(this.communicationLogTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 24);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(449, 160);
            this.mainTabControl.TabIndex = 1;
            // 
            // acquiredDataTabPage
            // 
            this.acquiredDataTabPage.Controls.Add(this.acquiredDataColorListBox);
            this.acquiredDataTabPage.Location = new System.Drawing.Point(4, 22);
            this.acquiredDataTabPage.Name = "acquiredDataTabPage";
            this.acquiredDataTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.acquiredDataTabPage.Size = new System.Drawing.Size(441, 134);
            this.acquiredDataTabPage.TabIndex = 0;
            this.acquiredDataTabPage.Text = "Acquired data";
            this.acquiredDataTabPage.UseVisualStyleBackColor = true;
            // 
            // acquiredDataColorListBox
            // 
            this.acquiredDataColorListBox.BackColor = System.Drawing.Color.Black;
            this.acquiredDataColorListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.acquiredDataColorListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.acquiredDataColorListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.acquiredDataColorListBox.ForeColor = System.Drawing.Color.Lime;
            this.acquiredDataColorListBox.FormattingEnabled = true;
            this.acquiredDataColorListBox.Location = new System.Drawing.Point(3, 3);
            this.acquiredDataColorListBox.Name = "acquiredDataColorListBox";
            this.acquiredDataColorListBox.SelectedItemBackColor = System.Drawing.Color.Empty;
            this.acquiredDataColorListBox.SelectedItemForeColor = System.Drawing.Color.Empty;
            this.acquiredDataColorListBox.Size = new System.Drawing.Size(435, 128);
            this.acquiredDataColorListBox.TabIndex = 1;
            // 
            // searchRequestsTabPage
            // 
            this.searchRequestsTabPage.Controls.Add(this.searchRequestsColorListBox);
            this.searchRequestsTabPage.Controls.Add(this.searchRequestToolStrip);
            this.searchRequestsTabPage.Location = new System.Drawing.Point(4, 22);
            this.searchRequestsTabPage.Name = "searchRequestsTabPage";
            this.searchRequestsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.searchRequestsTabPage.Size = new System.Drawing.Size(441, 134);
            this.searchRequestsTabPage.TabIndex = 2;
            this.searchRequestsTabPage.Text = "Search requests";
            this.searchRequestsTabPage.UseVisualStyleBackColor = true;
            // 
            // searchRequestsColorListBox
            // 
            this.searchRequestsColorListBox.BackColor = System.Drawing.Color.Black;
            this.searchRequestsColorListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchRequestsColorListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchRequestsColorListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.searchRequestsColorListBox.ForeColor = System.Drawing.Color.Lime;
            this.searchRequestsColorListBox.FormattingEnabled = true;
            this.searchRequestsColorListBox.Location = new System.Drawing.Point(3, 28);
            this.searchRequestsColorListBox.Name = "searchRequestsColorListBox";
            this.searchRequestsColorListBox.SelectedItemBackColor = System.Drawing.Color.Empty;
            this.searchRequestsColorListBox.SelectedItemForeColor = System.Drawing.Color.Empty;
            this.searchRequestsColorListBox.Size = new System.Drawing.Size(435, 103);
            this.searchRequestsColorListBox.TabIndex = 3;
            // 
            // searchRequestToolStrip
            // 
            this.searchRequestToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendSearchRequestButton,
            this.searchRequestTextBox});
            this.searchRequestToolStrip.Location = new System.Drawing.Point(3, 3);
            this.searchRequestToolStrip.Name = "searchRequestToolStrip";
            this.searchRequestToolStrip.Size = new System.Drawing.Size(435, 25);
            this.searchRequestToolStrip.TabIndex = 0;
            this.searchRequestToolStrip.Text = "toolStrip1";
            this.searchRequestToolStrip.Resize += new System.EventHandler(this.searchRequestToolStrip_Resize);
            // 
            // sendSearchRequestButton
            // 
            this.sendSearchRequestButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sendSearchRequestButton.Image = ((System.Drawing.Image)(resources.GetObject("sendSearchRequestButton.Image")));
            this.sendSearchRequestButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sendSearchRequestButton.Name = "sendSearchRequestButton";
            this.sendSearchRequestButton.Size = new System.Drawing.Size(116, 22);
            this.sendSearchRequestButton.Text = "Send search request";
            this.sendSearchRequestButton.Click += new System.EventHandler(this.sendSearchRequestButton_Click);
            // 
            // searchRequestTextBox
            // 
            this.searchRequestTextBox.Name = "searchRequestTextBox";
            this.searchRequestTextBox.Size = new System.Drawing.Size(200, 25);
            // 
            // communicationLogTabPage
            // 
            this.communicationLogTabPage.Controls.Add(this.communicationLogColorListBox);
            this.communicationLogTabPage.Location = new System.Drawing.Point(4, 22);
            this.communicationLogTabPage.Name = "communicationLogTabPage";
            this.communicationLogTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.communicationLogTabPage.Size = new System.Drawing.Size(441, 134);
            this.communicationLogTabPage.TabIndex = 1;
            this.communicationLogTabPage.Text = "Communication log";
            this.communicationLogTabPage.UseVisualStyleBackColor = true;
            // 
            // communicationLogColorListBox
            // 
            this.communicationLogColorListBox.BackColor = System.Drawing.Color.Black;
            this.communicationLogColorListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.communicationLogColorListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.communicationLogColorListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.communicationLogColorListBox.ForeColor = System.Drawing.Color.Lime;
            this.communicationLogColorListBox.FormattingEnabled = true;
            this.communicationLogColorListBox.Location = new System.Drawing.Point(3, 3);
            this.communicationLogColorListBox.Name = "communicationLogColorListBox";
            this.communicationLogColorListBox.SelectedItemBackColor = System.Drawing.Color.Empty;
            this.communicationLogColorListBox.SelectedItemForeColor = System.Drawing.Color.Empty;
            this.communicationLogColorListBox.Size = new System.Drawing.Size(435, 128);
            this.communicationLogColorListBox.TabIndex = 1;
            // 
            // InternetDataAcquisitionMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 184);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "InternetDataAcquisitionMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Internet data acquisition";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InternetDataAcquisitionMainForm_FormClosing);
            this.Load += new System.EventHandler(this.InternetDataAcquisitionMainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.acquiredDataTabPage.ResumeLayout(false);
            this.searchRequestsTabPage.ResumeLayout(false);
            this.searchRequestsTabPage.PerformLayout();
            this.searchRequestToolStrip.ResumeLayout(false);
            this.searchRequestToolStrip.PerformLayout();
            this.communicationLogTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage acquiredDataTabPage;
        private System.Windows.Forms.TabPage communicationLogTabPage;
        private CustomUserControlsLibrary.ColorListBox acquiredDataColorListBox;
        private CustomUserControlsLibrary.ColorListBox communicationLogColorListBox;
        private System.Windows.Forms.ToolStripMenuItem testsToolStripMenuItem;
        private System.Windows.Forms.TabPage searchRequestsTabPage;
        private System.Windows.Forms.ToolStripMenuItem startRSSReaderToolStripMenuItem;
        private CustomUserControlsLibrary.ColorListBox searchRequestsColorListBox;
        private System.Windows.Forms.ToolStrip searchRequestToolStrip;
        private System.Windows.Forms.ToolStripButton sendSearchRequestButton;
        private System.Windows.Forms.ToolStripTextBox searchRequestTextBox;
    }
}

