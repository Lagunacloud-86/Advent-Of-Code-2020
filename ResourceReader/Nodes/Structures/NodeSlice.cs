using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceReader.Nodes.Structures
{
    public readonly struct NodeSlice
    {
        public Int32 SliceIndex { get; }

        public Int32 SliceLength { get; }

        public NodeSlice(in Int32 sliceIndex, in Int32 sliceLength)
        {
            SliceIndex = sliceIndex;
            SliceLength = sliceLength;
        }
    }
}
