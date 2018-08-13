using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConcurrentAccessExample
{
    public class AccessTestClassWithLock
    {
        private List<int> integerList = null;
        private static object accessLockObject = new object();

        public AccessTestClassWithLock()
        {
            integerList = new List<int>() { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }

        public void AddElement()
        {
                Monitor.Enter(accessLockObject);
                integerList.Add(1);
                integerList.RemoveAt(0); 
                Monitor.Exit(accessLockObject);
        }

        public int GetCheckSum()
        {
            int checkSum = 0;
            Monitor.Enter(accessLockObject);
            checkSum = integerList.Count;
            Monitor.Exit(accessLockObject);
            return checkSum;
        }
    }
}
