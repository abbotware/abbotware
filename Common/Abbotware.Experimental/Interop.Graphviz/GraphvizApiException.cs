// -----------------------------------------------------------------------
// <copyright file="GraphvizApiException.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Graphviz
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception type for indicating failures in the Graphviz API calls
    /// </summary>
    [Serializable]
    public class GraphvizApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphvizApiException"/> class.
        /// </summary>
        public GraphvizApiException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphvizApiException"/> class.
        /// </summary>
        /// <param name="message">error message</param>
        public GraphvizApiException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphvizApiException"/> class.
        /// </summary>
        /// <param name="message">error message</param>
        /// <param name="innerException">inner exception</param>
        public GraphvizApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphvizApiException"/> class.
        /// </summary>
        /// <param name="info">Serialization Info</param>
        /// <param name="context">Streaming Context</param>
        protected GraphvizApiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}