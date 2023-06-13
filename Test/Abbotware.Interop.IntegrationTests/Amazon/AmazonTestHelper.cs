namespace Abbotware.IntegrationTests.Interop.Amazon
{
    using System.Threading.Tasks;
    using Abbotware.Interop.Aws.Sqs;
    using Abbotware.Interop.Aws.Sqs.Configuration.Models;
    using Abbotware.Interop.Aws.Sqs.Plugins;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using Microsoft.Extensions.Logging.Abstractions;

    public static class AmazonTestHelper
    {
        public static async Task PurgeUnitTestQueue()
        {
            await Task.Delay(61000);

            using (var c = SqsHelper.CreateConnection(NullLoggerFactory.Instance, SqsSettings.DefaultSection, BaseNUnitTest.UnitTestSettingsFile))
            using (var q = (SqsQueueManager)c.CreateQueueManager())
            {
                await q.PurgeAsync(q.Configuration.Queue.ToString());
            }

            await Task.Delay(61000);
        }
    }
}
