// -----------------------------------------------------------------------
// <copyright file="AmountTypeExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Edgar.Report13F;

public static class AmountTypeExtensions
{
    public static char? ToChar(this AmountType extended)
        => ((AmountType?)extended).ToChar();

    public static char? ToChar(this AmountType? extended)
        => extended switch
        {
            AmountType.Shares => 'S',
            AmountType.Principal => 'P',
            _ => null,
        };

    public static char? ToChar(this PutCallType? extended)
        => extended switch
        {
            PutCallType.Put => 'P',
            PutCallType.Call=> 'C',
            PutCallType.Indexed => 'I',
            _ => null,
        };

    public static char? ToChar(this AmendmentType extended)
        => ((AmendmentType?)extended).ToChar();

    public static char? ToChar(this AmendmentType? extended)
        => extended switch
        {
            AmendmentType.NewHoldings => 'N',
            AmendmentType.Restatement => 'R',
            _ => null,
        };


    public static char? ToChar(this ReportType extended)
        => ((ReportType?)extended).ToChar();


    public static char? ToChar(this ReportType? extended)
        => extended switch
        {
            ReportType.Combination => 'C',
            ReportType.Holdings => 'H',
            ReportType.Notice => 'N',
            _ => null,
        };
}