// -----------------------------------------------------------------------
// <copyright file="YieldTarget.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Threading
{
    /// <summary>
    ///     Yield Type
    /// </summary>
    public enum YieldTarget
    {
        /// <summary>
        ///     Operating system will decide when to interrupt the thread
        /// </summary>
        None,

        /// <summary>
        ///     Yield time slice to any other thread on any processor
        /// </summary>
        AnyThreadOnAnyProcessor,

        /// <summary>
        ///     Yield time slice to other thread of same or higher piority on any processor
        /// </summary>
        SameOrHigherPriorityThreadOnAnyProcessor,

        /// <summary>
        ///     Yield time slice to any other thread on same processor
        /// </summary>
        AnyThreadOnSameProcessor,
    }
}