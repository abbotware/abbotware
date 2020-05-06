// -----------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Reviewed AA>", Scope = "member", Target = "~M:Abbotware.Core.Process.Plugins.ShellCommand.ProcessCleanup(Abbotware.Core.Process.Plugins.ShellCommandResult,System.Diagnostics.Process)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "<Reviewed AA>", Scope = "member", Target = "~M:Abbotware.Core.Net.Plugins.WebApiClient.OnCreateRequest``1(System.Net.Http.HttpMethod,System.Uri,``0)~System.Net.Http.HttpRequestMessage")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Reviewed AA>", Scope = "member", Target = "~M:Abbotware.Core.Helpers.NetworkHelper.LocalNonLoopbackIPV4~System.Net.IPAddress[]")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "<Reviewed AA>", Scope = "member", Target = "~P:Abbotware.Core.Threading.Counters.ActiveInstanceCounter`1.GlobalActiveCount")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "<Reviewed AA>", Scope = "member", Target = "~P:Abbotware.Core.Threading.Counters.TypeCreatedCounter`1.Count")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "<Reviewed AA>", Scope = "type", Target = "~T:Abbotware.Core.Cache.ICacheableSortedSet`2")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "<Reviewed AA>", Scope = "type", Target = "~T:Abbotware.Core.Cache.ICacheableCategorizedFieldSet")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "<Reviewed AA>>", Scope = "member", Target = "~M:Abbotware.Core.Objects.IFactory`1.Destroy(`0)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "<Reviewed AA>", Scope = "member", Target = "~M:Abbotware.Core.Extensions.DisposableExtensions.DisposeAfterTimeout(System.IDisposable,System.TimeSpan)~System.IDisposable")]
[assembly: SuppressMessage("Security", "CA5351:Do Not Use Broken Cryptographic Algorithms", Justification = "<Reviewed AA>", Scope = "member", Target = "~M:Abbotware.Core.Security.MD5Helper.ToGuid(System.Byte[])~System.Guid")]
[assembly: SuppressMessage("Security", "CA5351:Do Not Use Broken Cryptographic Algorithms", Justification = "<Reviewed AA>", Scope = "member", Target = "~M:Abbotware.Core.Security.MD5Helper.GenerateHash(System.String,Abbotware.Core.Security.HashStringFormat)~System.String")]
