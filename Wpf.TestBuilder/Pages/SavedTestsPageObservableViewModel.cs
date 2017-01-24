using Wpf.TestBuilder.Models;

namespace Wpf.TestBuilder.Pages
{
    public class SavedTestsPageObservableViewModel : ObservableViewModel
    {
        private TestsModel _testsModel = new TestsModel();

        public TestsModel TestsModel
        {
            get { return _testsModel; }
            set
            {
                _testsModel = value;
                OnPropertyChanged();
            }
        }

        public SavedTestsPageObservableViewModel()
        {
            TestsModel.Open();
        }
    }
}