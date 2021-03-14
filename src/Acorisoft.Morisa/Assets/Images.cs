using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Acorisoft.Morisa.Assets
{
    public static class Images
    {
        static Images()
        {
            DefaultCompositionSetCover = new BitmapImage(new Uri("pack://application:,,,/Acorisoft.Morisa;component/Assets/Default_CS_Cover.jpg"))
            {
                CacheOption = BitmapCacheOption.OnDemand
            };
        }

        public static readonly BitmapImage DefaultCompositionSetCover;
    }
}
