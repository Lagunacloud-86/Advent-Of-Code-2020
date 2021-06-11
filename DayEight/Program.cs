
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using ResourceReader;

namespace DayEight
{
    class Program
    {

        static async Task Main(string[] args)
        {

            var document = await ResourceDocument.DocumentFromEmbeddedResource(
                assembly: Assembly.GetExecutingAssembly(), "DayEight.InputDayEight.txt", null, default);

            String test = @"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6
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
            Computer computer = new Computer();
            List<ROM> romList = new List<ROM>();
            ROM[] rom;
            ReadOnlySpan<Char> testSpan = document;
            Int32 index = 0;
            while (NextGroup(
                testSpan,
                ref index,
                out Int32 sliceIndex, out Int32 sliceLength))
            {
                ReadOnlySpan<Char> node = testSpan.Slice(sliceIndex, sliceLength);
                romList.Add(new ROM(in node));
            }

            rom = romList.ToArray();
            index = 0;
            Int32 accumulator = 0;
            Dictionary<Int32, Int32> _instructionIndexAccumulator = new Dictionary<int, int>();
            do {
                if (!_instructionIndexAccumulator.ContainsKey(index))
                {
                    _instructionIndexAccumulator.Add(index, computer.Accumulator);
                }
                else
                {
                    accumulator = computer.Accumulator;
                    break;
                }

                index += computer.RunInstruction(
                    rom[index].Instruction,
                    rom[index].Parameter);

                

            } while (index >= 0 && index <= rom.Length);




            return accumulator == 5;
        }
        

        static void PartOne(in ResourceDocument document)
        {
            Computer computer = new Computer();
            List<ROM> romList = new List<ROM>();
            ROM[] rom;
            ReadOnlySpan<Char> testSpan = document.ToCharArray();
            Int32 index = 0;
            while (NextGroup(
                testSpan,
                ref index,
                out Int32 sliceIndex, out Int32 sliceLength))
            {
                ReadOnlySpan<Char> node = testSpan.Slice(sliceIndex, sliceLength);
                romList.Add(new ROM(in node));
            }

            rom = romList.ToArray();
            index = 0;
            Int32 accumulator = 0;
            Dictionary<Int32, Int32> _instructionIndexAccumulator = new Dictionary<int, int>();
            do
            {
                if (!_instructionIndexAccumulator.ContainsKey(index))
                {
                    _instructionIndexAccumulator.Add(index, computer.Accumulator);
                }
                else
                {
                    accumulator = computer.Accumulator;
                    break;
                }

                index += computer.RunInstruction(
                    rom[index].Instruction,
                    rom[index].Parameter);



            } while (index >= 0 && index <= rom.Length);


            Console.WriteLine($"Part One: {accumulator}");

        }

        static void PartTwo(in ResourceDocument document)
        {
            Computer computer = new Computer();
            List<ROM> romList = new List<ROM>();
            ReadOnlySpan<Char> testSpan = document.ToCharArray();
            Int32 index = 0;
            while (NextGroup(
                testSpan,
                ref index,
                out Int32 sliceIndex, out Int32 sliceLength))
            {
                ReadOnlySpan<Char> node = testSpan.Slice(sliceIndex, sliceLength);
                romList.Add(new ROM(in node));
            }


            Boolean didEndWell;
            Int32 changeIndex = 0;
            Int32 accumulator;
            Dictionary<Int32, Int32> _instructionIndexAccumulator = new Dictionary<int, int>();
            do {
                ROM[] rom = romList.ToArray();
                index = 0;
                accumulator = 0;
                didEndWell = true;
                _instructionIndexAccumulator.Clear();

                for (; changeIndex < rom.Length; ++changeIndex)
                {
                    if(rom[changeIndex].Instruction == "jmp")
                    {
                        rom[changeIndex] = new ROM("nop", rom[changeIndex].Parameter);
                        changeIndex++;
                        break;
                    }
                }


                do
                {
                    if (!_instructionIndexAccumulator.ContainsKey(index))
                    {
                        _instructionIndexAccumulator.Add(index, computer.Accumulator);
                    }
                    else
                    {
                        accumulator = computer.Accumulator;
                        didEndWell = false;
                        break;
                    }

                    accumulator = computer.Accumulator;

                    index += computer.RunInstruction(
                        rom[index].Instruction,
                        rom[index].Parameter);




                } while (index >= 0 && index < rom.Length);



            } while (!didEndWell);


            Console.WriteLine($"Part Two: {accumulator}");

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
