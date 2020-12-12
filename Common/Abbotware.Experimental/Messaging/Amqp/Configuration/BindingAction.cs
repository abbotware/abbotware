// -----------------------------------------------------------------------
// <copyright file="BindingAction.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.Configuration
{
    /// <summary>
    ///     determines what action to take when performing a bind
    /// </summary>
    public enum BindingAction
    {
        /// <summary>
        ///     Perform a Bind
        /// </summary>
        Bind,

        /// <summary>
        ///     Perform an Unbind
        /// </summary>
        Unbind,
    }
}