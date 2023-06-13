// -----------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "Reviewed - AWS library requires a list", Scope = "member", Target = "~M:Abbotware.Interop.Aws.Timestream.Protocol.Plugins.TimestreamProtocol`1.OnCreateDimensions(`0)~System.Collections.Generic.List{Amazon.TimestreamWrite.Model.Dimension}")]
[assembly: SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "Reviewed - AWS library requires a list", Scope = "member", Target = "~M:Abbotware.Interop.Aws.Timestream.Protocol.Plugins.TimestreamProtocol`1.OnCreateMeasures(`0)~System.Collections.Generic.List{Amazon.TimestreamWrite.Model.MeasureValue}")]
[assembly: SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "ownership transfer", Scope = "member", Target = "~M:Abbotware.Interop.Aws.Timestream.TimestreamPublisher`1.#ctor(Abbotware.Interop.Aws.Timestream.Configuration.TimestreamOptions,Abbotware.Interop.Aws.Timestream.Protocol.ITimestreamProtocol{`0},Microsoft.Extensions.Logging.ILogger{Abbotware.Interop.Aws.Timestream.TimestreamPublisher{`0}})")]
[assembly: SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "ownership transfer", Scope = "member", Target = "~M:Abbotware.Interop.Aws.Timestream.PocoPublisher`1.#ctor(Abbotware.Interop.Aws.Timestream.Configuration.TimestreamOptions,Microsoft.Extensions.Logging.ILogger)")]
[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "base class disposes", Scope = "member", Target = "~F:Abbotware.Interop.Aws.Timestream.BufferedTimestreamPublisher`1.channel")]
[assembly: SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "ownership transfer", Scope = "member", Target = "~M:Abbotware.Interop.Aws.Timestream.BufferedTimestreamPublisher`1.#ctor(Abbotware.Interop.Aws.Timestream.Configuration.TimestreamOptions,Abbotware.Interop.Aws.Timestream.Protocol.ITimestreamProtocol{`0},Microsoft.Extensions.Logging.ILoggerFactory,Microsoft.Extensions.Logging.ILogger{Abbotware.Interop.Aws.Timestream.TimestreamPublisher{`0}})")]