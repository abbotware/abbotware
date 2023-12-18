namespace Abbotware.UnitTests.Quant.LinearAlgebra
{
    using Abbotware.Quant.LinearAlgebra;
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
    }
}
