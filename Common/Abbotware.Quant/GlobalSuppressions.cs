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
[assembly: SuppressMessage("Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "Reviewed - Init code", Scope = "member", Target = "~M:Abbotware.Quant.LinearAlgebra.ExtensionMethods.ToJaggedArray``1(``0[,])~``0[][]")]
[assembly: SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "Reviewed AA", Scope = "member", Target = "~M:Abbotware.Quant.LinearAlgebra.Matrix`1.Multiply(Abbotware.Quant.LinearAlgebra.Matrix{`0},Abbotware.Quant.LinearAlgebra.Matrix{`0})~Abbotware.Quant.LinearAlgebra.Matrix{`0}")]
[assembly: SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "Reviewed AA", Scope = "member", Target = "~M:Abbotware.Quant.LinearAlgebra.RowVector`1.Multiply(Abbotware.Quant.LinearAlgebra.RowVector{`0},Abbotware.Quant.LinearAlgebra.ColumnVector{`0})~`0")]
[assembly: SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "Reviewed AA", Scope = "member", Target = "~M:Abbotware.Quant.LinearAlgebra.ColumnVector`1.Multiply(Abbotware.Quant.LinearAlgebra.ColumnVector{`0},Abbotware.Quant.LinearAlgebra.ColumnVector{`0})~Abbotware.Quant.LinearAlgebra.ColumnVector{`0}")]
