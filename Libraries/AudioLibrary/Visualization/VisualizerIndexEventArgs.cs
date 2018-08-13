using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioLibrary.Visualization
{
    public class VisualizerIndexEventArgs: EventArgs
    {
        private int xIndex;
        private int yIndex;

        public VisualizerIndexEventArgs(int xIndex, int yIndex)
        {
            this.xIndex = xIndex;
            this.yIndex = yIndex;
        }

        public int XIndex
        {
            get { return xIndex; }
        }

        public int YIndex
        {
            get { return yIndex; }
        }
    }
}
