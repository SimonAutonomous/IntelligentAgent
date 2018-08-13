namespace IsolatedWordRecognitionApplication
{
    partial class IWRMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IWRMainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speechRecognizerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.testSoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addWordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.parametersVisibleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.errorBarsVisibleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autorangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.setMaximumNonclippingVolumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.speechRecognizerTabPage = new System.Windows.Forms.TabPage();
            this.speechRecognizerMainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.availableWordsListBox = new System.Windows.Forms.ListBox();
            this.featurePlotPanel = new PlotLibrary.Plot2DPanel();
            this.speechRecognizerEditingToolStrip = new System.Windows.Forms.ToolStrip();
            this.featureLabel = new System.Windows.Forms.ToolStripLabel();
            this.featureComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.weightLabel = new System.Windows.Forms.ToolStripLabel();
            this.weightTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel11 = new System.Windows.Forms.ToolStripLabel();
            this.yMinTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel12 = new System.Windows.Forms.ToolStripLabel();
            this.yMaxTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.speechRecognizerToolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.autoCorrelationOrderTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel8 = new System.Windows.Forms.ToolStripLabel();
            this.lpcOrderTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel9 = new System.Windows.Forms.ToolStripLabel();
            this.cepstralOrderTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel10 = new System.Windows.Forms.ToolStripLabel();
            this.numberOfValuesPerFeatureTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.speechRecognizerToolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.frameDurationTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.frameShiftTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.alphaTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.speechRecognizerToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.soundExtractionMovingAverageLengthTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.soundExtractionThresholdTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.preEmphasisThresholdFrequencyTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.usageTabPage = new System.Windows.Forms.TabPage();
            this.usageMainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.soundVisualizer = new AudioLibrary.Visualization.SoundVisualizer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.recordToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.playSoundButton = new System.Windows.Forms.ToolStripButton();
            this.usageSecondarySplitContainer = new System.Windows.Forms.SplitContainer();
            this.deviationListBox = new System.Windows.Forms.ListBox();
            this.featureComparisonPlotPanel = new PlotLibrary.Plot2DPanel();
            this.featureComparisonComboBox = new System.Windows.Forms.ComboBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.recognizeButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.recognitionResultTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel13 = new System.Windows.Forms.ToolStripLabel();
            this.recognitionThresholdTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.menuStrip1.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.speechRecognizerTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speechRecognizerMainSplitContainer)).BeginInit();
            this.speechRecognizerMainSplitContainer.Panel1.SuspendLayout();
            this.speechRecognizerMainSplitContainer.Panel2.SuspendLayout();
            this.speechRecognizerMainSplitContainer.SuspendLayout();
            this.speechRecognizerEditingToolStrip.SuspendLayout();
            this.speechRecognizerToolStrip3.SuspendLayout();
            this.speechRecognizerToolStrip2.SuspendLayout();
            this.speechRecognizerToolStrip1.SuspendLayout();
            this.usageTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usageMainSplitContainer)).BeginInit();
            this.usageMainSplitContainer.Panel1.SuspendLayout();
            this.usageMainSplitContainer.Panel2.SuspendLayout();
            this.usageMainSplitContainer.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usageSecondarySplitContainer)).BeginInit();
            this.usageSecondarySplitContainer.Panel1.SuspendLayout();
            this.usageSecondarySplitContainer.Panel2.SuspendLayout();
            this.usageSecondarySplitContainer.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(897, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.speechRecognizerToolStripMenuItem,
            this.toolStripSeparator3,
            this.testSoundToolStripMenuItem,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // speechRecognizerToolStripMenuItem
            // 
            this.speechRecognizerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.speechRecognizerToolStripMenuItem.Name = "speechRecognizerToolStripMenuItem";
            this.speechRecognizerToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.speechRecognizerToolStripMenuItem.Text = "Speech recognizer";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(167, 6);
            // 
            // testSoundToolStripMenuItem
            // 
            this.testSoundToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem1,
            this.saveSoundToolStripMenuItem});
            this.testSoundToolStripMenuItem.Name = "testSoundToolStripMenuItem";
            this.testSoundToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.testSoundToolStripMenuItem.Text = "Test sound";
            // 
            // loadToolStripMenuItem1
            // 
            this.loadToolStripMenuItem1.Name = "loadToolStripMenuItem1";
            this.loadToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem1.Text = "Load";
            this.loadToolStripMenuItem1.Click += new System.EventHandler(this.loadToolStripMenuItem1_Click);
            // 
            // saveSoundToolStripMenuItem
            // 
            this.saveSoundToolStripMenuItem.Enabled = false;
            this.saveSoundToolStripMenuItem.Name = "saveSoundToolStripMenuItem";
            this.saveSoundToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.saveSoundToolStripMenuItem.Text = "Save";
            this.saveSoundToolStripMenuItem.Click += new System.EventHandler(this.saveSoundToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(167, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addWordToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // addWordToolStripMenuItem
            // 
            this.addWordToolStripMenuItem.Name = "addWordToolStripMenuItem";
            this.addWordToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.addWordToolStripMenuItem.Text = "Add word";
            this.addWordToolStripMenuItem.Click += new System.EventHandler(this.addWordToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator6,
            this.parametersVisibleToolStripMenuItem,
            this.toolStripSeparator2,
            this.errorBarsVisibleToolStripMenuItem,
            this.autorangeToolStripMenuItem,
            this.toolStripSeparator7,
            this.setMaximumNonclippingVolumeToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(259, 6);
            // 
            // parametersVisibleToolStripMenuItem
            // 
            this.parametersVisibleToolStripMenuItem.Checked = true;
            this.parametersVisibleToolStripMenuItem.CheckOnClick = true;
            this.parametersVisibleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.parametersVisibleToolStripMenuItem.Name = "parametersVisibleToolStripMenuItem";
            this.parametersVisibleToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.parametersVisibleToolStripMenuItem.Text = "Parameters visible";
            this.parametersVisibleToolStripMenuItem.Click += new System.EventHandler(this.parametersVisibleToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(259, 6);
            // 
            // errorBarsVisibleToolStripMenuItem
            // 
            this.errorBarsVisibleToolStripMenuItem.CheckOnClick = true;
            this.errorBarsVisibleToolStripMenuItem.Name = "errorBarsVisibleToolStripMenuItem";
            this.errorBarsVisibleToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.errorBarsVisibleToolStripMenuItem.Text = "Error bars visible";
            this.errorBarsVisibleToolStripMenuItem.Click += new System.EventHandler(this.errorBarsVisibleToolStripMenuItem_Click);
            // 
            // autorangeToolStripMenuItem
            // 
            this.autorangeToolStripMenuItem.CheckOnClick = true;
            this.autorangeToolStripMenuItem.Enabled = false;
            this.autorangeToolStripMenuItem.Name = "autorangeToolStripMenuItem";
            this.autorangeToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.autorangeToolStripMenuItem.Text = "Autorange";
            this.autorangeToolStripMenuItem.Click += new System.EventHandler(this.autorangeToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(259, 6);
            // 
            // setMaximumNonclippingVolumeToolStripMenuItem
            // 
            this.setMaximumNonclippingVolumeToolStripMenuItem.CheckOnClick = true;
            this.setMaximumNonclippingVolumeToolStripMenuItem.Name = "setMaximumNonclippingVolumeToolStripMenuItem";
            this.setMaximumNonclippingVolumeToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.setMaximumNonclippingVolumeToolStripMenuItem.Text = "Set maximum non-clipping volume";
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.speechRecognizerTabPage);
            this.mainTabControl.Controls.Add(this.usageTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 24);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(897, 555);
            this.mainTabControl.TabIndex = 1;
            // 
            // speechRecognizerTabPage
            // 
            this.speechRecognizerTabPage.Controls.Add(this.speechRecognizerMainSplitContainer);
            this.speechRecognizerTabPage.Controls.Add(this.speechRecognizerEditingToolStrip);
            this.speechRecognizerTabPage.Controls.Add(this.speechRecognizerToolStrip3);
            this.speechRecognizerTabPage.Controls.Add(this.speechRecognizerToolStrip2);
            this.speechRecognizerTabPage.Controls.Add(this.speechRecognizerToolStrip1);
            this.speechRecognizerTabPage.Location = new System.Drawing.Point(4, 22);
            this.speechRecognizerTabPage.Name = "speechRecognizerTabPage";
            this.speechRecognizerTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.speechRecognizerTabPage.Size = new System.Drawing.Size(889, 529);
            this.speechRecognizerTabPage.TabIndex = 0;
            this.speechRecognizerTabPage.Text = "Speech recognizer";
            this.speechRecognizerTabPage.UseVisualStyleBackColor = true;
            // 
            // speechRecognizerMainSplitContainer
            // 
            this.speechRecognizerMainSplitContainer.BackColor = System.Drawing.SystemColors.Control;
            this.speechRecognizerMainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.speechRecognizerMainSplitContainer.Location = new System.Drawing.Point(3, 103);
            this.speechRecognizerMainSplitContainer.Name = "speechRecognizerMainSplitContainer";
            // 
            // speechRecognizerMainSplitContainer.Panel1
            // 
            this.speechRecognizerMainSplitContainer.Panel1.Controls.Add(this.availableWordsListBox);
            // 
            // speechRecognizerMainSplitContainer.Panel2
            // 
            this.speechRecognizerMainSplitContainer.Panel2.Controls.Add(this.featurePlotPanel);
            this.speechRecognizerMainSplitContainer.Size = new System.Drawing.Size(883, 423);
            this.speechRecognizerMainSplitContainer.SplitterDistance = 204;
            this.speechRecognizerMainSplitContainer.SplitterWidth = 8;
            this.speechRecognizerMainSplitContainer.TabIndex = 7;
            // 
            // availableWordsListBox
            // 
            this.availableWordsListBox.BackColor = System.Drawing.Color.White;
            this.availableWordsListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.availableWordsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.availableWordsListBox.ForeColor = System.Drawing.Color.Black;
            this.availableWordsListBox.FormattingEnabled = true;
            this.availableWordsListBox.IntegralHeight = false;
            this.availableWordsListBox.Location = new System.Drawing.Point(0, 0);
            this.availableWordsListBox.Name = "availableWordsListBox";
            this.availableWordsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.availableWordsListBox.Size = new System.Drawing.Size(204, 423);
            this.availableWordsListBox.TabIndex = 0;
            this.availableWordsListBox.SelectedIndexChanged += new System.EventHandler(this.availableWordsListBox_SelectedIndexChanged);
            // 
            // featurePlotPanel
            // 
            this.featurePlotPanel.AxisColor = System.Drawing.Color.Black;
            this.featurePlotPanel.AxisMarkingsColor = System.Drawing.Color.Black;
            this.featurePlotPanel.AxisThickness = 1F;
            this.featurePlotPanel.BackColor = System.Drawing.Color.Black;
            this.featurePlotPanel.BottomFrameHeight = 45F;
            this.featurePlotPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.featurePlotPanel.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.featurePlotPanel.FrameBackColor = System.Drawing.Color.White;
            this.featurePlotPanel.GridColor = System.Drawing.Color.DarkGray;
            this.featurePlotPanel.GridLineThickness = 1F;
            this.featurePlotPanel.GridVisible = false;
            this.featurePlotPanel.HorizontalAxisLabel = "";
            this.featurePlotPanel.HorizontalAxisLabelFontSize = 8.25F;
            this.featurePlotPanel.HorizontalAxisLabelVisible = true;
            this.featurePlotPanel.HorizontalAxisMarkingsFormatString = "0.000";
            this.featurePlotPanel.HorizontalAxisMarkingsVisible = true;
            this.featurePlotPanel.HorizontalAxisVisible = false;
            this.featurePlotPanel.LeftFrameWidth = 70F;
            this.featurePlotPanel.Location = new System.Drawing.Point(0, 0);
            this.featurePlotPanel.MajorHorizontalTickMarkSpacing = 0D;
            this.featurePlotPanel.MajorHorizontalTickMarksVisible = true;
            this.featurePlotPanel.MajorVerticalTickMarkSpacing = 0D;
            this.featurePlotPanel.MajorVerticalTickMarksVisible = true;
            this.featurePlotPanel.Name = "featurePlotPanel";
            this.featurePlotPanel.PlotBackColor = System.Drawing.Color.White;
            this.featurePlotPanel.RelativeAutoRangeMargin = 0.01D;
            this.featurePlotPanel.RightFrameWidth = 45F;
            this.featurePlotPanel.Size = new System.Drawing.Size(671, 423);
            this.featurePlotPanel.TabIndex = 0;
            this.featurePlotPanel.TickMarkColor = System.Drawing.Color.Black;
            this.featurePlotPanel.TopFrameHeight = 45F;
            this.featurePlotPanel.VerticalAutoRange = false;
            this.featurePlotPanel.VerticalAxisLabel = "";
            this.featurePlotPanel.VerticalAxisLabelFontSize = 8.25F;
            this.featurePlotPanel.VerticalAxisLabelVisible = true;
            this.featurePlotPanel.VerticalAxisMarkingsFormatString = "0.000";
            this.featurePlotPanel.VerticalAxisMarkingsVisible = true;
            this.featurePlotPanel.VerticalAxisVisible = false;
            // 
            // speechRecognizerEditingToolStrip
            // 
            this.speechRecognizerEditingToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.featureLabel,
            this.featureComboBox,
            this.weightLabel,
            this.weightTextBox,
            this.toolStripSeparator1,
            this.toolStripLabel11,
            this.yMinTextBox,
            this.toolStripLabel12,
            this.yMaxTextBox});
            this.speechRecognizerEditingToolStrip.Location = new System.Drawing.Point(3, 78);
            this.speechRecognizerEditingToolStrip.Name = "speechRecognizerEditingToolStrip";
            this.speechRecognizerEditingToolStrip.Size = new System.Drawing.Size(883, 25);
            this.speechRecognizerEditingToolStrip.TabIndex = 6;
            this.speechRecognizerEditingToolStrip.Text = "toolStrip1";
            // 
            // featureLabel
            // 
            this.featureLabel.Name = "featureLabel";
            this.featureLabel.Size = new System.Drawing.Size(49, 22);
            this.featureLabel.Text = "Feature:";
            this.featureLabel.Visible = false;
            // 
            // featureComboBox
            // 
            this.featureComboBox.Name = "featureComboBox";
            this.featureComboBox.Size = new System.Drawing.Size(121, 25);
            this.featureComboBox.Visible = false;
            this.featureComboBox.SelectedIndexChanged += new System.EventHandler(this.featuresComboBox_SelectedIndexChanged);
            // 
            // weightLabel
            // 
            this.weightLabel.Name = "weightLabel";
            this.weightLabel.Size = new System.Drawing.Size(167, 22);
            this.weightLabel.Text = "Weight (in speech recognizer):";
            this.weightLabel.Visible = false;
            // 
            // weightTextBox
            // 
            this.weightTextBox.Name = "weightTextBox";
            this.weightTextBox.Size = new System.Drawing.Size(50, 25);
            this.weightTextBox.Visible = false;
            this.weightTextBox.TextChanged += new System.EventHandler(this.weightTextBox_TextChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel11
            // 
            this.toolStripLabel11.Name = "toolStripLabel11";
            this.toolStripLabel11.Size = new System.Drawing.Size(38, 22);
            this.toolStripLabel11.Text = "YMin:";
            // 
            // yMinTextBox
            // 
            this.yMinTextBox.Name = "yMinTextBox";
            this.yMinTextBox.Size = new System.Drawing.Size(40, 25);
            this.yMinTextBox.TextChanged += new System.EventHandler(this.yMinTextBox_TextChanged);
            // 
            // toolStripLabel12
            // 
            this.toolStripLabel12.Name = "toolStripLabel12";
            this.toolStripLabel12.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel12.Text = "YMax:";
            // 
            // yMaxTextBox
            // 
            this.yMaxTextBox.Name = "yMaxTextBox";
            this.yMaxTextBox.Size = new System.Drawing.Size(40, 25);
            this.yMaxTextBox.TextChanged += new System.EventHandler(this.yMaxTextBox_TextChanged);
            // 
            // speechRecognizerToolStrip3
            // 
            this.speechRecognizerToolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel7,
            this.autoCorrelationOrderTextBox,
            this.toolStripLabel8,
            this.lpcOrderTextBox,
            this.toolStripLabel9,
            this.cepstralOrderTextBox,
            this.toolStripLabel10,
            this.numberOfValuesPerFeatureTextBox});
            this.speechRecognizerToolStrip3.Location = new System.Drawing.Point(3, 53);
            this.speechRecognizerToolStrip3.Name = "speechRecognizerToolStrip3";
            this.speechRecognizerToolStrip3.Size = new System.Drawing.Size(883, 25);
            this.speechRecognizerToolStrip3.TabIndex = 2;
            this.speechRecognizerToolStrip3.Text = "toolStrip2";
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(124, 22);
            this.toolStripLabel7.Text = "Autocorrelation order:";
            // 
            // autoCorrelationOrderTextBox
            // 
            this.autoCorrelationOrderTextBox.Name = "autoCorrelationOrderTextBox";
            this.autoCorrelationOrderTextBox.Size = new System.Drawing.Size(40, 25);
            // 
            // toolStripLabel8
            // 
            this.toolStripLabel8.Name = "toolStripLabel8";
            this.toolStripLabel8.Size = new System.Drawing.Size(62, 22);
            this.toolStripLabel8.Text = "LPC order:";
            // 
            // lpcOrderTextBox
            // 
            this.lpcOrderTextBox.Name = "lpcOrderTextBox";
            this.lpcOrderTextBox.Size = new System.Drawing.Size(40, 25);
            // 
            // toolStripLabel9
            // 
            this.toolStripLabel9.Name = "toolStripLabel9";
            this.toolStripLabel9.Size = new System.Drawing.Size(84, 22);
            this.toolStripLabel9.Text = "Cepstral order:";
            // 
            // cepstralOrderTextBox
            // 
            this.cepstralOrderTextBox.Name = "cepstralOrderTextBox";
            this.cepstralOrderTextBox.Size = new System.Drawing.Size(40, 25);
            // 
            // toolStripLabel10
            // 
            this.toolStripLabel10.Name = "toolStripLabel10";
            this.toolStripLabel10.Size = new System.Drawing.Size(164, 22);
            this.toolStripLabel10.Text = "Number of values per feature:";
            // 
            // numberOfValuesPerFeatureTextBox
            // 
            this.numberOfValuesPerFeatureTextBox.Name = "numberOfValuesPerFeatureTextBox";
            this.numberOfValuesPerFeatureTextBox.Size = new System.Drawing.Size(100, 25);
            // 
            // speechRecognizerToolStrip2
            // 
            this.speechRecognizerToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel4,
            this.frameDurationTextBox,
            this.toolStripLabel5,
            this.frameShiftTextBox,
            this.toolStripLabel6,
            this.alphaTextBox});
            this.speechRecognizerToolStrip2.Location = new System.Drawing.Point(3, 28);
            this.speechRecognizerToolStrip2.Name = "speechRecognizerToolStrip2";
            this.speechRecognizerToolStrip2.Size = new System.Drawing.Size(883, 25);
            this.speechRecognizerToolStrip2.TabIndex = 1;
            this.speechRecognizerToolStrip2.Text = "toolStrip1";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(91, 22);
            this.toolStripLabel4.Text = "Frame duration:";
            // 
            // frameDurationTextBox
            // 
            this.frameDurationTextBox.Name = "frameDurationTextBox";
            this.frameDurationTextBox.Size = new System.Drawing.Size(60, 25);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(69, 22);
            this.toolStripLabel5.Text = "Frame shift:";
            // 
            // frameShiftTextBox
            // 
            this.frameShiftTextBox.Name = "frameShiftTextBox";
            this.frameShiftTextBox.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(96, 22);
            this.toolStripLabel6.Text = "Hamming alpha:";
            // 
            // alphaTextBox
            // 
            this.alphaTextBox.Name = "alphaTextBox";
            this.alphaTextBox.Size = new System.Drawing.Size(60, 25);
            // 
            // speechRecognizerToolStrip1
            // 
            this.speechRecognizerToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.soundExtractionMovingAverageLengthTextBox,
            this.toolStripLabel2,
            this.soundExtractionThresholdTextBox,
            this.toolStripLabel3,
            this.preEmphasisThresholdFrequencyTextBox});
            this.speechRecognizerToolStrip1.Location = new System.Drawing.Point(3, 3);
            this.speechRecognizerToolStrip1.Name = "speechRecognizerToolStrip1";
            this.speechRecognizerToolStrip1.Size = new System.Drawing.Size(883, 25);
            this.speechRecognizerToolStrip1.TabIndex = 0;
            this.speechRecognizerToolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(132, 22);
            this.toolStripLabel1.Text = "Moving average length:";
            // 
            // soundExtractionMovingAverageLengthTextBox
            // 
            this.soundExtractionMovingAverageLengthTextBox.Name = "soundExtractionMovingAverageLengthTextBox";
            this.soundExtractionMovingAverageLengthTextBox.Size = new System.Drawing.Size(40, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(115, 22);
            this.toolStripLabel2.Text = "Extraction threshold:";
            // 
            // soundExtractionThresholdTextBox
            // 
            this.soundExtractionThresholdTextBox.Name = "soundExtractionThresholdTextBox";
            this.soundExtractionThresholdTextBox.Size = new System.Drawing.Size(60, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(191, 22);
            this.toolStripLabel3.Text = "Pre-emphasis threshold frequency:";
            // 
            // preEmphasisThresholdFrequencyTextBox
            // 
            this.preEmphasisThresholdFrequencyTextBox.Name = "preEmphasisThresholdFrequencyTextBox";
            this.preEmphasisThresholdFrequencyTextBox.Size = new System.Drawing.Size(60, 25);
            // 
            // usageTabPage
            // 
            this.usageTabPage.Controls.Add(this.usageMainSplitContainer);
            this.usageTabPage.Location = new System.Drawing.Point(4, 22);
            this.usageTabPage.Name = "usageTabPage";
            this.usageTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.usageTabPage.Size = new System.Drawing.Size(889, 529);
            this.usageTabPage.TabIndex = 1;
            this.usageTabPage.Text = "Usage";
            this.usageTabPage.UseVisualStyleBackColor = true;
            // 
            // usageMainSplitContainer
            // 
            this.usageMainSplitContainer.BackColor = System.Drawing.SystemColors.Control;
            this.usageMainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usageMainSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.usageMainSplitContainer.Name = "usageMainSplitContainer";
            this.usageMainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // usageMainSplitContainer.Panel1
            // 
            this.usageMainSplitContainer.Panel1.Controls.Add(this.soundVisualizer);
            this.usageMainSplitContainer.Panel1.Controls.Add(this.toolStrip1);
            // 
            // usageMainSplitContainer.Panel2
            // 
            this.usageMainSplitContainer.Panel2.Controls.Add(this.usageSecondarySplitContainer);
            this.usageMainSplitContainer.Panel2.Controls.Add(this.toolStrip2);
            this.usageMainSplitContainer.Size = new System.Drawing.Size(883, 523);
            this.usageMainSplitContainer.SplitterDistance = 261;
            this.usageMainSplitContainer.SplitterWidth = 8;
            this.usageMainSplitContainer.TabIndex = 2;
            // 
            // soundVisualizer
            // 
            this.soundVisualizer.BackColor = System.Drawing.Color.Black;
            this.soundVisualizer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.soundVisualizer.Location = new System.Drawing.Point(0, 25);
            this.soundVisualizer.MarkerList = null;
            this.soundVisualizer.Name = "soundVisualizer";
            this.soundVisualizer.Size = new System.Drawing.Size(883, 236);
            this.soundVisualizer.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recordToolStripButton,
            this.playSoundButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(883, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // recordToolStripButton
            // 
            this.recordToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.recordToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("recordToolStripButton.Image")));
            this.recordToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.recordToolStripButton.Name = "recordToolStripButton";
            this.recordToolStripButton.Size = new System.Drawing.Size(89, 22);
            this.recordToolStripButton.Text = "Start recording";
            this.recordToolStripButton.Click += new System.EventHandler(this.recordToolStripButton_Click);
            // 
            // playSoundButton
            // 
            this.playSoundButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.playSoundButton.Enabled = false;
            this.playSoundButton.Image = ((System.Drawing.Image)(resources.GetObject("playSoundButton.Image")));
            this.playSoundButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.playSoundButton.Name = "playSoundButton";
            this.playSoundButton.Size = new System.Drawing.Size(33, 22);
            this.playSoundButton.Text = "Play";
            this.playSoundButton.Click += new System.EventHandler(this.playSoundButton_Click);
            // 
            // usageSecondarySplitContainer
            // 
            this.usageSecondarySplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usageSecondarySplitContainer.Location = new System.Drawing.Point(0, 25);
            this.usageSecondarySplitContainer.Name = "usageSecondarySplitContainer";
            // 
            // usageSecondarySplitContainer.Panel1
            // 
            this.usageSecondarySplitContainer.Panel1.Controls.Add(this.deviationListBox);
            // 
            // usageSecondarySplitContainer.Panel2
            // 
            this.usageSecondarySplitContainer.Panel2.Controls.Add(this.featureComparisonPlotPanel);
            this.usageSecondarySplitContainer.Panel2.Controls.Add(this.featureComparisonComboBox);
            this.usageSecondarySplitContainer.Size = new System.Drawing.Size(883, 229);
            this.usageSecondarySplitContainer.SplitterDistance = 214;
            this.usageSecondarySplitContainer.TabIndex = 4;
            // 
            // deviationListBox
            // 
            this.deviationListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.deviationListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deviationListBox.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deviationListBox.FormattingEnabled = true;
            this.deviationListBox.IntegralHeight = false;
            this.deviationListBox.ItemHeight = 11;
            this.deviationListBox.Location = new System.Drawing.Point(0, 0);
            this.deviationListBox.Name = "deviationListBox";
            this.deviationListBox.Size = new System.Drawing.Size(214, 229);
            this.deviationListBox.TabIndex = 0;
            this.deviationListBox.SelectedIndexChanged += new System.EventHandler(this.deviationListBox_SelectedIndexChanged);
            // 
            // featureComparisonPlotPanel
            // 
            this.featureComparisonPlotPanel.AxisColor = System.Drawing.Color.Black;
            this.featureComparisonPlotPanel.AxisMarkingsColor = System.Drawing.Color.Black;
            this.featureComparisonPlotPanel.AxisThickness = 1F;
            this.featureComparisonPlotPanel.BackColor = System.Drawing.SystemColors.Control;
            this.featureComparisonPlotPanel.BottomFrameHeight = 45F;
            this.featureComparisonPlotPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.featureComparisonPlotPanel.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.featureComparisonPlotPanel.FrameBackColor = System.Drawing.Color.White;
            this.featureComparisonPlotPanel.GridColor = System.Drawing.Color.DarkGray;
            this.featureComparisonPlotPanel.GridLineThickness = 1F;
            this.featureComparisonPlotPanel.GridVisible = false;
            this.featureComparisonPlotPanel.HorizontalAxisLabel = "";
            this.featureComparisonPlotPanel.HorizontalAxisLabelFontSize = 8.25F;
            this.featureComparisonPlotPanel.HorizontalAxisLabelVisible = true;
            this.featureComparisonPlotPanel.HorizontalAxisMarkingsFormatString = "0.000";
            this.featureComparisonPlotPanel.HorizontalAxisMarkingsVisible = true;
            this.featureComparisonPlotPanel.HorizontalAxisVisible = false;
            this.featureComparisonPlotPanel.LeftFrameWidth = 70F;
            this.featureComparisonPlotPanel.Location = new System.Drawing.Point(0, 21);
            this.featureComparisonPlotPanel.MajorHorizontalTickMarkSpacing = 0D;
            this.featureComparisonPlotPanel.MajorHorizontalTickMarksVisible = true;
            this.featureComparisonPlotPanel.MajorVerticalTickMarkSpacing = 0D;
            this.featureComparisonPlotPanel.MajorVerticalTickMarksVisible = true;
            this.featureComparisonPlotPanel.Name = "featureComparisonPlotPanel";
            this.featureComparisonPlotPanel.PlotBackColor = System.Drawing.Color.White;
            this.featureComparisonPlotPanel.RelativeAutoRangeMargin = 0.01D;
            this.featureComparisonPlotPanel.RightFrameWidth = 45F;
            this.featureComparisonPlotPanel.Size = new System.Drawing.Size(665, 208);
            this.featureComparisonPlotPanel.TabIndex = 3;
            this.featureComparisonPlotPanel.TickMarkColor = System.Drawing.Color.Black;
            this.featureComparisonPlotPanel.TopFrameHeight = 45F;
            this.featureComparisonPlotPanel.VerticalAutoRange = false;
            this.featureComparisonPlotPanel.VerticalAxisLabel = "";
            this.featureComparisonPlotPanel.VerticalAxisLabelFontSize = 8.25F;
            this.featureComparisonPlotPanel.VerticalAxisLabelVisible = true;
            this.featureComparisonPlotPanel.VerticalAxisMarkingsFormatString = "0.000";
            this.featureComparisonPlotPanel.VerticalAxisMarkingsVisible = true;
            this.featureComparisonPlotPanel.VerticalAxisVisible = false;
            // 
            // featureComparisonComboBox
            // 
            this.featureComparisonComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.featureComparisonComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.featureComparisonComboBox.FormattingEnabled = true;
            this.featureComparisonComboBox.Location = new System.Drawing.Point(0, 0);
            this.featureComparisonComboBox.Name = "featureComparisonComboBox";
            this.featureComparisonComboBox.Size = new System.Drawing.Size(665, 21);
            this.featureComparisonComboBox.TabIndex = 2;
            this.featureComparisonComboBox.SelectedIndexChanged += new System.EventHandler(this.featureComparisonComboBox_SelectedIndexChanged);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recognizeButton,
            this.toolStripSeparator4,
            this.recognitionResultTextBox,
            this.toolStripLabel13,
            this.recognitionThresholdTextBox});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(883, 25);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // recognizeButton
            // 
            this.recognizeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.recognizeButton.Image = ((System.Drawing.Image)(resources.GetObject("recognizeButton.Image")));
            this.recognizeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.recognizeButton.Name = "recognizeButton";
            this.recognizeButton.Size = new System.Drawing.Size(65, 22);
            this.recognizeButton.Text = "Recognize";
            this.recognizeButton.Click += new System.EventHandler(this.recognizeButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // recognitionResultTextBox
            // 
            this.recognitionResultTextBox.Name = "recognitionResultTextBox";
            this.recognitionResultTextBox.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripLabel13
            // 
            this.toolStripLabel13.Name = "toolStripLabel13";
            this.toolStripLabel13.Size = new System.Drawing.Size(127, 22);
            this.toolStripLabel13.Text = "Recognition threshold:";
            // 
            // recognitionThresholdTextBox
            // 
            this.recognitionThresholdTextBox.Name = "recognitionThresholdTextBox";
            this.recognitionThresholdTextBox.Size = new System.Drawing.Size(50, 25);
            this.recognitionThresholdTextBox.TextChanged += new System.EventHandler(this.recognitionThresholdTextBox_TextChanged);
            // 
            // IWRMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 579);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "IWRMainForm";
            this.Text = "Isolated word recognizer, v1.0 (c) Mattias Wahde 2016, mattias.wahde@chalmers.se";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.speechRecognizerTabPage.ResumeLayout(false);
            this.speechRecognizerTabPage.PerformLayout();
            this.speechRecognizerMainSplitContainer.Panel1.ResumeLayout(false);
            this.speechRecognizerMainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.speechRecognizerMainSplitContainer)).EndInit();
            this.speechRecognizerMainSplitContainer.ResumeLayout(false);
            this.speechRecognizerEditingToolStrip.ResumeLayout(false);
            this.speechRecognizerEditingToolStrip.PerformLayout();
            this.speechRecognizerToolStrip3.ResumeLayout(false);
            this.speechRecognizerToolStrip3.PerformLayout();
            this.speechRecognizerToolStrip2.ResumeLayout(false);
            this.speechRecognizerToolStrip2.PerformLayout();
            this.speechRecognizerToolStrip1.ResumeLayout(false);
            this.speechRecognizerToolStrip1.PerformLayout();
            this.usageTabPage.ResumeLayout(false);
            this.usageMainSplitContainer.Panel1.ResumeLayout(false);
            this.usageMainSplitContainer.Panel1.PerformLayout();
            this.usageMainSplitContainer.Panel2.ResumeLayout(false);
            this.usageMainSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usageMainSplitContainer)).EndInit();
            this.usageMainSplitContainer.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.usageSecondarySplitContainer.Panel1.ResumeLayout(false);
            this.usageSecondarySplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.usageSecondarySplitContainer)).EndInit();
            this.usageSecondarySplitContainer.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speechRecognizerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage speechRecognizerTabPage;
        private System.Windows.Forms.TabPage usageTabPage;
        private System.Windows.Forms.ToolStrip speechRecognizerToolStrip1;
        private System.Windows.Forms.ToolStrip speechRecognizerToolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox soundExtractionMovingAverageLengthTextBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox soundExtractionThresholdTextBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox preEmphasisThresholdFrequencyTextBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox frameDurationTextBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripTextBox frameShiftTextBox;
        private System.Windows.Forms.ToolStrip speechRecognizerToolStrip3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel7;
        private System.Windows.Forms.ToolStripTextBox autoCorrelationOrderTextBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel8;
        private System.Windows.Forms.ToolStripTextBox lpcOrderTextBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel9;
        private System.Windows.Forms.ToolStripTextBox cepstralOrderTextBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel10;
        private System.Windows.Forms.ToolStripTextBox numberOfValuesPerFeatureTextBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripTextBox alphaTextBox;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parametersVisibleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addWordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autorangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem errorBarsVisibleToolStripMenuItem;
        private System.Windows.Forms.SplitContainer speechRecognizerMainSplitContainer;
        private System.Windows.Forms.ListBox availableWordsListBox;
        private PlotLibrary.Plot2DPanel featurePlotPanel;
        private System.Windows.Forms.ToolStrip speechRecognizerEditingToolStrip;
        private System.Windows.Forms.ToolStripLabel featureLabel;
        private System.Windows.Forms.ToolStripComboBox featureComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel11;
        private System.Windows.Forms.ToolStripTextBox yMinTextBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel12;
        private System.Windows.Forms.ToolStripTextBox yMaxTextBox;
        private System.Windows.Forms.ToolStripLabel weightLabel;
        private System.Windows.Forms.ToolStripTextBox weightTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem testSoundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveSoundToolStripMenuItem;
        private System.Windows.Forms.SplitContainer usageMainSplitContainer;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton recordToolStripButton;
        private System.Windows.Forms.SplitContainer usageSecondarySplitContainer;
        private System.Windows.Forms.ListBox deviationListBox;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton recognizeButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripTextBox recognitionResultTextBox;
        private PlotLibrary.Plot2DPanel featureComparisonPlotPanel;
        private System.Windows.Forms.ComboBox featureComparisonComboBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel13;
        private System.Windows.Forms.ToolStripTextBox recognitionThresholdTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
      //  private AudioLibrary.Visualization.WAVRecorderVisualizer recorderVisualizer;
        private System.Windows.Forms.ToolStripButton playSoundButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem setMaximumNonclippingVolumeToolStripMenuItem;
        private AudioLibrary.Visualization.SoundVisualizer soundVisualizer;
    }
}

