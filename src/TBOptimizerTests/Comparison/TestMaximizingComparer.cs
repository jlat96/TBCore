using System.Collections.Generic;
using NUnit.Framework;
using TrailBlazer.TBOptimizer.Comparison;

namespace OptimizerTests.Comparison
{
    [TestFixture]
    public class TestMaximizingComparer
    {
        IComparer<int> comparer;

        [SetUp]
        public void Setup()
        {
            comparer = new MaximizingComparer<int>();
        }

        [Test]
        public void TestComparerChoosesMaximum()
        {
            int left = 1;
            int right = 2;

            int result = comparer.Compare(left, right);
            Assert.IsTrue(result > 0);
        }

        [Test]
        public void TestCompareLessThan()
        {
            int left = 2;
            int right = 1;

            int result = comparer.Compare(left, right);
            Assert.IsTrue(result < 0);
        }

        [Test]
        public void TestCompareEqual()
        {
            int left = 1;
            int right = 1;

            int result = comparer.Compare(left, right);
            Assert.IsTrue(result == 0);
        }
    }
}

