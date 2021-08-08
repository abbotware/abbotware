// -----------------------------------------------------------------------
// <copyright file="AssemblyQualifiedNameInHeader.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Plugins
{
    using System;
    using System.Text;
    using Abbotware.Core;
    using Abbotware.Core.Exceptions;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Messaging.Integration.Configuration;
    using Abbotware.Core.Messaging.Integration.Configuration.Models;

    /// <summary>
    ///     Encodes class type info using AMQP headers
    /// </summary>
    public class AssemblyQualifiedNameInHeader : ICSharpTypeEncoder
    {
        /// <inheritdoc />
        public void Encode(Type value, MessageEnvelope storage)
        {
            storage = Arguments.EnsureNotNull(storage, nameof(storage));
            value = Arguments.EnsureNotNull(value, nameof(value));

            storage.Headers["AssemblyQualifiedName"] = value.AssemblyQualifiedName!;
        }

        /// <inheritdoc />
        public Type Decode(IMessageEnvelope storage)
        {
            storage = Arguments.EnsureNotNull(storage, nameof(storage));

            var header = storage.Headers["AssemblyQualifiedName"];

            string aqn;

            if (header is byte[] headerBytes)
            {
                aqn = Encoding.ASCII.GetString(headerBytes);
            }
            else
            {
                aqn = (string)header;
            }

            if (aqn == null)
            {
                throw new AbbotwareException("Message header is missing Assembly Qualified Name");
            }

            var type = Type.GetType(aqn, false);

            if (type == null)
            {
                throw AbbotwareException.Create("unable to load assembly containing type info:{0}", aqn);
            }

            return type;
        }
    }
}