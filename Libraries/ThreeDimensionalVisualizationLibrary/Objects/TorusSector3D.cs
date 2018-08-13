using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreeDimensionalVisualizationLibrary.Objects
{
    public class TorusSector3D: Object3D
    {
        private double majorRadius;
        private double minorRadius;
        private int numberOfMajorPoints;
        private int numberOfMinorPoints;
        private double startAngle;
        private double endAngle;

        public override void Generate(List<double> parameterList)
        {
            base.Generate(parameterList);
            if (parameterList == null) { return; }
            if (parameterList.Count < 6) { return; }
            majorRadius = parameterList[0];
            minorRadius = parameterList[1];
            numberOfMajorPoints = (int)Math.Round(parameterList[2]);
            numberOfMinorPoints = (int)Math.Round(parameterList[3]);
            if (numberOfMajorPoints < 2) { return; }
            if (numberOfMinorPoints < 1) { return; }
            startAngle = parameterList[4];
            endAngle = parameterList[5];
            if (startAngle < 0) { return; }
            if (endAngle > (2*Math.PI)) {return;}
            if (endAngle < startAngle) { return; }
            double deltaMajorAngle = (endAngle - startAngle) / (numberOfMajorPoints - 1);
            double deltaMinorAngle = (2 * Math.PI) / numberOfMinorPoints;
            for (int ii = 0; ii < numberOfMajorPoints; ii++)
            {
                double majorAngle = startAngle + ii * deltaMajorAngle;
                for (int jj = 0; jj < numberOfMinorPoints; jj++)
                {
                    double minorAngle = jj * deltaMinorAngle;
                    double x = Math.Cos(majorAngle) * (majorRadius + minorRadius * Math.Cos(minorAngle));
                    double y = Math.Sin(majorAngle) * (majorRadius + minorRadius * Math.Cos(minorAngle));
                    double z = minorRadius * Math.Sin(minorAngle);
                    Vertex3D vertex = new Vertex3D(x, y, z);
                    vertexList.Add(vertex);
                }
            }

            for (int ii = 1; ii < numberOfMajorPoints; ii++)
            {
                int iSumPrevious = (ii-1) * numberOfMinorPoints;
                int iSum = ii * numberOfMinorPoints;
                double majorAngle = startAngle + ii * deltaMajorAngle;
                if (majorAngle >= 2 * Math.PI) { iSum = 0; }
                for (int jj = 0; jj < numberOfMinorPoints; jj++)
                {
                    int i1 = iSumPrevious + jj;
                    int i2 = iSum + jj;
                    int i3 = iSum + jj + 1;
                    if ((jj + 1) >= numberOfMinorPoints) { i3 -= numberOfMinorPoints; }
                    TriangleIndices triangleIndices1 = new TriangleIndices(i1, i2, i3);
                    triangleIndicesList.Add(triangleIndices1);
                    i2 = i3;
                    i3 = iSumPrevious + (jj + 1);
                    if ((jj+1) >= numberOfMinorPoints) {i3 -= numberOfMinorPoints;}
                    TriangleIndices triangleIndices2 = new TriangleIndices(i1, i2, i3);
                    triangleIndicesList.Add(triangleIndices2);
                }
            }
            GenerateTriangleConnectionLists();
            ComputeTriangleNormalVectors();
            ComputeVertexNormalVectors();
        }
    }
}
