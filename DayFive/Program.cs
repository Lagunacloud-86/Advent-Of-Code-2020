
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

using ResourceReader;


namespace DayFive
{
    class Program
    {

        static async Task Main(string[] args)
        {

            var document = await ResourceDocument.DocumentFromEmbeddedResource(
                assembly: Assembly.GetExecutingAssembly(), "DayFive.DayFiveInput.txt", new SeatIterator(), default);

            //Test("FBFBBFFRLR");
            //Test("BFFFBBFRRR");
            //Test("FFFBBBFRRR");
            //Test("BBFFBBFRLL");
            PartOne(in document);

            document.NodeIterator.Reset();
            PartTwo(in document);


        }
        static void Test(String test)
        {
            ReadOnlySpan<Char> seat = test;

            Seats.GetSeatIndex(
                    in seat,
                    out Int32 row, out Int32 col);

            Console.WriteLine($"Seat Id: {row * 8 + col}, Seat Row: {row}, Seat Column: {col}");


        }
        static void PartOne(in ResourceDocument document)
        {
            ReadOnlySpan<Char> readonlyDocument = document.ToCharArray();
            ReadOnlySpan<Char> trimmedDocument = readonlyDocument.Trim();

            Int32 highestId = Int32.MinValue;
            while (document.NodeIterator.NextNode(
                in trimmedDocument,
                out Int32 sliceIndex, out Int32 sliceLength))
            {
                ReadOnlySpan<Char> seat = trimmedDocument.Slice(sliceIndex, sliceLength);

                Seats.GetSeatIndex(
                    in seat,
                    out Int32 row, out Int32 col);

                Int32 id = row * 8 + col;
                if (highestId < id)
                    highestId = id;

            }


            Console.WriteLine($"PartOne: Highest {highestId}");


        }
        static void PartTwo(in ResourceDocument document)
        {
            ReadOnlySpan<Char> readonlyDocument = document.ToCharArray();
            ReadOnlySpan<Char> trimmedDocument = readonlyDocument.Trim();

            Dictionary<Int32, SeatInfo> seatData = new Dictionary<int, SeatInfo>();
            while (document.NodeIterator.NextNode(
                in trimmedDocument,
                out Int32 sliceIndex, out Int32 sliceLength))
            {
                ReadOnlySpan<Char> seat = trimmedDocument.Slice(sliceIndex, sliceLength);

                Seats.GetSeatIndex(
                    in seat,
                    out Int32 row, out Int32 col);

                
                SeatInfo seatInfo = new SeatInfo(in row, in col);

                seatData.Add(seatInfo.Id, seatInfo);

            }

            List<Int32> sortedList = seatData
                .OrderBy(o => o.Key)
                .Select(x => x.Key)
                .ToList();

            for(int i = 0; i < sortedList.Count - 1; ++i)
            {
                Int32 diff = sortedList[i + 1] - sortedList[i];
                if(diff > 1)
                {
                    Console.WriteLine($"Part Two: Difference found! id1:{sortedList[i]}, id2: {sortedList[i + 1]}");
                }
            }



        }

    }
}
