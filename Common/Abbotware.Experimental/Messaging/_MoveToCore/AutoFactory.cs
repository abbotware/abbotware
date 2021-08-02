// -----------------------------------------------------------------------
// <copyright file="AutoFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core
{
    using System;
    using Abbotware.Core.Objects;

    /// <summary>
    ///     Helper class for auto disposing disposable objects
    /// </summary>
    /// <typeparam name="TDisposable">Object to wrap</typeparam>
    public class AutoFactory<TDisposable> : BaseComponent
        where TDisposable : class, IDisposable
    {
        /// <summary>
        ///     internal reference used to detect if the value was ever returned;
        /// </summary>
        private readonly WeakReference<TDisposable> value;

        /// <summary>
        ///     internal reference used to detect if the value was ever returned;
        /// </summary>
        private TDisposable? returned;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AutoFactory{TDisposable}" /> class.
        /// </summary>
        /// <param name="factory">object factory</param>
        public AutoFactory(Func<TDisposable> factory)
            : this(factory())
        {
            Arguments.NotNull(factory, nameof(factory));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AutoFactory{TDisposable}" /> class.
        /// </summary>
        /// <param name="disposable">object to return</param>
        protected AutoFactory(TDisposable disposable)
        {
            this.returned = disposable;

            this.value = new WeakReference<TDisposable>(this.returned);
        }

        /// <summary>
        ///     Gets the wrapped disposable
        /// </summary>
        public TDisposable Value
        {
            get
            {
                this.ThrowIfReturned();

                this.value.TryGetTarget(out TDisposable target);

                if (target == null)
                {
                    throw new ObjectDisposedException("some how object was already disposed");
                }

                return target;
            }
        }

        /// <summary>
        ///     Called during function return
        /// </summary>
        /// <returns>the wrapped disposable</returns>
        public TDisposable Return()
        {
            this.ThrowIfReturned();

            var temp = this.Value;

            this.returned = null;

            return temp;
        }

        /// <inheritdoc />
        protected override void OnDisposeManagedResources()
        {
            this.returned?.Dispose();
        }

        /// <summary>
        ///     throws an exception if the wrapped disposable has already been returned
        /// </summary>
        private void ThrowIfReturned()
        {
            if (this.returned == null)
            {
                throw new InvalidOperationException("Object was already returned");
            }
        }
    }
}