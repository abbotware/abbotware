//-----------------------------------------------------------------------
// <copyright file="IMessageSubscription.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.Core.Messaging
{
    using System;

    /// <summary>
    ///     Not used
    /// </summary>
    [Obsolete("this might be development code and unused")]
    public interface IMessageSubscription
    {
        /// <summary>
        ///     Not used
        /// </summary>
        /// <typeparam name="T">This is Not used</typeparam>
        void Subscribe<T>();
    }
}