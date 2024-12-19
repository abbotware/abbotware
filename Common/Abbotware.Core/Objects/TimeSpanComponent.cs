// -----------------------------------------------------------------------
// <copyright file="TimeSpanComponent.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects;

using System;

/// <summary>
///     Abstract base class for writing objects that can be reinitialized only after a certain time has elapsed
/// </summary>
public abstract class TimeSpanComponent : BaseComponent
{
    /// <summary>
    ///     TimeSpan between initializations
    /// </summary>
    private readonly TimeSpan expirationTimeSpan;

    /// <summary>
    ///     Time of the last initialization
    /// </summary>
    private DateTimeOffset lastInitializationTime = DateTimeOffset.MinValue;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TimeSpanComponent" /> class.
    /// </summary>
    /// <param name="expirationTimeSpan">TimeSpan between initializations</param>
    /// <param name="logger">injected logger</param>
    protected TimeSpanComponent(TimeSpan expirationTimeSpan, ILogger logger)
        : base(logger)
    {
        Arguments.NotNull(logger, nameof(logger));
        Arguments.IsPositiveAndNotZero(expirationTimeSpan, nameof(expirationTimeSpan));

        this.expirationTimeSpan = expirationTimeSpan;
    }

    /// <inheritdoc />
    protected sealed override bool OnRequiresInitialization()
    {
        var currentTime = DateTimeOffset.UtcNow;

        return (this.lastInitializationTime + this.expirationTimeSpan) <= currentTime;
    }

    /// <inheritdoc />
    protected override void OnInitialize()
    {
        base.OnInitialize();

        this.lastInitializationTime = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Rests the time to the initial vaoid
    /// </summary>
    protected void ResetTimeSpan()
        => this.lastInitializationTime = DateTimeOffset.MinValue;
}