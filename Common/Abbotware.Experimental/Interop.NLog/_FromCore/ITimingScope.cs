// -----------------------------------------------------------------------
// <copyright file="ITimingScope.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Logging
{
    using System;

    /// <summary>
    /// interface used for "using" statement logging
    /// </summary>
    public interface ITimingScope : IDisposable
    {
        /// <summary>
        /// Gets the scope name
        /// </summary>
        string ScopeName { get; }

        /// <summary>
        /// Gets the Calling member name
        /// </summary>
        string MemberName { get; }

        /// <summary>
        /// Gets the elapsed time
        /// </summary>
        TimeSpan Elapsed { get; }
    }
}