using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuxiliaryLibrary
{
    public class BoxEventArgs: EventArgs
    {
        private int left;
        private int right;
        private int top;
        private int bottom;

        public BoxEventArgs(int left, int right, int top, int bottom)
        {
            this.left = left;
            this.right = right;
            this.top = top;
            this.bottom = bottom;
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
    }
}
