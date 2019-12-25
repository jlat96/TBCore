using System;
using NUnit.Framework;

namespace OptimizerTests.Comparison
{
    [TestFixture]
    public class TestGenericComparison
    {

        [Test]
        public void TestMax()
        {
            int left = 1;
            int right = 2;

            int max = left.Max(right);
            Assert.AreEqual(right, max);
        }

        [Test]
        public void TestMaxRepeating()
        {
            int left;
            int right;
            for (int i = -100; i < 100; i++)
            {
                left = i;
                right = i + 1;
                Assert.AreEqual(right, left.Max(right));
            }
        }

        [Test]
        public void TestMin()
        {
            int left = 1;
            int right = 2;

            int min = left.Min(right);
            Assert.AreEqual(left, min);
        }

        [Test]
        public void TestMinRepeating()
        {
            int left;
            int right;
            for (int i = -100; i < 100; i++)
            {
                left = i;
                right = i + 1;
                Assert.AreEqual(left, left.Min(right));
            }
        }
    }
}
