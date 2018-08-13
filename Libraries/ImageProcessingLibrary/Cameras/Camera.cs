using DirectShowLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;

namespace ImageProcessingLibrary.Cameras
{
    [DataContract]
    public class Camera
    {
        #region Fields
        private string deviceName;
        private int imageWidth;
        private int imageHeight;
        private int frameRate;
        private Boolean connected = false;
        private CaptureDevice captureDevice;    
        private static object lockObject = new object();
        private static Mutex readMutex = new Mutex();
        private Mutex unsafeCodeMutex = null;

        private Bitmap cameraBitmap;
        private int millisecondSleepInterval;
        private Thread runThread;

        public event EventHandler CameraStarted;
        public event EventHandler CameraStopped; // 20161027

        #endregion

        #region Constants
        private const int MILLISECOND_TIMEOUT = 20;
        #endregion

        #region Constructor
        public Camera()
        {
            deviceName = "";
            connected = false;
        }
        #endregion

        #region Public methods
        public void OnCameraStarted()
        {
            if (CameraStarted != null)
            {
                EventHandler handler = CameraStarted;
                handler(this, EventArgs.Empty);
            }
        }

        public void OnCameraStopped()
        {
            if (CameraStopped != null)
            {
                EventHandler handler = CameraStopped;
                handler(this, EventArgs.Empty);
            }
        }

        public void Start()
        {
            if (!connected)
            {
                try
                {
                    TimerResolution.TimeBeginPeriod(1);
                    List<string> deviceNames = CaptureDevice.GetDeviceNames();
                    int deviceIndex = deviceNames.IndexOf(deviceName);
                    if (deviceIndex >= 0)
                    {
                        millisecondSleepInterval = (int)Math.Round(1000 / (double)this.frameRate);
                        List<CameraResolution> cr = CaptureDevice.GetAvailableResolutions(deviceIndex);
                        captureDevice = new CaptureDevice(deviceIndex, this.frameRate, this.ImageWidth, this.ImageHeight);
                        captureDevice.Start();
                        connected = true;
                        OnCameraStarted();
                    //    OnCameraStarted();
                   //     captureDevice.SetVideoProperty(VideoProcAmpProperty.Brightness, 200);
                    }
                }
                catch
                {
                    connected = false;
                }
            }
            if (connected)
            {
                runThread = new Thread(new ThreadStart(RunLoop));
                runThread.Start();
            }
        }

        private Bitmap ReadFromCaptureDevice(CaptureDevice captureDevice)
        {
            readMutex.WaitOne();
            Bitmap bitmap = null;
            try
            {
                IntPtr imagePointer = captureDevice.GetBitMap();
                int width = captureDevice.Width;
                int height = captureDevice.Height;
                int stride = captureDevice.Stride;
                bitmap = new Bitmap(width, height, stride, PixelFormat.Format24bppRgb, imagePointer);
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                Marshal.FreeCoTaskMem(imagePointer);
            }
            catch
            {
                string errorString = "Failed to read image";
            }
            readMutex.ReleaseMutex();
            return bitmap;
        }

        public void Read() // Bitmap Read()
        {
            if (!connected)
            {
                return; // null;
            }
            cameraBitmap = ReadFromCaptureDevice(captureDevice);
        }

        public void RunLoop()
        {
            while (connected)
            {
                Read();
                if (unsafeCodeMutex != null) { unsafeCodeMutex.WaitOne(); }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (unsafeCodeMutex != null) { unsafeCodeMutex.ReleaseMutex(); }
                Thread.Sleep(millisecondSleepInterval);
            }
            captureDevice.Dispose();
            TimerResolution.TimeEndPeriod(1);
            OnCameraStopped();
        }

        public Boolean IsRunning()
        {
            return connected;
        }

        public Bitmap GetBitmap()
        {
            readMutex.WaitOne();
            Bitmap currentBitmap = null;
            if (cameraBitmap != null)
            {
                //  currentBitmap = new Bitmap(cameraBitmap.Width, cameraBitmap.Height, cameraBitmap.PixelFormat);
                currentBitmap = (Bitmap)cameraBitmap.Clone();
             //  currentBitmap = new Bitmap(cameraBitmap);
            }
            readMutex.ReleaseMutex();
            return currentBitmap;
        }

    /*    public Bitmap Read()
        {
            if (!connected) { return null; }
            Monitor.Enter(lockObject);
            Bitmap bitmap = null;
            try
            {
                IntPtr imagePointer = captureDevice.GetBitMap();
                int width = captureDevice.Width;
                int height = captureDevice.Height;
                int stride = captureDevice.Stride;
                bitmap = new Bitmap(width, height, stride, PixelFormat.Format24bppRgb, imagePointer);
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                Marshal.FreeCoTaskMem(imagePointer);
                bitmap = new Bitmap(bitmap);
            }
            catch
            {
            }
            Monitor.Exit(lockObject);
            return bitmap;
        }

        // For low-priority readings, e.g. visualizations
        public Bitmap TryRead()
        {
            if (!connected) { return null; }
            if (Monitor.TryEnter(lockObject, MILLISECOND_TIMEOUT))
            {
                Bitmap bitmap = null;
                try
                {
                    IntPtr imagePointer = captureDevice.GetBitMap();
                    int width = captureDevice.Width;
                    int height = captureDevice.Height;
                    int stride = captureDevice.Stride;
                    bitmap = new Bitmap(width, height, stride, PixelFormat.Format24bppRgb, imagePointer);
                    bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    Marshal.FreeCoTaskMem(imagePointer);
                    bitmap = new Bitmap(bitmap);
                }
                catch
                {
                }
                Monitor.Exit(lockObject);
                return bitmap;
            }
            else { return null; }
        }  */

        public void SetBrightness(int brightnessValue)
        {
            this.CaptureDevice.SetVideoProperty(VideoProcAmpProperty.Brightness, brightnessValue);
        }

        public void Stop()
        {
            if (connected)
            {
                connected = false;
          //      captureDevice.Dispose();
          //      TimerResolution.TimeEndPeriod(1);
            }
          //  OnCameraStopped();   // Move this to the end of the RunLoop() instead. 20171207
        }

        public void SetUnsafeCodeMutex(Mutex unsafeCodeMutex)
        {
            this.unsafeCodeMutex = unsafeCodeMutex;
        }
        #endregion

        #region Static public methods
        public static List<string> GetDeviceNames()
        {
            return CaptureDevice.GetDeviceNames();
        }
        #endregion

        #region Public properties
        [DataMember]
        public string DeviceName
        {
            get { return deviceName; }
            set { deviceName = value; }
        }

        [DataMember]
        public int ImageWidth
        {
            get { return imageWidth; }
            set { imageWidth = value; }
        }

        [DataMember]
        public int ImageHeight
        {
            get { return imageHeight; }
            set { imageHeight = value; }
        }

        [DataMember]
        public int FrameRate
        {
            get { return frameRate; }
            set { frameRate = value; }
        }

        public CaptureDevice CaptureDevice
        {
            get { return captureDevice; }
        }


        public Boolean Connected
        {
            get { return connected; }
        }
        #endregion
    }
}

