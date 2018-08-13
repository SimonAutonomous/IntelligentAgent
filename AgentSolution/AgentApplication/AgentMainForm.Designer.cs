namespace AgentApplication
{
    partial class AgentMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgentMainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateAgentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoarrangeWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.startButton = new System.Windows.Forms.ToolStripButton();
            this.stopButton = new System.Windows.Forms.ToolStripButton();
            this.longTermMemoryTabPage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.memoryItemDataGridView = new AgentLibrary.Visualization.MemoryItemDataGridView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.importMemoryItemsButton = new System.Windows.Forms.ToolStripButton();
            this.saveLongTermMemoryButton = new System.Windows.Forms.ToolStripButton();
            this.longTermMemoryViewer = new AgentLibrary.Visualization.LongTermMemoryViewer();
            this.communicationLogTabPage = new System.Windows.Forms.TabPage();
            this.communicationLogColorListBox = new CustomUserControlsLibrary.ColorListBox();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.workingMemoryTabPage = new System.Windows.Forms.TabPage();
            this.workingMemoryViewer = new AgentLibrary.Visualization.WorkingMemoryViewer();
            this.dialoguesTabPage = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dialogueListCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.longTermMemoryTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoryItemDataGridView)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.communicationLogTabPage.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.workingMemoryTabPage.SuspendLayout();
            this.dialoguesTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(858, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateAgentToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // generateAgentToolStripMenuItem
            // 
            this.generateAgentToolStripMenuItem.Name = "generateAgentToolStripMenuItem";
            this.generateAgentToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.generateAgentToolStripMenuItem.Text = "Generate agent";
            this.generateAgentToolStripMenuItem.Click += new System.EventHandler(this.generateAgentToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(151, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoarrangeWindowsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // autoarrangeWindowsToolStripMenuItem
            // 
            this.autoarrangeWindowsToolStripMenuItem.Checked = true;
            this.autoarrangeWindowsToolStripMenuItem.CheckOnClick = true;
            this.autoarrangeWindowsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoarrangeWindowsToolStripMenuItem.Name = "autoarrangeWindowsToolStripMenuItem";
            this.autoarrangeWindowsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.autoarrangeWindowsToolStripMenuItem.Text = "Autoarrange windows";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startButton,
            this.stopButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(858, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // startButton
            // 
            this.startButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.startButton.Enabled = false;
            this.startButton.Image = ((System.Drawing.Image)(resources.GetObject("startButton.Image")));
            this.startButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(35, 22);
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
            this.stopButton.Size = new System.Drawing.Size(35, 22);
            this.stopButton.Text = "Stop";
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // longTermMemoryTabPage
            // 
            this.longTermMemoryTabPage.Controls.Add(this.splitContainer1);
            this.longTermMemoryTabPage.Location = new System.Drawing.Point(4, 22);
            this.longTermMemoryTabPage.Name = "longTermMemoryTabPage";
            this.longTermMemoryTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.longTermMemoryTabPage.Size = new System.Drawing.Size(850, 360);
            this.longTermMemoryTabPage.TabIndex = 2;
            this.longTermMemoryTabPage.Text = "Long-term memory";
            this.longTermMemoryTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.memoryItemDataGridView);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.longTermMemoryViewer);
            this.splitContainer1.Size = new System.Drawing.Size(844, 354);
            this.splitContainer1.SplitterDistance = 94;
            this.splitContainer1.TabIndex = 0;
            // 
            // memoryItemDataGridView
            // 
            this.memoryItemDataGridView.BackgroundColor = System.Drawing.Color.Black;
            this.memoryItemDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.memoryItemDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.memoryItemDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoryItemDataGridView.Location = new System.Drawing.Point(0, 25);
            this.memoryItemDataGridView.Name = "memoryItemDataGridView";
            this.memoryItemDataGridView.Size = new System.Drawing.Size(844, 69);
            this.memoryItemDataGridView.TabIndex = 1;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importMemoryItemsButton,
            this.saveLongTermMemoryButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(844, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // importMemoryItemsButton
            // 
            this.importMemoryItemsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.importMemoryItemsButton.Enabled = false;
            this.importMemoryItemsButton.Image = ((System.Drawing.Image)(resources.GetObject("importMemoryItemsButton.Image")));
            this.importMemoryItemsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.importMemoryItemsButton.Name = "importMemoryItemsButton";
            this.importMemoryItemsButton.Size = new System.Drawing.Size(79, 22);
            this.importMemoryItemsButton.Text = "Import items";
            this.importMemoryItemsButton.Click += new System.EventHandler(this.importMemoryItemsButton_Click);
            // 
            // saveLongTermMemoryButton
            // 
            this.saveLongTermMemoryButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveLongTermMemoryButton.Enabled = false;
            this.saveLongTermMemoryButton.Image = ((System.Drawing.Image)(resources.GetObject("saveLongTermMemoryButton.Image")));
            this.saveLongTermMemoryButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveLongTermMemoryButton.Name = "saveLongTermMemoryButton";
            this.saveLongTermMemoryButton.Size = new System.Drawing.Size(140, 22);
            this.saveLongTermMemoryButton.Text = "Save long-term memory";
            this.saveLongTermMemoryButton.Click += new System.EventHandler(this.saveLongTermMemoryButton_Click);
            // 
            // longTermMemoryViewer
            // 
            this.longTermMemoryViewer.BackColor = System.Drawing.Color.Black;
            this.longTermMemoryViewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.longTermMemoryViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.longTermMemoryViewer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.longTermMemoryViewer.ForeColor = System.Drawing.Color.Lime;
            this.longTermMemoryViewer.FormattingEnabled = true;
            this.longTermMemoryViewer.IntegralHeight = false;
            this.longTermMemoryViewer.Location = new System.Drawing.Point(0, 0);
            this.longTermMemoryViewer.Name = "longTermMemoryViewer";
            this.longTermMemoryViewer.SelectedItemBackColor = System.Drawing.Color.Empty;
            this.longTermMemoryViewer.SelectedItemForeColor = System.Drawing.Color.Empty;
            this.longTermMemoryViewer.Size = new System.Drawing.Size(844, 256);
            this.longTermMemoryViewer.TabIndex = 0;
            this.longTermMemoryViewer.SelectedIndexChanged += new System.EventHandler(this.longTermMemoryViewer_SelectedIndexChanged);
            // 
            // communicationLogTabPage
            // 
            this.communicationLogTabPage.Controls.Add(this.communicationLogColorListBox);
            this.communicationLogTabPage.Location = new System.Drawing.Point(4, 22);
            this.communicationLogTabPage.Name = "communicationLogTabPage";
            this.communicationLogTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.communicationLogTabPage.Size = new System.Drawing.Size(850, 360);
            this.communicationLogTabPage.TabIndex = 0;
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
            this.communicationLogColorListBox.Size = new System.Drawing.Size(844, 354);
            this.communicationLogColorListBox.TabIndex = 0;
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.communicationLogTabPage);
            this.mainTabControl.Controls.Add(this.workingMemoryTabPage);
            this.mainTabControl.Controls.Add(this.longTermMemoryTabPage);
            this.mainTabControl.Controls.Add(this.dialoguesTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 49);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(858, 386);
            this.mainTabControl.TabIndex = 2;
            // 
            // workingMemoryTabPage
            // 
            this.workingMemoryTabPage.Controls.Add(this.workingMemoryViewer);
            this.workingMemoryTabPage.Location = new System.Drawing.Point(4, 22);
            this.workingMemoryTabPage.Name = "workingMemoryTabPage";
            this.workingMemoryTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.workingMemoryTabPage.Size = new System.Drawing.Size(850, 360);
            this.workingMemoryTabPage.TabIndex = 4;
            this.workingMemoryTabPage.Text = "Working memory";
            this.workingMemoryTabPage.UseVisualStyleBackColor = true;
            // 
            // workingMemoryViewer
            // 
            this.workingMemoryViewer.BackColor = System.Drawing.Color.Black;
            this.workingMemoryViewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.workingMemoryViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workingMemoryViewer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.workingMemoryViewer.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.workingMemoryViewer.ForeColor = System.Drawing.Color.Lime;
            this.workingMemoryViewer.FormattingEnabled = true;
            this.workingMemoryViewer.Location = new System.Drawing.Point(3, 3);
            this.workingMemoryViewer.Name = "workingMemoryViewer";
            this.workingMemoryViewer.SelectedItemBackColor = System.Drawing.Color.Empty;
            this.workingMemoryViewer.SelectedItemForeColor = System.Drawing.Color.Empty;
            this.workingMemoryViewer.Size = new System.Drawing.Size(844, 354);
            this.workingMemoryViewer.TabIndex = 0;
            // 
            // dialoguesTabPage
            // 
            this.dialoguesTabPage.Controls.Add(this.splitContainer2);
            this.dialoguesTabPage.Location = new System.Drawing.Point(4, 22);
            this.dialoguesTabPage.Name = "dialoguesTabPage";
            this.dialoguesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.dialoguesTabPage.Size = new System.Drawing.Size(850, 360);
            this.dialoguesTabPage.TabIndex = 3;
            this.dialoguesTabPage.Text = "Dialogues";
            this.dialoguesTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dialogueListCheckedListBox);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.Black;
            this.splitContainer2.Size = new System.Drawing.Size(844, 354);
            this.splitContainer2.SplitterDistance = 281;
            this.splitContainer2.TabIndex = 0;
            // 
            // dialogueListCheckedListBox
            // 
            this.dialogueListCheckedListBox.BackColor = System.Drawing.Color.Black;
            this.dialogueListCheckedListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dialogueListCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dialogueListCheckedListBox.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dialogueListCheckedListBox.ForeColor = System.Drawing.Color.Lime;
            this.dialogueListCheckedListBox.FormattingEnabled = true;
            this.dialogueListCheckedListBox.IntegralHeight = false;
            this.dialogueListCheckedListBox.Location = new System.Drawing.Point(0, 0);
            this.dialogueListCheckedListBox.Name = "dialogueListCheckedListBox";
            this.dialogueListCheckedListBox.Size = new System.Drawing.Size(281, 354);
            this.dialogueListCheckedListBox.TabIndex = 0;
            this.dialogueListCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.dialogueListCheckedListBox_ItemCheck);
            // 
            // AgentMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 435);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AgentMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agent";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AgentMainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.longTermMemoryTabPage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoryItemDataGridView)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.communicationLogTabPage.ResumeLayout(false);
            this.mainTabControl.ResumeLayout(false);
            this.workingMemoryTabPage.ResumeLayout(false);
            this.dialoguesTabPage.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton stopButton;
        private System.Windows.Forms.TabPage longTermMemoryTabPage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private AgentLibrary.Visualization.MemoryItemDataGridView memoryItemDataGridView;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private AgentLibrary.Visualization.LongTermMemoryViewer longTermMemoryViewer;
        private System.Windows.Forms.TabPage communicationLogTabPage;
        private CustomUserControlsLibrary.ColorListBox communicationLogColorListBox;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage dialoguesTabPage;
        private System.Windows.Forms.TabPage workingMemoryTabPage;
        private AgentLibrary.Visualization.WorkingMemoryViewer workingMemoryViewer;
        private System.Windows.Forms.ToolStripMenuItem generateAgentToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton startButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.CheckedListBox dialogueListCheckedListBox;
        private System.Windows.Forms.ToolStripButton importMemoryItemsButton;
        private System.Windows.Forms.ToolStripButton saveLongTermMemoryButton;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoarrangeWindowsToolStripMenuItem;
    }
}

