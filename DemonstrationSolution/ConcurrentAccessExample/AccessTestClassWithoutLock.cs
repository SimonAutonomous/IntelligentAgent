using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConcurrentAccessExample
{
    public class AccessTestClassWithoutLock
    {
        private List<int> integerList = null;

        public AccessTestClassWithoutLock()
        {
            integerList = new List<int>() { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }

        public void AddElement()
        {
            integerList.Add(1);
            integerList.RemoveAt(0);
        }

        public int GetCheckSum()
        {
            return integerList.Count;
        }
    }
}
