// -----------------------------------------------------------------------
// <copyright file="Arguments.netstandard2.1.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2019. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
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
        public static void NotNull<T>([NotNull]T argument, string name, [CallerMemberName] string? method = null)
        {
            throw new NotImplementedException("this function should not be reached on netstandard 2.1");
        }

        /// <summary>
        /// Throws ArgumentNullException if an argument is null, returns value unchanged otherwise
        /// </summary>
        /// <typeparam name="T">Argument type</typeparam>
        /// <param name="argument">argument value</param>
        /// <param name="name">name of argument</param>
        /// <param name="method">name of method</param>
        /// <returns>argument unchanged</returns>
        [return: NotNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T EnsureNotNull<T>(T argument, string name, [CallerMemberName] string? method = null)
                where T : class
        {
            return argument ?? throw new ArgumentNullException(name, $"Method:{method}");
        }
    }
}
