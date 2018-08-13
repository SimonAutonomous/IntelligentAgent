﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathematicsLibrary.Geometry;

namespace ThreeDimensionalVisualizationLibrary.Objects
{
    public class Square3D: Object3D
    {
        private double sideLength;

        public override void Generate(List<double> parameterList)
        {
            base.Generate(parameterList);

            if (parameterList == null) { return; }
            if (parameterList.Count < 1) { return; }
            double sideLength = parameterList[0];

            // Top
            Vertex3D vertex1 = new Vertex3D(-sideLength / 2, -sideLength / 2, 0);
            Vertex3D vertex2 = new Vertex3D(sideLength / 2, -sideLength / 2, 0);
            Vertex3D vertex3 = new Vertex3D(sideLength / 2, sideLength / 2, 0);
            Vertex3D vertex4 = new Vertex3D(-sideLength / 2, sideLength / 2, 0);
            vertexList.Add(vertex1);
            vertexList.Add(vertex2);
            vertexList.Add(vertex3);
            vertexList.Add(vertex4);
            TriangleIndices triangleIndices1 = new TriangleIndices(0, 1, 2);
            triangleIndicesList.Add(triangleIndices1);
            TriangleIndices triangleIndices2 = new TriangleIndices(0, 2, 3);
            triangleIndicesList.Add(triangleIndices2);


            GenerateTriangleConnectionLists();
            ComputeTriangleNormalVectors();
        }
    }
}
