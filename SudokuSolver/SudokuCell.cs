namespace SudokuSolver
{
    public class SudokuCell
    {
        public SudokuCell(int columnNumber, int rowNumber)
        {
            this.ColumnNumber = columnNumber;
            this.RowNumber = rowNumber;
        }

        public int ColumnNumber { get; }
        public int RowNumber { get; }
        public bool IsFilled { get { return Number.HasValue; } }

        private int? _number;

        public int? Number
        {
            get
            {
                return _number;
            }
            set
            {
                //if (this.ColumnNumber == 0 && this.RowNumber == 3 && value == 4)
                //{
                //    var text = "Hallo";
                //}
                _number = value;
            }
        }
    }
}
