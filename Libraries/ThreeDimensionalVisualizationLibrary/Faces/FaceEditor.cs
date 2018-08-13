using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CustomUserControlsLibrary;
using MathematicsLibrary.Interpolation;
using ThreeDimensionalVisualizationLibrary.Objects;

namespace ThreeDimensionalVisualizationLibrary.Faces
{
    public partial class FaceEditor : UserControl
    {
        private const double Z_MAX = 1;
        private const double Z_MIN = 0;

        private SplitContainer splitContainer;
        private Viewer3D viewer3D;
        private BezierCurveViewer bezierCurveViewer;

        private ToolStrip slicePositionToolStrip;
        private ToolStripLabel currentSliceLabel;
        private ToolStripLabel zLabel;
        private ToolStripButton moveUpButton;
        private ToolStripButton moveDownButton;
        private ToolStripLabel zStepLabel;
        private ToolStripTextBox zStepTextBox;
        private ToolStripButton insertSliceButton;
        private ToolStripButton removeSliceButton;

        private ToolStrip sliceEditingToolStrip;
        private ToolStripButton moveButton;
        private ToolStripButton expandButton;
        private ToolStripButton alignXButton;
        private ToolStripButton alignYButton;

        private ToolStrip renderingToolStrip;
        private ToolStripButton showWireframeButton;
        private ToolStripButton showSurfacesButton;
        private ToolStripButton showVerticesButton;
        private ToolStripButton showSlicePlaneButton;
        private ToolStripButton showAxesButton;
        private ToolStripLabel shadingModelLabel;
        private ToolStripComboBox shadingModelComboBox;
        private ToolStripLabel numberOfLongitudePointsLabel;
        private ToolStripTextBox numberOfLongitudePointsTextBox;
        private ToolStripButton autoRenderButton;


        private System.Boolean autoRender = true;

        private int currentSliceIndex = 0;
        private Scene3D scene;
        private Face face;
        private Rectangle3D slice;

        private double zMin = Z_MIN;
        private double zMax = Z_MAX;

        public FaceEditor()
        {
            InitializeComponent();
        }

        private void ClearControls()
        {
            if (splitContainer == null) { return; }
            int controlIndex = 0;
            while (controlIndex < this.Controls.Count)
            {
                Control control = this.Controls[controlIndex];
                if (control is SplitContainer)
                {
                    foreach (Control panel1Control in splitContainer.Panel1.Controls)
                    {
                        if (panel1Control is Viewer3D)
                        {
                            splitContainer.Panel1.Controls.Remove(panel1Control);
                            panel1Control.Dispose();
                        }
                    }
                    foreach (Control panel2Control in splitContainer.Panel2.Controls)
                    {
                        if (panel2Control is BezierCurveViewer)
                        {
                            splitContainer.Panel1.Controls.Remove(panel2Control);
                            panel2Control.Dispose();
                        }
                    }
                    this.Controls.Remove(control);
                    control.Dispose();
                }
                else if (control is ToolStrip)
                {
                    // To check: Enough?
                    ((ToolStrip)control).Items.Clear();
                    this.Controls.Remove(control);
                    control.Dispose();
                }
                else { controlIndex++; }
            }
        }

        private void HandlerViewer3DArrowUpPressed(object sender, EventArgs e)
        {
            if (currentSliceIndex == 0) { return; }
            double previousZ = face.HorizontalBezierCurveList[currentSliceIndex].SplineList[0].ControlPointList[0].CoordinateList[2];
            currentSliceIndex--;
            double currentZ = face.HorizontalBezierCurveList[currentSliceIndex].SplineList[0].ControlPointList[0].CoordinateList[2];
            slice.Move(0, 0, currentZ - previousZ); // Translate(0, 0, currentZ - previousZ);
            viewer3D.Invalidate();
            currentSliceLabel.Text = "Current slice: " + currentSliceIndex.ToString() + " of " + (face.HorizontalBezierCurveList.Count - 1).ToString();
            zLabel.Text = "z = " + face.HorizontalBezierCurveList[currentSliceIndex].SplineList[0].ControlPointList[0].CoordinateList[2].ToString("0.0000");
            insertSliceButton.Enabled = true;
            moveDownButton.Enabled = true;
            if (currentSliceIndex == 0)
            {
                moveUpButton.Enabled = false;
                removeSliceButton.Enabled = false;
            }
            else
            {
                if (face.HorizontalBezierCurveList.Count >= 4)
                {
                    removeSliceButton.Enabled = true;
                }
            }
            bezierCurveViewer.SetBezierCurve(face.HorizontalBezierCurveList[currentSliceIndex]);
        }

        private void HandleViewer3DArrowDownPressed(object sender, EventArgs e)
        {
            if (currentSliceIndex == (face.HorizontalBezierCurveList.Count - 1)) { return; }
            double previousZ = face.HorizontalBezierCurveList[currentSliceIndex].SplineList[0].ControlPointList[0].CoordinateList[2];
            currentSliceIndex++;
            double currentZ = face.HorizontalBezierCurveList[currentSliceIndex].SplineList[0].ControlPointList[0].CoordinateList[2];
            slice.Move(0,0, currentZ - previousZ); // Translate(0, 0, currentZ - previousZ);
            viewer3D.Invalidate();
            currentSliceLabel.Text = "Current slice: " + currentSliceIndex.ToString() + " of " + (face.HorizontalBezierCurveList.Count-1).ToString();
            zLabel.Text = "z = " + face.HorizontalBezierCurveList[currentSliceIndex].SplineList[0].ControlPointList[0].CoordinateList[2].ToString("0.0000");
            moveUpButton.Enabled = true;
            if (currentSliceIndex == (face.HorizontalBezierCurveList.Count - 1))
            {
                moveDownButton.Enabled = false;
                removeSliceButton.Enabled = false;
                insertSliceButton.Enabled = false;
            }
            else
            {
                insertSliceButton.Enabled = true;
                removeSliceButton.Enabled = false;
                if (face.HorizontalBezierCurveList.Count >= 4)
                {
                    removeSliceButton.Enabled = true;
                }
            }
            bezierCurveViewer.SetBezierCurve(face.HorizontalBezierCurveList[currentSliceIndex]);
        }

        private void HandleMoveUpButtonClick(object sender, EventArgs e)
        {
            double deltaZ = double.Parse(zStepTextBox.Text);
            face.MoveZ(currentSliceIndex, deltaZ);
            slice.Move(0, 0, deltaZ); // Translate(0, 0, deltaZ);
            if (autoRender) { RenderScene(); }
            else { viewer3D.Invalidate(); } // Only moves the slice plane 
            zLabel.Text = "z = " + face.HorizontalBezierCurveList[currentSliceIndex].SplineList[0].ControlPointList[0].CoordinateList[2].ToString("0.0000");
            double zCurrent = face.GetZ(currentSliceIndex);
            if (currentSliceIndex > 0)
            {
                double zSliceAbove = face.GetZ(currentSliceIndex - 1);
                if ((zSliceAbove - zCurrent) < deltaZ) { moveUpButton.Enabled = false; }
            }
            else
            {
                if ((Z_MAX - zCurrent) < deltaZ) { moveUpButton.Enabled = false; }
            }
        }

        private void HandleMoveDownButtonClick(object sender, EventArgs e)
        {
            double deltaZ = double.Parse(zStepTextBox.Text);
            face.MoveZ(currentSliceIndex, -deltaZ);
            slice.Move(0, 0, -deltaZ); //  Translate(0, 0, -deltaZ);
            if (autoRender) { RenderScene(); }
            else { viewer3D.Invalidate(); } // Only moves the slice plane 
            zLabel.Text = "z = " + face.HorizontalBezierCurveList[currentSliceIndex].SplineList[0].ControlPointList[0].CoordinateList[2].ToString("0.0000");
            double zCurrent = face.GetZ(currentSliceIndex);
            if (currentSliceIndex < (face.HorizontalBezierCurveList.Count-1))
            {
                double zSliceBelow = face.GetZ(currentSliceIndex + 1);
                if ((zCurrent - zSliceBelow) < deltaZ) { moveDownButton.Enabled = false; }
            }
            else
            {
                if ((zCurrent-Z_MIN) < deltaZ) { moveDownButton.Enabled = false; }
            }
        }

        private void HandleZStepTextBoxTextChanged(object sender, EventArgs e)
        {
            double zStep = 0;
            moveUpButton.Enabled = false;
            moveDownButton.Enabled = false;
            Boolean formatOK = double.TryParse(zStepTextBox.Text, out zStep);
            if (formatOK)
            {
                if (zStep > 0)
                {
                    double zCurrent = face.GetZ(currentSliceIndex);
                    double zSliceAbove = Z_MAX;
                    if (currentSliceIndex > 0) { zSliceAbove = face.GetZ(currentSliceIndex - 1); }
                    double zSliceBelow = Z_MIN;
                    if (currentSliceIndex < (face.HorizontalBezierCurveList.Count - 1)) { zSliceBelow = face.GetZ(currentSliceIndex + 1); }
                    if ((zSliceAbove - zCurrent) > zStep) { moveUpButton.Enabled = true; }
                    if ((zCurrent - zSliceBelow) > zStep) { moveDownButton.Enabled = true; }
                }
            }
        }

        private void HandleInsertSliceButtonClick(object sender, EventArgs e)
        {
            BezierCurve curveAbove = face.HorizontalBezierCurveList[currentSliceIndex];
            BezierCurve curveBelow = face.HorizontalBezierCurveList[currentSliceIndex + 1];
            BezierCurve insertedCurve = BezierCurve.GetAverage(curveAbove, curveBelow);
            double previousZ = face.HorizontalBezierCurveList[currentSliceIndex].SplineList[0].ControlPointList[0].CoordinateList[2];
            face.HorizontalBezierCurveList.Insert(currentSliceIndex+1, insertedCurve);
            currentSliceIndex++;
            double currentZ = face.HorizontalBezierCurveList[currentSliceIndex].SplineList[0].ControlPointList[0].CoordinateList[2];
            slice.Move(0,0, currentZ - previousZ); // Translate(0, 0, currentZ - previousZ);
            if (autoRender) { RenderScene(); }
            else { viewer3D.Invalidate(); } // Only moves the slice plane 
            currentSliceLabel.Text = "Current slice: " + currentSliceIndex.ToString() + " of " + (face.HorizontalBezierCurveList.Count - 1).ToString();
            zLabel.Text = "z = " + face.HorizontalBezierCurveList[currentSliceIndex].SplineList[0].ControlPointList[0].CoordinateList[2].ToString("0.0000");
            bezierCurveViewer.SetBezierCurve(face.HorizontalBezierCurveList[currentSliceIndex]);
            if (face.HorizontalBezierCurveList.Count >= 4) { removeSliceButton.Enabled = true; }
        }

        private void HandleRemoveSliceButtonClick(object sender, EventArgs e)
        {
       //     if ((currentSliceIndex > 0) && (currentSliceIndex < (face.HorizontalBezierCurveList.Count - 1))) ;
            double previousZ = face.HorizontalBezierCurveList[currentSliceIndex].SplineList[0].ControlPointList[0].CoordinateList[2];
            face.HorizontalBezierCurveList.RemoveAt(currentSliceIndex);
            currentSliceIndex--;
            double currentZ = face.HorizontalBezierCurveList[currentSliceIndex].SplineList[0].ControlPointList[0].CoordinateList[2];
            slice.Move(0, 0, currentZ - previousZ); // Translate(0, 0, currentZ - previousZ);
            if (autoRender) { RenderScene(); }
            else { viewer3D.Invalidate(); } // Only moves the slice plane 
            currentSliceLabel.Text = "Current slice: " + currentSliceIndex.ToString() + " of " + (face.HorizontalBezierCurveList.Count - 1).ToString();
            zLabel.Text = "z = " + face.HorizontalBezierCurveList[currentSliceIndex].SplineList[0].ControlPointList[0].CoordinateList[2].ToString("0.0000");
            if (currentSliceIndex == 0)
            {
                moveUpButton.Enabled = false;
                removeSliceButton.Enabled = false;
            }
            bezierCurveViewer.SetBezierCurve(face.HorizontalBezierCurveList[currentSliceIndex]);
            if (face.HorizontalBezierCurveList.Count < 4) {removeSliceButton.Enabled = false;}
            else if (currentSliceIndex == face.HorizontalBezierCurveList.Count - 1) { removeSliceButton.Enabled = false; }
        }

        private void HandleMoveButtonCheckedChanged(object sender, EventArgs e)
        {
            bezierCurveViewer.MoveActive = moveButton.Checked;
        }

        private void HandleExpandButtonCheckedChanged(object sender, EventArgs e)
        {
            bezierCurveViewer.ExpandActive = expandButton.Checked;
        }

        private void HandleAlignXButtonClick(object sender, EventArgs e)
        {
            bezierCurveViewer.AlignSelectedX();
        }

        private void HandleAlignYButtonClick(object sender, EventArgs e)
        {
            bezierCurveViewer.AlignSelectedY();
        }

        private void HandleShadingModelComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            Face face = (Face)viewer3D.Scene.ObjectList.Find(o => o is Face);
            if (face != null) // Should always be the case..
            {
                if (shadingModelComboBox.SelectedItem.ToString() == "Flat")
                {
                    viewer3D.UseSmoothShading = false;
                    face.ShadingModel = OpenTK.Graphics.OpenGL.ShadingModel.Flat;
                    viewer3D.ForceRender();
                }
                else
                {
                    viewer3D.UseSmoothShading = true;
                    face.ShadingModel = OpenTK.Graphics.OpenGL.ShadingModel.Smooth;
                    viewer3D.ForceRender();
                }
            }
        }

        private void RenderScene()
        {
            int numberOfLongitudePoints = int.Parse(numberOfLongitudePointsTextBox.Text);
            face.Generate(new List<double>() { numberOfLongitudePoints });
            viewer3D.Invalidate();
        }

        private void HandleAutoRenderButtonCheckedChanged(object sender, EventArgs e)
        {
            autoRender = autoRenderButton.Checked;
            if (!autoRender)
            {
            }
            else
            {
                RenderScene();
            }
        }

        private void HandleShowWireframeButtonCheckedChanged(object sender, EventArgs e)
        {
            Face face = (Face)viewer3D.Scene.ObjectList.Find(o => o is Face);
            if (face != null) // Should always be the case..
            {
                System.Boolean oldValue = face.ShowWireFrame;
                face.ShowWireFrame = showWireframeButton.Checked;
                if (face.ShowWireFrame != oldValue) { viewer3D.ForceRender(); }
            }
        }

        private void HandleShowSurfacesButtonCheckedChanged(object sender, EventArgs e)
        {
            Face face = (Face)viewer3D.Scene.ObjectList.Find(o => o is Face);
            if (face != null) // Should always be the case..
            {
                System.Boolean oldValue = face.ShowSurfaces;
                face.ShowSurfaces = showSurfacesButton.Checked;
                if (face.ShowSurfaces != oldValue) { viewer3D.ForceRender(); }
            }
        }

        private void HandleShowVerticesButtonCheckedChanged(object sender, EventArgs e)
        {
            Face face = (Face)viewer3D.Scene.ObjectList.Find(o => o is Face);
            if (face != null) // Should always be the case..
            {
                System.Boolean oldValue = face.ShowVertices;
                face.ShowVertices = showVerticesButton.Checked;
                if (face.ShowVertices != oldValue) { viewer3D.ForceRender(); }
            }
        }

        private void HandleShowSlicePlaneButtonCheckedChanged(object sender, EventArgs e)
        {
            slice.Visible = showSlicePlaneButton.Checked;
            viewer3D.Invalidate();
        }

        private void HandleShowAxesButtonCheckedChanged(object sender, EventArgs e)
        {
            viewer3D.ShowWorldAxes = showAxesButton.Checked;
            bezierCurveViewer.AxesVisible = showAxesButton.Checked;
        }

        private void HandleNumberOfLongitudePointsTextBoxTextChanged(object sender, EventArgs e)
        {
            int numberOfLongitudePoints;
            autoRenderButton.Enabled = true;
            Boolean ok = int.TryParse(numberOfLongitudePointsTextBox.Text, out numberOfLongitudePoints);
            if (ok)
            {
                if (numberOfLongitudePoints >= 4)
                {
                    if (autoRender) { RenderScene(); }
                    else
                    {
                        autoRenderButton.Enabled = true;
                    }
                }
                else
                {
                    autoRenderButton.Enabled = false;
                }
            }
            else
            {
                autoRenderButton.Enabled = false;
            }
        }

        private void HandleBezierSplineViewerSplineCurveChanged(object sender, EventArgs e)
        {
            if (autoRender) { RenderScene(); }
        }

        private void HandleBezierCurveViewerPointsSelected(object sender, EventArgs e)
        {
            alignXButton.Enabled = true;
            alignYButton.Enabled = true;
        }

        private void HandleBezierCurveViewerAllPointsDeSelected(object sender, EventArgs e)
        {
            alignXButton.Enabled = false;
            alignYButton.Enabled = false;
        }

        public void SetFace(Face face)
        {
            this.face = face;
        }

        public void Initialize()
        {
            ClearControls();
            this.slice = new Rectangle3D();
            this.slice.Name = "SlicePlane";
            // First tool strip: Slice position.
            slicePositionToolStrip = new ToolStrip();
            slicePositionToolStrip.Dock = DockStyle.Top;
            currentSliceLabel = new ToolStripLabel();
            currentSliceLabel.DisplayStyle = ToolStripItemDisplayStyle.Text; // NOTE: The text for this label is set below.
            slicePositionToolStrip.Items.Add(currentSliceLabel);
            zLabel = new ToolStripLabel();
            zLabel.DisplayStyle = ToolStripItemDisplayStyle.Text; // NOTE: The text for this label is set below.
            slicePositionToolStrip.Items.Add(zLabel);
            ToolStripSeparator separator1 = new ToolStripSeparator();
            slicePositionToolStrip.Items.Add(separator1);
            moveUpButton = new ToolStripButton();
            moveUpButton.Click += new EventHandler(HandleMoveUpButtonClick);
            moveUpButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            moveUpButton.Text = "Move up";
            slicePositionToolStrip.Items.Add(moveUpButton);
            moveDownButton = new ToolStripButton();
            moveDownButton.Click += new EventHandler(HandleMoveDownButtonClick);
            moveDownButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            moveDownButton.Text = "Move down";
            slicePositionToolStrip.Items.Add(moveDownButton);
            zStepLabel = new ToolStripLabel();
            zStepLabel.DisplayStyle = ToolStripItemDisplayStyle.Text;
            zStepLabel.Text = "Step: ";
            slicePositionToolStrip.Items.Add(zStepLabel);
            zStepTextBox = new ToolStripTextBox();
            zStepTextBox.DisplayStyle = ToolStripItemDisplayStyle.Text;
            zStepTextBox.Text = "0.01";
            zStepTextBox.AutoSize = false;
            zStepTextBox.Width = 50;
            slicePositionToolStrip.Items.Add(zStepTextBox);
            ToolStripSeparator separator2 = new ToolStripSeparator();
            slicePositionToolStrip.Items.Add(separator2);

            insertSliceButton = new ToolStripButton();
            insertSliceButton.Click += new EventHandler(HandleInsertSliceButtonClick);
            insertSliceButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            insertSliceButton.Text = "Insert slice";
            slicePositionToolStrip.Items.Add(insertSliceButton);
            removeSliceButton = new ToolStripButton();
            removeSliceButton.Enabled = false;
            removeSliceButton.Click += new EventHandler(HandleRemoveSliceButtonClick);
            removeSliceButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            removeSliceButton.Text = "Remove slice";
            slicePositionToolStrip.Items.Add(removeSliceButton);

            // Second tool strip: Slice editing
            sliceEditingToolStrip = new ToolStrip();
            sliceEditingToolStrip.Dock = DockStyle.Top;
            moveButton = new ToolStripButton();
            moveButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            moveButton.Text = "Move";
            moveButton.CheckOnClick = true;
            moveButton.Checked = false;
            moveButton.CheckedChanged += new EventHandler(HandleMoveButtonCheckedChanged);
            sliceEditingToolStrip.Items.Add(moveButton);
            expandButton = new ToolStripButton();
            expandButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            expandButton.Text = "Expand";
            expandButton.CheckOnClick = true;
            expandButton.Checked = false;
            expandButton.CheckedChanged += new EventHandler(HandleExpandButtonCheckedChanged);
            sliceEditingToolStrip.Items.Add(expandButton);
            alignXButton = new ToolStripButton();
            alignXButton.Enabled = false;
            alignXButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            alignXButton.Text = "Align X";
            alignXButton.Click += new EventHandler(HandleAlignXButtonClick);
            sliceEditingToolStrip.Items.Add(alignXButton);
            alignYButton = new ToolStripButton();
            alignYButton.Enabled = false;
            alignYButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            alignYButton.Text = "Align Y";
            alignYButton.Click += new EventHandler(HandleAlignYButtonClick);
            sliceEditingToolStrip.Items.Add(alignYButton);

            // Third tool strip: Rendering tool strip
            renderingToolStrip = new ToolStrip();
            renderingToolStrip.Dock = DockStyle.Top;
            showWireframeButton = new ToolStripButton();
            showWireframeButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            showWireframeButton.Text = "Show wireframe";
            showWireframeButton.CheckOnClick = true;
            showWireframeButton.Checked = false;
            showWireframeButton.CheckedChanged += new EventHandler(HandleShowWireframeButtonCheckedChanged);
            renderingToolStrip.Items.Add(showWireframeButton);
            showSurfacesButton = new ToolStripButton();
            showSurfacesButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            showSurfacesButton.Text = "Show surfaces";
            showSurfacesButton.CheckOnClick = true;
            showSurfacesButton.Checked = true;
            showSurfacesButton.CheckedChanged += new EventHandler(HandleShowSurfacesButtonCheckedChanged);
            renderingToolStrip.Items.Add(showSurfacesButton);
            showVerticesButton = new ToolStripButton();
            showVerticesButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            showVerticesButton.Text = "Show vertices";
            showVerticesButton.CheckOnClick = true;
            showVerticesButton.Checked = false;
            showVerticesButton.CheckedChanged +=new EventHandler(HandleShowVerticesButtonCheckedChanged);
            renderingToolStrip.Items.Add(showVerticesButton);
            showSlicePlaneButton = new ToolStripButton();
            showSlicePlaneButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            showSlicePlaneButton.Text = "Show slice plane";
            showSlicePlaneButton.CheckOnClick = true;
            showSlicePlaneButton.Checked = true;
            showSlicePlaneButton.CheckedChanged += new EventHandler(HandleShowSlicePlaneButtonCheckedChanged);
            renderingToolStrip.Items.Add(showSlicePlaneButton);
            showAxesButton = new ToolStripButton();
            showAxesButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            showAxesButton.Text = "Show axes";
            showAxesButton.CheckOnClick = true;
            showAxesButton.Checked = false;
            showAxesButton.CheckedChanged += new EventHandler(HandleShowAxesButtonCheckedChanged);
            renderingToolStrip.Items.Add(showAxesButton);
            ToolStripSeparator separator3 = new ToolStripSeparator();
            renderingToolStrip.Items.Add(separator3);
            numberOfLongitudePointsLabel = new ToolStripLabel();
            numberOfLongitudePointsLabel.DisplayStyle = ToolStripItemDisplayStyle.Text;
            numberOfLongitudePointsLabel.Text = "Number of longitude points: ";
            renderingToolStrip.Items.Add(numberOfLongitudePointsLabel);
            numberOfLongitudePointsTextBox = new ToolStripTextBox();
            numberOfLongitudePointsTextBox.DisplayStyle = ToolStripItemDisplayStyle.Text;
            numberOfLongitudePointsTextBox.Text = "32";
            numberOfLongitudePointsTextBox.AutoSize = false;
            numberOfLongitudePointsTextBox.Width = 30;
            numberOfLongitudePointsTextBox.TextChanged += new EventHandler(HandleNumberOfLongitudePointsTextBoxTextChanged);
            renderingToolStrip.Items.Add(numberOfLongitudePointsTextBox);
            shadingModelLabel = new ToolStripLabel();
            shadingModelLabel.DisplayStyle = ToolStripItemDisplayStyle.Text;
            shadingModelLabel.Text = "Shading model";
            renderingToolStrip.Items.Add(shadingModelLabel);
            shadingModelComboBox = new ToolStripComboBox();
            shadingModelComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            shadingModelComboBox.Items.Add("Flat");
            shadingModelComboBox.Items.Add("Smooth");
            shadingModelComboBox.AutoSize = false;
            shadingModelComboBox.Width = 70;
            shadingModelComboBox.SelectedIndex = 1;  // Default value: Smooth shading
            shadingModelComboBox.SelectedIndexChanged += new EventHandler(HandleShadingModelComboBoxSelectedIndexChanged);
            renderingToolStrip.Items.Add(shadingModelComboBox);
            autoRenderButton = new ToolStripButton();
            autoRenderButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            autoRenderButton.Text = "Autorender";
            autoRenderButton.CheckOnClick = true;
            autoRenderButton.Checked = true;
            autoRenderButton.CheckedChanged += new EventHandler(HandleAutoRenderButtonCheckedChanged);
            renderingToolStrip.Items.Add(autoRenderButton);



            splitContainer = new SplitContainer();
            splitContainer.Dock = DockStyle.Fill;
            this.Controls.Add(splitContainer);
            viewer3D = new Viewer3D();
            viewer3D.Dock = DockStyle.Fill;
            splitContainer.Panel1.Controls.Add(viewer3D);
            bezierCurveViewer = new BezierCurveViewer();
            bezierCurveViewer.Dock = DockStyle.Fill;
            splitContainer.Panel2.Controls.Add(bezierCurveViewer);
            splitContainer.SplitterDistance = this.Width / 2;
            bezierCurveViewer.SetRange(-2, 2, -2, 2);
            bezierCurveViewer.SplineCurveChanged += new EventHandler(HandleBezierSplineViewerSplineCurveChanged);
            bezierCurveViewer.PointsSelected += new EventHandler(HandleBezierCurveViewerPointsSelected);
            bezierCurveViewer.AllPointsDeSelected += new EventHandler(HandleBezierCurveViewerAllPointsDeSelected);

            // Add the toolstrips in reverse order
            // Strangely enough, they should also apparently be inserted AFTER the SplitContainer, despite
            // the latter's DockStyle (= Fill)!
            this.Controls.Add(renderingToolStrip);
            this.Controls.Add(sliceEditingToolStrip);
            this.Controls.Add(slicePositionToolStrip);

            // MW ToDo: Some ugly hard-coding here...
            scene = new Scene3D();
            Light light = new Light();
            light.Position[1] = -5;
            light.Position[2] = 3;
            light.IsOn = true;
            scene.LightList.Add(light);
            face.AmbientColor = Color.FromArgb(255, 205, 148);  // Typical skin color approximation
            face.DiffuseColor = Color.FromArgb(255, 205, 148);  // Typical skin color approximation
            face.SpecularColor = Color.White;
            face.Shininess = 50;//  20;
            int numberOfLongitudePoints = int.Parse(numberOfLongitudePointsTextBox.Text);
            face.Generate(new List<double>() { numberOfLongitudePoints });
            scene.AddObject(face);
            slice.AmbientColor = Color.FromArgb(200, 100, 255, 100);
            slice.DiffuseColor = Color.FromArgb(200, 100, 255, 100);
            slice.SpecularColor = Color.FromArgb(200, 100, 255, 100);
            slice.Shininess = 20;
            slice.Generate(new List<double>() { 3.0, 3.0 });
            scene.AddObject(slice);
            currentSliceIndex = face.HorizontalBezierCurveList.Count / 2;
            slice.Move(0, 0, face.HorizontalBezierCurveList[currentSliceIndex].SplineList[0].ControlPointList[0].CoordinateList[2]);
       //     slice.Translate(0, 0, face.HorizontalBezierCurveList[currentSliceIndex].SplineList[0].ControlPointList[0].CoordinateList[2]);
            bezierCurveViewer.SetBezierCurve(face.HorizontalBezierCurveList[currentSliceIndex]);
            currentSliceLabel.Text = "Current slice: " + currentSliceIndex.ToString() + " of " + (face.HorizontalBezierCurveList.Count - 1).ToString();
            zLabel.Text = "z = " + face.HorizontalBezierCurveList[currentSliceIndex].SplineList[0].ControlPointList[0].CoordinateList[2].ToString("0.0000");
            zStepTextBox.TextChanged += new EventHandler(HandleZStepTextBoxTextChanged); // Add the event handler here, AFTER setting the slice index
            viewer3D.Scene = scene;
            viewer3D.CameraTarget = new OpenTK.Vector3(0f, 0f, 0.5f); // The model extends from 0 to 1 in z.

            viewer3D.ArrowUpPressed -= new EventHandler(HandlerViewer3DArrowUpPressed);
            viewer3D.ArrowUpPressed += new EventHandler(HandlerViewer3DArrowUpPressed);
            viewer3D.ArrowDownPressed -= new EventHandler(HandleViewer3DArrowDownPressed);
            viewer3D.ArrowDownPressed += new EventHandler(HandleViewer3DArrowDownPressed);
        }

        public Face Face
        {
            get { return face; }
        }

    }
}
