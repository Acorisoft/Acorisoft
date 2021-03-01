using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Acorisoft.Morisa.Dialogs
{
    public static class DialogCommands
    {
        static RoutedUICommand Create(string name)
        {
            return new RoutedUICommand(name, name, typeof(DialogCommands));
        }


        public static RoutedUICommand Ok { get; } = Create("Acorisoft.Morisa.Dialogs.Add");
        public static RoutedUICommand Cancel { get; } = Create("Acorisoft.Morisa.Dialogs.Add");
    }
}
