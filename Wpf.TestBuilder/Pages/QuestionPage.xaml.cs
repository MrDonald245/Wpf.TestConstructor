using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Wpf.TestBuilder.Models;

namespace Wpf.TestBuilder.Pages
{
    /// <summary>
    /// Interaction logic for QuestionPage.xaml
    /// </summary>
    public partial class QuestionPage : Page
    {
        private readonly QuestionPageObservableViewModel _observableViewModel = new QuestionPageObservableViewModel();
        /// <summary>
        /// Contains variants of answers.
        /// </summary>
        public OptionsModel OptionsModel => _observableViewModel.OptionsModel;
        /// <summary>
        /// Question image. If is not selected returns null;
        /// </summary>
        public BitmapImage Image => ImageUploader.Source as BitmapImage;
        /// <summary>
        /// A text for a question.
        /// </summary>
        public string QuestionText => _observableViewModel.LabelTextValue;

        public QuestionPage()
        {
            InitializeComponent();

            DataContext = _observableViewModel;
        }

        /// <summary>
        /// Occures when user clicks on "add-image" depiction.
        /// Try to add the image to a question-page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RectangleAddImage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            // try to open an image
            try
            {
                _observableViewModel.IsLoadingBarEnabled = true;
                ImageUploader.Source = new BitmapImage(new Uri(_observableViewModel.OpenImage(), UriKind.Absolute));
                _observableViewModel.IsImageSelected = true;
            }
            catch (Exception)
            {
                // Ignore any exception...
            }
            finally
            {
                _observableViewModel.IsLoadingBarEnabled = false;
            }
        }

        /// <summary>
        /// Occures when "Remove-Image" rectangle was clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RectangleRemoveImage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _observableViewModel.IsImageSelected = false;
        }


        /// <summary>
        /// Occurese when an element image is under focus.
        /// Change mouse to the 'Hand' cursor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Element_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }


        /// <summary>
        /// Occures when an elemnt lost mouse focuse.
        /// Set cursor to default view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Element_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
        }

        /// <summary>
        /// Occures when a ractangle 'Add-Text' was clicked.
        /// Add a text to the label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RectangleLabelInserter_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _observableViewModel.IsRectangleAddTextVisible = false; // Hide 'Add-Text' image
            _observableViewModel.IsLabelTextInputEnabled = true; // Show input textbox.

            TextBoxInputLabel.Focus();
        }



        /// <summary>
        /// Occures when remove text-label was clicked.
        /// Remove text from the label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RectangleRemoveText_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _observableViewModel.LabelTextValue = null;
            _observableViewModel.IsLabelTextInputEnabled = false; // Hide input TextBox
        }

        /// <summary>
        /// Occures when add test-options was clicked.
        /// Add a new ListBox with options.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RectangleAddOptions_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _observableViewModel.IsOptionsListBoxEnabled = true;
        }

        /// <summary>
        /// Occures when add test-option was clicked.
        /// Add a new option to the ListBox with options.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RectangleAddOption_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            string optionName = _observableViewModel.OpenInputTextDialog();
            if (optionName != "")
                _observableViewModel.AddOption(optionName);
        }

        /// <summary>
        /// Occures when rectangle remove-option was clicked.
        /// Delete an option.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RectangleRemoveOption_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            OptionModel option = (OptionModel)ListViewOptions.SelectedItem;

            if (option != null)
                _observableViewModel.OptionsModel.Remove(option);
        }

        /// <summary>
        /// Occures when rectangle remove-options was clicked.
        /// Remove all options and hide ListViewOptions control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RectangleRemoveOptions_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _observableViewModel.OptionsModel = new OptionsModel();
            _observableViewModel.IsOptionsListBoxEnabled = false;
        }

        /// <summary>
        /// Occures when rectangle edit-option was clicked.
        /// Edit option.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RectangleEditOption_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            int selectedOptionIndex = ListViewOptions.SelectedIndex;

            if (selectedOptionIndex != -1)
            {
                string optionName = _observableViewModel.OpenInputTextDialog();
                if (optionName != "")
                    _observableViewModel.ChangeOptionNameByIndex(selectedOptionIndex, optionName);
            }
        }

        /// <summary>
        /// Occures when rectangle accept-text was clicked.
        /// Hide text input textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RectangleAcceptText_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _observableViewModel.IsLabelTextInputEnabled = false;
        }
    }
}