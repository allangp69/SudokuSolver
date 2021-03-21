using System.Collections.Generic;
using System.IO;
using System.Linq;
using SudokuSolver;

namespace Test.SudokuSolver.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new SudokuBoard(9);
            System.Console.WriteLine("*****************************************************");
            System.Console.WriteLine("************************TestCase*********************");
            var testCase = "TestCase_Medium_1";
            System.Console.WriteLine(testCase);
            var initialValues = GetInitialValues(testCase);
            board.SetInitialValues(initialValues);
            var solver = new Solver();
            solver.Solve(board);
            System.Console.WriteLine("*****************************************************");
            System.Console.WriteLine("************************Board*********************");
            for (int x = 0; x < board.Rows.Count; x++)
            {
                System.Console.Write("|");
                var row = board.Rows.FirstOrDefault(r => r.RowNumber == x);
                for (int y = 0; y < board.Columns.Count; y++)
                {
                    var cell = row.Cells.FirstOrDefault(c => c.ColumnNumber == y);
                    var number = cell.Number.HasValue ? cell.Number.Value.ToString() : " ";
                    System.Console.Write(number);
                    System.Console.Write("|");
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine("*****************************************************");
            System.Console.WriteLine("Solved: " + board.IsSolved);
        }

        private static List<SudokuCell> GetInitialValues(string resourceFileName)
        {
            var testCase = TestCaseResources.ResourceManager.GetString(resourceFileName);
            var retval = new List<SudokuCell>();
            var reader = new StringReader(testCase);
            var line = reader.ReadLine();
            var rowNumber = 0;
            while (line != null)
            {
                var columnNumber = 0;
                var numbers = line.Split(";");
                foreach (var number in numbers)
                {
                    if (!string.IsNullOrEmpty(number))
                    {
                        var value = 0;
                        if (int.TryParse(number, out value))
                        {
                            retval.Add(CreateCell(rowNumber, columnNumber, value));
                        }
                    }
                    columnNumber++;
                }
                rowNumber++;
                line = reader.ReadLine();
            }
            return retval;
        }

        //private static List<SudokuCell> GetInitialValues(string resourceFileName)
        //{
        //    var testCase = Resource1.ResourceManager.GetString(resourceFileName);
        //    var retval = new List<SudokuCell>();
        //    var rowNumber = 0;
        //    var columnNumber = 0;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 9));
        //    columnNumber = 2;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 2));
        //    columnNumber = 4;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 5));
        //    rowNumber = 1;
        //    columnNumber = 0;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 8));
        //    columnNumber = 2;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 5));
        //    columnNumber = 4;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 2));
        //    rowNumber = 2;
        //    columnNumber = 1;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 7));
        //    columnNumber = 3;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 6));
        //    rowNumber = 3;
        //    columnNumber = 1;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 5));
        //    columnNumber = 3;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 8));
        //    columnNumber = 6;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 1));
        //    rowNumber = 4;
        //    columnNumber = 2;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 1));
        //    columnNumber = 6;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 2));
        //    rowNumber = 5;
        //    columnNumber = 2;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 8));
        //    columnNumber = 5;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 3));
        //    columnNumber = 7;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 4));
        //    rowNumber = 6;
        //    columnNumber = 5;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 5));
        //    columnNumber = 7;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 3));
        //    rowNumber = 7;
        //    columnNumber = 4;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 4));
        //    columnNumber = 6;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 5));
        //    columnNumber = 8;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 2));
        //    rowNumber = 8;
        //    columnNumber = 4;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 6));
        //    columnNumber = 6;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 7));
        //    columnNumber = 8;
        //    retval.Add(CreateCell(rowNumber, columnNumber, 4));
        //    return retval;
        //}

        private static SudokuCell CreateCell(int rowNumber, int columnNumber, int value)
        {
            return new SudokuCell(columnNumber, rowNumber) { Number = value };
        }
    }
}
