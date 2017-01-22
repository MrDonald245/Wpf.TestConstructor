﻿using System;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf.TestBuilder.Models;

namespace Wpf.TestBuilder.Pages
{
    /// <summary>
    /// Interaction logic for SavedTestsPage.xaml
    /// </summary>
    public partial class SavedTestsPage : Page
    {
        public event Action<TestModel> SavedTestsSelected; 

        private readonly SavedTestsPageObservableViewModel _viewModel = new SavedTestsPageObservableViewModel();

        public TestsModel Tests
        {
            get { return _viewModel.TestsModel; }
            set { _viewModel.TestsModel = value; }
        }

        public SavedTestsPage()
        {
            InitializeComponent();

            DataContext = _viewModel;
        }

        /// <summary>
        /// Occures when double click is clicked on a saved test item.
        /// Invoke SavedTestsSelected event and pass the selected, saved test.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewSavedTests_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SavedTestsSelected?.Invoke(((ListView)sender).SelectedItem as TestModel);
        }
    }
}