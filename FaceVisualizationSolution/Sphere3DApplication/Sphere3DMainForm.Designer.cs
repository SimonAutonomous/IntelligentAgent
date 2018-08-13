namespace Sphere3DApplication
{
    partial class Sphere3DMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sphere3DMainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.examplesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.surfacesNoLightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.surfacesFlatShadingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.surfacesSmoothShadingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wireframeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.surfacesAndVerticesSmoothShadingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.surfacesWireframeAndVerticesSmoothShadingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.translucentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.withTextureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewer3D = new ThreeDimensionalVisualizationLibrary.Viewer3D();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.examplesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(558, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // examplesToolStripMenuItem
            // 
            this.examplesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.surfacesNoLightToolStripMenuItem,
            this.surfacesFlatShadingToolStripMenuItem,
            this.surfacesSmoothShadingToolStripMenuItem,
            this.wireframeToolStripMenuItem,
            this.verticesToolStripMenuItem,
            this.surfacesAndVerticesSmoothShadingToolStripMenuItem,
            this.surfacesWireframeAndVerticesSmoothShadingToolStripMenuItem,
            this.toolStripSeparator1,
            this.translucentToolStripMenuItem,
            this.toolStripSeparator2,
            this.withTextureToolStripMenuItem});
            this.examplesToolStripMenuItem.Name = "examplesToolStripMenuItem";
            this.examplesToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.examplesToolStripMenuItem.Text = "Examples";
            // 
            // surfacesNoLightToolStripMenuItem
            // 
            this.surfacesNoLightToolStripMenuItem.Name = "surfacesNoLightToolStripMenuItem";
            this.surfacesNoLightToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.surfacesNoLightToolStripMenuItem.Text = "Surfaces, no light";
            this.surfacesNoLightToolStripMenuItem.Click += new System.EventHandler(this.surfacesNoLightToolStripMenuItem_Click);
            // 
            // surfacesFlatShadingToolStripMenuItem
            // 
            this.surfacesFlatShadingToolStripMenuItem.Name = "surfacesFlatShadingToolStripMenuItem";
            this.surfacesFlatShadingToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.surfacesFlatShadingToolStripMenuItem.Text = "Surfaces, flat shading";
            this.surfacesFlatShadingToolStripMenuItem.Click += new System.EventHandler(this.surfacesFlatShadingToolStripMenuItem_Click);
            // 
            // surfacesSmoothShadingToolStripMenuItem
            // 
            this.surfacesSmoothShadingToolStripMenuItem.Name = "surfacesSmoothShadingToolStripMenuItem";
            this.surfacesSmoothShadingToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.surfacesSmoothShadingToolStripMenuItem.Text = "Surfaces, smooth shading";
            this.surfacesSmoothShadingToolStripMenuItem.Click += new System.EventHandler(this.surfacesSmoothShadingToolStripMenuItem_Click);
            // 
            // wireframeToolStripMenuItem
            // 
            this.wireframeToolStripMenuItem.Name = "wireframeToolStripMenuItem";
            this.wireframeToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.wireframeToolStripMenuItem.Text = "Wireframe";
            this.wireframeToolStripMenuItem.Click += new System.EventHandler(this.wireframeToolStripMenuItem_Click);
            // 
            // verticesToolStripMenuItem
            // 
            this.verticesToolStripMenuItem.Name = "verticesToolStripMenuItem";
            this.verticesToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.verticesToolStripMenuItem.Text = "Vertices";
            this.verticesToolStripMenuItem.Click += new System.EventHandler(this.verticesToolStripMenuItem_Click);
            // 
            // surfacesAndVerticesSmoothShadingToolStripMenuItem
            // 
            this.surfacesAndVerticesSmoothShadingToolStripMenuItem.Name = "surfacesAndVerticesSmoothShadingToolStripMenuItem";
            this.surfacesAndVerticesSmoothShadingToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.surfacesAndVerticesSmoothShadingToolStripMenuItem.Text = "Surfaces and vertices, smooth shading";
            this.surfacesAndVerticesSmoothShadingToolStripMenuItem.Click += new System.EventHandler(this.surfacesAndVerticesSmoothShadingToolStripMenuItem_Click);
            // 
            // surfacesWireframeAndVerticesSmoothShadingToolStripMenuItem
            // 
            this.surfacesWireframeAndVerticesSmoothShadingToolStripMenuItem.Name = "surfacesWireframeAndVerticesSmoothShadingToolStripMenuItem";
            this.surfacesWireframeAndVerticesSmoothShadingToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.surfacesWireframeAndVerticesSmoothShadingToolStripMenuItem.Text = "Surfaces, wireframe, and vertices, smooth shading";
            this.surfacesWireframeAndVerticesSmoothShadingToolStripMenuItem.Click += new System.EventHandler(this.surfacesWireframeAndVerticesSmoothShadingToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(335, 6);
            // 
            // translucentToolStripMenuItem
            // 
            this.translucentToolStripMenuItem.Name = "translucentToolStripMenuItem";
            this.translucentToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.translucentToolStripMenuItem.Text = "Translucent";
            this.translucentToolStripMenuItem.Click += new System.EventHandler(this.translucentToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(335, 6);
            // 
            // withTextureToolStripMenuItem
            // 
            this.withTextureToolStripMenuItem.Name = "withTextureToolStripMenuItem";
            this.withTextureToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.withTextureToolStripMenuItem.Text = "With texture";
            this.withTextureToolStripMenuItem.Click += new System.EventHandler(this.withTextureToolStripMenuItem_Click);
            // 
            // viewer3D
            // 
            this.viewer3D.BackColor = System.Drawing.Color.Black;
            this.viewer3D.CameraDistance = 4D;
            this.viewer3D.CameraLatitude = 0.39269908169872414D;
            this.viewer3D.CameraLongitude = 0.78539816339744828D;
            this.viewer3D.CameraTarget = ((OpenTK.Vector3)(resources.GetObject("viewer3D.CameraTarget")));
            this.viewer3D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer3D.Location = new System.Drawing.Point(0, 24);
            this.viewer3D.Name = "viewer3D";
            this.viewer3D.Scene = null;
            this.viewer3D.ShowSurfaces = true;
            this.viewer3D.ShowVertices = false;
            this.viewer3D.ShowWireframe = false;
            this.viewer3D.ShowWorldAxes = false;
            this.viewer3D.Size = new System.Drawing.Size(558, 487);
            this.viewer3D.TabIndex = 1;
            this.viewer3D.UseSmoothShading = false;
            this.viewer3D.VSync = false;
            // 
            // Sphere3DMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 511);
            this.Controls.Add(this.viewer3D);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Sphere3DMainForm";
            this.Text = "Sphere 3D visualizer (c) Mattias Wahde, 2016, mattias.wahde@chalmers.se";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem examplesToolStripMenuItem;
        private ThreeDimensionalVisualizationLibrary.Viewer3D viewer3D;
        private System.Windows.Forms.ToolStripMenuItem surfacesNoLightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem surfacesFlatShadingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem surfacesSmoothShadingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wireframeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verticesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem surfacesAndVerticesSmoothShadingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem surfacesWireframeAndVerticesSmoothShadingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem translucentToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem withTextureToolStripMenuItem;
    }
}

