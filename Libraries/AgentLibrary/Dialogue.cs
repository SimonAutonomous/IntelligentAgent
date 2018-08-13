using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AgentLibrary.Memories;
using AgentLibrary.DialogueItems;

namespace AgentLibrary
{
    [DataContract]
    public class Dialogue
    {
        private List<DialogueItem> dialogueItemList;
        private Boolean isAlwaysAvailable; // True for dialogues that are available at any time (e.g. asking for the current time). Normally false.
        private string context;

        public Dialogue()
        {
            isAlwaysAvailable = false;
            dialogueItemList = new List<DialogueItem>();
        }

        public Dialogue(string context, Boolean isAlwaysAvailable)
        {
            this.dialogueItemList = new List<DialogueItem>();
            this.isAlwaysAvailable = isAlwaysAvailable;
            this.context = context;
        }

        public void Initialize(Agent ownerAgent)
        {
            foreach (DialogueItem dialogueItem in dialogueItemList)
            {
                dialogueItem.Initialize(ownerAgent);
            }
        }

        public void ResetRepetitionCount()
        {
            foreach (DialogueItem dialogueItem in dialogueItemList)
            {
                dialogueItem.ResetRepetitionCount();
            }
        }

        [DataMember]
        public List<DialogueItem> DialogueItemList
        {
            get { return dialogueItemList; }
            set { dialogueItemList = value; }
        }

        [DataMember]
        public Boolean IsAlwaysAvailable
        {
            get { return isAlwaysAvailable; }
            set { isAlwaysAvailable = value; }
        }

        [DataMember]
        public string Context
        {
            get { return context; }
            set { context = value; }
        }

     /*   [DataMember]
        public Boolean Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }  */
    }
}
