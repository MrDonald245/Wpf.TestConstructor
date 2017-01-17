using System;

namespace Wpf.TestBuilder.Models
{
    [Serializable]
    public class OptionModel : ObservableViewModel
    {
        private bool _isRightAnswer;
        public bool IsRightAnswer
        {
            get { return _isRightAnswer; }
            set
            {
                _isRightAnswer = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public OptionModel(bool isRightAnswer, string name)
        {
            IsRightAnswer = isRightAnswer;
            Name = name;
        }
    }
}
