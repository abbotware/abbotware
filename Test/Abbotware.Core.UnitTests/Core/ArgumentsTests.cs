namespace Abbotware.UnitTests.Core
{
    using System;
    using Abbotware.Core;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [Category("Core")]
    [Category("Core.Arguments")]
    [TestFixture]
    public class ArgumentsTests : BaseNUnitTest
    {
        [Test]
        public void NotNull_ThrowsOnNull()
        {
            object value = null;

            void Execute() => Arguments.NotNull(value, nameof(value));

            Assert.DoesNotThrow(Execute);
        }

        [Test]
        public void NotNull_NoThrow()
        {
            object value = new object();

            void Execute() => Arguments.NotNull(value, nameof(value));

            Assert.DoesNotThrow(Execute);
        }

        [Test]
        public void EnsureNotNull_ThrowsOnNull()
        {
            object value = null;

            void Execute() => Arguments.EnsureNotNull(value, nameof(value));

            Assert.Throws<ArgumentNullException>(Execute);
        }

        [Test]
        public void EnsureNotNull_ReturnOnNotNull()
        {
            object value = new object();

            object returned = Arguments.EnsureNotNull(value, nameof(value));

            Assert.That(returned, Is.SameAs(value));
        }

        [Test]
        public void NotNullOrWhitespace_Throws()
        {
            string value = " ";

            void Execute() => Arguments.NotNullOrWhitespace(value, nameof(value));

            Assert.Throws<ArgumentException>(Execute);
        }

        [Test]
        public void NotNullOrWhitespace_Throws_WithMessage()
        {
            string value = " ";

            void Execute() => Arguments.NotNullOrWhitespace(value, nameof(value), "message about string");

            var ex = Assert.Throws<ArgumentException>(Execute);

#if NETCOREAPP3_1
            Assert.That(ex.Message, Is.EqualTo($"string is not valid:message about string Method:NotNullOrWhitespace_Throws_WithMessage (Parameter 'value')"));
#else
            Assert.That(ex.Message, Is.EqualTo($"string is not valid:message about string Method:NotNullOrWhitespace_Throws_WithMessage{Environment.NewLine}Parameter name: value"));
#endif
        }

        [Test]
        public void NotNullOrWhitespace_NoThrow()
        {
            string value = "asdf";

            void Execute() => Arguments.NotNullOrWhitespace(value, nameof(value));

            Assert.DoesNotThrow(Execute);
        }

        [Test]
        public void NotValue_Throws()
        {
            int value = 4;

            void Execute() => Arguments.NotValue<int>(value, 4, nameof(value));

            Assert.Throws<ArgumentException>(Execute);
        }

        [Test]
        public void NotValue_NoThrows()
        {
            int value = 4;

            void Execute() => Arguments.NotValue<int>(value, 3, nameof(value));

            Assert.DoesNotThrow(Execute);
        }

        [Test]
        public void NotZero_Throws()
        {
            IntPtr value = IntPtr.Zero;

            void Execute() => Arguments.NotZero(value, nameof(value));

            Assert.Throws<ArgumentException>(Execute);
        }

        [Test]
        public void NotZero_NoThrows()
        {
            IntPtr value = new IntPtr(123);

            void Execute() => Arguments.NotZero(value, nameof(value));

            Assert.DoesNotThrow(Execute);
        }

        [Test]
        public void IsPositive_Throws()
        {
            {
                TimeSpan value = new TimeSpan(0);

                void Execute() => Arguments.IsPositive(value, nameof(value));

                Assert.Throws<ArgumentOutOfRangeException>(Execute);
            }

            {
                TimeSpan value = new TimeSpan(-123);

                void Execute() => Arguments.IsPositive(value, nameof(value));

                Assert.Throws<ArgumentOutOfRangeException>(Execute);
            }

            {
                long value = 0;

                void Execute() => Arguments.IsPositive(value, nameof(value));

                Assert.Throws<ArgumentOutOfRangeException>(Execute);
            }

            {
                long value = -123;

                void Execute() => Arguments.IsPositive(value, nameof(value));

                Assert.Throws<ArgumentOutOfRangeException>(Execute);
            }
        }

        [Test]
        public void IsPositive_NoThrows()
        {
            {
                TimeSpan value = new TimeSpan(123);

                void Execute() => Arguments.IsPositive(value, nameof(value));

                Assert.DoesNotThrow(Execute);
            }

            {
                long value = 123;

                void Execute() => Arguments.IsPositive(value, nameof(value));

                Assert.DoesNotThrow(Execute);
            }
        }

        [Test]
        public void IsPositiveOrZero_Throws()
        {
            {
                int value = 0;

                void Execute() => Arguments.IsPositiveOrZero(value, nameof(value));

                Assert.Throws<ArgumentOutOfRangeException>(Execute);
            }

            {
                int value = 24;

                void Execute() => Arguments.IsPositiveOrZero(value, nameof(value));

                Assert.Throws<ArgumentOutOfRangeException>(Execute);
            }
        }

        [Test]
        public void IsPositiveOrZero_NoThrows()
        {
            int value = -1;

            void Execute() => Arguments.IsPositiveOrZero(value, nameof(value));

            Assert.DoesNotThrow(Execute);
        }

        [Test]
        [Category("windows")]
        public void FilePathIsRooted_NoThrow_WindowsOnly()
        {
            this.SkipTestOnLinux();
            {
                string value = "C:\\Test\\asdf";

                void Execute() => Arguments.FilePathIsRooted(value, nameof(value));

                Assert.DoesNotThrow(Execute);
            }

            {
                string value = "\\Test\asdf";

                void Execute() => Arguments.FilePathIsRooted(value, nameof(value));

                Assert.DoesNotThrow(Execute);
            }
        }

        [Test]
        public void FilePathIsRooted_NoThrow()
        {
            string value = "/Test/asdf";

            void Execute() => Arguments.FilePathIsRooted(value, nameof(value));

            Assert.DoesNotThrow(Execute);
        }

        [Test]
        public void FilePathIsRooted_Throws()
        {
            {
                string value = "Test\\asdf";

                void Execute() => Arguments.FilePathIsRooted(value, nameof(value));

                Assert.Throws<ArgumentException>(Execute);
            }

            {
                string value = "asdf.asd";

                void Execute() => Arguments.FilePathIsRooted(value, nameof(value));

                Assert.Throws<ArgumentException>(Execute);
            }
        }

        [Test]
        public void Within_Throws()
        {
            {
                long value = 1234;

                void Execute() => Arguments.Within(value, 0, 4, nameof(value));

                Assert.Throws<ArgumentOutOfRangeException>(Execute);
            }

            {
                long value = -1234;

                void Execute() => Arguments.Within(value, 0, 4, nameof(value));

                Assert.Throws<ArgumentOutOfRangeException>(Execute);
            }
        }

        [Test]
        public void Within_NoThrows()
        {
            long value = 1234;

            void Execute() => Arguments.Within(value, 0, 4000, nameof(value));

            Assert.DoesNotThrow(Execute);
        }
    }
}