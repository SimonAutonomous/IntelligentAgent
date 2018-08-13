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
    public class GaussianExponentialAveraging: MotionDetector
    {
        private const double DEFAULT_RELATIVE_THRESHOLD = 1.3;
        private const double DEFAULT_RHO = 0.025;
        private Boolean backgroundAvailable = false;
        private double relativeThreshold = DEFAULT_RELATIVE_THRESHOLD;
        private double rho = DEFAULT_RHO;
        private double[,] averageMatrix;
        private double[,] varianceMatrix;

        protected override void GetForeground(System.Drawing.Bitmap cameraBitmap)
        {
            Monitor.Enter(motionLockObject);
            if (!backgroundAvailable)
            {
                ImageProcessor backgroundProcessor = new ImageProcessor(cameraBitmap);
                backgroundProcessor.ConvertToStandardGrayscale();
                averageMatrix = backgroundProcessor.AsGrayMatrix();
                varianceMatrix = new double[cameraBitmap.Width,cameraBitmap.Height]; // Set to 0 here...
                for (int ii = 0; ii < cameraBitmap.Width; ii++)
                {
                    for (int jj = 0; jj < cameraBitmap.Height; jj++)
                    {
                        varianceMatrix[ii, jj] = 10;
                    }
                }
                backgroundProcessor.Release();
                ImageProcessor foregroundProcessor = new ImageProcessor(cameraBitmap);
                foregroundProcessor.Binarize(256); // Quick-and-dirty way to get a black bitmap..
                foregroundProcessor.Release();
                motionBitmap = foregroundProcessor.Bitmap;
                backgroundAvailable = true;
            }
            else
            {
                ImageProcessor foregroundProcessor = new ImageProcessor(cameraBitmap);
                foregroundProcessor.ConvertToStandardGrayscale();
                double[,] imageAsGrayMatrix = foregroundProcessor.AsGrayMatrix();
                byte[,] foregroundMatrix = new byte[cameraBitmap.Width, cameraBitmap.Height];
                for (int ii = 0; ii < cameraBitmap.Width; ii++)
                {
                    for (int jj = 0; jj < cameraBitmap.Height; jj++)
                    {
                        averageMatrix[ii,jj] = rho*imageAsGrayMatrix[ii,jj] + (1-rho)*averageMatrix[ii,jj];
                        double distance = Math.Abs(imageAsGrayMatrix[ii,jj]-averageMatrix[ii,jj]);
                        varianceMatrix[ii, jj] = rho * distance * distance + (1 - rho) * varianceMatrix[ii, jj];
                    }
                }
                for (int ii = 0; ii < cameraBitmap.Width; ii++)
                {
                    for (int jj = 0; jj < cameraBitmap.Height; jj++)
                    {
                        if (varianceMatrix[ii, jj] > double.Epsilon)
                        {
                            double distance = Math.Abs(imageAsGrayMatrix[ii, jj] - averageMatrix[ii, jj]);
                            double relativeDistance = distance / Math.Sqrt(varianceMatrix[ii, jj]);
                            if (relativeDistance > relativeThreshold)
                            {
                                foregroundMatrix[ii, jj] = 255;
                            }
                            else
                            {
                                foregroundMatrix[ii, jj] = 0;
                            }
                        }
                    }
                }
                foregroundProcessor.AssignFromBinarizedArray(foregroundMatrix);
                foregroundProcessor.Release();
                motionBitmap = foregroundProcessor.Bitmap;
            }
            Monitor.Exit(motionLockObject);
        }

        [DataMember]
        public double RelativeThreshold
        {
            get { return relativeThreshold; }
            set { relativeThreshold = value; }
        }

        [DataMember]
        public double Rho
        {
            get { return rho; }
            set { rho = value; }
        }
    }
}
