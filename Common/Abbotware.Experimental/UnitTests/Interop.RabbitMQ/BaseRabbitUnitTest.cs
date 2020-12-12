namespace Abbotware.Tests.Interop.RabbitMQ
{
    using System.Net;
    using Abbotware.Interop.RabbitMQ.Config;
    using Abbotware.Interop.RabbitMQ.ExtensionPoints;
    using Abbotware.Interop.RabbitMQ.Plugins;
    using Abbotware.UnitTest;
    using global::Castle.MicroKernel.Registration;
    using global::Castle.Windsor;
    using global::RabbitMQ.Client;
    using IConnection = Abbotware.Interop.RabbitMQ.ExtensionPoints.IConnection;

    /// <summary>
    ///     Unit test base class for RabbitMQ unit tests
    /// </summary>
    public class BaseRabbitUnitTest : ContainerUnitTestBase
    {
        private readonly ConnectionManagerConfiguration config = new ConnectionManagerConfiguration(new NetworkCredential("test", "test"), new AmqpTcpEndpoint("192.168.2.103"));

        protected override void OnInstall(IWindsorContainer container)
        {
            base.OnInstall(container);

            container.Register(Component.For<ConnectionManager, IConnection>().ImplementedBy<ConnectionManager>().LifeStyle.Transient);
            container.Register(Component.For<ConnectionManagerConfiguration>().Instance(this.config));
        }
    }
}