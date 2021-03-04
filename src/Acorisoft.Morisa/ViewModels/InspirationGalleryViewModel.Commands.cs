using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Acorisoft.Morisa.ViewModels
{
    public static class InspirationGalleryCommands
    {
        static RoutedUICommand Create(string name)
        {
            return new RoutedUICommand(name, name, typeof(InspirationGalleryCommands));
        }

        public static RoutedUICommand Insert { get; } = Create("App.InspirationGalleryView.Insert");
        public static RoutedUICommand Filter { get; } = Create("App.InspirationGalleryView.Filter");
    }
}
