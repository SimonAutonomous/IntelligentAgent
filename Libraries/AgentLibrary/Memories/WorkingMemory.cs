using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AgentLibrary.Memories
{
    public class WorkingMemory: Memory
    {
        private List<ContextIDPair> contextIDPairList;
        private string currentContext;
        private string currentID;
        private string previousID;

        public event EventHandler CurrentContextChanged = null;
        public event EventHandler CurrentIDChanged = null;

        public WorkingMemory(): base()
        {
            contextIDPairList = new List<ContextIDPair>();
            currentContext = "";
            currentID = "";
            previousID = "";
        }

        private void OnCurrentContextChanged()
        {
            if (CurrentContextChanged != null)
            {
                EventHandler handler = CurrentContextChanged;
                handler(this, EventArgs.Empty);
            }
        }
        
        private void OnCurrentIDChanged()
        {
            if (CurrentIDChanged != null)
            {
                EventHandler handler = CurrentIDChanged;
                handler(this, EventArgs.Empty);
            }
        }

        public string CurrentContext
        {
            get { return currentContext; }
            set
            {
                string oldContext = currentContext;
                currentContext = value;
                if (currentContext != oldContext)
                {
                    if (currentContext == "")  
                    {
                        currentID = "";
                        previousID = "";
                        if (contextIDPairList.Count > 0)
                        {
                            if (contextIDPairList[0].Context == oldContext)  // The oldContext dialogue has been completed ...
                            {
                                contextIDPairList.RemoveAt(0);  // ... so remove it from the list
                                if (contextIDPairList.Count > 0)
                                {
                                    ContextIDPair contextIDPair = contextIDPairList[0];
                                    currentContext = contextIDPair.Context;
                                    currentID = contextIDPair.ID;
                                    previousID = contextIDPair.PreviousID;
                                }
                            }
                        }
                    }
                    else
                    {
                        // Check if the current context is already available in the contextIDPairList (<=> a paused dialogue)    
                        ContextIDPair contextIDPair = contextIDPairList.Find(c => c.Context == currentContext); // If found then set the contextIDPair to the value found.
                        if ((contextIDPair == null) && (currentContext != ""))  // Not available => no paused dialogue found for this context.
                        {
                            currentID = "";
                            contextIDPair = new ContextIDPair();  // => ID = "", PreviousID = "".
                            contextIDPair.Context = currentContext;
                            contextIDPairList.Insert(0, contextIDPair);
                        }
                    }                            
                    OnCurrentContextChanged();
                }
            }
        }

        public string CurrentID
        {
            get { return currentID; }
            set
            {
                string oldID = currentID;
                currentID = value;
                if (currentID != oldID)
                {
                    previousID = oldID;
                    if (contextIDPairList.Count > 0)
                    {
                        ContextIDPair contextIDPair = contextIDPairList[0]; // Change of ID in the current context
                        contextIDPair.ID = currentID;
                        contextIDPair.PreviousID = previousID;
                    }
                    OnCurrentIDChanged();
                }
            }
        }

        public string PreviousID
        {
            get { return previousID; }
        }

        public void RemoveContextIDPair(string context)
        {
            int index = contextIDPairList.FindIndex(c => c.Context == context);
            ContextIDPairList.RemoveAt(index);
        }

        public List<ContextIDPair> ContextIDPairList
        {
            get { return contextIDPairList; }
        }

    }
}
