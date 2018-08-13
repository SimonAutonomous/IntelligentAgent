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

namespace AgentLibrary.Visualization
{
    public partial class MemoryItemDataGridView : DataGridView
    {
        private const int MARGIN = 30;

        private MemoryItem memoryItem;

        public event EventHandler ItemChanged = null;

        public MemoryItemDataGridView()
        {
            InitializeComponent();
        }

        private void OnItemChanged()
        {
            if (ItemChanged != null)
            {
                EventHandler handler = ItemChanged;
                handler(this, EventArgs.Empty);
            }
        }

        private void InitializeRows()
        {
            this.Rows.Clear();
            this.Columns.Clear();
            this.RowHeadersVisible = true;
            this.ColumnHeadersVisible = false;
            this.Columns.Add("valueColumn", "");
            this.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.Rows.Add("");
            this.Rows.Add("");
            this.Rows.Add("");
            this.Rows[0].HeaderCell.Value = "TagList";
            this.Rows[1].HeaderCell.Value = "InsertionTime";
            this.Rows[1].ReadOnly = true;
            this.Rows[2].HeaderCell.Value = "StringValue";
            Bitmap tmpBitmap = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(tmpBitmap);
            float headerWidth = g.MeasureString("InsertionTime", this.Font).Width + MARGIN;
            this.RowHeadersWidth = (int)headerWidth;
            this.Columns[0].Width = this.Width - (int)headerWidth;
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToOrderColumns = false;
            this.AllowUserToResizeColumns = false;
            this.AllowUserToResizeRows = false;
        }

        protected override void OnCellEndEdit(DataGridViewCellEventArgs e)
        {
            base.OnCellEndEdit(e);
            if (e.RowIndex == 0)
            {
                List<string> tagList = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                this.memoryItem.TagList = tagList;
                OnItemChanged();
            }
            else if (e.RowIndex == 2)
            {
                string stringValue = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                this.memoryItem.SetContent(stringValue);
                OnItemChanged();
            }
        }

        public void SetMemoryItem(MemoryItem memoryItem)
        {
            this.memoryItem = memoryItem;
            InitializeRows();
            this.Rows[0].Cells[0].Value = memoryItem.TagListAsString();
            this.Rows[1].Cells[0].Value = memoryItem.InsertionTime.ToString();
            string memoryValueString = "<Not displayable>";
            if (memoryItem is StringMemoryItem) { memoryValueString = (string)memoryItem.GetContent(); }
            this.Rows[2].Cells[0].Value = memoryValueString;
        }

        public MemoryItem MemoryItem
        {
            get { return this.memoryItem; }
        }
    }
}
