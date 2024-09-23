//-----------------------------------------------------------------------
// <copyright file="InteropRedisTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.UnitTests.Interop.Redis
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Objects.Configuration.Models;
    using Abbotware.Interop.NUnit;
    using Abbotware.Interop.Redis;
    using Abbotware.Interop.Redis.Configuration;
    using Abbotware.Utility.UnitTest;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using global::NUnit.Framework;

    [TestFixture]
    [Category("Interop")]
    [Category("Interop.Redis")]
    public class RedisTests : BaseNUnitTest
    {
        [Test]
        public void Configuration()
        {
            {
                var cfg = RedisHelper.GetRedisConfiguration(Path.Combine("Redis", "redis.localhost.json"));
                Assert.That("127.1.2.3", Is.EqualTo(cfg.Endpoint.Host));
                Assert.That(1234, Is.EqualTo(cfg.Endpoint.Port));
                Assert.That("redis", Is.EqualTo(cfg.Endpoint.Scheme));
                Assert.That("user", Is.EqualTo(cfg.Credential!.UserName));
                Assert.That("pass", Is.EqualTo(cfg.Credential.Password));
                Assert.That(cfg.Endpoint.UserInfo, Is.Empty);
            }

            {
                var cfg = new ConnectionOptions(new Uri("redis://127.0.0.1:6379"), Defaults.Credential);

                Assert.That("redis://guest:guest@127.0.0.1:6379/", Is.EqualTo(cfg.ToServiceStackUri().ToString()));
            }

            {
                var cfg = new ConnectionOptions(new Uri("redis://127.0.0.1:6379"), Defaults.Credential);

                Assert.That("redis://guest:guest@127.0.0.1:6379/", Is.EqualTo(cfg.ToServiceStackUri().ToString()));
            }

            {
                var cfg = new ConnectionOptions(new Uri("redis://127.0.0.1"), Defaults.Credential);

                Assert.That("redis://guest:guest@127.0.0.1:6379/", Is.EqualTo(cfg.ToServiceStackUri().ToString()));
            }

            {
                var cfg = new ConnectionOptions(new Uri("redis://127.0.0.1/asdf/asdf/"), Defaults.Credential);

                Assert.That("redis://guest:guest@127.0.0.1:6379/", Is.EqualTo(cfg.ToServiceStackUri().ToString()));
            }

            {
                var cfg = new ConnectionOptions(new Uri("redis://127.0.0.1/asdf/asdf/"), new NetworkCredential("asdf", "qwer"));

                Assert.That("redis://asdf:qwer@127.0.0.1:6379/", Is.EqualTo(cfg.ToServiceStackUri().ToString()));
            }

            {
                var cfg = new ConnectionOptions(new Uri("redis://127.0.0.1/asdf/asdf/?ssl=true&db=1"), new NetworkCredential("asdf", "qwer"));

                Assert.That("redis://asdf:qwer@127.0.0.1:6379/?ssl=true&db=1", Is.EqualTo(cfg.ToServiceStackUri().ToString()));
            }
        }

        [Test]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void HashKeyValueCollection_KeyNotExist()
        {
            var encoder = new HashKeyValueStore();
            encoder.DecodeInt32("someInt");
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void HashKeyValueCollection_KeyAddTwice()
        {
            var encoder = new HashKeyValueStore();
            encoder.EncodeInt32("someInt", 123456);
            encoder.EncodeInt32("someInt", 123456);
        }

        [Test]
        public void HashKeyValueCollection_TestCases()
        {
            var encoder = new HashKeyValueStore();

            this.KeyValueEncoderTestCases(encoder);
        }
    }
}