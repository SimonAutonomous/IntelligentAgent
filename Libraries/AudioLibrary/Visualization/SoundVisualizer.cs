using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;
using CustomUserControlsLibrary;

namespace AudioLibrary.Visualization
{
    public partial class SoundVisualizer : HorizontalScrollableZoomControl
    {
        protected WAVSound sound = null;
        private List<WAVSound> soundSequenceList = null; // Previous sounds - for undo capabilities.
        private List<SoundMarker> markerList = null;
        private const int MINIMUM_SAMPLE_VALUE = -32768;
        private const int MAXIMUM_SAMPLE_VALUE = 32767;

        private Color soundColor = Color.LightSkyBlue;
        private Color axisColor = Color.LightGray;

        private Boolean horizontalAxisVisible = true;
        private float tickMarkSpacing = 0.010f; // Default value
        private float tickMarkRelativeLength = 0.02f; // Fraction of window height.
        private List<float> horizontalTickMarkList = null;

        public event EventHandler AssignedSoundChanged = null;


        private Boolean selecting = false;
        private float selectionStart;
        private float selectionEnd;

        public SoundVisualizer()
        {
            InitializeComponent();
            yMin = MINIMUM_SAMPLE_VALUE;
            yMax = MAXIMUM_SAMPLE_VALUE;
            DetermineVisibleArea();
        }

        private void OnAssignedSoundChanged()
        {
            if (AssignedSoundChanged != null)
            {
                EventHandler handler = AssignedSoundChanged;
                handler(this, EventArgs.Empty);
            }
        }

        public void ClearHistory()
        {
            soundSequenceList = new List<WAVSound>();
        }

        public void SetSound(WAVSound sound)
        {
            this.sound = sound;
            if (sound == null) { return; } // 20160912
            if (soundSequenceList == null) { soundSequenceList = new List<WAVSound>(); }
            soundSequenceList.Add(this.sound.Copy());
            this.xMin = 0;
            this.xMax = (float)sound.GetDuration();
            scrollbarVisible = false;
            SetRange(xMin, xMax, MINIMUM_SAMPLE_VALUE, MAXIMUM_SAMPLE_VALUE);
            OnViewingAreaChanged();
            horizontalTickMarkList = new List<float>();
            float tickMarkPosition = (float)xMin;
            while (tickMarkPosition <= xMax)
            {
                horizontalTickMarkList.Add(tickMarkPosition);
                tickMarkPosition += tickMarkSpacing;
            }
            OnAssignedSoundChanged();
            Invalidate();
        }

        protected void DrawHorizontalAxis(Graphics g)
        {
            using (Pen axisPen = new Pen(axisColor))
            {
                float yAxis = 0;
                float yPlot = GetPlotYAtY(yAxis); // GetPixelYatY(yAxis);
                float xPlotMin = GetPlotXAtX(xViewMin); // GetPixelXatX(plotLeft);
                float xPlotMax = GetPlotXAtX(xViewMax);  // GetPixelXatX(plotRight);
                g.DrawLine(axisPen, xPlotMin, yPlot, xPlotMax, yPlot);
                g.DrawLine(axisPen, xPlotMin, mainPanelHeight - 1, xPlotMax, mainPanelHeight - 1);
                float tickMarkAbsoluteLength = tickMarkRelativeLength * mainPanelHeight;
                if (horizontalTickMarkList == null) { return; }
                foreach (float horizontalTickMark in horizontalTickMarkList)
                {
                    float xPlot = GetPlotXAtX(horizontalTickMark);
                    g.DrawLine(axisPen, xPlot, mainPanelHeight - 1, xPlot, mainPanelHeight - tickMarkAbsoluteLength);
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (horizontalAxisVisible)
            {
                DrawHorizontalAxis(e.Graphics);
            }
            if (selecting) { DrawSelection(e.Graphics); }
        }

        protected void DrawMarker(Graphics g, SoundMarker marker)
        {
            using (Pen markerPen = new Pen(Color.Empty))
            {
                markerPen.Color = marker.Color;
                markerPen.Width = marker.Thickness;
                if (marker.Type == SoundMarkerType.HorizontalLine)
                {
                    float yPlot = GetPlotYAtY(marker.Level);
                    float xPlotStart = GetPlotXAtX(marker.Start);
                    float xPlotEnd = GetPlotXAtX(marker.End);
                    g.DrawLine(markerPen, xPlotStart, yPlot, xPlotEnd, yPlot);
                }
                else if (marker.Type == SoundMarkerType.VerticalLine)
                {
                    float xPlot = GetPlotXAtX(marker.Level);
                    float yPlotStart = GetPlotYAtY(marker.Start);
                    float yPlotEnd = GetPlotYAtY(marker.End);
                    g.DrawLine(markerPen, xPlot, yPlotStart, xPlot, yPlotEnd);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (sound == null) { return; }
            if (sound.Samples == null) { return; }
            float xPrevious;
            float yPrevious;
            float x;
            float y;
            using (Pen soundPen = new Pen(soundColor))
            {
                for (int ii = 1; ii < sound.Samples[0].Count; ii++)  // Assume mono sound, for now
                {
                    xPrevious = (float)sound.GetTimeAtSampleIndex(ii - 1);
                    x = (float)sound.GetTimeAtSampleIndex(ii);
                    yPrevious = (float)sound.Samples[0][ii - 1];
                    y = (float)sound.Samples[0][ii];
                    float xPlotPrevious = GetPlotXAtX(xPrevious);
                    float xPlot = GetPlotXAtX(x);
                    float yPlotPrevious = GetPlotYAtY(yPrevious);
                    float yPlot = GetPlotYAtY(y);
                    if (xPlot > xPlotPrevious) { e.Graphics.DrawLine(soundPen, xPlotPrevious, yPlotPrevious, xPlot, yPlot); }
                }
            }
            if (markerList != null)
            {
                foreach (SoundMarker marker in markerList)
                {
                    DrawMarker(e.Graphics, marker);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Right)
            {
                    if (!selecting)
                    {
                        selecting = true;
                        selectionStart = e.X;
                    }
                    else
                    {
                        selectionEnd = e.X;
                        Invalidate();
                    }
            }
        }

        private void DrawSelection(Graphics g)
        {
            using (SolidBrush selectionBrush = new SolidBrush(Color.FromArgb(128, Color.Gray)))
            {
                g.FillRectangle(selectionBrush, selectionStart, 0, (selectionEnd-selectionStart), mainPanelHeight);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (selecting)
            {
                selecting = false;
                selectionEnd = e.X;
                if (selectionEnd < selectionStart)
                {
                    float tmp = selectionStart;
                    selectionStart = selectionEnd;
                    selectionEnd = tmp;
                }
                double startTime = GetXAtPlotX(selectionStart);
                double endTime = GetXAtPlotX(selectionEnd);

                float tstStart = GetPlotXAtX((float)startTime);
                float tstEnd = GetPlotXAtX((float)endTime);

                WAVSound selectedSound = this.sound.Extract(startTime, endTime);
                this.SetSound(selectedSound);
                OnAssignedSoundChanged();
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (soundSequenceList != null)
            {
                if (soundSequenceList.Count > 1)
                {
                    soundSequenceList.RemoveAt(soundSequenceList.Count - 1);
                    WAVSound previousSound = soundSequenceList[soundSequenceList.Count - 1];
                    SetSound(previousSound); 
                    // In this case (reverting to a previous sound), the sound that is
                    // displayed should not also be ADDED to the soundsequenceList:
                    soundSequenceList.RemoveAt(soundSequenceList.Count - 1);
                }
            }
        }

        public List<SoundMarker> MarkerList
        {
            get { return markerList; }
            set { markerList = value; }
        }

        public WAVSound Sound
        {
            get { return sound; }
        }
    }
}
