using System;
using System.Runtime.Serialization;
using System.Windows.Media.Imaging;
using Wpf.TestBuilder.Utils;

namespace Wpf.TestBuilder.Models
{
    [Serializable]
    public class QuestionModel : ISerializable
    {
        public BitmapImage Image { get; set; }

        public string QuestionText { get; set; }
        public OptionsModel Options { get; set; }

        public QuestionModel()
        {
        }

        /// <summary>
        /// Serializable constructor for deserialization
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public QuestionModel(SerializationInfo info, StreamingContext context)
        {
            Image = SerializationTools.ByteArreyToBitmapImage(info.GetValue("Image", typeof(byte[])) as byte[]);
            QuestionText = info.GetString("QuestionText");
            Options = (OptionsModel) info.GetValue("Options", typeof(OptionsModel));
        }

        public QuestionModel(BitmapImage image, string questionText, OptionsModel options)
        {
            Image = image;
            QuestionText = questionText;
            Options = options;
        }


        /// <summary>
        /// Instructions what to serialize.
        /// BitmapImage is skiped becouse of its inability to serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Image", SerializationTools.BitmapImageToByteArrey(Image));
            info.AddValue("QuestionText", QuestionText);
            info.AddValue("Options", Options);
        }
    }
}