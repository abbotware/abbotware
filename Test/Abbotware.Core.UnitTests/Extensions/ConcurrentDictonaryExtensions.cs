// <copyright file="StringExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>

namespace Abbotware.UnitTests.Core.Extensions
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Math;
    using NUnit.Framework;

    /// <summary>
    ///     Arguments class unit tests
    /// </summary>
    [Category("Core")]
    [Category("Core.Extensions")]
    public class ConcurrentDictonaryExtensions
    {
        [Test]
        public void Increment()
        {
            var d = new ConcurrentDictionary<string, int>();

            var v = d.Increment("test");
            Assert.That(v, Is.EqualTo(1));

            _ = d.Increment("test");
            _ = d.Increment("test");
            v = d.Increment("test");

            Assert.That(v, Is.EqualTo(4));
        }

        [Test]
        public async Task Increment_Threaded()
        {
            var d = new ConcurrentDictionary<string, int>();

            var tasks = new List<Task>();
            for (var i = 0; i < 1000; ++i)
            {
                var task = Task.Run(() => d.Increment("Test"));
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            _ = d.TryGetValue("Test", out var t);
            Assert.That(t, Is.EqualTo(1000));
        }

        [Test]
        public void Add_Zero()
        {
            var d = new ConcurrentDictionary<string, int>();

            var v = d.Add("test", 0);
            Assert.That(v, Is.EqualTo(0));
        }

        [Test]
        public void Add()
        {
            var d = new ConcurrentDictionary<string, int>();

            var v = d.Add("test", 3);
            Assert.That(v, Is.EqualTo(3));

            v = d.Add("test", -7);
            Assert.That(v, Is.EqualTo(-4));

            v = d.Add("test", 0);
            Assert.That(v, Is.EqualTo(-4));
        }

        [Test]
        public void Add_SingleThreaded()
        {
            var d = new ConcurrentDictionary<string, int>();

            for (var i = 1; i <= 1000; ++i)
            {
                _ = d.Add("Test", i);
            }

            _ = d.TryGetValue("Test", out var t);

            Assert.That(t, Is.EqualTo(Functions.GaussSummation(1000)));
        }

        [Test]
        public async Task Add_MultiThreaded()
        {
            var d = new ConcurrentDictionary<string, int>();

            var tasks = new List<Task>();
            for (var i = 1; i <= 1000; ++i)
            {
                var captured = i; // so the lambda has the correct value in the future
                var task = Task.Run(() => d.Add("Test", captured));

                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            _ = d.TryGetValue("Test", out var t);
            Assert.That(t, Is.EqualTo(Functions.GaussSummation(1000)));
        }
    }
}