using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AgentLibrary.Memories
{
    [DataContract]
    public abstract class MemoryItem
    {
        protected DateTime insertionTime;
        protected List<string> tagList;

        public abstract object GetContent();
        public abstract void SetContent(object itemValue);

        public MemoryItem()
        {
            tagList = new List<string>();
        }

        public abstract MemoryItem Copy();

        public string TagListAsString()
        {
            string tagListAsString = "";
            foreach (string tag in tagList) { tagListAsString += tag + " "; }
            tagListAsString = tagListAsString.TrimEnd(new char[] { ' ' });
            return tagListAsString;
        }

        [DataMember]
        public DateTime InsertionTime
        {
            get { return insertionTime; }
            set { insertionTime = value; }
        }
        
        [DataMember]
        public List<string> TagList
        {
            get { return tagList; }
            set { tagList = value; }
        }
    }
}
