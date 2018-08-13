using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionApplication
{
    public class FaceDetectionEventArgs: EventArgs
    {
        private int left;
        private int right;
        private int top;
        private int bottom;
        private Boolean lockAcquired;

        public FaceDetectionEventArgs(int left, int right, int top, int bottom, Boolean lockAcquired)
        {
            this.left = left;
            this.right = right;
            this.top = top;
            this.bottom = bottom;
            this.lockAcquired = lockAcquired;
        }

        public int Left
        {
            get { return left; }
            set { left = value; }
        }

        public int Right
        {
            get { return right; }
            set { right = value; }
        }

        public int Top
        {
            get { return top; }
            set { top = value; }
        }

        public int Bottom
        {
            get { return bottom; }
            set { bottom = value; }
        }

        public Boolean LockAcquired
        {
            get { return lockAcquired; }
            set { lockAcquired = value; }
        }
    }
}
