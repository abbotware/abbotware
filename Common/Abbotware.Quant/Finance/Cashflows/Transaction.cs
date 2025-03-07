﻿// -----------------------------------------------------------------------
// <copyright file="Transaction.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Cashflows
{
    /// <summary>
    /// Represents a cashflow transaction
    /// </summary>
    /// <typeparam name="TDate">type for date</typeparam>
    /// <typeparam name="TAmount">type for amount</typeparam>
    /// <param name="Date">date value</param>
    /// <param name="Amount">transaction amount</param>
    public record Transaction<TDate, TAmount>(TDate Date, TAmount Amount);
}
