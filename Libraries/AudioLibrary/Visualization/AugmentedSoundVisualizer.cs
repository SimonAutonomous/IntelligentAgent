using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CustomUserControlsLibrary;

namespace AudioLibrary.Visualization
{
    public partial class AugmentedSoundVisualizer : HorizontalScrollableZoomControl
    {
        private const int DEFAULT_DIVIDER_HEIGHT = 4;
        private const double DEFAULT_SOUND_PANEL_FRACTION = 0.75;
        private const int HEIGHT_MARGIN = 50;
        private const double Y_MIN_SOUND = -32768;
        private const double Y_MAX_SOUND = 32768;
        private const double Y_MIN_PITCH = 0;
        private const double Y_MAX_PITCH = 200;
        private const float DEFAULT_PITCH_MARKER_RADIUS = 4.0f;

        private Boolean pitchPanelVisible = true;
        private float dividerHeight = DEFAULT_DIVIDER_HEIGHT;
        private double soundPanelFraction = DEFAULT_SOUND_PANEL_FRACTION;
        private Color panelBackColor = Color.DimGray;
        private Color dividerColor = Color.Black;
        private Color axisColor = Color.Silver;
        private float nominalTopPanelHeight;
        private float soundPanelHeight;
        private float pitchPanelHeight;
        private float axisLocation;

        private PointF grabPoint;
        private Boolean dividedGrabbed = false;

        private WAVSound sound = null;
        private List<double> pitchList = null;

        private double firstVisibleTime;
        private double lastVisibleTIme;
        private Color soundColor = Color.LightBlue;
        private double scaleX;
        private double soundScaleY;
        private double pitchScaleY;
        private Color pitchColor = Color.Orange;
        private float pitchMarkerRadius = DEFAULT_PITCH_MARKER_RADIUS;

        public AugmentedSoundVisualizer()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                Initialize();
            }
        }

        private void Initialize()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.AutoScaleMode = AutoScaleMode.Font;
        }


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
                using (Pen axisPen = new Pen(axisColor))
                {
                    axisPen.Width = 1.0f;
                    axisLocation = soundPanelHeight / 2;
                    e.Graphics.DrawLine(axisPen, 0, axisLocation, this.Width, axisLocation);
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
                using (SolidBrush pitchBrush = new SolidBrush(pitchColor))
                {
                    float x0 = (float)sound.GetTimeAtSampleIndex(0);
                    float xPlot0 = GetSoundPlotXAtX(x0);
                    //       if (xPlot0 >= 0)
                    //       {
                    if (pitchList != null)
                    {
                        if (pitchList[0] > 0)
                        {
                            float yPlotPitch0 = GetPitchPlotYatY(pitchList[0]);
                            e.Graphics.FillEllipse(pitchBrush, xPlot0 - pitchMarkerRadius, yPlotPitch0 - pitchMarkerRadius, 2 * pitchMarkerRadius, 2 * pitchMarkerRadius);
                        }
                    }
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
                        if (xPlot > xPlotPrevious)
                        {
                            e.Graphics.DrawLine(soundPen, xPlotPrevious, yPlotPrevious, xPlot, yPlot);
                            if (pitchList != null)
                            {
                                if (pitchList[ii] > 0)
                                {
                                    float yPitch = (float)pitchList[ii];
                                    float yPlotPitch = GetPitchPlotYatY(yPitch);
                                    e.Graphics.FillEllipse(pitchBrush, xPlot - pitchMarkerRadius, yPlotPitch - pitchMarkerRadius, 2 * pitchMarkerRadius, 2 * pitchMarkerRadius);
                                }
                            }
                        }
                    }
            //        }
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
                    grabPoint = new PointF(e.X, e.Y);
                    Cursor = Cursors.HSplit;
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
            if ((this.sound != null) && (this.pitchList != null))
            {
                PlotSoundAndPitch(); // Easiest way to scale correctly!
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            PlotSoundAndPitch();
        }

        private void PlotSoundAndPitch()
        {
            nominalTopPanelHeight = (int)Math.Round(soundPanelFraction * this.Height);
            soundPanelHeight = nominalTopPanelHeight - dividerHeight;
            pitchPanelHeight = this.Height - soundPanelHeight - dividerHeight;
            firstVisibleTime = xViewMin; // 0;
            lastVisibleTIme = xViewMax; // sound.GetDuration();
            SetScale();
            Invalidate();
        }

        public void SetSound(WAVSound sound, List<double> pitchList)
        {
            this.sound = sound;
            this.pitchList = pitchList;
            SetRange(0, sound.GetDuration(), 0, 1);
            PlotSoundAndPitch();
        }
    }
}
