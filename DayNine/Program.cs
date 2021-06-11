
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using ResourceReader;

namespace DayNine
{
    class Program
    {

        static async Task Main(string[] args)
        {

            var document = await ResourceDocument.DocumentFromEmbeddedResource(
                assembly: Assembly.GetExecutingAssembly(), "DayNine.InputDayNine.txt", null, default);

            String test = @"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576
";

            if (!TestOne(test.Replace('\r', '_').Replace("_", ""), 5))
            {
                Console.WriteLine("Test one failed...");
                return;
            }

            PartOne(in document, 25);
            PartTwo(in document);
        }
        static Boolean TestOne(String document, Int32 preamble)
        {
            Int32 resultIndex = 0;
            List<Int32> preambleValues = new List<Int32>();
            List<Int32> data = new List<int>();

            ReadOnlySpan<Char> testSpan = document;
            Int32 index = 0;
            while (NextGroup(
                testSpan,
                ref index,
                out Int32 sliceIndex, out Int32 sliceLength))
            {
                ReadOnlySpan<Char> node = testSpan.Slice(sliceIndex, sliceLength);

                if (resultIndex < preamble)
                    preambleValues.Add(Int32.Parse(node.ToString()));
                else
                    data.Add(Int32.Parse(node.ToString()));
                resultIndex++;
            }
           
            Int32 result = 0;

            for(Int32 i = 0; i < data.Count; ++i)
            {
                result = data[i];

                Boolean additionFound = false;
                for (Int32 x = 0; x < preambleValues.Count - 1; ++x)
                {
                    for (Int32 y = x + 1; y < preambleValues.Count; ++y)
                    {
                        if (preambleValues[x] + preambleValues[y] == result)
                        {
                            additionFound = true;
                            preambleValues.RemoveAt(0);
                            preambleValues.Add(data[i]);
                            break;
                        }
                    }
                    if (additionFound)
                        break;

                }
                if (!additionFound)
                    break;

            }

            return result == 127;
        }


        static void PartOne(in ResourceDocument document, Int32 preamble)
        {
            Int32 resultIndex = 0;
            List<UInt64> preambleValues = new List<UInt64>();
            List<UInt64> data = new List<UInt64>();

            ReadOnlySpan<Char> testSpan = document.ToCharArray();
            Int32 index = 0;
            while (NextGroup(
                testSpan,
                ref index,
                out Int32 sliceIndex, out Int32 sliceLength))
            {
                ReadOnlySpan<Char> node = testSpan.Slice(sliceIndex, sliceLength);

                if (resultIndex < preamble)
                    preambleValues.Add(UInt64.Parse(node.ToString()));
                else
                    data.Add(UInt64.Parse(node.ToString()));
                resultIndex++;
            }

            UInt64 result = 0;

            for (Int32 i = 0; i < data.Count; ++i)
            {
                result = data[i];

                Boolean additionFound = false;
                for (Int32 x = 0; x < preambleValues.Count - 1; ++x)
                {
                    for (Int32 y = x + 1; y < preambleValues.Count; ++y)
                    {
                        if (preambleValues[x] + preambleValues[y] == result)
                        {
                            additionFound = true;
                            preambleValues.RemoveAt(0);
                            preambleValues.Add(data[i]);
                            break;
                        }
                    }
                    if (additionFound)
                        break;

                }
                if (!additionFound)
                    break;

            }


            Console.WriteLine($"Part One: {result}");

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
