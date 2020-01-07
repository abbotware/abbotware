// -----------------------------------------------------------------------
// <copyright file="TransactionScopeAttribute.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Castle.Plugins.Aspects
{
    using System;

    /// <summary>
    ///     Attribute that indicates a transaction is required
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TransactionScopeAttribute : Attribute
    {
    }
}