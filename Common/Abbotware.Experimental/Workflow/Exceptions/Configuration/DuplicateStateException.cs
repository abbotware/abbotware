// -----------------------------------------------------------------------
// <copyright file="DuplicateStateException.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>
namespace Abbotware.Core.Workflow
{
    /// <summary>
    /// Duplciate state configuration exception
    /// </summary>
    public class DuplicateStateException : ConfigurationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateStateException"/> class.
        /// </summary>
        /// <param name="state">state</param>
        public DuplicateStateException(IState state)
            : base($"State:{state?.Name} Id:{state?.Id} Already exists")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateStateException"/> class.
        /// </summary>
        /// <param name="stateName">state name</param>
        public DuplicateStateException(string stateName)
            : base($"State:{stateName} Already Exists")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateStateException"/> class.
        /// </summary>
        protected DuplicateStateException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateStateException"/> class.
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="innerException">inner exception</param>
        protected DuplicateStateException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}