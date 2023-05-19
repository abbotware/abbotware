// -----------------------------------------------------------------------
// <copyright file="DataContract.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Amqp.Plugins
{
    using Abbotware.Core.Messaging.Integration.Amqp;
    using Abbotware.Core.Messaging.Integration.Plugins;
    using Abbotware.Core.Serialization.Plugins;

    /// <summary>
    ///     Protocol using DataContractSerializer and TypeInfo encoding
    /// </summary>
    public class DataContract : BaseAmqpProtocol
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DataContract" /> class.
        /// </summary>
        public DataContract()
            : base(new DataContractSerializer(), new AssemblyQualifiedNameInHeader())
        {
        }
    }
}