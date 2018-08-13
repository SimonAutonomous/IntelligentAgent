using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageProcessingLibrary
{
    public class ImageProcessor: IDisposable
    {
        #region Fields
        protected Bitmap bitmap;
        protected BitmapData bitmapData = null;
        protected Boolean isLocked = false;
        #endregion

        #region Constants
        protected const byte MAXIMUM_PIXEL_VALUE = 255;
        protected const int PIXEL_SIZE = 3;
        #endregion

        #region Constructors
        public ImageProcessor(Bitmap bitmap)
        {
            this.bitmap = (Bitmap)bitmap.Clone(); //  20171109: Preserves pixelFormat.  new Bitmap(bitmap); 
            Lock();
        }
        #endregion

        #region Private methods
        private void AssignBitmap(Bitmap bitmap)
        {
            if (isLocked) { Release(); }
            this.bitmap = new Bitmap(bitmap);
        }

        private void ConvolveStripe(List<List<double>> convolutionMask, int offset, ImageProcessor outputProcessor)
        {
            int stripeHeight = convolutionMask.Count;
            int matrixSize = convolutionMask.Count;
            int halfSize = (convolutionMask.Count - 1) / 2;
            int numberOfStripes = (int)Math.Round(bitmap.Height / (double)stripeHeight) + 1;
            int width = bitmap.Width;
            int height = bitmap.Height;
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;
                byte* outputPtrFirstPixel = (byte*)outputProcessor.bitmapData.Scan0;
                Parallel.For(0, numberOfStripes, iStripe =>
                {
                    int y = iStripe * stripeHeight + offset;
                    if ((y >= halfSize) && (y < height - halfSize))
                    {
                        for (int x = halfSize; x < width - halfSize; x++)
                        {
                            double newRedDouble = 0;
                            double newGreenDouble = 0;
                            double newBlueDouble = 0;
                            for (int yMask = 0; yMask < matrixSize; yMask++)
                            {
                                byte* currentRow = ptrFirstPixel + ((y - halfSize + yMask) * bitmapData.Stride);
                                for (int xMask = 0; xMask < matrixSize; xMask++)
                                {
                                    int xByteIndexRed = (x - halfSize + xMask) * bytesPerPixel + 2; // Red
                                    newRedDouble += currentRow[xByteIndexRed] * convolutionMask[xMask][yMask];
                                    int xByteIndexGreen = xByteIndexRed - 1;
                                    newGreenDouble += currentRow[xByteIndexGreen] * convolutionMask[xMask][yMask];
                                    int xByteIndexBlue = xByteIndexGreen - 1;
                                    newBlueDouble += currentRow[xByteIndexBlue] * convolutionMask[xMask][yMask];
                                }
                            }
                            byte newRed;
                            if (newRedDouble > 255) { newRed = 255; }
                            else if (newRedDouble < 0) { newRed = 0; }
                            else { newRed = (byte)Math.Round(newRedDouble); }
                            byte newGreen;
                            if (newGreenDouble > 255) { newGreen = 255; }
                            else if (newGreenDouble < 0) { newGreen = 0; }
                            else { newGreen = (byte)Math.Round(newGreenDouble); }
                            byte newBlue;
                            if (newBlueDouble > 255) { newBlue = 255; }
                            else if (newBlueDouble < 0) { newBlue = 0; }
                            else { newBlue = (byte)Math.Round(newBlueDouble); }
                            byte* outputCurrentRow = outputPtrFirstPixel + (y * outputProcessor.bitmapData.Stride);
                            int outputXIndex = x * bytesPerPixel;
                            outputCurrentRow[outputXIndex + 2] = newRed;
                            outputCurrentRow[outputXIndex + 1] = newGreen;
                            outputCurrentRow[outputXIndex] = newBlue;
                        }
                    }
                });
            }
        }

        // Note: This method assumes that the image is in grayscale!
        private void EdgeDetectSobelStripe(int offset, ImageProcessor outputProcessor)
        {
            int stripeHeight = 3;
            int halfSize = 1;
            int numberOfStripes = (int)Math.Round(bitmap.Height / (double)stripeHeight) + 1;
            int width = bitmap.Width;
            int height = bitmap.Height;
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;
                byte* outputPtrFirstPixel = (byte*)outputProcessor.bitmapData.Scan0;
                Parallel.For(0, numberOfStripes, iStripe =>
                {
                    int y = iStripe * stripeHeight + offset;
                    if ((y >= halfSize) && (y < height - halfSize))
                    {
                        for (int x = halfSize; x < width - halfSize; x++)
                        {
                            double newGrayDouble;
                            byte* previousRow = ptrFirstPixel + ((y - 1) * bitmapData.Stride);
                            byte* currentRow = ptrFirstPixel + (y * bitmapData.Stride);
                            byte* nextRow = ptrFirstPixel + ((y + 1) * bitmapData.Stride);
                            // Grayscale image assumed: use blue channel
                            int xByteLeft = (x - 1) * bytesPerPixel;
                            int xByteCenter = x * bytesPerPixel;
                            int xByteRight = (x + 1) * bytesPerPixel;
                            byte p11 = previousRow[xByteLeft];
                            byte p12 = previousRow[xByteCenter];
                            byte p13 = previousRow[xByteRight];
                            byte p21 = currentRow[xByteLeft];
                            byte p22 = currentRow[xByteCenter];
                            byte p23 = currentRow[xByteRight];
                            byte p31 = currentRow[xByteLeft];
                            byte p32 = currentRow[xByteCenter];
                            byte p33 = currentRow[xByteRight];
                            byte* outputCurrentRow = outputPtrFirstPixel + (y * outputProcessor.bitmapData.Stride);
                            int xByteOutput = xByteCenter;
                            newGrayDouble = Math.Abs((p11 + 2 * p12 + p13) - (p31 + 2 * p32 + p33)) +
                                            Math.Abs((p13 + 2 * p23 + p33) - (p11 + 2 * p21 + p31));
                            byte newGray;
                            if (newGrayDouble > 255) { newGray = 255; }
                            else if (newGrayDouble < 0) { newGray = 0; }
                            else { newGray = (byte)Math.Round(newGrayDouble); }
                            outputCurrentRow[xByteOutput] = newGray;
                            outputCurrentRow[xByteOutput + 1] = newGray;
                            outputCurrentRow[xByteOutput + 2] = newGray;
                        }
                    }
                });
            }
        }

        private void Lock()
        {
            bitmapData = this.bitmap.LockBits(
            new Rectangle(0, 0, this.bitmap.Width, this.bitmap.Height),
            ImageLockMode.ReadWrite, this.bitmap.PixelFormat); // PixelFormat.Format32bppRgb);  // perhaps modify (use any pixelformat). Comment MW 20160412
            isLocked = true;
        }
        #endregion

        #region Public methods
        public void Release()
        {
            if (isLocked)
            {
                bitmap.UnlockBits(bitmapData);
                isLocked = false;
            }
        }

        public void Dispose()
        {
            this.bitmap.Dispose();
            this.bitmapData = null;
        }

        public void ConvertToStandardGrayscale()
        {
            ConvertToGrayscale(0.299, 0.587, 0.114);
        }

        public void ConvertToGrayscale(double rFraction, double gFraction, double bFraction)
        {
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, bitmapData.Height, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int oldBlue = currentLine[x];
                        int oldGreen = currentLine[x + 1];
                        int oldRed = currentLine[x + 2];
                        byte grayValue = (byte)Math.Round(rFraction * oldRed + gFraction * oldGreen + bFraction * oldBlue);
                        currentLine[x] = grayValue;
                        currentLine[x + 1] = grayValue;
                        currentLine[x + 2] = grayValue;
                    }
                });
            }
        }

        // NOTE: This method assumes that the image is in grayscale
        public void Binarize(double binarizationThreshold)
        {
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, bitmapData.Height, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int oldGray = currentLine[x];
                        byte newGray = 0;
                        if (oldGray >= binarizationThreshold) { newGray = 255; }
                        currentLine[x] = newGray;
                        currentLine[x + 1] = newGray;
                        currentLine[x + 2] = newGray;
                    }
                });
            }
        }

        public void Sharpen3x3(double sharpeningFactor)
        {
            double centerElement = 1 + sharpeningFactor;
            double peripheralElement = -sharpeningFactor / 8;
            List<List<double>> convolutionMask = new List<List<double>>();
            convolutionMask.Add(new List<double>() { peripheralElement, peripheralElement, peripheralElement });
            convolutionMask.Add(new List<double>() { peripheralElement, centerElement, peripheralElement });
            convolutionMask.Add(new List<double>() { peripheralElement, peripheralElement, peripheralElement });
            Convolve(convolutionMask);
        }

        public void BoxBlur3x3()
        {
            double element = 1.0 / 9.0;
            List<List<double>> convolutionMask = new List<List<double>>();
            convolutionMask.Add(new List<double>() { element, element, element });
            convolutionMask.Add(new List<double>() { element, element, element });
            convolutionMask.Add(new List<double>() { element, element, element });
            Convolve(convolutionMask);
        }

        public void GaussianBlur3x3()
        {
            double centerElement = 1.0 / 4.0;
            List<List<double>> convolutionMask = new List<List<double>>();
            convolutionMask.Add(new List<double>() { centerElement/4, centerElement/2, centerElement/4 });
            convolutionMask.Add(new List<double>() { centerElement/2, centerElement, centerElement/2 });
            convolutionMask.Add(new List<double>() { centerElement/4, centerElement/2, centerElement/4 });
            Convolve(convolutionMask);
        }

        // Note, this method requires a NxN convolutionMask, where
        // N >= 3 is an odd number.
        public void Convolve(List<List<double>> convolutionMask)
        {
            Release();
            ImageProcessor outputProcessor = new ImageProcessor(bitmap);
            Lock();
            for (int offset = 0; offset < convolutionMask.Count; offset++)
            {
                ConvolveStripe(convolutionMask, offset, outputProcessor);
            }
            outputProcessor.Release();
            Bitmap processedBitmap = outputProcessor.Bitmap;
            outputProcessor.Dispose();
            AssignBitmap(processedBitmap);
            Lock();
        }

        // Note: This method assumes that the image is in grayscale!
        public void EdgeDetectSobel()
        {
            Release();
            ImageProcessor outputProcessor = new ImageProcessor(bitmap);
            Lock();
            for (int offset = 0; offset < 3; offset++)
            {
                EdgeDetectSobelStripe(offset, outputProcessor);
            }
            outputProcessor.Release();
            Bitmap processedBitmap = outputProcessor.Bitmap;
            outputProcessor.Dispose();
            AssignBitmap(processedBitmap);
            Lock();
        }

        public void ChangeContrast(double alpha)
        {
            unsafe
            {
                int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, bitmapData.Height, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        double oldBlue = currentLine[x];
                        double oldGreen = currentLine[x + 1];
                        double oldRed = currentLine[x + 2];
                        int newBlue = (int)Math.Round(128 +(oldBlue-128) * alpha);
                        int newGreen = (int)Math.Round(128 + (oldGreen-128) * alpha);
                        int newRed = (int)Math.Round(128 + (oldRed-128) * alpha);
                        if (newBlue < 0) { newBlue = 0; }
                        else if (newBlue > 255) { newBlue = 255; }
                        if (newGreen < 0) { newGreen = 0; }
                        else if (newGreen > 255) { newGreen = 255; }
                        if (newRed < 0) { newRed = 0; }
                        else if (newRed > 255) { newRed = 255; }
                        currentLine[x] = (byte)newBlue;
                        currentLine[x + 1] = (byte)newGreen;
                        currentLine[x + 2] = (byte)newRed;
                    }
                });
            }
        }

        public ImageHistogram GenerateHistogram(ColorChannel colorChannel)
        {
            ImageHistogram imageHistogram = new ImageHistogram(colorChannel);
            object lockObject = new object();
            int[] pixelNumberArray = new int[256];
            unsafe
            {
                int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, bitmapData.Height, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        byte pixelValue = 0;
                        if (colorChannel == ColorChannel.Red) { pixelValue = currentLine[x + 2]; }
                        else if (colorChannel == ColorChannel.Green) { pixelValue = currentLine[x + 1]; }
                        else { pixelValue = currentLine[x]; }  // Arbitrarily use the blue channel for grayscale histograms
                      /*  lock(lockObject)
                        {
                            pixelNumberArray[(int)pixelValue]++;
                        }    */
                       Interlocked.Increment(ref pixelNumberArray[(int)pixelValue]);
                    }
                });
            }
            imageHistogram.PixelNumberList = pixelNumberArray.ToList();
            return imageHistogram;
        }

        public void ChangeBrightness(double relativeBrightness)
        {
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;
                int beta = (int)Math.Round((relativeBrightness-1) * 255);

                Parallel.For(0, bitmapData.Height, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        double oldBlue = currentLine[x];
                        double oldGreen = currentLine[x + 1];
                        double oldRed = currentLine[x + 2];
                        int newBlue = (int)Math.Round(oldBlue + beta);
                        int newGreen = (int)Math.Round(oldGreen + beta);
                        int newRed = (int)Math.Round(oldRed + beta);
                        if (newBlue < 0) { newBlue = 0; }
                        else if (newBlue > 255) { newBlue = 255; }
                        if (newGreen < 0) { newGreen = 0; }
                        else if (newGreen > 255) { newGreen = 255; }
                        if (newRed < 0) { newRed = 0; }
                        else if (newRed > 255) { newRed = 255; }
                        currentLine[x] = (byte)newBlue;
                        currentLine[x + 1] = (byte)newGreen;
                        currentLine[x + 2] = (byte)newRed;
                    }
                });
            }
        }

        // Assumes that the image is in gray scale (uses the blue channel)
        public List<List<int>> GetGrayIntegralImage()
        {
            List<List<int>> integralImage = new List<List<int>>();
            for (int ii = 0; ii < bitmap.Width; ii++)
            {
                List<int> integralImageColumn = new List<int>();
                for (int jj = 0; jj < bitmap.Height; jj++)
                {
                    integralImageColumn.Add(0);
                }
                integralImage.Add(integralImageColumn);
            }
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;
                for (int jj = 0; jj < bitmap.Height; jj++)
                {
                    byte* currentRow = (byte*)bitmapData.Scan0 + (jj * bitmapData.Stride);
                    int ii = 0;
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        byte color = currentRow[x];  // Use blue channel (any will do (binarized))
                        integralImage[ii][jj] = color;
                        if (ii > 0) { integralImage[ii][jj] += integralImage[ii - 1][jj]; }
                        if (jj > 0) { integralImage[ii][jj] += integralImage[ii][jj - 1]; }
                        if ((ii > 0) && (jj > 0)) { integralImage[ii][jj] -= integralImage[ii - 1][jj - 1]; }
                        ii++;
                    }
                }
            }
            return integralImage;
        }

        // Assumes that the image has first been binarized.
        // In the resulting integral image, black pixels are set to 0,
        // and white pixels to (note!) 1.
        public List<List<int>> GetBinarizedIntegralImage()
        {
            List<List<int>> integralImage = new List<List<int>>();
            for (int ii = 0; ii < bitmap.Width; ii++)
            {
                List<int> integralImageColumn = new List<int>();
                for (int jj = 0; jj < bitmap.Height; jj++)
                {
                    integralImageColumn.Add(0);
                }
                integralImage.Add(integralImageColumn);
            }
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                // ZZZ Test (remove) (See Chapter 4) -ZZZ
            /*    for (int jj = 0; jj < bitmap.Height; jj++)
                {
                    byte* currentRow = (byte*)bitmapData.Scan0 + (jj * bitmapData.Stride);
                    int index = 0;
                    for (int ii = 0; ii < widthInBytes; ii = ii + bytesPerPixel)
                    {
                        if ((index == 1) && (jj == 0)) { currentRow[ii] = 255; }
                        else if ((index == 2) && (jj == 0)) { currentRow[ii] = 255; }
                        else if ((index == 0) && (jj == 1)) { currentRow[ii] = 255; }
                        else if ((index == 2) && (jj == 1)) { currentRow[ii] = 255; }
                        else if ((index == 3) && (jj == 1)) { currentRow[ii] = 255; }
                        else if ((index == 4) && (jj == 1)) { currentRow[ii] = 255; }
                        else if ((index == 0) && (jj == 2)) { currentRow[ii] = 255; }
                        else if ((index == 1) && (jj == 2)) { currentRow[ii] = 255; }
                        else if ((index == 2) && (jj == 2)) { currentRow[ii] = 255; }
                        else if ((index == 1) && (jj == 3)) { currentRow[ii] = 255; }
                        else if ((index == 2) && (jj == 3)) { currentRow[ii] = 255; }
                        else if ((index == 3) && (jj == 3)) { currentRow[ii] = 255; }
                        else if ((index == 4) && (jj == 3)) { currentRow[ii] = 255; }
                        else if ((index == 0) && (jj == 4)) { currentRow[ii] = 255; }
                        else if ((index == 2) && (jj == 4)) { currentRow[ii] = 255; }
                        else if ((index == 4) && (jj == 4)) { currentRow[ii] = 255; }
                        else { currentRow[ii] = 0; }
                        index++;
                    }
                }  */


                for (int jj = 0; jj < bitmap.Height; jj++)
                {
                    byte* currentRow = (byte*)bitmapData.Scan0 + (jj * bitmapData.Stride);
                    int ii = 0;
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        byte color = currentRow[x];  // Use blue channel (any will do (binarized))
                        if (color == 0) { integralImage[ii][jj] = 0; }
                        else { integralImage[ii][jj] = 1; }
                        if (ii > 0) { integralImage[ii][jj] += integralImage[ii - 1][jj]; }
                        if (jj > 0) { integralImage[ii][jj] += integralImage[ii][jj - 1]; }
                        if ((ii > 0) && (jj > 0)) { integralImage[ii][jj] -= integralImage[ii - 1][jj - 1]; }
                        ii++;
                    }
                }
            }
            return integralImage;
        }

        // 20161104 (assumes grayscale and equally sized bitmaps
        public void ComputeDifferenceImage(Bitmap backgroundBitmap, int threshold, Boolean grayScale)
        {
            unsafe
            {
                BitmapData backgroundBitmapData = backgroundBitmap.LockBits(
                new Rectangle(0, 0, backgroundBitmap.Width, backgroundBitmap.Height),
                ImageLockMode.ReadOnly, backgroundBitmap.PixelFormat);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;
                byte* backgroundPtrFirstPixel = (byte*)backgroundBitmapData.Scan0;

                Parallel.For(0, bitmapData.Height, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    byte* backgroundCurrentLine = backgroundPtrFirstPixel + (y * backgroundBitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        Boolean isForeground = false;
                        if (grayScale)
                        {
                            double gray = currentLine[x];
                            double backgroundGray = backgroundCurrentLine[x];
                            if (Math.Abs(gray - backgroundGray) > threshold)
                            {
                                isForeground = true;
                            }
                        }
                        else
                        {
                            double blue = currentLine[x];
                            double green = currentLine[x + 1];
                            double red = currentLine[x + 2];
                            double backgroundBlue = backgroundCurrentLine[x];
                            double backgroundGreen = backgroundCurrentLine[x + 1];
                            double backgroundRed = backgroundCurrentLine[x + 2];
                            double distance = Math.Sqrt((red - backgroundRed)*(red-backgroundRed) + (blue - backgroundBlue) * (blue - backgroundBlue) +
                                (green - backgroundGreen) * (green - backgroundGreen));
                            if (distance > threshold) { isForeground = true; }
                        }
                        if (!isForeground)
                        {
                            currentLine[x] = 0;
                            currentLine[x + 1] = 0;
                            currentLine[x + 2] = 0;
                        }
                        else
                        {
                            currentLine[x] = 255;
                            currentLine[x + 1] = 255;
                            currentLine[x + 2] = 255;
                        }
                         /*   ;
                        }
                        else
                        {

                        }  */
                    }
                });
                backgroundBitmap.UnlockBits(backgroundBitmapData);
            }
        }

        // 20161104
        public double[,] AsGrayMatrix()
        {
            double[,] imageAsGrayMatrix = new double[bitmap.Width, bitmap.Height];
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;
                Parallel.For(0, bitmapData.Height, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    int iX = 0;
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        imageAsGrayMatrix[iX, y] = (double)currentLine[x];
                        iX++;
                    }
                });
            }
            return imageAsGrayMatrix;
        }

        // 20161104
        public void AssignFromBinarizedArray(byte[,] imageAsArray)
        {
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;
                Parallel.For(0, bitmapData.Height, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    int iX = 0;
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        currentLine[x] = imageAsArray[iX, y];
                        currentLine[x + 1] = imageAsArray[iX, y];
                        currentLine[x + 2] = imageAsArray[iX, y];
                        iX++;
                    }
                });
            }
        }

        // 
        public void ConvertToLuma(int cb, int cr)
        {
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, bitmapData.Height, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int oldBlue = currentLine[x];
                        int oldGreen = currentLine[x + 1];
                        int oldRed = currentLine[x + 2];
                        byte luma = (byte)(Math.Round(16 + 0.25679 * oldRed + 0.50413 * oldGreen + 0.09791 * oldBlue));
                      //  byte cb = (byte)(Math.Round(128 - 0.14822*oldRed - 0.29099*oldGreen + 0.43922*oldBlue));
                      //  byte cr = (byte)(Math.Round(128 + 0.43922 * oldRed - 0.36799 * oldGreen - 0.07143 * oldBlue));
                        byte newRed = (byte)Math.Round(1.16438 * (luma-16) + 1.59603*(cr- 128));
                        byte newGreen = (byte)Math.Round(1.16438 * (luma-16) -0.39176*(cb - 128) - 0.81297*(cr-128));
                        byte newBlue = (byte)Math.Round(1.16438 * (luma - 16) + 2.01723 * (cb - 128));
                        currentLine[x] = newBlue;
                        currentLine[x + 1] = newGreen;
                        currentLine[x + 2] = newRed;
                    }
                });
            }
        }

        public void ConvertToCb(int luma, int cr)
        {
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, bitmapData.Height, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int oldBlue = currentLine[x];
                        int oldGreen = currentLine[x + 1];
                        int oldRed = currentLine[x + 2];
                      //  byte luma = (byte)(Math.Round(16 + 0.25679 * oldRed + 0.50413 * oldGreen + 0.09791 * oldBlue));
                        byte cb = (byte)(Math.Round(128 - 0.14822*oldRed - 0.29099*oldGreen + 0.43922*oldBlue));
                        //  byte cr = (byte)(Math.Round(128 + 0.43922 * oldRed - 0.36799 * oldGreen - 0.07143 * oldBlue));
                        byte newRed = (byte)Math.Round(1.16438 * (luma - 16) + 1.59603 * (cr - 128));
                        byte newGreen = (byte)Math.Round(1.16438 * (luma - 16) - 0.39176 * (cb - 128) - 0.81297 * (cr - 128));
                        byte newBlue = (byte)Math.Round(1.16438 * (luma - 16) + 2.01723 * (cb - 128));
                        currentLine[x] = newBlue;
                        currentLine[x + 1] = newGreen;
                        currentLine[x + 2] = newRed;
                    }
                });
            }
        }

        public void ConvertToCr(int luma, int cb)
        {
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, bitmapData.Height, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int oldBlue = currentLine[x];
                        int oldGreen = currentLine[x + 1];
                        int oldRed = currentLine[x + 2];
                        //  byte luma = (byte)(Math.Round(16 + 0.25679 * oldRed + 0.50413 * oldGreen + 0.09791 * oldBlue));
                       // byte cb = (byte)(Math.Round(128 - 0.14822 * oldRed - 0.29099 * oldGreen + 0.43922 * oldBlue));
                        byte cr = (byte)(Math.Round(128 + 0.43922 * oldRed - 0.36799 * oldGreen - 0.07143 * oldBlue));
                        byte newRed = (byte)Math.Round(1.16438 * (luma - 16) + 1.59603 * (cr - 128));
                        byte newGreen = (byte)Math.Round(1.16438 * (luma - 16) - 0.39176 * (cb - 128) - 0.81297 * (cr - 128));
                        byte newBlue = (byte)Math.Round(1.16438 * (luma - 16) + 2.01723 * (cb - 128));
                        currentLine[x] = newBlue;
                        currentLine[x + 1] = newGreen;
                        currentLine[x + 2] = newRed;
                    }
                });
            }
        }

        // assumes a gray scale image
        public void StretchHistogramGray(double limitFraction)
        {
            ImageHistogram histogram = GenerateHistogram(ColorChannel.Blue); // Pick an arbitrary color channel here (all should be equal)
            histogram.MakeFractional();
            histogram.MakeCumulative();
            int startIndex = histogram.CumulativePixelFractionList.FindIndex(t => t > limitFraction);
            int endIndex = histogram.CumulativePixelFractionList.FindLastIndex(t => t < (1-limitFraction));
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, bitmapData.Height, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int gray = currentLine[x];
                        byte newGray;
                        if (gray < startIndex) { newGray = 0; }
                        else if (gray > endIndex) { newGray = 255; }
                        else // Stretch
                        {
                            newGray = (byte)Math.Round(255 * (gray - startIndex) / (double)(endIndex - startIndex));
                        }
                        currentLine[x] = newGray;
                        currentLine[x + 1] = newGray;
                        currentLine[x + 2] = newGray;
                    }
                });
            }
        }

        // Same as histogram stretching, but carried out channel by channel.
        public void StretchHistogram(double limitFraction, ColorChannel colorChannel)
        {
            int channelIndex = 0;
            if (colorChannel == ColorChannel.Red) { channelIndex = 2; }
            else if (colorChannel == ColorChannel.Green) { channelIndex = 1; }
            else if (colorChannel == ColorChannel.Blue) { channelIndex = 0; }
            else { return; } // Do nothing if the user incorrectly requests the gray channel (See stretchHistogramGray above).
            ImageHistogram histogram = GenerateHistogram(colorChannel);
            histogram.MakeFractional();
            histogram.MakeCumulative();
            int startIndex = histogram.CumulativePixelFractionList.FindIndex(t => t > limitFraction);
            int endIndex = histogram.CumulativePixelFractionList.FindLastIndex(t => t < (1 - limitFraction));
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, bitmapData.Height, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int colorComponent = currentLine[x+channelIndex];
                        byte newColorComponent;
                        if (colorComponent < startIndex) { newColorComponent = 0; }
                        else if (colorComponent > endIndex) { newColorComponent = 255; }
                        else // Stretch
                        {
                            newColorComponent = (byte)Math.Round(255 * (colorComponent - startIndex) / (double)(endIndex - startIndex));
                        }
                        currentLine[x + channelIndex] = newColorComponent;
                    }
                });
            }
        }

        // perhaps include?
        // assumes a gray scale image
        // Matrix size should be an ODD integer, larger than 1
        public void SauvolaBinarize(int matrixSize, double k, double r)
        {
            if ((matrixSize % 2 != 0) && (matrixSize > 1))
            {
                int halfSize = (matrixSize - 1) / 2; // Integer division. matrixSize - 1 divisible by 2.
                byte[,] newGray = new byte[bitmap.Width, bitmap.Height];
                double maxSTD = 0;
                unsafe
                {
                    int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                    int widthInBytes = bitmapData.Width * bytesPerPixel;
                    byte* PtrFirstPixel = (byte*)bitmapData.Scan0;
                    // Cannot (easily) use Parallel.For here..
                    for (int y = halfSize; y < bitmapData.Height - halfSize - 1; y++)
                    {
                         byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                         int xStart = halfSize*bytesPerPixel;
                         int xEnd = widthInBytes - (halfSize * bytesPerPixel);  // A bit ugly, since x runs to just BEFORE xEnd, but OK...)
                         int xIndex = 0;
                         for (int x = xStart; x < xEnd; x = x + bytesPerPixel)
                         {
                             byte[,] grayBytes = new byte[matrixSize, matrixSize];
                             int y1Index = 0;
                             for (int y1 = y - halfSize; y1 <= y + halfSize; y1++)
                             {
                                 byte* currentMatrixLine = PtrFirstPixel + (y1 * bitmapData.Stride);
                                 int xStart1 = x - halfSize * bytesPerPixel;
                                 int xEnd1 = x + halfSize * bytesPerPixel;
                                 int x1Index = 0;
                                 for (int x1 = xStart1; x1 <= xEnd1; x1 = x1 + bytesPerPixel)
                                 {
                                     byte gray = currentMatrixLine[x1];
                                     grayBytes[x1Index, y1Index] = gray;
                                     x1Index++;
                                 }
                                 y1Index++;
                             }
                             // Average
                             double average = 0;
                             for (int ii = 0; ii < matrixSize; ii++)
                             {
                                 for (int jj = 0; jj < matrixSize; jj++)
                                 {
                                     average += (double)grayBytes[ii, jj];
                                 }
                             }
                             average /= (matrixSize * matrixSize);
                             double variance = 0;
                             for (int ii = 0; ii < matrixSize; ii++)
                             {
                                 for (int jj = 0; jj < matrixSize; jj++)
                                 {
                                     variance += (double)((grayBytes[ii, jj] - average) * (grayBytes[ii, jj] - average));
                                 }
                             }
                             variance /= (matrixSize * matrixSize);
                             double standardDevation = Math.Sqrt(variance);
                             if (standardDevation > maxSTD) { maxSTD = standardDevation; }
                             // average + k * standardDevation; //  
                             double threshold = average * (1 + k * ((standardDevation / r) - 1));
                             double oldGray = (double)currentLine[x];
                             if (oldGray <= threshold)
                             {
                                 newGray[xIndex, y-halfSize] = 0;
                             }
                             else
                             {
                                 newGray[xIndex, y-halfSize] = 255;
                             }
                             xIndex++;
                         }
                    }
                    for (int y = halfSize; y < bitmapData.Height - halfSize - 1; y++)
                    {
                        byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                        int xStart = halfSize * bytesPerPixel;
                        int xEnd = widthInBytes - (halfSize * bytesPerPixel);
                        int xIndex = 0;
                        for (int x = xStart; x < xEnd; x = x + bytesPerPixel)
                        {
                            currentLine[x] = newGray[xIndex, y - halfSize];
                            currentLine[x+1] = newGray[xIndex, y - halfSize];
                            currentLine[x+2] = newGray[xIndex, y - halfSize];
                            xIndex++;
                        }
                    }
                }
            }
        }
        
        // Assumes grayscale..
        public double GetAverageBrightness()
        {
            double graySum = 0;
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;
                Parallel.For(0, bitmapData.Height, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int gray = currentLine[x];  // Again, grayscale assumed...
                        graySum += gray;
                    }
                });
            }
            double average = graySum / (bitmap.Width * bitmap.Height);
            return average;
        }

        public void FindSkinPixelsRGB()
        {
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, bitmapData.Height, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        // Changed 20171109: For RGBA images (32-bit) the order is typically ABGR!
                    /*    int blue = currentLine[x + bytesPerPixel - 3];
                        int green = currentLine[x + bytesPerPixel - 2];
                        int red = currentLine[x + bytesPerPixel - 1];  */
                        int blue = currentLine[x];
                        int green = currentLine[x + 1];
                        int red = currentLine[x + 2];  
                        Boolean isSkin = false;
                        Boolean isSkin1 = ((red > 95) && (green > 40) && (blue > 20));
                        if (isSkin1)
                        {
                            int max = Math.Max(red, Math.Max(green, blue));
                            int min = Math.Min(red, Math.Min(green, blue));
                            Boolean isSkin2 = ((max - min) > 15);
                            if (isSkin2)
                            {
                                Boolean isSkin3 =  (Math.Abs(red-green) > 15) &&  Math.Abs(red-green) < 75 &&
                                                   (red > green) && (red > blue);
                           //     isSkin3 = isSkin3 && ((red + green + blue) < 550);
                                if (isSkin3)
                                {
                                    isSkin = true;
                                }
                            }
                        }
                        if (!isSkin)
                        {
                            currentLine[x] = 0;
                            currentLine[x+1] = 0;
                            currentLine[x+2] = 0;
                        }                        
                    }
                });
            }
        }

        public void FindSkinPixelsYCbCr()
        {
           // int[,,] yCbCr = new int[bitmap.Width, bitmap.Height,3];
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;
                Parallel.For(0, bitmapData.Height, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        // Changed 20171109: For RGBA images (32-bit) the order is typically ABGR!
                     /*   int blue = currentLine[x + bytesPerPixel - 3];
                        int green = currentLine[x + bytesPerPixel - 2];
                        int red = currentLine[x + bytesPerPixel - 1];  */
                           int blue = currentLine[x];
                           int green = currentLine[x + 1];
                           int red = currentLine[x + 2];  

                        //    int luma = 16 + (int)Math.Round(0.25679 * red + 0.50413 * green + 0.09791 * blue);
                        int cb = 128 + (int)Math.Round(-0.14822 * red - 0.29099 * green + 0.43922 * blue);
                        int cr = 128 + (int)Math.Round(0.43922 * red - 0.36799 * green - 0.07143 * blue);

                        if ((cb < 77) || (cb > 127) || (cr < 133) || (cr > 173))
                        {
                            currentLine[x] = 0;
                            currentLine[x + 1] = 0;
                            currentLine[x + 2] = 0;
                        }

                    }
                });
            }
        }

        public void FindSkinPixelsFromHeatMap(int[,,] heatMapAsArray)
        {
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;
                Parallel.For(0, bitmapData.Height, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    //    int xPixel = 0;
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int blue = currentLine[x];
                        int green = currentLine[x + 1];
                        int red = currentLine[x + 2];
                        int cb = 128 + (int)Math.Round(-0.14822 * red - 0.29099 * green + 0.43922 * blue);
                        int cr = 128 + (int)Math.Round(0.43922 * red - 0.36799 * green - 0.07143 * blue);
                        int grayValue = heatMapAsArray[cb, cr, 0];
                        currentLine[x] = (byte)grayValue;
                        currentLine[x + 1] = (byte)grayValue;
                        currentLine[x + 2] = (byte)grayValue;
                    }
                });
            }
        }

        public Bitmap Extract(int xStart, int yStart, int xEnd, int yEnd)
        {
            Bitmap extractedBitmap = new Bitmap(xEnd - xStart + 1, yEnd - yStart + 1, bitmap.PixelFormat);
            unsafe
            {                
                BitmapData extractedBitmapData = extractedBitmap.LockBits(
                   new Rectangle(0, 0, extractedBitmap.Width, extractedBitmap.Height),
                   ImageLockMode.ReadWrite, extractedBitmap.PixelFormat);
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                //      int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;
                byte* extractionPtrFirstPixel = (byte*)extractedBitmapData.Scan0;
                Parallel.For(yStart, yEnd + 1, y =>
                {
                    int yExtracted = (int)(y - yStart);
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    byte* extractionLine = extractionPtrFirstPixel + (yExtracted * extractedBitmapData.Stride);
                    for (int x = xStart * bytesPerPixel; x <= xEnd * bytesPerPixel; x = x + bytesPerPixel)
                    {
                        int xExtraction = x - xStart * bytesPerPixel;
                        for (int kk = 0; kk < bytesPerPixel; kk++)
                        {
                            extractionLine[xExtraction+kk] = currentLine[x+kk];
                        }
                    }
                });  
                extractedBitmap.UnlockBits(extractedBitmapData);
            }
            return extractedBitmap;
        }

        public int[,] GetCbCrDistribution()
        {
            int[,] cbCrDistribution = new int[256, 256];
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;
                Parallel.For(0, bitmapData.Height, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int blue = currentLine[x];
                        int green = currentLine[x + 1];
                        int red = currentLine[x + 2];
                        int cb = 128 + (int)Math.Round(-0.14822 * red - 0.29099 * green + 0.43922 * blue);
                        int cr = 128 + (int)Math.Round(0.43922 * red - 0.36799 * green - 0.07143 * blue);
                        cbCrDistribution[cb, cr] += 1;
                    }
                });
            }
            return cbCrDistribution;
        }

        public void FindSkinPixelsFromCbCrDistribution(double[,] cbCrArray, double threshold)
        {
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;
                Parallel.For(0, bitmapData.Height, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int blue = currentLine[x];
                        int green = currentLine[x + 1];
                        int red = currentLine[x + 2];
                        int cb = 128 + (int)Math.Round(-0.14822 * red - 0.29099 * green + 0.43922 * blue);
                        int cr = 128 + (int)Math.Round(0.43922 * red - 0.36799 * green - 0.07143 * blue);
                        if (cbCrArray[cb, cr] < threshold)
                        {
                            currentLine[x] = 0;
                            currentLine[x + 1] = 0;
                            currentLine[x + 2] = 0;
                        }
                    }
                });
            }
        }

        public void GenerateFromHeatMap(int[,,] heatMapAsArray)
        {
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;
                Parallel.For(0, bitmapData.Height, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    int pixelX = 0;
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        currentLine[x] = (byte)heatMapAsArray[pixelX, bitmapData.Height-1 - y, 0];
                        currentLine[x + 1] = (byte)heatMapAsArray[pixelX, bitmapData.Height-1 -  y, 1];
                        currentLine[x + 2] = (byte)heatMapAsArray[pixelX, bitmapData.Height-1 -  y, 2];
                        if (bytesPerPixel > 3)
                        {
                            currentLine[x + 3] = 255; // Alpha
                        }
                        pixelX++;
                    }
                });
            }
        }


        public void DrawVerticalLine(int position, Color color)
        { 
            if ((position < 0) || (position >= bitmap.Width)) { return; }
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                byte red = color.R;
                byte green = color.G;
                byte blue = color.B;
                int x = position * bytesPerPixel;
                Parallel.For(0, bitmapData.Height, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    currentLine[x] = blue;
                    currentLine[x + 1] = green;
                    currentLine[x + 2] = red;
                });
            }
        }

        public void DrawBox(int left, int right, int top, int bottom, Color color)
        {
            unsafe
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                byte red = color.R;
                byte green = color.G;
                byte blue = color.B;
                int xLeft = left * bytesPerPixel;
                int xRight = right * bytesPerPixel;
                Parallel.For(top, bottom, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    currentLine[xLeft] = blue;
                    currentLine[xLeft + 1] = green;
                    currentLine[xLeft + 2] = red;
                    currentLine[xRight] = blue;
                    currentLine[xRight + 1] = green;
                    currentLine[xRight + 2] = red;
                });
                Parallel.For(left, right, x =>
                {
                    int xPosition = x * bytesPerPixel;
                    byte* currentLine = PtrFirstPixel + (top * bitmapData.Stride);
                    currentLine[xPosition] = blue;
                    currentLine[xPosition + 1] = green;
                    currentLine[xPosition + 2] = red;
                });
                Parallel.For(left, right, x =>
                {
                    int xPosition = x * bytesPerPixel;
                    byte* currentLine = PtrFirstPixel + (bottom * bitmapData.Stride);
                    currentLine[xPosition] = blue;
                    currentLine[xPosition + 1] = green;
                    currentLine[xPosition + 2] = red;
                });
            }
        }
        #endregion

        #region Properties
        public Bitmap Bitmap
        {
            get
            {
                if (!isLocked)
                {
                    return (Bitmap)bitmap.Clone();
                 //   return new Bitmap(bitmap); 
                }
                else { return null; }
            }
        }
        #endregion
    }
}
