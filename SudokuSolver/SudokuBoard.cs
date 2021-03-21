using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    public class SudokuBoard
    {
        public int NumbersPerUnit { get; }
        public SudokuBoard(int numbersPerUnit)
        {
            NumbersPerUnit = numbersPerUnit;
            this.Rows = new List<SudokuRow>();
            this.Columns = new List<SudokuColumn>();
            this.Squares = new List<SudokuSquare>();
            if (numbersPerUnit % 3 != 0)
            {
                throw new Exception("Number of cells in a row must be divisible by 3");
            }
            for (int y = 0; y < numbersPerUnit; y++)
            {
                for (int x = 0; x < numbersPerUnit; x++)
                {
                    var cell = new SudokuCell(x, y);
                    var row = GetRow(y);
                    row.AddCell(cell);
                    var column = GetColumn(x);
                    column.AddCell(cell);
                    var square = GetSquare(y, x);
                    square.AddCell(cell);
                }
            }
        }

        private SudokuRow GetRow(int rowNumber)
        {
            if (this.Rows.All(r => r.RowNumber != rowNumber))
            {
                this.Rows.Add(new SudokuRow(rowNumber));
            }
            return this.Rows.First(r => r.RowNumber == rowNumber);
        }

        private SudokuColumn GetColumn(int columnNumber)
        {
            if (this.Columns.All(c => c.ColumnNumber != columnNumber))
            {
                this.Columns.Add(new SudokuColumn(columnNumber));
            }
            return this.Columns.First(c => c.ColumnNumber == columnNumber);
        }

        private SudokuSquare GetSquare(int rowNumber, int columnNumber)
        {
            var squareNumber = CalculateSquareNumber(rowNumber, columnNumber);
            if (this.Squares.All(s => s.SquareNumber != squareNumber))
            {
                this.Squares.Add(new SudokuSquare(squareNumber));
            }
            return this.Squares.First(s => s.SquareNumber == squareNumber);
        }

        public static int CalculateSquareNumber(int rowNumber, int columnNumber)
        {
            var squareRow = (int)Math.Floor(((double)(rowNumber)) / 3) ;
            var squareColumn = (int)Math.Floor(((double)(columnNumber)) / 3) ;
            return (squareRow * 3) + squareColumn;
        }

        public bool IsSolved
        {
            get
            {
                return Rows.All(r => r.IsFilled) &&
                       Columns.All(r => r.IsFilled) &&
                       Squares.All(r => r.IsFilled);
            }
        }

        public List<SudokuSquare> Squares
        {
            get;
        }
        public List<SudokuRow> Rows
        {
            get;
        }
        public List<SudokuColumn> Columns
        {
            get;
        }

        public SudokuCell GetCell(int columnNumber, int rowNumber)
        {
            var row = Rows.FirstOrDefault(r => r.RowNumber == rowNumber);
            var cell = row.Cells.FirstOrDefault(c => c.ColumnNumber == columnNumber);
            return cell;
        }

        public void SetInitialValues(List<SudokuCell> initialValues)
        {
            foreach (var initialValue in initialValues)
            {
                var cell = GetCell(initialValue.ColumnNumber, initialValue.RowNumber);
                cell.Number = initialValue.Number;
            }
        }

        public bool CanNumberBeUsedInCell(int number, SudokuCell cell)
        {
            var row = GetRow(cell.RowNumber);
            if (row.HasNumberBeenUsed(number))
            {
                return false;
            }
            var column = GetColumn(cell.ColumnNumber);
            if (column.HasNumberBeenUsed(number))
            {
                return false;
            }
            var square = GetSquare(cell.RowNumber, cell.ColumnNumber);
            if (square.HasNumberBeenUsed(number))
            {
                return false;
            }
            return true;
        }

        public SudokuBoard Clone()
        {
            var retval = new SudokuBoard(NumbersPerUnit);
            for (int i = 0; i < Rows.Count; i++)
            {
                for (int j = 0; j < Columns.Count; j++)
                {
                    retval.GetCell(j, i).Number = this.GetCell(j, i).Number;
                }
            }
            return retval;
        }
    }
}
