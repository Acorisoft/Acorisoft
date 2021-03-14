using System;
using System.Windows.Input;

namespace Acorisoft.Morisa.Commands
{
    public static class ApplicationCommands
    {
        static RoutedUICommand Create(string name)
        {
            return new RoutedUICommand(name, name, typeof(ApplicationCommands));
        }


        public static RoutedUICommand Goto { get; } = Create("Acorisoft.Morisa.Shell.Goto");
        public static RoutedUICommand Goback { get; } = Create("Acorisoft.Morisa.Shell.GoBack");
    }
}
