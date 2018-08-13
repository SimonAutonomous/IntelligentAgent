using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ObjectSerializerLibrary;

namespace SerializationExample
{
    public partial class MainForm : Form
    {
        private SerializationTestClass serializationTestObject = null;

        public MainForm()
        {
            InitializeComponent();
            serializationTestObject = new SerializationTestClass();
            serializationTestObject.IntParameter = 1;
            serializationTestObject.DoubleParameter = 5.0;
            serializationTestObject.DoubleParameter2 = 2.0;
            serializationTestObject.IntegerList = new List<int>() { 1, 5, 6, 3, 6, 1 };
            ShowTestObject();
        }

        private void ShowTestObject()
        {
            objectListBox.Items.Clear();
            objectListBox.Items.Add("IntParameter: " + serializationTestObject.IntParameter.ToString());
            objectListBox.Items.Add("DoubleParameter: " + serializationTestObject.DoubleParameter.ToString());
            objectListBox.Items.Add("DoubleParameter2: " + serializationTestObject.DoubleParameter2.ToString());
            string integerListAsString = "";
            foreach (int integer in serializationTestObject.IntegerList)
            {
                integerListAsString += integer.ToString() + ",";
            }
            integerListAsString = integerListAsString.TrimEnd(new char[] { ',' });
            objectListBox.Items.Add("IntegerList: " + integerListAsString);
        }

        private void loadObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = ".XML files (*.xml)|*.xml";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    serializationTestObject = (SerializationTestClass)ObjectXmlSerializer.
                    ObtainSerializedObject(openFileDialog.FileName, typeof(SerializationTestClass));
                    ShowTestObject();
                }
            }
        }

        private void saveObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = ".XML files (*.xml)|*.xml";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ObjectXmlSerializer.SerializeObject(saveFileDialog.FileName, serializationTestObject);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
