// -----------------------------------------------------------------------
// <copyright file="LazyExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Extensions
{
    using System;

    /// <summary>
    ///     Lazy Object Extensions methods
    /// </summary>
    public static class LazyExtensions
    {
        /// <summary>
        ///     Safely disposes a Lazy{IDisposable} by first checking for null and if the lazy object was initalzied.  If its null,
        ///     this does nothing
        /// </summary>
        /// <typeparam name="TObject">type of the IDisposable</typeparam>
        /// <param name="extended">IDisposable Object being extended</param>
        public static void DisposeIfInitialized<TObject>(this Lazy<TObject> extended)
            where TObject : IDisposable
        {
            if (extended != null && extended.IsValueCreated)
            {
                extended.Value.Dispose();
            }
        }
    }
}