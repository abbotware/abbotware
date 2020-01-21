// -----------------------------------------------------------------------
// <copyright file="ConsoleArguments.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Class that contains the command line arguments
    /// </summary>
    public class ConsoleArguments
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ConsoleArguments" /> class.
        /// </summary>
        /// <param name="arguments">command line arguments</param>
        public ConsoleArguments(IReadOnlyCollection<string> arguments)
        {
            Abbotware.Core.Arguments.NotNull(arguments, nameof(arguments));

            this.Arguments = arguments;
        }

        /// <summary>
        ///     Gets the list of arguments
        /// </summary>
        public IReadOnlyCollection<string> Arguments { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            var s = string.Join(" ", this.Arguments);

            return FormattableString.Invariant($"[{s}]");
        }
    }
}