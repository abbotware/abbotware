namespace Abbotware.UnitTests.Messaging
{
    using System;
    using Abbotware.Core.Implementation;
    using Abbotware.Interop.RabbitMQ;
    using Abbotware.Interop.RabbitMQ.Config;
    using Abbotware.Interop.RabbitMQ.Patterns;
    using Abbotware.Messaging.Interop.RabbitMQ.Protocol;
    using Abbotware.UnitTests.Interop.RabbitMQ;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ConsumerUnitTests : BaseRabbitUnitTest
    {
        [TestMethod]
        public void CreateConsumer()
        {
            using (var connection = Container.Resolve<RabbitMQConnection>())
            using (var defaultchannel = connection.CreateChannel<IConsumerManager>()) 
            {
                var consumer = new CallbackConsumer((a,b,c,d,e,f,g, h) => { return; }, defaultchannel, NullLogger.Instance);
                Assert.AreEqual(ConsumerStatus.Unknown, consumer.Status);
                Assert.IsNull(consumer.ConsumerTag);
                Assert.IsNull(consumer.ShutdownReason);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Cancel_NonExistantConsumer()
        {
            using (var connection = Container.Resolve<RabbitMQConnection>())
            using (var cm = connection.CreateChannel<IConsumerManager>())
            {
                cm.Cancel("does not exist");
            }
        }

        [TestMethod]
        public void StartCancel()
        {
            for (int i = 0; i < 100; ++i)
            {
                using (var connection = Container.Resolve<RabbitMQConnection>())
                using (var fullchannel = connection.CreateChannel<IConsumerManager>())
                {
                    var cm = fullchannel as IConsumerManager;
                    var qm = fullchannel as IQueueManager;

                    var consumer = new CallbackConsumer((a, b, c, d, e, f, g, h) => { return; }, fullchannel, NullLogger.Instance);
                    Assert.AreEqual(ConsumerStatus.Unknown, consumer.Status);
                    Assert.IsNull(consumer.ConsumerTag);
                    Assert.IsNull(consumer.ShutdownReason);

                    var qinfo = qm.Create(Templates.UnitTestQueue());

                    var ctag = cm.Start(qinfo.Name, true, consumer);
                    Assert.AreEqual(ConsumerStatus.Running, consumer.Status);
                    Assert.AreEqual(ctag, consumer.ConsumerTag);
                    Assert.IsNull(consumer.ShutdownReason);

                    cm.Cancel(ctag);
                    Assert.AreEqual(ConsumerStatus.Canceled, consumer.Status);
                    Assert.IsNull(consumer.ShutdownReason);
                }
            }
        }

        
        [TestMethod]
        public void CreateConsumer1()
        {
            using (var connection = Container.Resolve<RabbitMQConnection>())
            using (var pub = connection.CreateChannel<IPublishManager>())
            using (var sub = connection.CreateChannel<IMessageRetrievalManager>())
            using (var queueMananger = connection.CreateChannel<IQueueManager>())
            using (var exchangeMananger = connection.CreateChannel < IExchangeManager>())
            using (var publisher = new ExchangePublisher<PocoDataContractSerialization>("test", pub, NullLogger.Instance))
          
            {
                exchangeMananger.Create(Templates.TemporaryExchange("test", Abbotware.Interop.RabbitMQ.ExchangeType.Topic));

                var info = queueMananger.Create(Templates.UnitTestQueue());
                queueMananger.Bind(new QueueBindingConfiguration { Action = BindingAction.Bind, DestinationQueue= info.Name, SourceExchange= "test", Topic = Constants.AllTopics});

                System.Threading.Thread.Sleep(500);

                var t1 = publisher.Publish("asdf1");
                var t2 = publisher.Publish("asdf2", "asdfasd");
                t1.Wait();
                t2.Wait();

                using (var getter = new QueueRetriever<PocoDataContractSerialization>(info.Name, false, sub, NullLogger.Instance))
                {
                    var r1 = getter.Retrieve<string>();
                    Assert.IsNotNull(r1);
                    Assert.AreEqual("asdf1", r1.Message);
                    getter.Ack(r1.DeliveryTag, false);

                    var r2 = getter.Retrieve();
                    Assert.IsNotNull(r2);
                    Assert.AreEqual("asdf2", r2.Message);
                    getter.Ack(r2.DeliveryTag, false);
                }
            }
        }
    }
}
