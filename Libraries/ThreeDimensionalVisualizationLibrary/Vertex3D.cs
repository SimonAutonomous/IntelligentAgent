using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MathematicsLibrary.Geometry;

namespace ThreeDimensionalVisualizationLibrary
{
    public class Vertex3D
    {
        private Point3D position;
        private Color color;
        private Vector3D normalVector;
        private List<int> triangleConnectionList; // Indices of the triangles that include this vertex
        private Point2D textureCoordinates = null;

        public Vertex3D(double x, double y, double z)
        {
            position = new Point3D(x, y, z);
            color = Color.White;
            triangleConnectionList = new List<int>();
        }

        public Point3D Position
        {
            get { return position; }
        }

        public Point2D TextureCoordinates
        {
            get { return textureCoordinates; }
            set { textureCoordinates = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public Vector3D NormalVector
        {
            get { return normalVector; }
            set { normalVector = value; }
        }

        public List<int> TriangleConnectionList
        {
            get { return triangleConnectionList; }
            set { triangleConnectionList = value; }
        }
    }
}
