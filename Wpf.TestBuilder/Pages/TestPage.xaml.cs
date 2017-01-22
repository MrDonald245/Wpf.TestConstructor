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
        /// Save a test.
        /// </summary>
        public event Action<TestModel> Saving;

        private Dictionary<int, QuestionModel> _questionsDictionary = new Dictionary<int, QuestionModel>();

        /// <summary>
        /// Checks is there eny tab remaning.
        /// </summary>
        public bool IsTabsEmpty => _observableViewModel.Tabs.Count < 2;

        public bool IsInEditMode
        {
            get { return _observableViewModel.IsInEditMode; }
            set { _observableViewModel.IsInEditMode = value; }
        }

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
        /// Init test page with a test model.
        /// </summary>
        /// <param name="testModel"></param>
        public void Edit(TestModel testModel)
        {
            IsInEditMode = true;
            _observableViewModel.SubjectName = testModel.Name;

            for (int i = 0; i < testModel.Questions.Count; ++i)
            {
                if (i == 0)
                {
                    _questionsDictionary.Add(i, testModel.Questions[i]);
                    WriteChangesToQuestionPage(i);
                    continue;
                }


                _observableViewModel.QuestionContainerContentRenderd += ObservableViewModelOnQuestionContainerContentRenderd;
                _observableViewModel.AddTabItem(TabControlDynamic.FindResource("TabHeader") as DataTemplate);
                _questionsDictionary.Add(i, testModel.Questions[i]);
            }
        }

        /// <summary>
        /// Handler for an event when questin page in a tab item is loaded.
        /// </summary>
        /// <param name="index"></param>
        private void ObservableViewModelOnQuestionContainerContentRenderd(int index)
        {
            WriteChangesToQuestionPage(index);
        }

        /// <summary>
        /// Save changes in UI
        /// </summary>
        /// <param name="tabIndex">Tab index.</param>
        private void WriteChangesToQuestionPage(int tabIndex)
        {
            if (_questionsDictionary.ContainsKey(tabIndex))
            {
                if (_questionsDictionary[tabIndex].Image != null)
                {
                    ((QuestionPage)((Frame)_observableViewModel.Tabs[tabIndex].Content).Content).Image = _questionsDictionary[tabIndex].Image;
                    ((QuestionPage)((Frame)_observableViewModel.Tabs[tabIndex].Content).Content).IsImageSelected = true;
                }

                if (_questionsDictionary[tabIndex].QuestionText != null)
                    ((QuestionPage)((Frame)_observableViewModel.Tabs[tabIndex].Content).Content).QuestionText = _questionsDictionary[tabIndex].QuestionText;

                if (_questionsDictionary[tabIndex].Options.Count > 0)
                {
                    ((QuestionPage)((Frame)_observableViewModel.Tabs[tabIndex].Content).Content).OptionsModel = _questionsDictionary[tabIndex].Options;
                    ((QuestionPage)((Frame)_observableViewModel.Tabs[tabIndex].Content).Content).IsOptionsListBoxEnabled = true;
                }
            }
        }


        /// <summary>
        /// Saving event will be invoked with a List of QuestinModel.
        /// All subscribed methods on Saving event will have got a data after invoking the Save method. 
        /// </summary>
        public void Save()
        {
            TestModel test = new TestModel { Name = _observableViewModel.SubjectName };

            foreach (TabItem tabItem in _observableViewModel.Tabs)
            {
                Frame frame = tabItem.Content as Frame;
                if (frame != null)
                {
                    QuestionPage questionPage = frame.Content as QuestionPage;

                    QuestionModel question = new QuestionModel(
                        questionPage.Image,
                        questionPage.QuestionText,
                        questionPage.OptionsModel);

                    test.Questions.Add(question);
                }
            }

            Saving?.Invoke(test);
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
