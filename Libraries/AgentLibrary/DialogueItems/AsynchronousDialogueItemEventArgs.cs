using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentLibrary.DialogueItems
{
    public class AsynchronousDialogueItemEventArgs: EventArgs
    {
        private string originalContext; // => the current context when the asynchronous dialogue item was started.
        private string targetContext;
        private string targetID;

        public AsynchronousDialogueItemEventArgs(string originalContext, string targetContext, string targetID)
        {
            this.originalContext = originalContext;
            this.targetContext = targetContext;
            this.targetID = targetID;
        }

        public string OriginalContext  
        {
            get { return originalContext; }
            set { originalContext = value; }
        }

        public string TargetContext
        {
            get { return targetContext; }
            set { targetContext = value; }
        }

        public string TargetID
        {
            get { return targetID; }
            set { targetID = value; }
        }
    }
}
