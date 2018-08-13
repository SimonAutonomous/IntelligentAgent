namespace ImageProcessingLibrary.Cameras
{
    partial class CameraViewControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cameraPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.cameraPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // cameraPictureBox
            // 
            this.cameraPictureBox.BackColor = System.Drawing.Color.Black;
            this.cameraPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraPictureBox.Location = new System.Drawing.Point(0, 0);
            this.cameraPictureBox.Name = "cameraPictureBox";
            this.cameraPictureBox.Size = new System.Drawing.Size(320, 240);
            this.cameraPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cameraPictureBox.TabIndex = 0;
            this.cameraPictureBox.TabStop = false;
            // 
            // CameraViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cameraPictureBox);
            this.Name = "CameraViewControl";
            this.Size = new System.Drawing.Size(320, 240);
            ((System.ComponentModel.ISupportInitialize)(this.cameraPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox cameraPictureBox;
    }
}
