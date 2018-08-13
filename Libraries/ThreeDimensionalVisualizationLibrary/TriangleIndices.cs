using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreeDimensionalVisualizationLibrary
{
    public class TriangleIndices
    {
        public int Index1 { get; set; }
        public int Index2 { get; set; }
        public int Index3 { get; set; }

        public TriangleIndices(int index1, int index2, int index3)
        {
            this.Index1 = index1;
            this.Index2 = index2;
            this.Index3 = index3;
        }
    }
}
