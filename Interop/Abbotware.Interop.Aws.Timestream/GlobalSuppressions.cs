// -----------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "Reviewed - AWS library requires a list", Scope = "member", Target = "~M:Abbotware.Interop.Aws.Timestream.BaseTimestreamPublisher`1.OnCreateDimensions(`0)~System.Collections.Generic.List{Amazon.TimestreamWrite.Model.Dimension}")]
[assembly: SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "Reviewed - AWS library requires a list", Scope = "member", Target = "~M:Abbotware.Interop.Aws.Timestream.BaseTimestreamPublisher`1.OnCreateMeasures(`0)~System.Collections.Generic.List{Amazon.TimestreamWrite.Model.MeasureValue}")]
[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Reviewed - correctly called via inheritenc", Scope = "member", Target = "~F:Abbotware.Interop.Aws.Timestream.BaseTimestreamPublisher`1.writeClient")]
