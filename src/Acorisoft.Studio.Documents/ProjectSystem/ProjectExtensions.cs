using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    public static class ProjectExtensions
    {
        public static string GetImagesDirectory(this ICompositionProject information)
        {
            return Path.Combine(information.Path, "Images");
        }

        public static string GetVideosDirectory(this ICompositionProject information)
        {
            return Path.Combine(information.Path, "Videos");
        }

        public static string GetBrushesDirectory(this ICompositionProject information)
        {
            return Path.Combine(information.Path, "Brushes");
        }
    }
}
