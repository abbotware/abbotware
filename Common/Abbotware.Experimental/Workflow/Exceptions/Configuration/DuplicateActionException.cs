// -----------------------------------------------------------------------
// <copyright file="DuplicateActionException.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow
{
    public class DuplicateActionException : ConfigurationException
    {
        public DuplicateActionException(string action, IState s, IState t)
            : base($"Action:{action} {s?.Name}->{t?.Name} already exists")
        {
        }

        public DuplicateActionException(IAction action)
         : base($"Id:{action?.Id} already exists for Action:{action?.Name} {action?.Source?.Name}->{action?.Target?.Name} already exists")
        {
        }

        protected DuplicateActionException()
        {
        }

        protected DuplicateActionException(string message)
            : base(message)
        {
        }

        protected DuplicateActionException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}