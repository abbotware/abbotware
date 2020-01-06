// -----------------------------------------------------------------------
// <copyright file="AssemblyHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Helpers
{
    using System.Reflection;

    /// <summary>
    /// Static Assembly helper methods
    /// </summary>
    public static class AssemblyHelper
    {
        /// <summary>
        /// Gets the Executing Assembly Version
        /// </summary>
        /// <returns>version</returns>
        public static string GetExecutingAssemblyVersion()
        {
            return Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString() ?? string.Empty;
        }
    }
}