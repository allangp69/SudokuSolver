namespace SudokuSolver
{
    public class SudokuRow
        : SudokuShape
    {
        public SudokuRow(int rowNumber)
        {
            RowNumber = rowNumber;
        }
        public int RowNumber { get; }
    }
}
