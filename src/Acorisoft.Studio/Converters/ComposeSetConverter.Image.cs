using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;
using Acorisoft.Extensions.Platforms.Windows.Converters;
using Acorisoft.Extensions.Platforms.Windows.Services;
using Acorisoft.Studio.Documents.Resources;

namespace Acorisoft.Studio.Converters
{
    public class ComposeSetImageConverter : OneWayConverter<ImageResource, BitmapSource>
    {
        protected override BitmapSource ConvertTo(ImageResource source, object parameter, CultureInfo cultureInfo)
        {
            using (var rawFileStream = ServiceLocator.ComposeSetSystem.Open(source))
            {
                using (var ms = new MemoryStream((int)rawFileStream.Length))
                {
                    rawFileStream.CopyTo(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    
                    //
                    // 创建图片
                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = ms;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze();
                    return bitmapImage;
                }
            }
        }
    }
}