// -----------------------------------------------------------------------
// <copyright file="Strings.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Amqp.Plugins
{
    using System.Text;
    using Abbotware.Core.Messaging.Integration.Amqp;
    using Abbotware.Core.Plugins.Serialization;

    /// <summary>
    ///     Binary protocol that performs no data alterations
    /// </summary>
    public class Strings : BaseAmqpProtocol<string>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Strings" /> class.
        /// </summary>
        public Strings()
            : this(new UTF8Encoding())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Strings" /> class.
        /// </summary>
        /// <param name="encoding">type of character encoding to use</param>
        public Strings(Encoding encoding)
            : base(new StringEncoder(encoding))
        {
            Arguments.NotNull(encoding, nameof(encoding));
        }
    }
}