
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using ResourceReader;

namespace DayEleven
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var document = await ResourceDocument.DocumentFromEmbeddedResource(
                assembly: Assembly.GetExecutingAssembly(), "DayTen.InputDayTen.txt", null, default);

            String test = @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL
";

            if (!TestOne(test.Replace('\r', '_').Replace("_", "")))
            {
                Console.WriteLine("Test one failed...");
                return;
            }

            PartOne(in document);
            PartTwo(in document);
        }
        static Boolean TestOne(String document)
        {
            List<UInt64> dataSet = new List<UInt64>();

            ReadOnlySpan<Char> testSpan = document.ToCharArray();
            Int32 index = 0;
            while (NextGroup(
                testSpan,
                ref index,
                out Int32 sliceIndex, out Int32 sliceLength))
            {
                ReadOnlySpan<Char> node = testSpan.Slice(sliceIndex, sliceLength);

                dataSet.Add(UInt64.Parse(node.ToString()));
            }

            UInt64 currentJoltage = 0;
            UInt64 oneJoltDiff = 0;
            UInt64 threeJoltDiff = 0;

            UInt64[] sortedDataSet = dataSet
                .OrderBy(x => x)
                .ToArray();

            UInt64 finalJoltage = sortedDataSet[sortedDataSet.Length - 1] + 3;
            for (index = 0; index < sortedDataSet.Length; ++index)
            {
                if (currentJoltage + 1 == sortedDataSet[index])
                {
                    oneJoltDiff++;
                    currentJoltage = currentJoltage + 1;
                }

                if (currentJoltage + 3 == sortedDataSet[index])
                {
                    threeJoltDiff++;
                    currentJoltage = currentJoltage + 3;
                }
            }
            threeJoltDiff++;



            return (oneJoltDiff * threeJoltDiff) == 220;
        }


        static void PartOne(in ResourceDocument document)
        {
            List<UInt64> dataSet = new List<UInt64>();

            ReadOnlySpan<Char> testSpan = document.ToCharArray();
            Int32 index = 0;
            while (NextGroup(
                testSpan,
                ref index,
                out Int32 sliceIndex, out Int32 sliceLength))
            {
                ReadOnlySpan<Char> node = testSpan.Slice(sliceIndex, sliceLength);

                dataSet.Add(UInt64.Parse(node.ToString()));
            }

            UInt64 currentJoltage = 0;
            UInt64 oneJoltDiff = 0;
            UInt64 threeJoltDiff = 0;

            UInt64[] sortedDataSet = dataSet
                .OrderBy(x => x)
                .ToArray();

            UInt64 finalJoltage = sortedDataSet[sortedDataSet.Length - 1] + 3;
            for (index = 0; index < sortedDataSet.Length; ++index)
            {
                if (currentJoltage + 1 == sortedDataSet[index])
                {
                    oneJoltDiff++;
                    currentJoltage = currentJoltage + 1;
                }

                if (currentJoltage + 3 == sortedDataSet[index])
                {
                    threeJoltDiff++;
                    currentJoltage = currentJoltage + 3;
                }
            }
            threeJoltDiff++;
            Console.WriteLine($"Part One: {(oneJoltDiff * threeJoltDiff)}");

        }

        static void PartTwo(in ResourceDocument document)
        {


        }


        static Boolean NextGroup(
            in ReadOnlySpan<Char> document,
            ref Int32 index,
            out Int32 sliceIndex, out Int32 sliceLength)
        {
            sliceIndex = index;
            sliceLength = 0;

            if (index >= document.Length)
                return false;

            for (; index < document.Length - 1; index++)
            {
                if (document[index] == '\n')
                {

                    break;
                }
            }
            sliceLength = index - sliceIndex;
            index = index + 1;


            return true;
        }
    }
}
