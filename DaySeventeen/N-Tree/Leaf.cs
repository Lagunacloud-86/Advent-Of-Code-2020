//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DaySeventeen
//{
//    public class Leaf<TNode, TKey>
//        where TNode : struct
//    {
//        private readonly Guid _id;

//        public Guid Id => _id;

//        public List<TNode> Nodes { get; }

//        public Leaf<TNode, TKey> Parent { get; }

//        public Int32 ChildrenNodeCount { get; private set; }

//        public List<Leaf<TNode, TKey>> Children { get; private set; }

//        //public Action<Leaf<TNode>> LeafSplit { get; }

//        //public Action<Leaf<TNode>> LeafMerge { get; }


//        public Leaf(Leaf<TNode, TKey> parent = null)
//        {
//            _id = Guid.NewGuid();
//            Parent = parent;
//            Children = new List<Leaf<TNode, TKey>>();
//            ChildrenNodeCount = 0;
//            Nodes = new List<TNode>();
//        }


//        public void AddNode(in TNode node, in Int32 maxNodeCount)
//        {
//            if(Nodes.Count == maxNodeCount)
//            {
//                //LeafSplit?.Invoke(this, null);

//            }
//        }
//        public void RemoveNode(in TKey id, in Int32 maxNodeCount)
//        {
//            if (Nodes.Count == maxNodeCount)
//            {
//                //LeafSplit?.Invoke(this, null);

//            }
//        }
//    }
//}
