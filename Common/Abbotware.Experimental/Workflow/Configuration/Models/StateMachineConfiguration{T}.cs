// -----------------------------------------------------------------------
// <copyright file="StateMachineConfiguration{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow.Configuration.Models
{
    using System;

    public class StateMachineConfiguration<T>
    {
        public StateMachineConfiguration(Func<T, string> retrieve, Action<T, string> store)
        {
            this.StoreStateFunctor = store;
            this.RetrieveStateFunctor = retrieve;
        }

        public Action<T, string> StoreStateFunctor { get; }

        public Func<T, string> RetrieveStateFunctor { get; }
    }
}
