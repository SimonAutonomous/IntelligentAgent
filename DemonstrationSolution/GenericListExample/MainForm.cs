using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GenericListExample
{
    public partial class MainForm : Form
    {
        private List<TestClass> list1;
        private List<TestClass> list2;
        private List<TestClass> list3;
        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShowList(string description, List<int> integerList)
        {
            displayTextBox.Text += description.PadRight(30) + " ";
            foreach (int element in integerList)
            {
                displayTextBox.Text += element.ToString() + " ";
            }
            displayTextBox.Text += "\r\n"; // new line
        }

        private void runExample1Button_Click(object sender, EventArgs e)
        {
            List<int> integerList1 = new List<int>();  // => { }
            integerList1.Add(5);    // => {5}
            integerList1.Add(8);    // => {5,8}
            integerList1.Add(-1);   // => {5,8,1}
            ShowList("Addition of elements:", integerList1);
            integerList1.Sort();    // => {-1, 5, 8}
            ShowList("Sorting:", integerList1);
            integerList1.Reverse(); // => {8, 5 -1}
            ShowList("Reversal:", integerList1);
            integerList1.Insert(0, 3); // => {3, 8, 5, -1}
            ShowList("Insertion:", integerList1);
            integerList1.RemoveAt(2); // => {3,8,-1}
            ShowList("Removal at index 2:", integerList1);
            List<int> integerList2 = integerList1; // list2 points to list1!
            ShowList("Pointer to list:", integerList2);
            integerList1[1] = 2; // => Assigns 2 to integerList1[1] AND integerList2[1] (both are the same list!)
            ShowList("List 1, element 1 modified: ", integerList1);
            ShowList("...and list2:", integerList2);
            List<int> integerList3 = new List<int>(); // A new instance ...
            foreach (int element in integerList1) { integerList3.Add(element); }
            integerList3[1] = 7; // => Assigns 7 to integerList3[1] but NOT integerList1[1]
            ShowList("List 1, again: ", integerList1);
            ShowList("...and list3:", integerList3);
        }

        private void ShowTestClassList(string information, List<TestClass> testClassList)
        {
            displayTextBox.Text += information + "\r\n";
            foreach (TestClass testObject in testClassList)
            {
                displayTextBox.Text += testObject.AsString() + "\r\n";
            }
            displayTextBox.Text += "\r\n";
        }

        // Generates the first list in Examples 2, 3, and 4.
        // See Appendix A.3 of the compendium.
        private void GenerateList1()
        {
            list1 = new List<TestClass>();
            TestClass testObject1 = new TestClass();
            testObject1.IntegerProperty = 4;
            testObject1.DoubleProperty = 0.5;
            list1.Add(testObject1);
            TestClass testObject2 = new TestClass();
            testObject2.IntegerProperty = 2;
            testObject2.DoubleProperty = 1.5;
            list1.Add(testObject2);
            TestClass testObject3 = new TestClass();
            testObject3.IntegerProperty = 5;
            testObject3.DoubleProperty = -1.5;
            list1.Add(testObject3);
            TestClass testObject4 = new TestClass();
            testObject4.IntegerProperty = 2;
            testObject4.DoubleProperty = -0.5;
            list1.Add(testObject4);        
        }

        private void runExample2Button_Click(object sender, EventArgs e)
        {
            displayTextBox.Text = "";
            GenerateList1();
            ShowTestClassList("Initial list", list1);
            list1.Sort((a, b) => a.DoubleProperty.CompareTo(b.DoubleProperty));
            ShowTestClassList("List sorted (DoubleProperty)", list1);
            list1 = (List<TestClass>)list1.OrderBy(a => a.IntegerProperty).
                                           ThenBy(b => b.DoubleProperty).ToList();
            ShowTestClassList("List sorted (IntegerProperty, then DoubleProperty)", list1);
        }

        private void runExample3Button_Click(object sender, EventArgs e)
        {
            displayTextBox.Text = "";
            GenerateList1();
            ShowTestClassList("List1", list1);
            // Shallow copy
            list2 = new List<TestClass>();
            list2.Add(list1[0]);
            list2.Add(list1[1]);
            list2.Add(list1[2]);
            list2.Add(list1[3]);
            ShowTestClassList("List 2", list1);
            list2[0].DoubleProperty = -1; // Changes list2[0] AND list1[0].
            ShowTestClassList("List 2 again", list2);
            ShowTestClassList("List 1 again", list1);
        }

        private void runExample4Button_Click(object sender, EventArgs e)
        {
            displayTextBox.Text = "";
            GenerateList1();
            ShowTestClassList("List1", list1);
            // Deep copy
            list3 = new List<TestClass>();
            list3.Add(list1[0].Copy());  // a bit inelegant (function call within function call), but OK here...
            list3.Add(list1[1].Copy());
            list3.Add(list1[2].Copy());
            list3.Add(list1[3].Copy());
            ShowTestClassList("List 3", list1);
            list3[0].DoubleProperty = -5; // Changes ONLY list3[0].
            ShowTestClassList("List 3 again", list3);
            ShowTestClassList("List 1 again", list1);
        }
    }
}
