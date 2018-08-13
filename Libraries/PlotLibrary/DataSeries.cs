using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PlotLibrary
{
    public class DataSeries
    {
        private string name;
        private List<DataPoint> dataPointList = null;
        private List<PlotAttributes> plotAttributesList = null;

        // Generates a data series without error bars, and with default plot attributes
        public void Generate(string name, List<double> horizontalData, List<double> verticalData)
        {
            this.name = name;
            dataPointList = new List<DataPoint>();
            plotAttributesList = new List<PlotAttributes>();
            if (horizontalData.Count != verticalData.Count) { return; }
            for (int ii = 0; ii < horizontalData.Count; ii++)
            {
                DataPoint dataPoint = new DataPoint();
                dataPoint.X = horizontalData[ii];
                dataPoint.Y = verticalData[ii];
                dataPointList.Add(dataPoint);
                PlotAttributes plotAttributes = new PlotAttributes();
                plotAttributesList.Add(plotAttributes);
            }
        }
        
        // 20160119
        public void Generate(string name, List<DataPoint> dataPointList)
        {
            this.name = name;
            this.dataPointList = dataPointList;
            plotAttributesList = new List<PlotAttributes>();
            foreach (DataPoint dataPoint in dataPointList)
            {
                PlotAttributes plotAttributes = new PlotAttributes();
                plotAttributesList.Add(plotAttributes);
            }
        }

        public void AddSymmetricVerticalErrorBars(List<double> verticalErrorBarList)
        {
            if (verticalErrorBarList.Count != dataPointList.Count) { return; }
            for (int ii = 0; ii < dataPointList.Count; ii++)
            {
                dataPointList[ii].ErrorTop = verticalErrorBarList[ii];
                dataPointList[ii].ErrorBottom = verticalErrorBarList[ii];
            }
        }

        public void AddSymmetricHorizontalErrorBars(List<double> verticalErrorBarList)
        {
            if (verticalErrorBarList.Count != dataPointList.Count) { return; }
            for (int ii = 0; ii < dataPointList.Count; ii++)
            {
                dataPointList[ii].ErrorLeft = verticalErrorBarList[ii];
                dataPointList[ii].ErrorRight = verticalErrorBarList[ii];
            }
        }

        public void SetPointConnectionState(Boolean pointConnectionState)
        {
            foreach (PlotAttributes plotAttributes in plotAttributesList)
            {
                plotAttributes.Connect = pointConnectionState;
            }
        }

        public void ConnectRange(double xMin, double xMax)
        {
            for (int ii = 0; ii < dataPointList.Count; ii++)
            {
                if ((dataPointList[ii].X >= xMin) && (dataPointList[ii].X <= xMax))
                {
                    plotAttributesList[ii].Connect = true;
                }
            }
        }

        public void SetPointVisibilityState(Boolean visibilityState)
        {
            foreach (PlotAttributes plotAttributes in plotAttributesList)
            {
                plotAttributes.PointVisible = visibilityState;
            }
        }

        public void SetHorizontalErrorBarsVisibilityState(Boolean visibilityState)
        {
            foreach (PlotAttributes plotAttributes in plotAttributesList)
            {
                plotAttributes.HorizontalErrorBarVisible = visibilityState;
            }
        }

        public void SetVerticalErrorBarsVisibilityState(Boolean visibilityState)
        {
            foreach (PlotAttributes plotAttributes in plotAttributesList)
            {
                plotAttributes.VerticalErrorBarVisible = visibilityState;
            }
        }

        public void SetErrorBarColor(Color color)
        {
            foreach (PlotAttributes plotAttributes in plotAttributesList)
            {
                plotAttributes.ErrorBarColor = color;
            }
        }

        public void SetHorizontalErrorBarSerifVisibilityState(Boolean visibilityState)
        {
            foreach (PlotAttributes plotAttributes in plotAttributesList)
            {
                plotAttributes.UseHorizontalErrorBarSerifs = visibilityState;
            }
        }

        public void SetVerticalErrorBarSerifVisibilityState(Boolean visibilityState)
        {
            foreach (PlotAttributes plotAttributes in plotAttributesList)
            {
                plotAttributes.UseVerticalErrorBarSerifs = visibilityState;
            }
        }

        public void SetErrorBarRelativeSerifLength(double relativeSerifLength)
        {
            foreach (PlotAttributes plotAttributes in plotAttributesList)
            {
                plotAttributes.RelativeErrorBarSerifLength = relativeSerifLength;
            }
        }

        // The following method was added to change datapoints' size, 20160307 ST.
        public void SetRelativePointSize(double size)
        {
            foreach (PlotAttributes plotAttributes in plotAttributesList)
            {
                plotAttributes.RelativePointSize = size;
            }
        }

        // The following method was added to change line width, 20160307 ST.
        public void SetRelativeLineWidth(double width)
        {
            foreach (PlotAttributes plotAttributes in plotAttributesList)
            {
                plotAttributes.RelativeLineWidth = width;
            }
        }

        public void SetPointColor(Color color)
        {
            foreach (PlotAttributes plotAttributes in plotAttributesList)
            {
                plotAttributes.PointColor = color;
            }
        }

        public void SetLineColor(Color color)
        {
            foreach (PlotAttributes plotAttributes in plotAttributesList)
            {
                plotAttributes.LineColor = color;
            }
        }

        public double GetMinimumY()
        {
            double minimumY = double.MaxValue;
            foreach (DataPoint dataPoint in dataPointList)
            {
                if (dataPoint.Y < minimumY) { minimumY = dataPoint.Y; }
            }
            return minimumY;
        }

        public double GetMaximumY()
        {
            double maximumY = double.MinValue;
            foreach (DataPoint dataPoint in dataPointList)
            {
                if (dataPoint.Y > maximumY) { maximumY = dataPoint.Y; }
            }
            return maximumY;
        }

        public string Name
        {
            get { return name; }
        }

        public List<DataPoint> DataPointList
        {
            get { return dataPointList; }
        }

        public List<PlotAttributes> PlotAttributesList
        {
            get { return plotAttributesList; }
        }
    }
}
