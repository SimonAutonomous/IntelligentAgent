using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AgentLibrary.Memories
{
    [DataContract]
    public class StringMemoryItem: MemoryItem
    {
        private string itemString;
     //   private char separationCharacter = AgentConstants.MEMORY_ITEM_SEPARATION_CHARACTER; // Used in case the itemString consists of several separate items.
     //   private char assignmentCharacter = AgentConstants.MEMORY_ITEM_ASSIGNMENT_CHARACTER;
    //    private char listLeftCharacter = AgentConstants.MEMORY_ITEM_LIST_LEFT_CHARACTER;
    //    private char listRightCharacter = AgentConstants.MEMORY_ITEM_LIST_RIGHT_CHARACTER;
    //    private char listSeparationCharacter = AgentConstants.MEMORY_ITEM_LIST_ITEM_SEPARATION_CHARACTER;
        private List<LabelContentPair> labelContentPairList = null;

        public override object GetContent()
        {
            return itemString;
        }

        // Must put the Copy() method here (MemoryItem cannot be instantiated)
        public override MemoryItem Copy()
        {
            StringMemoryItem copiedItem = new StringMemoryItem();
            copiedItem.InsertionTime = this.InsertionTime;
            copiedItem.TagList = new List<string>();
            foreach (string tag in this.tagList)
            {
                copiedItem.TagList.Add(tag);
            }
            copiedItem.ItemString = this.itemString;
            return copiedItem;
        }

        // Note: must be called before trying to access information via labels.
        public void GenerateLabelContentPairs()
        {
            labelContentPairList = new List<LabelContentPair>();
            if (itemString != null)
            {
                List<string> itemSplitList = itemString.Split(new char[] { AgentConstants.MEMORY_ITEM_SEPARATION_CHARACTER }, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (string itemSplit in itemSplitList)
                {
                    List<string> labelContentStringList = itemSplit.Split(new char[] { AgentConstants.MEMORY_ITEM_ASSIGNMENT_CHARACTER }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    string label = labelContentStringList[0].TrimStart(new char[] { ' ' }).TrimEnd(new char[] { ' ' });
                    string content = labelContentStringList[1].TrimStart(new char[] { ' ' }).TrimEnd(new char[] { ' ' });
                    LabelContentPair labelContentPair = new LabelContentPair(label, content);
                    labelContentPairList.Add(labelContentPair);
                }
            }
        }

        public override void SetContent(object itemValue)
        { 
            itemString = (string)itemValue;  // (string) not really needed, as the itemValue should BE a string.
        }

        [DataMember]
        public string ItemString
        {
            get { return itemString; }
            set { itemString = value; }
        }

        public List<LabelContentPair> LabelContentPairList
        {
            get { return labelContentPairList; }
            set { labelContentPairList = value; }
        }
    }
}
