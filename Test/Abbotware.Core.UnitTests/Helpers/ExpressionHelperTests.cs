namespace Abbotware.UnitTests.Core
{
    using System;
    using Abbotware.Core.Helpers;
    using NUnit.Framework;

    [TestFixture]
    [Category("Core")]
    [Category("Core.Helpers")]
    public class ExpressionHelperTests
    {
        [Test]
        public void TypeHelper_TestUsage()
        {
            var o = new Test { Foo = "abc" };

            var t = ExpressionHelper.GetPropertyExpression<Test, string>(x => x.Foo);

            Assert.That(t.PropertyInfo.Name, Is.EqualTo("Foo"));
            Assert.That(t.Compiled(o), Is.EqualTo("abc"));
        }

        [Test]
        public void TypeHelper_NotOnProperty()
        {
            Assert.Throws<ArgumentException>(() => ExpressionHelper.GetPropertyExpression<Test, string>(x => x.Bar()));
        }

        internal sealed class Test
        {
            public string Foo { get; set; }

            public string Bar() => this.ToString() ?? string.Empty;
        }
    }
}