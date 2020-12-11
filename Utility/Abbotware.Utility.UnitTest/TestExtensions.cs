// -----------------------------------------------------------------------
// <copyright file="TestExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Utility.UnitTest
{
    using System;
    using Abbotware.Core;
    using Abbotware.Core.Collections;

    /// <summary>
    /// Test Extensions
    /// </summary>
    public static class TestExtensions
    {
        /// <summary>
        /// test cases for IEncodedKeyValueCollection
        /// </summary>
        /// <param name="test">unit test classinstance</param>
        /// <param name="encoder">encoder instance</param>
        public static void KeyValueEncoderTestCases(this IAssert test, IEncodedKeyValueStore encoder)
        {
            test = Arguments.EnsureNotNull(test, nameof(test));
            encoder = Arguments.EnsureNotNull(encoder, nameof(encoder));

            encoder.EncodeInt32("someInt", 123456);

            test.AssertEqual(123456, encoder.DecodeInt32("someInt"));

            encoder.EncodeInt32("maxInt", int.MaxValue);
            test.AssertEqual(int.MaxValue, encoder.DecodeInt32("maxInt"));

            encoder.EncodeInt32("minInt", int.MinValue);
            test.AssertEqual(int.MinValue, encoder.DecodeInt32("minInt"));

            encoder.EncodeInt64("someLong", 123456);
            test.AssertEqual(123456, encoder.DecodeInt64("someLong"));

            encoder.EncodeInt64("maxLong", long.MaxValue);
            test.AssertEqual(long.MaxValue, encoder.DecodeInt64("maxLong"));

            encoder.EncodeInt64("minLong", long.MinValue);
            test.AssertEqual(long.MinValue, encoder.DecodeInt64("minLong"));

            encoder.EncodeString("string1", "asdfasdfasdf");
            test.AssertEqual("asdfasdfasdf", encoder.DecodeString("string1"));

            var guid = Guid.NewGuid();

            encoder.EncodeGuid("guid", guid);
            test.AssertEqual(guid, encoder.DecodeGuid("guid"));

            encoder.EncodeGuid("emptyGuid", Guid.Empty);
            test.AssertEqual(Guid.Empty, encoder.DecodeGuid("emptyGuid"));

            var ts = TimeSpan.FromMilliseconds(123456767);

            encoder.EncodeTimeSpan("ts", ts);
            test.AssertEqual(ts, encoder.DecodeTimeSpan("ts"));
            encoder.EncodeTimeSpan("TimeSpan.Zero", TimeSpan.Zero);
            test.AssertEqual(TimeSpan.Zero, encoder.DecodeTimeSpan("TimeSpan.Zero"));
            encoder.EncodeTimeSpan("TimeSpan.MaxValue", TimeSpan.MaxValue);
            test.AssertEqual(TimeSpan.MaxValue, encoder.DecodeTimeSpan("TimeSpan.MaxValue"));
            encoder.EncodeTimeSpan("TimeSpan.MinValue", TimeSpan.MinValue);
            test.AssertEqual(TimeSpan.MinValue, encoder.DecodeTimeSpan("TimeSpan.MinValue"));

            var dtoNow = DateTimeOffset.Now;
            encoder.EncodeDateTimeOffset("DateTimeOffset.Now", dtoNow);
            test.AssertEqual(dtoNow, encoder.DecodeDateTimeOffset("DateTimeOffset.Now"));

            var dtoUtcNow = DateTimeOffset.UtcNow;
            encoder.EncodeDateTimeOffset("DateTimeOffset.UtcNow", dtoUtcNow);
            test.AssertEqual(dtoUtcNow, encoder.DecodeDateTimeOffset("DateTimeOffset.UtcNow"));

            encoder.EncodeDateTimeOffset("DateTimeOffset.MaxValue", DateTimeOffset.MaxValue);
            test.AssertEqual(DateTimeOffset.MaxValue, encoder.DecodeDateTimeOffset("DateTimeOffset.MaxValue"));

            encoder.EncodeDateTimeOffset("DateTimeOffset.MinValue", DateTimeOffset.MinValue);
            test.AssertEqual(DateTimeOffset.MinValue, encoder.DecodeDateTimeOffset("DateTimeOffset.MinValue"));

            var localDateTime = DateTime.Now;
            encoder.EncodeUtcDateTime("DateTime.Now", localDateTime);
            test.AssertEqual(localDateTime.ToUniversalTime(), encoder.DecodeUtcDateTime("DateTime.Now"));

            var utcDateTime = DateTime.Now.ToUniversalTime();
            encoder.EncodeUtcDateTime("DateTime.Now.ToUniversalTime()", utcDateTime);
            test.AssertEqual(utcDateTime, encoder.DecodeUtcDateTime("DateTime.Now.ToUniversalTime()"));

            encoder.EncodeUtcDateTime("DateTime.MaxValue", DateTime.MaxValue.ToUniversalTime());
            test.AssertEqual(DateTime.MaxValue.ToUniversalTime(), encoder.DecodeUtcDateTime("DateTime.MaxValue"));

            encoder.EncodeUtcDateTime("DateTime.MinValue", DateTime.MinValue.ToUniversalTime());
            test.AssertEqual(DateTime.MinValue.ToUniversalTime(), encoder.DecodeUtcDateTime("DateTime.MinValue"));

            ////// TODO: Enums = 0, negative, positive
            ////// TODO: Nullable Cases - Add Null, verify key is not in .Keys
        }
    }
}
