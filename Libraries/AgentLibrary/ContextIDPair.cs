using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentLibrary
{
    public class ContextIDPair
    {
        public string Context { get; set; }
        public string ID { get; set; }
        public string PreviousID { get; set; }

        public ContextIDPair()
        {
            Context = "";
            ID = "";
            PreviousID = "";
        }

     /*   public ContextIDPair(string context, string id)
        {
            Context = context;
            ID = id;
        }   */
    }
}
