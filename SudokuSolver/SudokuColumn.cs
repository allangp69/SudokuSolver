namespace SudokuSolver
{
    public class SudokuColumn
        :SudokuShape
    {
        public SudokuColumn(int columnNumber)
        {
            ColumnNumber = columnNumber;
        }
        
        public int ColumnNumber { get; }
    }
}
