using System.Windows;

namespace Wpf.TestBuilder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            System.Threading.Thread.CurrentThread.CurrentUICulture =
            new System.Globalization.CultureInfo("ru-RU");
        }
    }
}