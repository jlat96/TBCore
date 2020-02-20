using System;
using System.Collections.Generic;

namespace TBGraph.Graphing
{
    /// <summary>
    /// A representation of a collection of nodes (vertices) and the edges that connect those verticies
    /// </summary>
    /// <typeparam name="TNode">The type of node representing a vertex</typeparam>
    public interface IGraph<TNode>
    {
        /// <summary>
        /// Add a vertex to the graph
        /// </summary>
        /// <param name="node">The node to add</param>
        /// <returns>True if the node was added successfully </returns>
        bool AddNode(TNode node);

        /// <summary>
        /// Adds an edge to the graph between the source and dest nodes
        /// </summary>
        /// <param name="source">The beginning of the edge</param>
        /// <param name="dest">The end of the edge</param>
        /// <returns>True if the edge was added successfully</returns>
        bool AddEdge(TNode source, TNode dest);

        /// <summary>
        /// Removes all edges between the given node and any other vertex in the graph
        /// and removes the given node's vertex
        /// </summary>
        /// <param name="node">Node to remove</param>
        /// <returns>True if the node could be successfully removed</returns>
        bool RemoveNode(TNode node);

        /// <summary>
        /// Removes an edge between source and dest from the graph
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns>True if the edge could be successfully removed</returns>
        bool RemoveEdge(TNode source, TNode dest);
        IList<TNode> GetEdges(TNode node);
    }
}
