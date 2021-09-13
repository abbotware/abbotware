// -----------------------------------------------------------------------
// <copyright file="InsiderTransaction.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Insider Transaction POCO
    /// </summary>
    public record InsiderTransaction(
        DateTimeOffset? Date,
        [property: MaxLength(100)] string? OwnerCik,
        [property: MaxLength(100)] string OwnerName,
        DateTimeOffset? TransactionDate,
        char? TransactionCode,
        decimal? TransactionAmount,
        decimal? TransactionPrice,
        char? TransactionAcquiredDisposed,
        decimal? PostTransactionAmount,
        [property: MaxLength(500)] string? SecLink)
    {
    }
}