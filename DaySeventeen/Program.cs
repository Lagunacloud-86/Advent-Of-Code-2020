using ResourceReader;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace DaySeventeen
{
    class Program
    {
        static async Task Main(string[] args)
        {

            //var document = await ResourceDocument.DocumentFromEmbeddedResource(
            //    assembly: Assembly.GetExecutingAssembly(), "DaySeven.DaySeventeen.txt", null, default);

            String test = @".#.
..#
###";


            if (!TestOne(test.Replace('\r', '_').Replace("_", "")))
            {
                Console.WriteLine("Test one failed...");
                return;
            }
            //if (!TestTwo(test.Replace('\r', '_').Replace("_", "")))
            //{
            //    Console.WriteLine("Test two failed...");
            //    return;
            //}
        }
        static Boolean TestOne(String document)
        {
            ReadOnlySpan<Char> documentSpan = document;
            NTree nTree = new NTree();
            Int32 x = 0, y = 0, z = 0;
            for (Int32 i = 0; i < documentSpan.Length; ++i)
            {
                if (documentSpan[i] == '#')
                {
                    nTree.AddNode(new ConwayCubeInfo(x, y, z, 0, true));
                }
                if (documentSpan[i] == '.')
                {
                    nTree.AddNode(new ConwayCubeInfo(x, y, z, 0, false));
                }

                x++;

                if (documentSpan[i] == '\n')
                {
                    y++;
                    x = 0;
                }
            }
            nTree.BuildNeighbors();
            for (Int32 i = 0; i < 6; ++i)
                nTree.Iterate(depth: 3);
          

            //Int32 result = 4;
            return nTree.ActiveNodes == 112;
        }
        static Boolean TestTwo(String document)
        {
            //ReadOnlySpan<Char> testSpan = document;
            //Int32 index = 0;
            //while (NextGroup(
            //    testSpan,
            //    ref index,
            //    out Int32 sliceIndex, out Int32 sliceLength))
            //{
            //    ReadOnlySpan<Char> group = testSpan.Slice(sliceIndex, sliceLength);


            //}


            return false;
        }


    }
}
