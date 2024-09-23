// -----------------------------------------------------------------------
// <copyright file="LegacyAssert.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.NUnit
{
    using global::NUnit.Framework;

    /// <summary>
    /// Wrappers for legacy assert methods
    /// </summary>
    public static class LegacyAssert
    {
        /// <summary>
        /// Legacy Assert method
        /// </summary>
        /// <param name="value">value to check</param>
        public static void IsFalse(bool @value)
        {
            Assert.That(@value, Is.False);
        }

        /// <summary>
        /// Legacy Assert method
        /// </summary>
        /// <param name="value">value to check</param>
        public static void IsTrue(bool @value)
        {
            Assert.That(@value, Is.True);
        }

        /// <summary>
        /// Legacy Assert method
        /// </summary>
        /// <param name="value">object t1o check</param>
        public static void IsNull(object? @value)
        {
            Assert.That(value, Is.Null);
        }

        /// <summary>
        /// Legacy Assert method
        /// </summary>
        /// <param name="value">object t1o check</param>
        public static void IsNotNull(object? @value)
        {
            Assert.That(value, Is.Not.Null);
        }

        /// <summary>
        /// Legacy Assert Method
        /// </summary>
        /// <param name="left">left side</param>
        /// <param name="right">right side</param>
        /// <param name="message">optional message</param>
        public static void AreEqual(object? left, object? right, string? message = null)
        {
            Assert.That(left, Is.EqualTo(right), message);
        }

        /// <summary>
        /// Legacy Assert Method
        /// </summary>
        /// <param name="left">left side</param>
        /// <param name="right">right side</param>
        public static void AreSame(object? left, object? right)
        {
            Assert.That(left, Is.SameAs(right));
        }
    }
}
