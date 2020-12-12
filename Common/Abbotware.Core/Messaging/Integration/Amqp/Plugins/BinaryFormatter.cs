// -----------------------------------------------------------------------
// <copyright file="BinaryFormatter.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Amqp.Plugins
{
    using Abbotware.Core.Messaging.Integration.Amqp;
    using Abbotware.Core.Serialization.Plugins;

    /// <summary>
    ///     protocol using the .Net BinaryFormatter
    /// </summary>
    public class BinaryFormatter : BaseAmqpProtocol
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BinaryFormatter" /> class.
        /// </summary>
        public BinaryFormatter()
            : base(new BinaryFormatterSerializer())
        {
        }
    }
}