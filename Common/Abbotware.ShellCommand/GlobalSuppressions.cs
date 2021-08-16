// -----------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Reviewed AA>", Scope = "member", Target = "~M:Abbotware.ShellCommand.AbbotwareShellCommand`1.ProcessCleanup(Abbotware.ShellCommand.ExitInfo,System.Diagnostics.Process)")]
[assembly: SuppressMessage("Design", "CA1019:Define accessors for attribute arguments", Justification = "<Reviewed AA>", Scope = "member", Target = "~M:Abbotware.ShellCommand.Plugins.ShellCommandOptionAttribute.#ctor(System.Char)")]
#if NET5_0_OR_GREATER
[assembly: SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Reviewed AA>", Scope = "member", Target = "~M:Abbotware.ShellCommand.AbbotwareShellCommand`1.OnExecuteAsync(System.Threading.CancellationToken)~System.Threading.Tasks.Task{Abbotware.ShellCommand.IExitInfo}")]
#endif