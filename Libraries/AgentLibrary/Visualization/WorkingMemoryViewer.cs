using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AgentLibrary.Memories;
using CustomUserControlsLibrary;

namespace AgentLibrary.Visualization
{
    public partial class WorkingMemoryViewer : ColorListBox
    {
        private const int DEFAULT_NUMBER_OF_VISIBLE_ITEMS = 50;
        private const string DEFAULT_NON_DISPLAYABLE_MESSAGE = "<Not displayed>";

        private WorkingMemory workingMemory = null;
        private int numberOfVisibleItems = DEFAULT_NUMBER_OF_VISIBLE_ITEMS;
        private Color contextIDColor = Color.White;
        private Color memoryItemForeColor = Color.Lime;
        private string nonDisplayableMessage = DEFAULT_NON_DISPLAYABLE_MESSAGE;

        public WorkingMemoryViewer()
        {
            InitializeComponent();
        }

        public void ShowMemory()
        {
            this.Items.Clear();
            ShowContextID();
         /*   ColorListBoxItem contextIDColorListBoxItem = new ColorListBoxItem("Current context: " + workingMemory.CurrentContext + " Current ID: " + workingMemory.CurrentID,
                this.BackColor, contextIDColor);
            this.Items.Add(contextIDColorListBoxItem);  */
            List<MemoryItem> itemList = workingMemory.TryGetItems(numberOfVisibleItems);
            if (itemList != null)
            {
                foreach (MemoryItem item in itemList)
                {
                    string displayString = "[" + item.InsertionTime.ToString() + "] " + " Tags: " + item.TagListAsString() + " Content: ";
                    if (item is StringMemoryItem)
                    {
                        string contentString = (string)item.GetContent();
                        displayString += contentString;
                    }
                    else
                    {
                        displayString += nonDisplayableMessage;
                    }
                    ColorListBoxItem memoryItemColorListBoxItem = new ColorListBoxItem(displayString, this.BackColor, memoryItemForeColor);
                    this.Items.Add(memoryItemColorListBoxItem);
                }
            }
        }

        private void ShowContextID()
        {
            if (this.Items.Count > 0) { this.Items.RemoveAt(0); }
            ColorListBoxItem contextIDColorListBoxItem = new ColorListBoxItem("Current context: " + workingMemory.CurrentContext + " Current ID: " + workingMemory.CurrentID +
                " Previous ID: " + workingMemory.PreviousID, this.BackColor, contextIDColor);
            this.Items.Insert(0, contextIDColorListBoxItem);
        }

        private void ThreadSafeShowMemory(object sender, EventArgs e)
        {
            if (InvokeRequired) { this.BeginInvoke(new MethodInvoker(() => ShowMemory())); }
            else { ShowMemory(); }
        }

        private void ThreadSafeShowContextID(object sender, EventArgs e)
        {
            if (InvokeRequired) { this.BeginInvoke(new MethodInvoker(() => ShowContextID())); }
            else { ShowContextID(); }
        }

        public void SetWorkingMemory(WorkingMemory workingMemory)
        {
            this.workingMemory = workingMemory;
            this.workingMemory.ItemInserted -= new EventHandler(ThreadSafeShowMemory); // Just in case... (should not be needed).
            this.workingMemory.ItemInserted += new EventHandler(ThreadSafeShowMemory);
            this.workingMemory.ItemDeleted -= new EventHandler(ThreadSafeShowMemory);
            this.workingMemory.ItemDeleted += new EventHandler(ThreadSafeShowMemory);
            this.workingMemory.CurrentContextChanged -= new EventHandler(ThreadSafeShowContextID);
            this.workingMemory.CurrentContextChanged += new EventHandler(ThreadSafeShowContextID);
            this.workingMemory.CurrentIDChanged -= new EventHandler(ThreadSafeShowContextID);
            this.workingMemory.CurrentIDChanged += new EventHandler(ThreadSafeShowContextID);
        }
    }
}
