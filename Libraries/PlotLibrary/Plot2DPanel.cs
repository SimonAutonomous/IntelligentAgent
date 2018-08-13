using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PostscriptLibrary;

namespace PlotLibrary
{
    public partial class Plot2DPanel : UserControl
    {
        private const int DEFAULT_FRAME_WIDTH = 70;
        private const int DEFAULT_FRAME_HEIGHT = 45;
        private const int DEFAULT_MAJOR_TICK_MARK_LENGTH = 8;
        private const int DEFAULT_MAJOR_TICK_MARK_THICKNESS = 1;
        private const string DEFAULT_FORMAT_STRING = "0.000";
        private List<DataSeries> dataSeriesList = null;
        private double xMin;
        private double xMax;
        private double yMin;
        private double yMax;

        private float leftFrameWidth;
        private float rightFrameWidth;
        private float topFrameHeight;
        private float bottomFrameHeight;

        private double xScale;
        private double yScale;
        private float plotWidth;
        private float plotHeight;


        private Color frameBackColor;
        private Color plotBackColor;
        private Color axisColor;
        private Color gridColor;
        private float axisThickness; // Absolute value.
        private float gridLineThickness; // Absolute value;
        private Boolean gridVisible;
        private double majorGridSpacingX;
        private double majorGridSpacingY;
        private double horizontalAxisY = 0;
        private double verticalAxisX = 0;
        private Boolean horizontalAxisVisible;
        private Boolean verticalAxisVisible;

        private double majorHorizontalTickMarkSpacing;
        private double majorVerticalTickMarkSpacing;
        private float majorTickMarkLength;
        private float majorTickMarkThickness;
        private Boolean majorHorizontalTickMarksVisible;
        private Boolean majorVerticalTickMarksVisible;
        private Color tickMarkColor;

        private Boolean horizontalAxisMarkingsVisible;
        private string horizontalAxisMarkingsFormatString;
        private Color axisMarkingsColor;
        private Boolean verticalAxisMarkingsVisible;
        private string verticalAxisMarkingsFormatString;

        private Boolean horizontalAxisLabelVisible;
        private string horizontalAxisLabel;
        private float horizontalAxisLabelFontSize;
        private Boolean verticalAxisLabelVisible;
        private string verticalAxisLabel;
        private float verticalAxisLabelFontSize;

        private Boolean verticalAutoRange = false; // 20160827
        private double relativeAutoRangeMargin = 0.01; // 20160827 (default value)
        private double dataPointYMin = 0;
        private double dataPointYMax = 0;

        private Boolean generatePostscript;
        private PostscriptRenderer postscriptRenderer;

        public Plot2DPanel()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                Initialize();
            }
        }

        private void Initialize()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.AutoScaleMode = AutoScaleMode.Font;

            this.Paint += new PaintEventHandler(HandlePaint);
            this.Resize += new EventHandler(HandleResize);

            this.xMin = 0;
            this.xMax = 1;
            this.yMin = 0;
            this.yMax = 1;

            this.leftFrameWidth = DEFAULT_FRAME_WIDTH;
            this.rightFrameWidth = DEFAULT_FRAME_HEIGHT; // Yes, use height here.
            this.topFrameHeight = DEFAULT_FRAME_HEIGHT;
            this.bottomFrameHeight = DEFAULT_FRAME_HEIGHT;

            this.Font = new System.Drawing.Font(new FontFamily("Times New Roman"), this.Font.Size);

            frameBackColor = Color.White;
            plotBackColor = Color.White;
            axisColor = Color.Black;
            axisThickness = 1;
            horizontalAxisVisible = false;
            verticalAxisVisible = false;

            gridColor = Color.DarkGray;
            gridLineThickness = 1;
            gridVisible = false; // ToDo: Perhaps change to false as default

            majorHorizontalTickMarksVisible = true;
            majorVerticalTickMarksVisible = true;
            majorTickMarkThickness = DEFAULT_MAJOR_TICK_MARK_THICKNESS;
            majorTickMarkLength = DEFAULT_MAJOR_TICK_MARK_LENGTH;
            tickMarkColor = Color.Black;

            horizontalAxisMarkingsVisible = true;
            horizontalAxisMarkingsFormatString = DEFAULT_FORMAT_STRING;
            axisMarkingsColor = Color.Black;
            verticalAxisMarkingsVisible = true;
            verticalAxisMarkingsFormatString = DEFAULT_FORMAT_STRING;

            horizontalAxisLabelVisible = true;
            horizontalAxisLabel = "";
            horizontalAxisLabelFontSize = this.Font.Size;
            verticalAxisLabelVisible = true;
            verticalAxisLabel = "";
            verticalAxisLabelFontSize = this.Font.Size;

            generatePostscript = false;
            postscriptRenderer = null;

            Invalidate();
        }

        private double GetDataPointYMin()
        {
            if (dataSeriesList == null) { return 0; }
            double dataPointYMin = double.MaxValue;
            foreach (DataSeries dataSeries in dataSeriesList)
            {
                double seriesDataPointYMin = dataSeries.GetMinimumY();
                if (seriesDataPointYMin < dataPointYMin) { dataPointYMin = seriesDataPointYMin; }
            }
            return dataPointYMin;
        }

        private double GetDataPointYMax()
        {
            if (dataSeriesList == null) { return 0; }
            double dataPointYMax = double.MinValue;
            foreach (DataSeries dataSeries in dataSeriesList)
            {
                double seriesDataPointYMax= dataSeries.GetMaximumY();
                if (seriesDataPointYMax > dataPointYMax) { dataPointYMax = seriesDataPointYMax; }
            }
            return dataPointYMax;
        }

        private void DrawBackground(Graphics g)
        {
            using (SolidBrush brush = new SolidBrush(frameBackColor))
            {
                g.FillRectangle(brush, new RectangleF(0, 0, this.Width, this.Height));
                if (generatePostscript) { postscriptRenderer.AddFilledRectangle(brush, 0, 0, this.Width, this.Height); }
                brush.Color = plotBackColor;
                g.FillRectangle(brush, new RectangleF(leftFrameWidth, topFrameHeight, plotWidth, plotHeight));
                if (generatePostscript) { postscriptRenderer.AddFilledRectangle(brush, leftFrameWidth, topFrameHeight, 
                    plotWidth, plotHeight); }
            }
        }

        private float GetPlotXAtX(double x)
        {
            float plotX = leftFrameWidth + (float)((x - xMin) * xScale);
            return plotX;
        }

        private float GetPlotYAtY(double y)
        {
            float plotY = this.Height - bottomFrameHeight - (float)((y - yMin) * yScale);
            return plotY;
        }

        private void DrawLine(Graphics g, Pen pen, float xStart, float yStart, float xEnd, float yEnd)
        {
            g.DrawLine(pen, xStart, yStart, xEnd, yEnd);
            if (generatePostscript) { postscriptRenderer.AddLine(pen, xStart, yStart, xEnd, yEnd); }
        }

        private void DrawString(Graphics g, string s, Font font, SolidBrush brush, float x, float y, float offsetX, float offsetY)
        {
            g.DrawString(s, font, brush, x + offsetX, y + offsetY);
            if (generatePostscript) { postscriptRenderer.AddString(s, font, x, y, offsetX, offsetY); }
        }

        private void DrawVerticalString(Graphics g, string s, Font font, SolidBrush brush, float x, float y, float offsetX, float offsetY)
        {
            g.TranslateTransform(x, y);
            g.RotateTransform(90);
            g.DrawString(verticalAxisLabel, font, brush, offsetX, offsetY);
            g.ResetTransform();
            if (generatePostscript) { postscriptRenderer.AddVerticalString(s, font, x, y, offsetX, offsetY); }
        }

        private void DrawEllipse(Graphics g, SolidBrush brush, float x, float y, float pointSize)
        {
            g.FillEllipse(brush, new RectangleF(x - pointSize / 2, y - pointSize / 2, pointSize, pointSize));
            if (generatePostscript) { postscriptRenderer.AddDisc(brush, x, y, pointSize); }
        }

        private void PlotGrid(Graphics g)
        {
            using (Pen gridPen = new Pen(gridColor))
            {
                gridPen.Width = gridLineThickness;
                // find the position of the first vertical grid line
                if (Math.Abs(yMax - yMin) < double.Epsilon) { return; }
                if (majorGridSpacingX < double.Epsilon) { return; }
                float yPlotMin = GetPlotYAtY(yMin);
                float yPlotMax = GetPlotYAtY(yMax);
                double xPosition = majorGridSpacingX * Math.Round(xMin / majorGridSpacingX) - majorGridSpacingX;
                while (xPosition <= (xMax + double.Epsilon))
                {
                    if (xPosition > xMin)
                    {
                        float xPlot = GetPlotXAtX(xPosition);
                        DrawLine(g, gridPen, xPlot, yPlotMin, xPlot, yPlotMax);
                    }
                    xPosition += majorGridSpacingX;
                } 
                if (Math.Abs(xMax - xMin) < double.Epsilon) { return; }
                if (majorGridSpacingY < double.Epsilon) { return; }
                float xPlotMin = GetPlotXAtX(xMin);
                float xPlotMax = GetPlotXAtX(xMax);
                double yPosition = majorGridSpacingY * Math.Round(yMin / majorGridSpacingY) - majorGridSpacingY;
                while (yPosition <= (yMax + double.Epsilon))
                {
                    if (yPosition > yMin)
                    {
                        float yPlot = GetPlotYAtY(yPosition);
                        DrawLine(g, gridPen, xPlotMin, yPlot, xPlotMax, yPlot);
                    }
                    yPosition += majorGridSpacingY;
                } 
            }
        }

        private void PlotMajorHorizontalTickMarks(Graphics g)
        {
            using (Pen tickPen = new Pen(tickMarkColor))
            {
                tickPen.Width = majorTickMarkThickness;
                if (Math.Abs(xMax - xMin) < double.Epsilon) { return; }
                if (majorHorizontalTickMarkSpacing < double.Epsilon) { return; }
                float yPlotTop = GetPlotYAtY(verticalAxisX); // (yMin);
                float yPlotBottom = yPlotTop + majorTickMarkLength;
                double xPosition = majorHorizontalTickMarkSpacing * Math.Round(xMin / majorHorizontalTickMarkSpacing) - majorHorizontalTickMarkSpacing;
                while (xPosition <= (xMax + double.Epsilon))
                {
                    if ((xPosition- xMin) > -double.Epsilon)
                    {
                        float xPlot = GetPlotXAtX(xPosition);
                        DrawLine(g, tickPen, xPlot, yPlotBottom, xPlot, yPlotTop);
                    }
                    xPosition += majorHorizontalTickMarkSpacing;
                } 
            }
        }

        private void PlotHorizontalAxisMarkings(Graphics g)
        {
            using (SolidBrush axisMarkingsBrush = new SolidBrush(axisMarkingsColor))
            {
                if (Math.Abs(xMax - xMin) < double.Epsilon) { return; }
                if (majorHorizontalTickMarkSpacing < double.Epsilon) { return; }
                float yPlotTop = GetPlotYAtY(horizontalAxisY); // (yMin);
                float yPlotBottom = yPlotTop + majorTickMarkLength;
                double xPosition = majorHorizontalTickMarkSpacing * Math.Round(xMin / majorHorizontalTickMarkSpacing) - majorHorizontalTickMarkSpacing;
                double previousRight = 0;
                while (xPosition <= (xMax + double.Epsilon))
                {
                    if ((xPosition - xMin) > -double.Epsilon)
                    {
                        float xPlot = GetPlotXAtX(xPosition);
                        string axisMarking = xPosition.ToString(horizontalAxisMarkingsFormatString);
                        float width = g.MeasureString(axisMarking, this.Font).Width;
                        double currentLeft = xPlot - width / 2;
                        if (currentLeft > previousRight)
                        {
                            DrawString(g, axisMarking, this.Font, axisMarkingsBrush, xPlot, yPlotBottom, -width / 2, 0);
                            previousRight = xPlot + width / 2;
                        }
                    }
                    xPosition += majorHorizontalTickMarkSpacing;
                }
            }
        }

        private void PlotMajorVerticalTickMarks(Graphics g)
        {
            using (Pen tickPen = new Pen(tickMarkColor))
            {
                tickPen.Width = majorTickMarkThickness;
                if (Math.Abs(yMax - yMin) < double.Epsilon) { return; }
                if (majorVerticalTickMarkSpacing < double.Epsilon) { return; }
                float xPlotRight = GetPlotXAtX(verticalAxisX); // (xMin);
                float xPlotLeft = xPlotRight - majorTickMarkLength;
                double yPosition = majorVerticalTickMarkSpacing * Math.Round(yMin / majorVerticalTickMarkSpacing) - majorVerticalTickMarkSpacing;
                while (yPosition <= (yMax + double.Epsilon))
                {
                    if ((yPosition - yMin) > -double.Epsilon)
                    {
                        float yPlot = GetPlotYAtY(yPosition);
                        DrawLine(g, tickPen, xPlotLeft, yPlot, xPlotRight, yPlot);
                    }
                    yPosition += majorVerticalTickMarkSpacing;
                }
            }
        }

        private void PlotVerticalAxisMarkings(Graphics g)
        {
            using (SolidBrush axisMarkingsBrush = new SolidBrush(axisMarkingsColor))
            {
                if (Math.Abs(yMax - yMin) < double.Epsilon) { return; }
                if (majorVerticalTickMarkSpacing < double.Epsilon) { return; }
                float xPlotRight = GetPlotXAtX(verticalAxisX); //  (xMin);
                float xPlotLeft = xPlotRight - majorTickMarkLength;
                double yPosition = majorVerticalTickMarkSpacing * Math.Round(yMin / majorVerticalTickMarkSpacing) - majorVerticalTickMarkSpacing;
                double previousTop = double.MaxValue;
                while (yPosition <= (yMax + double.Epsilon))
                {
                    if ((yPosition - yMin) > -double.Epsilon)
                    {
                        float yPlot = GetPlotYAtY(yPosition);
                        string axisMarking = yPosition.ToString(verticalAxisMarkingsFormatString);
                        float height = g.MeasureString(axisMarking, this.Font).Height;
                        float width = g.MeasureString(axisMarking, this.Font).Width;
                        double currentBottom = yPlot + height / 2;
                        if (currentBottom < previousTop)
                        {
                            DrawString(g, axisMarking, this.Font, axisMarkingsBrush, xPlotLeft, yPlot, -width, -height / 2);
                            previousTop = yPlot - height / 2;
                        }
                    }
                    yPosition += majorVerticalTickMarkSpacing;
                }
            }
        }

        private void PlotHorizontalAxisLabel(Graphics g)
        {
            using (SolidBrush labelBrush = new SolidBrush(axisMarkingsColor))
            {
                if (Math.Abs(xMax - xMin) < double.Epsilon) { return; }
                float yPlotTop = GetPlotYAtY(horizontalAxisY); // (yMin);
                float yPlotBottom = yPlotTop + majorTickMarkLength;
                float xRight = GetPlotXAtX(xMax);
              //  float xMiddle = (GetPlotXAtX(xMin)+GetPlotXAtX(xMax))/2;
                float height = g.MeasureString(" ", this.Font).Height;  // Make space for the axis markings
                float yPlot = yPlotBottom + height;
                using (Font labelFont = new Font(this.Font.FontFamily, horizontalAxisLabelFontSize))
                {
                    float labelWidth = g.MeasureString(horizontalAxisLabel, labelFont).Width;
                    DrawString(g, horizontalAxisLabel, labelFont, labelBrush, xRight, yPlot, -labelWidth / 2, 0);
                 //   DrawString(g, horizontalAxisLabel, labelFont, labelBrush, xMiddle, yPlot, -labelWidth/2, 0);
                }
            }
        }

        private void PlotVerticalAxisLabel(Graphics g)
        {
            using (SolidBrush labelBrush = new SolidBrush(axisMarkingsColor))
            {
                if (Math.Abs(yMax - yMin) < double.Epsilon) { return; }
                float xPlotRight = GetPlotXAtX(xMin);
                float xPlotLeft = xPlotRight - majorTickMarkLength;
                float yMiddle = (GetPlotYAtY(yMin) + GetPlotYAtY(yMax)) / 2;
                float height = g.MeasureString(" ", this.Font).Height;  // Make space for the axis markings
                float xRight = xPlotLeft - height;
                using (Font labelFont = new Font(this.Font.FontFamily, verticalAxisLabelFontSize))
                {
                    StringFormat drawFormat = new StringFormat(StringFormatFlags.DirectionVertical);
                    float labelHeight = g.MeasureString(verticalAxisLabel, labelFont).Height;
                    float labelWidth = g.MeasureString(verticalAxisLabel, labelFont).Width;
                    DrawVerticalString(g, verticalAxisLabel, labelFont, labelBrush, xRight , yMiddle, -labelWidth/2, labelHeight);
                }
            }
        }

        private void PlotHorizontalAxis(Graphics g)
        {
            float startX = leftFrameWidth;
            float endX = leftFrameWidth + plotWidth;
            float y = GetPlotYAtY(horizontalAxisY);
            using (Pen axisPen = new Pen(axisColor))
            {
                axisPen.Width = axisThickness;
                g.DrawLine(axisPen, startX, y, endX, y);
            }
        }

        private void PlotVerticalAxis(Graphics g)
        {
            float startY = topFrameHeight;
            float endY = topFrameHeight + plotHeight;
            float x = GetPlotXAtX(verticalAxisX);
            using (Pen axisPen = new Pen(axisColor))
            {
                axisPen.Width = axisThickness;
                g.DrawLine(axisPen, x, startY, x, endY);
            }
        }
        
        private void PlotAxes(Graphics g)
        {
            float startX = leftFrameWidth;
            float endX = leftFrameWidth + plotWidth;
            float y = GetPlotYAtY(horizontalAxisY);
            float startY = topFrameHeight;
            float endY = topFrameHeight + plotHeight;
            float x = GetPlotXAtX(verticalAxisX);
            using (Pen axisPen = new Pen(axisColor))
            {
                axisPen.Width = axisThickness;
                g.DrawLine(axisPen, startX, y, endX, y);
                g.DrawLine(axisPen, x, startY, x, endY);
            }
        }

        private void PlotPoint(DataPoint currentPoint, DataPoint nextPoint, PlotAttributes plotAttributes, Graphics g)
        {
            using (SolidBrush pointBrush = new SolidBrush(Color.White))
            {
                // To Do: More here - add connection to the next point, different symbols etc.
                float plotX = GetPlotXAtX(currentPoint.X);
                float plotY = GetPlotYAtY(currentPoint.Y);
                pointBrush.Color = plotAttributes.PointColor;
                if (plotAttributes.Connect)
                {
                    using (Pen linePen = new Pen(plotAttributes.LineColor))
                    {
                        linePen.Width = (float)(plotAttributes.RelativeLineWidth * Math.Max(plotWidth, plotHeight));
                        if (nextPoint != null)
                        {
                            float nextPlotX = GetPlotXAtX(nextPoint.X);
                            float nextPlotY = GetPlotYAtY(nextPoint.Y);
                            DrawLine(g, linePen, plotX, plotY, nextPlotX, nextPlotY);
                        }
                    }
                }
                if (plotAttributes.PointVisible)
                {
                    if (plotAttributes.PlotSymbol == PlotSymbol.Disc)
                    {
                        float pointSize = (float)(2*plotAttributes.RelativePointSize * Math.Max(plotWidth, plotHeight));
                        if (((currentPoint.X - xMin) > -double.Epsilon) && ((xMax - currentPoint.X) > -double.Epsilon) &&
                            ((currentPoint.Y - yMin) > -double.Epsilon) && ((yMax - currentPoint.Y) > -double.Epsilon))
                        {
                            DrawEllipse(g, pointBrush, plotX, plotY, pointSize);
                        }
                    }
                }
                if (plotAttributes.HorizontalErrorBarVisible)
                {
                    float horizontalErrorPlotXMin = GetPlotXAtX(currentPoint.X - currentPoint.ErrorLeft);
                    float horizontalErrorPlotXMax = GetPlotXAtX(currentPoint.X + currentPoint.ErrorRight);
                    float horizontalErrorPlotY = GetPlotYAtY(currentPoint.Y);
                    using (Pen errorPen = new Pen(plotAttributes.ErrorBarColor))
                    {
                        DrawLine(g, errorPen, horizontalErrorPlotXMin, horizontalErrorPlotY, horizontalErrorPlotXMax, horizontalErrorPlotY);
                        if (plotAttributes.UseHorizontalErrorBarSerifs)
                        {
                            float serifLength = (float)(plotAttributes.RelativeErrorBarSerifLength*Math.Max(plotWidth, plotHeight));
                            float serifYMin = GetPlotYAtY(currentPoint.Y) - serifLength;
                            float serifYMax = GetPlotYAtY(currentPoint.Y) + serifLength;
                            DrawLine(g, errorPen, horizontalErrorPlotXMin, serifYMin, horizontalErrorPlotXMin, serifYMax);
                            DrawLine(g, errorPen, horizontalErrorPlotXMax, serifYMin, horizontalErrorPlotXMax, serifYMax);
                        }
                    }

                }
                if (plotAttributes.VerticalErrorBarVisible)
                {
                    float verticalErrorPlotYMin = GetPlotYAtY(currentPoint.Y - currentPoint.ErrorBottom);
                    float verticalErrorPlotYMax = GetPlotYAtY(currentPoint.Y + currentPoint.ErrorTop);
                    float verticalErrorPlotX = GetPlotXAtX(currentPoint.X);
                    using (Pen errorPen = new Pen(plotAttributes.ErrorBarColor))
                    {
                        DrawLine(g, errorPen, verticalErrorPlotX, verticalErrorPlotYMin, verticalErrorPlotX, verticalErrorPlotYMax);
                        if (plotAttributes.UseVerticalErrorBarSerifs)
                        {
                            float serifLength = (float)(plotAttributes.RelativeErrorBarSerifLength * Math.Max(plotWidth, plotHeight));
                            float serifXMin = GetPlotXAtX(currentPoint.X) - serifLength;
                            float serifXMax = GetPlotXAtX(currentPoint.X) + serifLength;
                            DrawLine(g, errorPen, serifXMin, verticalErrorPlotYMin, serifXMax, verticalErrorPlotYMin);
                            DrawLine(g, errorPen, serifXMin, verticalErrorPlotYMax, serifXMax, verticalErrorPlotYMax);
                        }
                    }
                }
            }
        }

        private void PlotSeries(DataSeries dataSeries, Graphics g)
        {
            for (int ii = 0; ii < dataSeries.DataPointList.Count; ii++)
            {
                DataPoint currentPoint = dataSeries.DataPointList[ii];
                DataPoint nextPoint = null;
                if ((ii+1) < dataSeries.DataPointList.Count) {nextPoint = dataSeries.DataPointList[ii+1];}
                PlotAttributes plotAttributes = dataSeries.PlotAttributesList[ii];
                PlotPoint(currentPoint, nextPoint, plotAttributes, g);
            }
        }

        private void SetVerticalAutoRange()
        {
            double dataPointYMin = GetDataPointYMin();
            double dataPointYMax = GetDataPointYMax();
            double range = dataPointYMax - dataPointYMin;
            double rangeMargin = range*relativeAutoRangeMargin;
            dataPointYMin -= rangeMargin;
            dataPointYMax += rangeMargin;
            int powerOfTen = (int)Math.Round(Math.Log10(range));
            majorVerticalTickMarkSpacing = Math.Pow(10, powerOfTen - 1);
            majorGridSpacingY = majorVerticalTickMarkSpacing;
            SetVerticalPlotRange(dataPointYMin, dataPointYMax);
            if (dataPointYMin * dataPointYMax > 0) // same sign
            {
                horizontalAxisY = dataPointYMax - rangeMargin;  // A bit ugly - better to set at the tick mark
            }
            else { horizontalAxisY = 0; }
        }

        protected virtual void HandlePaint(object sender, PaintEventArgs e)
        {
            if (verticalAutoRange) { SetVerticalAutoRange(); }
            SetScale();
            DrawBackground(e.Graphics);
            if (gridVisible) { PlotGrid(e.Graphics); }
            if (horizontalAxisVisible) { PlotHorizontalAxis(e.Graphics); }
            if (verticalAxisVisible) { PlotVerticalAxis(e.Graphics); }
            if (majorHorizontalTickMarksVisible) { PlotMajorHorizontalTickMarks(e.Graphics); }
            if (majorVerticalTickMarksVisible) { PlotMajorVerticalTickMarks(e.Graphics); }
            if (horizontalAxisMarkingsVisible) { PlotHorizontalAxisMarkings(e.Graphics); }
            if (verticalAxisMarkingsVisible) { PlotVerticalAxisMarkings(e.Graphics); }
            if (horizontalAxisLabelVisible) { PlotHorizontalAxisLabel(e.Graphics); }
            if (verticalAxisLabelVisible) { PlotVerticalAxisLabel(e.Graphics); }
            if (dataSeriesList == null) { return; }
            foreach (DataSeries dataSeries in dataSeriesList)
            {
                PlotSeries(dataSeries, e.Graphics);
            }
        }

        private void SetScale()
        {
            plotWidth = (float)(this.Width - leftFrameWidth - rightFrameWidth);
            plotHeight = (float)(this.Height - topFrameHeight - bottomFrameHeight);
            xScale = 0;
            if (xMax > xMin)
            {
                xScale = plotWidth / (xMax - xMin);
            }
            yScale = 0;
            if (yMax > yMin)
            {
                yScale = plotHeight / (yMax - yMin);
            }
        }

        private void HandleResize(object sender, EventArgs e)
        {
            // ToDo: Compute frame width (with rescaled fonts etc.)
            
            Invalidate();
        }

        public void SetFrameSize(double leftFrameWidth, double rightFrameWidth, double topFrameHeight, double bottomFrameHeight)
        {
            this.leftFrameWidth = (float)leftFrameWidth;
            this.rightFrameWidth = (float)rightFrameWidth;
            this.topFrameHeight = (float)topFrameHeight;
            this.bottomFrameHeight = (float)bottomFrameHeight;
            Invalidate();
        }

        public void SetHorizontalPlotRange(double xMin, double xMax)
        {
            this.xMin = xMin;
            this.xMax = xMax;
           // verticalAxisX = 0; // xMin; // Default value.
        }

        public void SetVerticalPlotRange(double yMin, double yMax)
        {
            this.yMin = yMin;
            this.yMax = yMax;
           // horizontalAxisY = 0; // yMin;
        }

        public void SaveAsPostscript(string filePath)
        {
            postscriptRenderer = new PostscriptRenderer();
            postscriptRenderer.SetSize(0, 0, this.Width, this.Height);
            generatePostscript = true;
            Refresh();
            generatePostscript = false;
            postscriptRenderer.MakeHeader();
            postscriptRenderer.SaveToFile(filePath);
        }

        public void SaveAsPDF(string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            string tmpFilePath = directory + "\\tmp" + DateTime.Now.ToString("yyyymmddHHMMssfff") + ".eps";
            string pathRoot = Path.GetPathRoot(filePath).TrimEnd(new char[] { '\\' });
            SaveAsPostscript(tmpFilePath);
            try
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                string tmpFileName = Path.GetFileName(tmpFilePath);
                string fileName = Path.GetFileName(filePath);
                startInfo.WorkingDirectory = directory;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C ps2pdf " + tmpFileName + " " + fileName;
                process.StartInfo = startInfo;
                process.Start();
                while (!process.HasExited) { }  // necessary for the first process to complete first
                Process processDelete = new Process();
                ProcessStartInfo startInfoDelete = new ProcessStartInfo();
                startInfoDelete.WindowStyle = ProcessWindowStyle.Hidden;
                startInfoDelete.WorkingDirectory = directory;
                startInfoDelete.FileName = "cmd.exe";
                startInfoDelete.Arguments = "/C del " + tmpFileName;
                processDelete.StartInfo = startInfoDelete;
                processDelete.Start(); 
            }
            catch
            {
                MessageBox.Show("Could not convert to PDF");
            }
        }

     /*   public void SetGridLineSpacing(double spacingX, double spacingY)
        {
            this.majorGridSpacingX = spacingX;
            this.majorGridSpacingY = spacingY;
        }  */

    /*    public void SetMajorHorizontalTickMarkSpacing(double spacing)
        {
            this.majorHorizontalTickMarkSpacing = spacing;
            this.majorGridSpacingX = spacing;
        }

        public void SetMajorVerticalTickMarkSpacing(double spacing)
        {
            this.majorVerticalTickMarkSpacing = spacing;
            this.majorGridSpacingY = spacing;
        }  */

        public void AddDataSeries(DataSeries dataSeries)
        {
            if (dataSeriesList == null) {dataSeriesList = new List<DataSeries>(); }
            dataSeriesList.Add(dataSeries);
            Refresh();
        }

        public void RemoveSeries(string dataSeriesName)
        {
            int dataSeriesIndex = dataSeriesList.FindIndex(d => d.Name == dataSeriesName);
            if (dataSeriesIndex >= 0)
            {
                dataSeriesList.RemoveAt(dataSeriesIndex);
                Refresh();
            }
        }

        public void Clear()
        {
            dataSeriesList = null;
            Refresh();
        }

     /*   public void SetHorizontalAxisLabel(string horizontalAxisLabel)
        {
            this.horizontalAxisLabel = horizontalAxisLabel;
        }

        public void SetHorizontalAxisLabelFontSize(float size)
        {
            this.horizontalAxisLabelFontSize = size;
        }

        public void SetVerticalAxisLabel(string verticalAxisLabel)
        {
            this.verticalAxisLabel = verticalAxisLabel;
        }

        public void SetVerticalAxisLabelFontSize(float size)
        {
            this.verticalAxisLabelFontSize = size;
        }  */

        public void SetPointConnectionState(string seriesName, Boolean connectionState)
        {
            DataSeries dataSeries = dataSeriesList.Find(d => d.Name == seriesName);
            if (dataSeries != null)
            {
                dataSeries.SetPointConnectionState(connectionState);
                Refresh();
            }
        }

        public void SetPointVisibilityState(string seriesName, Boolean visibilityState)
        {
            DataSeries dataSeries = dataSeriesList.Find(d => d.Name == seriesName);
            if (dataSeries != null)
            {
                dataSeries.SetPointVisibilityState(visibilityState);
                Refresh();
            }
        }

        public void SetHorizontalErrorBarVisibilityState(string seriesName, Boolean visibilityState)
        {
            DataSeries dataSeries = dataSeriesList.Find(d => d.Name == seriesName);
            if (dataSeries != null)
            {
                dataSeries.SetHorizontalErrorBarsVisibilityState(visibilityState);
                Refresh();
            }
        }

        public void SetHorizontalErrorBarVisibilityState(Boolean visibilityState)
        {
            if (this.dataSeriesList == null) { return; }
            foreach (DataSeries dataSeries in this.dataSeriesList)
            {
                SetHorizontalErrorBarVisibilityState(dataSeries.Name, visibilityState);
            }
        }

        public void SetVerticalErrorBarVisibilityState(string seriesName, Boolean visibilityState)
        {
            DataSeries dataSeries = dataSeriesList.Find(d => d.Name == seriesName);
            if (dataSeries != null)
            {
                dataSeries.SetVerticalErrorBarsVisibilityState(visibilityState);
           //     Refresh();
            }
        }

        public void SetVerticalErrorBarVisibilityState(Boolean visibilityState)
        {
            if (this.dataSeriesList == null) { return; }
            foreach (DataSeries dataSeries in this.dataSeriesList)
            {
                dataSeries.SetVerticalErrorBarsVisibilityState(visibilityState);
             //   SetVerticalErrorBarVisibilityState(visibilityState);
            }
        }

        public void SetPointColor(string seriesName, Color color)
        {
            DataSeries dataSeries = dataSeriesList.Find(d => d.Name == seriesName);
            if (dataSeries != null)
            {
                dataSeries.SetPointColor(color);
                Refresh();
            }
        }

        public void SetLineColor(string seriesName, Color color)
        {
            DataSeries dataSeries = dataSeriesList.Find(d => d.Name == seriesName);
            if (dataSeries != null)
            {
                dataSeries.SetLineColor(color);
                Refresh();
            }
        }

        public List<DataSeries> DataSeriesList
        {
            get { return dataSeriesList; }
        }

        public void SetHorizontalAxisPosition(double y)
        {
            horizontalAxisY = y;
        }

        public void SetVerticalAxisPosition(double x)
        {
            verticalAxisX = x;
        }

        public float LeftFrameWidth
        {
            get { return leftFrameWidth; }
            set
            {
                leftFrameWidth = value;
                if (leftFrameWidth < 0) { leftFrameWidth = 0; }
            }
        }

        public float RightFrameWidth
        {
            get { return rightFrameWidth; }
            set
            {
                rightFrameWidth = value;
                if (rightFrameWidth < 0) { rightFrameWidth = 0; }
            }
        }

        public float TopFrameHeight
        {
            get { return topFrameHeight; }
            set
            {
                topFrameHeight = value; ;
                if (topFrameHeight < 0) { topFrameHeight = 0; }
            }
        }

        public float BottomFrameHeight
        {
            get { return bottomFrameHeight; }
            set
            {
                bottomFrameHeight = value;
                if (bottomFrameHeight < 0) { bottomFrameHeight = 0; }
            }
        }
           

        public Color PlotBackColor
        {
            get { return plotBackColor; }
            set { plotBackColor = value; }
        }

        public Color FrameBackColor
        {
            get { return frameBackColor; }
            set { frameBackColor = value; }
        }

        public Boolean GridVisible
        {
            get { return gridVisible; }
            set { gridVisible = value; }
        }

        public float GridLineThickness
        {
            get { return gridLineThickness; }
            set { gridLineThickness = value; }
        }

        public Color GridColor
        {
            get { return gridColor; }
            set { gridColor = value; }
        }

        public Color AxisColor
        {
            get { return axisColor; }
            set { axisColor = value; }
        }

        public float AxisThickness
        {
            get { return axisThickness; }
            set { axisThickness = value; }
        }

        public double MajorHorizontalTickMarkSpacing
        {
            get { return majorHorizontalTickMarkSpacing; }
            set
            {
                majorHorizontalTickMarkSpacing = value;
                majorGridSpacingX = value;
            }
        }

        public double MajorVerticalTickMarkSpacing
        {
            get { return majorVerticalTickMarkSpacing; }
            set
            {
                majorVerticalTickMarkSpacing = value;
                majorGridSpacingY = value;
            }
        }

        public Boolean HorizontalAxisVisible
        {
            get { return horizontalAxisVisible; }
            set { horizontalAxisVisible = value; }
        }

        public Boolean VerticalAxisVisible
        {
            get { return verticalAxisVisible; }
            set { verticalAxisVisible = value; }
        }

        public Boolean HorizontalAxisMarkingsVisible
        {
            get { return horizontalAxisMarkingsVisible; }
            set { horizontalAxisMarkingsVisible = value; }
        }

        public Boolean VerticalAxisMarkingsVisible
        {
            get { return verticalAxisMarkingsVisible; }
            set { verticalAxisMarkingsVisible = value; }
        }

        public Boolean MajorHorizontalTickMarksVisible
        {
            get { return majorHorizontalTickMarksVisible; }
            set { majorHorizontalTickMarksVisible = value; }
        }

        public Boolean MajorVerticalTickMarksVisible
        {
            get { return majorVerticalTickMarksVisible; }
            set { majorVerticalTickMarksVisible = value; }
        }

        public Color TickMarkColor
        {
            get { return tickMarkColor; }
            set { tickMarkColor = value; }
        }

        public Color AxisMarkingsColor
        {
            get { return axisMarkingsColor; }
            set { axisMarkingsColor = value; }
        }

        public string HorizontalAxisMarkingsFormatString
        {
            get { return horizontalAxisMarkingsFormatString; }
            set 
            { 
                Boolean valid = true;
                foreach (Char c in value)
                {
                    if (!(Char.IsNumber(c) || Char.IsPunctuation(c)))
                    {
                        valid = false;
                        break;
                    }
                }
                if (valid) { horizontalAxisMarkingsFormatString = value; }
                else { horizontalAxisMarkingsFormatString = DEFAULT_FORMAT_STRING; }
            }
        }

        public string VerticalAxisMarkingsFormatString
        {
            get { return verticalAxisMarkingsFormatString; }
            set
            {
                Boolean valid = true;
                foreach (Char c in value)
                {
                    if (!(Char.IsNumber(c) || Char.IsPunctuation(c)))
                    {
                        valid = false;
                        break;
                    }
                }
                if (valid) { verticalAxisMarkingsFormatString = value; }
                else { verticalAxisMarkingsFormatString = DEFAULT_FORMAT_STRING; }
            }
        }

        public Boolean HorizontalAxisLabelVisible
        {
            get { return horizontalAxisLabelVisible; }
            set { horizontalAxisLabelVisible = value; }
        }

        public string HorizontalAxisLabel
        {
            get { return horizontalAxisLabel; }
            set { horizontalAxisLabel = value; }
        }

        public float HorizontalAxisLabelFontSize
        {
            get { return horizontalAxisLabelFontSize; }
            set { horizontalAxisLabelFontSize = value; }
        }

        public Boolean VerticalAxisLabelVisible
        {
            get { return verticalAxisLabelVisible; }
            set { verticalAxisLabelVisible = value; }
        }

        public string VerticalAxisLabel
        {
            get { return verticalAxisLabel; }
            set { verticalAxisLabel = value; }
        }

        public float VerticalAxisLabelFontSize
        {
            get { return verticalAxisLabelFontSize; }
            set { verticalAxisLabelFontSize = value; }
        }

        public Boolean VerticalAutoRange
        {
            get { return verticalAutoRange; }
            set {verticalAutoRange = value;}
        }

        public double RelativeAutoRangeMargin
        {
            get { return relativeAutoRangeMargin; }
            set
            {
                relativeAutoRangeMargin = value;
                if (relativeAutoRangeMargin < 0) { relativeAutoRangeMargin = 0; }
            }
        }

        public double YMin
        {
            get { return yMin; }
        }

        public double YMax
        {
            get { return yMax; }
        }
    }
}
