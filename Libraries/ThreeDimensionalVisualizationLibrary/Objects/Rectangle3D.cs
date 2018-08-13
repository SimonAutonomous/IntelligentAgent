using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ThreeDimensionalVisualizationLibrary.Objects
{
    public class Rectangle3D : Object3D
    {
        private double sideLength1;
        private double sideLength2;

        public override void Generate(List<double> parameterList)
        {
            base.Generate(parameterList);
            if (parameterList == null) { return; }
            if (parameterList.Count < 2) { return; }
            sideLength1 = parameterList[0];
            sideLength2 = parameterList[1];
            Vertex3D vertex1 = new Vertex3D(-sideLength1 / 2, -sideLength2 / 2, 0);
            Vertex3D vertex2 = new Vertex3D(sideLength1 / 2, -sideLength2 / 2, 0);
            Vertex3D vertex3 = new Vertex3D(sideLength1 / 2, sideLength2 / 2, 0);
            Vertex3D vertex4 = new Vertex3D(-sideLength1 / 2, sideLength2 / 2, 0);
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
            ComputeVertexNormalVectors();
        }
    }
}
