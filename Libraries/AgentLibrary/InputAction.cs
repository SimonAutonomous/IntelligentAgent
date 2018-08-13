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
    public class InputAction
    {
        private List<Pattern> patternList;
        private string targetContext;
        private string targetID; // If >= 0 target the specific ID, otherwise allow any ID within the given context
        private string requiredSource = AgentConstants.LISTENER_INPUT_TAG; // Default value.

        public InputAction(string targetContext, string targetID)
        {
            this.targetContext = targetContext;
            this.targetID = targetID;
            patternList = new List<Pattern>();
            requiredSource = AgentConstants.LISTENER_INPUT_TAG;
        }

        public InputAction()
        {
            targetContext = "";
            targetID = "";
            patternList = new List<Pattern>();
            requiredSource = AgentConstants.LISTENER_INPUT_TAG;  
        }

        public Boolean CheckMatch(string inputString, string sourceTag, out Pattern matchingPattern)
        {
            matchingPattern = null;
            if (sourceTag == requiredSource)
            {
                foreach (Pattern pattern in patternList)
                {
                    if (pattern.IsMatching(inputString))
                    {
                        matchingPattern = pattern;
                        return true;
                    }
                }
            }
            return false;
        }

        [DataMember]
        public List<Pattern> PatternList
        {
            get { return patternList; }
            set { patternList = value; }
        }

        [DataMember]
        public string RequiredSource
        {
            get { return requiredSource; }
            set { requiredSource = value; }
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
