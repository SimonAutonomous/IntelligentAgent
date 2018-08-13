using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOPExample
{
    public abstract class Shape
    {
        protected Boolean hasCorners;

        public abstract double ComputeArea();

        public Boolean HasCorners
        {
            get { return hasCorners; }
        }
    }

    // Alternative implementation
  /*  public abstract class Shape
    {
        protected double area;
        protected Boolean hasCorners;

        public abstract void ComputeArea();

        public double Area
        {
            get { return area; }
        }

        public Boolean HasCorners
        {
            get { return hasCorners; }
        }
    }  */
}
