using System;
using System.Collections.Generic;
using System.Text;

namespace DayThree
{
    public readonly struct TreeFieldInfo
    {
        public Int32 Width { get; }

        public Int32 CharWidth { get; }

        public Int32 Height { get; }


        public TreeFieldInfo(in Int32 width, in Int32 height, in Int32 charWidth)
        {
            Width = width;
            CharWidth = charWidth;
            Height = height;
        }

        public Boolean CheckSpot(in ReadOnlySpan<Char> data, in Int32 x, in Int32 y, in Char character)
        {
            Int32 realX = x % Width;
            return data[realX + y * CharWidth] == character;
        }
    }
}
