using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using ResourceReader;

namespace DaySix
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var document = await ResourceDocument.DocumentFromEmbeddedResource(
                assembly: Assembly.GetExecutingAssembly(), "DaySix.DaySixInput.txt", null, default);

            String test = @"abc

a
b
c

ab
ac

a
a
a
a

b
";
            if (!TestOne(test.Replace('\r', ' ').Replace(" ", "")))
            {
                Console.WriteLine("Test one failed...");
                return;
            }
            if (!TestTwo(test.Replace('\r', ' ').Replace(" ", "")))
            {
                Console.WriteLine("Test two failed...");
                return;
            }

            PartOne(in document);
            PartTwo(in document);
        }
        static Boolean TestOne(String document)
        {
            List<Group> groups = new List<Group>();
            ReadOnlySpan<Char> testSpan = document;
            Int32 index = 0;
            while(NextGroup(
                testSpan, 
                ref index,
                out Int32 sliceIndex, out Int32 sliceLength))
            {
                ReadOnlySpan<Char> group = testSpan.Slice(sliceIndex, sliceLength);

                groups.Add(new Group(group));

            }


            Int32 result = groups.Sum(g => g.UserYes.Count);
            return result == 11;
        }
        static Boolean TestTwo(String document)
        {
            List<Group> groups = new List<Group>();
            ReadOnlySpan<Char> testSpan = document;
            Int32 index = 0;
            while (NextGroup(
                testSpan,
                ref index,
                out Int32 sliceIndex, out Int32 sliceLength))
            {
                ReadOnlySpan<Char> group = testSpan.Slice(sliceIndex, sliceLength);

                groups.Add(new Group(group));

            }


            Int32 result = groups.Sum(g => g.UserYes.Where(x => x.Value == g.GroupSize).Count());
            return result == 6;
        }


        static void PartOne(in ResourceDocument document)
        {
            ReadOnlySpan<Char> readonlyDocument = document.ToCharArray();

            List<Group> groups = new List<Group>();
            Int32 index = 0;
            while (NextGroup(
                readonlyDocument,
                ref index,
                out Int32 sliceIndex, out Int32 sliceLength))
            {
                ReadOnlySpan<Char> group = readonlyDocument.Slice(sliceIndex, sliceLength);

                groups.Add(new Group(group));

            }

            Console.WriteLine($"Part One: {groups.Sum(g => g.UserYes.Count)}");


        }
        static void PartTwo(in ResourceDocument document)
        {
            ReadOnlySpan<Char> readonlyDocument = document.ToCharArray();

            List<Group> groups = new List<Group>();
            Int32 index = 0;
            while (NextGroup(
                readonlyDocument,
                ref index,
                out Int32 sliceIndex, out Int32 sliceLength))
            {
                ReadOnlySpan<Char> group = readonlyDocument.Slice(sliceIndex, sliceLength);

                groups.Add(new Group(group));

            }

            Console.WriteLine($"Part Two: {groups.Sum(g => g.UserYes.Where(x => x.Value == g.GroupSize).Count())}");

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
                if(document[index] == '\n' && document[index + 1] == '\n')
                {
                    break;
                }
            }
            sliceLength = index - sliceIndex;
            index = index + 2;


            return true;
        }
    }
}
