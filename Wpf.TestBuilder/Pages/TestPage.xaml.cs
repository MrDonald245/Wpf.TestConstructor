using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Wpf.TestBuilder.Models;

namespace Wpf.TestBuilder.Pages
{
    /// <summary>
    /// Interaction logic for TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        /// <summary>
        /// Occures when all tabs are closed.
        /// </summary>
        public event Action AllTabsClosed;

        /// <summary>
        /// Occures when save button is clicked.
        /// </summary>
        public event Action<List<QuestionModel>> Saving;

        /// <summary>
        /// Checks is there eny tab remaning.
        /// </summary>
        public bool IsTabsEmpty => _observableViewModel.Tabs.Count < 2;

        private readonly TestPageObservableViewModel _observableViewModel = new TestPageObservableViewModel();

        public TestPage()
        {
            InitializeComponent();

            DataContext = _observableViewModel;
        }

        /// <summary>
        /// Occures when selection changed on TabControl.
        /// If add-item is pressed create new tab-item otherwise do nothing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControlDynamic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem item = TabControlDynamic.SelectedItem as TabItem;
            if (item == null)
            {
                AllTabsClosed?.Invoke();
                return;
            }
            
            if (_observableViewModel.IsAddTabItem(item))
                _observableViewModel.AddTabItem(TabControlDynamic.FindResource("TabHeader") as DataTemplate);
        }

        /// <summary>
        /// Occures when delete button in a tab header is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            string tabName = (sender as Button).CommandParameter.ToString();
            TabItem item = TabControlDynamic.Items.Cast<TabItem>().SingleOrDefault(i => i.Name.Equals(tabName));

            if (item != null)
            {
                _observableViewModel.SelectPreLastElement(item);
                _observableViewModel.Tabs.Remove(item);
            }
        }

        /// <summary>
        /// Occures when tabcontrol is loaded.
        /// Create first tab and "add-tab"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControlDynamic_OnLoaded(object sender, RoutedEventArgs e)
        {
            _observableViewModel.InitTabControl(TabControlDynamic.FindResource("TabHeader") as DataTemplate);
        }

        /// <summary>
        /// Saving event will be invoked with a List of QuestinModel.
        /// All subscribed methods on Saving event will have got a data after invoking the Save method. 
        /// </summary>
        public void Save()
        {
            List<QuestionModel> questionsModel = new List<QuestionModel>();

            foreach (TabItem tabItem in _observableViewModel.Tabs)
            {
                Frame frame = tabItem.Content as Frame;
                if (frame != null)
                {
                    QuestionPage questionPage = frame.Content as QuestionPage;
                    questionsModel.Add(new QuestionModel(
                        _observableViewModel.SubjectName,
                        questionPage.Image,
                        questionPage.QuestionText,
                        questionPage.OptionsModel));
                }
            }

            Saving?.Invoke(questionsModel);
        }

        /// <summary>
        /// Remove all tabs with test pages.
        /// </summary>
        public void ClearTabs()
        {
            _observableViewModel.Tabs.Clear();
        }
    }
}
