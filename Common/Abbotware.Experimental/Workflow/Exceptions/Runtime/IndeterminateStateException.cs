// -----------------------------------------------------------------------
// <copyright file="IndeterminateStateException.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow
{
    public class IndeterminateStateException : RuntimeException
    {
        public IndeterminateStateException(object o, string state, string workflowName)
            : base($"object:{o} is in an inconsistent/invalid state.  State:{state} not in state machine:{workflowName}")
        {
        }

        protected IndeterminateStateException()
        {
        }

        protected IndeterminateStateException(string message)
            : base(message)
        {
        }

        protected IndeterminateStateException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}