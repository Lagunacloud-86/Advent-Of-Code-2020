using System;
using System.Collections.Generic;
using System.Text;

namespace DaySeven
{
    public readonly struct SubBag
    {

        public String BagType { get; }

        public Int32 BagCount { get; }

        public SubBag(in ReadOnlySpan<Char> node)
        {
            ReadOnlySpan<Char> bagSpan = "bag";

            Int32 index = 0, bagIndex = 0;

            this.BagType = "";
            this.BagCount = 0;


            for(; index < node.Length; index++)
            {
                if (node[index] == ' ')
                {
                    if (Int32.TryParse(node.Slice(0, index).ToString(), out Int32 result))
                        this.BagCount = result;
                    index++;
                    break;
                }
            }
            this.BagType = null;
            bagIndex = index;
            for (; index < node.Length - 2; index++)
            {
                if (bagSpan.SequenceEqual(node.Slice(index, 3)))
                {
                    this.BagType = node.Slice(bagIndex, index - (bagIndex + 1)).ToString();
                    break;
                }
            }

            



        }

    }
}
