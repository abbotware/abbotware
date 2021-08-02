// -----------------------------------------------------------------------
// <copyright file="Parser.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.IO
{
    using System;
    using System.Runtime.CompilerServices;
    using Abbotware.Core.Exceptions;

    /// <summary>
    ///     optimized parser methods
    /// </summary>
    public static class Parser
    {
        /// <summary>
        ///     internal multiplication table for base 10
        /// </summary>
        private static readonly int[] Int32MultiplicationTable = GenerateInt32Table();

        /// <summary>
        ///     internal multiplication table for base 10
        /// </summary>
        private static readonly ushort[] UInt16MultiplicationTable = GenerateUInt16Table();

        /// <summary>
        ///     converts a buffer section to an Int32
        /// </summary>
        /// <param name="buffer">buffer</param>
        /// <param name="start">buffer start posiiton</param>
        /// <param name="end">buffer end position</param>
        /// <returns>parsed Int32</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int? ToInt32(char[] buffer, int start, int end)
        {
            if (buffer == null)
            {
                return null;
            }

            if (start == end)
            {
                return null;
            }

            int? output = 0;
            var basePos = 0;

            for (var j = end; j >= start; --j)
            {
                var digit = buffer[j] - 48;

                if (digit > 9 || digit < 0)
                {
                    throw AbbotwareException.Create("Parse Error:buffer[{0}]={1}", j, buffer[j]);
                }

                output += digit * Int32MultiplicationTable[basePos];
                ++basePos;
            }

            return output;
        }

        /// <summary>
        ///     converts a buffer section to an UInt16
        /// </summary>
        /// <param name="buffer">buffer</param>
        /// <param name="start">buffer start posiiton</param>
        /// <param name="end">buffer end position</param>
        /// <returns>parsed Int32</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort? ToUInt16(char[] buffer, int start, int end)
        {
            if (buffer == null)
            {
                return null;
            }

            if (start == end)
            {
                return null;
            }

            ushort? output = 0;
            var basePos = 0;

            for (var j = end; j >= start; --j)
            {
                var digit = buffer[j] - 48;

                if (digit > 9 || digit < 0)
                {
                    throw AbbotwareException.Create("Parse Error:buffer[{0}]={1}", j, buffer[j]);
                }

                output += (ushort?)(digit * UInt16MultiplicationTable[basePos]);
                ++basePos;
            }

            return output;
        }

        /// <summary>
        ///     generates multiplication table for int base 10
        /// </summary>
        /// <returns>initialized array</returns>
        private static int[] GenerateInt32Table()
        {
            var table = new int[20];

            table[0] = 1;

            for (var i = 1; i < 20; ++i)
            {
                table[i] = 10 * table[i - 1];
            }

            return table;
        }

        /// <summary>
        ///     generates multiplication table for int base 10
        /// </summary>
        /// <returns>initialized array</returns>
        private static ushort[] GenerateUInt16Table()
        {
            var table = new ushort[10];

            table[0] = 1;

            for (ushort i = 1; i < 10; ++i)
            {
                table[i] = (ushort)(10 * table[i - 1]);
            }

            return table;
        }
    }
}