using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using CustomUserControlsLibrary;

namespace PlotLibrary
{
    public class PlotPanel
    {
        public int Top = 0;
        public int Height = 0;
        public List<DataSeries> DataSeriesList;
        public List<DataSeriesDisplaySettings> DisplaySettingsList;
        public List<PlotMarker> MarkerList = null;
        public float YMin;
        public float YMax;
        public float XMin;
        public float XMax;
        public Boolean HorizontalAxisVisible = true;
        public Color AxisColor = Color.LightGray;
        public float HorizontalTickMarkSpacing = 0.010f; // Default value
        public float TickMarkRelativeLength = 0.04f; // Fraction of window height.
        public List<float> HorizontalTickMarkList = null;
        public List<string> HorizontalTickLabelList = null;
        public float AxesOriginX = 0;
        public float AxesOriginY = 0;
        public float MinimumLabelSpacing = 5; // Default value, ToDo: introduce a constant.

        private HorizontalScrollableZoomControl ownerControl;
        private float scaleY;

        public void SetOwnerControl(HorizontalScrollableZoomControl ownerControl)
        {
            this.ownerControl = ownerControl;
        }

        public PlotPanel()
        {
            DataSeriesList = new List<DataSeries>();
            DisplaySettingsList = new List<DataSeriesDisplaySettings>();
            MarkerList = new List<PlotMarker>();
        }

        public void Clear()
        {
            this.DataSeriesList = new List<DataSeries>();
            this.DisplaySettingsList = new List<DataSeriesDisplaySettings>();
            this.MarkerList = new List<PlotMarker>();
            ownerControl.Invalidate();
        }

        public void AddSeries(DataSeries dataSeries, DataSeriesDisplaySettings displaySettings)
        {
            if (DataSeriesList == null)
            {
                DataSeriesList = new List<DataSeries>();
                DisplaySettingsList = new List<DataSeriesDisplaySettings>();
            }
            DataSeriesList.Add(dataSeries);
            DisplaySettingsList.Add(displaySettings);
            if (ownerControl != null) { ownerControl.Invalidate(); }
        }

        public void GenerateHorizontalAxisMarkings()
        {
            if (ownerControl == null) { return; }
            if (HorizontalTickMarkSpacing < double.Epsilon) { return; }
            HorizontalTickMarkList = new List<float>();
            float tickMarkPosition  = AxesOriginX;
            while (tickMarkPosition <= ownerControl.XMax)
            {
                HorizontalTickMarkList.Add(tickMarkPosition);
                tickMarkPosition += HorizontalTickMarkSpacing;
            }
            tickMarkPosition = AxesOriginX - HorizontalTickMarkSpacing;
            while (tickMarkPosition >= ownerControl.XMin)
            {
                HorizontalTickMarkList.Insert(0, tickMarkPosition);
                tickMarkPosition -= HorizontalTickMarkSpacing;
            }
            HorizontalTickLabelList = new List<string>();
            for (int ii = 0; ii < HorizontalTickLabelList.Count; ii++)
            {
                string tickLabel = HorizontalTickMarkList[ii].ToString("0.000"); // To do: make general
                HorizontalTickLabelList.Add(tickLabel);
            }
        }

        public void AddMarker(PlotMarker marker)
        {
            if (MarkerList == null) { MarkerList = new List<PlotMarker>(); }
            MarkerList.Add(marker);
        }

        private void DrawMarker(Graphics g, PlotMarker marker)
        {
            using (Pen markerPen = new Pen(Color.Empty))
            {
                markerPen.Color = marker.Color;
                markerPen.Width = marker.Thickness;
                if (marker.Type == PlotMarkerType.HorizontalLine)
                {
                    float yPlot = GetPixelYatY(marker.Level);
                    float xPlotStart = ownerControl.GetPixelXatX(marker.Start);
                    float xPlotEnd = ownerControl.GetPixelXatX(marker.End);
                    g.DrawLine(markerPen, xPlotStart, yPlot, xPlotEnd, yPlot);
                }
                else if (marker.Type == PlotMarkerType.VerticalLine)
                {
                    float xPlot = ownerControl.GetPixelXatX(marker.Level);
                    float yPlotStart = GetPixelYatY(marker.Start);
                    float yPlotEnd = GetPixelYatY(marker.End);
                    g.DrawLine(markerPen, xPlot, yPlotStart, xPlot, yPlotEnd);
                }
            }
        }

        private void DrawSeries(Graphics g, DataSeries dataSeries, DataSeriesDisplaySettings displaySettings)
        {
            float absoluteSymbolSize = Math.Min(this.Height, ownerControl.MainPanelWidth) * displaySettings.RelativeSymbolSize;
            if ((displaySettings.DrawLine) && (dataSeries.HorizontalData.Count > 1))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                using (Pen linePen = new Pen(displaySettings.LineColor))
                {
                    for (int ii = 1; ii < dataSeries.HorizontalData.Count; ii++)
                    {
                        double xPrevious = dataSeries.HorizontalData[ii - 1];
                        double yPrevious = dataSeries.VerticalData[ii - 1];
                        float xPlotPrevious = ownerControl.GetPixelXatX((float)xPrevious);
                        float yPlotPrevious = GetPixelYatY((float)yPrevious);
                        double x = dataSeries.HorizontalData[ii];
                        double y = dataSeries.VerticalData[ii];
                        float xPlot = ownerControl.GetPixelXatX((float)x);
                        float yPlot = GetPixelYatY((float)y);
                        if ((yPlot >= Top) && (yPlot <= Top + Height)) // MW ToDo: Need to deal with this, somehow.
                        {
                            g.DrawLine(linePen, xPlotPrevious, yPlotPrevious, xPlot, yPlot);
                        }
                    }
                }
            }
            using (SolidBrush pointBrush = new SolidBrush(displaySettings.SymbolColor))
            {
                for (int ii = 0; ii < dataSeries.HorizontalData.Count; ii++)
                {
                    double x = dataSeries.HorizontalData[ii];
                    double y = dataSeries.VerticalData[ii];
                    float xPlot = ownerControl.GetPixelXatX((float)x);
                    float yPlot = GetPixelYatY((float)y);
                    if (displaySettings.Symbol == SymbolStyle.Disc)
                    {
                        g.FillEllipse(pointBrush, xPlot - absoluteSymbolSize / 2, yPlot - absoluteSymbolSize / 2, absoluteSymbolSize, absoluteSymbolSize);
                    }
                    else if (displaySettings.Symbol == SymbolStyle.Square)
                    {
                        g.FillRectangle(pointBrush, xPlot - absoluteSymbolSize / 2, yPlot - absoluteSymbolSize / 2, absoluteSymbolSize, absoluteSymbolSize);
                    }
                }
            }
        }

        private float GetPixelYatY(float y)
        {
            float pixelY = Top + Height - (y - YMin) * Height / (YMax - YMin);
            return pixelY;
        }

        private void DrawHorizontalAxis(Graphics g)
        {
            using (Pen axisPen = new Pen(AxisColor))
            {
                using (SolidBrush axisBrush = new SolidBrush(AxisColor))
                {
                    float axisYPlot = GetPixelYatY(AxesOriginY);
                    float xPlotMin = ownerControl.GetPixelXatX(ownerControl.PlotLeft);
                    float xPlotMax = ownerControl.GetPixelXatX(ownerControl.PlotRight);
                    g.DrawLine(axisPen, xPlotMin, axisYPlot, xPlotMax, axisYPlot);
                    g.DrawLine(axisPen, xPlotMin, this.Top + this.Height - 1, xPlotMax, this.Top + this.Height - 1);
                    float tickMarkAbsoluteLength = TickMarkRelativeLength * this.Height;
                    if (HorizontalTickMarkList == null) { GenerateHorizontalAxisMarkings(); }
                    float previousRight = float.MinValue;
                    float previousLeft = float.MinValue;
                    foreach (float horizontalTickMark in HorizontalTickMarkList)
                    {
                        float xPlot = ownerControl.GetPixelXatX(horizontalTickMark);
                        g.DrawLine(axisPen, xPlot, this.Top + this.Height - 1, xPlot,
                            this.Top + this.Height - tickMarkAbsoluteLength);
                        if (Math.Abs(horizontalTickMark - AxesOriginX) > double.Epsilon)
                        {
                            string tickLabel = horizontalTickMark.ToString("0.000"); // To do: make general
                            float tickLabelWidth = g.MeasureString(tickLabel, ownerControl.Font).Width;
                            float tickLabelHeight = g.MeasureString(tickLabel, ownerControl.Font).Height;
                            float tickLabelLeft = xPlot - tickLabelWidth / 2;
                            float tickLabelRight = tickLabelLeft + tickLabelWidth;
                            float yPlotEnd = axisYPlot - tickLabelHeight - 2*tickMarkAbsoluteLength;
                            if ((tickLabelLeft >= 0) && (tickLabelLeft > (previousRight + MinimumLabelSpacing)) && (tickLabelRight < ownerControl.MainPanelWidth))
                            {
                                float verticalAxisXPlot = ownerControl.GetPixelXatX(AxesOriginX);
                                g.DrawLine(axisPen, xPlot, this.Top + this.Height - 1, xPlot,
                                this.Top + this.Height - 2*tickMarkAbsoluteLength);
                                g.DrawString(tickLabel, ownerControl.Font, axisBrush, xPlot - tickLabelWidth / 2, yPlotEnd);
                                previousLeft = tickLabelLeft;
                                previousRight = tickLabelRight;
                            }
                        }
                    }
                }
            }
        }

        public void Draw(Graphics g)
        {
            if (this.DataSeriesList == null) { return; }
            if (HorizontalAxisVisible) { DrawHorizontalAxis(g); }
            scaleY = this.Height / (this.YMax - this.YMin);
            for (int ii = 0; ii < DataSeriesList.Count; ii++)
            {
                DrawSeries(g, DataSeriesList[ii], DisplaySettingsList[ii]);
            }
            if (this.MarkerList == null) { return; }
            for (int ii = 0; ii < MarkerList.Count; ii++)
            {
                DrawMarker(g, MarkerList[ii]);
            }
        }

        public void SetVerticalRange(float yMin, float yMax)
        {
            this.YMin = yMin;
            this.YMax = yMax;
        }
    }
}
