using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommunicationLibrary;
using CustomUserControlsLibrary;
using ImageProcessingLibrary;
using AuxiliaryLibrary;
using ImageProcessingLibrary.Cameras;
using ImageProcessingLibrary.Visualization;

namespace VisionApplication
{
    public partial class VisionApplicationMainForm : Form
    {
        private const int DEFAULT_CAMERA_WIDTH = 640;
        private const int DEFAULT_CAMERA_HEIGHT = 480;
        private const int DEFAULT_FRAME_RATE = 25;
        private const int CAMERA_SETUP_REQUIRED_WIDTH = 1100;
        private const int CAMERA_SETUP_REQUIRED_HEIGHT = 550;
        private const int DEFAULT_DEVICE_INDEX = 0;

        private const int MARGIN = 10;  // for resizing the test text box

        private const string CLIENT_NAME = "Vision";
        private const string DEFAULT_IP_ADDRESS = "127.0.0.1";
        private const int DEFAULT_PORT = 7;

        private string ipAddress = DEFAULT_IP_ADDRESS;
        private int port = DEFAULT_PORT;
        private Client client = null;

        private Camera camera;
        private int cameraWidth = DEFAULT_CAMERA_WIDTH;
        private int cameraHeight = DEFAULT_CAMERA_HEIGHT;
        private int formWidth;
        private int formHeight;
        private int frameRate = DEFAULT_FRAME_RATE;
        private int deviceIndex = DEFAULT_DEVICE_INDEX;
        private Boolean exitRequested = false;
        private Boolean formSizeSet = false;
        private Boolean showBoundingBox;
   //     private FaceDetector faceDetector = null;
        

        public VisionApplicationMainForm()
        {
            InitializeComponent();
            Initialize();
            Connect();
        }

        private void Connect()
        {
            client = new Client();
            client.Progress += new EventHandler<CommunicationProgressEventArgs>(HandleClientProgress);
            client.Name = CLIENT_NAME;
            client.Connect(ipAddress, port);
        }

        private void HandleClientProgress(object sender, CommunicationProgressEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => ShowProgress(e)));
            }
            else { ShowProgress(e); }
        }

        private void ShowProgress(CommunicationProgressEventArgs e)
        {
            ColorListBoxItem item;
            item = new ColorListBoxItem(e.Message, communicationLogColorListBox.BackColor, communicationLogColorListBox.ForeColor);
            communicationLogColorListBox.Items.Insert(0, item);
        }

        private void Start()
        {
            startCameraButton.Enabled = false;
            deviceNameComboBox.Enabled = false;
            camera.DeviceName = Camera.GetDeviceNames()[deviceIndex];
            camera.FrameRate = frameRate;
            camera.ImageWidth = cameraWidth;
            camera.ImageHeight = cameraHeight;
            camera.Start();
            faceDetectionControl.ShowSkinPixelsOnly = showOnlySkinPixelsToolStripMenuItem.Checked;
            faceDetectionControl.ShowBoundingBox = showBoundingBoxToolStripMenuItem.Checked;
            faceDetectionControl.ShowCenterLine = showCenterLineToolStripMenuItem.Checked;
            faceDetectionControl.SetCamera(camera);
            faceDetectionControl.Start();
            faceDetectionControl.FaceDetector.FaceBoundingBoxAvailable -= new EventHandler<FaceDetectionEventArgs>(ThreadSafeHandleFaceDetected); // For re-runs. 
            faceDetectionControl.FaceDetector.FaceBoundingBoxAvailable += new EventHandler<FaceDetectionEventArgs>(ThreadSafeHandleFaceDetected);
            stopCameraButton.Enabled = true;
        }

        private void Initialize()
        {
            formWidth = this.Width;
            formHeight = this.Height;
            camera = new Camera();
            camera.CameraStopped += new EventHandler(HandleCameraStopped);
            if (CaptureDevice.GetDeviceNames().Count == 0)
            {
                MessageBox.Show("Please connect a camera!");
                exitToolStripMenuItem.Enabled = true;
                return;
            }
            else
            {
                deviceNameComboBox.Items.Clear();
                List<string> deviceNameList = Camera.GetDeviceNames();
                foreach (string deviceName in deviceNameList)
                {
                    deviceNameComboBox.Items.Add(deviceName);
                }
                deviceNameComboBox.SelectedIndex = deviceIndex;
            }
            Start();
        }

        private void EnableStart()
        {
            startCameraButton.Enabled = true;
            deviceNameComboBox.Enabled = true;
        }

        private void ThreadSafeEnableStart()
        {
            if (InvokeRequired) { this.BeginInvoke(new MethodInvoker(() => EnableStart())); }
            else { EnableStart(); }
        }

        private void HandleCameraStopped(object sender, EventArgs e)
        {
            if (exitRequested)
            {
                Application.Exit();
            }
            else
            {
                ThreadSafeEnableStart();                
            }
        }

        // To do (for the students): Write this two methods (HandleFaceDetected) to handle the face detection. Perhaps pass
        // the face to a recognizer?
        private void HandleFaceDetected(object sender, FaceDetectionEventArgs e)
        {


            // Test: Uncomment to see the bounding box of the detected face.
            /* string information = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " Face detected. Bounding box: (" + e.Left.ToString() + "," +
                e.Top.ToString() + "," + e.Right.ToString() + "," + e.Bottom.ToString() + ")";
            ColorListBoxItem item = new ColorListBoxItem(information, faceDetectionColorListBox.BackColor, faceDetectionColorListBox.ForeColor);
            faceDetectionColorListBox.Items.Insert(0, item); */
        }


        private void ThreadSafeHandleFaceDetected(object sender, FaceDetectionEventArgs e)
        {
            if (e.LockAcquired)
            {
                if (InvokeRequired) { this.Invoke(new MethodInvoker(() => HandleFaceDetected(sender, e))); }
                else { HandleFaceDetected(sender, e); }
            }
        }    

        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formSizeSet)
            {
                formWidth = this.Width;
                formHeight = this.Height;
                formSizeSet = true;
            }
            if (mainTabControl.SelectedTab == cameraSetupTabPage)
            {
                this.Width = CAMERA_SETUP_REQUIRED_WIDTH;
                this.Height = CAMERA_SETUP_REQUIRED_HEIGHT;
                cameraSetupControl.SetCamera(camera);
            }
            else
            {
                this.Width = formWidth;
                this.Height = formHeight;
            }
        }

        private void VisionApplicationMainForm_ResizeEnd(object sender, EventArgs e)
        {
            if (mainTabControl.SelectedTab != cameraSetupTabPage)
            {
                cameraSetupControl.Stop();
                formWidth = this.Width;
                formHeight = this.Height;
            }
            else
            {
                cameraSetupControl.SetCamera(camera);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (camera.Connected)
            {
                exitRequested = true;
                client.Disconnect();
                faceDetectionControl.Stop();
                cameraSetupControl.Stop();
                camera.Stop();
            }
            else
            {
                Application.Exit();
            }
        }

        private void HandleFaceCenterHorizontalPositionAvailable(object sender, IntEventArgs e)
        {

        }

        private void testToolStrip_Resize(object sender, EventArgs e)
        {
            testTextBox.Width = testToolStrip.Width - testTextBox.Bounds.Left - MARGIN;
        }

        private void VisionApplicationMainForm_Load(object sender, EventArgs e)
        {
            testToolStrip_Resize(this, e);
        }

        private void sendTestCommandButton_Click(object sender, EventArgs e)
        {
            string testMessage = testTextBox.Text;
            client.Send(testMessage);
            testListBox.Items.Add(testTextBox.Text);
            testTextBox.Clear();
        }

        private void testListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (testListBox.SelectedIndex >= 0)
            {
                testTextBox.Text = testListBox.SelectedItem.ToString();
            }
        }

        private void testTextBox_TextChanged(object sender, EventArgs e)
        {
            sendTestCommandButton.Enabled = false;
            if (testTextBox.Text != "")
            {
                if (!testTextBox.Text.Contains("_"))  // _ is the only forbidden character in a client message. See DataPacket, line 18.
                {
                    sendTestCommandButton.Enabled = true;
                }
            }
        }

        private void showOnlySkinPixelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            faceDetectionControl.ShowSkinPixelsOnly = showOnlySkinPixelsToolStripMenuItem.Checked;
        }

        private void stopCameraButton_Click(object sender, EventArgs e)
        {
            stopCameraButton.Enabled = false;
            faceDetectionControl.Stop();
            camera.Stop();
        }

        private void startCameraButton_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void deviceNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            deviceIndex = deviceNameComboBox.SelectedIndex;
        }

        private void showBoundingBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            faceDetectionControl.ShowBoundingBox = showBoundingBoxToolStripMenuItem.Checked;
        }

        private void showCenterLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            faceDetectionControl.ShowCenterLine = showCenterLineToolStripMenuItem.Checked;
        }

        private void VisionApplicationMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (camera.Connected)
            {
                exitRequested = true;
                client.Disconnect();
                faceDetectionControl.Stop();
                cameraSetupControl.Stop();
                camera.Stop();
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
