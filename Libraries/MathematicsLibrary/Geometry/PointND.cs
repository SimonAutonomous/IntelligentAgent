using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MathematicsLibrary.Geometry
{
    [DataContract]
    /// This class implements an N-dimensional point.
    public class PointND
    {
        [DataMember]
        public List<double> CoordinateList { get; set; }
        //  public double X { get; set; }
        //  public double Y { get; set; }

        /// This default constructor generates a new point, but does not assign values (set to NaN)
        public PointND(int numberOfDimensions)
        {
            CoordinateList = new List<double>();
            for (int ii = 0; ii < numberOfDimensions; ii++)
            {
                CoordinateList.Add(double.NaN);
            }
        }

        /// This constructor generates a 2-dimensional point and assigns the x and y coordinates.
        public PointND(double x, double y)
        {
            this.CoordinateList = new List<double>();
            this.CoordinateList.Add(x);
            this.CoordinateList.Add(y);
        }

        /// This constructor generates a 3-dimensional point and assigns the x and y coordinates.
        public PointND(double x, double y, double z)
        {
            this.CoordinateList = new List<double>();
            this.CoordinateList.Add(x);
            this.CoordinateList.Add(y);
            this.CoordinateList.Add(z);
        }

        public PointND(List<double> coordinateList)
        {
            this.CoordinateList = new List<double>();
            foreach (double c in coordinateList)
            {
                this.CoordinateList.Add(c);
            }
        }

        public static PointND GetAverage(PointND point1, PointND point2)
        {
            PointND averagePoint = new PointND(point1.GetNumberOfDimensions());
            for (int ii = 0; ii < averagePoint.GetNumberOfDimensions(); ii++)
            {
                averagePoint.CoordinateList[ii] = (point1.CoordinateList[ii]+point2.CoordinateList[ii])/2;
            }
            return averagePoint;
        }

        /// This method generates a copy (i.e. a new instance) of the point.
        public PointND Copy()
        {
            PointND copiedPoint = new PointND(this.CoordinateList);
            return copiedPoint;
        }

        /// This static method computes the (euclidean) squared distance between two points.
        public static double GetDistanceSquared(PointND point1, PointND point2)
        {
            double distanceSquared = 0;
            for (int ii = 0; ii < point1.CoordinateList.Count; ii++)
            {
                distanceSquared += ((point1.CoordinateList[ii] - point2.CoordinateList[ii]) *
                                    (point1.CoordinateList[ii] - point2.CoordinateList[ii]));
            }
            return distanceSquared;
        }

        public void SetValue(double constant)
        {
            for (int ii = 0; ii < CoordinateList.Count; ii++)
            {
                CoordinateList[ii] = constant;
            }
        }

        /// This method returns the number of dimensions of the point.
        public int GetNumberOfDimensions()
        {
            return CoordinateList.Count;
        }
    }
}
