using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DayFour
{
    public abstract class RequirementBase
    {
        public abstract Boolean IsValid(in ReadOnlySpan<Char> passport);
    }

    public class BYRRequirements : RequirementBase
    {
        public override Boolean IsValid(in ReadOnlySpan<Char> passport)
        {
            Passport.GetFieldValue(in passport, "byr", out String value);
            if (value == null)
                return false;
            if (Int32.TryParse(value, out Int32 result))
                return result >= 1920 && result <= 2002;
            return false;
        }
    }

    public class IYRRequirements : RequirementBase
    {
        public override bool IsValid(in ReadOnlySpan<char> passport)
        {
            Passport.GetFieldValue(in passport, "iyr", out String value);
            if (value == null)
                return false;
            if (Int32.TryParse(value, out Int32 result))
                return result >= 2010 && result <= 2020;
            return false;
        }
    }

    public class EYRRequirements : RequirementBase
    {
        public override bool IsValid(in ReadOnlySpan<char> passport)
        {
            Passport.GetFieldValue(in passport, "eyr", out String value);
            if (value == null)
                return false;
            if (Int32.TryParse(value, out Int32 result))
                return result >= 2020 && result <= 2030;
            return false;
        }
    }

    public class HGTRequirements : RequirementBase
    {
        public override bool IsValid(in ReadOnlySpan<char> passport)
        {
            Passport.GetFieldValue(in passport, "hgt", out String value);
            if (value == null)
                return false;
            if (value.Contains("cm"))
            {
                value = value.Replace("cm", "");
                if (Int32.TryParse(value, out Int32 result))
                    return result >= 150 && result <= 193;

            }
            else if(value.Contains("in"))
            {
                value = value.Replace("in", "");
                if (Int32.TryParse(value, out Int32 result))
                    return result >= 59 && result <= 79;
            }
            return false;
        }
    }

    public class HCLRequirements : RequirementBase
    {
        public override bool IsValid(in ReadOnlySpan<char> passport)
        {
            Passport.GetFieldValue(in passport, "hcl", out String value);
            if (value == null)
                return false;
            Boolean matches = Regex.Match(value, "^#(?:[0-9a-f]{6})$").Success;
            return matches;
        }
    }

    public class ECLRequirements : RequirementBase
    {
        public override bool IsValid(in ReadOnlySpan<char> passport)
        {
            String[] reqs = new string[]
            {
                "amb", "blu", "brn", "gry", "grn", "hzl", "oth"
            };
            Passport.GetFieldValue(in passport, "ecl", out String value);
            if (value == null)
                return false;

            foreach (var r in reqs)
            {
                if (r == value)
                    return true;
            }


            return false;
        }
    }
    public class PIDRequirements : RequirementBase
    {
        public override bool IsValid(in ReadOnlySpan<char> passport)
        {
            Passport.GetFieldValue(in passport, "pid", out String value);
            if (value == null)
                return false;
            if (value.Length != 9)
                return false;

            Boolean isValid = Int32.TryParse(value, out _);
            return isValid;
        }
    }
}
