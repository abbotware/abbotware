// -----------------------------------------------------------------------
// <copyright file="IndeterminateStateException.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow
{
    /// <summary>
    /// Indeterminate State runtime exception
    /// </summary>
    public class IndeterminateStateException : RuntimeException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndeterminateStateException"/> class.
        /// </summary>
        /// <param name="o">external object</param>
        /// <param name="stateName">state name</param>
        /// <param name="workflowName">workflow name</param>
        public IndeterminateStateException(object o, string stateName, string workflowName)
            : base($"object:{o} is in an inconsistent/invalid state.  State:{stateName} not in state machine:{workflowName}")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndeterminateStateException"/> class.
        /// </summary>
        protected IndeterminateStateException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndeterminateStateException"/> class.
        /// </summary>
        /// <param name="message">message</param>
        protected IndeterminateStateException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndeterminateStateException"/> class.
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="innerException">inner exception</param>
        protected IndeterminateStateException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}