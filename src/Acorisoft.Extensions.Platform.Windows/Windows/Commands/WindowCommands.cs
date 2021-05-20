using System.Windows.Input;

namespace Acorisoft.Extensions.Windows.Commands
{
    public class WindowCommands
    {
        public static RoutedCommand Next => new RoutedCommand("Dialog.Next", typeof(WindowCommands));
        public static RoutedCommand Last => new RoutedCommand("Dialog.Last", typeof(WindowCommands));
        public static RoutedCommand Completed => new RoutedCommand("Dialog.Completed", typeof(WindowCommands));
        public static RoutedCommand Ignore => new RoutedCommand("Dialog.Ignore", typeof(WindowCommands));
        public static RoutedCommand Cancel => new RoutedCommand("Dialog.Cancel", typeof(WindowCommands));
        public static RoutedCommand Skip => new RoutedCommand("Dialog.Skip", typeof(WindowCommands));
    }   
}