
using System;
using System.Collections.Generic;
using System.Text;


namespace DayEight
{
    public class Computer
    {

        private readonly Dictionary<String, Func<Computer, Int32, Int32>> _instructions = null;

        public Int32 Accumulator { get; set; }

        public Computer()
        {
            this.Accumulator = 0;

            _instructions = new Dictionary<string, Func<Computer, Int32, Int32>>
            {
                { "nop", (computer, param) => 1 },
                { "acc", (computer, param) => { computer.Accumulator += param; return 1; } },
                { "jmp", (computer, param) => { return param; } }
            };
        }


        public Int32 RunInstruction(String instruction, Int32 paramater)
        {
            return _instructions[instruction].Invoke(this, paramater);
        }

    }
}
