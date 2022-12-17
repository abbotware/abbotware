// -----------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Reviewed AA>", Scope = "member", Target = "~M:Abbotware.Interop.Castle.ExtensionPoints.PollingComponent.OnDispatchAsync~System.Threading.Tasks.Task")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Reviewed AA>", Scope = "member", Target = "~M:Abbotware.Interop.Castle.Plugins.Installers.LoadInstallersInAssembly.OnInstall(Castle.Windsor.IWindsorContainer,Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore)")]
[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Disposed in OnDisposeManagedResources", Scope = "member", Target = "~F:Abbotware.Interop.Castle.ExtensionPoints.PollingComponent.cts")]
[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Disposed in OnDisposeManagedResources", Scope = "member", Target = "~F:Abbotware.Interop.Castle.ExtensionPoints.StartablePollingComponent.subscriptionHandle")]
[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Disposed in OnDisposeManagedResources", Scope = "member", Target = "~F:Abbotware.Interop.Castle.ExtensionPoints.BaseStartableComponent.cts")]
