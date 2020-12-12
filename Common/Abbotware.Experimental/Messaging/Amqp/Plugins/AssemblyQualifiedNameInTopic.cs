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
        public Type Decode(IMessageEnvelope envelope)
        {
            Arguments.NotNull(envelope, nameof(envelope));

#pragma warning disable CA1062 // Validate arguments of public methods
            if (string.IsNullOrWhiteSpace(envelope.PublishProperties.RoutingKey))
#pragma warning restore CA1062 // Validate arguments of public methods
            {
                throw new AbbotwareException("Topic is empty, can't resolve type info");
            }

            var typeName = envelope.PublishProperties.RoutingKey;

            var type = Type.GetType(typeName, false);

            if (type == null)
            {
                throw AbbotwareException.Create("unable to load assembly containing type:{0}", typeName);
            }

            return type;
        }

        /// <inheritdoc />
        public void Encode(Type type, MessageEnvelope envelope)
        {
            Arguments.NotNull(type, nameof(type));
            Arguments.NotNull(envelope, nameof(envelope));

#pragma warning disable CA1062 // Validate arguments of public methods
            envelope.PublishProperties.RoutingKey = type.AssemblyQualifiedName;
#pragma warning restore CA1062 // Validate arguments of public methods
        }
    }
}