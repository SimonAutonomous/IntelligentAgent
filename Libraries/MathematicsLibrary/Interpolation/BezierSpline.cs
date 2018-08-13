using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using MathematicsLibrary.Geometry;

namespace MathematicsLibrary.Interpolation
{
    [DataContract]
    public class BezierSpline
    {
        [DataMember]
        public List<PointND> ControlPointList { get; set; }

        /// This is the default constructor, which simply
        /// generates the four points A, B, C, and D 
        /// (of type Point).
        public BezierSpline(int numberOfDimensions)
        {
            PointND P0 = new PointND(numberOfDimensions);
            PointND P1 = new PointND(numberOfDimensions);
            PointND P2 = new PointND(numberOfDimensions);
            PointND P3 = new PointND(numberOfDimensions);
            ControlPointList = new List<PointND>() { P0, P1, P2, P3 };
        }

        public BezierSpline Copy()
        {
            int numberOfDimensions = ControlPointList[0].CoordinateList.Count;
            BezierSpline copiedSpline = new BezierSpline(numberOfDimensions);
            copiedSpline.ControlPointList[0] = this.ControlPointList[0].Copy();
            copiedSpline.ControlPointList[1] = this.ControlPointList[1].Copy();
            copiedSpline.ControlPointList[2] = this.ControlPointList[2].Copy();
            copiedSpline.ControlPointList[3] = this.ControlPointList[3].Copy();
            return copiedSpline;
        }

        /// This method returns a Point object containing the x and y
        /// coordinates at a given value of u (which should be in the range [0,1]).
        public PointND GetPoint(double u)
        {
            List<double> coordinateList = new List<double>();
            int numberOfDimensions = ControlPointList[0].GetNumberOfDimensions();
            for (int ii = 0; ii < numberOfDimensions; ii++)
            {
                double coordinate = ControlPointList[0].CoordinateList[ii] * (1 - u) * (1 - u) * (1 - u) +
                                    3 * ControlPointList[1].CoordinateList[ii] * u * (1 - u) * (1 - u) +
                                    3 * ControlPointList[2].CoordinateList[ii] * u * u * (1 - u) +
                                    ControlPointList[3].CoordinateList[ii] * u * u * u;
                coordinateList.Add(coordinate);
            }
            PointND point = new PointND(coordinateList);


            return point;
        }

        // Note: This method ASSUMES that the spline is a curve in two dimensions!
        public Point2D GetPoint2D(double u)
        {
            double x = ControlPointList[0].CoordinateList[0] * (1 - u) * (1 - u) * (1 - u) +
                                    3 * ControlPointList[1].CoordinateList[0] * u * (1 - u) * (1 - u) +
                                    3 * ControlPointList[2].CoordinateList[0] * u * u * (1 - u) +
                                    ControlPointList[3].CoordinateList[0] * u * u * u;
            double y = ControlPointList[0].CoordinateList[1] * (1 - u) * (1 - u) * (1 - u) +
                        3 * ControlPointList[1].CoordinateList[1] * u * (1 - u) * (1 - u) +
                        3 * ControlPointList[2].CoordinateList[1] * u * u * (1 - u) +
                        ControlPointList[3].CoordinateList[1] * u * u * u;
            Point2D point2D = new Point2D(x, y);
            return point2D;
        }

        //
        // Generates a (2-dimensional) curve, at a given z-value, in the form of a circular arc
        // It is assumed that a 3D constructor (PointND(2)) has been called
        // It is further assumed that the arcAngle > 0.
        //
        // See http://www.tinaja.com/glib/bezcirc2.pdf
        // 
        public void GenerateCircularArc(double radius, double centerAngle, double arcAngle, double z)
        {
            ControlPointList[0].CoordinateList[0] = radius*Math.Cos(arcAngle / 2);
            ControlPointList[0].CoordinateList[1] = radius * Math.Sin(arcAngle / 2);
            ControlPointList[0].CoordinateList[2] = z;
            ControlPointList[1].CoordinateList[0] = (4 * radius - ControlPointList[0].CoordinateList[0]) / 3;
            if (Math.Abs(ControlPointList[0].CoordinateList[1]) > double.Epsilon)
            {
                ControlPointList[1].CoordinateList[1] = (radius - ControlPointList[0].CoordinateList[0]) *
                            (3 * radius - ControlPointList[0].CoordinateList[0]) / (3 * ControlPointList[0].CoordinateList[1]);
            }
            else { ControlPointList[1].CoordinateList[1] = 0; }
            ControlPointList[1].CoordinateList[2] = z;
            ControlPointList[2].CoordinateList[0] = ControlPointList[1].CoordinateList[0];
            ControlPointList[2].CoordinateList[1] = -ControlPointList[1].CoordinateList[1];
            ControlPointList[2].CoordinateList[2] = z;
            ControlPointList[3].CoordinateList[0] = ControlPointList[0].CoordinateList[0];
            ControlPointList[3].CoordinateList[1] = -ControlPointList[0].CoordinateList[1];
            ControlPointList[3].CoordinateList[2] = z;


            // Rotate the points to the desired center angle:
            foreach (PointND controlPoint in ControlPointList)
            {
                double xOld = controlPoint.CoordinateList[0];
                double yOld = controlPoint.CoordinateList[1];
                double xNew = xOld * Math.Cos(centerAngle) - yOld * Math.Sin(centerAngle);
                double yNew = xOld * Math.Sin(centerAngle) + yOld * Math.Cos(centerAngle);
                controlPoint.CoordinateList[0] = xNew;
                controlPoint.CoordinateList[1] = yNew;
            }
        }
    }
}