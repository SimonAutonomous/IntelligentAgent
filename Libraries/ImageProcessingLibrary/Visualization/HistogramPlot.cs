using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageProcessingLibrary.Visualization
{
    public partial class HistogramPlot : UserControl
    {
        private ImageHistogram imageHistogram = null;
        private double xMin;
        private double xMax;
        private double yMin;
        private double yMax;
        private double horizontalScale;
        private double verticalScale;
        private int colorBarHeight = 20;

        private int HorizontalValueToPlotCoordinate(double x)
        {
            int plotCoordinateX = (int)Math.Round(horizontalScale * (x - xMin));
            return plotCoordinateX;
        }

        private int VerticalValueToPlotCoordinate(double y)
        {
            int plotCoordinateY = Height - (int)Math.Round(verticalScale * (y - yMin));
            return plotCoordinateY;
        }

        public HistogramPlot()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.imageHistogram != null)
            {
                int redParameter = 0;
                int greenParameter = 0;
                int blueParameter = 0;
                if (imageHistogram.ColorChannel == ColorChannel.Red) { redParameter = 1; }
                else if (imageHistogram.ColorChannel == ColorChannel.Green) { greenParameter = 1; }
                else if (imageHistogram.ColorChannel == ColorChannel.Blue) { blueParameter = 1; }
                else
                {
                    redParameter = 1;
                    greenParameter = 1;
                    blueParameter = 1;
                }
                using (SolidBrush backgroundBrush = new SolidBrush(Color.Black))
                {
                    for (int ii = 0; ii < imageHistogram.PixelNumberList.Count; ii++)
                    {
                        int currentXPlot = HorizontalValueToPlotCoordinate(ii);
                        int nextXPlot = HorizontalValueToPlotCoordinate(ii + 1);
                        backgroundBrush.Color = Color.FromArgb(redParameter * ii, greenParameter * ii, blueParameter * ii);
                        e.Graphics.FillRectangle(backgroundBrush, currentXPlot, 0, nextXPlot - currentXPlot, colorBarHeight);
                    }
                }
                using (SolidBrush brush = new SolidBrush(Color.Black))
                {
                    int yTopPlot = VerticalValueToPlotCoordinate(0);
                    for (int ii = 0; ii < imageHistogram.PixelNumberList.Count; ii++)
                    {
                        int currentXPlot = HorizontalValueToPlotCoordinate(ii);
                        int nextXPlot = HorizontalValueToPlotCoordinate(ii + 1);
                        int yBottomPlot = VerticalValueToPlotCoordinate(imageHistogram.PixelNumberList[ii]);
                        e.Graphics.FillRectangle(brush, currentXPlot, yBottomPlot, nextXPlot - currentXPlot, yTopPlot - yBottomPlot);
                    }
                }
            }
        }

        private void SetScales()
        {
            verticalScale = 0;
            if (yMax > yMin) { verticalScale = (this.Height-colorBarHeight) / (yMax - yMin); }
            horizontalScale = this.Width / (xMax - yMin);
        }

        protected override void OnResize(EventArgs e)
        {
            SetScales();
            base.OnResize(e);
        }

        public void SetImageHistogram(ImageHistogram imageHistogram)
        {
            if (imageHistogram == null) {return;}
            this.imageHistogram = imageHistogram;
            xMin = 0;
            xMax = 256;
            yMin = 0;
            yMax = this.imageHistogram.PixelNumberList.Max();
            SetScales();
            Invalidate();
        }
    }
}
