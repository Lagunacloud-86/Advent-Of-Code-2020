using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DayOne
{
    class Program
    {
        struct ItemPointers
        {
            public Int32 Index1 { get; set; }
            public Int32 Value1 { get; set; }

            public Int32 Index2 { get; set; }
            public Int32 Value2 { get; set; }

            public Int32 Index3 { get; set; }
            public Int32 Value3 { get; set; }
        }

        static void Main(string[] args)
        {

            String[] items = GetInputData();
            Console.WriteLine($"Part One Answer: {CalculatePartOne(items)}");
            Console.WriteLine($"Part Two Answer: {CalculatePartTwo(items)}");

        }


        static Int32 CalculatePartOne(String[] items)
        {
            List<ItemPointers> itemPointers = new List<ItemPointers>();

            for (Int32 i = 0; i < items.Length - 1; ++i)
            {
                if (!Int32.TryParse(items[i], out Int32 value1))
                    continue;

                for (Int32 j = i + 1; j < items.Length; ++j)
                {
                    if (!Int32.TryParse(items[j], out Int32 value2))
                        continue;

                    if (value1 + value2 == 2020)
                        itemPointers.Add(new ItemPointers
                        {
                            Index1 = i,
                            Value1 = value1,
                            Index2 = j,
                            Value2 = value2
                        });


                }
            }

            Console.WriteLine($"Pointer counts: {itemPointers.Count}");
            foreach (ItemPointers pointer in itemPointers)
            {
                return pointer.Value1 * pointer.Value2;
            }

            return 0;
        }

        static Int32 CalculatePartTwo(String[] items)
        {
            List<ItemPointers> itemPointers = new List<ItemPointers>();

            for (Int32 i = 0; i < items.Length - 2; ++i)
            {
                if (!Int32.TryParse(items[i], out Int32 value1))
                    continue;

                for (Int32 j = i + 1; j < items.Length - 1; ++j)
                {
                    if (!Int32.TryParse(items[j], out Int32 value2))
                        continue;

                    for(Int32 k = j + 1; k < items.Length; ++k)
                    {

                        if (!Int32.TryParse(items[k], out Int32 value3))
                            continue;

                        if (value1 + value2 + value3 == 2020)
                            itemPointers.Add(new ItemPointers
                            {
                                Index1 = i,
                                Value1 = value1,
                                Index2 = j,
                                Value2 = value2,
                                Index3 = k,
                                Value3 = value3
                            });
                    }

                   


                }
            }

            Console.WriteLine($"Pointer counts: {itemPointers.Count}");
            foreach (ItemPointers pointer in itemPointers)
            {
                return pointer.Value1 * pointer.Value2 * pointer.Value3;
            }

            return 0;
        }


        static String[] GetInputData()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "DayOne.DayOneInput.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return result.Split("\n", StringSplitOptions.None);
            }

        }
    }
}
