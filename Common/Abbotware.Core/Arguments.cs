// -----------------------------------------------------------------------
// <copyright file="Arguments.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using Abbotware.Core.Diagnostics;

    /// <summary>
    /// Collection of entry/guard methods for function arguments
    /// </summary>
    public static partial class Arguments
    {
        /// <summary>
        /// Throws ArgumentNullException if argument is the value
        /// </summary>
        /// <typeparam name="T">Argument type</typeparam>
        /// <param name="argument">argument value</param>
        /// <param name="value">value to check against</param>
        /// <param name="name">name of argument</param>
        /// <param name="method">name of method</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotValue<T>(T argument, T value, string name, [CallerMemberName] string? method = null)
            where T : IEquatable<T>
        {
            if (argument.Equals(value))
            {
                throw new ArgumentException($"value:{value} is not valid.  Method:{method}", name);
            }
        }

        /// <summary>
        /// Throws ArgumentException if string is null or whitespace
        /// </summary>
        /// <param name="argument">argument value</param>
        /// <param name="name">name of argument</param>
        /// <param name="message">message</param>
        /// <param name="method">name of method</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullOrWhitespace(string argument, string name, string? message = null, [CallerMemberName] string? method = null)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                var m = $"string is not valid:{message} Method:{method}";

                throw new ArgumentException(m, name);
            }
        }

        /// <summary>
        /// Throws InvalidOperationException if type is missing [Serializable] attribute
        /// </summary>
        /// <typeparam name="T">Argument type</typeparam>
        /// <param name="method">name of method</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsSerializable<T>([CallerMemberName] string? method = null)
        {
            var t = typeof(T);

            var a = ReflectionHelper.SingleOrDefaultAttribute<SerializableAttribute>(t);

            if (a is null)
            {
                throw new ArgumentException($"can not serialaize/deserialize '{t.FullName}'  missing [Serializable] attribute.  Method:{method}");
            }
        }

        /// <summary>
        /// Throws ArgumentException if IntPtr is zero
        /// </summary>
        /// <param name="argument">argument value</param>
        /// <param name="name">name of argument</param>
        /// <param name="method">name of method</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotZero(IntPtr argument, string name, [CallerMemberName] string? method = null)
        {
            if (argument == IntPtr.Zero)
            {
                throw new ArgumentException($"IntPtr is not valid.  Method:{method}", name);
            }
        }

        /// <summary>
        /// Throws ArgumentException if IntPtr is zero
        /// </summary>
        /// <param name="argument">argument value</param>
        /// <param name="name">name of argument</param>
        /// <param name="method">name of method</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsPositiveAndNotZero(TimeSpan argument, string name, [CallerMemberName] string? method = null)
        {
            if (argument.Ticks <= 0)
            {
                throw new ArgumentOutOfRangeException($"argument:{argument} is not greater than zero.  Method:{method}", name);
            }
        }

        /// <summary>
        /// Throws ArgumentException if IntPtr is zero
        /// </summary>
        /// <param name="argument">argument value</param>
        /// <param name="name">name of argument</param>
        /// <param name="method">name of method</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsPositiveAndNotZero(long argument, string name, [CallerMemberName] string? method = null)
        {
            if (argument <= 0)
            {
                throw new ArgumentOutOfRangeException($"argument:{argument} is not greater than zero.  Method:{method}", name);
            }
        }

        /// <summary>
        /// Throws ArgumentException if IntPtr is zero
        /// </summary>
        /// <param name="argument">argument value</param>
        /// <param name="name">name of argument</param>
        /// <param name="method">name of method</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsPositiveOrZero(long argument, string name, [CallerMemberName] string? method = null)
        {
            if (argument < 0)
            {
                throw new ArgumentOutOfRangeException($"argument:{argument} is not zero or positive.  Method:{method}", name);
            }
        }

        /// <summary>
        /// Throws ArgumentOutOfRangeException number is not with in the range
        /// </summary>
        /// <param name="argument">argument value</param>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <param name="name">name of argument</param>
        /// <param name="method">name of method</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Within(long argument, long min, long max, string name, [CallerMemberName] string? method = null)
        {
            var floor = global::System.Math.Min(max, min);

            var ceiling = global::System.Math.Max(max, min);

            if (argument > ceiling)
            {
                throw new ArgumentOutOfRangeException($"argment out of range:{min} - {max}.  Method:{method}", name);
            }

            if (argument < floor)
            {
                throw new ArgumentOutOfRangeException($"argment out of range:{min} - {max}.  Method:{method}", name);
            }
        }

        /// <summary>
        /// Throws ArgumentException if path is not rooted
        /// </summary>
        /// <param name="argument">argument value</param>
        /// <param name="name">name of argument</param>
        /// <param name="method">name of method</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FilePathIsRooted(string argument, string name, [CallerMemberName] string? method = null)
        {
            if (!Path.IsPathRooted(argument))
            {
                throw new ArgumentException($"{argument} is not valid file path.  Method:{method}", name);
            }
        }

        /// <summary>
        /// Verifies the types are the same
        /// </summary>
        /// <typeparam name="TGeneric">generic type</typeparam>
        /// <typeparam name="TConcrete">concrete type</typeparam>
        public static void TypesAreSame<TGeneric, TConcrete>()
        {
            var generic = typeof(TGeneric);
            var concrete = typeof(TConcrete);

            if (typeof(TGeneric) != typeof(TConcrete))
            {
                throw new NotSupportedException($"generic: {generic.Name} is not compatible with concrete:{concrete.Name}");
            }
        }
    }
}
