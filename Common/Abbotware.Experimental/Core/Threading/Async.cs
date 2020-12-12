// -----------------------------------------------------------------------
// <copyright file="Async.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Sid Sacek</author>

namespace Abbotware.Core.Threading
{
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    /// <summary>
    ///     Async/Await helpers
    /// </summary>
    public static class Async
    {
        //-------------------------------------------------------------------------------
        //  These four static methods can be called using the following syntaxes:
        //
        //      await task.OnAny();
        //      await task.OnSame();
        //
        //      var result = await task<>.OnAny();
        //      var result = await task<>.OnSame();
        //
        //  Or alternatively:
        //
        //      await Async.OnAny( task );
        //      await Async.OnSame( task );
        //
        //      var result = await Async.OnAny( task<> );
        //      var result = await Async.OnSame( task<> );
        //-------------------------------------------------------------------------------

        /// <summary>
        /// Forces the Task to always use ConfigureAwait(false)
        /// </summary>
        /// <param name="task">The Task to modify</param>
        /// <returns>An awaitable ConfiguredTaskAwaitable instance</returns>
        public static ConfiguredTaskAwaitable OnAny(this Task task)
        {
            task = Arguments.EnsureNotNull(task, nameof(task));

            var awaitable = task.ConfigureAwait(false);

            return awaitable;
        }

        /// <summary>
        /// Forces the Task to always use ConfigureAwait(false)
        /// </summary>
        /// <typeparam name="T">The Task's type</typeparam>
        /// <param name="task">The Task to modify</param>
        /// <returns>An awaitable ConfiguredTaskAwaitable instance</returns>
        public static ConfiguredTaskAwaitable<T> OnAny<T>(this Task<T> task)
        {
            task = Arguments.EnsureNotNull(task, nameof(task));

            var awaitable = task.ConfigureAwait(false);

            return awaitable;
        }

        /// <summary>
        /// Forces the Task to always use ConfigureAwait(true)
        /// </summary>
        /// <param name="task">The Task to modify</param>
        /// <returns>An awaitable ConfiguredTaskAwaitable instance</returns>
        public static ConfiguredTaskAwaitable OnSame(this Task task)
        {
            task = Arguments.EnsureNotNull(task, nameof(task));

            var awaitable = task.ConfigureAwait(true);

            return awaitable;
        }

        /// <summary>
        /// Forces the Task to always use ConfigureAwait(true)
        /// </summary>
        /// <typeparam name="T">The Task's type</typeparam>
        /// <param name="task">The Task to modify</param>
        /// <returns>An awaitable ConfiguredTaskAwaitable instance</returns>
        public static ConfiguredTaskAwaitable<T> OnSame<T>(this Task<T> task)
        {
            task = Arguments.EnsureNotNull(task, nameof(task));

            var awaitable = task.ConfigureAwait(true);

            return awaitable;
        }
    }
}
