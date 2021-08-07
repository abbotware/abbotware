// -----------------------------------------------------------------------
// <copyright file="RuntimeParameterInformation{TParameter}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Contrib.Roslyn
{
#pragma warning disable CA1815 // Override equals and operator equals on value types
    /// <summary>
    /// class to capture the runtime caller attributes
    /// </summary>
    /// <typeparam name="TParameter">type pf paramater</typeparam>
    public struct RuntimeParameterInformation<TParameter>
#pragma warning restore CA1815 // Override equals and operator equals on value types
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeParameterInformation{TParameter}"/> struct.
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="name">name</param>
        public RuntimeParameterInformation(TParameter value, string name)
        {
            this.Value = value;
            this.Name = name;
        }

        /// <summary>
        /// Gets the argument name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the argument value
        /// </summary>
        public TParameter Value { get; }

#pragma warning disable CA2225 // Operator overloads have named alternates
        /// <summary>
        /// careate RuntimeParameterInformation
        /// </summary>
        /// <param name="runtime">runtime data</param>
        public static implicit operator RuntimeParameterInformation<TParameter>((TParameter Value, string Name) runtime)
        {
            return new RuntimeParameterInformation<TParameter>(runtime.Value, runtime.Name);
        }

        /// <summary>
        /// careate RuntimeParameterInformation
        /// </summary>
        /// <param name="value">value</param>
        public static implicit operator RuntimeParameterInformation<TParameter>(TParameter value)
#pragma warning restore CA2225 // Operator overloads have named alternates
        {
            return new RuntimeParameterInformation<TParameter>(value, "unknown");
        }
    }
}