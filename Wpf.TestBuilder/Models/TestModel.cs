using System;
using System.Runtime.Serialization;

namespace Wpf.TestBuilder.Models
{
    [Serializable]
    public class TestModel : ISerializable
    {
        public string Name { get; set; }
        public QuestionsModel Questions { get; set; } = new QuestionsModel();

        public TestModel()
        {
        }

        /// <summary>
        /// Serializable constructor.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public TestModel(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString("Name");
            Questions = info.GetValue("Questions", typeof(QuestionsModel)) as QuestionsModel;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Questions", Questions);
        }
    }
}