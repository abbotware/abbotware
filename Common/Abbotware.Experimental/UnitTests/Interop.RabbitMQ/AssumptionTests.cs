namespace Abbotware.Tests.Interop.RabbitMQ
{
    using System;
    using Abbotware.Interop.RabbitMQ.Plugins;
    using global::RabbitMQ.Client;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AssumptionTests   : BaseRabbitUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.Assumptions")]
        public void NullExchange()
        {
            var factory = new ConnectionFactory { HostName = "192.168.2.103", UserName = "test", Password = "test" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.BasicPublish(null, string.Empty, true, null, null);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.Assumptions")]
        public void NullRoutingKey()
        {
            var factory = new ConnectionFactory { HostName = "192.168.2.103", UserName = "test", Password = "test" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.BasicPublish(string.Empty, null, true, null, null);
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.Assumptions")]
        public void EmptyString_EmptyExchange_RoutingKey()
        {
            var factory = new ConnectionFactory { HostName = "192.168.2.103", UserName = "test", Password = "test" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.BasicPublish(string.Empty, string.Empty, null, null);
            }
        }
    }
}
