namespace RSSReaderApplication
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
            this.settingsToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.settingsToolStrip2 = new System.Windows.Forms.ToolStrip();
            this.actionsToolStrip = new System.Windows.Forms.ToolStrip();
            this.startButton = new System.Windows.Forms.ToolStripButton();
            this.stopButton = new System.Windows.Forms.ToolStripButton();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.dateFormatTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.downloadIntervalTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.rssFeedURLTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.rssItemsListBox = new CustomUserControlsLibrary.ColorListBox();
            this.menuStrip1.SuspendLayout();
            this.settingsToolStrip1.SuspendLayout();
            this.settingsToolStrip2.SuspendLayout();
            this.actionsToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startButton,
            this.stopButton});
            this.menuStrip1.Location = new System.Drawing.Point(0, 75);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(785, 26);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStrip1
            // 
            this.settingsToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.dateFormatTextBox,
            this.toolStripLabel3,
            this.downloadIntervalTextBox});
            this.settingsToolStrip1.Location = new System.Drawing.Point(0, 50);
            this.settingsToolStrip1.Name = "settingsToolStrip1";
            this.settingsToolStrip1.Size = new System.Drawing.Size(785, 25);
            this.settingsToolStrip1.TabIndex = 7;
            this.settingsToolStrip1.Text = "toolStrip1";
            // 
            // settingsToolStrip2
            // 
            this.settingsToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.rssFeedURLTextBox});
            this.settingsToolStrip2.Location = new System.Drawing.Point(0, 25);
            this.settingsToolStrip2.Name = "settingsToolStrip2";
            this.settingsToolStrip2.Size = new System.Drawing.Size(785, 25);
            this.settingsToolStrip2.TabIndex = 8;
            this.settingsToolStrip2.Text = "toolStrip1";
            // 
            // actionsToolStrip
            // 
            this.actionsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.actionsToolStrip.Location = new System.Drawing.Point(0, 0);
            this.actionsToolStrip.Name = "actionsToolStrip";
            this.actionsToolStrip.Size = new System.Drawing.Size(785, 25);
            this.actionsToolStrip.TabIndex = 9;
            this.actionsToolStrip.Text = "toolStrip1";
            // 
            // startButton
            // 
            this.startButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.startButton.Image = ((System.Drawing.Image)(resources.GetObject("startButton.Image")));
            this.startButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(35, 19);
            this.startButton.Text = "Start";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stopButton.Enabled = false;
            this.stopButton.Image = ((System.Drawing.Image)(resources.GetObject("stopButton.Image")));
            this.stopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(35, 19);
            this.stopButton.Text = "Stop";
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 25);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(73, 22);
            this.toolStripLabel2.Text = "Date format:";
            // 
            // dateFormatTextBox
            // 
            this.dateFormatTextBox.Name = "dateFormatTextBox";
            this.dateFormatTextBox.Size = new System.Drawing.Size(250, 25);
            this.dateFormatTextBox.Text = "ddd, dd MMM yyyy HH:mm:ss \'GMT\'";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(119, 22);
            this.toolStripLabel3.Text = "Download interval (s)";
            // 
            // downloadIntervalTextBox
            // 
            this.downloadIntervalTextBox.Name = "downloadIntervalTextBox";
            this.downloadIntervalTextBox.Size = new System.Drawing.Size(51, 25);
            this.downloadIntervalTextBox.Text = "30";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(75, 22);
            this.toolStripLabel1.Text = "RSS feed url: ";
            // 
            // rssFeedURLTextBox
            // 
            this.rssFeedURLTextBox.Name = "rssFeedURLTextBox";
            this.rssFeedURLTextBox.Size = new System.Drawing.Size(616, 25);
            this.rssFeedURLTextBox.Text = "http://feeds.bbci.co.uk/news/world/rss.xml";
            // 
            // rssItemsListBox
            // 
            this.rssItemsListBox.BackColor = System.Drawing.Color.Black;
            this.rssItemsListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rssItemsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rssItemsListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.rssItemsListBox.ForeColor = System.Drawing.Color.Lime;
            this.rssItemsListBox.FormattingEnabled = true;
            this.rssItemsListBox.IntegralHeight = false;
            this.rssItemsListBox.Location = new System.Drawing.Point(0, 101);
            this.rssItemsListBox.Name = "rssItemsListBox";
            this.rssItemsListBox.SelectedItemBackColor = System.Drawing.Color.Empty;
            this.rssItemsListBox.SelectedItemForeColor = System.Drawing.Color.Empty;
            this.rssItemsListBox.Size = new System.Drawing.Size(785, 387);
            this.rssItemsListBox.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 488);
            this.Controls.Add(this.rssItemsListBox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.settingsToolStrip1);
            this.Controls.Add(this.settingsToolStrip2);
            this.Controls.Add(this.actionsToolStrip);
            this.Name = "MainForm";
            this.Text = "RSS Reader (c) 2018 Mattias Wahde, mattias.wahde@chalmers.se";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.settingsToolStrip1.ResumeLayout(false);
            this.settingsToolStrip1.PerformLayout();
            this.settingsToolStrip2.ResumeLayout(false);
            this.settingsToolStrip2.PerformLayout();
            this.actionsToolStrip.ResumeLayout(false);
            this.actionsToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip settingsToolStrip1;
        private System.Windows.Forms.ToolStrip settingsToolStrip2;
        private System.Windows.Forms.ToolStrip actionsToolStrip;
        private System.Windows.Forms.ToolStripButton startButton;
        private System.Windows.Forms.ToolStripButton stopButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox dateFormatTextBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox downloadIntervalTextBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox rssFeedURLTextBox;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private CustomUserControlsLibrary.ColorListBox rssItemsListBox;
    }
}