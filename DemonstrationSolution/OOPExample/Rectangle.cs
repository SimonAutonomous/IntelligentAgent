using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOPExample
{
    public class Rectangle : Shape
    {
        private double sideLengthX;
        private double sideLengthY;

        public Rectangle()  // Constructor
        {
            hasCorners = true;
        }

        public override double ComputeArea()
        {
            double area = sideLengthX * sideLengthY;
            return area;
        }

        public double SideLengthX
        {
            get { return sideLengthX; }
            set { sideLengthX = value; }
        }

        public double SideLengthY
        {
            get { return sideLengthY; }
            set { sideLengthY = value; }
        }

        public double Area
        {
            get { return sideLengthX * sideLengthY; }
        }
    }
        // Alternative implementation
  /*  
        private double sideLengthX;
        private double sideLengthY;

        public Rectangle()  // Constructor
        {
            hasCorners = true;
        }

        public override void ComputeArea()
        {
            area = sideLengthX * sideLengthY;
        }

        public double SideLengthX
        {
            get { return sideLengthX; }
            set
            {
                sideLengthX = value;
                ComputeArea();
            }
        }

        public double SideLengthY
        {
            get { return sideLengthY; }
            set
            {
                sideLengthY = value;
                ComputeArea();
            }
        }
    }  */
}
