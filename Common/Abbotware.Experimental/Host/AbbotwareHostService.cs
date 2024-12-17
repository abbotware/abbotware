// -----------------------------------------------------------------------
// <copyright file="AbbotwareHostService.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Host
{
    using Abbotware.Host.Configuration;

    /// <summary>
    /// Abbotware Host Service using the default configuration class
    /// </summary>
    public class AbbotwareHostService : AbbotwareHostService<IHostOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbbotwareHostService"/> class.
        /// </summary>
        /// <param name="hostConfiguration">host configuration</param>
        public AbbotwareHostService(IHostOptions hostConfiguration)
            : base(hostConfiguration)
        {
        }
    }
}
