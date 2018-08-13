using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace ImageProcessingLibrary.MotionDetection
{
    [DataContract]
    public class FrameDifferencing: MotionDetector
    {
        private const int DEFAULT_THRESHOLD = 100;
        private Boolean backgroundAvailable = false;
        private Bitmap backgroundBitmap = null;
        private int threshold = DEFAULT_THRESHOLD;
        private Boolean useGrayscale = false;

        protected override void GetForeground(Bitmap cameraBitmap)
        {
            Monitor.Enter(motionLockObject);
            if (!backgroundAvailable)
            {
                ImageProcessor backgroundProcessor = new ImageProcessor(cameraBitmap);
                if (useGrayscale) { backgroundProcessor.ConvertToStandardGrayscale(); }
                backgroundProcessor.Release();
                backgroundBitmap = backgroundProcessor.Bitmap;
                ImageProcessor foregroundProcessor = new ImageProcessor(cameraBitmap);
                foregroundProcessor.ConvertToStandardGrayscale();
                foregroundProcessor.Binarize(256); // Quick-and-dirty way to get a black bitmap..
                foregroundProcessor.Release();
                motionBitmap = foregroundProcessor.Bitmap;
                backgroundAvailable = true;
            }
            else
            {
                ImageProcessor foregroundProcessor = new ImageProcessor(cameraBitmap);
                foregroundProcessor.ComputeDifferenceImage(backgroundBitmap, threshold, useGrayscale);
                foregroundProcessor.Release();
                motionBitmap = foregroundProcessor.Bitmap;
            }
            Monitor.Exit(motionLockObject);
        }

        public override void Stop()
        {
            base.Stop();
            backgroundAvailable = false;
        }
    }
}
