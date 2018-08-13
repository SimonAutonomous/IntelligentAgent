namespace ImageProcessingApplication
{
    partial class ImageProcessingMainForm
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
            this.loadImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageProcessingSplitContainer = new System.Windows.Forms.SplitContainer();
            this.imageProcessingSecondarySplitContainer = new System.Windows.Forms.SplitContainer();
            this.imageSequenceListBox = new System.Windows.Forms.ListBox();
            this.sobelEdgeDetectButton = new System.Windows.Forms.Button();
            this.gaussianBlurButton = new System.Windows.Forms.Button();
            this.boxBlurButton = new System.Windows.Forms.Button();
            this.sharpeningFactorTextBox = new System.Windows.Forms.TextBox();
            this.sharpenButton = new System.Windows.Forms.Button();
            this.contrastAlphaTextBox = new System.Windows.Forms.TextBox();
            this.changeContrastButton = new System.Windows.Forms.Button();
            this.relativeBrightnessTextBox = new System.Windows.Forms.TextBox();
            this.changeBrightnessButton = new System.Windows.Forms.Button();
            this.binarizationThresholdTextBox = new System.Windows.Forms.TextBox();
            this.binarizeButton = new System.Windows.Forms.Button();
            this.convertToStandardGrayscaleButton = new System.Windows.Forms.Button();
            this.imageProcessingPlot = new ImageProcessingLibrary.Visualization.ImagePlot();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageProcessingSplitContainer)).BeginInit();
            this.imageProcessingSplitContainer.Panel1.SuspendLayout();
            this.imageProcessingSplitContainer.Panel2.SuspendLayout();
            this.imageProcessingSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageProcessingSecondarySplitContainer)).BeginInit();
            this.imageProcessingSecondarySplitContainer.Panel1.SuspendLayout();
            this.imageProcessingSecondarySplitContainer.Panel2.SuspendLayout();
            this.imageProcessingSecondarySplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1002, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadImageToolStripMenuItem,
            this.saveImageToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadImageToolStripMenuItem
            // 
            this.loadImageToolStripMenuItem.Name = "loadImageToolStripMenuItem";
            this.loadImageToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.loadImageToolStripMenuItem.Text = "Load image";
            this.loadImageToolStripMenuItem.Click += new System.EventHandler(this.loadImageToolStripMenuItem_Click);
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Enabled = false;
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveImageToolStripMenuItem.Text = "Save image";
            this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // imageProcessingSplitContainer
            // 
            this.imageProcessingSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageProcessingSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.imageProcessingSplitContainer.IsSplitterFixed = true;
            this.imageProcessingSplitContainer.Location = new System.Drawing.Point(0, 24);
            this.imageProcessingSplitContainer.Name = "imageProcessingSplitContainer";
            // 
            // imageProcessingSplitContainer.Panel1
            // 
            this.imageProcessingSplitContainer.Panel1.Controls.Add(this.imageProcessingSecondarySplitContainer);
            // 
            // imageProcessingSplitContainer.Panel2
            // 
            this.imageProcessingSplitContainer.Panel2.Controls.Add(this.imageProcessingPlot);
            this.imageProcessingSplitContainer.Size = new System.Drawing.Size(1002, 543);
            this.imageProcessingSplitContainer.SplitterDistance = 198;
            this.imageProcessingSplitContainer.TabIndex = 2;
            // 
            // imageProcessingSecondarySplitContainer
            // 
            this.imageProcessingSecondarySplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageProcessingSecondarySplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.imageProcessingSecondarySplitContainer.IsSplitterFixed = true;
            this.imageProcessingSecondarySplitContainer.Location = new System.Drawing.Point(0, 0);
            this.imageProcessingSecondarySplitContainer.Name = "imageProcessingSecondarySplitContainer";
            this.imageProcessingSecondarySplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // imageProcessingSecondarySplitContainer.Panel1
            // 
            this.imageProcessingSecondarySplitContainer.Panel1.Controls.Add(this.imageSequenceListBox);
            // 
            // imageProcessingSecondarySplitContainer.Panel2
            // 
            this.imageProcessingSecondarySplitContainer.Panel2.BackColor = System.Drawing.Color.DimGray;
            this.imageProcessingSecondarySplitContainer.Panel2.Controls.Add(this.sobelEdgeDetectButton);
            this.imageProcessingSecondarySplitContainer.Panel2.Controls.Add(this.gaussianBlurButton);
            this.imageProcessingSecondarySplitContainer.Panel2.Controls.Add(this.boxBlurButton);
            this.imageProcessingSecondarySplitContainer.Panel2.Controls.Add(this.sharpeningFactorTextBox);
            this.imageProcessingSecondarySplitContainer.Panel2.Controls.Add(this.sharpenButton);
            this.imageProcessingSecondarySplitContainer.Panel2.Controls.Add(this.contrastAlphaTextBox);
            this.imageProcessingSecondarySplitContainer.Panel2.Controls.Add(this.changeContrastButton);
            this.imageProcessingSecondarySplitContainer.Panel2.Controls.Add(this.relativeBrightnessTextBox);
            this.imageProcessingSecondarySplitContainer.Panel2.Controls.Add(this.changeBrightnessButton);
            this.imageProcessingSecondarySplitContainer.Panel2.Controls.Add(this.binarizationThresholdTextBox);
            this.imageProcessingSecondarySplitContainer.Panel2.Controls.Add(this.binarizeButton);
            this.imageProcessingSecondarySplitContainer.Panel2.Controls.Add(this.convertToStandardGrayscaleButton);
            this.imageProcessingSecondarySplitContainer.Size = new System.Drawing.Size(198, 543);
            this.imageProcessingSecondarySplitContainer.SplitterDistance = 298;
            this.imageProcessingSecondarySplitContainer.TabIndex = 1;
            // 
            // imageSequenceListBox
            // 
            this.imageSequenceListBox.BackColor = System.Drawing.Color.DimGray;
            this.imageSequenceListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageSequenceListBox.ForeColor = System.Drawing.Color.Lime;
            this.imageSequenceListBox.FormattingEnabled = true;
            this.imageSequenceListBox.IntegralHeight = false;
            this.imageSequenceListBox.Location = new System.Drawing.Point(0, 0);
            this.imageSequenceListBox.Name = "imageSequenceListBox";
            this.imageSequenceListBox.Size = new System.Drawing.Size(198, 298);
            this.imageSequenceListBox.TabIndex = 0;
            this.imageSequenceListBox.SelectedIndexChanged += new System.EventHandler(this.imageSequenceListBox_SelectedIndexChanged);
            // 
            // sobelEdgeDetectButton
            // 
            this.sobelEdgeDetectButton.Location = new System.Drawing.Point(5, 207);
            this.sobelEdgeDetectButton.Name = "sobelEdgeDetectButton";
            this.sobelEdgeDetectButton.Size = new System.Drawing.Size(134, 23);
            this.sobelEdgeDetectButton.TabIndex = 17;
            this.sobelEdgeDetectButton.Text = "Edge detect (Sobel)";
            this.sobelEdgeDetectButton.UseVisualStyleBackColor = true;
            this.sobelEdgeDetectButton.Click += new System.EventHandler(this.sobelEdgeDetectButton_Click);
            // 
            // gaussianBlurButton
            // 
            this.gaussianBlurButton.Location = new System.Drawing.Point(5, 178);
            this.gaussianBlurButton.Name = "gaussianBlurButton";
            this.gaussianBlurButton.Size = new System.Drawing.Size(134, 23);
            this.gaussianBlurButton.TabIndex = 16;
            this.gaussianBlurButton.Text = "Gaussian blur (3x3)";
            this.gaussianBlurButton.UseVisualStyleBackColor = true;
            this.gaussianBlurButton.Click += new System.EventHandler(this.gaussianBlurButton_Click);
            // 
            // boxBlurButton
            // 
            this.boxBlurButton.Location = new System.Drawing.Point(5, 149);
            this.boxBlurButton.Name = "boxBlurButton";
            this.boxBlurButton.Size = new System.Drawing.Size(134, 23);
            this.boxBlurButton.TabIndex = 15;
            this.boxBlurButton.Text = "Box blur (3x3)";
            this.boxBlurButton.UseVisualStyleBackColor = true;
            this.boxBlurButton.Click += new System.EventHandler(this.boxBlurButton_Click);
            // 
            // sharpeningFactorTextBox
            // 
            this.sharpeningFactorTextBox.Location = new System.Drawing.Point(145, 122);
            this.sharpeningFactorTextBox.Name = "sharpeningFactorTextBox";
            this.sharpeningFactorTextBox.Size = new System.Drawing.Size(41, 20);
            this.sharpeningFactorTextBox.TabIndex = 14;
            this.sharpeningFactorTextBox.Text = "0.25";
            this.sharpeningFactorTextBox.TextChanged += new System.EventHandler(this.sharpeningFactorTextBox_TextChanged);
            // 
            // sharpenButton
            // 
            this.sharpenButton.Location = new System.Drawing.Point(5, 120);
            this.sharpenButton.Name = "sharpenButton";
            this.sharpenButton.Size = new System.Drawing.Size(134, 23);
            this.sharpenButton.TabIndex = 13;
            this.sharpenButton.Text = "Sharpen (3x3)";
            this.sharpenButton.UseVisualStyleBackColor = true;
            this.sharpenButton.Click += new System.EventHandler(this.sharpenButton_Click);
            // 
            // contrastAlphaTextBox
            // 
            this.contrastAlphaTextBox.Location = new System.Drawing.Point(145, 6);
            this.contrastAlphaTextBox.Name = "contrastAlphaTextBox";
            this.contrastAlphaTextBox.Size = new System.Drawing.Size(41, 20);
            this.contrastAlphaTextBox.TabIndex = 8;
            this.contrastAlphaTextBox.Text = "1.50";
            this.contrastAlphaTextBox.TextChanged += new System.EventHandler(this.contrastAlphaTextBox_TextChanged);
            // 
            // changeContrastButton
            // 
            this.changeContrastButton.Location = new System.Drawing.Point(5, 4);
            this.changeContrastButton.Name = "changeContrastButton";
            this.changeContrastButton.Size = new System.Drawing.Size(134, 23);
            this.changeContrastButton.TabIndex = 7;
            this.changeContrastButton.Text = "Change contrast";
            this.changeContrastButton.UseVisualStyleBackColor = true;
            this.changeContrastButton.Click += new System.EventHandler(this.changeContrastButton_Click);
            // 
            // relativeBrightnessTextBox
            // 
            this.relativeBrightnessTextBox.Location = new System.Drawing.Point(145, 35);
            this.relativeBrightnessTextBox.Name = "relativeBrightnessTextBox";
            this.relativeBrightnessTextBox.Size = new System.Drawing.Size(41, 20);
            this.relativeBrightnessTextBox.TabIndex = 6;
            this.relativeBrightnessTextBox.Text = "0.90";
            // 
            // changeBrightnessButton
            // 
            this.changeBrightnessButton.Location = new System.Drawing.Point(5, 33);
            this.changeBrightnessButton.Name = "changeBrightnessButton";
            this.changeBrightnessButton.Size = new System.Drawing.Size(134, 23);
            this.changeBrightnessButton.TabIndex = 5;
            this.changeBrightnessButton.Text = "Change brightness";
            this.changeBrightnessButton.UseVisualStyleBackColor = true;
            this.changeBrightnessButton.Click += new System.EventHandler(this.changeBrightnessButton_Click);
            // 
            // binarizationThresholdTextBox
            // 
            this.binarizationThresholdTextBox.Location = new System.Drawing.Point(145, 93);
            this.binarizationThresholdTextBox.Name = "binarizationThresholdTextBox";
            this.binarizationThresholdTextBox.Size = new System.Drawing.Size(41, 20);
            this.binarizationThresholdTextBox.TabIndex = 3;
            this.binarizationThresholdTextBox.Text = "127";
            // 
            // binarizeButton
            // 
            this.binarizeButton.Location = new System.Drawing.Point(5, 91);
            this.binarizeButton.Name = "binarizeButton";
            this.binarizeButton.Size = new System.Drawing.Size(134, 23);
            this.binarizeButton.TabIndex = 2;
            this.binarizeButton.Text = "Binarize";
            this.binarizeButton.UseVisualStyleBackColor = true;
            this.binarizeButton.Click += new System.EventHandler(this.binarizeButton_Click);
            // 
            // convertToStandardGrayscaleButton
            // 
            this.convertToStandardGrayscaleButton.Location = new System.Drawing.Point(5, 62);
            this.convertToStandardGrayscaleButton.Name = "convertToStandardGrayscaleButton";
            this.convertToStandardGrayscaleButton.Size = new System.Drawing.Size(183, 23);
            this.convertToStandardGrayscaleButton.TabIndex = 1;
            this.convertToStandardGrayscaleButton.Text = "Convert to standard grayscale";
            this.convertToStandardGrayscaleButton.UseVisualStyleBackColor = true;
            this.convertToStandardGrayscaleButton.Click += new System.EventHandler(this.convertToStandardGrayscaleButton_Click);
            // 
            // imageProcessingPlot
            // 
            this.imageProcessingPlot.BackColor = System.Drawing.Color.Black;
            this.imageProcessingPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageProcessingPlot.Location = new System.Drawing.Point(0, 0);
            this.imageProcessingPlot.Name = "imageProcessingPlot";
            this.imageProcessingPlot.Size = new System.Drawing.Size(800, 543);
            this.imageProcessingPlot.TabIndex = 0;
            // 
            // ImageProcessingMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 567);
            this.Controls.Add(this.imageProcessingSplitContainer);
            this.Controls.Add(this.menuStrip1);
            this.Name = "ImageProcessingMainForm";
            this.Text = "Image processing application (c) 2017 Mattias Wahde, mattias.wahde@chalmers.se";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.imageProcessingSplitContainer.Panel1.ResumeLayout(false);
            this.imageProcessingSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageProcessingSplitContainer)).EndInit();
            this.imageProcessingSplitContainer.ResumeLayout(false);
            this.imageProcessingSecondarySplitContainer.Panel1.ResumeLayout(false);
            this.imageProcessingSecondarySplitContainer.Panel2.ResumeLayout(false);
            this.imageProcessingSecondarySplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageProcessingSecondarySplitContainer)).EndInit();
            this.imageProcessingSecondarySplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer imageProcessingSplitContainer;
        private System.Windows.Forms.SplitContainer imageProcessingSecondarySplitContainer;
        private System.Windows.Forms.ListBox imageSequenceListBox;
        private System.Windows.Forms.Button sobelEdgeDetectButton;
        private System.Windows.Forms.Button gaussianBlurButton;
        private System.Windows.Forms.Button boxBlurButton;
        private System.Windows.Forms.TextBox sharpeningFactorTextBox;
        private System.Windows.Forms.Button sharpenButton;
        private System.Windows.Forms.TextBox contrastAlphaTextBox;
        private System.Windows.Forms.Button changeContrastButton;
        private System.Windows.Forms.TextBox relativeBrightnessTextBox;
        private System.Windows.Forms.Button changeBrightnessButton;
        private System.Windows.Forms.TextBox binarizationThresholdTextBox;
        private System.Windows.Forms.Button binarizeButton;
        private System.Windows.Forms.Button convertToStandardGrayscaleButton;
        private ImageProcessingLibrary.Visualization.ImagePlot imageProcessingPlot;
    }
}

