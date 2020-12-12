// -----------------------------------------------------------------------
// <copyright file="WorkflowException.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow
{
    using System;

    public abstract class WorkflowException : Exception
    {
        public WorkflowException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public WorkflowException(string message)
            : this(message, null)
        {
        }

        protected WorkflowException()
        {
        }
    }
}