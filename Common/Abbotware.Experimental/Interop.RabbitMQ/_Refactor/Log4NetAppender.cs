// -----------------------------------------------------------------------
// <copyright file="Log4NetAppender.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.RabbitMQ.Plugins
{
    using log4net.Appender;
    using log4net.Core;

    /// <summary>
    /// Log4net Appender for logging to RabbitMQ
    /// </summary>
    public class Log4NetAppender : AppenderSkeleton
    {
        /// <inheritdoc/>
        protected override void Append(LoggingEvent loggingEvent)
        {
        }
    }
}
