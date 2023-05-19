// -----------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "<Reviewed AA>", Scope = "member", Target = "~M:Abbotware.Web.Api.Plugins.ApiClient.OnCreateRequest``1(System.Net.Http.HttpMethod,System.Uri,``0)~System.Net.Http.HttpRequestMessage")]
[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Disposed in OnDisposeManagedResources", Scope = "member", Target = "~F:Abbotware.Web.Api.Plugins.ApiClient.httpClient")]
[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Disposed in OnDisposeManagedResources", Scope = "member", Target = "~F:Abbotware.Web.Objects.WebComponent`1.client")]
