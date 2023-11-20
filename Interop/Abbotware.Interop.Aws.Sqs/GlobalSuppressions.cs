// -----------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Disposed in OnDisposeManagedResources", Scope = "member", Target = "~F:Abbotware.Interop.Aws.Sqs.Plugins.SqsConnectionFactory.factory")]
[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Disposed in OnDisposeManagedResources", Scope = "member", Target = "~F:Abbotware.Interop.Aws.Sqs.Plugins.SqsConnection.factory")]
[assembly: SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Reviewed - False Positive", Scope = "member", Target = "~M:Abbotware.Interop.Aws.Sqs.Plugins.SqsConnectionFactory.OnCreate(Abbotware.Interop.Aws.Sqs.Configuration.ISqsSettings)~Abbotware.Interop.Aws.Sqs.Plugins.SqsConnection")]