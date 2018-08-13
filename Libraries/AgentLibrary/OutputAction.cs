using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AgentLibrary.Memories;

namespace AgentLibrary
{
    [DataContract]
    public class OutputAction
    {
        private List<Pattern> patternList;
        private string targetContext;
        private string targetID; // If >= 0 target the specific ID, otherwise allow any ID within the given context

        public OutputAction(string targetContext, string targetID)
        {
            this.targetContext = targetContext;
            this.targetID = targetID;
            patternList = new List<Pattern>();
        }

        public OutputAction()
        {
            targetContext = "";
            targetID = "";
            patternList = new List<Pattern>();
        }

        // Used for generating agent output (for the speech process): Generates a random output string based on the available patterns
        public string GetString(Random randomNumberGenerator, List<StringMemoryItem> queryMemoryItemList)
        {
            if (patternList.Count == 0) { return null; }
            else
            {
                int index = randomNumberGenerator.Next(0, patternList.Count);
                string patternString = patternList[index].GetString(randomNumberGenerator, queryMemoryItemList);
                return patternString;
            }
        }

        // Used for generating output to the face or internet processes. This method does not remove { ... } as
        // get string does. Moreover, it simply picks the first pattern (no reason to have more than one
        // since, in this case, the pattern should have a very specific form
        public string GetVerbatimString()
        {
            if (patternList.Count == 0) { return null; }
            else
            {
                string patternString = patternList[0].GetVerbatimString();
                return patternString;
            }
        }

        [DataMember]
        public List<Pattern> PatternList
        {
            get { return patternList; }
            set { patternList = value; }
        }

        [DataMember]
        public string TargetContext
        {
            get { return targetContext; }
            set { targetContext = value; }
        }

        [DataMember]
        public string TargetID
        {
            get { return targetID; }
            set { targetID = value; }
        }
    }
}
