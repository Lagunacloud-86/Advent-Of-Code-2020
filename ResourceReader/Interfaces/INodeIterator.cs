
using System;

using ResourceReader.Enum;
using ResourceReader.Nodes.Structures;

namespace ResourceReader.Interfaces
{
    public interface INodeIterator
    {
        ref Node NextSibling();
        ref Node PrevSibling();




    }
}
