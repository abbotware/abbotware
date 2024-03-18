namespace Abbotware.UnitTests.Core.Collections
{
    using System.Linq;
    using Abbotware.Core.Collections.Trees;
    using Abbotware.Core.Collections.Trees.Internal;
    using NUnit.Framework;

    internal class Trees
    {
        [Test]
        [Ignore("broken")]
        public void CompleteHeapTree()
        {
            var t = new CompleteHeapTree<Node<int>>(2, 10);

            Assert.That(t.OnComputeDepth(0), Is.EqualTo(0));
            Assert.That(t.OnComputeDepth(1), Is.EqualTo(1));
            Assert.That(t.OnComputeDepth(2), Is.EqualTo(1));
            Assert.That(t.OnComputeDepth(3), Is.EqualTo(2));
            Assert.That(t.OnComputeDepth(4), Is.EqualTo(2));
            Assert.That(t.OnComputeDepth(5), Is.EqualTo(2));
            Assert.That(t.OnComputeDepth(6), Is.EqualTo(2));

            t.TraverseDown(a => { }, a => a.State = a.Parents[0]?.State + 1 ?? 1, a => { });

            t.TraverseUp(
                a => { },
                a =>
                {
                    if (!a.Children.Any())
                    {
                        return;
                    }

                    Assert.That(a.Children.Count, Is.EqualTo(t.Branches));
                    Assert.That(a.Children.Where(x => x.ChildIds.Any()).All(x => x.State == a.Depth + 2), Is.True);
                });
        }

        [Test]
        public void RecombingHeapTree2()
        {
            var t = new RecombingHeapTree<int>(2, 10);

            Assert.That(t.OnComputeDepth(0), Is.EqualTo(0));
            Assert.That(t.OnComputeDepth(1), Is.EqualTo(1));
            Assert.That(t.OnComputeDepth(2), Is.EqualTo(1));
            Assert.That(t.OnComputeDepth(3), Is.EqualTo(2));
            Assert.That(t.OnComputeDepth(4), Is.EqualTo(2));
            Assert.That(t.OnComputeDepth(5), Is.EqualTo(2));
            Assert.That(t.OnComputeDepth(6), Is.EqualTo(3));
            Assert.That(t.OnComputeDepth(7), Is.EqualTo(3));
            Assert.That(t.OnComputeDepth(8), Is.EqualTo(3));
            Assert.That(t.OnComputeDepth(9), Is.EqualTo(3));
            Assert.That(t.OnComputeDepth(10), Is.EqualTo(4));
            Assert.That(t.OnComputeDepth(11), Is.EqualTo(4));
            Assert.That(t.OnComputeDepth(12), Is.EqualTo(4));
            Assert.That(t.OnComputeDepth(13), Is.EqualTo(4));
            Assert.That(t.OnComputeDepth(14), Is.EqualTo(4));
            Assert.That(t.OnComputeDepth(15), Is.EqualTo(5));
            Assert.That(t.OnComputeDepth(16), Is.EqualTo(5));
            Assert.That(t.OnComputeDepth(17), Is.EqualTo(5));
            Assert.That(t.OnComputeDepth(18), Is.EqualTo(5));
            Assert.That(t.OnComputeDepth(19), Is.EqualTo(5));
            Assert.That(t.OnComputeDepth(20), Is.EqualTo(5));

            Assert.That(t.OnComputeChildIndex(0, 0), Is.EqualTo(1));
            Assert.That(t.OnComputeChildIndex(0, 1), Is.EqualTo(2));
            Assert.That(t.OnComputeChildIndex(1, 0), Is.EqualTo(3));
            Assert.That(t.OnComputeChildIndex(1, 1), Is.EqualTo(4));
            Assert.That(t.OnComputeChildIndex(2, 0), Is.EqualTo(4));
            Assert.That(t.OnComputeChildIndex(2, 1), Is.EqualTo(5));
            Assert.That(t.OnComputeChildIndex(3, 0), Is.EqualTo(6));
            Assert.That(t.OnComputeChildIndex(3, 1), Is.EqualTo(7));
            Assert.That(t.OnComputeChildIndex(4, 0), Is.EqualTo(7));
            Assert.That(t.OnComputeChildIndex(4, 1), Is.EqualTo(8));
            Assert.That(t.OnComputeChildIndex(5, 0), Is.EqualTo(8));
            Assert.That(t.OnComputeChildIndex(5, 1), Is.EqualTo(9));
            Assert.That(t.OnComputeChildIndex(6, 0), Is.EqualTo(10));
            Assert.That(t.OnComputeChildIndex(6, 1), Is.EqualTo(11));

            Assert.That(t.OnComputeNodeCount(1), Is.EqualTo(1));
            Assert.That(t.OnComputeNodeCount(2), Is.EqualTo(3));
            Assert.That(t.OnComputeNodeCount(3), Is.EqualTo(6));
            Assert.That(t.OnComputeNodeCount(4), Is.EqualTo(10));
            Assert.That(t.OnComputeNodeCount(5), Is.EqualTo(15));
        }
    }
}
