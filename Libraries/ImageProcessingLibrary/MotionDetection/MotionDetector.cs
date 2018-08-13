using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using ImageProcessingLibrary.Cameras;

namespace ImageProcessingLibrary.MotionDetection
{
    [DataContract]
    public abstract class MotionDetector
    {
        private const int DEFAULT_FRAME_RATE = 25;
        protected const int LOCK_ACQUISITION_MILLISECOND_TIMEOUT = 40;
        
        protected static object motionLockObject = new object();

        protected Bitmap motionBitmap;
        protected Camera camera;
        private Thread runThread;
        private Boolean running = false;
        private int frameRate = DEFAULT_FRAME_RATE;
        private int millisecondInterval;
        private Boolean bitmapAvailable; // => motion detection has started.

        public void SetCamera(Camera camera)
        {
            this.camera = camera;
            millisecondInterval = (int)Math.Round(1000 / (double)frameRate);
            bitmapAvailable = false;
            running = false;
        }

        protected abstract void GetForeground(Bitmap cameraBitmap);

        private void RunLoop()
        {
        //    Thread.Sleep(5000);   // Test, remove
            while (running)
            {
                if (camera != null)
                {
                    try
                    {
                        Stopwatch stopWatch = new Stopwatch();
                        stopWatch.Start();
                        Bitmap cameraBitmap = camera.GetBitmap(); //  TRIES to get the bitmap from the camera
                        if (cameraBitmap != null)
                        {
                            GetForeground(cameraBitmap);
                            if (!bitmapAvailable)
                            {
                                if (motionBitmap != null) { bitmapAvailable = true; }
                            }
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
                    }
                }
            }
            runThread.Abort();
            runThread = null;
        }

        public void Start()
        {
            if (!running)
            {
              //  randomNumberGenerator = new Random();
                runThread = new Thread(new ThreadStart(RunLoop));
                runThread.Start();
                running = true;
            }
        }

        public virtual void Stop()
        {
            if (running)
            {
                running = false;
            }
        }

        public Bitmap MotionBitmap
        {
            get
            {
                if (bitmapAvailable) // => at least one successful call to GetForeground() has been made.
                {
                    if (Monitor.TryEnter(motionLockObject, LOCK_ACQUISITION_MILLISECOND_TIMEOUT))
                    {
                        Bitmap accessedBitmap = new Bitmap(motionBitmap);
                        Monitor.Exit(motionLockObject);
                        return accessedBitmap;
                    }
                    else
                    {
                        return null;
                    }
                }
                else { return null; }
            }
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
    }
}
