// -----------------------------------------------------------------------
// <copyright file="TaskExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Logging;

    /// <summary>
    ///     Task extensions
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// fire and forget task (use default task scheduler)
        /// </summary>
        /// <param name="task">task to extend</param>
        /// <param name="logger">logger to handle errors</param>
        public static void Forget(this Task task, ILogger logger)
        {
            Forget(task, logger, TaskScheduler.Default);
        }

        /// <summary>
        /// fire and forget task
        /// </summary>
        /// <param name="task">task to extend</param>
        /// <param name="logger">logger to handle errors</param>
        /// <param name="taskScheduler">task scheduler</param>
        public static void Forget(this Task task, ILogger logger, TaskScheduler taskScheduler)
        {
            task = Arguments.EnsureNotNull(task, nameof(task));

            taskScheduler ??= TaskScheduler.Default;

            task.ContinueWith(
                t => { logger.Error(t.Exception, "task"); },
                CancellationToken.None,
                TaskContinuationOptions.OnlyOnFaulted,
                taskScheduler);
        }

        /// <summary>
        /// fire and forget task (use default task scheduler)
        /// </summary>
        /// <param name="task">task to extend</param>
        /// <param name="onCompleted">on complete callback</param>
        /// <param name="onFaulted">on fault callback</param>
        public static void Forget(this Task task, Action<Task> onCompleted, Action<Task> onFaulted)
        {
            Forget(task, onCompleted, onFaulted, TaskScheduler.Default);
        }

        /// <summary>
        /// fire and forget task
        /// </summary>
        /// <param name="task">task to extend</param>
        /// <param name="onCompleted">on complete callback</param>
        /// <param name="onFaulted">on fault callback</param>
        /// <param name="taskScheduler">task scheduler</param>
        public static void Forget(this Task task, Action<Task> onCompleted, Action<Task> onFaulted, TaskScheduler taskScheduler)
        {
            task = Arguments.EnsureNotNull(task, nameof(task));

            task.ContinueWith(
                t => onCompleted(t),
                taskScheduler);

            task.ContinueWith(
                t => onFaulted(t),
                CancellationToken.None,
                TaskContinuationOptions.OnlyOnFaulted,
                taskScheduler);
        }

        /// <summary>
        /// Timeout a task after a timeout
        /// </summary>
        /// <remarks>https://blogs.msdn.microsoft.com/pfxteam/2011/11/10/crafting-a-task-timeoutafter-method/</remarks>
        /// <param name="task">wrapped task</param>
        /// <param name="timeout">timeout value</param>
        /// <returns>awaitable task</returns>
        public static async Task TimeoutAfter(this Task task, TimeSpan timeout)
        {
            Arguments.NotNull(task, nameof(task));

            var timeoutTask = Task.Delay(timeout);
            var completedTask = await Task.WhenAny(task, timeoutTask)
                .ConfigureAwait(false);

            if (completedTask == timeoutTask)
            {
                throw new TimeoutException();
            }
        }
    }
}
