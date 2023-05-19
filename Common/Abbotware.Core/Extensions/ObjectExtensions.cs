// -----------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Extensions
{
#if !NETSTANDARD2_0
    using System.Diagnostics.CodeAnalysis;
#endif
    using System.Runtime.CompilerServices;
    using Abbotware.Core.Diagnostics;

    /// <summary>
    ///     Object Helper methods
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        ///     Dumps an object's contents to a string
        /// </summary>
        /// <param name="extended">object to dump</param>
        /// <returns>object's contents</returns>
        public static string Dump(this object extended)
        {
            Arguments.NotNull(extended, nameof(extended));

            return extended.Dump(4);
        }

        /// <summary>
        ///     Dumps an object's contents to a string
        /// </summary>
        /// <param name="extended">object to dump</param>
        /// <param name="maxDepth">max depth to walk</param>
        /// <returns>object's contents</returns>
        public static string Dump(this object extended, ushort maxDepth)
        {
            Arguments.NotNull(extended, nameof(extended));

            var context = new DumperContext(maxDepth);

            DumpHelper.Write(extended, context);

            return context.ToString();
        }

        /// <summary>
        ///     Used when overriding the Equals() to provide implementation for common trivial equality checks
        /// </summary>
        /// <typeparam name="TObject">type of object</typeparam>
        /// <param name="extended">left hand side of equality check</param>
        /// <param name="right">right hand size of equality check</param>
        /// <param name="output">object casted</param>
        /// <returns>false if the trival equal cases, true if need to perform an</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETSTANDARD2_0
        public static bool StructPossiblyEquals<TObject>(this object extended, object? right, out TObject? output)
#else
        public static bool StructPossiblyEquals<TObject>(this object extended, object? right, [NotNullWhen(true)] out TObject? output)
#endif
            where TObject : struct
        {
            var left = Arguments.EnsureNotNull(extended, nameof(extended));

            output = null;

            if (!PossiblyEquals(left, right))
            {
                return false;
            }

            output = (TObject?)right;

            return true;
        }

        /// <summary>
        ///     Used when overriding the Equals() to provide implementation for common trivial equality checks
        /// </summary>
        /// <typeparam name="TObject">type of object</typeparam>
        /// <param name="extended">left hand side of equality check</param>
        /// <param name="right">right hand size of equality check</param>
        /// <param name="output">object casted</param>
        /// <returns>false if the trival equal cases, true if need to perform an</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETSTANDARD2_0
        public static bool ClassPossiblyEquals<TObject>(this object extended, object? right, out TObject? output)
#else
        public static bool ClassPossiblyEquals<TObject>(this object extended, object? right, [NotNullWhen(true)] out TObject? output)
#endif

            where TObject : class
        {
            var left = Arguments.EnsureNotNull(extended, nameof(extended));

            output = null;

            if (!PossiblyEquals(left, right))
            {
                return false;
            }

            output = (TObject)right!;

            return true;
        }

        /// <summary>
        ///     Used when overriding the Equals() to provide implementation for common trivial equality checks
        /// </summary>
        /// <param name="left">left hand side of equality check</param>
        /// <param name="right">right hand size of equality check</param>
        /// <returns>false if the trival equal cases, true if need to perform an</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETSTANDARD2_0
        private static bool PossiblyEquals(object left, object? right)
#else
        private static bool PossiblyEquals(object left, [NotNullWhen(true)] object? right)
#endif
        {
            var l = Arguments.EnsureNotNull(left, nameof(left));

            if (right == null)
            {
                return false;
            }

            if (l.GetType() != right.GetType())
            {
                return false;
            }

            if (l.GetHashCode() != right.GetHashCode())
            {
                return false;
            }

            return true;
        }
    }
}