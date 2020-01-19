// -----------------------------------------------------------------------
// <copyright file="Arguments.netstandard2.0.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Collection of entry/guard methods for function arguments
    /// </summary>
    public static partial class Arguments
    {
        /// <summary>
        /// Throws ArgumentNullException if argument is null
        /// </summary>
        /// <typeparam name="T">Argument type</typeparam>
        /// <param name="argument">argument value</param>
        /// <param name="name">name of argument</param>
        /// <param name="method">name of method</param>
        [Conditional("NETSTANDARD2_0")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNull<T>(T argument, string name, [CallerMemberName] string? method = null)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(name, $"Method:{method}");
            }
        }

        /// <summary>
        /// Throws ArgumentNullException if an argument is null, returns value unchanged otherwise
        /// </summary>
        /// <typeparam name="T">Argument type</typeparam>
        /// <param name="argument">argument value</param>
        /// <param name="name">name of argument</param>
        /// <param name="method">name of method</param>
        /// <returns>argument unchanged</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T EnsureNotNull<T>(T argument, string name, [CallerMemberName] string? method = null)
                where T : class
        {
            return argument ?? throw new ArgumentNullException(name, $"Method:{method}");
        }

        /// <summary>
        /// Throws ArgumentException if string is null or whitespace
        /// </summary>
        /// <param name="argument">argument value</param>
        /// <param name="name">name of argument</param>
        /// <param name="message">message</param>
        /// <param name="method">name of method</param>
        /// <returns>argument unchanged</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string EnsureNotNullOrWhitespace(string argument, string name, string? message = null, [CallerMemberName] string? method = null)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                var m = $"string is not valid:{message} Method:{method}";

                throw new ArgumentException(m, name);
            }

            return argument;
        }
    }
}
