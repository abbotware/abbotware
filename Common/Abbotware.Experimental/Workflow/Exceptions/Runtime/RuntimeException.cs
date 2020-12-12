// -----------------------------------------------------------------------
// <copyright file="RuntimeException.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>
namespace Abbotware.Core.Workflow
{
    using System;

    public class RuntimeException : WorkflowException
    {
        public RuntimeException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public RuntimeException(string message)
            : this(message, null)
        {
        }

        protected RuntimeException()
        {
        }
    }
}