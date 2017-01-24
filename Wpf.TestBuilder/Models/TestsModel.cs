using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Wpf.TestBuilder.Models
{
    public class TestsModel : ObservableCollection<TestModel>
    {
        /// <summary>
        /// A name of a file which be saved with questions.
        /// </summary>
        private const string SerializationFileName = "tests.bin";

        /// <summary>
        /// Save question models to binary file.
        /// The data will be transform and will be contained in a test.bin file 
        /// </summary>
        public void Save()
        {
            using (FileStream fs = new FileStream(SerializationFileName, FileMode.OpenOrCreate))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fs, Items);
            }
        }

        /// <summary>
        /// Try to deserialize tests.bin. If it is not found, return null.
        /// </summary>
        /// <returns></returns>
        public void Open()
        {
            try
            {
                List<TestModel> result;
                using (FileStream fs = new FileStream(SerializationFileName, FileMode.Open))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    result = binaryFormatter.Deserialize(fs) as List<TestModel>;
                }

                foreach (TestModel questionModel in result)
                    Add(questionModel);
            }
            catch (FileNotFoundException)
            {
                // Do not write changes from tests.bin when an exception occures
            }
        }
    }
}