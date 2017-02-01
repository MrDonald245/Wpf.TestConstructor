namespace Wpf.TestBuilder.Dialogs
{
    public class InputTextDialogObservableViewModel : ObservableViewModel
    {
        private string _inputNameValue;

        public string InputNameValue
        {
            get { return _inputNameValue; }
            set
            {
                _inputNameValue = value;
                OnPropertyChanged();
            }
        }
    }
}