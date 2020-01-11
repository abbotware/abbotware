// -----------------------------------------------------------------------
// <copyright file="Functions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Math
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Math helper functions
    /// </summary>
    public static class Functions
    {
        /// <summary>
        /// checks if two numbers have the same sign
        /// </summary>
        /// <param name="operandA">operand a</param>
        /// <param name="operandB">operand b</param>
        /// <returns>true if signs are the same</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SignEquals(decimal operandA, decimal operandB)
        {
            return Math.Sign(operandA) == Math.Sign(operandB);

            //// Alternative:
            //// quick check for matching signs
            //// (-a x -b) and (a x b) are both positive

            ////if (a*b >= 0)
            ////{
            ////    return true;
            ////}

            ////return false;
        }

        /// <summary>
        /// checks if two numbers have opposite signs
        /// </summary>
        /// <param name="operandA">operand a</param>
        /// <param name="operandB">operand b</param>
        /// <returns>true if signs are not the same</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SignNotEquals(decimal operandA, decimal operandB)
        {
            return !SignEquals(operandA, operandB);
        }

        /// <summary>
        /// The standard logistic function with parameters (k = 1, x0 = 0, L = 1)
        /// </summary>
        /// <param name="input">input value for logisitic function</param>
        /// <returns>the result</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Logistic(double input)
        {
            return 1.0 / (1.0 + Math.Exp(-input));
        }

        /// <summary>
        /// Round off the specified number of units
        /// </summary>
        /// <param name="value">value to round</param>
        /// <param name="unit">digits to round off</param>
        /// <returns>the result</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double RoundOff(double value, int unit)
        {
            return Math.Round(value / unit) * unit;
        }
    }
}