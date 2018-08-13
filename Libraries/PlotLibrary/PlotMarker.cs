using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PlotLibrary
{
    public class PlotMarker
    {
        public PlotMarkerType Type { get; set; }
        public float Thickness { get; set; }
        public Color Color { get; set; }
        public float Start { get; set; }
        public float End { get; set; }
        public float Level { get; set; } // The vertical coordinate for a horizontal line, the horizontal coordinate for a vertical line.
    }
}