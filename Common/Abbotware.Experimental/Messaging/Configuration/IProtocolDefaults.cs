// -----------------------------------------------------------------------
// <copyright file="IProtocolDefaults.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Configuration
{
    /// <summary>
    ///     read only interface containing message queue protocol default values
    /// </summary>
    public interface IProtocolDefaults
    {
        /// <summary>
        ///     Gets the default exchange name for the protocol
        /// </summary>
        string DefaultExchange { get; }
    }
}