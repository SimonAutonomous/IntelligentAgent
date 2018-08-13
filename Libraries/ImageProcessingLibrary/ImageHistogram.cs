using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImageProcessingLibrary
{
    public class ImageHistogram
    {
        private List<int> pixelNumberList = null;
        private List<double> pixelFractionList = null;
        private List<double> cumulativePixelFractionList = null;
        private ColorChannel colorChannel;

        public ImageHistogram(ColorChannel colorChannel)
        {
            pixelNumberList = new List<int>();
            this.colorChannel = colorChannel;
        }

        public void MakeFractional()
        {
            pixelFractionList = new List<double>();
            double pixelSum = pixelNumberList.Sum();
            foreach (int pixelNumber in pixelNumberList)
            {
                double pixelFraction = pixelNumber/(double)pixelSum;
                pixelFractionList.Add(pixelFraction);
            }
        }

        // Bin contents from 0 (at intensity 0) to 1 (at intensity 255)
        public void MakeCumulative()
        {
            if (pixelFractionList == null) { MakeFractional(); }
            cumulativePixelFractionList = new List<double>();
            cumulativePixelFractionList.Add(pixelFractionList[0]);
            for (int ii = 1; ii < pixelFractionList.Count; ii++)
            {
                double cumulativeFraction = cumulativePixelFractionList[ii - 1] + pixelFractionList[ii];
                cumulativePixelFractionList.Add(cumulativeFraction);
            }
        }

        public ColorChannel ColorChannel
        {
            get { return colorChannel; }
        }

        public List<int> PixelNumberList
        {
            get { return pixelNumberList; }
            set { pixelNumberList = value; }
        }

        public List<double> PixelFractionList
        {
            get { return pixelFractionList; }
        }

        public List<double> CumulativePixelFractionList
        {
            get { return cumulativePixelFractionList; }
        }
    }
}
