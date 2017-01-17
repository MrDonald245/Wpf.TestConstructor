using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Wpf.TestBuilder.Models;
using Wpf.TestBuilder.Pages;

namespace Wpf.TestBuilder
{
    public class MainWindowViewModel : ViewModelBase
    {
        private const string SerializationFileName = "tests.bin";

        public TestPage TestPage = new TestPage();

        /// <summary>
        /// Save question models to binary file.
        /// </summary>
        /// <param name="questionModels"></param>
        public void SerializeQuestionModels(List<QuestionModel> questionModels)
        {
            using (FileStream fs = new FileStream(SerializationFileName, FileMode.OpenOrCreate))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fs, questionModels);
            }
        }

        /// <summary>
        /// Try to deserialize tests.bin. If it is not found, return null.
        /// </summary>
        /// <returns></returns>
        public List<QuestionModel> DeserializeQuestionModel()
        {
            List<QuestionModel> result = new List<QuestionModel>();

            try
            {
                using (FileStream fs = new FileStream(SerializationFileName, FileMode.Open))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    result = binaryFormatter.Deserialize(fs) as List<QuestionModel>;
                }
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            
            return result;
        }
    }
}
