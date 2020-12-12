namespace Abbotware.Tests.Interop.RabbitMQ
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using Abbotware.Interop.RabbitMQ.Plugins.MessageProtocol.Generic;
    using Abbotware.Interop.RabbitMQ.Plugins.MessageProtocol.Specific;
    using global::RabbitMQ.Client.Framing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProtocolUnitTests
    {
        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.Protocol")]
        public void DataContract_Tests()
        {
            var initial = new TestClass {D = DateTime.Now, Ignored = 22, S = "string"};

            var protocol = new PocoDataContractSerialization();
            var pubconfig = protocol.Encode(initial);

            var props = new BasicProperties();
            props.Headers = new Dictionary<string, object>();
            props.Headers["AssemblyQualifiedName"] = initial.GetType().AssemblyQualifiedName;
            var decoded = protocol.Decode<TestClass>(pubconfig.Body, string.Empty, string.Empty, props);

            Assert.AreEqual(initial.D, decoded.D);
            Assert.AreEqual(initial.S, decoded.S);
            Assert.AreEqual(0, decoded.Ignored);
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.Protocol")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullDataContract()
        {
            var protocol = new PocoDataContractSerialization();
            var pubconfig = protocol.Encode<TestClass>(null);
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.Protocol")]
        public void Xml_Tests()
        {
            var initial = new TestClass {D = DateTime.Now, Ignored = 22, S = "string"};

            var protocol = new PocoXmlSerialization();
            var pubconfig = protocol.Encode(initial);

            var props = new BasicProperties();

            props.Headers = new Dictionary<string, object>();
            props.Headers["AssemblyQualifiedName"] = initial.GetType().AssemblyQualifiedName;
            var decoded = protocol.Decode<TestClass>(pubconfig.Body, string.Empty, string.Empty, props);

            Assert.AreEqual(initial.D, decoded.D);
            Assert.AreEqual(initial.S, decoded.S);
            Assert.AreEqual(0, decoded.Ignored);
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.Protocol")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullXml()
        {
            var protocol = new PocoXmlSerialization();
            var pubconfig = protocol.Encode<TestClass>(null);
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.Protocol")]
        public void String_Tests()
        {
            var protocol = new StringProtocol();
            var pubconfig = protocol.Encode("this is a test");
            var props = new BasicProperties();
            var decoded = protocol.Decode(pubconfig.Body, string.Empty, string.Empty, props);
            Assert.AreEqual("this is a test", decoded);

            pubconfig = protocol.Encode("");
            decoded = protocol.Decode(pubconfig.Body, string.Empty, string.Empty, props);
            Assert.AreEqual("", decoded);
        }


        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.Protocol")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullString()
        {
            var protocol = new StringProtocol();
            var pubconfig = protocol.Encode(null);
        }


        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.Protocol")]
        public void BinaryFormatter_Tests()
        {
            var initial = new TestClass {D = DateTime.Now, Ignored = 22, S = "string"};

            var protocol = new PocoBinaryFormatter();
            var pubconfig = protocol.Encode(initial);

            var props = new BasicProperties();

            props.Headers = new Dictionary<string, object>();
            props.Headers["AssemblyQualifiedName"] = initial.GetType().AssemblyQualifiedName;
            var decoded = protocol.Decode<TestClass>(pubconfig.Body, string.Empty, string.Empty, props);

            Assert.AreEqual(initial.D, decoded.D);
            Assert.AreEqual(initial.S, decoded.S);
            Assert.AreEqual(22, decoded.Ignored);
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.Protocol")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullPocoBinaryFormatter()
        {
            var protocol = new PocoBinaryFormatter();
            var pubconfig = protocol.Encode<TestClass>(null);
        }


        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.Protocol")]
        public void NetDataContract_Tests()
        {
            var initial = new TestClass {D = DateTime.Now, Ignored = 22, S = "string"};

            var protocol = new PocoNetDataContractSerialization();
            var pubconfig = protocol.Encode(initial);

            var props = new BasicProperties();

            props.Headers = new Dictionary<string, object>();
            props.Headers["AssemblyQualifiedName"] = initial.GetType().AssemblyQualifiedName;
            var decoded = protocol.Decode<TestClass>(pubconfig.Body, string.Empty, string.Empty, props);

            Assert.AreEqual(initial.D, decoded.D);
            Assert.AreEqual(initial.S, decoded.S);
            Assert.AreEqual(0, decoded.Ignored);
        }

        [TestMethod]
        [TestCategory("Interop.RabbitMQ")]
        [TestCategory("Interop.RabbitMQ.Protocol")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullNetDataContract()
        {
            var protocol = new PocoNetDataContractSerialization();
            var pubconfig = protocol.Encode<TestClass>(null);
        }

        [DataContract]
        [Serializable]
        public class TestClass
        {
            [DataMember]
            public string S { get; set; }

            [DataMember]
            public DateTime D { get; set; }

            [XmlIgnore]
            public int Ignored { get; set; }
        }
    }
}