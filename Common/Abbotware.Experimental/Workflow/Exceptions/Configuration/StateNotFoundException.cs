// -----------------------------------------------------------------------
// <copyright file="StateNotFoundException.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow
{
    public class StateNotFoundException : ConfigurationException
    {
        public StateNotFoundException(IState state)
            : base($"State:{state?.Name} Id:{state?.Id} Does not exist in state machine")
        {
        }

        public StateNotFoundException(string workflowName, string stateName)
            : base($"State:{stateName} Does not exist in state machine:{workflowName}")
        {
        }

        public StateNotFoundException(string stateName)
            : base($"State:{stateName} Does not exist in state machine")
        {
        }

        public StateNotFoundException(long id)
        : base($"State:{id} Does not exist in state machine")
        {
        }

        protected StateNotFoundException()
        {
        }

        protected StateNotFoundException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}