// -----------------------------------------------------------------------
// <copyright file="BaseUnitTest.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Test
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using Abbotware.Core.Diagnostics;
    using Abbotware.Core.Helpers;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;
    using AutoFixture;
    using Moq;

    /// <summary>
    /// Base class for creating a unit test with helper / utility features
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class BaseUnitTest : BaseComponent
    {
        /// <summary>
        ///  Gets the path for unit test exports
        /// </summary>
        public static Uri UnitTestExportPath
        {
            get
            {
                return new Uri(Environment.CurrentDirectory);
            }
        }

        /// <summary>
        /// Gets the logger (in memory) for unit test
        /// </summary>
        protected ILogger NewLogger { get; }

        /// <summary>
        /// Checks if system is linux
        /// </summary>
        /// <returns>true if linux</returns>
        protected static bool IsLinux()
        {
            return OperatingSystemHelper.IsUnix;
        }

        /// <summary>
        /// Create a test object with random populated data
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <returns>random data filled object</returns>
        protected virtual T AutoFixture<T>()
        {
            var f = new Fixture();
            return f.Create<T>();
        }

        /// <summary>
        /// Creates a mock object
        /// </summary>
        /// <typeparam name="T">object type to mock</typeparam>
        /// <returns>Mocked object</returns>
        protected virtual T MockIt<T>()
            where T : class
        {
            return Mock.Of<T>();
        }

        /// <summary>
        ///     Compares the properties of two objects of the same type and returns if all properties are equal.
        /// </summary>
        /// <typeparam name="T">paramter type</typeparam>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compre.</param>
        /// <param name="ignoreList">A list of property names to ignore from the comparison.</param>
        protected virtual void ReflectionEquals<T>(T left, T right, params string[] ignoreList)
        {
            ReflectionHelper.ThrowIfNotEqual(left, right, ignoreList);
        }

        /// <summary>
        /// Debug Helper - Blocks forever if a debugger is attached
        /// </summary>
        protected virtual void BlockIfDebugging()
        {
            if (Debugger.IsAttached)
            {
                BlockForever();
            }
        }

        /// <summary>
        /// Debug Helper - Blocks forever if a debugger is attached, else sleeps
        /// </summary>
        /// <param name="timespan">sleep timepspan</param>
        protected virtual void SleepOrBlockIfDebugging(TimeSpan timespan)
        {
            if (Debugger.IsAttached)
            {
                BlockForever();
            }

            Thread.Sleep(timespan);
        }

        /// <summary>
        /// Debug Helper - Blocks forever if a debugger is attached, else waits on the event with a timeout
        /// </summary>
        /// <param name="condition">wait handle to block on timepspan</param>
        /// <param name="timespan">sleep timepspan</param>
        protected virtual void WaitOrBlockIfDebugging(ManualResetEventSlim condition, TimeSpan timespan)
        {
            condition = Arguments.EnsureNotNull(condition, nameof(condition));

            this.WaitOrBlockIfDebugging(condition.WaitHandle, timespan);
        }

        /// <summary>
        /// Debug Helper - Blocks forever if a debugger is attached, else waits on the handle with a timeout
        /// </summary>
        /// <param name="waitHandle">wait handle to block on timepspan</param>
        /// <param name="timespan">sleep timepspan</param>
        protected virtual void WaitOrBlockIfDebugging(WaitHandle waitHandle, TimeSpan timespan)
        {
            waitHandle = Arguments.EnsureNotNull(waitHandle, nameof(waitHandle));

            if (Debugger.IsAttached)
            {
                waitHandle.WaitOne();
            }

            waitHandle.WaitOne(timespan);
        }

        /// <summary>
        /// Skips a Test on linux
        /// </summary>
        protected void SkipTestOnLinux()
        {
            this.SkipTestOnLinux(string.Empty);
        }

        /// <summary>
        /// Skips a Test on linux
        /// </summary>
        /// <param name="message">message for test runner</param>
        protected void SkipTestOnLinux(string message)
        {
            if (IsLinux())
            {
                this.AssertInconclusive($"Not Available on this platform:{message}");
            }
        }

        /// <summary>
        /// Skips a Test on Windows
        /// </summary>
        protected void SkipTestOnWindows()
        {
            this.SkipTestOnWindows(string.Empty);
        }

        /// <summary>
        /// Skips a Test on Windows
        /// </summary>
        /// <param name="message">message for test runner</param>
        protected void SkipTestOnWindows(string message)
        {
            if (!IsLinux())
            {
                this.AssertInconclusive($"Not Available on this platform:{message}");
            }
        }

        /// <summary>
        /// Assert Test is Inconclusive
        /// </summary>
        /// <param name="message">message</param>
        protected abstract void AssertInconclusive(string message);

        /// <summary>
        /// Assert Test failue
        /// </summary>
        /// <param name="message">message</param>
        protected abstract void AssertFail(string message);

        /// <summary>
        /// Assert Equal
        /// </summary>
        /// <param name="left">left</param>
        /// <param name="right">right</param>
        protected abstract void AssertEqual(object left, object right);

        private static void BlockForever()
        {
            Thread.Sleep(-1);
        }
    }
}