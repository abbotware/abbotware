// <copyright file="ObjectExtensionsTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>

namespace Abbotware.UnitTests.Core
{
    using System;
    using System.IO;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using Abbotware.Core.Serialization.Helpers;
    using NUnit.Framework;

    /// <summary>
    ///     Arguments class unit tests
    /// </summary>
    [TestFixture]
    [Category("Core")]
    [Category("Core.Extensions")]
    public class ObjectExtensionsTests
    {
        [Test]
        public void XmlConversionTests()
        {
            var sample = new SerailizableType
            {
                Attribute1 = "sdf",
            };

            var xDoc = sample.ToXDocument();
            Assert.IsNotNull(xDoc);

            var xElement = sample.ToXElement();
            Assert.IsNotNull(xElement);

            Assert.IsTrue(XNode.DeepEquals(xDoc.Root, xElement));

            var xmlDoc = sample.ToXmlDocument();
            Assert.IsNotNull(xmlDoc);

            var xmlString = sample.ToStringViaXmlSerializer();
            Assert.IsNotNull(xmlString);

            using var stringW = new StringWriter();
            xmlDoc.ToXDocument().Save(stringW);

            Assert.AreEqual(xmlString, stringW.ToString());
        }

        [Serializable]
        public class SerailizableType
        {
            [XmlElement]
            public string Name { get; set; }

            [XmlAttribute]
            public string Attribute1 { get; set; }
        }
    }
}