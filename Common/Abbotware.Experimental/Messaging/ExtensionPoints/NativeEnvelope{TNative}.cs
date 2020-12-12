// -----------------------------------------------------------------------
// <copyright file="NativeEnvelope{TNative}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.ExtensionPoints
{
    using Abbotware.Core.Messaging.Integration.Configuration.Models;

    /// <summary>
    /// Native message envelope
    /// </summary>
    /// <typeparam name="TNative">native envelope type</typeparam>
    public abstract class NativeEnvelope<TNative> : MessageEnvelope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NativeEnvelope{TNative}"/> class.
        /// </summary>
        /// <param name="nativeEnvelope">native envelope type</param>
        protected NativeEnvelope(TNative nativeEnvelope)
        {
            this.Native = nativeEnvelope;
        }

        /// <summary>
        /// Gets the native envelope type
        /// </summary>
        public TNative Native { get; }
    }
}