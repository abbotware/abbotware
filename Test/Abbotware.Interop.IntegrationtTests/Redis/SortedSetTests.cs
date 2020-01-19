namespace Abbotware.IntegrationTests.Interop.Redis
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Abbotware.Core.Cache;
    using Abbotware.Interop.Redis;
    using Abbotware.Interop.Redis.Collections;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using AutoMapper;
    using NUnit.Framework;
    using StackExchange.Redis;

    [TestFixture]
    [Category("Interop")]
    [Category("Interop.Redis")]
    public class SortedSetTests : BaseNUnitTest
    {
        [Test]
        public async Task LoadKeyDoesNotExist()
        {
            using var db = RedisHelper.CreateRedisConnection(UnitTestSettingsFile);

            ICacheableSortedSet<DateTimeOffset, int> l = new AutoMapperSortedSet<DateTimeOffset, int>(Guid.NewGuid().ToString(), 9, db.GetDatabase().Native, null);

            await l.Remote.LoadAsync(default);
        }

        [Test]
        public async Task SortedSet_SaveAsync()
        {
            using var db = RedisHelper.CreateRedisConnection(UnitTestSettingsFile);

            var key = Guid.NewGuid().ToString();

            var dt1 = DateTimeOffset.FromUnixTimeSeconds(1000000);
            var dt2 = DateTimeOffset.FromUnixTimeSeconds(3000000);
            var dt3 = DateTimeOffset.FromUnixTimeSeconds(2000000);
            var dt4 = DateTimeOffset.FromUnixTimeSeconds(5000000);
            var dt5 = DateTimeOffset.FromUnixTimeSeconds(4000000);
            var dt6 = DateTimeOffset.FromUnixTimeSeconds(7000000);
            var dt2_5 = DateTimeOffset.FromUnixTimeSeconds(3500000);
            var dt4_5 = DateTimeOffset.FromUnixTimeSeconds(4500000);
            {
                ICacheableSortedSet<DateTimeOffset, int> l = new AutoMapperSortedSet<DateTimeOffset, int>(key, 3, db.GetDatabase().Native, CreateMapper());

                await l.Remote.LoadAsync(default);
                var s = l.Local.AsSortedList();

                Assert.AreEqual(0, s.Count);

                l.Local.Add(dt1, 1);

                await l.Remote.SaveAsync(default);
            }

            //// Set Contains : [1000000 - 1]*

            {
                ICacheableSortedSet<DateTimeOffset, int> l = new AutoMapperSortedSet<DateTimeOffset, int>(key, 3, db.GetDatabase().Native, CreateMapper());

                await l.Remote.LoadAsync(default);
                var s = l.Local.AsSortedList();

                Assert.AreEqual(1, s.Count);

                var kvlist = s.OrderBy(x => x.Key).ToList();

                Assert.AreEqual(kvlist[0].Key, dt1);
                Assert.AreEqual(kvlist[0].Value, 1);

                l.Local.Add(dt2, 2);

                await l.Remote.SaveAsync(default);
            }

            //// Set Contains : [1000000 - 1] [3000000 - 2]*

            {
                ICacheableSortedSet<DateTimeOffset, int> l = new AutoMapperSortedSet<DateTimeOffset, int>(key, 3, db.GetDatabase().Native, CreateMapper());

                await l.Remote.LoadAsync(default);
                var s = l.Local.AsSortedList();

                Assert.AreEqual(2, s.Count);

                var kvlist = s.OrderBy(x => x.Key).ToList();

                Assert.AreEqual(kvlist[0].Key, dt1);
                Assert.AreEqual(kvlist[0].Value, 1);

                Assert.AreEqual(kvlist[1].Key, dt2);
                Assert.AreEqual(kvlist[1].Value, 2);

                l.Local.Add(dt3, 3);

                await l.Remote.SaveAsync(default);
            }

            //// Set Contains : [1000000 - 1] [2000000 - 3]* [3000000 - 2]

            {
                ICacheableSortedSet<DateTimeOffset, int> l = new AutoMapperSortedSet<DateTimeOffset, int>(key, 3, db.GetDatabase().Native, CreateMapper());

                await l.Remote.LoadAsync(default);

                var s = l.Local.AsSortedList();

                Assert.AreEqual(3, s.Count);

                var kvlist = s.OrderBy(x => x.Key).ToList();

                Assert.AreEqual(kvlist[0].Key, dt1);
                Assert.AreEqual(kvlist[0].Value, 1);

                Assert.AreEqual(kvlist[1].Key, dt3);
                Assert.AreEqual(kvlist[1].Value, 3);

                Assert.AreEqual(kvlist[2].Key, dt2);
                Assert.AreEqual(kvlist[2].Value, 2);

                l.Local.Add(dt4, 4);

                await l.Remote.SaveAsync(default);
            }

            //// Set Contains : [2000000 - 3] [3000000 - 2] [ [5000000 - 4]*

            {
                ICacheableSortedSet<DateTimeOffset, int> l = new AutoMapperSortedSet<DateTimeOffset, int>(key, 3, db.GetDatabase().Native, CreateMapper());

                await l.Remote.LoadAsync(default);
                var s = l.Local.AsSortedList();

                Assert.AreEqual(3, s.Count);

                var kvlist = s.OrderBy(x => x.Key).ToList();

                Assert.AreEqual(kvlist[0].Key, dt3);
                Assert.AreEqual(kvlist[0].Value, 3);

                Assert.AreEqual(kvlist[1].Key, dt2);
                Assert.AreEqual(kvlist[1].Value, 2);

                Assert.AreEqual(kvlist[2].Key, dt4);
                Assert.AreEqual(kvlist[2].Value, 4);

                l.Local.Add(dt5, 5);

                await l.Remote.SaveAsync(default);
            }

            //// Set Contains : [3000000 - 2] [4000000 - 5]* [5000000 - 4]

            {
                ICacheableSortedSet<DateTimeOffset, int> l = new AutoMapperSortedSet<DateTimeOffset, int>(key, 3, db.GetDatabase().Native, CreateMapper());

                await l.Remote.LoadAsync(default);
                var s = l.Local.AsSortedList();

                Assert.AreEqual(3, s.Count);

                var kvlist = s.OrderBy(x => x.Key).ToList();

                Assert.AreEqual(kvlist[0].Key, dt2);
                Assert.AreEqual(kvlist[0].Value, 2);

                Assert.AreEqual(kvlist[1].Key, dt5);
                Assert.AreEqual(kvlist[1].Value, 5);

                Assert.AreEqual(kvlist[2].Key, dt4);
                Assert.AreEqual(kvlist[2].Value, 4);

                l.Local.Add(dt1, 1);

                await l.Remote.SaveAsync(default);
            }

            //// Set STILL Contains : [3000000 - 2] [4000000 - 5] [5000000 - 4]

            {
                ICacheableSortedSet<DateTimeOffset, int> l = new AutoMapperSortedSet<DateTimeOffset, int>(key, 3, db.GetDatabase().Native, CreateMapper());

                await l.Remote.LoadAsync(default);
                var s = l.Local.AsSortedList();

                Assert.AreEqual(3, s.Count);

                var kvlist = s.OrderBy(x => x.Key).ToList();

                Assert.AreEqual(kvlist[0].Key, dt2);
                Assert.AreEqual(kvlist[0].Value, 2);

                Assert.AreEqual(kvlist[1].Key, dt5);
                Assert.AreEqual(kvlist[1].Value, 5);

                Assert.AreEqual(kvlist[2].Key, dt4);
                Assert.AreEqual(kvlist[2].Value, 4);

                l.Local.Add(dt2, 2);

                await l.Remote.SaveAsync(default);
            }

            //// Set STILL Contains : [3000000 - 2] [4000000 - 5] [5000000 - 4]

            {
                ICacheableSortedSet<DateTimeOffset, int> l = new AutoMapperSortedSet<DateTimeOffset, int>(key, 3, db.GetDatabase().Native, CreateMapper());

                await l.Remote.LoadAsync(default);
                var s = l.Local.AsSortedList();

                Assert.AreEqual(3, s.Count);

                var kvlist = s.OrderBy(x => x.Key).ToList();

                Assert.AreEqual(kvlist[0].Key, dt2);
                Assert.AreEqual(kvlist[0].Value, 2);

                Assert.AreEqual(kvlist[1].Key, dt5);
                Assert.AreEqual(kvlist[1].Value, 5);

                Assert.AreEqual(kvlist[2].Key, dt4);
                Assert.AreEqual(kvlist[2].Value, 4);

                l.Local.Add(dt5, 5);

                await l.Remote.SaveAsync(default);
            }

            //// Set STILL Contains : [3000000 - 2] [4000000 - 5] [5000000 - 4]

            {
                ICacheableSortedSet<DateTimeOffset, int> l = new AutoMapperSortedSet<DateTimeOffset, int>(key, 3, db.GetDatabase().Native, CreateMapper());

                await l.Remote.LoadAsync(default);
                var s = l.Local.AsSortedList();

                Assert.AreEqual(3, s.Count);

                var kvlist = s.OrderBy(x => x.Key).ToList();

                Assert.AreEqual(kvlist[0].Key, dt2);
                Assert.AreEqual(kvlist[0].Value, 2);

                Assert.AreEqual(kvlist[1].Key, dt5);
                Assert.AreEqual(kvlist[1].Value, 5);

                Assert.AreEqual(kvlist[2].Key, dt4);
                Assert.AreEqual(kvlist[2].Value, 4);

                l.Local.Add(dt2_5, 25);

                await l.Remote.SaveAsync(default);
            }

            //// Set now Contains : [3500000 - 25]* [4000000 - 5] [5000000 - 4]

            {
                ICacheableSortedSet<DateTimeOffset, int> l = new AutoMapperSortedSet<DateTimeOffset, int>(key, 3, db.GetDatabase().Native, CreateMapper());

                await l.Remote.LoadAsync(default);
                var s = l.Local.AsSortedList();

                Assert.AreEqual(3, s.Count);

                var kvlist = s.OrderBy(x => x.Key).ToList();

                Assert.AreEqual(kvlist[0].Key, dt2_5);
                Assert.AreEqual(kvlist[0].Value, 25);

                Assert.AreEqual(kvlist[1].Key, dt5);
                Assert.AreEqual(kvlist[1].Value, 5);

                Assert.AreEqual(kvlist[2].Key, dt4);
                Assert.AreEqual(kvlist[2].Value, 4);

                l.Local.Add(dt6, 6);
                l.Local.Add(dt4_5, 45);

                await l.Remote.SaveAsync(default);
            }
            //// Set now Contains : [4500000 - 45]* [5000000 - 4]  [7000000 - 6]*
            {
                ICacheableSortedSet<DateTimeOffset, int> l = new AutoMapperSortedSet<DateTimeOffset, int>(key, 3, db.GetDatabase().Native, CreateMapper());

                await l.Remote.LoadAsync(default);
                var s = l.Local.AsSortedList();

                Assert.AreEqual(3, s.Count);

                var kvlist = s.OrderBy(x => x.Key).ToList();

                Assert.AreEqual(kvlist[0].Key.ToUnixTimeSeconds(), dt4_5.ToUnixTimeSeconds());
                Assert.AreEqual(kvlist[0].Value, 45);

                Assert.AreEqual(kvlist[1].Key.ToUnixTimeSeconds(), dt4.ToUnixTimeSeconds());
                Assert.AreEqual(kvlist[1].Value, 4);

                Assert.AreEqual(kvlist[2].Key.ToUnixTimeSeconds(), dt6.ToUnixTimeSeconds());
                Assert.AreEqual(kvlist[2].Value, 6);

                await l.Remote.SaveAsync(default);
            }
        }

        [Test]
        public async Task SortedSet_Add()
        {
            using var db = RedisHelper.CreateRedisConnection(UnitTestSettingsFile);

            ICacheableSortedSet<DateTimeOffset, int> l = new AutoMapperSortedSet<DateTimeOffset, int>(Guid.NewGuid().ToString(), 9, db.GetDatabase().Native, CreateMapper());

            await l.Remote.LoadAsync(default);

            l.Local.Add(DateTimeOffset.Now, 2);
            l.Local.Add(DateTimeOffset.Now.AddHours(2), 3);
            l.Local.Add(DateTimeOffset.Now.AddHours(-1), 1);
        }

        [Test]
        [Ignore("Test Not Finished")]
        public async Task SortedSet_Add_Duplicate_Scores()
        {
            // TODO: verify duplicate scores are set (should overwrite existing score)
            using var db = RedisHelper.CreateRedisConnection(UnitTestSettingsFile);

            ICacheableSortedSet<DateTimeOffset, int> l = new AutoMapperSortedSet<DateTimeOffset, int>(Guid.NewGuid().ToString(), 9, db.GetDatabase().Native, CreateMapper());

            await l.Remote.LoadAsync(default);
        }

        [Test]
        [Ignore("Test Not Finished")]
        public async Task SortedSet_Add_Duplicate_Keys()
        {
            // TODO: verify duplicate keys are not allows (should throw exception)
            using var db = RedisHelper.CreateRedisConnection(UnitTestSettingsFile);

            ICacheableSortedSet<DateTimeOffset, int> l = new AutoMapperSortedSet<DateTimeOffset, int>(Guid.NewGuid().ToString(), 9, db.GetDatabase().Native, CreateMapper());

            await l.Remote.LoadAsync(default);

            l.Local.Add(DateTimeOffset.Now, 2);
            l.Local.Add(DateTimeOffset.Now.AddHours(2), 3);
            l.Local.Add(DateTimeOffset.Now.AddHours(-1), 1);
        }

        private static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<DateTimeOffset, double>().ConvertUsing(i => i.UtcDateTime.Ticks);
                    cfg.CreateMap<double, DateTimeOffset>().ConvertUsing(i => new DateTimeOffset((long)i, TimeSpan.Zero));
                    cfg.CreateMap<int, RedisValue>().ConvertUsing(i => i);
                    cfg.CreateMap<RedisValue, int>().ConvertUsing(i => (int)i);
                });

            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}