// -----------------------------------------------------------------------
// <copyright file="RuntimeException.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>
namespace Abbotware.Core.Workflow
{
    using System;

    /// <summary>
    /// Base Exception for a Workflow Runtime Error
    /// </summary>
    public class RuntimeException : WorkflowException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeException"/> class.
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="inner">inner exception</param>
        public RuntimeException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeException"/> class.
        /// </summary>
        /// <param name="message">message</param>
        public RuntimeException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeException"/> class.
        /// </summary>
        protected RuntimeException()
        {
        }
    }
}