//-----------------------------------------------------------------------
// <copyright file="StringCappedListTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.IntegrationTests.Interop.Redis
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Abbotware.Core.Cache;
    using Abbotware.Interop.Redis;
    using Abbotware.Interop.Redis.Collections;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Interop")]
    [Category("Interop.Redis")]
    public class StringCappedListTests : BaseNUnitTest
    {
        [Test]
        public async Task LoadKeyDoesNotExist()
        {
            using var db = RedisHelper.CreateRedisConnection(this.Logger, UnitTestSettingsFile);

            ICacheableList<string> l = new StringCappedList(Guid.NewGuid().ToString(), 10, db.GetDatabase().Native);

            await l.Remote.LoadAsync(default);
        }

        [Test]
        public async Task StringCappedList_Add()
        {
            using var db = RedisHelper.CreateRedisConnection(this.Logger, UnitTestSettingsFile);

            ICacheableList<string> l = new StringCappedList(Guid.NewGuid().ToString(), 10, db.GetDatabase().Native);

            await l.Remote.LoadAsync(default);

            l.Local.Add("test");
            l.Local.Add("test");
            l.Local.Add("test");
        }

        [Test]
        public async Task StringCappedList_SaveAsync()
        {
            using var db = RedisHelper.CreateRedisConnection(this.Logger, UnitTestSettingsFile);

            var key = Guid.NewGuid().ToString();
            {
                ICacheableList<string> l = new StringCappedList(key, 3, db.GetDatabase().Native);

                await l.Remote.LoadAsync(default);

                var s = l.Local.AsEnumerable();

                Assert.AreEqual(0, s.Count());

                l.Local.Add("Hello 1");

                await l.Remote.SaveAsync(default);
            }

            {
                ICacheableList<string> l = new StringCappedList(key, 3, db.GetDatabase().Native);

                await l.Remote.LoadAsync(default);
                var a = l.Local.ToArray();

                Assert.AreEqual(1, a.Length);

                Assert.AreEqual("Hello 1", a[0]);

                l.Local.Add("Hello 2");
                l.Local.Add("Hello 3");

                await l.Remote.SaveAsync(default);
            }

            {
                ICacheableList<string> l = new StringCappedList(key, 3, db.GetDatabase().Native);

                await l.Remote.LoadAsync(default);
                var a = l.Local.ToArray();

                Assert.AreEqual(3, a.Length);

                Assert.AreEqual("Hello 3", a[0]);
                Assert.AreEqual("Hello 2", a[1]);
                Assert.AreEqual("Hello 1", a[2]);

                l.Local.Add("Hello 4");

                await l.Remote.SaveAsync(default);
            }

            {
                ICacheableList<string> l = new StringCappedList(key, 3, db.GetDatabase().Native);

                await l.Remote.LoadAsync(default);
                var a = l.Local.ToArray();

                Assert.AreEqual(3, a.Length);

                Assert.AreEqual("Hello 4", a[0]);
                Assert.AreEqual("Hello 3", a[1]);
                Assert.AreEqual("Hello 2", a[2]);

                l.Local.Add("Hello 5");
                l.Local.Add("Hello 6");
                l.Local.Add("Hello 7");

                await l.Remote.SaveAsync(default);
            }

            {
                ICacheableList<string> l = new StringCappedList(key, 3, db.GetDatabase().Native);

                await l.Remote.LoadAsync(default);
                var a = l.Local.ToArray();

                Assert.AreEqual(3, a.Length);

                Assert.AreEqual("Hello 7", a[0]);
                Assert.AreEqual("Hello 6", a[1]);
                Assert.AreEqual("Hello 5", a[2]);
            }
        }
    }
}
