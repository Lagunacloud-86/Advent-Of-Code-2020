using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySeventeen
{
    public class NTree
    {
        private Dictionary<Guid, List<Guid>> _activeNodeNeighborLookup =
            new Dictionary<Guid, List<Guid>>();

        private List<ConwayCubeInfo> _enqueuedAdd =
            new List<ConwayCubeInfo>();

        private List<Guid> _enqueuedRemoval = 
            new List<Guid>();

        private Dictionary<Guid, ConwayCubeInfo> _nodes = 
            new Dictionary<Guid, ConwayCubeInfo>();

        private Dictionary<Point, Guid> _activeNodePositions = 
            new Dictionary<Point, Guid>();

        public Int32 ActiveNodes => _nodes.Where(x => x.Value.IsActive).Count();

        public void AddNode(in ConwayCubeInfo node)
        {
            _nodes.Add(node.Id, node);
            if (node.IsActive)
                _activeNodePositions.Add(node.Point, node.Id);
        }
        

        public void BuildNeighbors()
        {
            _activeNodeNeighborLookup.Clear();
            foreach(var nodeKey in _nodes.Keys)
            {
                List<Guid> _activeNeighbors = new List<Guid>();
                Internal_BuildActiveNeighbors(nodeKey, _activeNeighbors);
                _activeNodeNeighborLookup.Add(nodeKey, _activeNeighbors);
            }
        }

        public void Iterate(Int32 depth)
        {
            List<Point> potentialPoints = new List<Point>();
            List<Point> vectors = new List<Point>();
            Internal_BuildVectors(depth, vectors);

            foreach (var kvp in _activeNodeNeighborLookup)
            {
                Int32 activeNeighbors = Internal_CalculateActiveNeighbors(_nodes[kvp.Key]);
                //if (!_nodes[kvp.Key].IsActive && activeNeighbors == 3)
                //{
                //    _enqueuedAdd.Add(new ConwayCubeInfo(_nodes[kvp.Key].Point, true));
                //}
                if (_nodes[kvp.Key].IsActive && activeNeighbors != 2 && activeNeighbors != 3)
                {
                    _enqueuedRemoval.Add(kvp.Key);
                }

                foreach (Point v in vectors)
                {
                    Point potentialPoint = Point
                        .Add(_nodes[kvp.Key].Point, in v);
                    if (_activeNodePositions.ContainsKey(potentialPoint))
                        continue;
                    potentialPoints.Add(potentialPoint);
                }
            }

            var pointGroupings = potentialPoints
                .GroupBy(x => new { x.X, x.Y, x.Z, x.W })
                .Where(x => x.Count() == 3)
                .ToList();
            var newPoints = pointGroupings
                .SelectMany(x => x)
                .ToList();
            foreach(Point point in newPoints)
            {
                _enqueuedAdd.Add(new ConwayCubeInfo(point, true));
            }


            Internal_DoRemovalOfNodes();
            Internal_DoAddOfNodes();
            BuildNeighbors();
        }
 

        private Int32 Internal_CalculateActiveNeighbors(in ConwayCubeInfo node)
        {
            Int32 count = 0;
            foreach (var neighbor in _activeNodeNeighborLookup[node.Id])
            {
                if (_nodes[neighbor].IsActive)
                    count++;
            }
            return count;
        }

        private void Internal_BuildActiveNeighbors(Guid nodeKey, List<Guid> neighbors)
        {
            foreach(var node in _nodes.Where(x => x.Value.Id != nodeKey))
            {
                if (Point.IsNeighbor(_nodes[nodeKey].Point, node.Value.Point))
                {
                    if (node.Value.IsActive)
                        neighbors.Add(node.Value.Id);
                    //else
                    //    _inactiveNeighbors.Add(_nodes[j].Id);
                }

            }
        }
        private void Internal_BuildVectors(Int32 depth, List<Point> vectors)
        {
            Int32 depthLength = depth * depth * 2;
            for(Int32 i = 0; i < depthLength; ++i)
            {

            }
        }

        private void Internal_DoAddOfNodes()
        {
            foreach (ConwayCubeInfo key in _enqueuedAdd)
            {
                AddNode(key);
            }
            _enqueuedAdd.Clear();
        }
        private void Internal_DoRemovalOfNodes()
        {

            foreach (Guid key in _enqueuedRemoval)
            {
                if (_activeNodePositions.ContainsKey(_nodes[key].Point))
                    _activeNodePositions.Remove(_nodes[key].Point);
                _nodes.Remove(key);
            }
            _enqueuedRemoval.Clear();
        }
    }
}
