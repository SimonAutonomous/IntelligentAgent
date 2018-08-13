using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AgentLibrary.Memories;

namespace AgentLibrary.DialogueItems
{
    [DataContract]
    public abstract class DialogueItem
    {
        protected string id;
        protected Agent ownerAgent; // Pointer to the agent
        protected int repetitionCount;
        protected Boolean doReset = true;

        public virtual void Initialize(Agent ownerAgent)
        {
            this.ownerAgent = ownerAgent;
            repetitionCount = 0;
        }

        public void ResetRepetitionCount()
        {
            if (doReset)
            {
                repetitionCount = 0;
            }
        }

        public virtual Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
        {
            repetitionCount++;
            // These values are set in the derived classes, but must be initialized here, due to the "out" keyword.
            targetContext = "";  
            targetID = "";
            return true;
        }

        [DataMember]
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public Boolean DoReset
        {
            get { return doReset; }
            set { doReset = value; }
        }

        public int RepetitionCount
        {
            get { return repetitionCount; }
        }
    }
}
