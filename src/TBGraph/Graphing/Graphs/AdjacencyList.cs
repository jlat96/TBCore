using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TBGraph.Graphing.Exceptions;

namespace TBGraph.Graphing
{
    public class AdjacencyList<TNode> : IGraph<TNode>, IEnumerable<KeyValuePair<TNode, List<TNode>>>
    {
        private readonly Dictionary<TNode, List<TNode>> graph;

        public int Count => graph.Keys.Count;

        public AdjacencyList()
        {
            graph = new Dictionary<TNode, List<TNode>>();
        }

        public bool AddEdge(TNode source, TNode dest)
        {
            if (!graph.ContainsKey(source) || !graph.ContainsKey(dest))
            {
                return false;
            }

            graph[source].Add(dest);
            return true;
        }

        public bool AddNode(TNode node)
        {
            if (!graph.ContainsKey(node))
            {
                graph[node] = new List<TNode>();
                return true;
            }

            return false;
        }

        public IList<TNode> GetEdges(TNode node)
        {
            if (graph.ContainsKey(node))
            {
                return graph[node];
            }

            throw new KeyNotFoundException("The requested node is not a vertex");
        }

        public IList<TNode> this[TNode node]
        {
            get { return GetEdges(node); }
        }

        public bool RemoveEdge(TNode source, TNode dest)
        {
            IList<TNode> sourceEdges = GetEdges(source);
            return sourceEdges.Remove(dest);
        }

        public bool RemoveNode(TNode node)
        {
            if (!graph.ContainsKey(node))
            {
                return false;
            }

            foreach(TNode currentKey in graph.Keys)
            {
                if (!currentKey.Equals(node))
                {
                    GetEdges(currentKey).Remove(node);
                }
            }

            return graph.Remove(node);
        }

        public IEnumerator<KeyValuePair<TNode, List<TNode>>> GetEnumerator()
        {
            return graph.Select(s => s).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
