using Wpf.TestBuilder.Models;
using Wpf.TestBuilder.Pages;

namespace Wpf.TestBuilder
{
    public class MainWindowViewModel : ViewModelBase
    {
        public TestPage TestPage { get; set; }
        public SavedTestsPage SavedTestsPage { get; set; } = new SavedTestsPage();
        public TestModel TestModelForEdit { get; set; }
    }
}
