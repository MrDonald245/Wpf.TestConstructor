using System;

namespace Wpf.TestBuilder.Models
{
    [Serializable]
    public class TestModel : ObservableViewModel
    {
        public string Name { get; set; }
        public QuestionsModel Questions { get; set; } = new QuestionsModel();
    }
}
