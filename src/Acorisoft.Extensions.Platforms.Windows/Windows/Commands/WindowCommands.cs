using System;
using System.Windows.Input;

namespace Acorisoft.Extensions.Platforms.Windows.Commands
{
    public static class WindowCommands
    {
        public static RoutedCommand Next { get; } = new RoutedUICommand("Next", "Next", typeof(WindowCommands));
        public static RoutedCommand Last { get; } = new RoutedUICommand("Last", "Last", typeof(WindowCommands));
        public static RoutedCommand Completed { get; } = new RoutedUICommand("Completed", "Completed", typeof(WindowCommands));
        public static RoutedCommand Ignore { get; } = new RoutedUICommand("Ignore", "Ignore", typeof(WindowCommands));
        public static RoutedCommand Cancel { get; } = new RoutedUICommand("Cancel", "Cancel", typeof(WindowCommands));
        public static RoutedCommand Skip { get; } = new RoutedUICommand("Skip", "Skip", typeof(WindowCommands));
    }   
}