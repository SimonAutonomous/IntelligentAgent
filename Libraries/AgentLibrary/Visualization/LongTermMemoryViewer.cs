using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomUserControlsLibrary;
using AgentLibrary.Memories;

namespace AgentLibrary.Visualization
{
    public partial class LongTermMemoryViewer : ColorListBox
    {
        private Memory longTermMemory;

        public LongTermMemoryViewer()
        {
            InitializeComponent();
        }

        public void ThreadSafeShowItems()
        {
            if (InvokeRequired) { this.BeginInvoke(new MethodInvoker(() => ShowItems())); }
            else { ShowItems();  }
        }

        public void ShowItems()
        {
            this.Items.Clear();
            foreach (MemoryItem memoryItem in longTermMemory.ItemList)
            {
                string itemAsString = "[Insertion time] " + memoryItem.InsertionTime.ToString() + " " +
                     " [Tags] " + memoryItem.TagListAsString();
                if (memoryItem is StringMemoryItem)
                {
                    itemAsString += " [Content] " + (string)memoryItem.GetContent();
                }
                else
                {
                    itemAsString += " [Content] <" + memoryItem.GetContent().GetType().Name + ">";
                }
                ColorListBoxItem item = new ColorListBoxItem(itemAsString, this.BackColor, this.ForeColor);
                this.Items.Add(item);
            }
        }

        private void HandleItemInserted(object sender, EventArgs e)
        {
            ThreadSafeShowItems();
        }

        public void SetMemory(Memory longTermMemory)
        {
            this.SelectionMode = SelectionMode.One;
            this.SelectedItemBackColor = Color.White;
            this.SelectedItemForeColor = Color.Red;
            this.longTermMemory = longTermMemory;
            this.longTermMemory.ItemInserted -= new EventHandler(HandleItemInserted);
            this.longTermMemory.ItemInserted += new EventHandler(HandleItemInserted);
            ThreadSafeShowItems();
        }

        public void UpdateView()
        {
            ThreadSafeShowItems();
        }
    }
}
