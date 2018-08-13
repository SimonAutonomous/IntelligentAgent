using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using ThreeDimensionalVisualizationLibrary.Objects;

namespace ThreeDimensionalVisualizationLibrary
{
    public class Viewer3D: GLControl
    {
        private const double DEFAULT_FOCAL_LENGTH = 0.036;
        private const double DEFAULT_SENSOR_SIZE = 0.072;
        private const float DEFAULT_KEY_TRANSLATION_STEP = 0.25f;
        private const double DEFAULT_FRAMES_PER_SECOND = 60;

        private double focalLength = DEFAULT_FOCAL_LENGTH;
        private double sensorSize = DEFAULT_SENSOR_SIZE;
        private Vector3 cameraPosition; // Camera position in world coordinates
        private float cameraYaw = 0f;
        private float cameraPitch = 0f;
        private float cameraRoll = 0f;
        private Matrix4 cameraMatrix;
        private Matrix4 translationMatrix;
        private Matrix4 rollMatrix;
        private Matrix4 pitchMatrix;
        private Matrix4 yawMatrix;

        private System.Boolean showOpenGLAxes = false; // false;
        private System.Boolean showWorldAxes = false; // false;
        private System.Boolean useSmoothShading = false;

        private ShadingModel shadingModel = ShadingModel.Flat;
        private System.Boolean showWireframe = false;  // Overall setting (for all objects, if used)
        private System.Boolean showSurfaces = true; // Overall setting (for all objects, if used)
        private System.Boolean showVertices = false; // Overall setting (for all objects, if used)

        private List<Keys> pressedKeys = new List<Keys>();
        private float keyTranslationStep = DEFAULT_KEY_TRANSLATION_STEP;

        private Scene3D scene = null;

        private double cameraDistance; // Distance from the camera to the origin (0,0,0);
        private double cameraLongitude; // Angle of vector connecting the origin to the camera (note: the camera LOOKS in the opposite direction!)
        private double cameraLatitude;
        private Vector3 cameraTarget; // The target for the camera, in view coordinates (i.e. NOT OpenGL coordinates)

        private PointF grabPoint;

        public event EventHandler ArrowUpPressed = null;
        public event EventHandler ArrowDownPressed = null;

        private Thread animationThread = null;  // 20161011 NEW
        private System.Boolean animationRunning = false; // 20161011 NEW
        private double framesPerSecond = DEFAULT_FRAMES_PER_SECOND;
        private int millisecondAnimationSleepInterval;

        public Viewer3D()
            : base()
        {
            this.Paint += new PaintEventHandler(HandlePaint);
            this.Resize += new EventHandler(HandleResize);
            this.MouseDown += new MouseEventHandler(HandleMouseDown);  // test method to be removed or generalized
            this.MouseMove += new MouseEventHandler(HandleMouseMove);
            this.MouseWheel += new MouseEventHandler(HandleMouseWheel);
            this.KeyDown += new KeyEventHandler(HandleKeyDown);
            this.KeyUp += new KeyEventHandler(HandleKeyUp);
            InitializeCamera();
        }

        private void OnArrowUpPressed()
        {
            if (ArrowUpPressed != null)
            {
                EventHandler handler = ArrowUpPressed;
                handler(this, EventArgs.Empty);
            }
        }

        private void OnArrowDownPressed()
        {
            if (ArrowDownPressed != null)
            {
                EventHandler handler = ArrowDownPressed;
                handler(this, EventArgs.Empty);
            }
        }

        // Needed for handling arrow keys etc.
        protected override System.Boolean IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
            
        }

        private void HandleMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                grabPoint = new PointF(e.X, e.Y);
            }
        }

        // MW ToDo: Some ugly-hard coding here...
        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                float deltaX = e.X - grabPoint.X;
                float deltaY = e.Y - grabPoint.Y;
                cameraLongitude -= 0.03 * deltaX;
                cameraLongitude = (cameraLongitude % (2 * Math.PI));
                cameraLatitude += 0.05 * deltaY;
                if (cameraLatitude > Math.PI / 2) { cameraLatitude = Math.PI / 2; }
                else if (cameraLatitude < -Math.PI / 2) { cameraLatitude = -Math.PI / 2; }
                ApplyCameraPose();
                grabPoint.X = e.X;
                grabPoint.Y = e.Y;
            }
        }

        // MW ToDo: Some ugly hard-coding here.
        private void HandleMouseWheel(object sender, MouseEventArgs e)
        {
            cameraDistance *= 1.00 + (0.001 * e.Delta);
            ApplyCameraPose();
        }

        protected virtual void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (!pressedKeys.Contains(e.KeyCode))
            {
                pressedKeys.Add(e.KeyCode);
            }
            if (pressedKeys.Contains(Keys.Up))
            {
                OnArrowUpPressed();
            }
            if (pressedKeys.Contains(Keys.Down))
            {
                OnArrowDownPressed();
            }
        }

        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            pressedKeys.Remove(e.KeyCode);
        }

        // Note: The camera is really at (0,0,0). Moving the camera <=> moving the world in the other direction.  (ToDo: CHECK THIS!)
        // See https://www.opengl.org/archives/resources/faq/technical/viewing.htm
        private void InitializeCamera()
        {
            cameraLongitude = Math.PI / 4;
            cameraLatitude = Math.PI/8;
            cameraDistance = 4;

            cameraTarget = new Vector3(0f, 0f, 0f);

            // Translation: Moving the camera <=> moving the world in the opposite direction,
            //              hence minus signs. However, since z(OpenGL) = -y(World), there is
            //              a double minus sign on the third coordinate. Note that cameraPosition
            //              is in world coordinates.
            ApplyCameraPose();
        }

        public void RotateYaw(float cameraYaw)
        {
            this.cameraYaw = cameraYaw;
            yawMatrix = Matrix4.CreateRotationY(-cameraYaw);
        }

        public void RotatePitch(float cameraPitch)
        {
            this.cameraPitch = cameraPitch;
            pitchMatrix = Matrix4.CreateRotationX(-cameraPitch);
        }

        public void RotateRoll(float cameraRoll)
        {
            this.cameraRoll = cameraRoll;
            rollMatrix = Matrix4.CreateRotationZ(cameraRoll);
        }

        // See http://www.glprogramming.com/red/chapter03.html  (text around Fig. 3.10)
        private void ApplyCameraPose()
        {
            float cameraX = (float)(cameraDistance * Math.Cos(cameraLongitude) * Math.Cos(cameraLatitude));
            float cameraY = (float)(cameraDistance * Math.Sin(cameraLongitude) * Math.Cos(cameraLatitude));
            float cameraZ = (float)(cameraDistance * Math.Sin(cameraLatitude));
            cameraPosition = new Vector3(cameraX, cameraZ, -cameraY);

            cameraMatrix = Matrix4.LookAt(cameraPosition.X, cameraPosition.Y, cameraPosition.Z, 
                                          cameraTarget.X, cameraTarget.Z, -cameraTarget.Y, 
                                          0f, 1f, 0f);

            Invalidate();
        }

        private void DrawOpenGLAxes()
        {
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0f, 0f, 0f);
            GL.Vertex3(10f, 0f, 0f);
            GL.End();
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Green);
            GL.Vertex3(0f, 0f, 0f);
            GL.Vertex3(0f, 10f, 0f);
            GL.End();
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Blue);
            GL.Vertex3(0f, 0f, 0f);
            GL.Vertex3(0f, 0f, 10f);
            GL.End();
        }

        // x(World) = x(OpenGL)
        // y(World) = -z(OpenGL)
        // z(World) = y(OpenGL)
        private void DrawWorldAxes()
        {
            if (scene.AnyLightOn())
            {
                GL.Disable(EnableCap.Lighting);  // Temporarily disable lighting to get the specified colors of the axis
            }
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0f, 0f, 0f);
            GL.Vertex3(10f, 0f, 0f);
            GL.End();
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Green);
            GL.Vertex3(0f, 0f, 0f);
            GL.Vertex3(0f, 0f, -10f);
            GL.End();
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Blue);
            GL.Vertex3(0f, 0f, 0f);
            GL.Vertex3(0f, 10f, 0f);
            GL.End();
            if (scene.AnyLightOn())
            {
                GL.Enable(EnableCap.Lighting);  // Enable lighting again
            }
        }

        private void SetLights()
        {
            if (scene == null) { return; }
            if (scene.ObjectList == null) { return; }
            if (scene.AnyLightOn())
            {
             //   GL.ShadeModel(ShadingModel.Smooth);
                GL.Enable(EnableCap.Lighting);
                foreach (Light light in scene.LightList)
                {
                    if (light.IsOn)
                    {
                        GL.Enable(light.EnableCap);
                        GL.Enable(EnableCap.DepthTest);

                        GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 0.2f, 0.2f, 0.2f, 1.0f });
                        GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 0.8f, 0.8f, 0.8f, 1.0f });
                        GL.Light(LightName.Light0, LightParameter.Specular, new float[] { 1f, 1f, 1f, 1.0f });
                     
                     
                        Vector4 lightPosition = new Vector4(light.Position[0], light.Position[2], -light.Position[1], light.Position[3]);

                        GL.Light(LightName.Light0, LightParameter.Position, lightPosition);
                    }
                }
            }
        }

        public void ForceRender()
        {
            RenderObjects();
            Invalidate();
        }

        protected void RenderObjects()
        {
            if (scene == null) { return; }
            if (scene.ObjectList == null) { return; }
            foreach (Object3D object3D in scene.ObjectList)
            {
                object3D.Render(); 
            }

        }

        private void HandlePaint(object sender, PaintEventArgs e)
        {
            if (scene == null) { return; }
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref cameraMatrix);
            SetLights();
            if (showOpenGLAxes) { DrawOpenGLAxes(); }
            if (showWorldAxes) { DrawWorldAxes(); }
            RenderObjects();
            SwapBuffers();
        }

        private void HandleResize(object sender, EventArgs e)
        {
            if (DesignMode) { return; }
        //    if (scene == null) { return; }
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(Color.Black);
            GL.Viewport(0, 0, this.Width, this.Height);
            float fieldOfViewY = (float)(2 * Math.Atan2(sensorSize / 2, focalLength));
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(fieldOfViewY, this.Width / (float)this.Height, 0.1f, 100.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        // 20160617
        public void AddTexture(Bitmap textureBitmap)
        {
            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            BitmapData textureBitmapData = textureBitmap.LockBits(new Rectangle(0, 0, textureBitmap.Width, textureBitmap.Height), 
                  ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, textureBitmap.Width, textureBitmap.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, textureBitmapData.Scan0);

            textureBitmap.UnlockBits(textureBitmapData);

        }

        // 20161011 NEW
        private void AnimationLoop()
        {
            while (animationRunning)
            {
                Thread.Sleep(millisecondAnimationSleepInterval);  
                Invalidate();
            }
        }

        //20161011 NEW
        public void StartAnimation()
        {
            millisecondAnimationSleepInterval = (int)Math.Round(1000 / framesPerSecond);
            animationThread = new Thread(new ThreadStart(() => AnimationLoop()));
            animationRunning = true;
            animationThread.Start();
        }

        public void Stop()
        {
            animationRunning = false;
        }


        public Scene3D Scene
        {
            get { return scene; }
            set 
            { 
                scene = value;
                Invalidate();
            }
        }

        public System.Boolean UseSmoothShading
        {
            get { return useSmoothShading; }
            set
            {
                System.Boolean oldValue = useSmoothShading;
                useSmoothShading = value;
                if (useSmoothShading) {shadingModel = ShadingModel.Smooth; }
                else { shadingModel = ShadingModel.Flat; }
                if (useSmoothShading != oldValue) { Invalidate(); }
            }
        }

        // This property determines whether or not to show wire frame for ALL objects in the scene
        // In order to show wire frame for just one object, access that
        // object directly instead (scene.ObjectList ...)
        public System.Boolean ShowWireframe
        {
            get { return showWireframe; }
            set
            {
                System.Boolean oldValue = showWireframe;
                showWireframe = value;
                if (showWireframe != oldValue)
                {
                    if (scene != null)
                    {
                        if (scene.ObjectList != null)
                        {
                            foreach (Object3D object3D in scene.ObjectList) { object3D.ShowWireFrame = value; }
                        }
                    }
                    ForceRender();
                }
            }
        }

        // This property determines whether or not to show surfaces for ALL objects in the scene
        // In order to show surfaces for just one object, access that
        // object directly instead (scene.ObjectList ...)
        public System.Boolean ShowSurfaces
        {
            get { return showSurfaces; }
            set
            {
                System.Boolean oldValue = showSurfaces;
                showSurfaces = value;
                if (showSurfaces != oldValue)
                {
                    if (scene != null)
                    {
                        if (scene.ObjectList != null)
                        {
                            foreach (Object3D object3D in scene.ObjectList) { object3D.ShowSurfaces = value; }
                        }
                    }
                    ForceRender();
                }
            }
        }

        // This property determines whether or not to show vertices for ALL objects in the scene
        // In order to show vertices for just one object, access that
        // object directly instead (scene.ObjectList ...)
        public System.Boolean ShowVertices
        {
            get { return showVertices; }
            set
            {
                System.Boolean oldValue = showVertices;
                showVertices = value;
                if (showVertices != oldValue)
                {
                    if (scene != null)
                    {
                        if (scene.ObjectList != null)
                        {
                            foreach (Object3D object3D in scene.ObjectList) { object3D.ShowVertices = value; }
                        }
                    }
                    ForceRender();
                }
            }
        }

        public System.Boolean ShowWorldAxes
        {
            get { return showWorldAxes; }
            set
            {
                System.Boolean oldValue = showWorldAxes;
                showWorldAxes = value;
                if (showWorldAxes != oldValue)
                {
                    Invalidate();
                }
            }
        }

        public Vector3 CameraTarget
        {
            get { return cameraTarget; }
            set
            {
                cameraTarget = value;
                ApplyCameraPose();
            }
        }

        public double CameraDistance
        {
            get { return cameraDistance; }
            set
            {
                cameraDistance = value;
                ApplyCameraPose();
            }
        }

        public double CameraLatitude
        {
            get { return cameraLatitude; }
            set
            {
                cameraLatitude = value;
                ApplyCameraPose();
            }
        }

        public double CameraLongitude
        {
            get { return cameraLongitude; }
            set
            {
                cameraLongitude = value;
                ApplyCameraPose();
            }
        }
    }
}
