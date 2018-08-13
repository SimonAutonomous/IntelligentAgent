using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ImageProcessingLibrary.MotionDetection
{
    public partial class MotionDetectionControl : UserControl
    {
        private const int DEFAULT_FRAME_RATE = 25;

        private MotionDetector motionDetector = null;
        private Thread runThread;
        private Boolean running = false;
        private int frameRate = DEFAULT_FRAME_RATE;
        private int millisecondInterval;

        public MotionDetectionControl()
        {
            InitializeComponent();
            millisecondInterval = (int)Math.Round(1000 / (double)frameRate);
            running = false;
        }

        private void ThreadSafeShowBitmap(Bitmap bitmap)
        {
            if (InvokeRequired) { this.Invoke(new MethodInvoker(() => ShowBitmap(bitmap))); }
            else { ShowBitmap(bitmap); }
        }

        private void ShowBitmap(Bitmap bitmap)
        {
            motionDetectionPictureBox.Image = bitmap;
        }

        public void SetMotionDetector(MotionDetector motionDetector)
        {
            this.motionDetector = motionDetector;
        }

        private void RunLoop()
        {
            while (running)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                Bitmap motionBitmap = motionDetector.MotionBitmap;
                if (motionBitmap != null)
                {
                    ThreadSafeShowBitmap(motionBitmap);
                }
                stopWatch.Stop();
                int elapsedMilliseconds = (int)stopWatch.ElapsedMilliseconds;
                if (elapsedMilliseconds < millisecondInterval)
                {
                    int sleepMilliseconds = millisecondInterval - elapsedMilliseconds;
                    Thread.Sleep(sleepMilliseconds);
                }

                /*
                try
                {
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    Bitmap motionBitmap = motionDetector.MotionBitmap;
                    if (motionBitmap != null)
                    {
                        ThreadSafeShowBitmap(motionBitmap);
                    }
                    stopWatch.Stop();
                    int elapsedMilliseconds = (int)stopWatch.ElapsedMilliseconds;
                    if (elapsedMilliseconds < millisecondInterval)
                    {
                        int sleepMilliseconds = millisecondInterval - elapsedMilliseconds;
                        Thread.Sleep(sleepMilliseconds);
                    }
                }
               catch
                {
                    Thread.Sleep(millisecondInterval);
                }  */
            }
            runThread = null;
        }

        public void Start()
        {
            if ((motionDetector != null) && (!running))
            {
                running = true;
                runThread = new Thread(new ThreadStart(RunLoop));
                runThread.Start();
            }
        }

        public void Stop()
        {
            running = false;
        }

        public int FrameRate
        {
            get { return frameRate; }
            set
            {
                if (!running)
                {
                    frameRate = value;
                    millisecondInterval = (int)Math.Round(1000 / (double)frameRate);
                }
            }
        }

        // Remove
     /*   public Bitmap Bitmap
        {
            get { return (Bitmap)motionDetectionPictureBox.Image; }
        }  */
    }
}
