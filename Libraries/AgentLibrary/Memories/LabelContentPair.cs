using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentLibrary.Memories
{
    public class LabelContentPair
    {
        public string Label { get; set; }
        public string Content { get; set; }

        public LabelContentPair(string label, string content)
        {
            this.Label = label;
            this.Content = content;
        }
    }
}
