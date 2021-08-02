// -----------------------------------------------------------------------
// <copyright file="TrackingScheduler.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Threading
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class TrackingScheduler : TaskScheduler
    {
        protected override IEnumerable<Task> GetScheduledTasks()
        { 
            return this.GetScheduledTasks();
        }

        /// <inheritdoc/>
        protected override void QueueTask(Task task)
        {
            this.QueueTask(task);
        }

        /// <inheritdoc/>
        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return this.TryExecuteTaskInline(task, taskWasPreviouslyQueued);
        }

        /// <inheritdoc/>
        protected override bool TryDequeue(Task task)
        {
            return base.TryDequeue(task);
        }
    }
}
