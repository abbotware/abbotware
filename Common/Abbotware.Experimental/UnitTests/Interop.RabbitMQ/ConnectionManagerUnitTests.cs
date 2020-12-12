namespace Abbotware.Tests.Interop.RabbitMQ
{
    using System;
    using Abbotware.Interop.RabbitMQ.Plugins;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ConnectionManagerUnitTests : BaseRabbitUnitTest
    {
        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.ConnectionManager")]
        public void CreateWithoutConnecting()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            {
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.ConnectionManager")]
        public void ConnectWithoutHeartBeat()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            {
                connection.Initialize();
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.ConnectionManager")]
        public void ConnectWithHeartBeat()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            {
                connection.Initialize();
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.ConnectionManager")]
        public void InitializeTwice()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            {
                connection.Initialize();
                connection.Initialize();
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.ConnectionManager")]
        public void MultipleDispose()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            {
                connection.Dispose();
                connection.Dispose();
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.ConnectionManager")]
        public void DisposeAfterConnect()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            {
                connection.Initialize();
                connection.Dispose();
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.ConnectionManager")]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void ConnectAfterDispose()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            {
                connection.Dispose();
                connection.Initialize();
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.ConnectionManager")]
        public void CreateDefaultChannels()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreatePublishManager())
            using (var channel2 = connection.CreateExchangeManager())
            using (var channel3 = connection.CreateQueueManager())
            {
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.ConnectionManager")]
        public void CreateDefaultChannelsDispose()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreateMessageRetrievalManager())
            using (var channel2 = connection.CreateConsumerManager())
            using (var channel3 = connection.CreateAcknowledgementManager())
            {
                channel2.Dispose();
                connection.Dispose();
                channel.Dispose();
                channel3.Dispose();
            }
        }
    }
}