// -----------------------------------------------------------------------
// <copyright file="UsageForRuntimeCallerInformation.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Contrib.Roslyn
{
    using System;
    using Abbotware.Core.Diagnostics;

    /// <summary>
    /// sample code for UsageForRuntimeCallerInformation
    /// </summary>
    public class UsageForRuntimeCallerInformation
    {
        /// <summary>
        /// sample method
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <param name="callerInfo">caller info</param>
        public virtual void UserMethod(string parameter, RuntimeCallerInformation? callerInfo = null)
        {
            Console.WriteLine($"{parameter} {callerInfo}");
        }

        /// <summary>
        /// this is what a user types when calling the method
        /// </summary>
        public void UserTypes()
        {
            this.UserMethod("some value");
        }

        /// <summary>
        /// This is what the compiler generators
        /// </summary>
        public void CompilerInserts()
        {
            this.UserMethod("some value", new RuntimeCallerInformation());
        }
    }
}