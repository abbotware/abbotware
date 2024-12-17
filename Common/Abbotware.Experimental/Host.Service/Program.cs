// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Host.Task
{
    using System.Threading.Tasks;

    /// <summary>
    ///     Program Entry Point class
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Entry Point
        /// </summary>
        /// <param name="args">command line args</param>
        /// <returns>async task</returns>
        public static async Task<int> Main(string[] args)
        {
            return await Template.RunAsync<AbbotwareHostService>(args)
                .ConfigureAwait(false);
        }
    }
}
