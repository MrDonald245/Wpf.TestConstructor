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

        public ObservableCollection<TabItem> Tabs { get; set; } = new ObservableCollection<TabItem>();

        private TabItem _addTabItem = new TabItem { Header = "+", IsSelected = false };

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
            return new TabItem
            {
                Header = CreateTabHeaderName(),
                Name = CreateTabHeaderName(),
                HeaderTemplate = headerTemplate,
                Content = new Frame { Source = new Uri("Pages/QuestionPage.xaml", UriKind.Relative) }
            };
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
