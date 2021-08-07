// -----------------------------------------------------------------------
// <copyright file="SetOnceProperty{TProperty}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core
{
    using System;
    using System.Globalization;

    /// <summary>
    ///     Special class useful for creating 'immutable' like class with public getter / setters.  When used in a special way,
    ///     the setter for a
    /// </summary>
    /// <typeparam name="TProperty">type of the property</typeparam>
    public class SetOnceProperty<TProperty>
    {
        /// <summary>
        ///     mutex for synchronizing the set method
        /// </summary>
        private readonly object setMutex = new();

        /// <summary>
        ///     name of the property
        /// </summary>
        private readonly string name;

        /// <summary>
        ///     callback after property is set
        /// </summary>
        private readonly Action<TProperty>? setAction;

        /// <summary>
        ///     holds the actual value of this property
        /// </summary>
        private TProperty? propertyValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetOnceProperty{TProperty}"/> class.
        /// </summary>
        /// <param name="name">name of the property</param>
        public SetOnceProperty(string name)
            : this(name, null)
        {
            Arguments.NotNullOrWhitespace(name, nameof(name));

            this.name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetOnceProperty{TProperty}"/> class.
        /// </summary>
        /// <param name="name">name of the property</param>
        /// <param name="setAction">action invoked after the set</param>
        public SetOnceProperty(string name, Action<TProperty>? setAction)
        {
            Arguments.NotNullOrWhitespace(name, nameof(name));

            this.name = name;
            this.setAction = setAction;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetOnceProperty{TProperty}"/> class.
        /// </summary>
        /// <param name="name">name of the property</param>
        /// <param name="value">value of property</param>
        public SetOnceProperty(string name, TProperty value)
            : this(name)
        {
            Arguments.NotNullOrWhitespace(name, nameof(name));
            Arguments.NotNull(value, nameof(value));

            this.propertyValue = value;
            this.HasValue = true;
        }

        /// <summary>
        ///     Gets a value indicating whether or not this property has been set
        /// </summary>
        public bool HasValue { get; private set; }

        /// <summary>
        ///     Gets or sets the Value of the property
        /// </summary>
        public TProperty? Value
        {
            get
            {
                // since reading will be more frequent, no need to lock.  Besides, once its set, it can't change or else an exception will be thrown
                if (!this.HasValue)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "property: {0} not set!", this.name));
                }

                return this.propertyValue;
            }

            set
            {
                Arguments.NotNull(value, nameof(value));

                // lock to prevent race condition
                lock (this.setMutex)
                {
                    if (this.HasValue)
                    {
                        throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "can not change value, property: {0} already set!", this.name));
                    }

                    this.propertyValue = value;
                    this.HasValue = true;

                    this.setAction?.Invoke(value);
                }
            }
        }
    }
}