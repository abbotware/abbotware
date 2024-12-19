// -----------------------------------------------------------------------
// <copyright file="ClassDataRecord{TClass}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Data.BulkInsert
{
    using System;
    using System.Data;

    /// <summary>
    ///     Wrapper DataRecord for TClass used by DataReader / BulkInsert
    /// </summary>
    /// <typeparam name="TClass">Class Type</typeparam>
    public sealed class ClassDataRecord<TClass> : IDataRecord
    {
        /// <inheritdoc />
        public int FieldCount { get; }

        /// <inheritdoc />
        object IDataRecord.this[int i]
        {
            get { throw new NotImplementedException(); }
        }

        /// <inheritdoc />
        object IDataRecord.this[string name]
        {
            get { throw new NotImplementedException(); }
        }

        /// <inheritdoc />
        public string GetName(int i)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Type GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object GetValue(int i)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public long GetBytes(int i, long fieldOffset, byte[]? buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public long GetChars(int i, long fieldoffset, char[]? buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }
    }
}