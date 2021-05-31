using System.Windows.Input;
using Acorisoft.Extensions.Platforms.Windows.Commands;

namespace Acorisoft.Studio.Commands
{
    public static class StudioCommands
    {
        public static RoutedCommand NewProject { get; } = new RoutedUICommand("NewProject", "NewProject", typeof(StudioCommands));
        public static RoutedCommand OpenProject { get; } = new RoutedUICommand("OpenProject", "OpenProject", typeof(StudioCommands));
        public static RoutedCommand NewInspiration { get; } = new RoutedUICommand("NewInspiration", "NewInspiration", typeof(StudioCommands));
        public static RoutedCommand NewStickyNote { get; } = new RoutedUICommand("NewStickyNote", "NewStickyNote", typeof(StudioCommands));
        public static RoutedCommand Save { get; } = new RoutedUICommand("Save", "Save", typeof(StudioCommands));
        public static RoutedCommand Sync { get; } = new RoutedUICommand("Sync", "Sync", typeof(StudioCommands));
    }
}