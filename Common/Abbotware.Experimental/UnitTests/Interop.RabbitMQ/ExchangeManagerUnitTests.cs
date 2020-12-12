namespace Abbotware.Tests.Interop.RabbitMQ
{
    using System;
    using Abbotware.Interop.RabbitMQ;
    using Abbotware.Interop.RabbitMQ.Config;
    using Abbotware.Interop.RabbitMQ.Plugins;
    using global::RabbitMQ.Client.Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExchangeManagerUnitTests : BaseRabbitUnitTest
    {
        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.ExchangeManager")]
        public void TestExchange_Exists_Create_Delete()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreateExchangeManager())

            {
                for (var i = 0; i < 100; ++i)
                {
                    Assert.IsFalse(channel.ExchangeExists("TestExchangeExists"));
                    channel.Create(Templates.PersistentExchange("TestExchangeExists", ExchangeType.Topic));
                    Assert.IsTrue(channel.ExchangeExists("TestExchangeExists"));
                    channel.Delete("TestExchangeExists", false);
                }
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.ExchangeManager")]
        [ExpectedException(typeof(OperationInterruptedException))]
        public void CreateExchangeBindings_BothExchangesDontExist()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreateExchangeManager())

            {
                for (var i = 0; i < 100; ++i)
                {
                    channel.Bind(new ExchangeBindingConfiguration {Action = BindingAction.Bind, SourceExchange = "dne1", DestinationExchange = "dne2", Topic = Constants.AllTopics});
                }
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.ExchangeManager")]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateExchangeBindings_DestinationDefault()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreateExchangeManager())

            {
                channel.Bind(new ExchangeBindingConfiguration {Action = BindingAction.Bind, SourceExchange = "asdf", DestinationExchange = Constants.DefaultExchange, Topic = Constants.AllTopics});
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.ExchangeManager")]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateExchangeBindings_SourceDefault()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreateExchangeManager())

            {
                channel.Bind(new ExchangeBindingConfiguration {Action = BindingAction.Bind, SourceExchange = Constants.DefaultExchange, DestinationExchange = "asdfasd", Topic = Constants.AllTopics});
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.ExchangeManager")]
        [ExpectedException(typeof(OperationInterruptedException))]
        public void CreateExchangeBindings_Dest_DoesnotExist()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreateExchangeManager())

            {
                channel.Create(Templates.TemporaryExchange("asdf", ExchangeType.Topic));
                channel.Bind(new ExchangeBindingConfiguration {Action = BindingAction.Bind, SourceExchange = "asdf", DestinationExchange = "dne2", Topic = Constants.AllTopics});
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.ExchangeManager")]
        [ExpectedException(typeof(OperationInterruptedException))]
        public void CreateExchangeBindings_Src_DoesnotExist()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreateExchangeManager())

            {
                channel.Create(Templates.TemporaryExchange("asdf", ExchangeType.Topic));
                channel.Bind(new ExchangeBindingConfiguration {Action = BindingAction.Bind, SourceExchange = "dne", DestinationExchange = "asdf", Topic = Constants.AllTopics});
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.ExchangeManager")]
        public void CreateTempExchangeBinding()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreateExchangeManager())

            {
                for (var i = 0; i < 100; ++i)
                {
                    channel.Create(Templates.TemporaryExchange("ex1", ExchangeType.Topic));
                    channel.Create(Templates.TemporaryExchange("ex2", ExchangeType.Topic));
                    channel.Bind(new ExchangeBindingConfiguration {Action = BindingAction.Bind, SourceExchange = "ex1", DestinationExchange = "ex2", Topic = "e1->e2"});
                    channel.Bind(new ExchangeBindingConfiguration {Action = BindingAction.Bind, SourceExchange = "ex2", DestinationExchange = "ex1", Topic = "e2->e1"});

                    channel.Bind(new ExchangeBindingConfiguration {Action = BindingAction.Unbind, SourceExchange = "ex1", DestinationExchange = "ex2", Topic = "e1->e2"});
                }
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.ExchangeManager")]
        public void CreateDurableExchangeBinding()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreateExchangeManager())

            {
                channel.Delete("ex1", false);
                channel.Delete("ex2", false);

                for (var i = 0; i < 100; ++i)
                {
                    channel.Create(Templates.PersistentExchange("ex1", ExchangeType.Topic));
                    channel.Create(Templates.PersistentExchange("ex2", ExchangeType.Topic));
                    channel.Bind(new ExchangeBindingConfiguration {Action = BindingAction.Bind, SourceExchange = "ex1", DestinationExchange = "ex2", Topic = "e1->e2"});
                    channel.Bind(new ExchangeBindingConfiguration {Action = BindingAction.Bind, SourceExchange = "ex2", DestinationExchange = "ex1", Topic = "e2->e1"});

                    channel.Bind(new ExchangeBindingConfiguration {Action = BindingAction.Unbind, SourceExchange = "ex1", DestinationExchange = "ex2", Topic = "e1->e2"});
                    channel.Bind(new ExchangeBindingConfiguration {Action = BindingAction.Unbind, SourceExchange = "ex2", DestinationExchange = "ex1", Topic = "e2->e1"});

                    channel.Delete("ex1", false);
                    channel.Delete("ex2", false);
                }
            }
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.ExchangeManager")]
        [ExpectedException(typeof(OperationInterruptedException))]
        public void Exchange_UnbindTopicsThatDontExist()
        {
            using (var connection = this.Container.Resolve<ConnectionManager>())
            using (var channel = connection.CreateExchangeManager())
            {
                for (var i = 0; i < 100; ++i)
                {
                    channel.Create(Templates.TemporaryExchange("ex1", ExchangeType.Topic));
                    channel.Create(Templates.TemporaryExchange("ex2", ExchangeType.Topic));
                    channel.Bind(new ExchangeBindingConfiguration {Action = BindingAction.Unbind, SourceExchange = "ex1", DestinationExchange = "ex2", Topic = "e1->e2"});
                }
            }
        }
    }
}