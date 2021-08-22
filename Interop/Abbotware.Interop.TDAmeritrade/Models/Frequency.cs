// -----------------------------------------------------------------------
// <copyright file="Frequency.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    /// <summary>
    /// Base Frequency Configuration
    /// </summary>
    /// <param name="Type">period type</param>
    /// <param name="Count">period count</param>
    public abstract record Frequency(FrequencyType Type, ushort Count)
    {
    }

    /// <summary>
    /// Base Frequency Configuration
    /// </summary>
    /// <typeparam name="TCount">period count type</typeparam>
    /// <param name="Type">period type</param>
    /// <param name="Periods">period count</param>
    public abstract record Frequency<TCount>(FrequencyType Type, TCount Periods)
        : Frequency(Type, (ushort)(int)(object)Periods!)
    {
    }
}
