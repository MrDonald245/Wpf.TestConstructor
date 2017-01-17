using System.Windows;

namespace Wpf.TestBuilder.Dialogs
{
    /// <summary>
    /// Interaction logic for InputTextDialog.xaml
    /// </summary>
    public partial class InputTextDialog : Window
    {
        private DialogWindowAddNewOptionObservableViewModel _observableViewModel = new DialogWindowAddNewOptionObservableViewModel();

        public string OptionNameResult => _observableViewModel.InputNameValue;

        public InputTextDialog()
        {
            InitializeComponent();

            DataContext = _observableViewModel;
        }

        /// <summary>
        /// Occures when accept button was clicked.
        /// Save TextBoxName data to a propery and set Dialog result to 'true'.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAccept_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        /// <summary>
        /// Occures when cancel button was clicked.
        /// Set Dialog result to 'false'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
