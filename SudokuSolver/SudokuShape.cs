using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    public abstract class SudokuShape
    {
        public SudokuShape()
        {
            this.Cells = new List<SudokuCell>();
        }
        public bool IsFilled
        {
            get { return Cells.All(c => c.IsFilled); }
        }
        public List<SudokuCell> Cells
        {
            get; set;
        }
        public void AddCell(SudokuCell cell)
        {
            Cells.Add(cell);
        }
        public SudokuCell GetCell(int rowNumber, int columnNumber)
        {
            return Cells.FirstOrDefault(c => c.ColumnNumber == columnNumber && c.RowNumber == rowNumber);
        }

        public bool HasNumberBeenUsed(int number)
        {
            foreach (var sudokuCell in Cells)
            {
                if (sudokuCell.Number.HasValue && sudokuCell.Number.Value == number)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsComplete()
        {
            if (!Cells.All(c => c.IsFilled))
            {
                return false;
            }
            var numbers = new List<int>();
            foreach (var sudokuCell in Cells)
            {
                var value = sudokuCell.Number.Value;
                if (numbers.Contains(value))
                {
                    throw new Exception("Duplicate number");
                }
                numbers.Add(value);
            }
            return numbers.Count == 9;
        }
    }
}