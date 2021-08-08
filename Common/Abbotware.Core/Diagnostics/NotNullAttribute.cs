// -----------------------------------------------------------------------
// <copyright file="NotNullAttribute.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
#if NETSTANDARD2_0
namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// Place Holder for missing attribute in NetStandard2.0
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
    public sealed class NotNullAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotNullAttribute"/> class.
        /// </summary>
        public NotNullAttribute()
        {
        }
    }
}
#endif