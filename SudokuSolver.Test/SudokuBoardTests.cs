using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SudokuSolver.Test
{
    [TestClass]
    public class SudokuBoardTests
    {
        [TestMethod]
        [DataRow(0, 0, 0)]
        [DataRow(1, 1, 0)]
        [DataRow(2, 2, 0)]
        [DataRow(3, 3, 4)]
        [DataRow(0, 3, 1)]
        [DataRow(3, 0, 3)]
        public void TestCalculateSquareNumber(int rowNumber, int columnNumber, int expectedValue)
        {
            var result = SudokuBoard.CalculateSquareNumber(rowNumber, columnNumber);
            Assert.AreEqual(expectedValue, result);
        }
    }
}
