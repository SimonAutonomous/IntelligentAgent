using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PlotLibrary
{
    public class PlotAttributes
    {
        private PlotSymbol plotSymbol;
        private Boolean connect; // To the next point, with a line
        private Boolean pointVisible;
        private double relativePointSize; // Relative to Min(Width, Height) of the plot
        private Color pointColor;
        private double relativeLineWidth; // Relative to Min(Width, Height) of the plot
        private Color lineColor; // For the segment connecting this point to the next point.
        private Boolean horizontalErrorBarVisible;
        private Boolean verticalErrorBarVisible;
        private Boolean useHorizontalErrorBarSerifs;
        private Boolean useVerticalErrorBarSerifs;
        private double relativeErrorBarSerifLength;
        private Color errorBarColor;

        public PlotAttributes()
        {
            plotSymbol = PlotSymbol.Disc;
            connect = false; //  true; // false;
            pointVisible = true;
            relativePointSize = 0.004;
            pointColor = Color.Black; //  Color.Red;
            relativeLineWidth = 0.002;
            lineColor = Color.Black;
            horizontalErrorBarVisible = false; // true; // false;
            verticalErrorBarVisible = false; // true; //  false;
            useHorizontalErrorBarSerifs = true;
            useVerticalErrorBarSerifs = true;
            relativeErrorBarSerifLength = 0.0005;
            errorBarColor = Color.Black;
        }

        public Boolean PointVisible
        {
            get { return pointVisible; }
            set { pointVisible = value; }
        }

        public PlotSymbol PlotSymbol
        {
            get { return plotSymbol; }
            set { plotSymbol = value; }
        }

        public double RelativePointSize
        {
            get { return relativePointSize; }
            set { relativePointSize = value; }
        }

        public Color PointColor
        {
            get { return pointColor; }
            set { pointColor = value; }
        }

        public Boolean Connect
        {
            get { return connect; }
            set { connect = value; }
        }

        public Color LineColor
        {
            get { return lineColor; }
            set { lineColor = value; }
        }

        public double RelativeLineWidth
        {
            get { return relativeLineWidth; }
            set { relativeLineWidth = value; }
        }

        public Boolean HorizontalErrorBarVisible
        {
            get { return horizontalErrorBarVisible; }
            set { horizontalErrorBarVisible = value; }
        }

        public Boolean VerticalErrorBarVisible
        {
            get { return verticalErrorBarVisible; }
            set { verticalErrorBarVisible = value; }
        }

        public Boolean UseHorizontalErrorBarSerifs
        {
            get { return useHorizontalErrorBarSerifs; }
            set { useHorizontalErrorBarSerifs = value; }
        }

        public Boolean UseVerticalErrorBarSerifs
        {
            get { return useVerticalErrorBarSerifs; }
            set { useVerticalErrorBarSerifs = value; }
        }

        public double RelativeErrorBarSerifLength
        {
            get { return relativeErrorBarSerifLength; }
            set { relativeErrorBarSerifLength = value; }
        }

        public Color ErrorBarColor
        {
            get { return errorBarColor; }
            set { errorBarColor = value; }
        }
    }
}
