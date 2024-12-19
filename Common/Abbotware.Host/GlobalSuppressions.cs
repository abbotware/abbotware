// -----------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Bottom of Stack", Scope = "member", Target = "~M:Abbotware.Host.PollingComponent.OnDispatchAsync~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Disposed in Virtual", Scope = "member", Target = "~F:Abbotware.Host.BaseStartableComponent.cts")]