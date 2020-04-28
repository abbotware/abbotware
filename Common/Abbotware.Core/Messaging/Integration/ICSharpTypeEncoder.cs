// -----------------------------------------------------------------------
// <copyright file="ICSharpTypeEncoder.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration
{
    using System;
    using Abbotware.Core.Messaging.Integration.Configuration;
    using Abbotware.Core.Messaging.Integration.Configuration.Models;
    using Abbotware.Core.Serialization;

    /// <summary>
    ///     Interface for encoding the C# type information into the message envelope
    /// </summary>
    public interface ICSharpTypeEncoder : IDecode<Type?, IMessageEnvelope>, IEncodeInto<Type, MessageEnvelope>
    {
    }
}