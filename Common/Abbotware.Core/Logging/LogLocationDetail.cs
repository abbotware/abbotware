// -----------------------------------------------------------------------
// <copyright file="LogLocationDetail.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Logging
{
    using System;
    using Abbotware.Core.Extensions;

    /// <summary>
    /// Log Location Details
    /// </summary>
    public readonly struct LogLocationDetail : IEquatable<LogLocationDetail>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogLocationDetail"/> struct.
        /// </summary>
        /// <param name="line">line number</param>
        /// <param name="member">class method</param>
        /// <param name="file">file</param>
        public LogLocationDetail(int? line, string? member, string? file)
        {
            this.Line = line;
            this.Member = member;
            this.File = file;
        }

        /// <summary>
        /// Gets the line number
        /// </summary>
        public int? Line { get; }

        /// <summary>
        /// Gets the class member name
        /// </summary>
        public string? Member { get; }

        /// <summary>
        /// Gets the file name
        /// </summary>
        public string? File { get; }

        /// <summary>
        /// equals operator
        /// </summary>
        /// <param name="left">left side</param>
        /// <param name="right">right side</param>
        /// <returns>true if equal</returns>
        public static bool operator ==(LogLocationDetail left, LogLocationDetail right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// not equals operator
        /// </summary>
        /// <param name="left">left side</param>
        /// <param name="right">right side</param>
        /// <returns>true if not equal</returns>
        public static bool operator !=(LogLocationDetail left, LogLocationDetail right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (!this.StructPossiblyEquals<LogLocationDetail>(obj, out var other))
            {
                return false;
            }

#if NETSTANDARD2_0
            if (other == null)
            {
                return false;
            }
#endif
            return this.Equals(other);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
#if NETSTANDARD2_0
            return (this.Line, this.Member, this.File).GetHashCode();
#else
            return HashCode.Combine(this.Line, this.Member, this.File);
#endif
        }

        /// <inheritdoc/>
        public bool Equals(LogLocationDetail other)
        {
            if (this.Line != other.Line)
            {
                return false;
            }

            if (this.Member != other.Member)
            {
                return false;
            }

            if (this.File != other.File)
            {
                return false;
            }

            return true;
        }
    }
}
