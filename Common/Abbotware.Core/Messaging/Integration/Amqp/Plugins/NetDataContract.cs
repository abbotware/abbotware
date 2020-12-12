// -----------------------------------------------------------------------
// <copyright file="NetDataContract.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Messaging.Integration.Amqp.Plugins
{
    using Abbotware.Core.Messaging.Amqp.ExtensionPoints;
    using Abbotware.Core.Messaging.Plugins.Protocol;
    using Abbotware.Core.Plugins.Serialization;

    /// <summary>
    ///     Protocol using NetDataContractSerializer and Type Info encoding
    /// </summary>
    public class NetDataContract : BaseAmqpProtocol
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NetDataContract" /> class.
        /// </summary>
        public NetDataContract()
            : base(new NetDataContractSerializer())
        {
        }
    }
}