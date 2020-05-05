namespace Abbotware.UnitTests.Core
{
    using System;
    using System.Collections.Generic;
    using Abbotware.Core.Diagnostics;
    using NUnit.Framework;

    [TestFixture]
    [Category("Core")]
    [Category("Core.Diagnostics")]
    public class ReflectionHelperTests
    {
        [Test]
        public void ReflectionHelper_BothNull()
        {
            Assert.IsTrue(ReflectionHelper.AreObjectsEqual(null, null));
        }

        [Test]
        public void ReflectionHelper_LeftNull()
        {
            var b = new TestClass();

            Assert.IsFalse(ReflectionHelper.AreObjectsEqual(null, b));
        }

        [Test]
        public void ReflectionHelper_RightNull()
        {
            var a = new TestClass();

            Assert.IsFalse(ReflectionHelper.AreObjectsEqual(a, null));
        }

        [Test]
        public void ReflectionHelper_BothNothingSet()
        {
            var a = new TestClass();
            var b = new TestClass();

            Assert.IsTrue(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_Equal_String()
        {
            var a = new TestClass
            {
                String1 = "test",
            };

            var b = new TestClass
            {
                String1 = "test",
            };

            Assert.IsTrue(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_Equal_Enum()
        {
            var a = new TestClass
            {
                Enum = TestClass.T.A,
            };

            var b = new TestClass
            {
                Enum = TestClass.T.A,
            };

            Assert.IsTrue(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_Equal_NullableEnum()
        {
            var a = new TestClass
            {
                NullableEnum = TestClass.T.C,
            };

            var b = new TestClass
            {
                NullableEnum = TestClass.T.C,
            };

            Assert.IsTrue(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_Equal_DateTimeOffset()
        {
            var dto = DateTimeOffset.Now;

            var a = new TestClass
            {
                DateTimeOffset = dto,
            };
            var b = new TestClass
            {
                DateTimeOffset = dto,
            };

            Assert.IsTrue(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_Equal_NullableDateTimeOffset()
        {
            var dto = DateTimeOffset.Now;

            var a = new TestClass
            {
                NullableDateTimeOffset = dto,
            };

            var b = new TestClass
            {
                NullableDateTimeOffset = dto,
            };

            Assert.IsTrue(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]

        public void ReflectionHelper_NotEqual_DateTimeOffset()
        {
            var dto = DateTimeOffset.Now;

            var a = new TestClass
            {
                DateTimeOffset = dto,
            };

            var b = new TestClass
            {
                DateTimeOffset = dto.AddDays(1),
            };

            Assert.IsFalse(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_NotEqual_NullableDateTimeOffset()
        {
            var dto = DateTimeOffset.Now;

            var a = new TestClass
            {
                NullableDateTimeOffset = dto.AddDays(1),
            };

            var b = new TestClass
            {
                NullableDateTimeOffset = dto,
            };

            Assert.IsFalse(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_Equal()
        {
            var a = new TestClass
            {
                String1 = "test",
                NullableInt = 5,
                Enum = TestClass.T.B,
                Complex = new TestClass.Nested { Int = 123 },
            };

            var b = new TestClass
            {
                String1 = "test",
                NullableInt = 5,
                Enum = TestClass.T.B,
                Complex = new TestClass.Nested { Int = 123 },
            };

            Assert.IsTrue(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_NotEqual()
        {
            var a = new TestClass
            {
                String1 = "test",
                NullableInt = 5,
                Enum = TestClass.T.B,
                Complex = new TestClass.Nested { Int = 123 },
                Children = new List<TestClass>(),
            };

            var b = new TestClass
            {
                String1 = "test",
                NullableInt = 6,
                Enum = TestClass.T.C,
                Complex = new TestClass.Nested { Int = 133 },
            };

            Assert.IsFalse(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_Equal_WithIgnore()
        {
            var a = new TestClass
            {
                String1 = "test",
                NullableInt = 5,
                Enum = TestClass.T.B,
                Complex = new TestClass.Nested { Int = 123 },
                Children = new List<TestClass>(),
            };

            var b = new TestClass
            {
                String1 = "test",
                NullableInt = 6,
                Enum = TestClass.T.C,
                Complex = new TestClass.Nested { Int = 133 },
            };

            var fields = new string[] { "NullableInt", "Enum", "Complex", "Children" };

            Assert.IsTrue(ReflectionHelper.AreObjectsEqual(a, b, fields));
        }

        [Test]
        public void ReflectionHelper_NotEqual_CaseName()
        {
            var a = new TestClass
            {
                String1 = "test",
            };

            var b = new TestClass
            {
                string1 = "test",
            };

            Assert.IsFalse(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_NotEqual_Enum()
        {
            var a = new TestClass
            {
                Enum = TestClass.T.A,
            };
            var b = new TestClass
            {
                Enum = TestClass.T.B,
            };

            Assert.IsFalse(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_NotEqual_NullableEnum()
        {
            var a = new TestClass
            {
                NullableEnum = TestClass.T.B,
            };
            var b = new TestClass
            {
                NullableEnum = TestClass.T.C,
            };

            Assert.IsFalse(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_NotEqual_NullableEnum2()
        {
            var a = new TestClass
            {
                NullableEnum = null,
            };

            var b = new TestClass
            {
                NullableEnum = TestClass.T.C,
            };

            Assert.IsFalse(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_NotEqual_CollectionMissing_Right()
        {
            var a = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>(),
            };
            var b = new TestClass
            {
                String1 = "test",
            };

            Assert.IsFalse(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_NotEqual_CollectionMissing_Left()
        {
            var a = new TestClass
            {
                String1 = "test",
            };

            var b = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>(),
            };

            Assert.IsFalse(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_Equal_CollectionMissing_BothEmpty()
        {
            var a = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>(),
            };

            var b = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>(),
            };

            Assert.IsTrue(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_NotEqual_Collection_LeftHasMore()
        {
            var a = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>
            {
                new TestClass(),
            },
            };

            var b = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>(),
            };

            Assert.IsFalse(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_NotEqual_Collection_RightHasMore()
        {
            var a = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>(),
            };

            var b = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>(),
            };
            b.Children.Add(new TestClass());

            Assert.IsFalse(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_Equal_Collection_EmptyClass()
        {
            var a = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>
                {
                    new TestClass(),
                },
            };

            var b = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>
                {
                    new TestClass(),
                },
            };

            Assert.IsTrue(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_Equal_Collection_OneItem_Int()
        {
            var a = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>
                {
                    new TestClass() { Int = 123, },
                },
            };

            var b = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>
                {
                    new TestClass() { Int = 123 },
                },
            };

            Assert.IsTrue(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_Equal_Collection_TwoItem()
        {
            var a = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>
                {
                    new TestClass() { Int = 123 },
                    new TestClass() { string1 = "asdfa" },
                },
            };

            var b = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>
                {
                    new TestClass() { Int = 123 },
                    new TestClass() { string1 = "asdfa" },
                },
            };

            Assert.IsTrue(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_NotEqual_Collection_DifferentOrder()
        {
            var a = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>
                {
                    new TestClass() { string1 = "asdfa" },
                    new TestClass() { Int = 123 },
                },
            };

            var b = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>
                {
                    new TestClass() { Int = 123 },
                    new TestClass() { string1 = "asdfa" },
                },
            };

            Assert.IsFalse(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_NotEqual_ComplexProperty_Left()
        {
            var a = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>
                {
                    new TestClass() { Int = 123 },
                },
                Complex = new TestClass.Nested(),
            };

            var b = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>
                {
                    new TestClass() { Int = 123 },
                },
            };

            Assert.IsFalse(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_NotEqual_ComplexProperty_Right()
        {
            var a = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>
                {
                    new TestClass() { Int = 123 },
                },
            };

            var b = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>
                {
                    new TestClass() { Int = 123 },
                },
                Complex = new TestClass.Nested(),
            };

            Assert.IsFalse(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_Equal_ComplexProperty_Empty()
        {
            var a = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>
                {
                    new TestClass() { Int = 123 },
                },
                Complex = new TestClass.Nested(),
            };

            var b = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>
                {
                    new TestClass() { Int = 123 },
                },
                Complex = new TestClass.Nested(),
            };

            Assert.IsTrue(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_Equal_ComplexProperty()
        {
            var a = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>
                {
                    new TestClass() { Int = 123, },
                },
                Complex = new TestClass.Nested() { Int = 33, String1 = "abc" },
            };

            var b = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>
                {
                    new TestClass() { Int = 123 },
                },
                Complex = new TestClass.Nested(),
            };
            b.Complex = new TestClass.Nested() { Int = 33, String1 = "abc" };

            Assert.IsTrue(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_NotEqual_ComplexProperty1()
        {
            var a = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>
                {
                    new TestClass() { Int = 123, },
                },
                Complex = new TestClass.Nested() { Int = 33, String1 = "abc" },
            };

            var b = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>
                {
                    new TestClass() { Int = 123, },
                },
                Complex = new TestClass.Nested(),
            };
            b.Complex = new TestClass.Nested() { Int = 55, String1 = "abc" };

            Assert.IsFalse(ReflectionHelper.AreObjectsEqual(a, b));
        }

        [Test]
        public void ReflectionHelper_NotEqual_ComplexProperty2()
        {
            var a = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>
                {
                    new TestClass() { Int = 123, },
                },
                Complex = new TestClass.Nested() { Int = 33, String1 = "efg" },
            };

            var b = new TestClass
            {
                String1 = "test",
                Children = new List<TestClass>(),
            };

            b.Children.Add(new TestClass() { Int = 123 });
            b.Complex = new TestClass.Nested();
            b.Complex = new TestClass.Nested() { Int = 33, String1 = "abc" };

            Assert.IsFalse(ReflectionHelper.AreObjectsEqual(a, b));
        }

        public class TestClass
        {
            public enum T
            {
                A,
                B,
                C,
            }

            public int Int { get; set; }

            public int? NullableInt { get; set; }

            public DateTimeOffset DateTimeOffset { get; set; }

            public DateTimeOffset? NullableDateTimeOffset { get; set; }

            public T Enum { get; set; }

            public T? NullableEnum { get; set; }

            public string String1 { get; set; }

#pragma warning disable SA1300,IDE1006 // Element must begin with upper-case letter
            public string string1 { get; set; }
#pragma warning restore SA1300, IDE1006 // Element must begin with upper-case letter

#pragma warning disable CA2227 // Collection properties should be read only
            public List<TestClass> Children { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

            public Nested Complex { get; set; }

            public class Nested
            {
                public string String1 { get; set; }

                public int Int { get; set; }
            }
        }
    }
}