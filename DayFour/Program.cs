using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

using ResourceReader;

namespace DayFour
{
    class Program
    {


        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            var document = await ResourceDocument.DocumentFromEmbeddedResource(
                assembly: Assembly.GetExecutingAssembly(), "DayFour.DayFourInput.txt", null, default);

            PartOne(in document.ToCharArray());
            PartTwo(in document.ToCharArray());

        }

        static void PartOne(in Char[] document)
        {
            String[] requiredFields = new string[]
            {
                "ecl", "pid", "eyr", "hcl", "byr", "iyr", "hgt"
            };

            ReadOnlySpan<Char> span = document;
            Int32 index = 0;
            Int32 validPassports = 0;
            while (NextPassport(
                in span,
                in index,
                out Int32 sliceIndex, out Int32 sliceLength))
            {
                ReadOnlySpan<Char> passportData = span.Slice(sliceIndex, sliceLength).Trim();
                Boolean hasFields = true;

                foreach (String field in requiredFields)
                    hasFields &= Passport.HasField(in passportData, field);

                validPassports += hasFields ? 1 : 0;

                index += sliceLength + 1;
            }


            Console.WriteLine($"Part One: Valid Passports {validPassports}");


        }
        static void PartTwo(in Char[] document)
        {
            Dictionary<String, RequirementBase> requirements = new Dictionary<string, RequirementBase>
            {
                { "ecl", new ECLRequirements() },
                { "pid", new PIDRequirements() },
                { "eyr", new EYRRequirements() },
                { "hcl", new HCLRequirements() },
                { "byr", new BYRRequirements() },
                { "iyr", new IYRRequirements() },
                { "hgt", new HGTRequirements() }
            };

            ReadOnlySpan<Char> span = document;
            Int32 index = 0;
            Int32 validPassports = 0;
            while (NextPassport(
                in span,
                in index,
                out Int32 sliceIndex, out Int32 sliceLength))
            {
                ReadOnlySpan<Char> passportData = span.Slice(sliceIndex, sliceLength).Trim();
                Boolean hasFields = true;

                foreach (var req in requirements)
                {
                    hasFields &= Passport.HasField(in passportData, req.Key);
                    hasFields &= req.Value.IsValid(in passportData);
                }

                validPassports += hasFields ? 1 : 0;



                index += sliceLength + 1;
            }


            Console.WriteLine($"Part Two: Valid Passports {validPassports}");
        }


        static Boolean NextPassport(
            in ReadOnlySpan<Char> document,
            in Int32 index,
            out Int32 sliceIndex, out Int32 sliceLength)
        {

            sliceIndex = index;
            sliceLength = 0;

            if (index >= document.Length)
                return false;

            Int32 i = index;
            for (; i < document.Length - 1; ++i)
            {
                if(document[i] == '\n' && document[i + 1] == '\n')
                {
                    break;
                }
            }
            sliceLength = (i + 1) - sliceIndex;


            return true;
        }


    }
}
