using System;
namespace TBGraph.Graphing
{
    public class GraphNode<TValue>
    {
        public GraphNode()
        {

        }

        public GraphNode(TValue value)
        {
            Value = value;
        }


        public TValue Value { get; set; }
    }
}
