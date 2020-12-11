// -----------------------------------------------------------------------
// <copyright file="NoCSharpType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Plugins
{
    using System;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Messaging.Integration.Configuration;
    using Abbotware.Core.Messaging.Integration.Configuration.Models;

    /// <summary>
    ///     Adds no type info to the message envelope
    /// </summary>
    public class NoCSharpType : ICSharpTypeEncoder
    {
        /// <inheritdoc />
        public void Encode(Type value, MessageEnvelope storage)
        {
        }

        /// <inheritdoc />
        public Type? Decode(IMessageEnvelope storage)
        {
            return null;
        }
    }
}