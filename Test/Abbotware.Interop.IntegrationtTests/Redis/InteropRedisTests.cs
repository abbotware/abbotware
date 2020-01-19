//-----------------------------------------------------------------------
// <copyright file="RedisUnitTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.IntegrationTests.Interop.Redis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Abbotware.Core.Cache;
    using Abbotware.Interop.Redis;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Interop")]
    [Category("Interop.Redis")]
    public class InteropRedisTests : BaseNUnitTest
    {
        [Test]
        public async Task Redis_GetSetKeyValueStore()
        {
            var keyId = Guid.NewGuid();

            using (var c = RedisHelper.CreateRedisConnection(UnitTestSettingsFile))
            {
                var db = c.GetDatabase();

                var kvp = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("key1", "value1"),
                    new KeyValuePair<string, string>("key2", "value2"),
                };

                await db.SetFieldsAsync(keyId.ToString(), kvp, default);
            }

            using (var c = RedisHelper.CreateRedisConnection(UnitTestSettingsFile))
            {
                var db = c.GetDatabase();

                var k = await db.GetFieldsAsync(keyId.ToString(), default);
                Assert.AreEqual(2, k.Count);

                Assert.AreEqual("value1", k["key1"]);
                Assert.AreEqual("value2", k["key2"]);
            }
        }

        [Test]
        public async Task Redis_HashFieldSet_Id()
        {
            var id = Guid.NewGuid().ToString();

            using var c = RedisHelper.CreateRedisConnection(UnitTestSettingsFile);

            var db = c.GetDatabase();

            ICacheableCategorizedFieldValues hfs = new CategorizedFieldValues(id, "123", db);

            Assert.AreEqual($"{id}:123", hfs.Remote.RemoteKey);
            Assert.AreEqual(0, hfs.Local.ValueCount);
            Assert.AreEqual(0, hfs.Local.Categories.Count());

            await hfs.Remote.LoadAsync(default);
            Assert.AreEqual(0, hfs.Local.ValueCount);
            Assert.AreEqual(0, hfs.Local.Categories.Count());

            await hfs.Remote.SaveAsync(default);
            Assert.AreEqual(0, hfs.Local.ValueCount);
            Assert.AreEqual(0, hfs.Local.Categories.Count());

            Assert.IsNull(hfs.Local.GetOrDefault("asdf", "asdf"));
        }

        [Test]
        public async Task Redis_HashFieldSet_SetGet()
        {
            var id = Guid.NewGuid();

            using (var c = RedisHelper.CreateRedisConnection(UnitTestSettingsFile))
            {
                var db = c.GetDatabase();

                ICacheableCategorizedFieldValues hfs = new CategorizedFieldValues("entity", id.ToString(), db);

                hfs.Local.AddOrUpdate("c1", "f1", "v1");

                Assert.AreEqual("c1", hfs.Local.Categories.Single());
                Assert.AreEqual("v1", hfs.Local.GetOrDefault("c1", "f1"));

                hfs.Local.AddOrUpdate("c1", "f1", "v2");
                hfs.Local.AddOrUpdate("c1", "f2", "v3");

                Assert.AreEqual("c1", hfs.Local.Categories.Single());
                Assert.AreEqual("v2", hfs.Local.GetOrDefault("c1", "f1"));
                Assert.AreEqual("v3", hfs.Local.GetOrDefault("c1", "f2"));

                hfs.Local.AddOrUpdate("c1", "f1", "v4");
                hfs.Local.AddOrUpdate("c1", "f2", "v5");
                hfs.Local.AddOrUpdate("c2", "f1", "v6");

                Assert.AreEqual(2, hfs.Local.Categories.Count());

                Assert.AreEqual("v4", hfs.Local.GetOrDefault("c1", "f1"));
                Assert.AreEqual("v5", hfs.Local.GetOrDefault("c1", "f2"));
                Assert.AreEqual("v6", hfs.Local.GetOrDefault("c2", "f1"));

                await hfs.Remote.SaveAsync(default);
            }

            using (var c = RedisHelper.CreateRedisConnection(UnitTestSettingsFile))
            {
                var db = c.GetDatabase();

                ICacheableCategorizedFieldValues hfs = new CategorizedFieldValues("entity", id.ToString(), db);

                await hfs.Remote.LoadAsync(default);

                Assert.AreEqual(2, hfs.Local.Categories.Count());
                Assert.AreEqual("v4", hfs.Local.GetOrDefault("c1", "f1"));
                Assert.AreEqual("v5", hfs.Local.GetOrDefault("c1", "f2"));
                Assert.AreEqual("v6", hfs.Local.GetOrDefault("c2", "f1"));

                await hfs.Remote.SaveAsync(default);
            }
        }

        [Test]
        public async Task Redis_PropertySet_SetGet()
        {
            var id = Guid.NewGuid();

            using (var c = RedisHelper.CreateRedisConnection(UnitTestSettingsFile))
            {
                var db = c.GetDatabase();

                var ps = new PropertySet("entity", id.ToString(), db);

                ps.AddOrUpdate("f1", "v1");

                await ps.SaveAsync(default);
            }

            using (var c = RedisHelper.CreateRedisConnection(UnitTestSettingsFile))
            {
                var db = c.GetDatabase();

                var ps = new PropertySet("entity", id.ToString(), db);

                await ps.SaveAsync(default);
            }
        }
    }
}