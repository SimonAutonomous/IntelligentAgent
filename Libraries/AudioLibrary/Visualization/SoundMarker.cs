using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AudioLibrary.Visualization
{
    public class SoundMarker
    {
        public SoundMarkerType Type { get; set; }
        public float Thickness { get; set; }
        public Color Color { get; set; }
        public double Start { get; set; }
        public double End { get; set; }
        public double Level { get; set; } // The vertical coordinate for a horizontal line, the horizontal coordinate for a vertical line.
    }
}
