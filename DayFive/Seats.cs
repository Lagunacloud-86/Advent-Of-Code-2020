using System;
using System.Collections.Generic;
using System.Text;

namespace DayFive
{
    public static class Seats
    {
        public static void GetSeatIndex(
            in ReadOnlySpan<Char> seatNode,
            out Int32 row, out Int32 col)
        {
            row = 0;
            col = 0;

            Int32 start = 0, end = 127;
            Int32 range = 128;
            for (Int32 i = 0; i < 7; ++i)
            {
                range = range / 2;


                if (seatNode[i] == 'F')
                    end = end - range;
                else if (seatNode[i] == 'B')
                    start = start + range;
            }

            row = start;

            start = 0; end = 7;
            range = 8;
            for (Int32 i = 0; i < 3; ++i)
            {
                range = range / 2;


                if (seatNode[i + 7] == 'L')
                    end = end - range;
                else if (seatNode[i + 7] == 'R')
                    start = start + range;
            }
            col = start;



            //Byte start = 1; Byte end = 128;
            //for (Int32 i = 0; i < 6; ++i)
            //{
            //    //Byte result = (Byte)((Decimal)(end - (end - start) * 0.5m));
            //    //if (seatNode[i] == 'F')
            //    //    end = result;
            //    //else if (seatNode[i] == 'B')
            //    //    start = result;

            //}


        }

    }
}
