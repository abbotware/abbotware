// -----------------------------------------------------------------------
// <copyright file="DerivativeModel.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Derivatives
{
    public record DerivativeModel { }

    public record ForwardModel : DerivativeModel { }

    public record RateAgreementModel : ForwardModel { }
}
