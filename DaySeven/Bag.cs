using System;
using System.Collections.Generic;
using System.Text;

namespace DaySeven
{
    public readonly struct Bag
    {
        public Dictionary<String, SubBag> SubBags { get; }

        public String BagType { get; }


        public Bag(in ReadOnlySpan<Char> node)
        {
            Int32 index = 0, subStartIndex = 0;

            ReadOnlySpan<Char> containSpan = "contain";
            ReadOnlySpan<Char> bagSpan = "bag";

            //Find bag type
            this.BagType = null;
            for (; index < node.Length; ++index)
            {
                if (bagSpan.SequenceEqual(node.Slice(index, 3)))
                {
                    this.BagType = node.Slice(0, index - 1).ToString();
                    index += 3;
                }
                if (containSpan.SequenceEqual(node.Slice(index, 7)))
                {
                    index += 8;
                    subStartIndex = index;
                    break;
                }
               
            }

            this.SubBags = new Dictionary<String, SubBag>();
            for(; index < node.Length; ++index)
            {
                if (node[index] == ',' || node[index] == '.')
                {
                    ReadOnlySpan<Char> subNode = node.Slice(subStartIndex, index - subStartIndex);
                    SubBag subBag = new SubBag(in subNode);
                    this.SubBags.Add(subBag.BagType, subBag);
                    subStartIndex = index + 2;
                    index++;

                }
            }
        }

        public Boolean HasSubBag(in String bagType)
        {
            return this.SubBags.ContainsKey(bagType);
        }

        public Int32 SubBagCount(in String bagType)
        {
            if (this.SubBags.ContainsKey(bagType))
                return this.SubBags[bagType].BagCount;
            return -1;
        }

    }
}
