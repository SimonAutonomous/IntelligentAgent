using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CustomUserControlsLibrary;

namespace PlotLibrary
{
    public partial class ScrollableMultiPanelPlotControl : HorizontalScrollableZoomControl
    {
        private List<Separator> separatorList = null;
        private List<PlotPanel> plotPanelList = null;
        private int separatorHeight = 4; // MW ToDo ...
        private Boolean draggingSeparator = false;
        private int selectedSeparatorIndex = -1;

        public ScrollableMultiPanelPlotControl()
        {
            InitializeComponent();
        }

        public void InsertPanel(int index)
        {
        }

        public void SetHorizontalRange(float xMin, float xMax)
        {
            SetRange(xMin, xMax, 0, 1); // yMin and yMax are not used for this control - set to dummy values.
        }

        public void AppendPanel(PlotPanel plotPanel)
        {
            plotPanel.SetOwnerControl(this);
            if (plotPanelList == null) { plotPanelList = new List<PlotPanel>(); }
            if (separatorList == null) { separatorList = new List<Separator>(); }
          //  PlotPanel plotPanel = new PlotPanel();
            Separator separator = new Separator();
            if (plotPanelList.Count == 0)
            {
                plotPanel.Top = 0;
                plotPanel.Height = mainPanelHeight;
                plotPanelList.Add(plotPanel);
            }
            else
            {
                float heightChangeFactor = plotPanelList.Count / (float)(plotPanelList.Count + 1);
                for (int ii = 0; ii < plotPanelList.Count; ii++)
                {
                    plotPanelList[ii].Height = (int)Math.Round(heightChangeFactor * plotPanelList[ii].Height);
                }
                plotPanelList[0].Top = 0;
                plotPanelList[0].Height -= separatorHeight;
                separatorList[0].Top = plotPanelList[0].Height;
                for (int ii = 1; ii < plotPanelList.Count; ii++)
                {
                    plotPanelList[ii].Top = separatorList[ii - 1].Top + separatorList[ii - 1].Height;
                    plotPanelList[ii].Height -= separatorHeight;
                    separatorList[ii].Top = plotPanelList[ii].Top + plotPanelList[ii].Height;
                }
                int panelHeight = mainPanelHeight - (separatorList.Last().Top + separatorList.Last().Height);
                plotPanel.Top = separatorList.Last().Top + separatorList.Last().Height;
                plotPanel.Height = panelHeight;
                plotPanelList.Add(plotPanel);
            }
            separator.Top = mainPanelHeight;
            separator.Height = separatorHeight;
            separatorList.Add(separator);
            Invalidate();
        }

        public void RemovePanel(int index)
        {
        }

        private void DrawSeparators(Graphics g)
        {
            using (SolidBrush separatorBrush = new SolidBrush(Color.White))
            {
                for (int ii = 0; ii < separatorList.Count; ii++)
                {
                    g.FillRectangle(separatorBrush, 0, separatorList[ii].Top, mainPanelWidth, separatorList[ii].Height);
                }
            }
        }

        private Boolean InSeparator(float y)
        {
            if (separatorList == null) { return false; }
            foreach (Separator separator in separatorList)
            {
                if (separator.Top < y)
                {
                    if (separator.Top + separator.Height >= y) { return true; }
                }
            }
            return false;
        }

        private int GetSeparatorIndex(float y)
        {
            for (int ii = 0; ii < separatorList.Count; ii++)
            {
                Separator separator = separatorList[ii];
                if (separator.Top < y)
                {
                    if (separator.Top + separator.Height >= y) { return ii; }
                }
            }
            return -1;
        }

        private void DrawPanels(Graphics g)
        {
            if (this.plotPanelList == null) { return; }
            foreach (PlotPanel plotPanel in this.plotPanelList)
            {
                plotPanel.Draw(g);
            }
        }

        // Draw the plots here. The slider is drawn last
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (separatorList == null) { return; }
            DrawPanels(e.Graphics);
            DrawSeparators(e.Graphics);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (InSeparator(e.Y))
            {
                draggingSeparator = true;
                selectedSeparatorIndex = GetSeparatorIndex(e.Y);
                grabPoint.X = e.X;
                grabPoint.Y = e.Y;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            draggingSeparator = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!InHorizontalSliderArea(e.X, e.Y))
            {
                if (InSeparator(e.Y) || draggingSeparator)
                {
                    Cursor = Cursors.HSplit;
                    if (draggingSeparator)
                    {
                        int delta = (e.Y - (int)grabPoint.Y);
                        if (((plotPanelList[selectedSeparatorIndex].Height + delta) > 0) &&
                            ((plotPanelList[selectedSeparatorIndex + 1].Height - delta) > 0))
                        {
                         //   plotPanelList[selectedSeparatorIndex].Top += delta;
                            plotPanelList[selectedSeparatorIndex].Height += delta;
                            separatorList[selectedSeparatorIndex].Top += delta;
                            plotPanelList[selectedSeparatorIndex + 1].Top += delta;
                            plotPanelList[selectedSeparatorIndex + 1].Height -= delta;
                            grabPoint.X = e.X;
                            grabPoint.Y = e.Y;
                            Invalidate();
                        }
                    }
                }
                else { Cursor = Cursors.Arrow; }
            }
        }

        private void DeterminePanelHeights()
        {
            int panelAboveBottom = 0;
            if (plotPanelList.Count > 1)
            {
                panelAboveBottom = plotPanelList[plotPanelList.Count-2].Top +
                                   plotPanelList[plotPanelList.Count-2].Height +
                                   separatorList[plotPanelList.Count-2].Height;
            }
            plotPanelList.Last().Height = (mainPanelHeight - panelAboveBottom);
            separatorList.Last().Top = this.Height;
        }

        public void Clear()
        {
            if (plotPanelList == null) { return; }
            plotPanelList = new List<PlotPanel>();
            separatorList = new List<Separator>();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (plotPanelList == null) { return; }
            DeterminePanelHeights();

            // Improve - scale all panels:
          //  plotPanelList.Last().Height += (mainPanelHeight - plotPanelList.Last().Height);
          //  separatorList.Last().Top = mainPanelHeight;
           // Invalidate();
        }

        public List<PlotPanel> PlotPanelList
        {
            get { return plotPanelList; }
        }
    }
}
