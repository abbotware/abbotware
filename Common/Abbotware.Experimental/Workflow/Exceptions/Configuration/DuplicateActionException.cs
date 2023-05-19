// -----------------------------------------------------------------------
// <copyright file="DuplicateActionException.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow
{
    /// <summary>
    /// Duplciate action configuration exception
    /// </summary>
    public class DuplicateActionException : ConfigurationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateActionException"/> class.
        /// </summary>
        /// <param name="action">action</param>
        /// <param name="s">source state</param>
        /// <param name="t">target state</param>
        public DuplicateActionException(string action, IState s, IState t)
            : base($"Action:{action} {s?.Name}->{t?.Name} already exists")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateActionException"/> class.
        /// </summary>
        /// <param name="action">action</param>
        public DuplicateActionException(IAction action)
         : base($"Id:{action?.Id} already exists for Action:{action?.Name} {action?.Source?.Name}->{action?.Target?.Name} already exists")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateActionException"/> class.
        /// </summary>
        protected DuplicateActionException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateActionException"/> class.
        /// </summary>
        /// <param name="message">message</param>
        protected DuplicateActionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateActionException"/> class.
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="innerException">inner exception</param>
        protected DuplicateActionException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}