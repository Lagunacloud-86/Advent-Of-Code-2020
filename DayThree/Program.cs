using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

using ResourceReader;

namespace DayThree
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            var document = await ResourceDocument
                .DocumentFromEmbeddedResource(Assembly.GetExecutingAssembly(), "DayThree.DayThreeInput.txt");

            PartOne(in document);
            PartTwo(in document);
        }

        private static void PartOne(in ResourceDocument document)
        {
            ReadOnlySpan<Char> documentSpan =
                new ReadOnlySpan<char>(document.ToCharArray());
            ReadOnlySpan<Char> trimmed = documentSpan.Trim();
            Int32 index = 0;
            Int32 height = 0, width = 0;
            while (GetNextLine(in trimmed, in index, out Int32 sliceIndex, out Int32 sliceLength))
            {
                ReadOnlySpan<Char> line = trimmed.Slice(sliceIndex, sliceLength);
                width = line.Length;
                index = sliceIndex + sliceLength + 1;
                height++;
            }

            TreeFieldInfo treeFieldInfo =
                new TreeFieldInfo(in width, in height, width + 1);
            Int32 x = 0, hitTree = 0;
            for(Int32 y = 0; y < treeFieldInfo.Height; ++y)
            {
                hitTree += treeFieldInfo.CheckSpot(in trimmed, in x, in y, '#') ? 1 : 0;
                x += 3;
            }
            Console.WriteLine($"Part 1 Result: {hitTree}");

        }
        private static void PartTwo(in ResourceDocument document)
        {
            ReadOnlySpan<Char> documentSpan =
                new ReadOnlySpan<char>(document.ToCharArray());
            ReadOnlySpan<Char> trimmed = documentSpan.Trim();
            Int32 index = 0;
            Int32 height = 0, width = 0;
            while (GetNextLine(in trimmed, in index, out Int32 sliceIndex, out Int32 sliceLength))
            {
                ReadOnlySpan<Char> line = trimmed.Slice(sliceIndex, sliceLength);
                width = line.Length;
                index = sliceIndex + sliceLength + 1;
                height++;
            }

            TreeFieldInfo treeFieldInfo =
                new TreeFieldInfo(in width, in height, width + 1);

            Slope[] slopes = new Slope[]
            {
                new Slope(1, 1),
                new Slope(3, 1),
                new Slope(5, 1),
                new Slope(7, 1),
                new Slope(1, 2)
            };
            //Int32[] results = new int[5];
            UInt64 finalResult = 1;

            for(Int32 i = 0; i < 5; ++i)
            {
                Int32 x = 0, hitTree = 0;
                for (Int32 y = 0; y < treeFieldInfo.Height; y += slopes[i].Y)
                {
                    hitTree += treeFieldInfo.CheckSpot(in trimmed, in x, in y, '#') ? 1 : 0;
                    x += slopes[i].X;
                }
                finalResult *= (UInt64)hitTree;
                //results[i] = hitTree;
            }
            
            Console.WriteLine($"Part 2 Result: {finalResult}");

        }


        static bool GetNextLine(
            in ReadOnlySpan<Char> trimmedData,
            in Int32 currentIndex,
            out Int32 sliceIndex, out Int32 sliceLength)
        {
            sliceIndex = 0;
            sliceLength = 0;
            if (currentIndex >= trimmedData.Length)
                return false;


            sliceIndex = currentIndex;
            Int32 i;
            for (i = currentIndex; i < trimmedData.Length; ++i)
            {
                if (trimmedData[i] == '\n')
                {
                    break;
                }
            }
            sliceLength = i - sliceIndex;
            return true;
        }


    }
}
