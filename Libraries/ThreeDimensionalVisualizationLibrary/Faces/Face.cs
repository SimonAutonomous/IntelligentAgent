using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using MathematicsLibrary.Geometry;
using MathematicsLibrary.Interpolation;
using ThreeDimensionalVisualizationLibrary.Objects;

namespace ThreeDimensionalVisualizationLibrary.Faces
{
    [DataContract]
    public class Face: Object3D
    {
        private List<BezierCurve> horizontalBezierCurveList = null;
        private int numberOfLongitudePoints;

        public void Initialize()
        {
            vertexSize = 3f;  // A bit ugly to hard-code, but OK ...
            wireFrameWidth = 1.5f; // A bit ugly to hard-code, but OK ...
            horizontalBezierCurveList = new List<BezierCurve>();
            // Some arbitrary hard-coding here:
            BezierCurve bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(32, 0, 1);
            horizontalBezierCurveList.Add(bezierCurve);
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(32, 0.025, 0.999);
            horizontalBezierCurveList.Add(bezierCurve);
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(32, 0.075, 0.99);
            horizontalBezierCurveList.Add(bezierCurve);
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(32, 0.15, 0.98);
            horizontalBezierCurveList.Add(bezierCurve);
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(32, 0.3, 0.93);
            horizontalBezierCurveList.Add(bezierCurve);
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(32, 0.4, 0.875);
            horizontalBezierCurveList.Add(bezierCurve);
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(32, 0.5, 0.80);
            horizontalBezierCurveList.Add(bezierCurve);
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(32, 0.55, 0.70);
            horizontalBezierCurveList.Add(bezierCurve);
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(32, 0.55, 0.60);
            horizontalBezierCurveList.Add(bezierCurve);
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(32, 0.55, 0.50);
            horizontalBezierCurveList.Add(bezierCurve);
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(32, 0.525, 0.40);
            horizontalBezierCurveList.Add(bezierCurve);
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(32, 0.50, 0.30);
            horizontalBezierCurveList.Add(bezierCurve);
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(32, 0.15, 0.20);
            horizontalBezierCurveList.Add(bezierCurve);
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(32, 0.15, 0.10);
            horizontalBezierCurveList.Add(bezierCurve);
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(32, 0.15, 0.00);
            horizontalBezierCurveList.Add(bezierCurve);
        }
        // This method sets up a basic, rotationally symmetric starting point for generating a face shape
        public void InitializeOLD()
        {
            horizontalBezierCurveList = new List<BezierCurve>();
            double z = 1;
            double radius = 0;
            double deltaZ = 0.1;
            // Some arbitrary hard-coding here:
            BezierCurve bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(16, radius, z);
            horizontalBezierCurveList.Add(bezierCurve);
            z -= deltaZ;  // 0.9
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(16, 0.2, z);
            horizontalBezierCurveList.Add(bezierCurve);
            z -= deltaZ;  // 0.8
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(16, 0.6, z);
            horizontalBezierCurveList.Add(bezierCurve);
            z -= deltaZ; // 0.7
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(16, 0.8, z);
            horizontalBezierCurveList.Add(bezierCurve);
            z -= deltaZ; // 0.6;
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(16, 1.0, z);
            horizontalBezierCurveList.Add(bezierCurve);
            z -= deltaZ; // 0.5;
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(16, 1.0, z);
            horizontalBezierCurveList.Add(bezierCurve);
            z -= deltaZ; // 0.4;
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(16, 1.0, z);
            horizontalBezierCurveList.Add(bezierCurve);
            z -= deltaZ; // 0.3;
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(16, 0.5, z);
            horizontalBezierCurveList.Add(bezierCurve);
            z -= deltaZ; // 0.2;
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(16, 0.3, z);
            horizontalBezierCurveList.Add(bezierCurve);
            z -= deltaZ; // 0.1;
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(16, 0.3, z);
            horizontalBezierCurveList.Add(bezierCurve);
            z -= deltaZ; // 0.0;
            bezierCurve = new BezierCurve();
            bezierCurve.GenerateCircle(16, 0.3, z);
            horizontalBezierCurveList.Add(bezierCurve);
        }

        public override void Generate(List<double> parameterList)
        {
            base.Generate(parameterList);
            if (parameterList == null) { return; }
            if (parameterList.Count < 1) { return; }
            numberOfLongitudePoints = (int)Math.Round(parameterList[0]);
            double deltaU = 1/(double)numberOfLongitudePoints;
            List<List<List<double>>> pointList = new List<List<List<double>>>();
            for (int iZ = 0; iZ < horizontalBezierCurveList.Count; iZ++)
            {
                List<List<double>> slicePointList = new List<List<double>>();
                BezierCurve horizontalBezierCurve = horizontalBezierCurveList[iZ];
                double z = horizontalBezierCurve.SplineList[0].ControlPointList[0].CoordinateList[2];
                for (int iLongitude = 0; iLongitude < numberOfLongitudePoints; iLongitude++)
                {
                    double uGlobal = iLongitude * deltaU;
                    if (iZ % 2 == 0) { uGlobal += deltaU / 2; } // Shift points in every other layer
                    PointND point = horizontalBezierCurve.GetPoint(uGlobal);
                    double x = point.CoordinateList[0];
                    double y = point.CoordinateList[1];
                    slicePointList.Add(new List<double>() { x, y, z });
                    Vertex3D vertex = new Vertex3D(x, y, z);
                    vertexList.Add(vertex);
                }
                pointList.Add(slicePointList);
            }

            for (int iZ = 1; iZ < horizontalBezierCurveList.Count; iZ++)
            {
                int iSumPrevious = (iZ - 1) * numberOfLongitudePoints;
                int iSum = iZ * numberOfLongitudePoints;
                for (int iPhi = 0; iPhi < numberOfLongitudePoints; iPhi++)
                {
                    if (iZ % 2 == 0)
                    {
                        int i1 = iSumPrevious + iPhi;
                        int i3 = iSum + iPhi;
                        int i2 = iSumPrevious + iPhi + 1;


                        if ((iPhi + 1) >= numberOfLongitudePoints) { i2 -= numberOfLongitudePoints; }
                        TriangleIndices triangleIndices1 = new TriangleIndices(i1, i2, i3);
                        triangleIndicesList.Add(triangleIndices1);
                        i1 = iSum + iPhi;
                        i3 = iSum + iPhi + 1;
                        i2 = iSumPrevious + iPhi + 1;
                        if ((iPhi + 1) >= numberOfLongitudePoints)
                        {
                            i2 -= numberOfLongitudePoints;
                            i3 -= numberOfLongitudePoints;
                        }
                        TriangleIndices triangleIndices2 = new TriangleIndices(i1, i2, i3);
                        triangleIndicesList.Add(triangleIndices2);
                    }
                    else
                    {
                        int i1 = iSumPrevious + iPhi;
                        int i3 = iSum + iPhi + 1;
                        int i2 = iSumPrevious + iPhi + 1;
                        if ((iPhi + 1) >= numberOfLongitudePoints)
                        {
                            i2 -= numberOfLongitudePoints;
                            i3 -= numberOfLongitudePoints;
                        }
                        TriangleIndices triangleIndices1 = new TriangleIndices(i1, i2, i3);
                        triangleIndicesList.Add(triangleIndices1);
                        i1 = iSum + iPhi;
                        i3 = iSum + iPhi + 1;
                        i2 = iSumPrevious + iPhi;
                        if ((iPhi + 1) >= numberOfLongitudePoints)
                        {
                            i3 -= numberOfLongitudePoints;
                        }
                        TriangleIndices triangleIndices2 = new TriangleIndices(i1, i2, i3);
                        triangleIndicesList.Add(triangleIndices2);
                    }  
                }
            }
            GenerateTriangleConnectionLists();
            ComputeTriangleNormalVectors();
            ComputeVertexNormalVectors();
        }

        public void MoveZ(int index, double deltaZ)
        {
            BezierCurve bezierCurve = horizontalBezierCurveList[index];
            foreach (BezierSpline bezierSpline in bezierCurve.SplineList)
            {
                foreach (PointND controlPoint in bezierSpline.ControlPointList)
                {
                    controlPoint.CoordinateList[2] += deltaZ;
                }
            }
        }

        public double GetZ(int index)
        {
            double z = horizontalBezierCurveList[index].SplineList[0].ControlPointList[0].CoordinateList[2];
            return z;
        }

        [DataMember]
        public List<BezierCurve> HorizontalBezierCurveList
        {
            get { return horizontalBezierCurveList; }
            set { horizontalBezierCurveList = value; }
        }

        [DataMember]
        public int NumberOfLongitudePoints
        {
            get { return numberOfLongitudePoints; }
            set { numberOfLongitudePoints = value; }
        }
    }
}
