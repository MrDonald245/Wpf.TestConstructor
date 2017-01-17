using Wpf.TestBuilder.Pages;

namespace Wpf.TestBuilder.Dialogs
{
    public class DialogWindowAddNewOptionObservableViewModel : ObservableViewModel
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
