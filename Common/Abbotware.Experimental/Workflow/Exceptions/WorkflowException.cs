// -----------------------------------------------------------------------
// <copyright file="WorkflowException.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow
{
    using System;

    /// <summary>
    /// Workflow Exception type
    /// </summary>
    public abstract class WorkflowException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowException"/> class.
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="inner">inner exception</param>
        protected WorkflowException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowException"/> class.
        /// </summary>
        /// <param name="message">message</param>
        protected WorkflowException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowException"/> class.
        /// </summary>
        protected WorkflowException()
        {
        }
    }
}