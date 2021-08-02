// -----------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Security", "CA2109:Review visible event handlers", Justification = "Reviewed AA", Scope = "member", Target = "~M:Abbotware.Interop.RabbitMQ.ExtensionPoints.BaseChannelManager.OnBasicAck(System.Object,RabbitMQ.Client.Events.BasicAckEventArgs)")]
[assembly: SuppressMessage("Security", "CA2109:Review visible event handlers", Justification = "Reviewed AA", Scope = "member", Target = "~M:Abbotware.Interop.RabbitMQ.ExtensionPoints.BaseChannelManager.OnBasicReturn(System.Object,RabbitMQ.Client.Events.BasicReturnEventArgs)")]
[assembly: SuppressMessage("Security", "CA2109:Review visible event handlers", Justification = "Reviewed AA", Scope = "member", Target = "~M:Abbotware.Interop.RabbitMQ.ExtensionPoints.BaseChannelManager.OnModelShutdown(System.Object,RabbitMQ.Client.ShutdownEventArgs)")]
