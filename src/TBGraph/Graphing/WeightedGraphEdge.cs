using System;
namespace TBGraph.Graphing
{
    public class WeightedGraphEdge<TNode> : GraphEdge<TNode>
    {
        public WeightedGraphEdge()
        {
        }

        public int Weight { get; set; }
    }
}
