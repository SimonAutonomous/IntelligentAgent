using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentLibrary
{
    public class PatternItem
    {
        private List<string> positiveList;
        private List<string> negativeList;

        public PatternItem()
        {
            positiveList = new List<string>();
            negativeList = new List<string>();
        }

        public List<string> PositiveList
        {
            get { return positiveList; }
            set { positiveList = value; }
        }

        public List<string> NegativeList
        {
            get { return negativeList; }
            set { negativeList = value; }
        }
    }
}
