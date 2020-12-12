// -----------------------------------------------------------------------
// <copyright file="DuplicateStateException.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>
namespace Abbotware.Core.Workflow
{
    public class DuplicateStateException : ConfigurationException
    {
        public DuplicateStateException(IState state)
            : base($"State:{state?.Name} Id:{state?.Id} Already exists")
        {
        }

        public DuplicateStateException(string stateName)
            : base($"State:{stateName} Already Exists")
        {
        }

        protected DuplicateStateException()
        {
        }

        protected DuplicateStateException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}