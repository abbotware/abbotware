// -----------------------------------------------------------------------
// <copyright file="IGetSecurityToken.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.ExtensionPoints
{
    /// <summary>
    ///     interface for retrieving a security token
    /// </summary>
    public interface IGetSecurityToken
    {
        /// <summary>
        ///     Gets the current security token
        /// </summary>
        string Token { get; }
    }
}