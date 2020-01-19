// -----------------------------------------------------------------------
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
        /// <param name="message">exception message</param>
        /// <param name="innerException">inner exception</param>
        public AbbotwareException(string message, Exception innerException)
            : base(message, innerException)
        {
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

        /// <summary>
        ///     create a new <see cref="AbbotwareException" /> and build the string format message
        /// </summary>
        /// <param name="format">exception message</param>
        /// <param name="parameters">parameters for format string</param>
        /// <returns>exception with message formatted</returns>
        public static AbbotwareException Create(string format, params object[] parameters)
        {
            var message = string.Format(CultureInfo.InvariantCulture, format, parameters);

            return new AbbotwareException(message);
        }

        /// <summary>
        ///     create a new <see cref="AbbotwareException" /> and build the string format message
        /// </summary>
        /// <param name="innerException">inner exception</param>
        /// <param name="format">format string for message</param>
        /// <param name="parameters">parameters for format string</param>
        /// <returns>exception with message formatted</returns>
        public static AbbotwareException Create(Exception innerException, string format, params object[] parameters)
        {
            Arguments.NotNull(innerException, nameof(innerException));
            Arguments.NotNullOrWhitespace(format, nameof(format));

            var message = string.Format(CultureInfo.InvariantCulture, format, parameters);

            return new AbbotwareException(message, innerException);
        }
    }
}