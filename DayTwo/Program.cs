using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DayTwo
{
    class Program
    {

        static void Main(string[] args)
        {
            String input = GetInput("DayTwo.DayTwoInput.txt");
            ReadOnlySpan<Char> inputSpan = input;
            ReadOnlySpan<Char> trimmed = inputSpan.Trim();

            List<RuleData> data = new List<RuleData>();
            Int32 currentIndex = 0;

            while (GetNextLine(in trimmed, currentIndex, out Int32 sliceIndex, out Int32 sliceLength))
            {

                ReadOnlySpan<Char> line = trimmed.Slice(sliceIndex, sliceLength);
                data.Add(new RuleData(line));

                currentIndex = sliceIndex + sliceLength + 1;
            }



            Console.WriteLine($"Part 1: Answer {data.Where(x => x.GetRule().IsValid(in x.GetPassword())).Count()}");
            Console.WriteLine($"Part 2: Answer {data.Where(x => x.GetRule().IsValid2(in x.GetPassword())).Count()}");



        }

        static bool GetNextLine(
            in ReadOnlySpan<Char> trimmedData,
            Int32 currentIndex,
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
                if(trimmedData[i] == '\n')
                {
                    break;
                }
            }
            sliceLength = i - sliceIndex;
            return true;
        }




        static String GetInput(String resourceName)
        {
            String result = null;
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
                result = reader.ReadToEnd();

            return result;

        }


    }
}
