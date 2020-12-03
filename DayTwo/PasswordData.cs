using System;

namespace DayTwo
{
    public class RuleData
    {
        private readonly Rule _rule;

        private readonly String _password;


        public RuleData(ReadOnlySpan<Char> line)
        {
            GetRuleRange(
                in line,
                out char character,
                out int min, out int max,
                out int sliceIndex, out int sliceLength);

            _rule = new Rule(character, min, max);
            _password = line.Slice(sliceIndex, sliceLength).ToString();
        }

        public ref readonly Rule GetRule()
        {
            return ref _rule;
        }

        public ref readonly String GetPassword()
        {
            return ref _password;
        }

        private void GetRuleRange(
            in ReadOnlySpan<Char> line,
            out Char character,
            out Int32 min, out Int32 max, 
            out int sliceIndex, out int sliceLength)
        {
            ReadOnlySpan<Char> range = line.Slice(0, line.IndexOf(' '));

            Int32 rangeSplitIndex = range.IndexOf('-');
            ReadOnlySpan<Char> minSpan = range.Slice(0, rangeSplitIndex);
            ReadOnlySpan<Char> maxSpan = range.Slice(rangeSplitIndex + 1, range.Length - (rangeSplitIndex + 1));

            min = 0;
            max = 0;
            Int32.TryParse(minSpan.ToString(), out min);
            Int32.TryParse(maxSpan.ToString(), out max);



            character = line[line.IndexOf(':') - 1];

            sliceIndex = line.IndexOf(':') + 2;
            sliceLength = line.Length - (line.IndexOf(':') + 2);
        }
    }
}
