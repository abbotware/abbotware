// -----------------------------------------------------------------------
// <copyright file="IResponseProtocol{TResponse,TError}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.ExtensionPoints
{
    /// <summary>
    ///     interface for decoding response objects in an API call
    /// </summary>
    /// <typeparam name="TResponse">type of response object</typeparam>
    /// <typeparam name="TError">strongly typed error object</typeparam>
    public interface IResponseProtocol<TResponse, TError>
    {
        /// <summary>
        ///     Decodes an a byte array into a typed response object
        /// </summary>
        /// <param name="binary">binary data</param>
        /// <returns>decoded response objecte</returns>
        TResponse DecodeResponse(byte[] binary);

        /// <summary>
        ///     Decodes an a byte array into a typed error object
        /// </summary>
        /// <param name="binary">binary data</param>
        /// <returns>decoded error objecte</returns>
        TError DecodeError(byte[] binary);
    }
}