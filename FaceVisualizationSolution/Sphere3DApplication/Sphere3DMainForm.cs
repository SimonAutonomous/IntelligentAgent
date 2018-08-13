using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using ThreeDimensionalVisualizationLibrary;
using ThreeDimensionalVisualizationLibrary.Objects;

namespace Sphere3DApplication
{
    public partial class Sphere3DMainForm : Form
    {
        private Sphere3D sphere;
        private Light light;

        public Sphere3DMainForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            light = new Light();
            light.Position[0] = 5;
            light.Position[1] = 2;
            light.Position[2] = 5;
            light.IsOn = true;
        }

        private void noLightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scene3D scene = new Scene3D();
            scene.AddObject(sphere);
            sphere = new Sphere3D();
            sphere.Generate(new List<double>() { 2, 64, 64 });
            sphere.SetUniformColor(Color.FromArgb(0, 255, 0));
            scene.LightList = new List<Light>();
            viewer3D.Scene = scene;
            viewer3D.UseSmoothShading = false;
        }

        private void lightFlatShadingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sphere = new Sphere3D();
            sphere.Generate(new List<double>() { 2, 64, 64 });
            sphere.AmbientColor = Color.FromArgb(0, 255, 0);
            sphere.DiffuseColor = Color.FromArgb(0, 255, 0);
            sphere.SpecularColor = Color.White;
            sphere.Shininess = 20;
            Scene3D scene = new Scene3D();
            scene.AddObject(sphere);
            scene.LightList.Add(light);
            viewer3D.Scene = scene;
            viewer3D.UseSmoothShading = false;
         //   viewer3D.ShowWorldAxes = true;
        }

        private void lightSmoothShadingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sphere = new Sphere3D();
            sphere.Generate(new List<double>() { 2, 64, 64 });
            sphere.AmbientColor = Color.FromArgb(0, 255, 0);
            sphere.DiffuseColor = Color.FromArgb(0, 255, 0);
            sphere.SpecularColor = Color.White;
            sphere.Shininess = 20;
            Scene3D scene = new Scene3D();
            scene.AddObject(sphere);
            scene.LightList.Add(light);
            viewer3D.Scene = scene;
            viewer3D.UseSmoothShading = true;
        }

        private void surfacesNoLightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sphere = new Sphere3D();
            sphere.Generate(new List<double>() { 2, 64, 64 });
            Scene3D scene = new Scene3D();
            scene.LightList.Add(light);
            scene.AddObject(sphere);
            sphere.SetUniformColor(Color.FromArgb(0, 255, 0));
            sphere.ShowVertices = false;
            sphere.ShowWireFrame = false;
            sphere.ShowSurfaces = true;
            sphere.UseLight = false;
            sphere.ShadingModel = ShadingModel.Flat;
            scene.LightList = new List<Light>();
            viewer3D.Scene = scene;
        }

        private void surfacesFlatShadingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sphere = new Sphere3D();
            sphere.Generate(new List<double>() { 2, 64, 64 });
            Scene3D scene = new Scene3D();
            scene.LightList.Add(light);
            scene.AddObject(sphere);
            sphere.AmbientColor =  Color.FromArgb(0, 255, 0);
            sphere.DiffuseColor = Color.FromArgb(0, 255, 0);
            sphere.SpecularColor = Color.White;
            sphere.Shininess = 20;
            sphere.ShowVertices = false;
            sphere.ShowWireFrame = false;
            sphere.ShowSurfaces = true;
            sphere.UseLight = true;
            sphere.ShadingModel = ShadingModel.Flat;
            viewer3D.Scene = scene;
        }

        private void surfacesSmoothShadingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sphere = new Sphere3D();
            sphere.Generate(new List<double>() { 2, 64, 64 });
            Scene3D scene = new Scene3D();
            scene.LightList.Add(light);
            scene.AddObject(sphere);
            sphere.AmbientColor = Color.FromArgb(0, 255, 0);
            sphere.DiffuseColor = Color.FromArgb(0, 255, 0);
            sphere.SpecularColor = Color.White;
            sphere.Shininess = 20;
            sphere.ShowVertices = false;
            sphere.ShowWireFrame = false;
            sphere.ShowSurfaces = true;
            sphere.UseLight = true;
            sphere.ShadingModel = ShadingModel.Smooth;
            viewer3D.Scene = scene;
        }

        private void wireframeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sphere = new Sphere3D();
            sphere.Generate(new List<double>() { 2, 64, 64 });
            Scene3D scene = new Scene3D();
            scene.AddObject(sphere);
            sphere.SetUniformColor(Color.FromArgb(255, 255, 255));
            sphere.ShowVertices = false;
            sphere.ShowWireFrame = true;
            sphere.ShowSurfaces = false;
            sphere.WireFrameWidth = 1f;
            viewer3D.Scene = scene;
        }

        private void verticesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sphere = new Sphere3D();
            sphere.Generate(new List<double>() { 2, 64, 64 });
            Scene3D scene = new Scene3D();
            scene.AddObject(sphere);
            sphere.ShowVertices = true;
            sphere.ShowWireFrame = false;
            sphere.ShowSurfaces = false;
            sphere.VertexSize = 2f;
            viewer3D.Scene = scene;
        }

    /*    private void wireframeAndVerticesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sphere = new Sphere3D();
            sphere.Generate(new List<double>() { 2, 64, 64 });
            Scene3D scene = new Scene3D();
            scene.AddObject(sphere);
            sphere.SetUniformColor(Color.White); // This color will be used for the wireframe
            sphere.ShowVertices = true;
            sphere.ShowWireFrame = true;
            sphere.ShowSurfaces = false;
            sphere.VertexSize = 2.5f;
            sphere.WireFrameWidth = 1.5f;
            viewer3D.Scene = scene;
        }  */

        private void surfacesAndVerticesSmoothShadingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sphere = new Sphere3D();
            sphere.Generate(new List<double>() { 2, 64, 64 });
            Scene3D scene = new Scene3D();
            scene.LightList.Add(light);
            scene.AddObject(sphere);
            sphere.AmbientColor = Color.FromArgb(0, 255, 0);
            sphere.DiffuseColor = Color.FromArgb(0, 255, 0);
            sphere.SpecularColor = Color.White;
            sphere.Shininess = 20;
            sphere.ShowVertices = true;
            sphere.ShowWireFrame = false;
            sphere.ShowSurfaces = true;
            sphere.UseLight = true;
            sphere.VertexSize = 2.5f;
            sphere.ShadingModel = ShadingModel.Smooth;
            viewer3D.Scene = scene;
        }

        private void surfacesWireframeAndVerticesSmoothShadingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sphere = new Sphere3D();
            sphere.Generate(new List<double>() { 2, 64, 64 });
            Scene3D scene = new Scene3D();
            scene.LightList.Add(light);
            scene.AddObject(sphere);
            sphere.AmbientColor = Color.FromArgb(0, 255, 0);
            sphere.DiffuseColor = Color.FromArgb(0, 255, 0);
            sphere.SpecularColor = Color.White;
            sphere.Shininess = 20;
            sphere.SetUniformColor(Color.White); // This color is used ONLY for the wireframe.
            sphere.ShowVertices = true;
            sphere.ShowWireFrame = true;
            sphere.ShowSurfaces = true;
            sphere.WireFrameWidth = 1.5f;
            sphere.VertexSize = 2.5f;
            sphere.UseLight = true;
            sphere.ShadingModel = ShadingModel.Smooth;
            sphere.Alpha = 0.5f;
            viewer3D.Scene = scene;
        }

        private void translucentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sphere = new Sphere3D();
            sphere.Generate(new List<double>() { 2, 64, 64 });
            Scene3D scene = new Scene3D();
            scene.LightList.Add(light);
            scene.AddObject(sphere);
            sphere.AmbientColor = Color.FromArgb(255, 255, 255);
            sphere.DiffuseColor = Color.FromArgb(255, 255, 255);
            sphere.SpecularColor = Color.White;
            sphere.Shininess = 20;
            sphere.ShowVertices = false;
            sphere.ShowWireFrame = false;
            sphere.ShowSurfaces = true;
            sphere.UseLight = true;
            sphere.ShadingModel = ShadingModel.Smooth;
            sphere.SetAlpha(0.5f);
            Sphere3D smallSphere = new Sphere3D();
            smallSphere.Generate(new List<double>() { 1.5, 64, 64 });
            smallSphere.AmbientColor = Color.FromArgb(0, 0, 255);
            smallSphere.DiffuseColor = Color.FromArgb(0, 0, 255);
            smallSphere.SpecularColor = Color.White;
            smallSphere.Shininess = 20;
            smallSphere.SetUniformColor(Color.White); // This color is used ONLY for the wireframe.
            smallSphere.ShowVertices = false;
            smallSphere.ShowWireFrame = false;
            smallSphere.ShowSurfaces = true;
            smallSphere.UseLight = true;
            smallSphere.ShadingModel = ShadingModel.Smooth;
            scene.AddObject(smallSphere);
            scene.SortForTranslucence();
            viewer3D.Scene = scene;
        }

        private void withTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string FILE_NAME = "./EarthTextureSmall.bmp";
            if (File.Exists(FILE_NAME))
            {
                Bitmap bitmap = (Bitmap)Image.FromFile(FILE_NAME);
                viewer3D.AddTexture(bitmap);
                Scene3D scene = new Scene3D();
                scene.LightList.Add(light);
                sphere = new Sphere3D();
                sphere.Generate(new List<double>() { 2, 64, 64 });
                sphere.AmbientColor = Color.FromArgb(255,255,255);
                sphere.DiffuseColor = Color.FromArgb(255,255,255);
                sphere.SpecularColor = Color.White;
                sphere.Shininess = 20;
                sphere.ShowVertices = false;
                sphere.ShowWireFrame = false;
                sphere.ShowSurfaces = true;
                sphere.UseLight = true;
                sphere.ShadingModel = ShadingModel.Smooth;
                sphere.SetTexture(1);
                scene.AddObject(sphere);
                viewer3D.Scene = scene;
                viewer3D.Invalidate();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Visualization of a torus (example)
        /*
        private void testremoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TorusSector3D torusSector = new TorusSector3D();
            torusSector.Generate(new List<double>() { 3, 0.2, 100, 40, 3*Math.PI / 2 - 0.3, 3*Math.PI / 2 + 0.3 });
          //  torusSector.Generate(new List<double>() { 1, 0.1, 100, 40, 0,  2*Math.PI });
            Scene3D scene = new Scene3D();
            scene.AddObject(torusSector);
            light.Position[0] = 5;
            light.Position[1] = 5;
            light.Position[2] = 10;
            scene.LightList.Add(light);
            torusSector.ShowVertices = false;
            torusSector.ShowWireFrame = false;
            torusSector.ShowSurfaces = true;
            torusSector.AmbientColor = Color.SaddleBrown;
            torusSector.DiffuseColor = Color.SaddleBrown;
            torusSector.SpecularColor = Color.White;
            torusSector.Shininess = 50;
            torusSector.VertexSize = 2f;
            torusSector.ShadingModel = ShadingModel.Smooth;
            torusSector.UseLight = true;
            viewer3D.Scene = scene;
            viewer3D.ShowWorldAxes = true;
        }  */



    }
}
