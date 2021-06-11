
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using ResourceReader;

namespace DaySeven
{
    class Program
    {

        private class BagStackItem
        {
            public String BagType { get; set; }

            public List<BagStackItem> Children { get; set; }

            public BagStackItem Parent { get; set; }

            public BagStackItem Root => Parent == null ? this : Parent.Root;

            public Boolean HasChildren => (Children?.Count ?? 0) > 0;
           
        }

        static async Task Main(string[] args)
        {

            var document = await ResourceDocument.DocumentFromEmbeddedResource(
                assembly: Assembly.GetExecutingAssembly(), "DaySeven.DaySevenInput.txt", null, default);

            String test = @"light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.
";
            
            if (!TestOne(test.Replace('\r', '_').Replace("_", "")))
            {
                Console.WriteLine("Test one failed...");
                return;
            }
            if (!TestTwo(test.Replace('\r', '_').Replace("_", "")))
            {
                Console.WriteLine("Test two failed...");
                return;
            }
        }
        static Boolean TestOne(String document)
        {
            List<Bag> bags = new List<Bag>();
            ReadOnlySpan<Char> testSpan = document;
            Int32 index = 0;
            while (NextGroup(
                testSpan,
                ref index,
                out Int32 sliceIndex, out Int32 sliceLength))
            {
                ReadOnlySpan<Char> node = testSpan.Slice(sliceIndex, sliceLength);
                bags.Add(new Bag(node));
            }


            Int32 result = GetBagDepth(in bags, "shiny gold", 1);
                
           
            

            //Int32 result = 4;
            return result == 4;
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


        static Int32 GetBagDepth(in List<Bag> bags, String bagType, Int32 depth)
        {
            Int32 bagCount = 0, currentDepth = 0;

            Stack<BagStackItem> bagStack = new Stack<BagStackItem>();
            BagStackItem bagStackItem = new BagStackItem
            {
                BagType = bagType
            };
            bagStack.Push(bagStackItem);


            while(bagStack.Count > 0)
            {
                BagStackItem currentItem = bagStack.Pop();
                Bag currentBag = bags.First(x => x.BagType == currentItem.BagType);

                if (currentDepth < depth)
                {
                    currentDepth++;
                    currentItem.Children = new List<BagStackItem>();
                    foreach (var subBag in currentBag.SubBags)
                    {
                        BagStackItem child = new BagStackItem
                        {
                            BagType = subBag.Key
                        };
                        bagStack.Push(child);
                        currentItem.Children.Add(child);
                    }
                }

            }

            //for (Int32 i = 0; i < depth; ++i)
            //{

            //    Bag currentBag = bags.First(x => x.BagType == current.BagType);

            //    current.Children = new List<BagStackItem>();
            //    foreach(var subBag in currentBag.SubBags)
            //    {
            //        current.Children.Add(new BagStackItem
            //        {
            //            BagType = subBag.Key
            //        });
            //    }


            //}

            //bagStack.Push(new BagStackItem(0, bagType));
            //while(bagStack.Count > 0)
            //{
            //    BagStackItem currentBagItem = bagStack.Pop();

            //    Bag currentBag = bags.FirstOrDefault(x => x.BagType == currentBagItem.BagType);

            //    bagCount += bags.Count(x => x.SubBags.Any(sb => sb.Key == currentBag.BagType));

            //    if (currentDepth < depth)
            //    {
            //        currentDepth++;
            //        foreach (var subBag in currentBag.SubBags)
            //        {
            //            bagStack.Push(new BagStackItem(in currentDepth, subBag.Key));
            //        }
            //    }
            //}
            //foreach(Bag bag in bags.Where(x => x.SubBags.Any(sb => sb.Key == bagType)))
            //{
            //    bagStack.Push(bag);
            //}
            return bagCount;
            
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
