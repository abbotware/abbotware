﻿// -----------------------------------------------------------------------
// <copyright file="AbbotwareException.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Exceptions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.Serialization;

    /// <summary>
    ///     Base Exception class for all Abbotware thrown exceptions
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "favoring exception string formatting over the convention")]
    [Serializable]
    public class AbbotwareException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AbbotwareException" /> class.
        /// </summary>
        public AbbotwareException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbbotwareException" /> class.
        /// </summary>
        /// <param name="message">exception message</param>
        public AbbotwareException(string message)
            : base(message)
        {
            Arguments.NotNullOrWhitespace(message, nameof(message));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbbotwareException" /> class.
        /// </summary>
        /// <param name="format">exception message</param>
        /// <param name="parameters">parameters for format string</param>
        public AbbotwareException(string format, params object[] parameters)
            : base(string.Format(CultureInfo.InvariantCulture, format, parameters))
        {
            Arguments.NotNullOrWhitespace(format, nameof(format));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbbotwareException" /> class.
        /// </summary>
        /// <param name="innerException">inner exception</param>
        /// <param name="format">format string for message</param>
        /// <param name="parameters">parameters for format string</param>
        public AbbotwareException(Exception innerException, string format, params object[] parameters)
            : base(string.Format(CultureInfo.InvariantCulture, format, parameters), innerException)
        {
            Arguments.NotNull(innerException, nameof(innerException));
            Arguments.NotNullOrWhitespace(format, nameof(format));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbbotwareException" /> class.
        /// </summary>
        /// <param name="info">serialization info</param>
        /// <param name="context">streaming context</param>
        [ExcludeFromCodeCoverage]
        protected AbbotwareException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Arguments.NotNull(info, nameof(info));
        }
    }
}