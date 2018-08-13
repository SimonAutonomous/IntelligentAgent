using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ThreeDimensionalVisualizationLibrary.Objects
{
    // To do: To be turned into an Object3D, once the Sphere object has been completed.
    public class EquilateralTriangle3D: Object3D
    {
        private double sideLength;
         
        public override void Generate(List<double> parameterList)
        {
            base.Generate(parameterList);
            if (parameterList.Count < 1) { return; }
            sideLength = parameterList[0];
            Vertex3D vertex1 = new Vertex3D(-sideLength / 2, 0, 0);
        //    vertex1.Color = Color.Red;
            Vertex3D vertex2 = new Vertex3D(sideLength / 2, 0, 0);
         //   vertex2.Color = Color.Green;
            Vertex3D vertex3 = new Vertex3D(0, sideLength * Math.Sqrt(3) / 2, 0);
         //   vertex3.Color = Color.Blue;
            vertexList.Add(vertex1);
            vertexList.Add(vertex2);
            vertexList.Add(vertex3);
            TriangleIndices triangleIndices1 = new TriangleIndices(0, 1, 2);
            triangleIndicesList.Add(triangleIndices1);
            GenerateTriangleConnectionLists();
            ComputeTriangleNormalVectors();
            ComputeVertexNormalVectors();
        }
    }
}
