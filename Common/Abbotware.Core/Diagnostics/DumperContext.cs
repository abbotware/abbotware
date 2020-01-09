// -----------------------------------------------------------------------
// <copyright file="DumperContext.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Diagnostics
{
    using System;
    using System.Text;

    /// <summary>
    ///     class the stores the current context information for dumping an object's contents to a string
    /// </summary>
    public class DumperContext
    {
        /// <summary>
        ///     the cached string contents of the dumped object
        /// </summary>
        private readonly StringBuilder writer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DumperContext" /> class.
        /// </summary>
        /// <param name="maxDepth">max depth to walk</param>
        public DumperContext(ushort maxDepth)
            : this(maxDepth, new StringBuilder())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DumperContext" /> class.
        /// </summary>
        /// <param name="maxDepth">max depth to walk</param>
        /// <param name="writer">conents of the current dump/walk</param>
        protected DumperContext(ushort maxDepth, StringBuilder writer)
        {
            Arguments.NotNull(writer, nameof(writer));

            this.MaxDepth = maxDepth;
            this.Prefix = string.Empty;
            this.FieldName = string.Empty;
            this.writer = writer;
        }

        /// <summary>
        ///     Gets the field name currently being walked
        /// </summary>
        public string FieldName { get; private set; }

        /// <summary>
        ///     Gets the prefix for logging (if any)
        /// </summary>
        public string Prefix { get; private set; }

        /// <summary>
        ///     Gets the max depth to walk
        /// </summary>
        public ushort MaxDepth { get; }

        /// <summary>
        ///     Gets the current depth of walk
        /// </summary>
        public ushort CurrentDepth { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether or not to dump contents of enumerations
        /// </summary>
        public bool EnumerateCollections { get; private set; }

        /// <summary>
        ///     Appends message to dumped contents
        /// </summary>
        /// <param name="message">message to append</param>
        public void Append(string message)
        {
            Arguments.NotNull(message, nameof(message));

            this.writer.Append(message);
        }

        /// <summary>
        ///     Appends formatted message to dumped contents
        /// </summary>
        /// <param name="formatProvider">format provider</param>
        /// <param name="message">message to append</param>
        /// <param name="parameters">message format parameters</param>
        public void AppendFormat(IFormatProvider formatProvider, string message, params object[] parameters)
        {
            Arguments.NotNull(formatProvider, nameof(formatProvider));
            Arguments.NotNull(message, nameof(message));
            Arguments.NotNull(parameters, nameof(parameters));

            this.writer.AppendFormat(formatProvider, message, parameters);
        }

        /// <summary>
        ///     Creates new context object for the next level of recursion
        /// </summary>
        /// <param name="fieldName">field name being walked at next recursion level</param>
        /// <returns>new context</returns>
        public DumperContext RecursionCopy(string fieldName)
        {
            var context = new DumperContext(this.MaxDepth, this.writer)
                {
                    CurrentDepth = (ushort)(this.CurrentDepth + 1),
                    FieldName = fieldName,
                    Prefix = this.Prefix,
                    EnumerateCollections = this.EnumerateCollections,
                };

            return context;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.writer.ToString();
        }
    }
}