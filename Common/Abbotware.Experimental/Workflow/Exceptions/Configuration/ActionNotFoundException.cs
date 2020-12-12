// -----------------------------------------------------------------------
// <copyright file="ActionNotFoundException.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow
{
    public class ActionNotFoundException : ConfigurationException
    {
        public ActionNotFoundException(IAction action)
            : base($"Action:{action} {action?.Source}->{action?.Target} does not exist in state machine")
        {
        }

        public ActionNotFoundException(IState state, string action)
        : base($"State:{state?.Name} - Action:{action} does not exist in state machine")
        {
        }

        public ActionNotFoundException(string action, IState s, IState t)
            : base($"Action:{action} {s?.Name}->{t?.Name} does not exist in state machine")
        {
        }

        public ActionNotFoundException(string action, string sourceName, string targetName)
            : base($"Action:{action} {sourceName}->{targetName} does not exist in state machine")
        {
        }

        public ActionNotFoundException(long id)
            : base($"Action:{id} Does not exist in state machine")
        {
        }

        public ActionNotFoundException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected ActionNotFoundException()
        {
        }

        protected ActionNotFoundException(string message)
            : base(message)
        {
        }
    }
}