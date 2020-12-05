
using System;

using ResourceReader.Enum;
using ResourceReader.Nodes.Structures;

namespace ResourceReader.Interfaces
{
    public interface INodeIterator
    {
        //ref Node NextSibling(in ReadOnlySpan<Char> document);
        //ref Node PrevSibling(in ReadOnlySpan<Char> document);

        Boolean NextNode(
            in ReadOnlySpan<Char> document, 
            out Int32 sliceIndex, out Int32 sliceLength);


        void Reset();
    }
}
