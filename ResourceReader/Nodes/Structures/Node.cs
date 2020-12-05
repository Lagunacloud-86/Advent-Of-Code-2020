
using System;
using System.Collections.Generic;
using System.Text;

using ResourceReader.Enum;

namespace ResourceReader.Nodes.Structures
{
    public readonly struct Node
    {

        public ENodeType NodeType { get; }

        public NodeSlice NodeSlice { get; }

        //public Node[] Children { get; }

        //public Boolean HasChildren => Children.Length > 0;


        public Node(
            in ENodeType nodeType, 
            in Int32 sliceIndex, in Int32 sliceLength)
        {
            this.NodeType = nodeType;
            this.NodeSlice = new NodeSlice(
                sliceIndex: sliceIndex,
                sliceLength: sliceLength);
        }
        public Node(
            in ENodeType nodeType,
            in NodeSlice nodeSlice)
        {
            this.NodeType = nodeType;
            this.NodeSlice = new NodeSlice(
                sliceIndex: nodeSlice.SliceIndex,
                sliceLength: nodeSlice.SliceLength);
        }

    }
}
