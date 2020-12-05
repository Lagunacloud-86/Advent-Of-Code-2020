using System;
using System.Collections.Generic;
using System.Text;

namespace DayFive
{
    public class SeatInfo
    {
        public Int32 Id { get; }

        public Int32 Row { get; }

        public Int32 Column { get; }


        public SeatInfo(
            in Int32 row,
            in Int32 column)
        {
            Row = row;
            Column = column;
            Id = row * 8 + column;
        }

    }
}
