using System.Windows.Input;

namespace Acorisoft.Morisa.Dialogs
{
    public static class DialogCommands
    {
        static RoutedUICommand Create(string name)
        {
            return new RoutedUICommand(name, name, typeof(DialogCommands));
        }


        public static RoutedUICommand Ok { get; } = Create("Acorisoft.Morisa.Shell.Dialogs.Add");
        public static RoutedUICommand NextStep { get; } = Create("Acorisoft.Morisa.Shell.Dialogs.NextStep");
        public static RoutedUICommand LastStep { get; } = Create("Acorisoft.Morisa.Shell.Dialogs.LastStep");
        public static RoutedUICommand Cancel { get; } = Create("Acorisoft.Morisa.Shell.Dialogs.Cancel");
    }
}
