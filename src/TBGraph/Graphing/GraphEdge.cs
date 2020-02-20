using System;
namespace TBGraph.Graphing
{
    public class GraphEdge<TNode>
    {
        public GraphEdge()
        {
        }

        public TNode Source { get; set; }
        public TNode Dest { get; set; }
    }
}
