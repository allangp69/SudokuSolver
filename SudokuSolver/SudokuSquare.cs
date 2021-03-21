namespace SudokuSolver
{
    public class SudokuSquare
        : SudokuShape
    {
        public int SquareNumber { get; }

        public SudokuSquare(int squareNumber)
        {
            SquareNumber = squareNumber;
        }
    }
}
