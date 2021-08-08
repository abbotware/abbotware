// -----------------------------------------------------------------------
// <copyright file="DisposeState.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Objects
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Abbotware.Core.Extensions;

    /// <summary>
    ///     represents the possible states the disposable object can be in
    /// </summary>
    public struct DisposeState : IEquatable<DisposeState>
    {
        /// <summary>
        /// Contains the Disposed state of the containing object
        /// </summary>
        private int stateValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposeState"/> struct.
        /// </summary>
        /// <param name="state">initial value</param>
        public DisposeState(State state)
        {
            this.stateValue = (int)state;
        }

        /// <summary>
        /// Allowable state values
        /// </summary>
        public enum State : int
        {
            /// <summary>
            /// Const value that represents the object is in it's initial state
            /// </summary>
            Initial = 0,

            /// <summary>
            /// Const value that represents the object is in the process of being disposed
            /// </summary>
            Disposing = 1,

            /// <summary>
            /// Const value that represents the object is disposed
            /// </summary>
            Disposed = 2,
        }

        /// <summary>
        /// Gets or sets the dispose state value
        /// </summary>
        public State Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return (State)Volatile.Read(ref this.stateValue);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                Volatile.Write(ref this.stateValue, (int)value);
            }
        }

        /// <summary>
        /// equal operator
        /// </summary>
        /// <param name="left">left</param>
        /// <param name="right">right</param>
        /// <returns>true if not equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(DisposeState left, DisposeState right)
        {
            return !(left == right);
        }

        /// <summary>
        /// not equal operator
        /// </summary>
        /// <param name="left">left</param>
        /// <param name="right">right</param>
        /// <returns>true if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(DisposeState left, DisposeState right)
        {
            return left.stateValue == right.stateValue;
        }

        /// <summary>
        /// Performs an atomic swap
        /// </summary>
        /// <param name="newState">new value</param>
        /// <returns>The original value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public State Swap(State newState)
        {
            var newIntState = (int)newState;
            return (State)Interlocked.Exchange(ref this.stateValue, newIntState);
        }

        /// <summary>
        /// Performs an atomic compare and swap
        /// </summary>
        /// <param name="compareValue">comparand</param>
        /// <param name="newState">new value</param>
        /// <returns>The original value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public State CompareAndSwap(State compareValue, State newState)
        {
            var compareIntValue = (int)compareValue;
            var newIntState = (int)newState;

            return (State)Interlocked.CompareExchange(ref this.stateValue, newIntState, compareIntValue);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            switch ((State)this.stateValue)
            {
                case State.Initial:
                case State.Disposing:
                case State.Disposed:
                    return this.stateValue.ToString(CultureInfo.InvariantCulture);
                default:
                    return "DisposeState.Unknown";
            }
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (!this.StructPossiblyEquals<DisposeState>(obj, out var other))
            {
                return false;
            }

#if NETSTANDARD2_0
            if (other.HasValue)
            {
                return this.Equals(other);
            }
            else
            {
                return false;
            }
#else
            return this.Equals(other);
#endif
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
#if NETSTANDARD2_0
            return this.stateValue.GetHashCode();
#else
            return HashCode.Combine(this.stateValue);
#endif
        }

        /// <inheritdoc/>
        public bool Equals(DisposeState other)
        {
            return this.stateValue == other.stateValue;
        }
    }
}
