namespace Abbotware.UnitTests.Quant.LinearAlgebra
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Abbotware.Quant.LinearAlgebra;
    using CsvHelper;
    using NUnit.Framework;

    internal class MatrixTests
    {
        [TestCase]
        public void Create_MxN()
        {
            var m1 = new Matrix<double>(1, 4);
            Assert.That(m1, Is.Not.Null);
            Assert.That(m1, Is.Not.Null);

            var m2 = new Matrix<double>(4, 1);

            Assert.That(m2, Is.Not.Null);
            var m3 = new Matrix<double>(4, 4);
            Assert.That(m3, Is.Not.Null);
        }

        [TestCase]
        public void Create_Array()
        {
            int[,] l =
                {
                    { 1, 2 },
                    { 3, 4 },
                    { 5, 6 },
                    { 7, 8 },
                };

            var m1 = new Matrix<int>(l);

            int[][] l2 =
                [
                    [1, 2],
                    [3, 4],
                    [5, 6],
                    [7, 8],
                ];

            var m2 = new Matrix<int>(l2);
        }

        [TestCase]
        public void Row_x_Column()
        {
            var v = new ColumnVector<double>(1, 2, 3, 4);
            var wᵗ = new RowVector<double>(1, 2, 3, 4);

            var x = wᵗ * v;

            Assert.That(x, Is.EqualTo(30));
        }

        [TestCase]
        public void MxN_x_NxP()
        {
            var A = new Matrix<double>(
                [
                    [1, 0, 1],
                    [2, 1, 1],
                    [0, 1, 1],
                    [1, 1, 2],
                ]);

            var B = new Matrix<double>(
                [
                    [1, 2, 1],
                    [2, 3, 1],
                    [4, 2, 2],
                ]);

            var x = A * B;

            var s = x.ToString();

            Assert.That(s, Is.EqualTo("[5 4 3]\r\n[8 9 5]\r\n[6 5 3]\r\n[11 9 6]\r\n"));
        }

        [TestCase]
        public void Row_x_Matrix()
        {
            var A = new Matrix<double>(
                [
                    [1, 0, 1],
                    [2, 1, 1],
                    [0, 1, 1],
                    [1, 1, 2],
                ]);

            var wᵗ = new RowVector<double>(1, 2, 3);

            var x = wᵗ * A;

            var s = x.ToString();

            Assert.That(s, Is.EqualTo("[4 6 15]\r\n"));
        }

        [TestCase]
        public void Matrix_x_Column()
        {
            var A = new Matrix<double>(
                [
                    [1, 0, 1],
                    [2, 1, 1],
                    [0, 1, 1],
                    [1, 1, 2],
                ]);

            var v = new ColumnVector<double>(1, 3, 2, 3);

            var x = A * v;

            var s = x.ToString();

            Assert.That(s, Is.EqualTo("[10]\r\n[8]\r\n[12]\r\n[0]\r\n"));
        }

        [TestCase]
        public void PercentageChange_Mean()
        {
            var df = LoadDataSheet();
            var u = df.PercentageChange().ColumnStatistics.Mean();
            Assert.That(-0.0101, Is.EqualTo(u[0]).Within(.0001));
            Assert.That(-0.0011, Is.EqualTo(u[1]).Within(.0001));
            Assert.That(0.0015, Is.EqualTo(u[2]).Within(.0001));
            Assert.That(0.0048, Is.EqualTo(u[3]).Within(.0001));
            Assert.That(0.0032, Is.EqualTo(u[4]).Within(.0001));
        }

        [TestCase]
        public void CovarianceMatrix_Example()
        {
            var df = LoadDataSheet();

            var x = df.PercentageChange().CovarianceMatrix().ToString();

            Assert.AreEqual("[0.00034588600025363967 7.980401130261063E-05 -1.927664822333721E-05 -5.5453899760169656E-05 0.000109679190871947]\r\n[7.980401130261063E-05 0.00014669444516753578 4.741956877115166E-05 1.0581677927140813E-05 -1.2142138578168365E-05]\r\n[-1.927664822333721E-05 4.741956877115166E-05 7.896367373363606E-05 1.6249883017728147E-05 -4.687186253996001E-05]\r\n[-5.5453899760169656E-05 1.0581677927140813E-05 1.6249883017728147E-05 2.4765061794046518E-05 -1.4542531420401893E-05]\r\n[0.000109679190871947 -1.2142138578168365E-05 -4.687186253996001E-05 -1.4542531420401893E-05 7.311619036918135E-05]\r\n", x);
        }

        private static DataSheet<double, DateTimeOffset> LoadDataSheet()
        {
            var rows = new List<double[]>();
            var labels = new List<string>();
            var index = new List<DateTimeOffset>();

            using (var reader = new StreamReader("samples\\example.01.01.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                labels = csv.HeaderRecord!.ToList();

                while (csv.Read())
                {
                    var columns = csv.HeaderRecord!.Length;
                    var row = new double[columns - 1];

                    index.Add(csv.GetField<DateTimeOffset>(0));

                    for (int i = 0; i < columns - 1; ++i)
                    {
                        row[i] = csv.GetField<double>(i + 1);
                    }

                    rows.Add(row);
                }

                return new(rows.ToArray(), index, labels);
            }
        }
    }
}
