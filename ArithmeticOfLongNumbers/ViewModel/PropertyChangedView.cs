using System.ComponentModel;

namespace ArithmeticOfLongNumbers.ViewModel
{
    public abstract class PropertyChangedView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
