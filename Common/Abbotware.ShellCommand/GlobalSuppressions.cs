// -----------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Reviewed AA>", Scope = "member", Target = "~M:Abbotware.ShellCommand.AbbotwareShellCommand.ProcessCleanup(Abbotware.ShellCommand.ExitInfo,System.Diagnostics.Process)")]
#if NET5_0_OR_GREATER
[assembly: SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Reviewed AA>", Scope = "member", Target = "~M:Abbotware.ShellCommand.AbbotwareShellCommand.OnExecuteAsync(System.Threading.CancellationToken)~System.Threading.Tasks.Task{Abbotware.ShellCommand.IExitInfo}")]
#endif