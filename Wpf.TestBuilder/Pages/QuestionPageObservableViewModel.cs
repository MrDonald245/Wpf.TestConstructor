using System;
using Microsoft.Win32;
using Wpf.TestBuilder.Dialogs;
using Wpf.TestBuilder.Models;

namespace Wpf.TestBuilder.Pages
{
    public class QuestionPageObservableViewModel : ObservableViewModel
    {
        private bool _isImageSelected;
        public bool IsImageSelected
        {
            get { return _isImageSelected; }
            set
            {
                _isImageSelected = value;
                OnPropertyChanged();
            }
        }

        private bool _isRectangleAddTextVisible;
        public bool IsRectangleAddTextVisible
        {
            get { return _isRectangleAddTextVisible; }
            set
            {
                _isRectangleAddTextVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingBarEnabled;
        public bool IsLoadingBarEnabled
        {
            get { return _isLoadingBarEnabled; }
            set
            {
                _isLoadingBarEnabled = value;
                OnPropertyChanged();
            }
        }

        private string _labelTextValue;
        public string LabelTextValue
        {
            get { return _labelTextValue; }
            set
            {
                _labelTextValue = value;
                OnPropertyChanged();
            }
        }

        private bool _isLabelTextInputEnabled;
        public bool IsLabelTextInputEnabled
        {
            get { return _isLabelTextInputEnabled; }
            set
            {
                _isLabelTextInputEnabled = value; 
                OnPropertyChanged();
            }
        }

        private bool _isInputOptionEnabled;
        public bool IsInputOptionEnabled
        {
            get { return _isInputOptionEnabled; }
            set
            {
                _isInputOptionEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _isOptionsListBoxEnabled;
        public bool IsOptionsListBoxEnabled
        {
            get { return _isOptionsListBoxEnabled; }
            set
            {
                _isOptionsListBoxEnabled = value;
                OnPropertyChanged();
            }
        }

        private OptionsModel _optionsModel;
        public OptionsModel OptionsModel
        {
            get { return _optionsModel; }
            set
            {
                _optionsModel = value;
                OnPropertyChanged();
            }
        }

        private bool IsRightAnswer => OptionsModel.Count == 0;

        public QuestionPageObservableViewModel()
        {
            OptionsModel = new OptionsModel();
        }

        /// <summary>
        /// Open an image.
        /// </summary>
        /// <exception cref="Exception">if nothing is selected</exception>
        /// <returns>full path to image</returns>
        public string OpenImage()
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Title = "Select a picture",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                         "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                         "Portable Network Graphic (*.png)|*.png"
            };

            if (fileDialog.ShowDialog() == true)
                return fileDialog.FileName;

            throw new Exception("None of files was selected.");
        }

        /// <summary>
        /// Change an optino name by index in OptionsModel.
        /// </summary>
        /// <param name="index"></param>
        public void ChangeOptionNameByIndex(int index, string name)
        {
            _optionsModel[index].Name = name;
        }

        /// <summary>
        /// Add a new option.
        /// </summary>
        /// <param name="name"></param>
        public void AddOption(string name)
        {
            _optionsModel.Add(new OptionModel(IsRightAnswer, name));
        }

        /// <summary>
        /// Open input text dialog.
        /// </summary>
        /// <returns>string option name or empty string otherwise.</returns>
        public string OpenInputTextDialog()
        {
            // Instantiate the dialog box
            InputTextDialog dlg = new InputTextDialog();

            // Open the dialog box modally 
            if (dlg.ShowDialog() == true)
                return dlg.OptionNameResult;

            return "";
        }
    }
}
