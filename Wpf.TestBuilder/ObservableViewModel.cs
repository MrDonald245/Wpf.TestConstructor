using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Wpf.TestBuilder
{
    [Serializable]
    public abstract class ObservableViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notify property changing.
        /// </summary>
        /// <param name="propertyName"></param>C:\Users\eboch\Documents\Visual Studio 2015\Projects\Wpf.TestConstructor\Wpf.TestBuilder\ObservableViewModel.cs
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
