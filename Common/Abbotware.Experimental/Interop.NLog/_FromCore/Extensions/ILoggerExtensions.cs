// -----------------------------------------------------------------------
// <copyright file="ILoggerExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Extensions
{
    using System.Runtime.CompilerServices;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Logging.Plugins;

    /// <summary>
    /// Extensions for the ILogger interface
    /// </summary>
    public static class ILoggerExtensions
    {
        /// <summary>
        /// Creates a scoped logger that can be used in a using block
        /// </summary>
        /// <param name="logger">Extendee</param>
        /// <param name="scopeName">name of the scope</param>
        /// <param name="memberName">member name</param>
        /// <returns>scoped logger</returns>
        public static ITimingScope TimingScope(this ILogger logger, string scopeName, [CallerMemberName] string memberName = "")
        {
            var scope = new TimingScope(logger, scopeName, memberName);
            scope.Enter();
            return scope;
        }
    }
}
