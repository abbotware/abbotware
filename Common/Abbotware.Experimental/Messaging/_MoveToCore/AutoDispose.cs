// -----------------------------------------------------------------------
// <copyright file="AutoDispose.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core
{
    using System;

    /// <summary>
    ///     Helper class for auto disposing disposable objects
    /// </summary>
    /// <example>
    /// IDisposable SomeFunction()
    /// {
    ///     using(var something = new AutoDispose{SomeType}())
    ///     {
    ///         something.Value.Function(d);
    ///
    ///         Do Other Stuff
    ///
    ///         return.something.Return();
    ///     }
    /// }
    /// </example>
    /// <typeparam name="TDisposable">Object to wrap</typeparam>
    public sealed class AutoDispose<TDisposable> : AutoFactory<TDisposable>
        where TDisposable : class, IDisposable, new()
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AutoDispose{TDisposable}" /> class.
        /// </summary>
        public AutoDispose()
            : base(new TDisposable())
        {
        }
    }
}