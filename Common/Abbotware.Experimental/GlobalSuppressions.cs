// -----------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Security", "CA2109:Review visible event handlers", Justification = "Reviewed AA", Scope = "member", Target = "~M:Abbotware.Interop.RabbitMQ.ExtensionPoints.BaseChannelManager.OnBasicAck(System.Object,RabbitMQ.Client.Events.BasicAckEventArgs)")]
[assembly: SuppressMessage("Security", "CA2109:Review visible event handlers", Justification = "Reviewed AA", Scope = "member", Target = "~M:Abbotware.Interop.RabbitMQ.ExtensionPoints.BaseChannelManager.OnBasicReturn(System.Object,RabbitMQ.Client.Events.BasicReturnEventArgs)")]
[assembly: SuppressMessage("Security", "CA2109:Review visible event handlers", Justification = "Reviewed AA", Scope = "member", Target = "~M:Abbotware.Interop.RabbitMQ.ExtensionPoints.BaseChannelManager.OnModelShutdown(System.Object,RabbitMQ.Client.ShutdownEventArgs)")]
[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Disposed in OnDisposeManagedResources", Scope = "member", Target = "~F:Abbotware.Interop.Graphviz.Api.GraphvizContext.graphvizContextPtr")]
[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Disposed in OnDisposeManagedResources", Scope = "member", Target = "~F:Abbotware.Core.ExtensionPoints.BaseConsumer`1.workQueue")]
[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Disposed in OnDisposeManagedResources", Scope = "member", Target = "~F:Abbotware.Interop.Graphviz.InMemoryRender.ctx")]
[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Disposed in OnDisposeManagedResources", Scope = "member", Target = "~F:Abbotware.Core.Messaging.Plugins.MqConsumer`2.publisher")]
[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Disposed in OnDisposeManagedResources", Scope = "member", Target = "~F:Abbotware.Core.Security.Cryptography.Aes.decryptor")]
[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Disposed in OnDisposeManagedResources", Scope = "member", Target = "~F:Abbotware.Core.Security.Cryptography.Aes.encryptor")]
[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Disposed in OnDisposeManagedResources", Scope = "member", Target = "~F:Abbotware.Interop.Graphviz.Api.Graph.graphManagedApi")]
[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Disposed in OnDisposeManagedResources", Scope = "member", Target = "~F:Abbotware.Interop.Graphviz.InMemoryRender.graph")]
