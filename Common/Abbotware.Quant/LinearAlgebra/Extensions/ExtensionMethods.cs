// -----------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.LinearAlgebra.Extensions
{
    /// <summary>
    /// Extension Methods
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Converts a Rectangular array to a Jagged Array
        /// </summary>
        /// <typeparam name="T">data typeS</typeparam>
        /// <param name="rectangularArray">input array</param>
        /// <returns>jagged array</returns>
        /// <remarks>https://stackoverflow.com/questions/21986909/convert-multidimensional-array-to-jagged-array-in-c-sharp</remarks>
        public static T[][] ToJaggedArray<T>(this T[,] rectangularArray)
        {
            int rowsFirstIndex = rectangularArray.GetLowerBound(0);
            int rowsLastIndex = rectangularArray.GetUpperBound(0);
            int numberOfRows = rowsLastIndex + 1;

            int columnsFirstIndex = rectangularArray.GetLowerBound(1);
            int columnsLastIndex = rectangularArray.GetUpperBound(1);
            int numberOfColumns = columnsLastIndex + 1;

            T[][] jaggedArray = new T[numberOfRows][];
            for (int i = rowsFirstIndex; i <= rowsLastIndex; i++)
            {
                jaggedArray[i] = new T[numberOfColumns];

                for (int j = columnsFirstIndex; j <= columnsLastIndex; j++)
                {
                    jaggedArray[i][j] = rectangularArray[i, j];
                }
            }

            return jaggedArray;
        }

        /// <summary>
        /// Transpose column values into a 2d array
        /// </summary>
        /// <typeparam name="T">data type</typeparam>
        /// <param name="columnValues">row</param>
        /// <returns>2d array</returns>
        public static T[][] Transpose<T>(this T[] columnValues)
        {
            var matrix = new T[columnValues.Length][];

            for (int i = 0; i < columnValues.Length; i++)
            {
                matrix[i] =
                    [
                        columnValues[i]
                    ];
            }

            return matrix;
        }

        /// <summary>
        /// Transpose a RowVector into a 2d array
        /// </summary>
        /// <typeparam name="T">data type</typeparam>
        /// <param name="rowVector">row</param>
        /// <returns>2d array</returns>
        public static T[][] AsMatrix<T>(this T[] rowVector)
        {
            var matrix = new T[1][];
            matrix[0] = rowVector;

            return matrix;
        }
    }
}
