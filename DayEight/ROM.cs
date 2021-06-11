
using System;

namespace DayEight
{
    public readonly struct ROM
    {

        public String Instruction { get; }

        public Int32 Parameter { get; }

        public ROM(String instruction, in Int32 parameter)
        {
            this.Instruction = instruction;
            this.Parameter = parameter;
        }
        public ROM(in ReadOnlySpan<Char> node)
        {
            this.Instruction = node.Slice(0, 3).ToString();
            this.Parameter = 0;
            ReadOnlySpan<Char> parameter = node[4..];

            if (Int32.TryParse(parameter.ToString(), out Int32 result))
            {
                this.Parameter = result;
            }
        }

    }
}
