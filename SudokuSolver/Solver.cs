using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    public class Solver
    {
        public void Solve(SudokuBoard board)
        {
            SolveBoard(board);
            if (!board.IsSolved)
            {
                SolveByBruteForce(board);
            }
        }

        private void SolveBoard(SudokuBoard board)
        {
            var c = 0;
            var maxNumber = board.Rows.Count;
            while (!board.IsSolved)
            {
                if (c > 200)
                {
                    return;
                }
                for (int number = 1; number <= maxNumber; number++)
                {
                    SolveByRow(board, number);
                }
                for (int number = 1; number <= maxNumber; number++)
                {
                    SolveByColumn(board, number);
                }
                c++;
            }
        }

        private void SolveByBruteForce(SudokuBoard board)
        {
            var listOfAttempts = new List<Attempt>();
            foreach (var sudokuRow in board.Rows)
            {
                foreach (var sudokuRowCell in sudokuRow.Cells.Where(c => !c.IsFilled))
                {
                    var column = board.Columns.FirstOrDefault(c => c.ColumnNumber == sudokuRowCell.ColumnNumber);
                    var squareNumber = SudokuBoard.CalculateSquareNumber(sudokuRowCell.RowNumber, sudokuRowCell.ColumnNumber);
                    var square = board.Squares.FirstOrDefault(s => s.SquareNumber == squareNumber);
                    var unusedNumber = 0;
                    for (int i = 1; i <= board.NumbersPerUnit; i++)
                    {
                        if (!sudokuRow.HasNumberBeenUsed(i) && !column.HasNumberBeenUsed(i) && !square.HasNumberBeenUsed(i) && !DoesAttemptExist(sudokuRowCell, i, listOfAttempts))
                        {
                            unusedNumber = i;
                        }
                    }
                    if (unusedNumber == 0)
                    {
                        continue;
                    }
                    //Try setting the number of the cell to the first unused number
                    listOfAttempts.Add(new Attempt(sudokuRowCell, unusedNumber));
                    var attemptBoard = board.Clone();
                    var attemptCell = attemptBoard.GetCell(sudokuRowCell.ColumnNumber, sudokuRowCell.RowNumber);
                    attemptCell.Number = unusedNumber;
                    SolveBoard(attemptBoard);
                    if (attemptBoard.IsSolved)
                    {
                        sudokuRowCell.Number = unusedNumber;
                        SolveBoard(board);
                        return;
                    }
                }
            }
        }

        private bool DoesAttemptExist(SudokuCell sudokuRowCell, int number, List<Attempt> listOfAttempts)
        {
            if (listOfAttempts == null || !listOfAttempts.Any())
            {
                return false;
            }
            if (listOfAttempts.Any(a => a.SudokuCell == sudokuRowCell && a.Number == number))
            {
                return true;
            }
            return false;
        }

        private void SolveByRow(SudokuBoard board, int number)
        {
            foreach (var sudokuRow in board.Rows.Where(r => !r.HasNumberBeenUsed(number)))
            {
                var possibleColumns = new List<int>();
                foreach (var sudokuRowCell in sudokuRow.Cells.Where(c => !c.IsFilled))
                {
                    //Check if the number can be used in this cell
                    var column = board.Columns.FirstOrDefault(c => c.ColumnNumber == sudokuRowCell.ColumnNumber);
                    if (column.HasNumberBeenUsed(number))
                    {
                        continue;
                    }
                    var squareNumber = SudokuBoard.CalculateSquareNumber(sudokuRowCell.RowNumber, sudokuRowCell.ColumnNumber);
                    var square = board.Squares.FirstOrDefault(s => s.SquareNumber == squareNumber);
                    if (square.HasNumberBeenUsed(number))
                    {
                        continue;
                    }
                    //The number has not been used in either the present row, column or square
                    //Check if the number is the only one that can be used by adding all
                    //possible numbers except the number being checked to the possible numbers list
                    var possibleNumbers = new List<int>();
                    for (int i = 1; i <= board.NumbersPerUnit; i++)
                    {
                        if (i == number)
                        {
                            continue;
                        }
                        if (!sudokuRow.HasNumberBeenUsed(i) && !column.HasNumberBeenUsed(i) && !square.HasNumberBeenUsed(i))
                        {
                            possibleNumbers.Add(i);
                        }
                    }
                    //If the possible numbers list contains any numbers, then the number being checked is not the only option in this cell
                    if (possibleNumbers.Any())
                    {
                        //But the number could possibly be used in the column of the present cell - otherwise the code would have skipped previously
                        possibleColumns.Add(sudokuRowCell.ColumnNumber);
                        continue;
                    }
                    //But if the possible numbers list does NOT contain any numbers
                    //Then Number IS the only one that is allowed
                    sudokuRowCell.Number = number;
                    return;
                }
                //And if the list of possible columns contains ONLY one record, then this cell is the only place where the number can be used
                if (possibleColumns.Count == 1)
                {
                    sudokuRow.Cells.FirstOrDefault(c => c.ColumnNumber == possibleColumns.First()).Number = number;
                }
            }
        }

        private void SolveByColumn(SudokuBoard board, int number)
        {
            foreach (var sudokuColumn in board.Columns.Where(c => !c.HasNumberBeenUsed(number)))
            {
                var possibleRows = new List<int>();
                foreach (var sudokuRowCell in sudokuColumn.Cells.Where(c => !c.IsFilled))
                {
                    //Check if the number can be used in this cell
                    var row = board.Rows.FirstOrDefault(r => r.RowNumber == sudokuRowCell.RowNumber);
                    if (row.HasNumberBeenUsed(number))
                    {
                        continue;
                    }
                    var squareNumber = SudokuBoard.CalculateSquareNumber(sudokuRowCell.RowNumber, sudokuRowCell.ColumnNumber);
                    var square = board.Squares.FirstOrDefault(s => s.SquareNumber == squareNumber);
                    if (square.HasNumberBeenUsed(number))
                    {
                        continue;
                    }
                    //The number has not been used in either the present row, column or square
                    //Check if the number is the only one that can be used by adding all
                    //possible numbers except the number being checked to the possible numbers list
                    var possibleNumbers = new List<int>();
                    for (int i = 1; i <= board.NumbersPerUnit; i++)
                    {
                        if (i == number)
                        {
                            continue;
                        }
                        if (!sudokuColumn.HasNumberBeenUsed(i) && !row.HasNumberBeenUsed(i) && !square.HasNumberBeenUsed(i))
                        {
                            possibleNumbers.Add(i);
                        }
                    }
                    //If the possible numbers list contains any numbers, then the number being checked is not the only option in this cell
                    if (possibleNumbers.Any())
                    {
                        //But the number could possibly be used in the row of the present cell - otherwise the code would have skipped previously
                        possibleRows.Add(sudokuRowCell.RowNumber);
                        continue;
                    }
                    //But if the possible numbers list does NOT contain any numbers
                    //Then Number IS the only one that is allowed
                    sudokuRowCell.Number = number;
                    return;
                }
                //And if the list of possible rows contains ONLY one record, then this cell is the only place where the number can be used
                if (possibleRows.Count == 1)
                {
                    sudokuColumn.Cells.FirstOrDefault(c => c.RowNumber == possibleRows.First()).Number = number;
                }
            }
        }
    }

    internal class Attempt
    {
        public Attempt(SudokuCell sudokuCell, int number)
        {
            SudokuCell = sudokuCell;
            Number = number;
        }
        public SudokuCell SudokuCell { get; set; }
        public int Number { get; set; }
    }
}
