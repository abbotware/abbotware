// -----------------------------------------------------------------------
// <copyright file="AssemblyQualifiedNameInTopic.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.Plugins
{
    using System;
    using Abbotware.Core;
    using Abbotware.Core.Exceptions;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Messaging.Integration.Configuration;
    using Abbotware.Core.Messaging.Integration.Configuration.Models;

    /// <summary>
    ///     Encodes Type Info into the message topic
    /// </summary>
    public class AssemblyQualifiedNameInTopic : ICSharpTypeEncoder
    {
        /// <inheritdoc />
        public Type Decode(IMessageEnvelope storage)
        {
            storage = Arguments.EnsureNotNull(storage, nameof(storage));

            if (string.IsNullOrWhiteSpace(storage.PublishProperties.RoutingKey))
            {
                throw new AbbotwareException("Topic is empty, can't resolve type info");
            }

            var typeName = storage.PublishProperties.RoutingKey;

            var type = Type.GetType(typeName, false);

            if (type == null)
            {
                throw AbbotwareException.Create("unable to load assembly containing type:{0}", typeName);
            }

            return type;
        }

        /// <inheritdoc />
        public void Encode(Type value, MessageEnvelope storage)
        {
            value = Arguments.EnsureNotNull(value, nameof(value));
            storage = Arguments.EnsureNotNull(storage, nameof(storage));

            storage.PublishProperties.RoutingKey = value.AssemblyQualifiedName ?? string.Empty;
        }
    }
}