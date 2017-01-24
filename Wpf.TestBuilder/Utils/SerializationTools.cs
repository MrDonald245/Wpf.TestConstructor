using System;
using System.IO;
using System.Windows.Media.Imaging;
using Wpf.TestBuilder.Models;

namespace Wpf.TestBuilder.Utils
{
    public static class SerializationTools
    {
        public static byte[] BitmapImageToByteArrey(BitmapImage image)
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

        public static BitmapImage ByteArreyToBitmapImage(byte[] byteImage)
        {
            if (byteImage == null)
                return null;

            try
            {
                MemoryStream strmImg = new MemoryStream(byteImage);
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                result.StreamSource = strmImg;
                result.DecodePixelWidth = 200;
                result.EndInit();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static OptionsModel OptionsArreyToOptionsModel(OptionModel[] optionsArrey)
        {
            OptionsModel optionsModel = new OptionsModel();

            foreach (OptionModel optionModel in optionsArrey)
                optionsModel.Add(optionModel);

            return optionsModel;
        }
    }
}