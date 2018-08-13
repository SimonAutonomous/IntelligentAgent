using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using MathematicsLibrary.Geometry;

namespace ThreeDimensionalVisualizationLibrary.Objects
{
    public class SphereSegment3D : Object3D
    {
        private double radius;
        private int numberOfLatitudePoints;
        private int numberOfLongitudePoints;
        private double latitudeLimit;

        public override void Generate(List<double> parameterList)
        {
            base.Generate(parameterList);
            if (parameterList == null) { return; }
            if (parameterList.Count < 4) { return; }
            radius = parameterList[0];
            numberOfLatitudePoints = (int)Math.Round(parameterList[1]);
            numberOfLongitudePoints = (int)Math.Round(parameterList[2]);
            latitudeLimit = parameterList[3];
            double deltaLatitude = (Math.PI/2 - latitudeLimit) / (double)(numberOfLatitudePoints - 1);
            double deltaLongitude = 2*Math.PI / (double)numberOfLongitudePoints;

            List<List<List<double>>> pointList = new List<List<List<double>>>();


            for (int iZ = 0; iZ < numberOfLatitudePoints; iZ++)
            {
                double theta = Math.PI/2 - iZ * deltaLatitude;
                List<List<double>> latitudePointList = new List<List<double>>();
                for (int iPhi = 0; iPhi < numberOfLongitudePoints; iPhi++)
                {
                    double phi = iPhi * deltaLongitude;
                    if (iZ % 2 == 0) { phi += deltaLongitude / 2; }
                    if (phi > 2 * Math.PI) { phi -= 2 * Math.PI; }
                    double x = radius * Math.Cos(theta) * Math.Cos(phi);
                    double y = radius * Math.Cos(theta) * Math.Sin(phi);
                    double z = radius * Math.Sin(theta);
                    latitudePointList.Add(new List<double>() { x, y, z });

                    Vertex3D vertex = new Vertex3D(x, y, z);

                    double u = phi / (2 * Math.PI);
                    double v = 1 - (theta + Math.PI / 2) / Math.PI;
                    vertex.TextureCoordinates = new Point2D(u, v);

                    vertexList.Add(vertex);

                }
                pointList.Add(latitudePointList);
            }

            for (int iZ = 1; iZ < numberOfLatitudePoints; iZ++)
            {
                int iSumPrevious = (iZ - 1) * numberOfLongitudePoints;
                int iSum = iZ * numberOfLongitudePoints;
                for (int iPhi = 0; iPhi < numberOfLongitudePoints; iPhi++)
                {
                    if (iZ % 2 == 0)
                    {
                        int i1 = iSumPrevious + iPhi;
                        int i2 = iSum + iPhi;
                        int i3 = iSumPrevious + iPhi + 1;
                        if ((iPhi + 1) >= numberOfLongitudePoints) { i3 -= numberOfLongitudePoints; }
                        TriangleIndices triangleIndices1 = new TriangleIndices(i1, i2, i3);
                        triangleIndicesList.Add(triangleIndices1);
                        i1 = iSum + iPhi;
                        i2 = iSum + iPhi + 1;
                        i3 = iSumPrevious + iPhi + 1;
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
                        int i2 = iSum + iPhi + 1;
                        int i3 = iSumPrevious + iPhi + 1;
                        if ((iPhi + 1) >= numberOfLongitudePoints)
                        {
                            i2 -= numberOfLongitudePoints;
                            i3 -= numberOfLongitudePoints;
                        }  
                        TriangleIndices triangleIndices1 = new TriangleIndices(i1, i2, i3);
                        triangleIndicesList.Add(triangleIndices1);
                        i1 = iSum + iPhi;
                        i2 = iSum + iPhi + 1;
                        i3 = iSumPrevious + iPhi;
                        if ((iPhi + 1) >= numberOfLongitudePoints)
                        {
                            i2 -= numberOfLongitudePoints;
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
    }
}
