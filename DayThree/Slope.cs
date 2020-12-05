using System;
using System.Collections.Generic;
using System.Text;

namespace DayThree
{
    public readonly struct Slope
    {
        public Int32 X { get; }

        public Int32 Y { get; }


        public Slope(in Int32 x, in Int32 y)
        { 
            X = x;
            Y = y;
        }
    }
}
