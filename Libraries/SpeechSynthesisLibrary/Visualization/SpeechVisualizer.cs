using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AudioLibrary;
using AudioLibrary.Visualization; // SoundMarker
using CustomUserControlsLibrary;
using SpeechSynthesisLibrary.TDPSOLA;

namespace SpeechSynthesisLibrary.Visualization
{
    public partial class SpeechVisualizer : HorizontalScrollableZoomControl
    {
        private const int DEFAULT_DIVIDER_HEIGHT = 4;
        private const double DEFAULT_SOUND_PANEL_FRACTION = 0.75;
        private const int HEIGHT_MARGIN = 50;
        private const double Y_MIN_SOUND = -32768;
        private const double Y_MAX_SOUND = 32768;
        private const double Y_MIN_PITCH = 0;
        private const double Y_MAX_PITCH = 0.02;
        private const float DEFAULT_PITCH_MARKER_RADIUS = 4.0f;

        private Boolean pitchPanelVisible = true;
        private float dividerHeight = DEFAULT_DIVIDER_HEIGHT;
        private double soundPanelFraction = DEFAULT_SOUND_PANEL_FRACTION;
        private Color panelBackColor = Color.DimGray;
        private Color dividerColor = Color.Black;
        private Color axisColor = Color.Silver;
        private float nominalTopPanelHeight;
        private float soundPanelHeight;
        private float pitchPanelHeight = 0;
        private float axisLocation;

   //     private PointF grabPoint;
        private Boolean dividedGrabbed = false;

        private WAVSound sound = null;
     //   private List<double> pitchList = null;

        private double firstVisibleTime;
        private double lastVisibleTIme;
        private Color soundColor = Color.LightBlue;
   //     private double scaleX;
        private double soundScaleY;
        private double pitchScaleY;
        private Color pitchColor = Color.Orange;
        private float pitchMarkerRadius = DEFAULT_PITCH_MARKER_RADIUS;

        private double tickMarkSpacing = 0.010; // Default value
        private double tickMarkRelativeLength = 0.02; // Fraction of panel height.
        private List<double> horizontalTickMarkList = null;
        private double pitchPeriodSpacing = 0.005;
        private const float HORIZONTAL_MARGIN = 4f;

        private PitchPeriodSpecification pitchPeriodSpecification = null;

        private List<SoundMarker> markerList = null;

        public SpeechVisualizer()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                Initialize();
            }
        }

    /*    private void Initialize()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.AutoScaleMode = AutoScaleMode.Font;
        }  */


        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (sound != null)
            {
                if (pitchPanelVisible)
                {
                    soundPanelHeight = nominalTopPanelHeight - dividerHeight;
                    if (soundPanelHeight < 0) { soundPanelHeight = 0; }
                    pitchPanelHeight = this.Height - soundPanelHeight - dividerHeight;
                    if (scrollbarVisible) { pitchPanelHeight -= SCROLLBAR_HEIGHT; }
                }
                else
                {
                    soundPanelHeight = this.Height;
                    pitchPanelHeight = 0;
                }
                using (SolidBrush backgroundBrush = new SolidBrush(panelBackColor))
                {
                    e.Graphics.FillRectangle(backgroundBrush, 0, 0, this.Width, this.Height);
                }
                using (SolidBrush dividerBrush = new SolidBrush(dividerColor))
                {
                    e.Graphics.FillRectangle(dividerBrush, 0, soundPanelHeight, this.Width, dividerHeight);
                }
                using (SolidBrush axisBrush = new SolidBrush(axisColor))
                {
                    using (Pen axisPen = new Pen(axisColor))
                    {
                        axisPen.Width = 1.0f;
                        axisLocation = soundPanelHeight / 2;
                        e.Graphics.DrawLine(axisPen, 0, axisLocation, this.Width, axisLocation);
                        if (horizontalTickMarkList == null) { return; }
                        float tickMarkAbsoluteLength = (float)tickMarkRelativeLength * mainPanelHeight;
                        foreach (float horizontalTickMark in horizontalTickMarkList)
                        {
                            float xPlot = GetPlotXAtX(horizontalTickMark);
                            e.Graphics.DrawLine(axisPen, xPlot, soundPanelHeight - 1, xPlot, soundPanelHeight - tickMarkAbsoluteLength);
                            e.Graphics.DrawLine(axisPen, xPlot, mainPanelHeight - 1, xPlot, mainPanelHeight - tickMarkAbsoluteLength);
                        }
                        // Vertical axis in the pitch period panel
                        double pitchPeriod = 0;
                        Boolean belowTop = true;
                        while (belowTop)
                        {
                            pitchPeriod += pitchPeriodSpacing;
                            float yPlot = GetPitchPlotYatY(pitchPeriod);
                            float stringHeight = e.Graphics.MeasureString(pitchPeriod.ToString("0.000"), this.Font).Height;
                            float stringWidth = e.Graphics.MeasureString(pitchPeriod.ToString("0.000"), this.Font).Width;
                            if (yPlot <= (soundPanelHeight + dividerHeight)) { belowTop = false; }
                            else if (yPlot <= mainPanelHeight)
                            {
                                e.Graphics.DrawLine(axisPen, 0, yPlot, tickMarkAbsoluteLength, yPlot);
                                float top = yPlot - stringHeight / 2;
                                if (top > 0)
                                {
                                    e.Graphics.DrawString(pitchPeriod.ToString("0.0000"), this.Font, axisBrush, tickMarkAbsoluteLength + HORIZONTAL_MARGIN, yPlot - stringHeight / 2);
                                }
                                e.Graphics.DrawLine(axisPen, tickMarkAbsoluteLength + stringWidth + 2* HORIZONTAL_MARGIN, yPlot, mainPanelWidth, yPlot);
                            }
                        }
                    }
                }
            }
            else
            {
                using (SolidBrush backgroundBrush = new SolidBrush(panelBackColor))
                {
                    e.Graphics.FillRectangle(backgroundBrush, 0, 0, this.Width, this.Height);
                }
            }
        }

        protected override void SetScale()
        {
            base.SetScale();
            firstVisibleTime = xViewMin;
            lastVisibleTIme = xViewMax;
            scaleX = this.Width / (lastVisibleTIme - firstVisibleTime);
            soundScaleY = soundPanelHeight / (Y_MAX_SOUND - Y_MIN_SOUND);
            pitchScaleY = pitchPanelHeight / (Y_MAX_PITCH - Y_MIN_PITCH);
        }

        protected float GetSoundPlotXAtX(double x)
        {
            float plotX = (float)((x - firstVisibleTime) * scaleX);
            return plotX;
        }

        protected float GetSoundPlotYAtY(double y)
        {
            float plotY = soundPanelHeight - (float)((y - Y_MIN_SOUND) * soundScaleY);
            return plotY;
        }

        protected float GetPitchPlotYatY(double y)
        {
            float pitchPlotY = soundPanelHeight + pitchPanelHeight - (float)((y - Y_MIN_PITCH) * pitchScaleY);
            return pitchPlotY;
        }

        protected void DrawMarker(Graphics g, SoundMarker marker)
        {
            using (Pen markerPen = new Pen(Color.Empty))
            {
                markerPen.Color = marker.Color;
                markerPen.Width = marker.Thickness;
                if (marker.Type == SoundMarkerType.HorizontalLine)
                {
                    float yPlot = GetSoundPlotYAtY(marker.Level);
                    float xPlotStart = GetPlotXAtX(marker.Start);
                    float xPlotEnd = GetPlotXAtX(marker.End);
                    g.DrawLine(markerPen, xPlotStart, yPlot, xPlotEnd, yPlot);
                }
                else if (marker.Type == SoundMarkerType.VerticalLine)
                {
                    float xPlot = GetPlotXAtX(marker.Level);
                    float yPlotStart = GetSoundPlotYAtY(marker.Start);
                    float yPlotEnd = GetSoundPlotYAtY(marker.End);
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
                float x0 = (float)sound.GetTimeAtSampleIndex(0);
                float xPlot0 = GetSoundPlotXAtX(x0);
                for (int ii = 1; ii < sound.Samples[0].Count; ii++)  // Assume mono sound, for now
                {
                    xPrevious = (float)sound.GetTimeAtSampleIndex(ii - 1);
                    x = (float)sound.GetTimeAtSampleIndex(ii);
                    yPrevious = (float)sound.Samples[0][ii - 1];
                    y = (float)sound.Samples[0][ii];
                    float xPlotPrevious = GetSoundPlotXAtX(xPrevious);
                    float xPlot = GetSoundPlotXAtX(x);
                    float yPlotPrevious = GetSoundPlotYAtY(yPrevious);
                    float yPlot = GetSoundPlotYAtY(y);
                    if ((xPlot > 0) && (xPlot < mainPanelWidth))
                    {
                        if (xPlot > xPlotPrevious)
                        {
                            e.Graphics.DrawLine(soundPen, xPlotPrevious, yPlotPrevious, xPlot, yPlot);
                        }
                    }
                }
            }
            if (markerList != null)
            {
                foreach (SoundMarker marker in markerList)
                {
                    DrawMarker(e.Graphics, marker);
                }
            }
            if (pitchPeriodSpecification != null)
            {
                using (SolidBrush pitchBrush = new SolidBrush(pitchColor))
                {
                    for (int ii = 0; ii < pitchPeriodSpecification.TimePitchPeriodTupleList.Count; ii++)
                    {
                        double time = pitchPeriodSpecification.TimePitchPeriodTupleList[ii].Item1;
                        float xPlot = GetSoundPlotXAtX(time);
                        if ((xPlot > 0) && (xPlot < mainPanelWidth))
                        {
                            double pitchPeriod = pitchPeriodSpecification.TimePitchPeriodTupleList[ii].Item2;
                        //    double pitchPeriod = 1.0 / pitch;
                            float yPlot = GetPitchPlotYatY(pitchPeriod);
                            e.Graphics.FillRectangle(pitchBrush, xPlot - 2, yPlot - 2, 4, 4);
                        }
                    }
                }
            }
        }

        protected Boolean IsInDivider(float y)
        {
            if ((y > soundPanelHeight) && (y <= soundPanelHeight + dividerHeight)) { return true; }
            else { return false; }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                if (IsInDivider(e.Y))
                {
                    dividedGrabbed = true;
                    grabPoint = new Point(e.X, e.Y);
                    Cursor = Cursors.HSplit;
                    Invalidate();
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (dividedGrabbed)
            {
                Cursor = Cursors.HSplit;
                nominalTopPanelHeight += (e.Y - grabPoint.Y);
                if (nominalTopPanelHeight < HEIGHT_MARGIN) { nominalTopPanelHeight = HEIGHT_MARGIN; }
                else if (nominalTopPanelHeight > (this.Height - HEIGHT_MARGIN)) { nominalTopPanelHeight = this.Height - HEIGHT_MARGIN; }
                grabPoint.Y = e.Y;
                SetScale();
                Invalidate();
            }
            else
            {
                SetScale();
                PlotSoundAndPitch();
                Cursor = Cursors.Default;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            dividedGrabbed = false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.sound != null)  //&& (this.pitchList != null))
            {
                PlotSoundAndPitch(); // Easiest way to scale correctly!
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (scrollbarVisible) { pitchPanelHeight = this.Height - soundPanelHeight - dividerHeight - SCROLLBAR_HEIGHT; }
            else { pitchPanelHeight = this.Height - soundPanelHeight - dividerHeight; }
            if (pitchPanelHeight < 0) { pitchPanelHeight = 0; }
            PlotSoundAndPitch();
        }

        private void PlotSoundAndPitch()
        {
        //    nominalTopPanelHeight = (int)Math.Round(soundPanelFraction * this.Height);
        //    soundPanelHeight = nominalTopPanelHeight - dividerHeight;            
            firstVisibleTime = xViewMin; // 0;
            lastVisibleTIme = xViewMax; // sound.GetDuration();
            SetScale();
            Refresh();
       //     Invalidate();
        }

        public void SetSound(WAVSound sound)
        {
            this.sound = sound;
            if (sound == null)
            {
                Refresh();
                return;
            }
            SetRange(0, sound.GetDuration(), -32768, 32768);
            horizontalTickMarkList = new List<double>();
            if (!pitchPanelVisible) { soundPanelFraction = 1; }
            nominalTopPanelHeight = (int)Math.Round(soundPanelFraction * this.Height);
            soundPanelHeight = nominalTopPanelHeight - dividerHeight;
            pitchPanelHeight = this.Height - soundPanelHeight - dividerHeight;
            double tickMarkPosition = xMin;
            while (tickMarkPosition <= xMax)
            {
                horizontalTickMarkList.Add(tickMarkPosition);
                tickMarkPosition += tickMarkSpacing;
            }
            this.zoomLevel = 1;
            scrollbarVisible = false;
            PlotSoundAndPitch();
        }

        public void SetPitchPeriodSpecification(PitchPeriodSpecification pitchPeriodSpecification)
        {
            this.pitchPeriodSpecification = pitchPeriodSpecification;
            pitchPanelHeight = this.Height - soundPanelHeight - dividerHeight;
            PlotSoundAndPitch();
        }

        public void SetTimePitchPeriodList(List<List<double>> timePitchPeriodList)
        {
            this.pitchPeriodSpecification = new PitchPeriodSpecification();
            this.pitchPeriodSpecification.GenerateFromList(timePitchPeriodList);
            PlotSoundAndPitch();
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

        public Boolean PitchPanelVisible
        {
            get { return pitchPanelVisible; }
            set { pitchPanelVisible = value; }
        }

    }
}
