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
    public class MemoryChangedItem: DialogueItem
    {

        // MW ToDo: 
        // Not yet completed ...

        private Boolean memoryWasChanged = false;

        public override Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
        {
            memoryWasChanged = false;
            ownerAgent.LongTermMemory.ItemInserted += new EventHandler(HandleMemoryChanged);
            base.Run(parameterList, out targetContext, out targetID);
            ownerAgent.LongTermMemory.ItemInserted -= new EventHandler(HandleMemoryChanged);
            return true;
        }

        private void HandleMemoryChanged(object sender, EventArgs e)
        {
            memoryWasChanged = true;
        }
    }
}
