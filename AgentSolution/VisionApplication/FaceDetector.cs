using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AuxiliaryLibrary;
using ImageProcessingLibrary;
using ImageProcessingLibrary.Cameras;

namespace VisionApplication
{
    public class FaceDetector
    {
        private const double DEFAULT_TIME_STEP = 0.050;
        private const int DEFAULT_MILLISECOND_TIMEOUT = 25;
        
        private Camera camera = null;
        private Boolean running = false;
        private Thread processingThread = null;
        private double timeStep = DEFAULT_TIME_STEP;
        private int millisecondTimeout = DEFAULT_MILLISECOND_TIMEOUT;
        private int millisecondTimeStep;
        private Stopwatch stopwatch;
        private double faceCenterHorizontalPosition;
        private int numberOfCenterPositions = 5;
        private List<int> centerHorizontalPositionList = new List<int>();

        public event EventHandler<IntEventArgs> FaceCenterPositionAvailable = null;
        public event EventHandler<FaceDetectionEventArgs> FaceBoundingBoxAvailable = null;

        private static object lockObject = new object();

        public void SetCamera(Camera camera)
        {
            this.camera = camera;
            centerHorizontalPositionList = new List<int>();
            for (int jj = 0; jj < numberOfCenterPositions; jj++)
            {
                centerHorizontalPositionList.Add(0);
            }
        }

        private void OnFaceCenterPositionAvailable(int position)
        {
            if (FaceCenterPositionAvailable != null)
            {
                EventHandler<IntEventArgs> handler = FaceCenterPositionAvailable;
                IntEventArgs e = new IntEventArgs(position);
                handler(this, e);
            }
        }

        private void OnFaceBoundingBoxAvailable(int left, int right, int top, int bottom, Boolean lockAcquired)
        { 
            if (FaceBoundingBoxAvailable != null)
            {
                EventHandler<FaceDetectionEventArgs> handler = FaceBoundingBoxAvailable;
                FaceDetectionEventArgs e = new FaceDetectionEventArgs(left, right, top, bottom, lockAcquired);
                handler(this, e);
            }
        }

        private void ProcessBitmap(Bitmap bitmap)
        {
            if (bitmap != null)
            {
                ImageProcessor faceDetectionProcessor = new ImageProcessor(bitmap);
                faceDetectionProcessor.FindSkinPixelsRGB();
                faceDetectionProcessor.Binarize(1);
                double[,] imageMatrix = faceDetectionProcessor.AsGrayMatrix();
                List<Tuple<int, int>> horizontalPositionSkinPixelTupleList = new List<Tuple<int, int>>();
                List<int> horizontalSkinPixelList = new List<int>();
                for (int ii = 0; ii < bitmap.Width; ii++)
                {
                    int skinPixels = 0;
                    for (int jj = 0; jj < bitmap.Height; jj++)
                    {
                        if (imageMatrix[ii, jj] > 0) { skinPixels++; }
                    }
                //    if (skinPixels > 1)
                //    {
                        Tuple<int, int> horizontalPositionSkinPixelTuple = new Tuple<int, int>(ii, skinPixels);
                        horizontalPositionSkinPixelTupleList.Add(horizontalPositionSkinPixelTuple);
                        horizontalSkinPixelList.Add(skinPixels);
                 //   }
                }
                List<int> verticalSkinPixelList = new List<int>();
                for (int jj = 0; jj < bitmap.Height; jj++)
                {
                    int skinPixels = 0;
                    for (int ii = 0; ii < bitmap.Width; ii++)
                    {
                        if (imageMatrix[ii, jj] > 0) { skinPixels++; }
                    }
                    verticalSkinPixelList.Add(skinPixels);
                }
                if (horizontalPositionSkinPixelTupleList.Count > 0)
                {
                    horizontalPositionSkinPixelTupleList.Sort((a, b) => a.Item2.CompareTo(b.Item2));
                    horizontalPositionSkinPixelTupleList.Reverse();
                    faceCenterHorizontalPosition = 0;
                    double centerFraction = 0.8;
                    double maxValue = horizontalPositionSkinPixelTupleList[0].Item2;
                    int index = 0;
                    double thresholdValue = (int)(centerFraction * maxValue);
                    while (horizontalPositionSkinPixelTupleList[index].Item2 >= thresholdValue)
                    {
                        faceCenterHorizontalPosition += horizontalPositionSkinPixelTupleList[index].Item1;
                        index++;
                        if (index >= horizontalPositionSkinPixelTupleList.Count) { break; }
                    }
                    faceCenterHorizontalPosition /= index;
                    int faceCenterIntHorizontalPosition = (int)Math.Round(faceCenterHorizontalPosition);
                    double edgeFraction = 0.25;
                    double edgeThreshold = (int)(edgeFraction * maxValue);
                    int horizontalScanLineIndex = faceCenterIntHorizontalPosition;
                    double scanLineSkinPixels = horizontalSkinPixelList[faceCenterIntHorizontalPosition];
                    while (scanLineSkinPixels > edgeThreshold)
                    {
                        horizontalScanLineIndex++;
                        if (horizontalScanLineIndex >= horizontalSkinPixelList.Count) { break; }
                        scanLineSkinPixels = horizontalSkinPixelList[horizontalScanLineIndex];
                    }
                    int rightEdge = horizontalScanLineIndex;
                    if (rightEdge >= bitmap.Width) { rightEdge = bitmap.Width - 1; }
                    horizontalScanLineIndex = faceCenterIntHorizontalPosition;
                    scanLineSkinPixels = horizontalSkinPixelList[faceCenterIntHorizontalPosition];
                    while (scanLineSkinPixels > edgeThreshold)
                    {
                        horizontalScanLineIndex--;
                        if (horizontalScanLineIndex < 0) { break; }
                        scanLineSkinPixels = horizontalSkinPixelList[horizontalScanLineIndex];
                    }
                    int leftEdge = horizontalScanLineIndex;
                    if (leftEdge < 0) { leftEdge = 0; }

                    int maximumVerticalSkinPixels = verticalSkinPixelList.Max();
                    int maximumVerticalIndex = verticalSkinPixelList.IndexOf(maximumVerticalSkinPixels);
                    edgeThreshold = (int)(edgeFraction * maximumVerticalSkinPixels);
                    int verticalScanLineIndex = maximumVerticalIndex;
                    int verticalSkinPixels = verticalSkinPixelList[verticalScanLineIndex];
                    while (verticalSkinPixels > edgeThreshold)
                    {
                        verticalScanLineIndex--;
                        if (verticalScanLineIndex <= 0) { break; }
                        verticalSkinPixels = verticalSkinPixelList[verticalScanLineIndex];
                    }

                    /*   int verticalScanLineIndex = 0;
                       int verticalSkinPixels = verticalSkinPixelList[verticalScanLineIndex];
                       while (verticalSkinPixels  < edgeThreshold)
                       {
                           verticalScanLineIndex++;
                           if (verticalScanLineIndex >= verticalSkinPixelList.Count) { break; }
                           verticalSkinPixels = verticalSkinPixelList[verticalScanLineIndex];
                       }  */
                    int topEdge = verticalScanLineIndex;
                    int width = rightEdge - leftEdge;
                    int heightEstimate = (int)Math.Round(1.3 * width);
                    int bottomEdge = topEdge + heightEstimate;
                    if (bottomEdge >= bitmap.Height) { bottomEdge = bitmap.Height - 1; }
                    OnFaceCenterPositionAvailable(faceCenterIntHorizontalPosition);
                    
                    centerHorizontalPositionList.RemoveAt(0);
                    centerHorizontalPositionList.Add(faceCenterIntHorizontalPosition);
                    double min = centerHorizontalPositionList.Min();
                    double max = centerHorizontalPositionList.Max();
                    Boolean lockAcquired = false;
                    if (((faceCenterIntHorizontalPosition - min) < 10) && ((max - faceCenterIntHorizontalPosition) < 10))
                    {
                        lockAcquired = true;
                    }
                    OnFaceBoundingBoxAvailable(leftEdge, rightEdge, topEdge, bottomEdge, lockAcquired);
                }
                faceDetectionProcessor.Release();
            }
        }

        private void ProcessLoop()
        {
            while (running)
            {
                if (!camera.IsRunning()) { running = false; }
                stopwatch.Start();
                Bitmap bitmap = camera.GetBitmap();
                if (Monitor.TryEnter(lockObject, millisecondTimeout))
                {
                    ProcessBitmap(bitmap);
                    Monitor.Exit(lockObject);
                }
                else // Nothing to do here, 
                {

                }
                stopwatch.Stop();
                double elapsedSeconds = stopwatch.ElapsedTicks / (double)Stopwatch.Frequency;
                if (elapsedSeconds < timeStep)
                {
                    int sleepTime = (int)Math.Round(1000 * (timeStep - elapsedSeconds));
                    Thread.Sleep(sleepTime);
                }
                stopwatch.Reset();
            }
        }

        public void Start()
        {
            if (!running)
            {
                stopwatch = new Stopwatch();
                millisecondTimeStep = (int)Math.Round(1000 * timeStep);
                running = true;
                processingThread = new Thread(new ThreadStart(ProcessLoop));
                processingThread.Start();
            }
        }

        public void Stop()
        {
            running = false;
        }
    }
}
