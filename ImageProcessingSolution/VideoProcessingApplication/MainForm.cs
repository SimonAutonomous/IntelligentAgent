using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
// using System.Threading; // REMOVE
using System.Windows.Forms;
using ImageProcessingLibrary.Cameras;
using ImageProcessingLibrary.MotionDetection;
using ObjectSerializerLibrary;

namespace VideoProcessingApplication
{
    public partial class MainForm : Form
    {
        #region Constants
        private const int DEFAULT_WIDTH = 640;
        private const int DEFAULT_HEIGHT = 480;
        private const int DEFAULT_FRAME_RATE = 25;
        private const int REQUIRED_SETUP_FORM_WIDTH = 1080;
        private const int REQUIRED_SETUP_FORM_HEIGHT = 580;
        #endregion

        #region Fields
        private Camera camera = null;
        private int width = DEFAULT_WIDTH;
        private int height = DEFAULT_HEIGHT;
        private int frameRate = DEFAULT_FRAME_RATE;
        private int previousWidth;
        private int previousHeight;
        private GaussianExponentialAveraging motionDetector;

        // Remove
     //   private Thread saveThread; 

        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (camera != null)
            {
                cameraViewControl.Stop();
                camera.Stop();
            }
            Application.Exit();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            exitToolStripMenuItem.Enabled = false;
            startButton.Enabled = false;
            camera = new Camera();
            camera.CameraStopped += new EventHandler(HandleCameraStopped);
            if (CaptureDevice.GetDeviceNames().Count == 0)
            {
                MessageBox.Show("Please connect a camera!");
                startButton.Enabled = true;
                exitToolStripMenuItem.Enabled = true;
                return;
            }
            camera.DeviceName = CaptureDevice.GetDeviceNames()[0];
            camera.ImageWidth = width;
            camera.ImageHeight = height;
            camera.FrameRate = frameRate;
            camera.Start();
            cameraViewControl.SetCamera(camera);
            cameraViewControl.Start();
            motionDetector = new GaussianExponentialAveraging();
            motionDetectionControl.SetMotionDetector(motionDetector);
            motionDetector.SetCamera(camera);
            motionDetector.Start();
            motionDetectionControl.Start();
            mainTabControl.Enabled = true;
            stopButton.Enabled = true;
            previousWidth = this.Width;
            previousHeight = this.Height;
         //   saveThread = new Thread(new ThreadStart(() => SaveLoop()));
         //   saveThread.Start();
        }

     /*   private void SaveLoop()
        {
            Thread.Sleep(5000);
            for (int ii = 1; ii < 15; ii++)
            {
                Thread.Sleep(500);
                cameraViewControl.Bitmap.Save("CameraImage" + ii.ToString() + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                motionDetectionControl.Bitmap.Save("GestueImage" + ii.ToString() + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            }
        }  */

        private void stopButton_Click(object sender, EventArgs e)
        {
            stopButton.Enabled = false;
            motionDetector.Stop();
            motionDetectionControl.Stop();
            camera.Stop();
        }

        private void ToggleStopped()
        {
            startButton.Enabled = true;
            exitToolStripMenuItem.Enabled = true;
            mainTabControl.SelectedTab = cameraViewTabPage;
            mainTabControl.Enabled = false;
        }

        private void ThreadSafeToggleStopped()
        {
            if (InvokeRequired) { this.BeginInvoke(new MethodInvoker(() => ToggleStopped())); }
            else { ToggleStopped(); }
        }

        private void HandleCameraStopped(object sender, EventArgs e)
        {
            cameraSetupControl.Stop();
            cameraViewControl.Stop();
            ThreadSafeToggleStopped();
        }

        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((mainTabControl.SelectedTab == cameraViewTabPage) || (mainTabControl.SelectedTab == gestureViewTabPage))
            {
                cameraSetupControl.Stop();
                this.Width = previousWidth;
                this.Height = previousHeight;
            }
            else
            {
                if (camera != null) { cameraSetupControl.SetCamera(camera); }
                previousWidth = this.Width;
                previousHeight = this.Height;
                this.Width = REQUIRED_SETUP_FORM_WIDTH;
                this.Height = REQUIRED_SETUP_FORM_HEIGHT;
            }
        }
    }
}
