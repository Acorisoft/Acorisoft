using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Acorisoft.Windows
{
    public static class WindowsCommands
    {
        static WindowsCommands()
        {
            Ok = Create("Acorisoft.Ok");
            Cancel = Create("Acorisoft.Cancel");
            Last = Create("Acorisoft.Last");
            Next = Create("Acorisoft.Next");
        }

        static RoutedUICommand Create(string name)
        {
            return new RoutedUICommand(name, name, typeof(WindowsCommands));
        }

        public static RoutedUICommand Ok { get; }
        public static RoutedUICommand Cancel { get; }
        public static RoutedUICommand Next { get; }
        public static RoutedUICommand Last { get; }
    }
}
