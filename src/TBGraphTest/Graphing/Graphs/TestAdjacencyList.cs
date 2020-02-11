using System;
using System.Collections.Generic;
using NUnit.Framework;
using TBGraph.Graphing;

namespace TBGraphTest.Graphing.Graphs
{
    public class TestAdjacencyList
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestAddNodeSingle()
        {
            AdjacencyList<int> graph = new AdjacencyList<int>();
            graph.AddNode(1);

            Assert.AreEqual(1, graph.Count);
        }

        [Test]
        public void TestAddNodeMultipleDifferent()
        {
            AdjacencyList<int> graph = new AdjacencyList<int>();
            graph.AddNode(1);
            graph.AddNode(2);

            Assert.AreEqual(2, graph.Count);
        }

        [Test]
        public void TestAddNodeSeveral()
        {
            AdjacencyList<int> graph = new AdjacencyList<int>();
            int ceiling = 100;
            for (int i = 0; i < ceiling; i++)
            {
                graph.AddNode(i);
            }

            Assert.AreEqual(ceiling, graph.Count);

        }

        [Test]
        public void TestAddNodeFailsAlreadyExists()
        {
            AdjacencyList<int> graph = new AdjacencyList<int>();
            graph.AddNode(1);
            Assert.AreEqual(1, graph.Count);

            bool success = graph.AddNode(1);
            Assert.IsFalse(success);
            Assert.AreEqual(1, graph.Count);
        }

        [Test]
        public void TestAddEdge()
        {
            AdjacencyList<int> graph = new AdjacencyList<int>();
            graph.AddNode(1);
            graph.AddNode(2);

            bool success = graph.AddEdge(1, 2);
            Assert.AreEqual(2, graph.Count);
            Assert.IsTrue(success);
        }

        [Test]
        public void TestAddEdgeDestDoesNotExist()
        {
            AdjacencyList<int> graph = new AdjacencyList<int>();
            graph.AddNode(1);

            bool success = graph.AddEdge(1, 2);
            Assert.IsFalse(success);
        }

        [Test]
        public void TestAddEdgeSourceDoesNotExist()
        {
            AdjacencyList<int> graph = new AdjacencyList<int>();
            graph.AddNode(2);

            bool success = graph.AddEdge(1, 2);
            Assert.IsFalse(success);
        }

        [Test]
        public void TestAddEdgeBothDoNotExist()
        {
            AdjacencyList<int> graph = new AdjacencyList<int>();
            graph.AddNode(1);
            graph.AddNode(2);

            bool success = graph.AddEdge(3, 4);
            Assert.IsFalse(success);
        }

        [Test]
        public void TestGetEdges()
        {
            AdjacencyList<int> graph = new AdjacencyList<int>();

            int one = 1;
            int two = 2;

            graph.AddNode(one);
            graph.AddNode(two);

            bool success = graph.AddEdge(one, two);
            IList<int> oneEdges = graph.GetEdges(1);
            IList<int> twoEdges = graph.GetEdges(2);

            Assert.IsTrue(success);
            Assert.AreEqual(one, oneEdges.Count);
            Assert.AreEqual(two, oneEdges[0]);

            Assert.AreEqual(0, twoEdges.Count);
        }

        [Test]
        public void TestGetEdgesEmptyGraph()
        {
            AdjacencyList<int> graph = new AdjacencyList<int>();

            bool thrown = false;

            for(int i = 0; i < 100; i++)
            {
                try
                {
                    graph.GetEdges(i);
                }
                catch(KeyNotFoundException)
                {
                    thrown = true;
                }

                if(!thrown)
                {
                    Assert.Fail("Exception not thrown");
                }
            }

            Assert.Pass();
        }

        [Test]
        public void TestGetEdgesNotInGraph()
        {
            AdjacencyList<int> graph = new AdjacencyList<int>();
            graph.AddNode(1);
            try
            {
                graph.GetEdges(2);
            }
            catch (KeyNotFoundException)
            {
                Assert.Pass();
            }
            Assert.Fail("Exception not thrown");
        }

        public void TestGetAccessor()
        {
            AdjacencyList<int> graph = new AdjacencyList<int>();

            int one = 1;
            int two = 2;

            graph.AddNode(one);
            graph.AddNode(two);

            bool success = graph.AddEdge(one, two);
            IList<int> oneEdges = graph[one];
            IList<int> twoEdges = graph[two];

            Assert.IsTrue(success);
            Assert.AreEqual(one, oneEdges.Count);
            Assert.AreEqual(two, oneEdges[0]);

            Assert.AreEqual(0, twoEdges.Count);
        }

        [Test]
        public void TestGetEnumerator()
        {
            List<int[]> expecteds = new List<int[]>
            {
                new int[] { 2, 3 },
                new int[] { 3 },
                new int[] { 1 },
            };

            AdjacencyList<int> graph = new AdjacencyList<int>();

            int one = 1;
            int two = 2;
            int three = 3;

            graph.AddNode(one);
            graph.AddNode(two);
            graph.AddNode(three);

            graph.AddEdge(one, two);
            graph.AddEdge(one, three);
            graph.AddEdge(two, three);
            graph.AddEdge(three, one);


            IEnumerator<KeyValuePair<int, List<int>>> enumerator = graph.GetEnumerator();
            Assert.IsNotNull(enumerator);

            KeyValuePair<int, List<int>> current;
            while(enumerator.MoveNext())
            {
                current = enumerator.Current;
                Assert.AreEqual(current.Key, expecteds[current.Key - 1]);
            }
        }
    }
}
