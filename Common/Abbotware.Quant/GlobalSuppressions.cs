// -----------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "Reviewed AA", Scope = "type", Target = "~T:Abbotware.Quant.Finance.CompoundingFrequency")]
[assembly: SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Reviewed AA", Scope = "type", Target = "~T:Abbotware.Quant.Finance.Equations.TimeValue.Discrete")]
[assembly: SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Reviewed AA", Scope = "type", Target = "~T:Abbotware.Quant.Finance.Equations.TimeValue.Continuous")]
[assembly: SuppressMessage("Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "Reviewed - Init code", Scope = "member", Target = "~F:Abbotware.Quant.LinearAlgebra.Matrix`1.m")]
[assembly: SuppressMessage("Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "Reviewed - Init code", Scope = "member", Target = "~M:Abbotware.Quant.LinearAlgebra.Matrix`1.#ctor(System.UInt32,System.UInt32)")]
[assembly: SuppressMessage("Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "Reviewed - Init code", Scope = "member", Target = "~M:Abbotware.Quant.LinearAlgebra.Matrix`1.#ctor(`0[,])")]
[assembly: SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "Reviewed AA", Scope = "member", Target = "~M:Abbotware.Quant.LinearAlgebra.Matrix`1.Multiply(Abbotware.Quant.LinearAlgebra.Matrix{`0},Abbotware.Quant.LinearAlgebra.Matrix{`0})~Abbotware.Quant.LinearAlgebra.Matrix{`0}")]
[assembly: SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "Reviewed AA", Scope = "member", Target = "~M:Abbotware.Quant.LinearAlgebra.RowVector`1.Multiply(Abbotware.Quant.LinearAlgebra.RowVector{`0},Abbotware.Quant.LinearAlgebra.ColumnVector{`0})~`0")]
[assembly: SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "Reviewed AA", Scope = "member", Target = "~M:Abbotware.Quant.LinearAlgebra.ColumnVector`1.Multiply(Abbotware.Quant.LinearAlgebra.ColumnVector{`0},Abbotware.Quant.LinearAlgebra.ColumnVector{`0})~Abbotware.Quant.LinearAlgebra.ColumnVector{`0}")]
[assembly: SuppressMessage("Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "Reviewed AA", Scope = "member", Target = "~M:Abbotware.Quant.LinearAlgebra.Extensions.ExtensionMethods.ToJaggedArray``1(``0[,])~``0[][]")]
[assembly: SuppressMessage("Design", "CA1033:Interface methods should be callable by child types", Justification = "Reviewed AA", Scope = "member", Target = "~M:Abbotware.Quant.LinearAlgebra.Matrix`1.Abbotware#Quant#LinearAlgebra#Operations#IRowTransform<T>#Add(Abbotware.Quant.LinearAlgebra.RowVector{`0})")]
[assembly: SuppressMessage("Design", "CA1033:Interface methods should be callable by child types", Justification = "Reviewed AA", Scope = "member", Target = "~M:Abbotware.Quant.LinearAlgebra.Matrix`1.Abbotware#Quant#LinearAlgebra#Operations#IColumnStatistics<T>#Mean~Abbotware.Quant.LinearAlgebra.RowVector{`0}")]
[assembly: SuppressMessage("Design", "CA1033:Interface methods should be callable by child types", Justification = "Reviewed AA", Scope = "member", Target = "~M:Abbotware.Quant.LinearAlgebra.Matrix`1.Abbotware#Quant#LinearAlgebra#Operations#IRowTransform<T>#Subtract(Abbotware.Quant.LinearAlgebra.RowVector{`0})")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Reviewed AA", Scope = "type", Target = "Abbotware.Quant.BlackScholes")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Reviewed AA", Scope = "type", Target = "Abbotware.Quant.Options.PutCallParity")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1312:Variable names should begin with lower-case letter", Justification = "Reviewed AA", Scope = "type", Target = "Abbotware.Quant.Options.PutCallParity")]
[assembly: SuppressMessage("Design", "CA1065:Do not raise exceptions in unexpected locations", Justification = "Reviewed AA", Scope = "member", Target = "~P:Abbotware.Quant.Rates.Plugins.ConstantRiskFreeRate`1.Range")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Reviewed AA", Scope = "member", Target = "~M:Abbotware.Quant.MonteCarlo.IStockMovement.Next(System.Double,System.Double)~System.Double")]
[assembly: SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Reviewed AA", Scope = "type", Target = "~T:Abbotware.Quant.Edgar.Report13F.InfoTable.Fields")]
[assembly: SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Reviewed AA", Scope = "type", Target = "~T:Abbotware.Quant.Edgar.Report13F.CoverPage.Fields")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed AA", Scope = "type", Target = "~T:Abbotware.Quant.Edgar.Report13F.CoverPage.Fields")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed AA", Scope = "type", Target = "~T:Abbotware.Quant.Edgar.Report13F.Submission.Fields")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed AA", Scope = "type", Target = "~T:Abbotware.Quant.Edgar.Report13F.InfoTable.Fields")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Reviewed AA", Scope = "type", Target = "~T:Abbotware.Quant.MonteCarlo.Plugins.BlackScholesEuler")]
[assembly: SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Reviewed AA", Scope = "type", Target = "~T:Abbotware.Quant.Edgar.Report13F.Submission.Fields")]
