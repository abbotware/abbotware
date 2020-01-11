// <copyright file="EventSpanTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2016. All rights reserved
// </copyright>

namespace Abbotware.UnitTests.Core
{
    using System;
    using Abbotware.Core.Chrono;
    using NUnit.Framework;

    [TestFixture]
    [Category("Core")]
    [Category("Core.Chrono")]
    public class EventSpanTests
    {
        [Test]
        public void EventSpanUsage()
        {
            var es = new EventSpan(new TimeSpan(0, 1, 0));

            Assert.AreEqual(new TimeSpan(0, 1, 0), es.CoolDownInterval);

            Assert.IsNull(es.First);
            Assert.IsNull(es.Last);

            var startTime = DateTimeOffset.Now;

            // first event is always new
            Assert.IsTrue(es.IsNewEvent(startTime));

            Assert.AreEqual(es.Last, es.First);
            Assert.AreEqual(startTime, es.First);

            Assert.IsFalse(es.IsNewEvent(startTime.AddSeconds(1)));
            Assert.AreEqual(startTime, es.Last);

            Assert.IsFalse(es.IsNewEvent(startTime.AddSeconds(1)));
            Assert.AreEqual(startTime, es.Last);

            Assert.IsFalse(es.IsNewEvent(startTime.AddSeconds(10)));
            Assert.AreEqual(startTime, es.Last);

            Assert.IsTrue(es.IsNewEvent(startTime.AddMinutes(1)));
            Assert.AreEqual(startTime.AddMinutes(1), es.Last);

            Assert.IsFalse(es.IsNewEvent(startTime.AddMinutes(0)));
            Assert.AreEqual(startTime.AddMinutes(1), es.Last);

            Assert.IsTrue(es.IsNewEvent(startTime.AddDays(1)));

            Assert.IsFalse(es.IsNewEvent(startTime.AddDays(-1)));

            Assert.AreEqual(startTime.AddDays(1), es.Last);
            Assert.AreEqual(startTime, es.First);

            es.Clear();

            Assert.IsTrue(es.IsNewEvent(startTime.AddDays(-1)));
            Assert.AreEqual(es.Last, es.First);
            Assert.AreEqual(startTime.AddDays(-1), es.First);
        }
    }
}