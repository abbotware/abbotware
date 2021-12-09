// -----------------------------------------------------------------------
// <copyright file="IRequestResponseProtocol{TRequest,TResponse,TError}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.ExtensionPoints
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     interface for encoding request / response objects in an API call
    /// </summary>
    /// <typeparam name="TRequest">type of request object</typeparam>
    /// <typeparam name="TResponse">type of response object</typeparam>
    /// <typeparam name="TError">strongly typed error object</typeparam>
    public interface IRequestResponseProtocol<TRequest, TResponse, TError> : IResponseProtocol<TResponse, TError>
    {
        /// <summary>
        ///     encodes an object into a byte array
        /// </summary>
        /// <param name="request">request object to encode</param>
        /// <returns>binary data</returns>
        byte[] EncodeRequest(TRequest request);
    }
}