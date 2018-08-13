using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CustomUserControlsLibrary;

namespace PlotLibrary
{
    public class PlotPanel
    {
        public int Top = 0;
        public int Height = 0;
        public float YMin;
        public float YMax;
        public float XMin;
        public float XMax;
        public Boolean HorizontalAxisVisible = true;
        public Color AxisColor = Color.LightGray;
        public float HorizontalTickMarkSpacing = 0.010f; // Default value
        public float TickMarkRelativeLength = 0.04f; // Fraction of window height.
        public List<float> HorizontalTickMarkList = null;
        public List<string> HorizontalTickLabelList = null;
        public float AxesOriginX = 0;
        public float AxesOriginY = 0;
        public float MinimumLabelSpacing = 5; // Default value, ToDo: introduce a constant.

        private HorizontalScrollableZoomControl ownerControl;
        private float scaleY;
    }
}
