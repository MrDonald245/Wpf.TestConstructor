using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Wpf.TestBuilder.Pages
{
    public class TestPageObservableViewModel : ObservableViewModel
    {
        private string _subjectName;
        public string SubjectName
        {
            get { return _subjectName; }
            set
            {
                _subjectName = value;
                OnPropertyChanged();
            }
        }

        private bool _isInEditMode;
        public bool IsInEditMode
        {
            get { return _isInEditMode; }
            set
            {
                _isInEditMode = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TabItem> Tabs { get; set; } = new ObservableCollection<TabItem>();

        public event Action<int> QuestionContainerContentRenderd;

        private TabItem _addTabItem = new TabItem { Header = "+", IsSelected = false };

        private int _editCurrentTabCount = 1;

        /// <summary>
        /// Make first tab and "add-button" after.
        /// </summary>
        public void InitTabControl(DataTemplate headerTemplate)
        {
            if (Tabs.Count == 0)
            {
                Tabs.Add(_addTabItem);
                AddTabItem(headerTemplate);
            }
        }

        /// <summary>
        /// Create new tab-item with an answer-page.
        /// </summary>
        /// <returns></returns>
        private TabItem CreateNewTabItem(DataTemplate headerTemplate)
        {
            Frame questionContainer = new Frame { Source = new Uri("Pages/QuestionPage.xaml", UriKind.Relative) };
            if (IsInEditMode)
                questionContainer.ContentRendered += QuestionContainerOnContentRendered;

            return new TabItem
            {
                Header = CreateTabHeaderName(),
                Name = CreateTabHeaderName(),
                HeaderTemplate = headerTemplate,
                Content = questionContainer
            };
        }
        
        /// <summary>
        /// Occures when question container content is ready.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuestionContainerOnContentRendered(object sender, EventArgs e)
        {
            QuestionContainerContentRenderd?.Invoke(_editCurrentTabCount);
            _editCurrentTabCount++;
        }

        /// <summary>
        /// Create name for header.
        /// Each tab-item add recieves iterated name as "Answer1, ... AnswerN"
        /// </summary>
        /// <returns></returns>
        private string CreateTabHeaderName()
        {
            int lastAddedTabIndex = Tabs.Max(Selector);
            if (lastAddedTabIndex == 0)
                return $"Answer{Tabs.Count}";

            return $"Answer{lastAddedTabIndex + 1}";
        }

        private int Selector(TabItem tabItem)
        {
            if (tabItem.Name == string.Empty)
                return 0;

            Match matchNumber = Regex.Match(tabItem.Name, @"Answer([0-9]+)");
            return Convert.ToInt32(matchNumber.Groups[1].Value);

        }


        /// <summary>
        /// Add a new tab-item with answer-page inside.
        /// </summary>
        /// <param name="headerTemplate"></param>
        public void AddTabItem(DataTemplate headerTemplate)
        {
            TabItem newTabItem = CreateNewTabItem(headerTemplate);
            newTabItem.IsSelected = true;
            Tabs.Insert(Tabs.Count - 1, newTabItem);
        }

        /// <summary>
        /// Checks if tabItem is add-item.
        /// </summary>
        /// <param name="tabItem"></param>
        /// <returns></returns>
        public bool IsAddTabItem(TabItem tabItem)
        {
            return tabItem.Equals(_addTabItem);
        }

        /// <summary>
        /// Select pre-last element.
        /// </summary>
        public void SelectPreLastElement(TabItem deletedTab)
        {
            if (Tabs.Count <= 2)
                Tabs[0].IsSelected = true;
            else
            {
                Tabs
                    .Select(item => item)
                    .Where(item => item != deletedTab)
                    .Last(item => item != _addTabItem)
                    .IsSelected = true;

            }
        }

    }
}
