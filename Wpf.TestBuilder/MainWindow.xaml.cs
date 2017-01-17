using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Wpf.TestBuilder.Models;
using Wpf.TestBuilder.Pages;

namespace Wpf.TestBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When the main window is loaded show all tests in a listview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// New command can be executed all the time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /// <summary>
        /// Creates new test constructor page in a frame.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FramePageContent.Source = new Uri("Pages/TestPage.xaml", UriKind.Relative);
            FramePageContent.ContentRendered += FramePageContentOnContentRendered;         
        }

        /// <summary>
        /// Occures when the frame has rendered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void FramePageContentOnContentRendered(object sender, EventArgs eventArgs)
        {
            _viewModel.TestPage = FramePageContent.Content as TestPage;

            if (_viewModel.TestPage != null)
            {
                _viewModel.TestPage.AllTabsClosed += TestPageOnAllTabsClosed;
                _viewModel.TestPage.Saving += TestPageOnSaving;
            }       
        }

        /// <summary>
        /// Occures when save button was clicked in a test page.
        /// Recieves question model to be serialized. 
        /// </summary>
        /// <param name="questionModels"></param>
        private void TestPageOnSaving(List<QuestionModel> questionModels)
        {
           _viewModel.SerializeQuestionModels(questionModels);
           _viewModel.TestPage.ClearTabs();
        }

        /// <summary>
        /// Occures when all tabs are closed.
        /// Close a test page.
        /// </summary>
        private void TestPageOnAllTabsClosed()
        {
            _viewModel.TestPage.ClearTabs();
            FramePageContent.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            FramePageContent.Source = null;
        }

        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
            throw new NotImplementedException();
        }

        /// <summary>
        /// Close command can be executed all the time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /// <summary>
        /// Close command closes the application. Does nothing with 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void HelpCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
        }

        private void HelpCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Save command can be executed only when a test page is opened.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_viewModel.TestPage != null)
                e.CanExecute = !_viewModel.TestPage.IsTabsEmpty;
            e.CanExecute = false;
        }

        /// <summary>
        /// Occures when accept button is clicked.
        /// Notify parent window when save button is clicked.
        /// If a question page in a tabitem is empty. Save it anyway in a quaestion model.
        /// </summary>
        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.TestPage.Save();
        }

        /// <summary>
        /// Cancel command is avalible always.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /// <summary>
        /// Occures when either canclel button or Esc is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.TestPage.ClearTabs();
        }
    }
}
