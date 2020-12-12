namespace Abbotware.Tests.Interop.RabbitMQ
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Messaging;
    using Abbotware.Core.Threading.Counters;
    using Abbotware.Interop.RabbitMQ;
    using Abbotware.Interop.RabbitMQ.Config;
    using Abbotware.Interop.RabbitMQ.ExtensionPoints;
    using Abbotware.Interop.RabbitMQ.Plugins;
    using global::RabbitMQ.Client.Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PublishManagerUnitTests : BaseRabbitUnitTest
    {
        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.PublishManager")]
        public void Publish_DefaultExchange_Mode_Notspecified()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var defaultchannel = connection.CreatePublishManager())
            {
                var list = new Queue<Task<PublishStatus>>();

                for (var i = 0; i < 1000; ++i)
                {
                    var data = new byte[2] { 0, 1 };

                    var p1 = defaultchannel.Publish(Constants.DefaultExchange, "a", true, null, data);
                    list.Enqueue(p1);
                    var p2 = defaultchannel.Publish(Constants.DefaultExchange, "b", false, null, data);
                    list.Enqueue(p2);
                }

                while (list.Count > 0)
                {
                    var statusFuture = list.Dequeue();
                    Assert.AreEqual(PublishStatus.Unknown, statusFuture.Result);
                }

                Thread.Sleep(5000);

                Assert.AreEqual(2000, defaultchannel.PublishedMessages);
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.PublishManager")]
        public void Publish_DefaultExchange_Mode_Confirm()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreatePublishManager(Templates.Channel.Default(ChannelMode.ConfirmationMode)))
            {
                var list = new Queue<Task<PublishStatus>>();

                for (var i = 0; i < 1000; ++i)
                {
                    var p1 = channel.Publish(Constants.DefaultExchange, "a", true, null, null);
                    list.Enqueue(p1);
                }

                for (var i = 0; i < 500; ++i)
                {
                    var p4 = channel.Publish(Constants.DefaultExchange, "b", false, null, null);
                    list.Enqueue(p4);
                }

                var k = 0;
                while (list.Count > 0)
                {
                    var statusFuture = list.Dequeue();
                    if (k < 1000)
                    {
                        Assert.AreEqual(PublishStatus.Returned, statusFuture.Result);
                    }
                    else
                    {
                        Assert.AreEqual(PublishStatus.Confirmed, statusFuture.Result);
                    }
                    ++k;
                }

                Assert.AreEqual(1000, channel.ReturnedMessages);
                Assert.AreEqual(1000, channel.ReturnedNoRoute);

                Assert.AreEqual(1500, channel.PublishedMessages);
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.PublishManager")]
        public void Publish_DefaultExchange_Mode_Transaction()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreatePublishManager(Templates.Channel.Default(ChannelMode.TransactionMode)))
            {
                var list = new Queue<Task<PublishStatus>>();

                for (var i = 0; i < 1000; ++i)
                {
                    var p1 = channel.Publish(Constants.DefaultExchange, "a", true, null, null);
                    list.Enqueue(p1);
                }

                for (var i = 0; i < 700; ++i)
                {
                    var p4 = channel.Publish(Constants.DefaultExchange, "b", false, null, null);
                    list.Enqueue(p4);
                }

                while (list.Count > 0)
                {
                    var statusFuture = list.Dequeue();
                    Assert.AreEqual(PublishStatus.Unknown, statusFuture.Result);
                }

                Assert.AreEqual(1000, channel.ReturnedMessages);
                Assert.AreEqual(1000, channel.ReturnedNoRoute);
                Assert.AreEqual(1700, channel.PublishedMessages);
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.PublishManager")]
        [ExpectedException(typeof(global::RabbitMQ.Client.Exceptions.AlreadyClosedException))]
        public void Publish_Exchange_DNE_Mode_Unspecified()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreatePublishManager(Templates.Channel.Default(ChannelMode.None)))
            {
                for (var i = 0; i < 1000; ++i)
                {
                    channel.Publish("dne", "a", true, null, null);
                    channel.Publish("dne", "c", false, null, null);
                }
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.PublishManager")]
        [ExpectedException(typeof(global::RabbitMQ.Client.Exceptions.AlreadyClosedException))]
        public void Publish_Exchange_DNE_Mode_Confirm()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreatePublishManager(Templates.Channel.Default(ChannelMode.ConfirmationMode)))
            {
                for (var i = 0; i < 1000; ++i)
                {
                    channel.Publish("dne", "a", true, null, null);
                    channel.Publish("dne", "d", false, null, null);
                }
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.PublishManager")]
        [ExpectedException(typeof(global::RabbitMQ.Client.Exceptions.AlreadyClosedException))]
        public void Publish_Exchange_DNE_Mode_Transaction()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreatePublishManager(Templates.Channel.Default(ChannelMode.TransactionMode)))
            {
                for (var i = 0; i < 1000; ++i)
                {
                    channel.Publish("dne", "a", true, null, null);
                    channel.Publish("dne", "d", false, null, null);
                }
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.PublishManager")]
        public void Publish_Confirm_NoConsumer()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var p = connection.CreatePublishManager(Templates.Channel.Default(ChannelMode.ConfirmationMode)))
            using (var adminChannel = connection.CreateResourceManager())
            {
                var testX = Templates.TemporaryExchange("testX", ExchangeType.Topic);
                var testQ = Templates.UnitTestQueue("testQ");

                adminChannel.Create(testX);
                adminChannel.Create(testQ);
                adminChannel.Bind(new QueueBindingConfiguration { Action = BindingAction.Bind, SourceExchange = testX.Name, DestinationQueue = testQ.Name, Topic = Constants.AllTopics });

                var t = p.Publish(testX.Name, "asdf", true, new byte[10], TimeSpan.Zero);
                t.Wait();

                Assert.AreEqual(PublishStatus.Returned, t.Result);
            }
        }

        //[TestMethod]
        //public void Publish_Immediate_Consumer_NoAckMode()
        //{
        //    using (var connection = this.Container.Resolve<IRabbitMQChannelFactory>())
        //    using (var pm = connection.CreateChannel<IPublishManager>(Templates.DefaultChannel(ChannelMode.ConfirmationMode)))
        //    using (var cm = connection.CreateChannel<IConsumerManager>())
        //    using (var adminChannel = connection.CreateChannel<IResourceManager>())
        //    {
        //        var testX = Templates.TemporaryExchange("testX", ExchangeType.Topic);
        //        var testQ = Templates.UnitTestQueue("testQ");

        //        var counter1 = new AtomicIncrementerWaitEvent(1, this.Logger.CreateChildLogger("counter1"));
        //        var counter2 = new AtomicIncrementerWaitEvent(1, this.Logger.CreateChildLogger("counter2"));

        //        adminChannel.Create(testX);
        //        adminChannel.Create(testQ);
        //        adminChannel.Bind(new QueueBindingConfiguration { Action = BindingAction.Bind, SourceExchange = testX.Name, DestinationQueue = testQ.Name, Topic = Constants.AllTopics });

        //        var cb = new CallbackConsumer((a, b, c, d, e, f, g, h) =>
        //        {
        //            counter1.Increment();
        //            counter2.Increment();

        //        }, cm, NullLogger.Instance);

        //        cm.Start("testQ", false, cb);

        //        var t1 = pm.Publish(testX.Name, "asdf", true, true, null, new byte[10]);
        //        t1.Wait();
        //        Assert.AreEqual(PublishStatus.Confirmed, t1.Result);

        //        counter1.WaitOrThrow(TimeSpan.FromSeconds(5));

        //        var t2 = pm.Publish(testX.Name, "asdf", true, true, null, new byte[10]);
        //        t2.Wait();

        //        Assert.AreEqual(PublishStatus.Confirmed, t2.Result);

        //        counter2.WaitOrThrow(TimeSpan.FromSeconds(5));
        //    }
        //}

        //[TestMethod]
        //public void Publish_Immediate_Acked()
        //{
        //    using (var connection = this.Container.Resolve<IRabbitMQChannelFactory>())
        //    using (var pm = connection.CreateChannel<IPublishManager>(Templates.DefaultChannel(ChannelMode.ConfirmationMode)))
        //    using (var cm = connection.CreateChannel<IConsumerManager>())
        //    using (var adminChannel = connection.CreateChannel<IResourceManager>())
        //    {
        //        var am = cm as IAcknowledgementManager;

        //        var testX = Templates.TemporaryExchange("testX", ExchangeType.Topic);
        //        var testQ = Templates.UnitTestQueue("testQ");

        //        var counter1 = new AtomicIncrementerWaitEvent(1, this.Logger.CreateChildLogger("counter1"));
        //        var counter2 = new AtomicIncrementerWaitEvent(1, this.Logger.CreateChildLogger("counter2"));

        //        adminChannel.Create(testX);
        //        adminChannel.Create(testQ);
        //        adminChannel.Bind(new QueueBindingConfiguration { Action = BindingAction.Bind, SourceExchange = testX.Name, DestinationQueue = testQ.Name, Topic = Constants.AllTopics });

        //        var cb = new CallbackConsumer((a, b, c, d, e, f, g, h) =>
        //        {
        //            if (e == "m1")
        //            {
        //                counter1.Increment();
        //                am.Ack(b, false);
        //            }

        //            if (e == "m2")
        //            {
        //                counter2.Increment();
        //                am.Ack(b, false);
        //            }

        //        }, am, NullLogger.Instance);

        //        cm.Start("testQ", true, cb);

        //        var t1 = pm.Publish(testX.Name, "m1", true, true, null, new byte[10]);
        //        t1.Wait();
        //        Assert.AreEqual(PublishStatus.Confirmed, t1.Result);

        //        counter1.WaitOrThrow(TimeSpan.FromSeconds(5));

        //        var t2 = pm.Publish(testX.Name, "m2", true, true, null, new byte[10]);
        //        t2.Wait();

        //        Assert.AreEqual(PublishStatus.Confirmed, t2.Result);

        //        counter2.WaitOrThrow(TimeSpan.FromSeconds(5));
        //    }
        //}

        //[TestMethod]
        //public void PublishToDeadLetter()
        //{
        //    using (var connection = this.Container.Resolve<IRabbitMQChannelFactory>())
        //    using (var pm = connection.CreateChannel<IPublishManager>())
        //    using (var m = connection.CreateChannel<IMessageRetrievalManager>())
        //    using (var adminChannel = connection.CreateChannel<IResourceManager>())
        //    {
        //        var deadLetterX = Templates.TemporaryExchange("testDLX", ExchangeType.Topic);
        //        var deadLetterQ = Templates.UnitTestQueue("testDLQ");

        //        var testX = Templates.TemporaryExchange("testX", ExchangeType.Topic);
        //        var testQ = Templates.UnitTestQueue("testQ");

        //        testQ.DeadLetterExchange = deadLetterX.Name;
        //        testQ.DeadLetterRoutingKey = "rejected from testq";

        //        adminChannel.Create(testX);
        //        adminChannel.Create(testQ);
        //        adminChannel.Create(deadLetterX);
        //        adminChannel.Create(deadLetterQ);
        //        adminChannel.Bind(new QueueBindingConfiguration { Action = BindingAction.Bind, SourceExchange = deadLetterX.Name, DestinationQueue = deadLetterQ.Name, Topic = Constants.AllTopics });
        //        adminChannel.Bind(new QueueBindingConfiguration { Action = BindingAction.Bind, SourceExchange = testX.Name, DestinationQueue = testQ.Name, Topic = Constants.AllTopics });

        //        var t = pm.Publish(testX.Name, "asdf", false, true, null, new byte[10]);
        //        t.Wait();

        //        var result = m.Retrieve(testQ.Name, false);
        //        Assert.IsNotNull(result);

        //        //Assert.AreEqual(result.DeliveryTag.ToString(), result.BasicProperties.MessageId);

        //        m.Reject(result.DeliveryTag, false);

        //        // there is a delay moving the message to the DLQ
        //        System.Threading.Thread.Sleep(100);

        //        var result2 = m.Retrieve(deadLetterQ.Name, true);
        //        Assert.IsNotNull(result2);
        //    }
        //}
    }
}