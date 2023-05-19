// -----------------------------------------------------------------------
// <copyright file="SafeRollingFileAppender.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Log4net
{
    using Abbotware.Core.Helpers;
    using log4net.Appender;

    /// <summary>
    ///     RollingFileAppender with fix for file name with AppDomain with bad characters (normally throws 'The given path's
    ///     format is not supported' exception)
    /// </summary>
    public class SafeRollingFileAppender : RollingFileAppender
    {
        /// <inheritdoc />
        public override string File
        {
            get
            {
                return base.File;
            }

            set
            {
                if (value == null)
                {
                    return;
                }

                base.File = PathHelper.CleanPath(value);
            }
        }
    }
}