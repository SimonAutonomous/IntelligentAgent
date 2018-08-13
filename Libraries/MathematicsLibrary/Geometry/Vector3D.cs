using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathematicsLibrary.Geometry
{
    public class Vector3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector3D()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }

        public Vector3D(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector3D(Vector3D vector)
        {
            this.X = vector.X;
            this.Y = vector.Y;
            this.Z = vector.Z;
        }

        public static Vector3D FromPoints(Point3D point1, Point3D point2)
        {
            Vector3D vector = new Vector3D(point2.X, point2.Y, point2.Z);
            vector.X -= point1.X;
            vector.Y -= point1.Y;
            vector.Z -= point1.Z;
            return vector;
        }

        public static Vector3D Cross(Vector3D vector1, Vector3D vector2)
        {
            Vector3D crossProduct = new Vector3D();
            crossProduct.X = vector1.Y * vector2.Z - vector2.Y * vector1.Z;
            crossProduct.Y = vector1.Z * vector2.X - vector2.Z * vector1.X;
            crossProduct.Z = vector1.X * vector2.Y - vector2.X * vector1.Y;
            return crossProduct;
        }

        public void Add(Vector3D addedVector)
        {
            this.X += addedVector.X;
            this.Y += addedVector.Y;
            this.Z += addedVector.Z;
        }

        public void Normalize()
        {
            double length = GetLength();
            if (length > 0)
            {
                X /= length;
                Y /= length;
                Z /= length;
            }
        }

        public double GetLength()
        {
            double length = Math.Sqrt(X * X + Y * Y + Z * Z);
            return length;
        }
    }
}
