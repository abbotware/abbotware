// -----------------------------------------------------------------------
// <copyright file="IAssert.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Utility.UnitTest
{
    /// <summary>
    /// interface for unit test assert methods
    /// </summary>
    public interface IAssert
    {
        /// <summary>
        /// Assert Test is Inconclusive
        /// </summary>
        /// <param name="message">message</param>
        void AssertInconclusive(string message);

        /// <summary>
        /// Assert Test failue
        /// </summary>
        /// <param name="message">message</param>
        void AssertFail(string message);

        /// <summary>
        /// Assert Equal
        /// </summary>
        /// <param name="left">left</param>
        /// <param name="right">right</param>
        void AssertEqual(object? left, object? right);
    }
}
