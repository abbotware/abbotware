// -----------------------------------------------------------------------
// <copyright file="YubicoHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Yubico
{
    using Abbotware.Core;

    /// <summary>
    /// Yubico Helper class
    /// </summary>
    public static class YubicoHelper
    {
        /// <summary>
        /// gets the id from the one time password
        /// </summary>
        /// <param name="otp">one time password</param>
        /// <returns>the id portion of the otp</returns>
        public static string GetOtpId(string otp)
        {
            otp = Arguments.EnsureNotNullOrWhitespace(otp, nameof(otp));

            if (otp.Length < 12)
            {
                return string.Empty;
            }

            return otp.Substring(0, 12);
        }
    }
}