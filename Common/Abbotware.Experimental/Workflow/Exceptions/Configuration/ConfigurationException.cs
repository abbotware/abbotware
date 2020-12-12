// -----------------------------------------------------------------------
// <copyright file="ConfigurationException.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow
{
    using System;

    public abstract class ConfigurationException : WorkflowException
    {
        public ConfigurationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public ConfigurationException(string message)
            : this(message, null)
        {
        }

        protected ConfigurationException()
        {
        }
    }
}