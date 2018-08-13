namespace VoiceModificationApplication
{
    partial class VoiceModificationParameterSettingsForm
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
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.speechTypeEstimatorTabPage = new System.Windows.Forms.TabPage();
            this.speechTypeEstimatorPropertyPanel = new CustomUserControlsLibrary.PropertyPanels.PropertyPanel();
            this.pitchPeriodEstimatorTabPage = new System.Windows.Forms.TabPage();
            this.pitchPeriodEstimatorPropertyPanel = new CustomUserControlsLibrary.PropertyPanels.PropertyPanel();
            this.pitchMarkEstimatorTabPage = new System.Windows.Forms.TabPage();
            this.pitchMarkEstimatorPropertyPanel = new CustomUserControlsLibrary.PropertyPanels.PropertyPanel();
            this.mainTabControl.SuspendLayout();
            this.speechTypeEstimatorTabPage.SuspendLayout();
            this.pitchPeriodEstimatorTabPage.SuspendLayout();
            this.pitchMarkEstimatorTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.speechTypeEstimatorTabPage);
            this.mainTabControl.Controls.Add(this.pitchPeriodEstimatorTabPage);
            this.mainTabControl.Controls.Add(this.pitchMarkEstimatorTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(679, 367);
            this.mainTabControl.TabIndex = 0;
            // 
            // speechTypeEstimatorTabPage
            // 
            this.speechTypeEstimatorTabPage.Controls.Add(this.speechTypeEstimatorPropertyPanel);
            this.speechTypeEstimatorTabPage.Location = new System.Drawing.Point(4, 22);
            this.speechTypeEstimatorTabPage.Name = "speechTypeEstimatorTabPage";
            this.speechTypeEstimatorTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.speechTypeEstimatorTabPage.Size = new System.Drawing.Size(671, 341);
            this.speechTypeEstimatorTabPage.TabIndex = 1;
            this.speechTypeEstimatorTabPage.Text = "Speech type estimator";
            this.speechTypeEstimatorTabPage.UseVisualStyleBackColor = true;
            // 
            // speechTypeEstimatorPropertyPanel
            // 
            this.speechTypeEstimatorPropertyPanel.BackColor = System.Drawing.Color.DimGray;
            this.speechTypeEstimatorPropertyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.speechTypeEstimatorPropertyPanel.GenericListSizeFixed = false;
            this.speechTypeEstimatorPropertyPanel.Location = new System.Drawing.Point(3, 3);
            this.speechTypeEstimatorPropertyPanel.Name = "speechTypeEstimatorPropertyPanel";
            this.speechTypeEstimatorPropertyPanel.Size = new System.Drawing.Size(665, 335);
            this.speechTypeEstimatorPropertyPanel.TabIndex = 0;
            // 
            // pitchPeriodEstimatorTabPage
            // 
            this.pitchPeriodEstimatorTabPage.Controls.Add(this.pitchPeriodEstimatorPropertyPanel);
            this.pitchPeriodEstimatorTabPage.Location = new System.Drawing.Point(4, 22);
            this.pitchPeriodEstimatorTabPage.Name = "pitchPeriodEstimatorTabPage";
            this.pitchPeriodEstimatorTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.pitchPeriodEstimatorTabPage.Size = new System.Drawing.Size(671, 341);
            this.pitchPeriodEstimatorTabPage.TabIndex = 2;
            this.pitchPeriodEstimatorTabPage.Text = "Pitch period estimator";
            this.pitchPeriodEstimatorTabPage.UseVisualStyleBackColor = true;
            // 
            // pitchPeriodEstimatorPropertyPanel
            // 
            this.pitchPeriodEstimatorPropertyPanel.BackColor = System.Drawing.Color.DimGray;
            this.pitchPeriodEstimatorPropertyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pitchPeriodEstimatorPropertyPanel.GenericListSizeFixed = false;
            this.pitchPeriodEstimatorPropertyPanel.Location = new System.Drawing.Point(3, 3);
            this.pitchPeriodEstimatorPropertyPanel.Name = "pitchPeriodEstimatorPropertyPanel";
            this.pitchPeriodEstimatorPropertyPanel.Size = new System.Drawing.Size(665, 335);
            this.pitchPeriodEstimatorPropertyPanel.TabIndex = 0;
            // 
            // pitchMarkEstimatorTabPage
            // 
            this.pitchMarkEstimatorTabPage.Controls.Add(this.pitchMarkEstimatorPropertyPanel);
            this.pitchMarkEstimatorTabPage.Location = new System.Drawing.Point(4, 22);
            this.pitchMarkEstimatorTabPage.Name = "pitchMarkEstimatorTabPage";
            this.pitchMarkEstimatorTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.pitchMarkEstimatorTabPage.Size = new System.Drawing.Size(671, 341);
            this.pitchMarkEstimatorTabPage.TabIndex = 3;
            this.pitchMarkEstimatorTabPage.Text = "Pitch mark estimator";
            this.pitchMarkEstimatorTabPage.UseVisualStyleBackColor = true;
            // 
            // pitchMarkEstimatorPropertyPanel
            // 
            this.pitchMarkEstimatorPropertyPanel.BackColor = System.Drawing.Color.DimGray;
            this.pitchMarkEstimatorPropertyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pitchMarkEstimatorPropertyPanel.GenericListSizeFixed = false;
            this.pitchMarkEstimatorPropertyPanel.Location = new System.Drawing.Point(3, 3);
            this.pitchMarkEstimatorPropertyPanel.Name = "pitchMarkEstimatorPropertyPanel";
            this.pitchMarkEstimatorPropertyPanel.Size = new System.Drawing.Size(665, 335);
            this.pitchMarkEstimatorPropertyPanel.TabIndex = 0;
            // 
            // VoiceModificationParameterSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 367);
            this.Controls.Add(this.mainTabControl);
            this.Name = "VoiceModificationParameterSettingsForm";
            this.Text = "VoiceModificationParameterSettingsForm";
            this.mainTabControl.ResumeLayout(false);
            this.speechTypeEstimatorTabPage.ResumeLayout(false);
            this.pitchPeriodEstimatorTabPage.ResumeLayout(false);
            this.pitchMarkEstimatorTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage speechTypeEstimatorTabPage;
        private CustomUserControlsLibrary.PropertyPanels.PropertyPanel speechTypeEstimatorPropertyPanel;
        private System.Windows.Forms.TabPage pitchPeriodEstimatorTabPage;
        private CustomUserControlsLibrary.PropertyPanels.PropertyPanel pitchPeriodEstimatorPropertyPanel;
        private System.Windows.Forms.TabPage pitchMarkEstimatorTabPage;
        private CustomUserControlsLibrary.PropertyPanels.PropertyPanel pitchMarkEstimatorPropertyPanel;
    }
}