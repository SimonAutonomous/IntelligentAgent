using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AgentLibrary.Memories;

namespace AgentLibrary.DialogueItems
{
    [DataContract]
    public class MemorySearchItem: DialogueItem
    {
        private string memoryIdentifier;
        private List<string> inputQueryTagList; // The tag (in working memory) for the item sought.
        private List<string> searchTagList; // The tags (in long-term memory) for the type of items that are to be checked.
        private TagSearchMode tagSearchMode; // Either: Or (any matching tag sufficient) or And (all tags must match).
        private string identificationLabel; // The label whose value must match (exactly or partially; see below) the value in the input query
     //   private LabelIdentificationMode labelIdentificationMode;
        private string searchLabel; // The label of the aspect sought.
        private string outputQueryTag; // The tag (in working memory) for the result obtained
         
        private string successTargetContext;
        private string successTargetID;
        private string failureTargetContext;
        private string failureTargetID;

        public MemorySearchItem(string id, string memoryIdentifier, List<string> inputQueryTagList, List<string> searchTagList, TagSearchMode tagSearchMode,
                                string identificationLabel, string searchLabel, string outputQueryTag, string successTargetContext, string successTargetID,
                                string failureTargetContext, string failureTargetID)
        {
            this.id = id;
            this.memoryIdentifier = memoryIdentifier;
            this.inputQueryTagList = inputQueryTagList;
            this.searchTagList = searchTagList;
            this.tagSearchMode = tagSearchMode;
            this.identificationLabel = identificationLabel;
            this.searchLabel = searchLabel;
            this.outputQueryTag = outputQueryTag;
            this.successTargetContext = successTargetContext;
            this.successTargetID = successTargetID;
            this.failureTargetContext = failureTargetContext;
            this.failureTargetID = failureTargetID;
        }

        public MemorySearchItem()
        {
            inputQueryTagList = new List<string>();
            searchTagList = new List<string>();
            tagSearchMode = TagSearchMode.Or;
        //    labelIdentificationMode = LabelIdentificationMode.Exact;
            memoryIdentifier = AgentConstants.LONG_TERM_MEMORY_NAME; // Search long-term memory per default.
        }

        public override Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
        {
            base.Run(parameterList, out targetContext, out targetID);
            Boolean labelFound = false;
            List<string> itemSoughtStringList = new List<string>();
            foreach (string inputQueryTag in inputQueryTagList)
            {
                MemoryItem itemSought = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTag);
                if (itemSought != null)  // 20171201
                {
                    string itemSoughtString = (string)itemSought.GetContent();
                    itemSoughtStringList.Add(itemSoughtString);
                }
            }
            List<MemoryItem> tagMatchingItemList = null;
            if (memoryIdentifier == AgentConstants.LONG_TERM_MEMORY_NAME)
            {
                tagMatchingItemList = ownerAgent.LongTermMemory.GetAllItemsByTagList(searchTagList, tagSearchMode);
            }
            else
            {
                tagMatchingItemList = ownerAgent.WorkingMemory.GetAllItemsByTagList(searchTagList, tagSearchMode);
            }
            if (tagMatchingItemList != null)
            {
                foreach (MemoryItem memoryItem in tagMatchingItemList)
                {
                    if (memoryItem is StringMemoryItem)  // The only case covered, so far.
                    {
                        StringMemoryItem stringMemoryItem = (StringMemoryItem)memoryItem;
                        stringMemoryItem.GenerateLabelContentPairs();
                        LabelContentPair labelContentPair = null;

                        labelContentPair = stringMemoryItem.LabelContentPairList.Find(lcp => (lcp.Label == identificationLabel)); // && (lcp.Content.ToLower() == itemSoughtString));
                        if (labelContentPair != null)  // 20171201
                        {
                            foreach (string itemSoughtString in itemSoughtStringList)
                            {
                                if (!labelContentPair.Content.ToLower().Contains(itemSoughtString))
                                {
                                    labelContentPair = null;
                                    break;
                                }
                            }
                        }


                        if (labelContentPair != null)
                        {
                            // A match was found. Now check if the searchLabel can be found as well:
                            LabelContentPair searchLabelContentPair = stringMemoryItem.LabelContentPairList.Find(lcp => lcp.Label == searchLabel);
                            if (searchLabelContentPair != null)
                            {
                                labelFound = true;
                                StringMemoryItem matchMemoryItem = new StringMemoryItem();
                                matchMemoryItem.TagList = new List<string>() { outputQueryTag };
                                matchMemoryItem.SetContent(searchLabelContentPair.Content);
                                ownerAgent.WorkingMemory.AddItem(matchMemoryItem);
                                break;
                            }
                        }
                    }
                }
            }
            if (labelFound)
            {
                targetContext = successTargetContext;  
                targetID = successTargetID; 
            }
            else
            {
                targetContext = failureTargetContext;
                targetID = failureTargetID;
            }
            return labelFound;
        }

        [DataMember]
        public string MemoryIdentifier
        {
            get { return memoryIdentifier; }
            set { memoryIdentifier = value; }
        }

        [DataMember]
        public List<string> InputQueryTagList
        {
            get { return inputQueryTagList; }
            set { inputQueryTagList = value; }
        }

        [DataMember]
        public List<string> SearchTagList
        {
            get { return searchTagList; }
            set { searchTagList = value; }
        }

        [DataMember]
        public TagSearchMode TagSearchMode
        {
            get { return TagSearchMode; }
            set { tagSearchMode = value; }
        }  

        [DataMember]
        public string IdentificationLabel
        {
            get { return identificationLabel; }
            set { identificationLabel = value; }
        }

    /*    [DataMember]
        public LabelIdentificationMode LabelIdentificationMode
        {
            get { return labelIdentificationMode; }
            set { labelIdentificationMode = value; }
        }  */

        [DataMember]
        public string SearchLabel
        {
            get { return searchLabel; }
            set { searchLabel = value; }
        }

        [DataMember]
        public string OutputQueryTag
        {
            get { return outputQueryTag; }
            set { outputQueryTag = value; }
        }

        [DataMember]
        public string SuccessTargetContext
        {
            get { return successTargetContext; }
            set { successTargetContext = value; }
        }

        [DataMember]
        public string SuccessTargetID
        {
            get { return successTargetID; }
            set { successTargetID = value; }
        }

        [DataMember]
        public string FailureTargetContext
        {
            get { return failureTargetContext; }
            set { failureTargetContext = value; }
        }

        [DataMember]
        public string FailureTargetID
        {
            get { return failureTargetID; }
            set { failureTargetID = value; }
        }
    }
}
