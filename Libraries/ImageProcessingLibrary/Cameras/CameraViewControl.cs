using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ImageProcessingLibrary.Cameras
{
    public partial class CameraViewControl : UserControl
    {
        protected int MILLSECOND_TIMEOUT = 50;

        protected Camera camera;
        protected Bitmap bitmap;
        protected int millisecondSleepTime;

        protected Thread runThread;
        protected Boolean running = false;
        protected static object lockObject = new object();

       

        public CameraViewControl()
        {
            InitializeComponent();
        }

        public virtual void SetCamera(Camera camera)
        {
            this.camera = camera;
            millisecondSleepTime = (int)Math.Round(1000 / (double)this.camera.FrameRate);
            running = false;
        }

        private void ThreadSafeShowBitmap(Bitmap bitmap, PictureBox pictureBox)
        {
            cameraPictureBox.Invoke(new MethodInvoker(
                    delegate { ShowBitmap(bitmap, pictureBox); }
             ));
        }

        private void ShowBitmap(Bitmap bitmap, PictureBox pictureBox)
        {
            pictureBox.Image = bitmap;
        }

        // Do nothing in the base class, but allows the user to add methods
        // for image processing in derived classes.
        protected virtual Bitmap ProcessImage(Bitmap bitmap)
        {
            return bitmap;
        }

        private void RunLoop()
        {
            while (running)
            {
                try
                {
                    Monitor.Enter(lockObject);
                    bitmap = camera.GetBitmap(); // camera.Read();
                    bitmap = ProcessImage(bitmap);
                    Monitor.Exit(lockObject);
                    if (bitmap != null)
                    {
                        ThreadSafeShowBitmap(bitmap, cameraPictureBox);
                    }
                    Thread.Sleep(millisecondSleepTime);
                }
                catch
                {
                    Thread.Sleep(millisecondSleepTime);
                }
            }
            runThread = null;
        }

        public void Start()
        {
            running = true;
            runThread = new Thread(new ThreadStart(RunLoop));
            runThread.Start();
        }

        public virtual void Stop()
        {
            running = false;
        }

        public Bitmap Bitmap
        {
            get
            {
                Monitor.Enter(lockObject);
                Bitmap accessedBitmap = (Bitmap)bitmap.Clone();
                Monitor.Exit(lockObject);
                return accessedBitmap;
            }
        }

    }
}
