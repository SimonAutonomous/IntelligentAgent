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
    public class IntegerArithmeticItem : DialogueItem
    {
        private string operation;
        private List<string> inputQueryTagList; // The tags (in long-term memory) for the type of items that are to be checked.
        private string outputQueryTag;
        private string outputTargetContext;
        private string outputTargetID;

        public IntegerArithmeticItem()
        {
            inputQueryTagList = new List<string>();
        }

        public IntegerArithmeticItem(string id, string operation, List<string> inputQueryTagList, string outputQueryTag, string outputTargetContext, string outputTargetID)
        {
            this.id = id;
            this.operation = operation;
            this.inputQueryTagList = inputQueryTagList;
            this.outputQueryTag = outputQueryTag;
            this.outputTargetContext = outputTargetContext;
            this.outputTargetID = outputTargetID;
        }

        public override Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
        {
            base.Run(parameterList, out targetContext, out targetID);
            List<double> operandList = new List<double>();
            foreach (string inputQueryTag in inputQueryTagList)
            {
                MemoryItem itemSought = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTag);
                string itemSoughtString = (string)itemSought.GetContent();
                double operand;
                Boolean inputOK = double.TryParse(itemSoughtString, out operand);
                if (inputOK)
                {
                    operandList.Add(operand);
                }
                else { return false; }
            }
            double output = 0;
            if (operation.ToLower() == "addition")
            {
                output = 0;
                foreach (double operand in operandList) { output += operand; }
            }
            else if (operation.ToLower() == "subtraction")
            {
                if (operandList.Count == 2)
                {
                    output = operandList[0] - operandList[1];
                }
                else { return false; }
            }
            else if (operation.ToLower() == "multiplication")
            {
                output = 1;
                foreach (double operand in operandList) { output *= operand; }
            }
            else { return false; }
            StringMemoryItem matchMemoryItem = new StringMemoryItem();
            matchMemoryItem.TagList = new List<string>() { outputQueryTag };
            matchMemoryItem.SetContent(output.ToString());
            ownerAgent.WorkingMemory.AddItem(matchMemoryItem);
            targetContext = outputTargetContext;
            targetID = outputTargetID;
            return true;
        }

        [DataMember]
        public List<string> InputQueryTagList
        {
            get { return inputQueryTagList; }
            set { inputQueryTagList = value; }
        }

        [DataMember]
        public string OutputQueryTag
        {
            get { return outputQueryTag; }
            set { outputQueryTag = value; }
        }

        [DataMember]
        public string OutputTargetContext
        {
            get { return outputTargetContext; }
            set { outputTargetContext = value; }
        }

        [DataMember]
        public string OutputTargetID
        {
            get { return outputTargetID; }
            set { outputTargetID = value; }
        }

        [DataMember]
        public string Operation
        {
            get { return operation; }
            set { operation = value; }
        }
    }
}
