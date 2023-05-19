// -----------------------------------------------------------------------
// <copyright file="IConnection.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Objects
{
    using System;

    /// <summary>
    ///     Interface for encapsulating a connection
    /// </summary>
    public interface IConnection : IDisposable
    {
        /// <summary>
        ///     Gets a value indicating whether or not the connection is open
        /// </summary>
        bool IsOpen { get; }
   }
}