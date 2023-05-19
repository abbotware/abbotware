// -----------------------------------------------------------------------
// <copyright file="StateMachineConfiguration{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow.Configuration.Models
{
    using System;

    /// <summary>
    /// State machine configuration
    /// </summary>
    /// <typeparam name="T">external object type</typeparam>
    public class StateMachineConfiguration<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateMachineConfiguration{T}"/> class.
        /// </summary>
        /// <param name="retrieve">retrieve state delegate</param>
        /// <param name="store">store state delegate</param>
        public StateMachineConfiguration(Func<T, string> retrieve, Action<T, string> store)
        {
            this.StoreStateFunctor = store;
            this.RetrieveStateFunctor = retrieve;
        }

        /// <summary>
        /// Gets the store state delegate
        /// </summary>
        public Action<T, string> StoreStateFunctor { get; }

        /// <summary>
        /// Gets the retrive state delegate
        /// </summary>
        public Func<T, string> RetrieveStateFunctor { get; }
    }
}
