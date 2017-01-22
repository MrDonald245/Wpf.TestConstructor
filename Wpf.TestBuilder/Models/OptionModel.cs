using System;
using System.CodeDom;
using System.Runtime.Serialization;

namespace Wpf.TestBuilder.Models
{
    [Serializable]
    public class OptionModel : ObservableViewModel, ISerializable
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

        public OptionModel(SerializationInfo info, StreamingContext context)
        {
            IsRightAnswer = info.GetBoolean("IsRightAnswer");
            Name = info.GetString("Name");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("IsRightAnswer", IsRightAnswer);
            info.AddValue("Name", Name);
        }
    }
}
