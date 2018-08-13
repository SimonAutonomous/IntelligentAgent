using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcessingLibrary
{
    // OBSOLETE
    public class BoundingBox
    {
        public int Left;
        public int Top;
        public int Right;  
        public int Bottom;  // Note: Bottom >= Top.

        public BoundingBox(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public int GetWidth()
        {
            return (Right - Left + 1);
        }

        public int GetHeight()
        {
            return (Bottom - Top + 1);
        }

        public string AsString()
        {
            return Left.ToString() + " " + Top.ToString() + " " + Right.ToString() + " " + Bottom.ToString();
        }
    }
}
