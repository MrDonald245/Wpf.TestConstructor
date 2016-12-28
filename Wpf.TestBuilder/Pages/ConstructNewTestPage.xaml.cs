using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Wpf.TestBuilder.Pages
{
    /// <summary>
    /// Interaction logic for ConstructNewTestPage.xaml
    /// </summary>
    public partial class ConstructNewTestPage : Page
    {
        private readonly ConstructNewTestPageModelView _viewModel = new ConstructNewTestPageModelView();

        public ConstructNewTestPage()
        {
            InitializeComponent();

            DataContext = _viewModel;
        }

        /// <summary>
        /// Occures when user clicks on "add-image" depiction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RectangleAddImage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            // try to open an image
            try
            {
                ImageUploader.Source = new BitmapImage(new Uri(_viewModel.OpenImage(), UriKind.Absolute));
                _viewModel.IsImageSelected = true;
            }
            catch (Exception)
            {
                // Ignore any exception...
            }

        }

        /// <summary>
        /// Occures when "Remove-Image" rectangle was clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RectangleRemoveImage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _viewModel.IsImageSelected = false;
        }
    }
}
