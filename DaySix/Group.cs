using System;
using System.Collections.Generic;
using System.Text;

namespace DaySix
{
    public readonly struct Group
    {

        public Int32 GroupSize { get; }

        public Dictionary<Char, Int32> UserYes { get; }

        public Group(in ReadOnlySpan<Char> group)
        {
            this.GroupSize = 1;
            this.UserYes = new Dictionary<char, int>();
            for (Int32 i = 0; i < group.Length; ++i)
            {
                if(group[i] == '\n')
                {
                    this.GroupSize++;
                    continue;
                }

                if(!this.UserYes.ContainsKey(group[i]))
                {
                    this.UserYes.Add(group[i], 1);
                }
                else
                {
                    this.UserYes[group[i]]++;
                }
            }
        }
    }
}
