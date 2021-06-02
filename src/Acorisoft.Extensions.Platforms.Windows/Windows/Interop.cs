using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace Acorisoft.Extensions.Platforms.Windows
{
    public class Interop
    {
        /// <summary>
        /// 将一个内存中的位图转化为一个Stream使得它允许保存。
        /// </summary>
        /// <param name="bitmapSource">指定要转化的位图。</param>
        /// <returns>返回创建的流。</returns>
        public static MemoryStream CopyBitmapToStream(BitmapSource bitmapSource)
        {
            if (bitmapSource == null)
            {
                throw new ArgumentNullException(nameof(bitmapSource));
            }
            
            return CopyBitmapToStream(bitmapSource, new JpegBitmapEncoder());
        }
        
        /// <summary>
        /// 将一个内存中的位图转化为一个Stream使得它允许保存。
        /// </summary>
        /// <param name="bitmapSource">指定要转化的位图。</param>
        /// <param name="encoder">指定的编码器</param>
        /// <returns>返回创建的流。</returns>
        /// <exception cref="ArgumentNullException">参数为空。</exception>
        public static MemoryStream CopyBitmapToStream(BitmapSource bitmapSource, BitmapEncoder encoder)
        {
            if (bitmapSource == null)
            {
                throw new ArgumentNullException(nameof(bitmapSource));
            }

            if (encoder == null)
            {
                throw new ArgumentNullException(nameof(encoder));
            }
            var ms = new MemoryStream();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            encoder.Save(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
    }
}