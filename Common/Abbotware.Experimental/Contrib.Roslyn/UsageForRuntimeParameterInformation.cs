// -----------------------------------------------------------------------
// <copyright file="UsageForRuntimeParameterInformation.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Contrib.Roslyn
{
    using System;

    /// <summary>
    /// sample code for RuntimeParameterInformation
    /// </summary>
    public class UsageForRuntimeParameterInformation
    {
        /// <summary>
        /// Gets or sets the string
        /// </summary>
        public string SomeProperty { get; set; } = string.Empty;

        /// <summary>
        /// a
        /// </summary>
        /// <param name="parameter">parameter</param>
        public virtual void UserMethod(RuntimeParameterInformation<string> parameter)
        {
            var n = parameter.Name;
            var v = parameter.Value;

            Console.WriteLine($"name:{n} value:{v}");
        }

        /// <summary>
        /// this is what a user types when calling the method
        /// </summary>
        public void UserTypes()
        {
            this.SomeProperty = "data";

            this.UserMethod(this.SomeProperty);
        }

        /// <summary>
        /// This is what the compiler generators
        /// </summary>
        public void CompilerInserts()
        {
            this.SomeProperty = "data";

            this.UserMethod((this.SomeProperty, nameof(this.SomeProperty)));
        }
    }
}