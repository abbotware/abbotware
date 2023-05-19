namespace Abbotware.Quant.Extensions
{
    using System.Linq;
    using Abbotware.Quant.Cashflows;
    using Abbotware.Quant.Finance.Equations;
    using Abbotware.Quant.InterestRates;

    public static class TransactionsExtensions
    {
        public static Transactions<double> Discounted(this Transactions<double> transactions, IRiskFreeRate<double> riskFreeRate)
        {
            var t = transactions.Select(x => x with { Amount = x.Amount * (decimal)DiscountFactor.Continuous(riskFreeRate.Nearest(x.Date), x.Date) });

            return new Transactions<double>(t);
        }
    }
}
