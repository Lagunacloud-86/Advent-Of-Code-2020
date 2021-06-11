using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySeventeen
{
    public readonly struct Point : IEquatable<Point>
    {

        public Int64 X { get; }
        public Int64 Y { get; }
        public Int64 Z { get; }
        public Int64 W { get; }

        public Int64 this[Int32 index]
        {
            get
            {
                switch (index)
                {
                    case 0: return X;
                    case 1: return Y;
                    case 2: return Z;
                    case 3: return W;
                }
                return 0;
            }
        }

        public Point(in Int64 x, in Int64 y)
        {
            X = x;
            Y = y;
            Z = 0;
            W = 0;
        }

        public Point(in Int64 x, in Int64 y, in Int64 z)
        {
            X = x;
            Y = y;
            Z = z;
            W = 0;
        }

        public Point(in Int64 x, in Int64 y, in Int64 z, in Int64 w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }


        public static Point Add(in Point A, in Point B)
        {
            return new Point(
                A.X + B.X,
                A.Y + B.Y,
                A.Z + B.Z,
                A.W + B.W);
        }

        public static Boolean IsNeighbor(in Point A, in Point B)
        {
            return
                Math.Abs(B.X - A.X) <= 1 &&
                Math.Abs(B.Y - A.Y) <= 1 &&
                Math.Abs(B.Z - A.Z) <= 1 &&
                Math.Abs(B.W - A.W) <= 1;
        }

        public bool Equals(Point other)
        {
            return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
        }
    }
}
