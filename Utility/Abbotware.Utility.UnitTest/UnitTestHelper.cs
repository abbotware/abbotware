// -----------------------------------------------------------------------
// <copyright file="UnitTestHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Utility.UnitTest
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Threading;
    using Abbotware.Core;

    /// <summary>
    ///     Helper methods for Unit Tests
    /// </summary>
    public static class UnitTestHelper
    {
        /// <summary>
        /// If a debugger is attached, this will suspend the current thread  This allows you debug unit tests that have other threads
        /// </summary>
        public static void SleepThreadWhenDebugging()
        {
            if (Debugger.IsAttached)
            {
                Thread.Sleep(TimeSpan.MaxValue);
            }
        }

        /// <summary>
        /// Extension for any wait object that will throw an exception if the timeout expired.  Useful for Asynchronous Unit Tests.
        /// NOTE: If a debugger is attached, this will wait forever!  This allows you to step throw code without unit test failing due to timeout
        /// </summary>
        /// <param name="extendedWaitHandle">the wait object</param>
        /// <param name="timeout">timespan to wait</param>
        /// <param name="message">message for exception</param>
        public static void WaitOrThrow(this WaitHandle extendedWaitHandle, TimeSpan timeout, string message)
        {
            extendedWaitHandle = Arguments.EnsureNotNull(extendedWaitHandle, nameof(extendedWaitHandle));
            message = Arguments.EnsureNotNullOrWhitespace(message, nameof(message));

            if (Debugger.IsAttached)
            {
                timeout = new TimeSpan(1, 0, 0);
            }

            if (!extendedWaitHandle.WaitOne(timeout))
            {
                throw new TimeoutException(message);
            }
        }

        /// <summary>
        /// Extension for any wait object that will throw an exception after a 1 sec timeout.  Useful for Asynchronous Unit Tests.
        /// NOTE: If a debugger is attached, this will wait forever!  This allows you to step throw code without unit test failing due to timeout
        /// </summary>
        /// <param name="extendedWaitHandle">the wait object</param>
        /// <param name="message">message for exception</param>
        public static void Assert1Sec(this WaitHandle extendedWaitHandle, string message)
        {
            extendedWaitHandle = Arguments.EnsureNotNull(extendedWaitHandle, nameof(extendedWaitHandle));
            message = Arguments.EnsureNotNullOrWhitespace(message, nameof(message));

            extendedWaitHandle.WaitOrThrow(new TimeSpan(0, 0, 1), message);
        }

        /// <summary>
        /// Extension for any wait object that will throw an exception after a 5 sec timeout.  Useful for Asynchronous Unit Tests.
        /// NOTE: If a debugger is attached, this will wait forever!  This allows you to step throw code without unit test failing due to timeout
        /// </summary>
        /// <param name="extendedWaitHandle">the wait object</param>
        /// <param name="message">message for exception</param>
        public static void Assert5Sec(this WaitHandle extendedWaitHandle, string message)
        {
            extendedWaitHandle = Arguments.EnsureNotNull(extendedWaitHandle, nameof(extendedWaitHandle));
            message = Arguments.EnsureNotNullOrWhitespace(message, nameof(message));

            extendedWaitHandle.WaitOrThrow(new TimeSpan(0, 0, 5), message);
        }

        /// <summary>
        ///     Gets a string specific to this machine/user/object
        /// </summary>
        /// <param name="objectName">name of the object</param>
        /// <returns>string with machine/user/object</returns>
        public static string UserSpecificName(string objectName)
        {
            objectName = Arguments.EnsureNotNullOrWhitespace(objectName, nameof(objectName));

            var temp = $"{Environment.MachineName}_{Environment.UserName}_{objectName}";

            return temp;
        }
    }
}