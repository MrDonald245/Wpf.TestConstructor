using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Media.Imaging;

namespace Wpf.TestBuilder.Models
{
    [Serializable]
    public class QuestionModel : ISerializable
    {
        public string Name { get; set; }
        public BitmapImage Image { get; set; }
        /// <summary>
        /// Get image in bytes.
        /// </summary>
        public byte[] ImageInBytes => BitmapImageToByteArrey(Image);
        public string QuestionText { get; set; }
        public OptionsModel Options { get; set; }

        public OptionModel[] OptionsInArrey => Options.ToArray();

        public QuestionModel()
        {
        }

        public QuestionModel(string name, BitmapImage image, string questionText, OptionsModel options)
        {

            Name = name;
            Image = image;
            QuestionText = questionText;
            Options = options;
        }

        public byte[] BitmapImageToByteArrey(BitmapImage image)
        {
            if (image != null)
            {
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));

                using (var ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    return ms.ToArray();
                }
            }

            return default(byte[]);
        }

        /// <summary>
        /// Instructions what to serialize.
        /// BitmapImage is skiped becouse of its inability to serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("ImageInBytes", ImageInBytes);
            info.AddValue("QuestionText", QuestionText);
            info.AddValue("OptionsInArrey", OptionsInArrey);
        }
    }
}
