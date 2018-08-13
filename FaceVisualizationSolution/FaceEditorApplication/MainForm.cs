using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ObjectSerializerLibrary;
using ThreeDimensionalVisualizationLibrary;
using ThreeDimensionalVisualizationLibrary.Faces;
using ThreeDimensionalVisualizationLibrary.Objects;

namespace FaceEditorApplication
{
    public partial class MainForm : Form
    {
        private Face face;

        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            face = new Face();
            face.Initialize();
            faceEditor.SetFace(face);
            faceEditor.Initialize();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "XML files (*.xml)|*.xml";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ObjectXmlSerializer.SerializeObject(saveFileDialog.FileName, faceEditor.Face);
                }
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "XML files (*.xml)|*.xml";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Face face = (Face)ObjectXmlSerializer.ObtainSerializedObject(openFileDialog.FileName, typeof(Face));
                    face.Generate(new List<double> { face.NumberOfLongitudePoints });
                    face.Visible = true; // This property is not serialized ...
                    faceEditor.SetFace(face);
                    faceEditor.Initialize();
                }
            }
        }
    }
}
