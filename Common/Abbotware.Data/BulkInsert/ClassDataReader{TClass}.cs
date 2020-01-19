// -----------------------------------------------------------------------
// <copyright file="ClassDataReader{TClass}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Data.BulkInsert
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;

    /// <summary>
    ///     Creates a data reader for a list of TClass objects
    /// </summary>
    /// <typeparam name="TClass">Class type</typeparam>
    public sealed class ClassDataReader<TClass> : BaseComponent, IDataReader
        where TClass : class
    {
        /// <summary>
        ///     iterator that keeps track of location in list
        /// </summary>
        private readonly IEnumerator<TClass> iterator;

        /// <summary>
        ///     meta data for the class
        /// </summary>
        private readonly ClassMetadata<TClass> meta = new ClassMetadata<TClass>();

        /// <summary>
        ///     list of records
        /// </summary>
        private readonly IEnumerable<TClass> records;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClassDataReader{TClass}" /> class.
        /// </summary>
        /// <param name="records">records used for dara reader</param>
        /// <param name="logger">injected logger</param>
        public ClassDataReader(IEnumerable<TClass> records, ILogger logger)
            : base(logger)
        {
            records = Arguments.EnsureNotNull(records, nameof(records));
            Arguments.NotNull(logger, nameof(logger));

            this.records = records;
            this.iterator = records.GetEnumerator();

            this.Logger.Info("Record Count:{0}", this.records.Count());
        }

        /// <inheritdoc />
        public int Depth
        {
            get
            {
                this.ThrowIfDisposed();

                throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
            }
        }

        /// <inheritdoc />
        public int RecordsAffected
        {
            get
            {
                this.ThrowIfDisposed();

                throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
            }
        }

        /// <inheritdoc />
        public bool IsClosed
        {
            get { return this.IsDisposedOrDisposing; }
        }

        /// <inheritdoc />
        public int FieldCount => this.meta.PropertyCount;

        /// <inheritdoc />
        object IDataRecord.this[int i]
        {
            get
            {
                this.ThrowIfDisposed();
                throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
            }
        }

        /// <inheritdoc />
        object IDataRecord.this[string name]
        {
            get
            {
                this.ThrowIfDisposed();
                throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
            }
        }

        /// <inheritdoc />
        public string GetName(int i)
        {
            this.ThrowIfDisposed();

            throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
        }

        /// <inheritdoc />
        public string GetDataTypeName(int i)
        {
            this.ThrowIfDisposed();

            throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
        }

        /// <inheritdoc />
        public Type GetFieldType(int i)
        {
            this.ThrowIfDisposed();

            throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
        }

        /// <inheritdoc />
        public object? GetValue(int i)
        {
            this.ThrowIfDisposed();

            if (this.iterator.Current == null)
            {
                throw new InvalidOperationException("Can not get value when iterator is empty or at the end");
            }

            return this.meta.GetPropertyValue(i, this.iterator.Current);
        }

        /// <inheritdoc />
        public int GetValues(object[] values)
        {
            this.ThrowIfDisposed();

            throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
        }

        /// <inheritdoc />
        public int GetOrdinal(string name)
        {
            this.ThrowIfDisposed();

            return this.meta.GetOrdinal(name);
        }

        /// <inheritdoc />
        public bool GetBoolean(int i)
        {
            this.ThrowIfDisposed();

            throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
        }

        /// <inheritdoc />
        public byte GetByte(int i)
        {
            this.ThrowIfDisposed();

            throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
        }

        /// <inheritdoc />
        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            this.ThrowIfDisposed();

            throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
        }

        /// <inheritdoc />
        public char GetChar(int i)
        {
            this.ThrowIfDisposed();

            throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
        }

        /// <inheritdoc />
        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            this.ThrowIfDisposed();

            throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
        }

        /// <inheritdoc />
        public Guid GetGuid(int i)
        {
            this.ThrowIfDisposed();

            throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
        }

        /// <inheritdoc />
        public short GetInt16(int i)
        {
            this.ThrowIfDisposed();

            throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
        }

        /// <inheritdoc />
        public int GetInt32(int i)
        {
            this.ThrowIfDisposed();

            throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
        }

        /// <inheritdoc />
        public long GetInt64(int i)
        {
            this.ThrowIfDisposed();

            throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
        }

        /// <inheritdoc />
        public float GetFloat(int i)
        {
            this.ThrowIfDisposed();

            throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
        }

        /// <inheritdoc />
        public double GetDouble(int i)
        {
            this.ThrowIfDisposed();

            throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
        }

        /// <inheritdoc />
        public string GetString(int i)
        {
            this.ThrowIfDisposed();

            throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
        }

        /// <inheritdoc />
        public decimal GetDecimal(int i)
        {
            this.ThrowIfDisposed();

            throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
        }

        /// <inheritdoc />
        public DateTime GetDateTime(int i)
        {
            this.ThrowIfDisposed();

            throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
        }

        /// <inheritdoc />
        public IDataReader GetData(int i)
        {
            this.ThrowIfDisposed();

            throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
        }

        /// <inheritdoc />
        public bool IsDBNull(int i)
        {
            this.ThrowIfDisposed();

            if (this.meta.GetPropertyValue(i, this.iterator.Current) == null)
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public void Close()
        {
            this.Dispose();
        }

        /// <inheritdoc />
        public DataTable GetSchemaTable()
        {
            this.ThrowIfDisposed();

            throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
        }

        /// <inheritdoc />
        public bool NextResult()
        {
            this.ThrowIfDisposed();

            throw new NotImplementedException("this is a minimum implementation for bulk insert with streaming support");
        }

        /// <inheritdoc />
        public bool Read()
        {
            this.ThrowIfDisposed();

            return this.iterator.MoveNext();
        }

        /// <inheritdoc />
        protected override void OnDisposeManagedResources()
        {
            this.iterator?.Dispose();

            base.OnDisposeManagedResources();
        }
    }
}