//-----------------------------------------------------------------------
// <copyright file="JsonCappedListTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.IntegrationTests.Interop.Redis
{
    using System;
    using System.Threading.Tasks;
    using Abbotware.Core.Chrono;
    using Abbotware.Interop.Redis;
    using Abbotware.Interop.Redis.Collections;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Interop")]
    [Category("Interop.Redis")]
    public class JsonCappedListTests : BaseNUnitTest
    {
        [Test]
        public async Task JsonCappedList_SaveLoadAsync()
        {
            using var db = RedisHelper.CreateRedisConnection(UnitTestSettingsFile);

            var input = new TimeSeriesValue<string> { Y = "asdf", X = new DateTimeOffset(12, 12, 12, 12, 12, 12, TimeSpan.FromMinutes(30)) };

            var key = Guid.NewGuid().ToString();
            {
                var l = new JsonCappedList<TimeSeriesValue<string>>(key, 3, db.GetDatabase().Native);

                await l.Remote.LoadAsync(default);
                var a = l.Local.ToArray();

                Assert.AreEqual(0, a.Length);

                l.Add(input);

                await l.SaveAsync(default);
            }

            {
                var l = new JsonCappedList<TimeSeriesValue<string>>(key, 3, db.GetDatabase().Native);

                await l.Remote.LoadAsync(default);
                var a = l.Local.ToArray();

                Assert.AreEqual(1, a.Length);

                Assert.AreEqual(input.X, a[0].X);
                Assert.AreEqual(input.Y, a[0].Y);
            }

            {
                var l = new StringCappedList(key, 3, db.GetDatabase().Native);

                await l.Remote.LoadAsync(default);
                var a = l.Local.ToArray();

                Assert.AreEqual(1, a.Length);

                Assert.AreEqual(@"{""X"":""0012-12-12T12:12:12+00:30"",""Y"":""asdf""}", a[0]);
            }
        }
    }
}
