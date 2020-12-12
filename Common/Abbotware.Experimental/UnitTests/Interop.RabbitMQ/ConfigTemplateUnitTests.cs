namespace Abbotware.Tests.Interop.RabbitMQ
{
    using Abbotware.Core.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ConfigTemplates
    {
        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.ConfigTemplates")]
        public void Templates()
        {
            var cInfo = Abbotware.Interop.RabbitMQ.Config.Templates.ConsumerQueue();

            var cformat = cInfo.Arguments.StringFormat();

            var uInfo = Abbotware.Interop.RabbitMQ.Config.Templates.UnitTestQueue();

            var uformat = uInfo.Arguments.StringFormat();
        }
    }
}