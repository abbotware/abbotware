namespace Abbotware.UnitTests.Messaging
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Abbotware.Messaging;
    using Castle.Windsor;
    using Castle.MicroKernel.Registration;
    using System.Net;
    using System;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Exceptions;
    using Abbotware.UnitTests.Messaging;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Abbotware.Interop.RabbitMQ;
    using Abbotware.Interop.RabbitMQ.Config;
    using Abbotware.Interop.RabbitMQ.Patterns;
    using Abbotware.Messaging.Interop.RabbitMQ.Protocol;
    using Castle.Core.Logging;
    using Abbotware.UnitTests.Interop.RabbitMQ;

    [TestClass]
    public class ExchangePublisherUnitTests : BaseRabbitUnitTest
    {
        [TestMethod]
        public void ExchangePublisher_Strings()
        {
            using (var container = SetUpContainer(rconfig))
            using (var connection = container.Resolve<RabbitMQConnection>())
            using (var defaultchannel = connection.CreateChannel())
            using (var queueMananger = connection.CreateChannel() as IQueueManager)
            using (var exchangeMananger = connection.CreateChannel() as IExchangeManager)
            using (var publisher = new MultiPocoPublisher<PocoDataContractSerialization>("test", defaultchannel, NullLogger.Instance))
            {
                exchangeMananger.Create(ExchangeConfiguration.Templates.Temporary("test", Interop.RabbitMQ.ExchangeType.Topic));

                var info = queueMananger.Create(QueueConfiguration.Templates.UnitTest());
                queueMananger.Bind(new QueueBindingConfiguration { Action = BindingAction.Bind, Queue = info.Name, Exchange = "test", Topic = Constants.AllTopics });

                System.Threading.Thread.Sleep(500);

                var t1 = publisher.Publish("asdf1");
                var t2 = publisher.Publish("asdf2", "asdfasd");
                t1.Wait();
                t2.Wait();

                using (var getter = new MultiPocoGetter<PocoDataContractSerialization>(info.Name, false, defaultchannel, NullLogger.Instance))
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
