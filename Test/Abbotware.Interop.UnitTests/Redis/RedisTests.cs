//-----------------------------------------------------------------------
// <copyright file="InteropRedisTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
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
                Assert.AreEqual("127.1.2.3", cfg.Endpoint.Host);
                Assert.AreEqual(1234, cfg.Endpoint.Port);
                Assert.AreEqual("redis", cfg.Endpoint.Scheme);
                Assert.AreEqual("user", cfg.Credential.UserName);
                Assert.AreEqual("pass", cfg.Credential.Password);
                Assert.IsEmpty(cfg.Endpoint.UserInfo);
            }

            {
                var cfg = new ConnectionOptions(new Uri("redis://127.0.0.1:6379"), Defaults.Credential);

                Assert.AreEqual("redis://guest:guest@127.0.0.1:6379/", cfg.ToServiceStackUri().ToString());
            }

            {
                var cfg = new ConnectionOptions(new Uri("redis://127.0.0.1:6379"), Defaults.Credential);

                Assert.AreEqual("redis://guest:guest@127.0.0.1:6379/", cfg.ToServiceStackUri().ToString());
            }

            {
                var cfg = new ConnectionOptions(new Uri("redis://127.0.0.1"), Defaults.Credential);

                Assert.AreEqual("redis://guest:guest@127.0.0.1:6379/", cfg.ToServiceStackUri().ToString());
            }

            {
                var cfg = new ConnectionOptions(new Uri("redis://127.0.0.1/asdf/asdf/"), Defaults.Credential);

                Assert.AreEqual("redis://guest:guest@127.0.0.1:6379/", cfg.ToServiceStackUri().ToString());
            }

            {
                var cfg = new ConnectionOptions(new Uri("redis://127.0.0.1/asdf/asdf/"), new NetworkCredential("asdf", "qwer"));

                Assert.AreEqual("redis://asdf:qwer@127.0.0.1:6379/", cfg.ToServiceStackUri().ToString());
            }

            {
                var cfg = new ConnectionOptions(new Uri("redis://127.0.0.1/asdf/asdf/?ssl=true&db=1"), new NetworkCredential("asdf", "qwer"));

                Assert.AreEqual("redis://asdf:qwer@127.0.0.1:6379/?ssl=true&db=1", cfg.ToServiceStackUri().ToString());
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