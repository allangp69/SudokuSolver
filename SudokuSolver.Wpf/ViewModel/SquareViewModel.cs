using System;
using GalaSoft.MvvmLight;

namespace SudokuSolver.Wpf.ViewModel
{
    public class SquareViewModel
        :ViewModelBase
    {
        private string _text00;
        public string Text_0_0 { get { return _text00; } set { Set(() => Text_0_0,ref _text00 , value); } }

        private string _text01;
        public string Text_0_1 { get { return _text01; } set { Set(() => Text_0_1, ref _text01, value); } }

        private string _text02;
        public string Text_0_2 { get { return _text02; } set { Set(() => Text_0_2, ref _text02, value); } }

        private string _text10;
        public string Text_1_0 { get { return _text10; } set { Set(() => Text_1_0, ref _text10, value); } }

        private string _text11;
        public string Text_1_1 { get { return _text11; } set { Set(() => Text_1_1, ref _text11, value); } }

        private string _text12;
        public string Text_1_2 { get { return _text12; } set { Set(() => Text_1_2, ref _text12, value); } }

        private string _text20;
        public string Text_2_0 { get { return _text20; } set { Set(() => Text_2_0, ref _text20, value); } }

        private string _text21;
        public string Text_2_1 { get { return _text21; } set { Set(() => Text_2_1, ref _text21, value); } }

        private string _text22;
        public string Text_2_2 { get { return _text22; } set { Set(() => Text_2_2, ref _text22, value); } }

        public string GetTextValue(int i)
        {
            if (i == 0) { return Text_0_0; }
            if (i == 1) { return Text_0_1; }
            if (i == 2) { return Text_0_2; }
            if (i == 3) { return Text_1_0; }
            if (i == 4) { return Text_1_1; }
            if (i == 5) { return Text_1_2; }
            if (i == 6) { return Text_2_0; }
            if (i == 7) { return Text_2_1; }
            if (i == 8) { return Text_2_2; }
            throw new ArgumentException("Invalid input: " + i);
        }

        public void SetTextValue(int i, int? number)
        {
            if (i == 0) { Text_0_0 = number.HasValue ? number.Value.ToString() : string.Empty; return; }
            if (i == 1) { Text_0_1 = number.HasValue ? number.Value.ToString() : string.Empty; return; }
            if (i == 2) { Text_0_2 = number.HasValue ? number.Value.ToString() : string.Empty; return; }
            if (i == 3) { Text_1_0 = number.HasValue ? number.Value.ToString() : string.Empty; return; }
            if (i == 4) { Text_1_1 = number.HasValue ? number.Value.ToString() : string.Empty; return; }
            if (i == 5) { Text_1_2 = number.HasValue ? number.Value.ToString() : string.Empty; return; }
            if (i == 6) { Text_2_0 = number.HasValue ? number.Value.ToString() : string.Empty; return; }
            if (i == 7) { Text_2_1 = number.HasValue ? number.Value.ToString() : string.Empty; return; }
            if (i == 8) { Text_2_2 = number.HasValue ? number.Value.ToString() : string.Empty; return; }
            throw new ArgumentException("Invalid input: " + i);
        }

        public void SetTextValue(int rowNumber, int columnNumber, int number)
        {
            if (rowNumber == 0 && columnNumber == 0) { Text_0_0 = number.ToString(); return; }
            if (rowNumber == 0 && columnNumber == 1) { Text_0_1 = number.ToString(); return; }
            if (rowNumber == 0 && columnNumber == 2) { Text_0_2 = number.ToString(); return; }
            if (rowNumber == 1 && columnNumber == 0) { Text_1_0 = number.ToString(); return; }
            if (rowNumber == 1 && columnNumber == 1) { Text_1_1 = number.ToString(); return; }
            if (rowNumber == 1 && columnNumber == 2) { Text_1_2 = number.ToString(); return; }
            if (rowNumber == 2 && columnNumber == 0) { Text_2_0 = number.ToString(); return; }
            if (rowNumber == 2 && columnNumber == 1) { Text_2_1 = number.ToString(); return; }
            if (rowNumber == 2 && columnNumber == 2) { Text_2_2 = number.ToString(); return; }
            throw new ArgumentException("Invalid input: rowNumber:" + rowNumber + " - columnNumber:" + columnNumber);
        }
    }
}