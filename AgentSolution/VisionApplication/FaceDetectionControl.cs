using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AuxiliaryLibrary;
using ImageProcessingLibrary;
using ImageProcessingLibrary.Cameras;

namespace VisionApplication
{
    public partial class FaceDetectionControl : CameraViewControl
    {
        private FaceDetector faceDetector;
        private int faceLeft = -1;
        private int faceRight = -1;
        private int faceTop = -1;
        private int faceBottom = -1;
        private int faceCenterPosition = -1;
        private Boolean faceLockAcquired = false;
        private Bitmap faceBitmap = null;
        private Boolean drawBoundingBox = true;
        private Boolean showSkinPixelsOnly = false;
        private Boolean showCenterLine = true;
        private Boolean showBoundingBox = true;

        public FaceDetectionControl()
        {
            InitializeComponent();
        }

        public override void SetCamera(Camera camera)
        {
            base.SetCamera(camera);
            camera.CaptureDevice.SetVideoProperty(DirectShowLib.VideoProcAmpProperty.WhiteBalance, 2500);
            camera.CaptureDevice.SetVideoProperty(DirectShowLib.VideoProcAmpProperty.Brightness, 0);
            faceCenterPosition = -1;
            faceDetector = new FaceDetector();
            faceDetector.SetCamera(camera);
            faceDetector.FaceCenterPositionAvailable += new EventHandler<IntEventArgs>(HandleFaceCenterHorizontalPositionAvailable);
            faceDetector.FaceBoundingBoxAvailable += new EventHandler<FaceDetectionEventArgs>(HandleFaceBoundingBoxAvailable);
            faceDetector.Start();
        }

        private void HandleFaceCenterHorizontalPositionAvailable(object sender, IntEventArgs e)
        {
            faceCenterPosition = e.IntValue;
        }

        private void HandleFaceBoundingBoxAvailable(object sender, FaceDetectionEventArgs e)
        {
            faceLeft = e.Left;
            faceRight = e.Right;
            faceTop = e.Top;
            faceBottom = e.Bottom;
            faceLockAcquired = e.LockAcquired;
        }

        protected override Bitmap ProcessImage(Bitmap bitmap)
        {
            ImageProcessor faceDetectionProcessor = new ImageProcessor(bitmap);
            if (drawBoundingBox)
            {
                if (showSkinPixelsOnly) { faceDetectionProcessor.FindSkinPixelsRGB(); }
                if (showCenterLine)
                {
                    if ((faceCenterPosition >= 0) && (faceCenterPosition < bitmap.Width))
                    {
                        faceDetectionProcessor.DrawVerticalLine(faceCenterPosition, Color.Red);
                    }
                }
                if (showBoundingBox)
                {
                    if ((faceLeft >= 0) && (faceRight < bitmap.Width) && (faceTop >= 0) && (faceBottom < bitmap.Height))
                    {
                        if (!faceLockAcquired)
                        {
                            faceDetectionProcessor.DrawBox(faceLeft, faceRight, faceTop, faceBottom, Color.Red);
                        }
                        else
                        {
                            faceDetectionProcessor.DrawBox(faceLeft, faceRight, faceTop, faceBottom, Color.Lime);
                        }
                    }
                }
            }
            faceDetectionProcessor.Release();
            bitmap = faceDetectionProcessor.Bitmap;
            return bitmap;
        }

        public Boolean IsRunning()
        {
            if (running) { return true; }
            else { return false; }
        }

        public override void Stop()
        {
            base.Stop();
            if (faceDetector != null)
            {
                faceDetector.Stop();
            }
        }

        public FaceDetector FaceDetector
        {
            get { return faceDetector; }
        }

        public Boolean DrawDetection
        {
            get { return drawBoundingBox; }
            set { drawBoundingBox = value; }
        }

        public Boolean ShowSkinPixelsOnly
        {
            get { return showSkinPixelsOnly; }
            set { showSkinPixelsOnly = value; }
        }

        public Boolean ShowBoundingBox
        {
            get { return showBoundingBox; }
            set { showBoundingBox = value; }
        }

        public Boolean ShowCenterLine
        {
            get { return showCenterLine; }
            set { showCenterLine = value; }
        }
    }
}
