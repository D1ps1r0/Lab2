using System.ComponentModel;

namespace Lab2_TicTacToe.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private string value = "";

        public int Index { get; set; }

        public string Value
        {
            get { return value; }
            set
            {
                this.value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CellViewModel(int index)
        {
            Index = index;
        }
    }
}
