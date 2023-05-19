// -----------------------------------------------------------------------
// <copyright file="TypeHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Helpers
{
    using System.Linq;

    /// <summary>
    /// Type helper methods
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        /// Truncates a full type name to just namespace and class name
        /// </summary>
        /// <param name="fullName">fully qualified type name</param>
        /// <returns>namespace and class</returns>
        public static string NamespaceAndClass(string fullName)
        {
            fullName = Arguments.EnsureNotNull(fullName, nameof(fullName));

            var parts = fullName.Split(',');

            return string.Join(",", parts.Take(2));
        }
    }
}
