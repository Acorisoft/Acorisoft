using System;
using System.Windows.Input;

namespace Acorisoft.Morisa.Windows
{
    public static class WindowCommands
    {
        static RoutedUICommand Create(string name)
        {
            return new RoutedUICommand(name, name, typeof(WindowCommands));
        }


        public static RoutedUICommand Goto { get; } = Create("Acorisoft.Morisa.Shell.Goto");
        public static RoutedUICommand Goback { get; } = Create("Acorisoft.Morisa.Shell.GoBack");
    }
}
