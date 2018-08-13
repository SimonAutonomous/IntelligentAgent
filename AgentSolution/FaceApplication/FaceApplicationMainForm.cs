using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommunicationLibrary;
using CustomUserControlsLibrary;
using ObjectSerializerLibrary;
using ThreeDimensionalVisualizationLibrary;
using ThreeDimensionalVisualizationLibrary.Faces;
using ThreeDimensionalVisualizationLibrary.Objects;
using OpenTK.Graphics.OpenGL;

namespace FaceApplication
{
    // =======================================================
    //
    // Note to students: Unlike most other code in the IPASrc
    // code collection, this simple demonstration application
    // contains a lot of ugly parameter hard-coding. 
    // However, the parameters in question concern the detailed
    // shape of the agent's facial features (e.g. eyes, eyebrows etc.)
    // which would normally be defined in a separate editor and
    // then simply be loaded into the application. Here, only
    // the skull (i.e. excluding eyes etc.) is loaded from file
    // since the FaceEditor does not yet have the capability of
    // generating eyes and other features.

    // The application is meant as a simple demonstration of
    // 3D visualization and animation. In your own code,
    // make sure that you parameterize constants properly.
    //
    // ========================================================


    public partial class FaceApplicationMainForm : Form
    {
        private const string DATETIME_FORMAT = "yyyyMMdd HH:mm:ss";
        private const string CLIENT_NAME = "Face";
        private const string DEFAULT_IP_ADDRESS = "127.0.0.1";
        private const int DEFAULT_PORT = 7;
        private const string DEFAULT_RELATIVE_FACE_FILE_NAME = "\\..\\..\\..\\Data\\Face.xml";
        private const double DEFAULT_SECOND_BLINK_PROBABILITY = 0.3;

        private string ipAddress = DEFAULT_IP_ADDRESS;
        private int port = DEFAULT_PORT;
        private Client client = null;

        private Thread blinkThread = null;
        private Thread rotateLeftThread = null;
        private Thread rotateHeadThread = null;
        private Thread nodThread = null;
        private Thread tiltThread = null;
        private Thread openEyesThread = null;
        private Thread shakeThread = null;

        private System.Boolean normalThreadRunning = false;
        private Thread normalThread = null; // Handles normal face actions, e.g. blinking.

        private System.Boolean blinking = false;
        private System.Boolean eyesClosed = false;
        private System.Boolean nodding = false;
        private System.Boolean shaking = false;

        private Random randomNumberGenerator;
        private double secondBlinkProbability = DEFAULT_SECOND_BLINK_PROBABILITY;

        private Face face;

        public FaceApplicationMainForm()
        {
            InitializeComponent();
            Initialize();
            Connect();
        }

        private void Connect()
        {
            client = new Client();
            client.Received += new EventHandler<DataPacketEventArgs>(HandleClientReceived);
            client.Progress += new EventHandler<CommunicationProgressEventArgs>(HandleClientProgress);
            client.Name = CLIENT_NAME;
            client.Connect(ipAddress, port);
        }

        private void HandleClientReceived(object sender, DataPacketEventArgs e)
        {
            string faceAction = e.DataPacket.Message;
            if (faceAction.ToLower() == "openeyes") { OpenEyes(); }

            // ToDO: Add more actions here

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
            item = new ColorListBoxItem(e.Message, communicationLogListBox.BackColor, communicationLogListBox.ForeColor);
            communicationLogListBox.Items.Insert(0, item);
        }

        private void Initialize()
        {
            randomNumberGenerator = new Random();
            string faceFileName = Path.GetDirectoryName(Application.ExecutablePath) + DEFAULT_RELATIVE_FACE_FILE_NAME;
            if (File.Exists(faceFileName))
            {
                face = (Face)ObjectXmlSerializer.ObtainSerializedObject(faceFileName, typeof(Face));
                face.Name = "Face";
                face.Generate(new List<double> { 128 });
                face.Visible = true; 
                face.SpecularColor = Color.OrangeRed;
                face.Shininess = 50;

                // A lot of ugly hard-coding here, but OK - just for demonstration:
                face.Object3DList = new List<Object3D>();

                Sphere3D leftEyeBulb = new Sphere3D();
                leftEyeBulb.Generate(new List<double>() { 0.07, 64, 64 });
                leftEyeBulb.AmbientColor = Color.White;
                leftEyeBulb.DiffuseColor = Color.White;
                leftEyeBulb.SpecularColor = Color.White;
                leftEyeBulb.Shininess = 50;
                leftEyeBulb.ShowSurfaces = true;
                // leftEyeBulb.Move(-0.1375, -0.403, 1.145);
                leftEyeBulb.ShadingModel = ShadingModel.Smooth;
                leftEyeBulb.UseLight = true;
                leftEyeBulb.Name = "LeftEye";
                leftEyeBulb.Object3DList = new List<Object3D>();

                SphereSegment3D leftEyeIris = new SphereSegment3D();
                leftEyeIris.Generate(new List<double>() { 0.07, 32, 32, Math.PI / 3 });
                leftEyeIris.AmbientColor = Color.Green;
                leftEyeIris.DiffuseColor = Color.Green;
                leftEyeIris.SpecularColor = Color.White;
                leftEyeIris.Shininess = 50;
                leftEyeIris.ShowSurfaces = true;
                //   leftEyeIris.Move(-0.1375, -0.407, 0.145);
                leftEyeIris.Move(0, -0.005, 0);
                leftEyeIris.RotateX(90);
                SphereSegment3D leftEyePupil = new SphereSegment3D();
                leftEyePupil.Generate(new List<double>() { 0.07, 16, 32, Math.PI / 2 - 0.2 });
                leftEyePupil.AmbientColor = Color.Black;
                leftEyePupil.DiffuseColor = Color.Black;
                leftEyePupil.SpecularColor = Color.White;
                leftEyePupil.Shininess = 50;
                leftEyePupil.ShowSurfaces = true;
                leftEyePupil.Move(0, -0.01, 0);
                leftEyePupil.RotateX(90); // , 0, 0);
                                          //   leftEyePupil.RotateX(Math.PI / 2);
                                          //  leftEyePupil.Translate(-0.1375, -0.4758, 0.0725);
                leftEyePupil.ShadingModel = ShadingModel.Smooth;
                leftEyePupil.UseLight = true;
                SphereSegment3D leftEyeLid = new SphereSegment3D();
                leftEyeLid.Name = "LeftEyeLid";
                leftEyeLid.Generate(new List<double>() { 0.07, 32, 32, 0.0 });
                leftEyeLid.AmbientColor = Color.FromArgb(255, 205, 148);  // Typical skin color approximation
                leftEyeLid.DiffuseColor = Color.FromArgb(255, 205, 148);  // Typical skin color approximation
                leftEyeLid.SpecularColor = Color.White;
                leftEyeLid.Shininess = 50;
                leftEyeLid.ShowSurfaces = true;
                leftEyeLid.RotateX(-36);
                leftEyeLid.Move(0, -0.015, 0);
                leftEyeBulb.Object3DList.Add(leftEyeIris);
                leftEyeBulb.Object3DList.Add(leftEyePupil);
                leftEyeBulb.Object3DList.Add(leftEyeLid);
                face.Object3DList.Add(leftEyeBulb);

                Sphere3D rightEyeBulb = new Sphere3D();
                rightEyeBulb.Generate(new List<double>() { 0.07, 64, 64 });
                rightEyeBulb.AmbientColor = Color.White;
                rightEyeBulb.DiffuseColor = Color.White;
                rightEyeBulb.SpecularColor = Color.White;
                rightEyeBulb.Shininess = 50;
                rightEyeBulb.ShowSurfaces = true;
                //  rightEyeBulb.Move(0.1375, -0.403, 0.145);
                rightEyeBulb.ShadingModel = ShadingModel.Smooth;
                rightEyeBulb.UseLight = true;
                rightEyeBulb.Name = "RightEye";
                rightEyeBulb.Object3DList = new List<Object3D>();
                face.Object3DList.Add(rightEyeBulb);
                SphereSegment3D rightEyeIris = new SphereSegment3D();
                rightEyeIris.Generate(new List<double>() { 0.07, 32, 32, Math.PI / 3 });
                rightEyeIris.AmbientColor = Color.Green;
                rightEyeIris.DiffuseColor = Color.Green;
                rightEyeIris.SpecularColor = Color.White;
                rightEyeIris.Shininess = 50;
                rightEyeIris.ShowSurfaces = true;
                rightEyeIris.RotateX(90); //  , 0, 0);
                rightEyeIris.Move(0, -0.005, 0);
                //   rightEyeIris.RotateX(Math.PI / 2);
                //   rightEyeIris.Translate(0.1375, -0.4715, 0.075);
                rightEyeIris.ShadingModel = ShadingModel.Smooth;
                rightEyeIris.UseLight = true;
                SphereSegment3D rightEyePupil = new SphereSegment3D();
                rightEyePupil.Generate(new List<double>() { 0.07, 16, 32, Math.PI / 2 - 0.2 });
                rightEyePupil.AmbientColor = Color.Black;
                rightEyePupil.DiffuseColor = Color.Black;
                rightEyePupil.SpecularColor = Color.White;
                rightEyePupil.Shininess = 50;
                rightEyePupil.ShowSurfaces = true;
                rightEyePupil.RotateX(90); //, 0, 0);
                rightEyePupil.Move(0, -0.01, 0);
                //  rightEyePupil.RotateX(Math.PI / 2);
                //  rightEyePupil.Translate(0.1375, -0.4758, 0.0725);
                rightEyePupil.ShadingModel = ShadingModel.Smooth;
                rightEyePupil.UseLight = true;
                SphereSegment3D rightEyeLid = new SphereSegment3D();
                rightEyeLid.Name = "RightEyeLid";
                rightEyeLid.Generate(new List<double>() { 0.07, 32, 32, 0.0 });
                rightEyeLid.AmbientColor = Color.FromArgb(255, 205, 148);  // Typical skin color approximation
                rightEyeLid.DiffuseColor = Color.FromArgb(255, 205, 148);  // Typical skin color approximation
                rightEyeLid.SpecularColor = Color.White;
                rightEyeLid.Shininess = 50;
                rightEyeLid.ShowSurfaces = true;
                rightEyeLid.RotateX(-36); // , 0, 0);
                rightEyeLid.Move(0, -0.015, 0);
                rightEyeBulb.Object3DList.Add(rightEyeIris);
                rightEyeBulb.Object3DList.Add(rightEyePupil);
                rightEyeBulb.Object3DList.Add(rightEyeLid);

                TorusSector3D leftEyebrow = new TorusSector3D();
                leftEyebrow.Name = "LeftEyebrow";
                leftEyebrow.Generate(new List<double> { 0.15, 0.01, 30, 30, 3 * Math.PI / 2 - 0.60, 3 * Math.PI / 2 + 0.40 });
                leftEyebrow.AmbientColor = Color.Black;
                leftEyebrow.DiffuseColor = Color.Black;
                leftEyebrow.SpecularColor = Color.White;
                leftEyebrow.Shininess = 50;
                leftEyebrow.ShowSurfaces = true;
                leftEyebrow.RotateZ(-11);
                TorusSector3D rightEyebrow = new TorusSector3D();
                rightEyebrow.Name = "RightEyebrow";
                rightEyebrow.Generate(new List<double> { 0.15, 0.01, 30, 30, 3 * Math.PI / 2 - 0.60, 3 * Math.PI / 2 + 0.40 });
                rightEyebrow.AmbientColor = Color.Black;
                rightEyebrow.DiffuseColor = Color.Black;
                rightEyebrow.SpecularColor = Color.White;
                rightEyebrow.Shininess = 50;
                rightEyebrow.ShowSurfaces = true;
                rightEyebrow.RotateZ(11);
                face.Object3DList.Add(leftEyebrow);
                face.Object3DList.Add(rightEyebrow);

                face.Move(0, 0, -0.5);
                leftEyeBulb.Move(-0.1375, -0.403, 0.645);
                rightEyeBulb.Move(0.1375, -0.403, 0.645);
                leftEyebrow.Move(-0.11, -0.325, 0.73);
                rightEyebrow.Move(0.11, -0.325, 0.73);

                // Close eyes to start with:
                leftEyeLid.RotateX(120);
                rightEyeLid.RotateX(120);
                leftEyebrow.Move(0, -0.005, -0.02);
                rightEyebrow.Move(0, -0.005, -0.02);
                eyesClosed = true;

                Scene3D scene = new Scene3D();
                Light light = new Light();
                light.IsOn = true;
                light.Position = new List<float>() { 0.0f, -3.0f, 1f, 1.0f };
                scene.LightList.Add(light);
                scene.AddObject(face);
                viewer3D.Scene = scene;
                viewer3D.ShowWorldAxes = false; // true;
                viewer3D.CameraDistance = 0.8;
                viewer3D.CameraLatitude = Math.PI / 24;
                viewer3D.CameraLongitude = -Math.PI / 2;
                //  viewer3D.Invalidate();
                viewer3D.StartAnimation();
            }

            normalThreadRunning = true;
            normalThread = new Thread(new ThreadStart(NormalLoop));
            normalThread.Start();
        }

        private void NormalLoop()
        {
            while (normalThreadRunning)
            {
                Thread.Sleep(1000);
                double r = randomNumberGenerator.NextDouble();
                if (r < secondBlinkProbability)
                {
                    if ((!blinking) && (!eyesClosed))
                    {
                        Blink();
                    }
                }
            }
        }

        private void OpenEyesLoop()
        {
            const int NUMBER_OF_OPENING_STEPS = 12;
            const int OPENING_STEP_MILLISECOND_DURATION = 10;

            Object3D leftEyeLid = viewer3D.Scene.GetObject("LeftEyeLid");
            Object3D rightEyeLid = viewer3D.Scene.GetObject("RightEyeLid");
            Object3D leftEyebrow = viewer3D.Scene.GetObject("LeftEyebrow");
            Object3D rightEyebrow = viewer3D.Scene.GetObject("RightEyebrow");
            for (int ii = 0; ii < NUMBER_OF_OPENING_STEPS; ii++)
            {
                Thread.Sleep(OPENING_STEP_MILLISECOND_DURATION);
                leftEyebrow.Move(0, 0.000416666, 0.0016666666);
                rightEyebrow.Move(0, 0.000416666, 0.001666666);
                leftEyeLid.RotateX(-10);
                rightEyeLid.RotateX(-10);
            }
            eyesClosed = false;
        }

        private void OpenEyes()
        {
            if (eyesClosed)
            {
                openEyesThread = new Thread(new ThreadStart(() => OpenEyesLoop()));
                openEyesThread.Start();
            }
        }

        private void openEyesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (eyesClosed)
            {
                OpenEyes();
            }
        }

        private void BlinkLoop()
        {
            const int NUMBER_OF_BLINK_STEPS = 15;
            const int BLINK_STEP_MILLISECOND_DURATION = 10;

            Object3D leftEyeLid = viewer3D.Scene.GetObject("LeftEyeLid");
            Object3D rightEyeLid = viewer3D.Scene.GetObject("RightEyeLid");
            for (int ii = 0; ii < NUMBER_OF_BLINK_STEPS; ii++)
            {
                Thread.Sleep(BLINK_STEP_MILLISECOND_DURATION);
                Stopwatch sw = new Stopwatch();
                sw.Start();
                leftEyeLid.RotateX(5);
                rightEyeLid.RotateX(5);
                sw.Stop();
                int elapsedMilliseconds = (int)Math.Round(1000 * sw.ElapsedTicks / (double)Stopwatch.Frequency);
            }
            for (int ii = 0; ii < NUMBER_OF_BLINK_STEPS; ii++)
            {
                Thread.Sleep(BLINK_STEP_MILLISECOND_DURATION);
                leftEyeLid.RotateX(-5);
                rightEyeLid.RotateX(-5);
            }
            blinking = false;
        }

        private void Blink()
        {
            if (!eyesClosed)
            {
                if (!blinking)
                {
                    blinking = true;
                    blinkThread = new Thread(new ThreadStart(() => BlinkLoop()));
                    blinkThread.Start();
                }
            }
        }

        private void blinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Blink();
        }

        private void NodLoop()
        {
            const int NUMBER_OF_NOD_STEPS = 20;
            const int NOD_STEP_MILLISECOND_DURATION = 10;

            Object3D face = viewer3D.Scene.GetObject("Face");
            for (int ii = 0; ii < NUMBER_OF_NOD_STEPS; ii++)
            {
                Thread.Sleep(NOD_STEP_MILLISECOND_DURATION);
                face.RotateX(0.5);
            }
            for (int ii = 0; ii < NUMBER_OF_NOD_STEPS; ii++)
            {
                Thread.Sleep(NOD_STEP_MILLISECOND_DURATION);
                face.RotateX(-0.5);
            }
            nodding = false;
        }

        private void Nod()
        {
            if ((!nodding) && (!shaking))
            {
                nodding = true;
                nodThread = new Thread(new ThreadStart(NodLoop));
                nodThread.Start();
            }
        }

        private void nodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Nod();
        }

        private void ShakeLoop()
        {
            const int NUMBER_OF_SHAKE_STEPS = 15;
            const int SHAKE_STEP_MILLISECOND_DURATION = 10;

            Object3D face = viewer3D.Scene.GetObject("Face");
            for (int ii = 0; ii < NUMBER_OF_SHAKE_STEPS; ii++)
            {
                Thread.Sleep(SHAKE_STEP_MILLISECOND_DURATION);
                face.RotateZ(0.5);
            }
            for (int ii = 0; ii < 2* NUMBER_OF_SHAKE_STEPS; ii++)
            {
                Thread.Sleep(10);
                face.RotateZ(-0.5);
            }
            for (int ii = 0; ii < NUMBER_OF_SHAKE_STEPS; ii++)
            {
                Thread.Sleep(SHAKE_STEP_MILLISECOND_DURATION);
                face.RotateZ(0.5);
            }
            shaking = false;
        }

        private void ShakeHead()
        {
            if ((!nodding) && (!shaking))
            {
                shaking = true;
                shakeThread = new Thread(new ThreadStart(ShakeLoop));
                shakeThread.Start();
            }
        }

        private void shakeHeadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShakeHead();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewer3D.Stop();
            if (normalThread != null)
            {
                normalThread.Abort();
            }
            Application.Exit();
        }

        private void FaceApplicationMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            viewer3D.Stop();
            if (normalThread != null)
            {
                normalThread.Abort();
            }
            Application.Exit();
        }
    }
}
