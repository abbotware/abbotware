namespace Abbotware.UnitTests.Core
{
    using System;
    using System.Collections.Generic;
    using Abbotware.Core.Collections;
    using Abbotware.Interop.NUnit;
    using NUnit.Framework;

    [Category("Core.UniquePairs")]
    [Category("Core")]
    [TestFixture]
    public class UniquePairTests
    {
        [Test]
        public void Create()
        {
            var up1 = new UniquePairs<string>();
            Assert.IsNotNull(up1);

            var up2 = new UniquePairs<int>();
            Assert.IsNotNull(up2);
        }

        [Test]
        public void String_Add_Lookup()
        {
            var pairs = new UniquePairs<string>();

            pairs.Add("test1", "test2");

            Assert.AreEqual("test2", pairs.Other("test1"));
            Assert.AreEqual("test1", pairs.Other("test2"));

            Assert.IsFalse(pairs.Contains("12345"));
            Assert.IsTrue(pairs.Contains("test1"));
            Assert.IsTrue(pairs.Contains("test2"));

            pairs.Remove("test1");
            Assert.IsFalse(pairs.Contains("test1"));
            Assert.IsFalse(pairs.Contains("test2"));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void String_Add_Duplicates()
        {
            var pairs = new UniquePairs<string>();

            pairs.Add("test", "test");
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void String_Add_Existing_First()
        {
            var pairs = new UniquePairs<string>();

            pairs.Add("test1", "test2");
            pairs.Add("test1", "test3");
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void String_Add_Existing_Second()
        {
            var pairs = new UniquePairs<string>();

            pairs.Add("test1", "test2");
            pairs.Add("test2", "test3");
        }

        [Test]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void String_OtherNotFound()
        {
            var pairs = new UniquePairs<string>();

            pairs.Other("asdf");
        }

        [Test]
        public void Long_Add_Lookup()
        {
            var pairs = new UniquePairs<long>();

            pairs.Add(10, 100);

            Assert.AreEqual(100, pairs.Other(10));
            Assert.AreEqual(10, pairs.Other(100));

            Assert.IsFalse(pairs.Contains(12345));
            Assert.IsTrue(pairs.Contains(10));
            Assert.IsTrue(pairs.Contains(100));

            pairs.Remove(100);
            Assert.IsFalse(pairs.Contains(10));
            Assert.IsFalse(pairs.Contains(100));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Long_Add_Duplicates()
        {
            var pairs = new UniquePairs<long>();

            pairs.Add(1, 1);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Long_Add_Existing_First()
        {
            var pairs = new UniquePairs<long>();

            pairs.Add(1, 2);
            pairs.Add(1, 3);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Long_Add_Existing_Second()
        {
            var pairs = new UniquePairs<long>();

            pairs.Add(1, 2);
            pairs.Add(2, 3);
        }

        [Test]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Long_OtherNotFound()
        {
            var pairs = new UniquePairs<long>();

            pairs.Other(1234);
        }
    }
}