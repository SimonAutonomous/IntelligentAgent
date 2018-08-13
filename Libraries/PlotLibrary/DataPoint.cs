using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlotLibrary
{
    public class DataPoint
    {
        private double x;
        private double errorLeft;
        private double errorRight;
        private double y;
        private double errorTop;
        private double errorBottom;

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public DataPoint()
        {
            x = 0;
            errorLeft = 0;
            errorRight = 0;
            y = 0;
            errorTop = 0;
            errorBottom = 0;
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public double ErrorLeft
        {
            get { return errorLeft; }
            set { errorLeft = value; }
        }

        public double ErrorRight
        {
            get { return errorRight; }
            set { errorRight = value; }
        }

        public double ErrorTop
        {
            get { return errorTop; }
            set { errorTop = value; }
        }

        public double ErrorBottom
        {
            get { return errorBottom; }
            set { errorBottom = value; }
        }
    }
}
