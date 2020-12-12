// -----------------------------------------------------------------------
// <copyright file="XmlProtocol.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Amqp.Plugins
{
    using Abbotware.Core;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Messaging.Integration.Amqp;
    using Abbotware.Core.Messaging.Integration.Plugins;
    using Abbotware.Core.Serialization.Plugins;

    /// <summary>
    ///     Protocol using XmlSerializer and Type Info encoding
    /// </summary>
    public class XmlProtocol : BaseAmqpProtocol
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="XmlProtocol" /> class.
        /// </summary>
        public XmlProtocol()
            : this(new AssemblyQualifiedNameInHeader())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="XmlProtocol" /> class.
        /// </summary>
        /// <param name="typeEncoder">type info encoder </param>
        public XmlProtocol(ICSharpTypeEncoder typeEncoder)
            : base(new XmlSerializer(), typeEncoder)
        {
            Arguments.NotNull(typeEncoder, nameof(typeEncoder));
        }
    }
}