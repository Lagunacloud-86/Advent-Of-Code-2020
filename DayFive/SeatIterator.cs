
using System;
using System.Collections.Generic;
using System.Text;

using ResourceReader.Interfaces;
using ResourceReader.Enum;
using ResourceReader.Nodes.Structures;

namespace DayFive
{
    public class SeatIterator : INodeIterator
    {
        private Int32 _index = 0;
        //public ref Node NextSibling(in ReadOnlySpan<Char> document)
        //{
        //    Int32 sliceIndex = _index;
        //    for(; _index < document.Length && document[_index] != '\n'; _index++)
        //    {

        //    }

        //    return ref new Node(
        //        ENodeType.NewNode,
        //        in sliceIndex, (_index - sliceIndex));
        //}

        //public ref Node PrevSibling(in ReadOnlySpan<Char> document)
        //{
        //    for (; _index >= 0 && document[_index] != '\n'; _index--)
        //    {

        //    }
        //}

        public Boolean NextNode(
            in ReadOnlySpan<Char> document,
            out Int32 sliceIndex, out Int32 sliceLength)
        {
            sliceIndex = _index;
            sliceLength = 0;

            if (_index >= document.Length)
                return false;

            do { _index++; } while (_index < document.Length && document[_index] != '\n');
            sliceLength = _index - sliceIndex;
            _index++;
            return true;
        }

        public void Reset()
        {
            _index = 0;
        }
    }
}
