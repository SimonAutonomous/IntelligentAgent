using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using MathematicsLibrary.Geometry;

namespace MathematicsLibrary.Interpolation
{
    // A bezier curve consists of a sequnce of bezier splines, such that
    // P0(k+1) = P3(k), where k enumerates the splines.
    [DataContract]
    public class BezierCurve
    {
        private List<BezierSpline> splineList;
        private Boolean isClosed;
        private Boolean isXSymmetric = false;
        private Boolean isPoint = false;

        public void Generate(List<BezierSpline> splineList)
        {
            this.splineList = new List<BezierSpline>();
            foreach (BezierSpline spline in splineList)
            {
                this.splineList.Add(spline.Copy());
            }

            // Check if the Bezier curve is a single point in space:
            isPoint = true;
            PointND firstPoint = this.splineList[0].ControlPointList[0];
            for (int ii = 0; ii < this.splineList.Count; ii++)
            {
                for (int jj = 0; jj < this.splineList[ii].ControlPointList.Count; jj++)
                {
                    PointND point = this.splineList[ii].ControlPointList[jj];
                    double distanceSquared = PointND.GetDistanceSquared(firstPoint, point);
                    if (distanceSquared > double.Epsilon)
                    {
                        isPoint = false;
                        break;
                    }
                }
            }
        }

        // Returns the point at uGlobal, where uGlobal ranges (linearly over the
        // splines) from 0 to 1. Thus, this method first finds the corresponding
        // spline and then the u-value within this spline.
        // Note, again, that this method obtains points using linear spacing in
        // u, which generally is not the same as actual linear spacing (since
        // the distance along a spline is non-linear in u). However, this
        // method is much faster than a method that attempts to generate points
        // with linear spacing.
        public PointND GetPoint(double uGlobal)
        {
            int iSpline = (int)Math.Truncate(uGlobal * splineList.Count);
            double u = splineList.Count*(uGlobal - (iSpline/ (double)splineList.Count));
            PointND point = splineList[iSpline].GetPoint(u);
            return point;
        }

        // Generates a spline curve in the form of a closed circle (approximation),
        // at a given z value.
        public void GenerateCircle(int numberOfSplines, double radius, double z)
        {
            if (numberOfSplines <= 1) { return; }
            if (numberOfSplines % 2 != 0) { return; }
            int nHalf = numberOfSplines / 2;
            double arcAngle = Math.PI / nHalf;
            List<BezierSpline> splineList = new List<BezierSpline>();
            // Generate the splines that form the right half (x >= 0) of the circle:
            for (int ii = 0; ii < nHalf; ii++)
            {
                double centerAngle = Math.PI / 2 - (2*ii + 1) * arcAngle / 2;
                BezierSpline bezierSpline = new BezierSpline(3);
                bezierSpline.GenerateCircularArc(radius, centerAngle, arcAngle, z);
                splineList.Add(bezierSpline);
            }
            Generate(splineList);
            CloseSymmetricallyX();
        }

        public static BezierCurve GetAverage(BezierCurve curve1, BezierCurve curve2)
        {
            if (curve1.SplineList.Count != curve2.SplineList.Count) { return null; }
            BezierCurve averageBezierCurve = new BezierCurve();
            int numberOfDimensions = curve1.SplineList[0].ControlPointList[0].GetNumberOfDimensions();  // A bit ugly, but OK...
            List<BezierSpline> splineList = new List<BezierSpline>();
            for (int iSpline = 0; iSpline < curve1.SplineList.Count; iSpline++)
            {
                BezierSpline averageSpline = new BezierSpline(numberOfDimensions);
                for (int jj = 0; jj < curve1.SplineList[iSpline].ControlPointList.Count; jj++)
                {
                    PointND point1 = curve1.SplineList[iSpline].ControlPointList[jj];
                    PointND point2 = curve2.SplineList[iSpline].ControlPointList[jj];
                    PointND averagePoint = PointND.GetAverage(point1, point2);
                    averageSpline.ControlPointList[jj] = averagePoint;
                }
                splineList.Add(averageSpline);
            }
            averageBezierCurve.Generate(splineList);
            if (curve1.isClosed) { averageBezierCurve.isClosed = true; }
            if (curve1.IsXSymmetric) { averageBezierCurve.isXSymmetric = true; }
            if ((curve1.isPoint) && (curve2.isPoint)) { averageBezierCurve.isPoint = true; }
            return averageBezierCurve;
        }

        public void CloseSymmetricallyX()
        {
            List<BezierSpline> mirrorSplineList = new List<BezierSpline>();
            int iMirror = splineList.Count;
            for (int ii = splineList.Count-1; ii >= 0; ii--)
            {
                BezierSpline mirrorSpline = new BezierSpline(2); // A bit ugly, but OK...
                mirrorSpline.ControlPointList[0] = splineList[ii].ControlPointList[3].Copy();
                mirrorSpline.ControlPointList[1] = splineList[ii].ControlPointList[2].Copy();
                mirrorSpline.ControlPointList[2] = splineList[ii].ControlPointList[1].Copy();
                mirrorSpline.ControlPointList[3] = splineList[ii].ControlPointList[0].Copy();
                mirrorSpline.ControlPointList[0].CoordinateList[0] *= -1;
                mirrorSpline.ControlPointList[1].CoordinateList[0] *= -1;
                mirrorSpline.ControlPointList[2].CoordinateList[0] *= -1;
                mirrorSpline.ControlPointList[3].CoordinateList[0] *= -1;
                mirrorSplineList.Add(mirrorSpline);
            }
            this.splineList.AddRange(mirrorSplineList);
            isXSymmetric = true;
            isClosed = true;
        }

        public List<Point2D> Interpolate(double uStep)
        {
            List<Point2D> pointList = new List<Point2D>();
            double u = 0;
            for (int ii = 0; ii < this.splineList.Count; ii++)
            {
                if (ii > 0) { u = uStep; }
                while (u < 1 + (uStep/2))
                {
                    if (u > 1) { u = 1; }
                    Point2D point = this.splineList[ii].GetPoint2D(u);
                    pointList.Add(point);
                    u += uStep;
                }
            }
            return pointList;
        }

        // Expands the Bézier curve, relative to the origin (0,0).
        // Note that the expansion can be negative
        public void Expand(double expansionDelta)
        {
            foreach (BezierSpline spline in splineList)
            {
                foreach (PointND controlPoint in spline.ControlPointList)
                {
                    double xOld = controlPoint.CoordinateList[0];
                    double yOld = controlPoint.CoordinateList[1];
                    double angle = Math.Atan2(yOld, xOld);
                    double radius = Math.Sqrt(xOld * xOld + yOld * yOld);
                    radius *= expansionDelta;
                    double xNew = radius * Math.Cos(angle);
                    double yNew = radius * Math.Sin(angle);
                    controlPoint.CoordinateList[0] = xNew;
                    controlPoint.CoordinateList[1] = yNew;
                }
            }
            
        }

        [DataMember]
        public List<BezierSpline> SplineList
        {
            get { return splineList; }
            set { splineList = value; }
        }

        [DataMember]
        public Boolean IsClosed
        {
            get { return isClosed; }
            set { isClosed = value; }
        }

        [DataMember]
        public Boolean IsXSymmetric
        {
            get { return isXSymmetric; }
            set { isXSymmetric = value; }
        }

        [DataMember]
        public Boolean IsPoint
        {
            get { return isPoint; }
            set { isPoint = value; }
        }
    }
}
