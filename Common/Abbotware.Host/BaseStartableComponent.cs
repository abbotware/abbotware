// -----------------------------------------------------------------------
// <copyright file="BaseStartableComponent.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Host;

using System;
using System.Threading;
using System.Threading.Tasks;
using Abbotware.Core;
using Abbotware.Core.Extensions;
using Abbotware.Core.Objects;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

/// <summary>
///     base class for a startable component
/// </summary>
public abstract class BaseStartableComponent : BaseComponent, IHostedService
{
    /// <summary>
    ///     wait handle used to signify shutdown
    /// </summary>
    private readonly CancellationTokenSource cts = new();

    /// <summary>
    ///     Initializes a new instance of the <see cref="BaseStartableComponent" /> class.
    /// </summary>
    /// <param name="logger">injected logger</param>
    protected BaseStartableComponent(ILogger logger)
        : base(logger) => Arguments.NotNull(logger, nameof(logger));

    /// <summary>
    ///     Gets the cancellation token for shutdown notification
    /// </summary>
    /// <returns>cancellation token</returns>
    protected CancellationToken CancellationToken => this.cts.Token;

    /// <summary>
    /// Start method
    /// </summary>
    public void Start()
    {
        try
        {
            this.Logger.Info("Start Requested");

            this.OnStart();

            this.Logger.Info("Exiting Start");
        }
        catch (Exception ex)
        {
            this.Logger.Error(ex, $"Error Stopping:{this.GetType()}");
            throw;
        }
    }

    /// <summary>
    /// Stop method
    /// </summary>
    public void Stop()
    {
        try
        {
            this.Logger.Info("Stop Requested");

            this.cts.Cancel();

            this.OnStop();

            this.Logger.Info("Exiting Stop");
        }
        catch (Exception ex)
        {
            this.Logger.Error(ex, $"Error Stopping:{this.GetType()}");

            throw;
        }
    }

    /// <inheritdoc/>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        this.Start();
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        this.Stop();
        return Task.CompletedTask;
    }

    /// <summary>
    ///     Hook to implement custom start logic
    /// </summary>
    protected virtual void OnStart()
    {
    }

    /// <summary>
    ///     Hook to implement custom stop logic
    /// </summary>
    protected virtual void OnStop()
    {
    }

    /// <inheritdoc/>
    protected sealed override void OnInitialize()
        => this.Start();

    /// <inheritdoc/>
    protected override void OnDisposeManagedResources()
    {
        base.OnDisposeManagedResources();
        this.cts.Dispose();
    }
}