using System;
using Microsoft.Win32;

namespace Wpf.TestBuilder.Pages
{
    public class ConstructNewTestPageModelView : ModelViewBase
    {

        private bool _isImageSelected;
        public bool IsImageSelected
        {
            get { return _isImageSelected; }
            set
            {
                _isImageSelected = value;
                OnPropertyChanged("IsImageSelected");
            }
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

            throw new Exception("None of files were selected.");
        }
    }
}
