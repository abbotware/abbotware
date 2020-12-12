namespace Abbotware.Tests.Interop.RabbitMQ
{
    using System;
    using System.Threading;
    using Abbotware.Interop.RabbitMQ;
    using Abbotware.Interop.RabbitMQ.Config;
    using Abbotware.Interop.RabbitMQ.ExtensionPoints;
    using Abbotware.Interop.RabbitMQ.Plugins;
    using Abbotware.UnitTest;
    using global::RabbitMQ.Client.Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class QueueManagerUnitTests : BaseRabbitUnitTest
    {
        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.QueueManager")]
        public void CreateDeleteQueue()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreateQueueManager())

            {
                for (var i = 0; i < 100; ++i)
                {
                    channel.Create(Templates.UnitTestQueue());
                    var qInfo = channel.Create(Templates.ConsumerQueue());
                    channel.Delete(qInfo.Name, true, true);
                }
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.QueueManager")]
        public void TestQueueExists()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreateQueueManager())

            {
                for (var i = 0; i < 100; ++i)
                {
                    Assert.IsFalse(channel.QueueExists("does not exist"));
                    var qInfo = channel.Create(Templates.UnitTestQueue());
                    Assert.IsTrue(channel.QueueExists(qInfo.Name));
                }
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.QueueManager")]
        [ExpectedException(typeof(OperationInterruptedException))]
        public void CreateQueueBindings_QueueDoesNotExist()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreateQueueManager())

            {
                for (var i = 0; i < 100; ++i)
                {
                    channel.Bind(new QueueBindingConfiguration {Action = BindingAction.Bind, DestinationQueue = "doesnot", SourceExchange = "dne", Topic = Constants.AllTopics});
                }
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.QueueManager")]
        [ExpectedException(typeof(OperationInterruptedException))]
        public void CreateQueueBindings_ExchangeDoesNotExist()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreateQueueManager())

            {
                for (var i = 0; i < 100; ++i)
                {
                    var qInfo = channel.Create(Templates.UnitTestQueue());
                    channel.Bind(new QueueBindingConfiguration {Action = BindingAction.Bind, DestinationQueue = qInfo.Name, SourceExchange = "dne", Topic = Constants.AllTopics});
                }
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.QueueManager")]
        [ExpectedException(typeof(OperationInterruptedException))]
        public void CreateQueueUnbind_QueueDoesNotExist()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreateQueueManager())

            {
                for (var i = 0; i < 100; ++i)
                {
                    channel.Bind(new QueueBindingConfiguration {Action = BindingAction.Unbind, DestinationQueue = "doesnot", SourceExchange = "dne", Topic = Constants.AllTopics});
                }
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.QueueManager")]
        [ExpectedException(typeof(OperationInterruptedException))]
        public void CreateQueueUnBind_ExchangeDoesNotExist()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreateQueueManager())

            {
                for (var i = 0; i < 100; ++i)
                {
                    var qInfo = channel.Create(Templates.UnitTestQueue());
                    channel.Bind(new QueueBindingConfiguration {Action = BindingAction.Unbind, DestinationQueue = qInfo.Name, SourceExchange = "dne", Topic = Constants.AllTopics});
                }
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.QueueManager")]
        [ExpectedContractFailure]
        public void CreateQueueBindings_DefaultExchange()
        {
            var config = new QueueBindingConfiguration {Action = BindingAction.Bind, DestinationQueue = "any", SourceExchange = Constants.DefaultExchange, Topic = ""};
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.QueueManager")]
        public void CreateQueueBindings()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreateQueueManager())

            using (var ex = channel as IExchangeManager)
            {
                for (var i = 0; i < 100; ++i)
                {
                    ex.Create(new ExchangeConfiguration {Name = "test", IsAutoDelete = true, IsDurable = false, ExchangeType = ExchangeType.Topic});

                    var qinfo = channel.Create(Templates.UnitTestQueue());

                    channel.Bind(new QueueBindingConfiguration {Action = BindingAction.Bind, DestinationQueue = qinfo.Name, SourceExchange = "test", Topic = "ddd"});
                    channel.Bind(new QueueBindingConfiguration {Action = BindingAction.Bind, DestinationQueue = qinfo.Name, SourceExchange = "test", Topic = "#"});
                    channel.Bind(new QueueBindingConfiguration {Action = BindingAction.Bind, DestinationQueue = qinfo.Name, SourceExchange = "test", Topic = "ddd"});
                    channel.Bind(new QueueBindingConfiguration {Action = BindingAction.Bind, DestinationQueue = qinfo.Name, SourceExchange = "test", Topic = "#3.3"});

                    Assert.IsTrue(channel.QueueExists(qinfo.Name));

                    channel.Bind(new QueueBindingConfiguration {Action = BindingAction.Unbind, DestinationQueue = qinfo.Name, SourceExchange = "test", Topic = "ddd"});
                    channel.Bind(new QueueBindingConfiguration {Action = BindingAction.Unbind, DestinationQueue = qinfo.Name, SourceExchange = "test", Topic = "#"});
                    channel.Bind(new QueueBindingConfiguration {Action = BindingAction.Unbind, DestinationQueue = qinfo.Name, SourceExchange = "test", Topic = "#3.3"});
                }
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.QueueManager")]
        [ExpectedException(typeof(OperationInterruptedException))]
        public void UnbindTopicsThatDontExist()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreateQueueManager())

            {
                for (var i = 0; i < 100; ++i)
                {
                    var qInfo = channel.Create(Templates.UnitTestQueue());

                    channel.Bind(new QueueBindingConfiguration {Action = BindingAction.Unbind, DestinationQueue = qInfo.Name, SourceExchange = "test", Topic = "ddd"});
                    channel.Bind(new QueueBindingConfiguration {Action = BindingAction.Unbind, DestinationQueue = qInfo.Name, SourceExchange = "test", Topic = "#"});
                    channel.Bind(new QueueBindingConfiguration {Action = BindingAction.Unbind, DestinationQueue = qInfo.Name, SourceExchange = "test", Topic = "#3.3"});

                    Assert.IsTrue(channel.QueueExists(qInfo.Name));
                }
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.QueueManager")]
        public void CreateQueueWitExpiration()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreateQueueManager())

            {
                for (var i = 0; i < 2; ++i)
                {
                    var cfg = Templates.UnitTestQueue("dltest", TimeSpan.FromMilliseconds(500));

                    Assert.IsFalse(channel.QueueExists(cfg.Name));
                    channel.Create(cfg);
                    Assert.IsTrue(channel.QueueExists(cfg.Name));
                    Thread.Sleep(1000);
                    Assert.IsFalse(channel.QueueExists(cfg.Name));
                }
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.QueueManager")]
        public void CreateQueueWithDeadLetterExchangeAndRoutingKey()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreateQueueManager())
            {
                for (var i = 0; i < 100; ++i)
                {
                    var cfg = Templates.UnitTestQueue("dltest");

                    cfg.DeadLetterExchange = "Dead Letter Exchange";
                    cfg.DeadLetterRoutingKey = "new routing key";

                    channel.Create(cfg);
                    Assert.IsTrue(channel.QueueExists(cfg.Name));
                    channel.Delete(cfg.Name, false, false);
                }
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.QueueManager")]
        public void PurgeTest()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreateQueueManager())
            using (var p = connection.CreatePublishManager(new ChannelConfiguration {Mode = ChannelMode.ConfirmationMode}))
            {
                for (var i = 0; i < 5; ++i)
                {
                    var cfg = Templates.UnitTestQueue("dltest");

                    channel.Create(cfg);
                    Assert.IsTrue(channel.QueueExists(cfg.Name));
                    channel.Purge(cfg.Name);

                    for (var j = 0; j < 50; ++j)
                    {
                        var pub = p.Publish(Constants.DefaultExchange, cfg.Name, false, null, new byte[30]);
                        pub.Wait();
                    }

                    var result = channel.Purge(cfg.Name);
                    Assert.AreEqual(50U, result);

                    channel.Delete(cfg.Name, false, false);
                }
            }
        }
    }
}