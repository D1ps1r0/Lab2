using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Lab2_TicTacToe.Commands;
using Lab2_TicTacToe.Models;

namespace Lab2_TicTacToe.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private GameState game = new GameState();
        private int level = 1;
        private string result = "Ваш хід: X";
        private bool gameOver = false;

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public int Level
        {
            get { return level; }
            set
            {
                level = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Level"));
            }
        }

        public string Result
        {
            get { return result; }
            set
            {
                result = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Result"));
            }
        }

        public ICommand MakeMoveCommand { get; set; }
        public ICommand NewGameCommand { get; set; }

        public MainViewModel()
        {
            for (int i = 0; i < 9; i++)
                Cells.Add(new CellViewModel(i));

            MakeMoveCommand = new RelayCommand(MakeMove);
            NewGameCommand = new RelayCommand(NewGame);
        }

        private void MakeMove(object parameter)
        {
            if (gameOver)
                return;

            int index = (int)parameter;

            if (!game.MakeMove(index, 'X'))
                return;

            UpdateBoard();

            if (CheckGame())
                return;

            int computerMove = game.GetComputerMove(Level);
            game.MakeMove(computerMove, 'O');
            UpdateBoard();

            if (!CheckGame())
                Result = "Ваш хід: X";
        }

        private bool CheckGame()
        {
            char winner = game.CheckWinner();

            if (winner == 'X')
            {
                Result = "Ви перемогли!";
                gameOver = true;
                return true;
            }

            if (winner == 'O')
            {
                Result = "Переміг комп'ютер!";
                gameOver = true;
                return true;
            }

            if (game.IsDraw())
            {
                Result = "Нічия!";
                gameOver = true;
                return true;
            }

            return false;
        }

        private void UpdateBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                char symbol = game.GetCell(i);
                Cells[i].Value = symbol == ' ' ? "" : symbol.ToString();
            }
        }

        private void NewGame(object parameter)
        {
            game.Clear();
            gameOver = false;
            Result = "Ваш хід: X";
            UpdateBoard();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
