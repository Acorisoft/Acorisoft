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

        public static RoutedUICommand InsertWizard { get; } = Create("Acorisoft.Morisa.Inspiration.Wizard");
        public static RoutedUICommand PickInsertion { get; } = Create("Acorisoft.Morisa.Inspiration.PickInsertion");
    }
}
