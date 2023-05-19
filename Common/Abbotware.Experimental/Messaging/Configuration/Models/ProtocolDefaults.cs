// -----------------------------------------------------------------------
// <copyright file="ProtocolDefaults.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Configuration.Models
{
    /// <summary>
    ///     class containing message queue protocol default values
    /// </summary>
    public class ProtocolDefaults : IProtocolDefaults
    {
        /// <inheritdoc/>
        public string DefaultExchange { get; set; } = string.Empty;
    }
}