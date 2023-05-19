// -----------------------------------------------------------------------
// <copyright file="AsyncObservableCollection{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Collections
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;

    /// <summary>
    ///     An ObservableCollection collection that can be used for asynchronous UI updates
    /// </summary>
    /// <typeparam name="T">Type for collection</typeparam>
    public class AsyncObservableCollection<T> : ObservableCollection<T>
    {
        /// <summary>
        ///     Holds synchronization context for callbacks / notification
        /// </summary>
        private readonly SynchronizationContext? localCachedSynchronizationContext;

        /// <summary>
        ///     Flag used in AddRange to disable batch notification
        /// </summary>
        private bool suppressNotification;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncObservableCollection{T}" /> class.
        /// </summary>
        public AsyncObservableCollection()
            : this(Enumerable.Empty<T>())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncObservableCollection{T}" /> class.
        /// </summary>
        /// <param name="initialList">Initial list to create collection with</param>
        public AsyncObservableCollection(IEnumerable<T> initialList)
            : base(initialList)
        {
#if NETFX_40
    ////HACK: workaround for bug:
    ////http://stackoverflow.com/questions/11621372/synchronizationcontext-current-is-null-in-continuation-on-the-main-ui-thread
    ////Fixed if .Net framework 4.5 is installed then change code to:

            Contract.Assume(GlobalSynchronizationContext.GlobalCachedSynchronizationContext != null);
            this.localCachedSynchronizationContext = GlobalSynchronizationContext.GlobalCachedSynchronizationContext;
#else
            this.localCachedSynchronizationContext = SynchronizationContext.Current;

#endif
        }

        /// <summary>
        ///     Replace the entire collection
        /// </summary>
        /// <param name="replacementList">list to replace current collection with</param>
        public void Replace(IEnumerable<T> replacementList)
        {
            Arguments.NotNull(replacementList, nameof(replacementList));

            try
            {
                this.suppressNotification = true;

                this.Clear();

                foreach (var item in replacementList)
                {
                    this.Add(item);
                }
            }
            finally
            {
                this.suppressNotification = false;
            }

            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        ///     Appends another list to this collection
        /// </summary>
        /// <param name="appendList">List to append to current collection</param>
        public void AddRange(IEnumerable<T> appendList)
        {
            if (appendList == null)
            {
                return;
            }

            try
            {
                this.suppressNotification = true;

                foreach (var item in appendList)
                {
                    this.Add(item);
                }
            }
            finally
            {
                this.suppressNotification = false;
            }

            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <inheritdoc />
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.suppressNotification)
            {
                return;
            }

            if (SynchronizationContext.Current == this.localCachedSynchronizationContext)
            {
                // Execute the CollectionChanged event on the current thread
                this.RaiseCollectionChanged(e);
            }
            else
            {
                // Post the CollectionChanged event on the creator thread
                this.localCachedSynchronizationContext?.Post(this.RaiseCollectionChanged, e);
            }
        }

        /// <inheritdoc />
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (this.suppressNotification)
            {
                return;
            }

            if (SynchronizationContext.Current == this.localCachedSynchronizationContext)
            {
                // Execute the PropertyChanged event on the current thread
                this.RaisePropertyChanged(e);
            }
            else
            {
                // Post the PropertyChanged event on the creator thread
                this.localCachedSynchronizationContext?.Post(this.RaisePropertyChanged, e);
            }
        }

        /// <summary>
        ///     Internal callback for Posting OnPropertyChanged
        /// </summary>
        /// <param name="param">object parameter</param>
        private void RaisePropertyChanged(object? param)
        {
            param = Arguments.EnsureNotNull(param, nameof(param));

            // We are in the creator thread, call the base implementation directly
            base.OnPropertyChanged((PropertyChangedEventArgs)param);
        }

        /// <summary>
        ///     Internal callback for Posting OnCollectionChanged
        /// </summary>
        /// <param name="param">object parameter</param>
        private void RaiseCollectionChanged(object? param)
        {
            param = Arguments.EnsureNotNull(param, nameof(param));

            // We are in the creator thread, call the base implementation directly
            base.OnCollectionChanged((NotifyCollectionChangedEventArgs)param);
        }
    }
}