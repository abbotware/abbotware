﻿// -----------------------------------------------------------------------
// <copyright file="CloudWatchRenderer.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.Plugins
{
    using System.IO;
    using Abbotware.Core;
    using Serilog.Events;
    using Serilog.Formatting;

    /// <summary>
    /// Renderer for cloud watch logs
    /// </summary>
    public class CloudWatchRenderer : ITextFormatter
    {
        /// <inheritdoc/>
        public void Format(LogEvent logEvent, TextWriter output)
        {
            logEvent = Arguments.EnsureNotNull(logEvent, nameof(logEvent));
            output = Arguments.EnsureNotNull(output, nameof(output));

            var msg = logEvent.RenderMessage();

            var t = $"[{logEvent.Timestamp}] [{logEvent.Level}] {msg} {logEvent.Exception}";

            output.WriteLine(t);
        }
    }
}