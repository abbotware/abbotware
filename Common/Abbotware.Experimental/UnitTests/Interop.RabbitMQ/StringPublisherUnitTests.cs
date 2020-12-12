namespace Abbotware.Tests.Interop.RabbitMQ
{
    using System.Threading;
    using Abbotware.Interop.RabbitMQ;
    using Abbotware.Interop.RabbitMQ.Config;
    using Abbotware.Interop.RabbitMQ.Plugins;
    using Abbotware.Interop.RabbitMQ.Plugins.Patterns.Specific;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StringPublisherGetterUnitTests : BaseRabbitUnitTest
    {
        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.StringPublisher/Retriever")]
        public void Publish_Getter()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var queueMananger = connection.CreateQueueManager())
            using (var exchangeMananger = connection.CreateExchangeManager())
            using (var publisher = new StringPublisher("test", connection.CreatePublishManager(), this.Logger))
            {
                exchangeMananger.Create(Templates.TemporaryExchange("test", ExchangeType.Topic));

                var info = queueMananger.Create(Templates.UnitTestQueue());
                queueMananger.Bind(new QueueBindingConfiguration { Action = BindingAction.Bind, DestinationQueue = info.Name, SourceExchange = "test", Topic = Constants.AllTopics });

                Thread.Sleep(500);

                var t1 = publisher.Publish("this is string 1");
                var t2 = publisher.Publish("this is string 2", "topic");
                t1.Wait();
                t2.Wait();

                using (var getter = new StringRetriever(info.Name, false, connection.CreateMessageRetrievalManager(), this.Logger))
                {
                    var r1 = getter.Retrieve();
                    Assert.IsNotNull(r1);
                    Assert.AreEqual("this is string 1", r1.Content);
                    getter.Ack(r1.DeliveryTag, false);

                    var r2 = getter.Retrieve();
                    Assert.IsNotNull(r2);
                    Assert.AreEqual("this is string 2", r2.Content);
                    getter.Ack(r2.DeliveryTag, false);
                }
            }
        }
    }
}