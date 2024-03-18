namespace Abbotware.UnitTests.Quant
{
    using Abbotware.Quant.Statistics;
    using NUnit.Framework;

    internal class ErrorAnalysisTests
    {
        [Test]
        public void AbsolutePercentageError()
        {
            var e = ErrorAnalysis.AbsolutePercentageError(34d, 37d);
            Assert.That(e, Is.EqualTo(0.088235294117647065d));
        }

        [Test]
        public void MeanAbsolutePercentageError()
        {
            var actual = new[] { 34d, 37d, 44d, 47d, 48d, 48d, 46d, 43d, 32d, 27d, 26d, 24d };
            var forecasts = new[] { 37d, 40d, 46d, 44d, 46d, 50d, 45d, 44d, 34d, 30d, 22d, 23d };

            var e = ErrorAnalysis.MeanAbsolutePercentageError(actual, forecasts);
            Assert.That(e, Is.EqualTo(0.064671076436071007d));
        }
    }
}
