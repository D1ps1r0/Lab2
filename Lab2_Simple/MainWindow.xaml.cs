using System.Windows;
using Lab2_TicTacToe.ViewModels;

namespace Lab2_TicTacToe
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
