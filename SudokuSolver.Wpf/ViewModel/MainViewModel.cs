using System.Collections.Generic;
using System.IO;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace SudokuSolver.Wpf.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Square1ViewModel = new SquareViewModel();
            Square2ViewModel = new SquareViewModel();
            Square3ViewModel = new SquareViewModel();
            Square4ViewModel = new SquareViewModel();
            Square5ViewModel = new SquareViewModel();
            Square6ViewModel = new SquareViewModel();
            Square7ViewModel = new SquareViewModel();
            Square8ViewModel = new SquareViewModel();
            Square9ViewModel = new SquareViewModel();
            LoadFromResource("TestCase_Super_Svær_1");
        }

        private void LoadFromResource(string resourceFileName)
        {
            var viewmodels = new List<SquareViewModel>
            {
                Square1ViewModel,
                Square2ViewModel,
                Square3ViewModel,
                Square4ViewModel,
                Square5ViewModel,
                Square6ViewModel,
                Square7ViewModel,
                Square8ViewModel,
                Square9ViewModel
            };
            var testCase = TestCaseResources.ResourceManager.GetString(resourceFileName);
            var reader = new StringReader(testCase);
            var line = reader.ReadLine();
            var rowNumber = 0;
            var rowNumberOffset = 0;
            while (line != null)
            {
                var columnNumber = 0;
                var columnNumberOffset = 0;
                var numbers = line.Split(";".ToCharArray());
                foreach (var number in numbers)
                {
                    if (!string.IsNullOrEmpty(number))
                    {
                        var value = 0;
                        if (int.TryParse(number, out value))
                        {
                            var squareViewModel = GetSquareViewModelFromRowAndColumnNumber(rowNumber, columnNumber, viewmodels);
                            squareViewModel.SetTextValue(rowNumberOffset, columnNumberOffset, value);
                        }
                    }
                    columnNumber++;
                    columnNumberOffset++;
                    if (columnNumberOffset == 3)
                    {
                        columnNumberOffset = 0;
                    }
                }
                rowNumber++;
                rowNumberOffset++;
                if (rowNumberOffset == 3)
                {
                    rowNumberOffset = 0;
                }
                line = reader.ReadLine();
            }
        }

        private SquareViewModel GetSquareViewModelFromRowAndColumnNumber(int rowNumber, int columnNumber, List<SquareViewModel> squareViewModels)
        {
            var squareNumber = SudokuBoard.CalculateSquareNumber(rowNumber, columnNumber);
            return squareViewModels[squareNumber];
        }

        private SquareViewModel _square1ViewModel;
        public SquareViewModel Square1ViewModel { get { return _square1ViewModel; } set { Set(() => Square1ViewModel, ref _square1ViewModel , value); } }

        private SquareViewModel _square2ViewModel;
        public SquareViewModel Square2ViewModel { get { return _square2ViewModel; } set { Set(() => Square2ViewModel, ref _square2ViewModel, value); } }

        private SquareViewModel _square3ViewModel;
        public SquareViewModel Square3ViewModel { get { return _square3ViewModel; } set { Set(() => Square3ViewModel, ref _square3ViewModel, value); } }

        private SquareViewModel _square4ViewModel;
        public SquareViewModel Square4ViewModel { get { return _square4ViewModel; } set { Set(() => Square4ViewModel, ref _square4ViewModel, value); } }
        
        private SquareViewModel _square5ViewModel;
        public SquareViewModel Square5ViewModel { get { return _square5ViewModel; } set { Set(() => Square5ViewModel, ref _square5ViewModel, value); } }

        private SquareViewModel _square6ViewModel;
        public SquareViewModel Square6ViewModel { get { return _square6ViewModel; } set { Set(() => Square6ViewModel, ref _square6ViewModel, value); } }

        private SquareViewModel _square7ViewModel;
        public SquareViewModel Square7ViewModel { get { return _square7ViewModel; } set { Set(() => Square7ViewModel, ref _square7ViewModel, value); } }

        private SquareViewModel _square8ViewModel;
        public SquareViewModel Square8ViewModel { get { return _square8ViewModel; } set { Set(() => Square8ViewModel, ref _square8ViewModel, value); } }
        
        private SquareViewModel _square9ViewModel;
        public SquareViewModel Square9ViewModel { get { return _square9ViewModel; } set { Set(() => Square9ViewModel, ref _square9ViewModel, value); }
        }

        private RelayCommand _solveCommand;
        public RelayCommand SolveCommand 
        { 
            get { return _solveCommand ?? (_solveCommand = new RelayCommand(Solve, Solve_CanExecute)); }
        }

        private void Solve()
        {
            var numbersPerUnit = 9;
            var board = new SudokuBoard(numbersPerUnit);
            SetInitialValues(board);
            var solver = new Solver();
            solver.Solve(board);
            SetFinalValues(board);
            if (!board.IsSolved)
            {
                MessageBox.Show("Could not solve board");
            }
        }

        private void SetFinalValues(SudokuBoard board)
        {
            var viewmodels = new List<SquareViewModel>
            {
                Square1ViewModel,
                Square2ViewModel,
                Square3ViewModel,
                Square4ViewModel,
                Square5ViewModel,
                Square6ViewModel,
                Square7ViewModel,
                Square8ViewModel,
                Square9ViewModel
            };
            var v = 0;
            foreach (var sudokuSquare in board.Squares)
            {
                var squareViewModel = viewmodels[v];
                var c = 0;
                foreach (var sudokuSquareCell in sudokuSquare.Cells)
                {
                    squareViewModel.SetTextValue(c, sudokuSquareCell.Number);
                    c++;
                }
                v++;
            }
        }

        private void SetInitialValues(SudokuBoard board)
        {
            var viewmodels = new List<SquareViewModel>
            {
                Square1ViewModel,
                Square2ViewModel,
                Square3ViewModel,
                Square4ViewModel,
                Square5ViewModel,
                Square6ViewModel,
                Square7ViewModel,
                Square8ViewModel,
                Square9ViewModel
            };
            var v = 0;
            foreach (var sudokuSquare in board.Squares)
            {
                var squareViewModel = viewmodels[v];
                var c = 0;
                foreach (var sudokuSquareCell in sudokuSquare.Cells)
                {
                    var textValue = squareViewModel.GetTextValue(c);
                    if (!string.IsNullOrEmpty(textValue))
                    {
                        int.TryParse(textValue, out var value);
                        sudokuSquareCell.Number = value;
                    }
                    c++;
                }
                v++;
            }
        }

        private bool Solve_CanExecute()
        {
            return true;
        }
    }
}