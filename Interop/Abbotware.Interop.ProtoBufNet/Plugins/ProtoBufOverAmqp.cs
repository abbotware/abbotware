// -----------------------------------------------------------------------
// <copyright file="ProtoBufOverAmqp.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.ProtoBufNet.Plugins
{
    using Abbotware.Core;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Messaging.Integration.Amqp;
    using Abbotware.Core.Messaging.Integration.Plugins;

    /// <summary>
    ///     Protocol using XmlSerializer and Type Info encoding
    /// </summary>
    public class ProtoBufOverAmqp : BaseAmqpProtocol
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ProtoBufOverAmqp" /> class.
        /// </summary>
        public ProtoBufOverAmqp()
            : this(new NoCSharpType())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProtoBufOverAmqp" /> class.
        /// </summary>
        /// <param name="typeEncoder">type info encoder </param>
        public ProtoBufOverAmqp(ICSharpTypeEncoder typeEncoder)
            : base(new ProtoBufSerializer(), typeEncoder)
        {
            Arguments.NotNull(typeEncoder, nameof(typeEncoder));
        }
    }
}