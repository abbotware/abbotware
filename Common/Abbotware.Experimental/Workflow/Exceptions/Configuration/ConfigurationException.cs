// -----------------------------------------------------------------------
// <copyright file="ConfigurationException.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow
{
    using System;

    /// <summary>
    /// Base Exception for a Workflow Configuration Error
    /// </summary>
    public abstract class ConfigurationException : WorkflowException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationException"/> class.
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="inner">inner exception</param>
        protected ConfigurationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationException"/> class.
        /// </summary>
        /// <param name="message">message</param>
        protected ConfigurationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationException"/> class.
        /// </summary>
        protected ConfigurationException()
        {
        }
    }
}