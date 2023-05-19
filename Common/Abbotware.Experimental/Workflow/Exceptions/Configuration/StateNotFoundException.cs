// -----------------------------------------------------------------------
// <copyright file="StateNotFoundException.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow
{
    /// <summary>
    /// State not found configuration exception
    /// </summary>
    public class StateNotFoundException : ConfigurationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateNotFoundException"/> class.
        /// </summary>
        /// <param name="state">state</param>
        public StateNotFoundException(IState state)
            : base($"State:{state?.Name} Id:{state?.Id} Does not exist in state machine")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateNotFoundException"/> class.
        /// </summary>
        /// <param name="workflowName">workflow name</param>
        /// <param name="stateName">state name</param>
        public StateNotFoundException(string workflowName, string stateName)
            : base($"State:{stateName} Does not exist in state machine:{workflowName}")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateNotFoundException"/> class.
        /// </summary>
        /// <param name="stateName">state name</param>
        public StateNotFoundException(string stateName)
            : base($"State:{stateName} Does not exist in state machine")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateNotFoundException"/> class.
        /// </summary>
        /// <param name="id">state id</param>
        public StateNotFoundException(long id)
        : base($"State:{id} Does not exist in state machine")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateNotFoundException"/> class.
        /// </summary>
        protected StateNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateNotFoundException"/> class.
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="innerException">inner exception</param>
        protected StateNotFoundException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}