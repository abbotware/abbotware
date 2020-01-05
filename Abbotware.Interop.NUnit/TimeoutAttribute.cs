// -----------------------------------------------------------------------
// <copyright file="TimeoutAttribute.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.NUnit
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Abbotware.Core.Extensions;
    using global::NUnit.Framework;
    using global::NUnit.Framework.Interfaces;
    using global::NUnit.Framework.Internal;
    using global::NUnit.Framework.Internal.Commands;

    /// <summary>
    /// A simple timeout attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class TimeoutAttribute : NUnitAttribute, IWrapTestMethod
    {
        private readonly TimeSpan timeout;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeoutAttribute"/> class.
        /// </summary>
        /// <param name="milliseconds">timeout in milliseconds</param>
        public TimeoutAttribute(int milliseconds)
        {
            this.timeout = TimeSpan.FromMilliseconds(milliseconds);
        }

        /// <inheritdoc/>
        public TestCommand Wrap(TestCommand command)
        {
            return new TimeoutCommand(command, this.timeout);
        }

        private class TimeoutCommand : DelegatingTestCommand
        {
            private readonly TimeSpan timeout;

            public TimeoutCommand(TestCommand innerCommand, TimeSpan timeout)
                : base(innerCommand)
            {
                this.timeout = timeout;
            }

            public override TestResult Execute(TestExecutionContext context)
            {
                var t = Task.Run(() => this.innerCommand.Execute(context));

                if (!Debugger.IsAttached)
                {
                    t.TimeoutAfter(this.timeout).GetAwaiter().GetResult();
                }

                return t.GetAwaiter().GetResult();
            }
        }
    }
}