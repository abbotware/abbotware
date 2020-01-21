// -----------------------------------------------------------------------
// <copyright file="IHost.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Host
{
    /// <summary>
    /// Interface for the host
    /// </summary>
    public interface IHost : IWaitForShutdown, IRequestShutdown
    {
    }
}
