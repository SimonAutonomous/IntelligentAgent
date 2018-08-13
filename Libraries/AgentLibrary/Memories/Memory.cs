using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AgentLibrary.Memories
{
    [DataContract]
    public class Memory
    {
        protected List<MemoryItem> itemList;

        public event EventHandler ItemInserted = null;
        public event EventHandler ItemDeleted = null;

        public static object lockObject = new object();

        private void OnItemInserted()
        {
            if (ItemInserted != null)
            {
                EventHandler handler = ItemInserted;
                handler(this, EventArgs.Empty);
            }
        }

        private void OnItemDeleted()
        {
            if (ItemDeleted != null)
            {
                EventHandler handler = ItemDeleted;
                handler(this, EventArgs.Empty);
            }
        }

        public Memory()
        {
            itemList = new List<MemoryItem>();
        }

        // Sets the dateTime of the item to the current time (the time of insertion), adds
        // the item, and then triggers the Changed event.
        public void AddItem(MemoryItem addedItem)
        {
            Monitor.Enter(lockObject);
            addedItem.InsertionTime = DateTime.Now;
            itemList.Insert(0, addedItem);
            Monitor.Exit(lockObject);
            OnItemInserted();
        }

        public void AddItems(List<MemoryItem> addedItemList)
        {
            Monitor.Enter(lockObject);
            DateTime insertionDateTime = DateTime.Now;
            foreach (MemoryItem addedItem in addedItemList)
            {
                addedItem.InsertionTime = insertionDateTime;
                itemList.Insert(0, addedItem);
            }
            Monitor.Exit(lockObject);
            OnItemInserted();
        }

        // Removes old items.
        public void RemoveItems(double maximumAge)
        {
            Boolean anyItemDeleted = false;
            DateTime now = DateTime.Now;
            Monitor.Enter(lockObject);
            int index = 0;
            while (index < itemList.Count)
            {
                double age = (now - itemList[index].InsertionTime).TotalSeconds;
                if (age > maximumAge)
                {
                    itemList.RemoveAt(index);
                    anyItemDeleted = true;
                }
                else { index++; }
            }
            Monitor.Exit(lockObject);
            if (anyItemDeleted) { OnItemDeleted(); }
        }

     /*   public MemoryItem GetItemByTag(string tag, DateTime firstAllowedTime)
        {
            if (itemList.Count == 0) { return null; }
            else
            {
                Monitor.Enter(lockObject);
                int index = 0;
                DateTime dateTime = itemList[index].InsertionTime;
                while (dateTime > firstAllowedTime)
                {
                    MemoryItem item = itemList[index];
                    if (item.TagList.Contains(tag))
                    {
                        MemoryItem accessedItem = item.Copy();
                        Monitor.Exit(lockObject);
                        return accessedItem;
                    }
                    index++;
                    if (index >= itemList.Count) { break; }
                    else { dateTime = itemList[index].InsertionTime; }
                }
                Monitor.Exit(lockObject);
                return null;
            }
        }

        public MemoryItem GetItemByTagList(List<string> tagList, DateTime firstAllowedTime)
        {
            return null; // TBW
        }  */

        public MemoryItem GetLastItemByTag(string tag)
        {
            if (itemList.Count == 0) { return null; }
            else
            {
                Monitor.Enter(lockObject);
                int index = 0;
                while (index < itemList.Count)
                {
                    MemoryItem item = itemList[index];
                    if (item.TagList.Contains(tag))
                    {
                        MemoryItem accessedItem = item.Copy();
                        Monitor.Exit(lockObject);
                        return accessedItem;
                    }
                    index++;
                }
                Monitor.Exit(lockObject);
                return null;
            }
        }

        public MemoryItem GetLastItemByTagList(List<string> tagList)
        {
            if (itemList.Count == 0) { return null; }
            else
            {
                Monitor.Enter(lockObject);
                int index = 0;
                while (index < itemList.Count)
                {
                    MemoryItem item = itemList[index];
                    Boolean matching = true;
                    foreach (string tag in tagList)
                    {
                        if (!item.TagList.Contains(tag))
                        {
                            matching = false;
                            break;
                        }
                    }
                    if (matching)
                    {
                        MemoryItem accessedItem = item.Copy();
                        Monitor.Exit(lockObject);
                        return accessedItem;
                    }
                    index++;
                }
                Monitor.Exit(lockObject);
                return null; // No match found.
            }
        }

        public List<MemoryItem> GetAllItemsByTagList(List<string> tagList, TagSearchMode tagSearchMode)
        {
            Monitor.Enter(lockObject);
            List<MemoryItem> matchingItemList = new List<MemoryItem>();
            foreach (MemoryItem memoryItem in itemList)
            {
                if (tagSearchMode == TagSearchMode.Or)
                {
                    foreach (string tag in tagList)
                    {
                        if (memoryItem.TagList.Contains(tag))
                        {
                            matchingItemList.Add(memoryItem.Copy());
                            break;
                        }
                    }
                }
                else if (tagSearchMode == TagSearchMode.And)
                {
                    Boolean allTagsMatch = true;
                    foreach (string tag in tagList)
                    {
                        if (!memoryItem.TagList.Contains(tag))
                        {
                            allTagsMatch = false;
                            break;
                        }
                    }
                    if (allTagsMatch) { matchingItemList.Add(memoryItem.Copy()); }
                }
            }
            if (matchingItemList.Count == 0) { matchingItemList = null; } // Return null if nothing is found.
            Monitor.Exit(lockObject);
            return matchingItemList;
        }

        public List<MemoryItem> TryGetItems(int maximumNumberOfItems)
        {
            List<MemoryItem> acquiredItemList = new List<MemoryItem>();
            if (Monitor.TryEnter(lockObject, 100))  // ToDo: parameterize
            {
                int numberOfItemsToGet = Math.Min(maximumNumberOfItems, itemList.Count);
                for (int ii = 0; ii < numberOfItemsToGet; ii++)
                {
                    acquiredItemList.Add(itemList[ii]);
                }
                Monitor.Exit(lockObject);
            }
            return acquiredItemList;
        }

        [DataMember]
        // Note: The AddItem method (rather than itemList.Add(...)) should be used when the agent is running, 
        // since only then will the ItemInserted event fire. The setter below is to be used only in deserialization.
        public List<MemoryItem> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }
    }
}
