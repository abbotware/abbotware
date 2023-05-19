// -----------------------------------------------------------------------
// <copyright file="ConfigurationNotFoundException.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    ///     Exception indicating the configuration was not loaded
    /// </summary>
    [Serializable]
    public class ConfigurationNotFoundException : AbbotwareException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigurationNotFoundException" /> class.
        /// </summary>
        public ConfigurationNotFoundException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigurationNotFoundException" /> class.
        /// </summary>
        /// <param name="message">message for exception</param>
        public ConfigurationNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigurationNotFoundException" /> class.
        /// </summary>
        /// <param name="message">message for exception</param>
        /// <param name="innerException">inner exception</param>
        public ConfigurationNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigurationNotFoundException" /> class.
        /// </summary>
        /// <param name="info">serialization info</param>
        /// <param name="context">streaming conted</param>
        protected ConfigurationNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}