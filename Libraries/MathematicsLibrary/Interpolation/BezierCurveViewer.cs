using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CustomUserControlsLibrary;
using MathematicsLibrary.Geometry;

namespace MathematicsLibrary.Interpolation
{
    // Visualizes 2D Bezier splines
    public enum DragMode { Uniform, Proportional };
    public enum SelectionSide { None, Left, Right };

    public partial class BezierCurveViewer : ScrollableZoomControl
    {
        private const double DEFAULT_RULER_LENGTH = 0.5;
        private const double DEFAULT_RULER_WIDTH = 0.1;
        private const double DEFAULT_RULER_ORIENTATION = 0;

        private BezierCurve bezierCurve = null;
        private Color controlPointColor = Color.Yellow;
        private Color selectedPointColor = Color.Red;
        private Color curveColor = Color.Lime;
        private float controlPointRadius = 3.0f;
        private float curveThickness = 2.0f;
        private Boolean pointsVisible = true;
        private Boolean curveVisible = true;

        private float clickTolerance = 10.0f;

   //     private Boolean singlePointSelected = false;
        private int dragSplineIndex = -1;
        private int dragPointIndex = -1; // 0 <=> P0 etc.
        private int dragSplineIndex2 = -1; // Needed in case the selected point is point 0 or point 3 on a spline 
        private int dragPointIndex2 = -1;  // Needed in case the selected point is point 0 or point 3 on a spline 

        private Boolean selectionClicked = false;

     //   private DragMode dragMode = DragMode.Uniform;
        private PointF dragPoint;

        private Boolean clicked = false;
        private PointF movePoint;

        private Boolean axesVisible = false;

        // Various editing tools:
        private Boolean expandActive = false;
        private Boolean moveActive = false;
     //   private Boolean rulerActive = false;

        private Boolean selecting = false;
        private PointF selectionCorner1;
        private PointF selectionCorner2;

        private SelectionSide selectionSide = SelectionSide.None;

        public event EventHandler SplineCurveChanged = null;
        public event EventHandler PointsSelected = null;
        public event EventHandler AllPointsDeSelected = null;


        // 20160701:
        // To replace single and multiple point selection:
        private List<Tuple<int, int>> selectedPointInformation  = new List<Tuple<int,int>>(); // Item1 = spline index, Item2 = control point index.
        private List<Tuple<int, int>> mirrorPointInformation = new List<Tuple<int,int>>(); 

        public BezierCurveViewer()
        {
            InitializeComponent();
        }

        private void DrawPoints(Graphics g)
        {
            using (SolidBrush brush = new SolidBrush(controlPointColor))
            {
                for (int ii = 0; ii < bezierCurve.SplineList.Count; ii++)
                {
                    for (int jj = 0; jj < bezierCurve.SplineList[ii].ControlPointList.Count; jj++)
                    {
                        PointND controlPoint = bezierCurve.SplineList[ii].ControlPointList[jj];
                        double x = controlPoint.CoordinateList[0];
                        double y = controlPoint.CoordinateList[1];
                        float plotX = GetPlotXAtX(x) - controlPointRadius;
                        float plotY = GetPlotYAtY(y) - controlPointRadius;
                        brush.Color = controlPointColor;
                        if (selectedPointInformation.Count > 0)
                        {
                            if (selectedPointInformation.FindIndex(t => (t.Item1 == ii) && (t.Item2 == jj)) >= 0)
                            {
                                brush.Color = selectedPointColor;
                            }
                        }
                        g.FillEllipse(brush, new RectangleF(plotX, plotY, 2 * controlPointRadius, 2 * controlPointRadius));
                    }
                }
            }  
        }

        private void OnSplineCurveChanged()
        {
            if (SplineCurveChanged != null)
            {
                EventHandler handler = SplineCurveChanged;
                handler(this, EventArgs.Empty);
            }
        }

        private void OnPointsSelected()
        {
            if (PointsSelected != null)
            {
                EventHandler handler = PointsSelected;
                handler(this, EventArgs.Empty);
            }
        }

        private void OnAllPointsDeSelected()
        {
            if (AllPointsDeSelected != null)
            {
                EventHandler handler = AllPointsDeSelected;
                handler(this, EventArgs.Empty);
            }
        }

        private void DrawCurve(Graphics g)
        {
            // To do: parameterize uStep!
            using (Pen pen = new Pen(curveColor))
            {
                pen.Width = curveThickness;
                List<Point2D> pointList = bezierCurve.Interpolate(0.1);
                for (int ii = 1; ii < pointList.Count; ii++)
                {
                    Point2D previousPoint = pointList[ii - 1];
                    float xPlotPrevious = GetPlotXAtX(previousPoint.X);
                    float yPlotPrevious = GetPlotYAtY(previousPoint.Y);
                    Point2D currentPoint = pointList[ii];
                    float xPlot = GetPlotXAtX(currentPoint.X);
                    float yPlot = GetPlotYAtY(currentPoint.Y);
                    g.DrawLine(pen, xPlotPrevious, yPlotPrevious, xPlot, yPlot);
                }
            }
        }

        private Boolean CheckDeSelection(float pixelX, float pixelY)
        {
            Boolean selectionChanged = false;
            if (bezierCurve == null) { return selectionChanged; }
            int splineIndex = -1;
            int controlPointIndex = -1;
            int splineIndex2 = -1;
            int controlPointIndex2 = -1;
            double minimumDistance = double.MaxValue;
            for (int ii = 0; ii < bezierCurve.SplineList.Count; ii++)
            {
                for (int jj = 0; jj < bezierCurve.SplineList[ii].ControlPointList.Count; jj++)
                {
                    float plotX = GetPlotXAtX(bezierCurve.SplineList[ii].ControlPointList[jj].CoordinateList[0]);
                    double plotY = GetPlotYAtY(bezierCurve.SplineList[ii].ControlPointList[jj].CoordinateList[1]);
                    double distanceSquared = (pixelX - plotX) * (pixelX - plotX) + (pixelY - plotY) * (pixelY - plotY);
                    if (distanceSquared < minimumDistance)
                    {
                        minimumDistance = distanceSquared;
                        splineIndex = ii;
                        controlPointIndex = jj;
                    }
                }
            }
            if (minimumDistance < clickTolerance)
            {
                // Find the underlying point from the connected spline, if any
                selectionChanged = true;
                if (controlPointIndex == 0)
                {
                    splineIndex2 = splineIndex - 1;
                    if (splineIndex2 < 0) { splineIndex2 = bezierCurve.SplineList.Count - 1; }
                    controlPointIndex2 = 3;
                }
                else if (controlPointIndex == 3)
                {
                    splineIndex2 = (splineIndex + 1) % bezierCurve.SplineList.Count;
                    controlPointIndex2 = 0;
                }
                int selectionIndex = selectedPointInformation.FindIndex(t => (t.Item1 == splineIndex) && (t.Item2 == controlPointIndex));
                if (selectionIndex >= 0) // select the point and any underlying points (from the next or previous spline)
                {
                    selectionClicked = false;
                    selectedPointInformation.RemoveAt(selectionIndex);
                    if (splineIndex2 >= 0)
                    {
                        int selectionIndex2 = selectedPointInformation.FindIndex(t => (t.Item1 == splineIndex2) && (t.Item2 == controlPointIndex2));
                        selectedPointInformation.RemoveAt(selectionIndex2);
                    }
                }
            }
            if (selectedPointInformation.Count == 0)
            {
                OnAllPointsDeSelected();
                selectionSide = SelectionSide.None;
            }
            return selectionChanged;
        }



        private Boolean CheckSelectedPointClicked(float pixelX, float pixelY)
        {
            if (bezierCurve == null) { return false; }
            double minimumDistance = double.MaxValue;
            for (int iPoint = 0; iPoint < selectedPointInformation.Count; iPoint++)
            {
                //    for (int ii = 0; ii < bezierCurve.SplineList.Count; ii++)
                //   {
                //      for (int jj = 0; jj < bezierCurve.SplineList[ii].ControlPointList.Count; jj++)
                //    {
                int iSpline = selectedPointInformation[iPoint].Item1;
                int iControlPoint = selectedPointInformation[iPoint].Item2;
                float plotX = GetPlotXAtX(bezierCurve.SplineList[iSpline].ControlPointList[iControlPoint].CoordinateList[0]);
                double plotY = GetPlotYAtY(bezierCurve.SplineList[iSpline].ControlPointList[iControlPoint].CoordinateList[1]);
                double distanceSquared = (pixelX - plotX) * (pixelX - plotX) + (pixelY - plotY) * (pixelY - plotY);
                if (distanceSquared < minimumDistance)
                {
                    minimumDistance = distanceSquared;
                    dragSplineIndex = iSpline;
                    dragPointIndex = iControlPoint;
                }
                //   }
            }
            if (minimumDistance < clickTolerance)
            {
                if (dragPointIndex == 0)
                {
                    int previousSplineIndex = dragSplineIndex - 1;
                    if (previousSplineIndex < 0) { previousSplineIndex = bezierCurve.SplineList.Count - 1; }
                  //  dragSplineIndex2 = previousSplineIndex;
                 //   dragPointIndex2 = 3;
                }
                else if (dragPointIndex == 3)
                {
                    int nextSplineIndex = (dragSplineIndex + 1) % bezierCurve.SplineList.Count;
                 //   dragSplineIndex2 = nextSplineIndex;
                 //   dragPointIndex2 = 0;
                }
                return true;
            }
            else
            {
                dragSplineIndex = -1;
                dragPointIndex = -1;
             //   dragSplineIndex2 = -1;
             //   dragPointIndex2 = -1;
                return false;
            }
        }

        private void DrawAxes(Graphics g)
        {
            float xPlotOrigin = GetPlotXAtX(0);
            float yPlotOrigin = GetPlotYAtY(0);
            float xPlotMax = this.Width;
            float yPlotMax = 0f;
            using (Pen axisPen = new Pen(Color.Red))
            {
                g.DrawLine(axisPen, xPlotOrigin, yPlotOrigin, xPlotMax, yPlotOrigin);
                axisPen.Color = Color.Green;
                g.DrawLine(axisPen, xPlotOrigin, yPlotOrigin, xPlotOrigin, yPlotMax);
            }
        }

        private void DrawSelection(Graphics g)
        {
            using (SolidBrush selectionBrush = new SolidBrush(Color.FromArgb(127,255,200,200)))
            {
                using (Pen selectionPen = new Pen(Color.FromArgb(255, 0, 0)))
                {
                    float selectionWidth = selectionCorner2.X - selectionCorner1.X;
                    float selectionHeight = selectionCorner2.Y - selectionCorner1.Y;
                    float x0 = selectionCorner1.X;
                    if (selectionWidth < 0)
                    {
                        x0 = selectionCorner2.X;
                        selectionWidth = -selectionWidth;
                    }
                    float y0 = selectionCorner1.Y;
                    if (selectionHeight < 0)
                    {
                        y0 = selectionCorner2.Y;
                        selectionHeight = -selectionHeight;
                    }
                    g.FillRectangle(selectionBrush, x0, y0, selectionWidth, selectionHeight);
                    g.DrawRectangle(selectionPen, x0, y0, selectionWidth, selectionHeight);
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (bezierCurve.SplineList == null) { return; }
            if (axesVisible) { DrawAxes(e.Graphics); }
            if (curveVisible) { DrawCurve(e.Graphics); }
            if (pointsVisible) { DrawPoints(e.Graphics); }
            if (selecting) { DrawSelection(e.Graphics); }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            clicked = true;
            dragging = false;  // Do not allow direct dragging. Instead, allow dragging only in the sliders.
            if (e.Button == MouseButtons.Left)
            {
                // 20160701
                if (moveActive)
                {
                    movePoint = new PointF(e.X, e.Y);
                    selectedPointInformation = new List<Tuple<int, int>>();
                    OnAllPointsDeSelected();
                    Refresh();
                }
                else
                {
                    if (CheckSelectedPointClicked(e.X, e.Y))
                    {
                        selectionClicked = true;
                        if (dragSplineIndex >= 0)  // If not, the user has not clicked on a SELECTED (red) point.
                        {
                            dragPoint = new Point(e.X, e.Y);
                        }
                    }
                    else
                    {
                        selectionClicked = false;
                        selecting = true;
                        selectionCorner1 = new PointF(e.X, e.Y);
                        selectionCorner2 = new PointF(e.X, e.Y);
                        // Make sure that selections are only allowed on ONE side of the symmetry axis:
                        if (selectionSide == SelectionSide.None)
                        {
                            float symmetryAxisPlotX = GetPlotXAtX(bezierCurve.SplineList[0].ControlPointList[0].CoordinateList[0]);
                            if (selectionCorner1.X <= symmetryAxisPlotX) { selectionSide = SelectionSide.Left; }
                            else { selectionSide = SelectionSide.Right; }
                        }
                        else
                        {
                            SelectionSide attemptedSelectionSide = SelectionSide.None;
                            float symmetryAxisPlotX = GetPlotXAtX(bezierCurve.SplineList[0].ControlPointList[0].CoordinateList[0]);
                            if (selectionCorner1.X <= symmetryAxisPlotX) { attemptedSelectionSide = SelectionSide.Left; }
                            else { attemptedSelectionSide = SelectionSide.Right; }
                            if (attemptedSelectionSide != selectionSide)
                            {
                                selecting = false;
                            }
                        }
                    }
                }
            }
            else
            {
                if (CheckDeSelection(e.X, e.Y))
                {
                    Refresh();
                }
                else
                {
                    selectionClicked = false;
                    selectedPointInformation = new List<Tuple<int, int>>();
                    selectionSide = SelectionSide.None;
                    OnAllPointsDeSelected();
                    Refresh();
                }
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if ((!expandActive) || InHorizontalSlider(e.X,e.Y) || InVerticalSlider(e.X,e.Y))
            {
                base.OnMouseWheel(e);
            }
            else
            {
                double expansionDelta = 1 + 0.0002 * e.Delta;
                bezierCurve.Expand(expansionDelta);
                OnSplineCurveChanged();
                Refresh();
            }
        }

        private void MovePoint(PointND point, float targetX, float targetY)
        {
            point.CoordinateList[0] = targetX;
            point.CoordinateList[1] = targetY;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!clicked) { return; }
            if (selectionClicked)
            {
                if (!bezierCurve.IsPoint)
                {
                    if (bezierCurve.IsXSymmetric)
                    {
                        double xSymmetry = bezierCurve.SplineList[0].ControlPointList[0].CoordinateList[0];
                        double xNew = GetXAtPlotX(e.X);
                        double xNewMirror = xSymmetry - (xNew - xSymmetry); // -xNew;
                        double yNew = GetYAtPlotY(e.Y);
                        // If the drag point is on the symmetry axis, allow only y-movements for ALL selected points:
                        double deltaX = xNew - bezierCurve.SplineList[dragSplineIndex].ControlPointList[dragPointIndex].CoordinateList[0];
                        Boolean dragPointOnSymmetryAxis = false;
                        if ((dragSplineIndex == 0) && (dragPointIndex == 0)) { dragPointOnSymmetryAxis = true; }
                        else if ((dragSplineIndex == (bezierCurve.SplineList.Count - 1)) && (dragPointIndex == 3)) { dragPointOnSymmetryAxis = true; }
                        else if ((dragSplineIndex == ((bezierCurve.SplineList.Count / 2) - 1)) && (dragPointIndex == 3)) { dragPointOnSymmetryAxis = true; }
                        else if ((dragSplineIndex == (bezierCurve.SplineList.Count / 2)) && (dragPointIndex == 0)) { dragPointOnSymmetryAxis = true; }
                        if (dragPointOnSymmetryAxis) { deltaX = 0; }
                        double deltaY = yNew - bezierCurve.SplineList[dragSplineIndex].ControlPointList[dragPointIndex].CoordinateList[1];
                        for (int kk = 0; kk < selectedPointInformation.Count; kk++)
                        {
                            // Find the mirror point for the point in question:
                            int splineIndex = selectedPointInformation[kk].Item1;
                            int controlPointIndex = selectedPointInformation[kk].Item2;
                            int mirrorSplineIndex = (bezierCurve.SplineList.Count - 1) - splineIndex;
                            int mirrorControlPointIndex = 3 - controlPointIndex;

                            Boolean onSymmetryAxis = false;
                            if ((splineIndex == 0) && (controlPointIndex == 0)) { onSymmetryAxis = true; }
                            else if ((splineIndex == (bezierCurve.SplineList.Count - 1)) && (controlPointIndex == 3)) { onSymmetryAxis = true; }
                            else if ((splineIndex == ((bezierCurve.SplineList.Count / 2) - 1)) && (controlPointIndex == 3)) { onSymmetryAxis = true; }
                            else if ((splineIndex == (bezierCurve.SplineList.Count / 2)) && (controlPointIndex == 0)) { onSymmetryAxis = true; }
                            // If the point is not on the symmetry axis, just move it:
                            if (!onSymmetryAxis)
                            {
                                bezierCurve.SplineList[splineIndex].ControlPointList[controlPointIndex].CoordinateList[0] += deltaX;
                                bezierCurve.SplineList[splineIndex].ControlPointList[controlPointIndex].CoordinateList[1] += deltaY;
                                bezierCurve.SplineList[mirrorSplineIndex].ControlPointList[mirrorControlPointIndex].CoordinateList[0] -= deltaX;
                                bezierCurve.SplineList[mirrorSplineIndex].ControlPointList[mirrorControlPointIndex].CoordinateList[1] += deltaY; // Yes, should be "+".
                            }
                            else  // For points on the symmetry axis, disallow all x-movements. Also, since each such point will
                            // be considered twice (such points are their own mirrors), make only half the y-movement:
                            {
                                bezierCurve.SplineList[splineIndex].ControlPointList[controlPointIndex].CoordinateList[1] += deltaY / 2;
                                bezierCurve.SplineList[mirrorSplineIndex].ControlPointList[mirrorControlPointIndex].CoordinateList[1] += deltaY / 2;
                            }
                        }
                    }
                    else
                    {
                        // MW ToDo:
                        // Not needed here, since (so far) only symmetric curves are considered.
                        // Still, should be written.
                    }
                    OnSplineCurveChanged();
                    Refresh();
                }
            }
            else if (moveActive)
            {
                if (!InHorizontalSlider(e.X, e.Y) && !InVerticalSlider(e.X, e.Y))
                {
                    double xOld = GetXAtPlotX(movePoint.X);
                    double yOld = GetYAtPlotY(movePoint.Y);
                    double xNew = GetXAtPlotX(e.X);
                    double yNew = GetYAtPlotY(e.Y);
                    foreach (BezierSpline bezierSpline in bezierCurve.SplineList)
                    {
                        foreach (PointND ControlPoint in bezierSpline.ControlPointList)
                        {
                            ControlPoint.CoordinateList[0] += (xNew - xOld);
                            ControlPoint.CoordinateList[1] += (yNew - yOld);
                        }
                    }
                    movePoint.X = e.X;
                    movePoint.Y = e.Y;
                    if ((Math.Abs(xOld - xNew) > double.Epsilon) || (Math.Abs(yOld - yNew) > double.Epsilon)) { OnSplineCurveChanged(); }
                    Refresh();
                }
            }
            else if (selecting)
            {
                selectionCorner2 = new PointF(e.X, e.Y);
                if (bezierCurve.IsXSymmetric)
                {
                    double xSymmetry = bezierCurve.SplineList[0].ControlPointList[0].CoordinateList[0];
                    float xPlotSymmetry = GetPlotXAtX(xSymmetry);
                    if (selectionCorner2.X > xPlotSymmetry)
                    {
                        if (selectionCorner1.X <= xPlotSymmetry)
                        {
                            selectionCorner2.X = xPlotSymmetry;
                        }
                    }
                    else
                    {
                        if (selectionCorner1.X >= xPlotSymmetry)
                        {
                            selectionCorner2.X = xPlotSymmetry;
                        }
                    }
                }
                Refresh();
            }
        }

        private void SetMultiSelection()
        {
            float xSelectionPlotMax = Math.Max(selectionCorner1.X, selectionCorner2.X);
            float xSelectionPlotMin = Math.Min(selectionCorner1.X, selectionCorner2.X);
            float ySelectionPlotMax = Math.Max(selectionCorner1.Y, selectionCorner2.Y);
            float ySelectionPlotMin = Math.Min(selectionCorner1.Y, selectionCorner2.Y);
            for (int ii = 0; ii < bezierCurve.SplineList.Count; ii++)
            {
                for (int jj = 0; jj < bezierCurve.SplineList[ii].ControlPointList.Count; jj++)
                {
                    double x = bezierCurve.SplineList[ii].ControlPointList[jj].CoordinateList[0];
                    double y = bezierCurve.SplineList[ii].ControlPointList[jj].CoordinateList[1];
                    float xPlot = GetPlotXAtX(x);
                    float yPlot = GetPlotYAtY(y);
                    if ((xPlot >= xSelectionPlotMin) && (xPlot <= xSelectionPlotMax) &&
                        (yPlot >= ySelectionPlotMin) && (yPlot <= ySelectionPlotMax))
                    {
                        if (selectedPointInformation.FindIndex(t => (t.Item1 == ii) && (t.Item2 == jj)) < 0)
                        {
                            selectedPointInformation.Add(new Tuple<int, int>(ii, jj));
                        }
                    }
                }
            }
            if (selectedPointInformation.Count > 0) { OnPointsSelected(); }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            clicked = false;
            selectionClicked = false;
            if (selecting)
            {
                selecting = false;
                SetMultiSelection();
                Refresh();
            }
        }

        public void SetBezierCurve(BezierCurve bezierCurve)
        {
            this.bezierCurve = bezierCurve;
            Refresh();
        }

        public void AlignSelectedX()
        {
            double xAverage = 0;
            for (int ii = 0; ii < selectedPointInformation.Count; ii++)
            {
                int splineIndex = selectedPointInformation[ii].Item1;
                int controlPointIndex = selectedPointInformation[ii].Item2;
                double x = this.bezierCurve.SplineList[splineIndex].ControlPointList[controlPointIndex].CoordinateList[0];
                xAverage += x;
            }
            xAverage /= (double)selectedPointInformation.Count;
            for (int ii = 0; ii < selectedPointInformation.Count; ii++)
            {
                int splineIndex = selectedPointInformation[ii].Item1;
                int controlPointIndex = selectedPointInformation[ii].Item2;
                this.bezierCurve.SplineList[splineIndex].ControlPointList[controlPointIndex].CoordinateList[0] = xAverage;
            }
            if (bezierCurve.IsXSymmetric)
            {
                double xSymmetry = bezierCurve.SplineList[0].ControlPointList[0].CoordinateList[0];
                for (int ii = 0; ii < selectedPointInformation.Count; ii++)
                {
                    int splineIndex = selectedPointInformation[ii].Item1;
                    int controlPointIndex = selectedPointInformation[ii].Item2;
                    int mirrorSplineIndex = (bezierCurve.SplineList.Count - 1) - splineIndex;
                    int mirrorPointIndex = 3 - controlPointIndex;
                    this.bezierCurve.SplineList[mirrorSplineIndex].ControlPointList[mirrorPointIndex].CoordinateList[0] = xSymmetry - (xAverage-xSymmetry);
                }
            }
            OnSplineCurveChanged();
            Refresh();
        }

        public void AlignSelectedY()
        {
            double yAverage = 0;
            for (int ii = 0; ii < selectedPointInformation.Count; ii++)
            {
                int splineIndex = selectedPointInformation[ii].Item1;
                int controlPointIndex = selectedPointInformation[ii].Item2;
                double y = this.bezierCurve.SplineList[splineIndex].ControlPointList[controlPointIndex].CoordinateList[1];
                yAverage += y;
            }
            yAverage /= (double)selectedPointInformation.Count;
            for (int ii = 0; ii < selectedPointInformation.Count; ii++)
            {
                int splineIndex = selectedPointInformation[ii].Item1;
                int controlPointIndex = selectedPointInformation[ii].Item2;
                this.bezierCurve.SplineList[splineIndex].ControlPointList[controlPointIndex].CoordinateList[1] = yAverage;
            }
            if (bezierCurve.IsXSymmetric)
            {
                for (int ii = 0; ii < selectedPointInformation.Count; ii++)
                {
                    int splineIndex = selectedPointInformation[ii].Item1;
                    int controlPointIndex = selectedPointInformation[ii].Item2;
                    int mirrorSplineIndex = (bezierCurve.SplineList.Count - 1) - splineIndex;
                    int mirrorPointIndex = 3 - controlPointIndex;
                    this.bezierCurve.SplineList[mirrorSplineIndex].ControlPointList[mirrorPointIndex].CoordinateList[1] = yAverage;
                }
            }
            OnSplineCurveChanged();
            Refresh();
        }

        public Boolean AxesVisible
        {
            get { return axesVisible; }
            set 
            {
                Boolean oldValue = axesVisible;
                axesVisible = value;
                if (axesVisible != oldValue) { Refresh(); }
            }
        }

        public Boolean ExpandActive
        {
            get { return expandActive; }
            set { expandActive = value; }
        }

        public Boolean MoveActive
        {
            get { return MoveActive; }
            set 
            {
                Boolean oldValue = moveActive;
                moveActive = value;
                if (moveActive)
                {
                    if (selectedPointInformation.Count > 0)
                    {
                        OnAllPointsDeSelected();
                        selectedPointInformation = new List<Tuple<int, int>>();
                    }  
                }
                if (moveActive != oldValue) { Refresh(); }
            }
        }
    }
}
