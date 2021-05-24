using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Acorisoft.Extensions.Platforms.Windows;

namespace WpfApp1
{
    public static class WindowCommands
    {
        public static RoutedCommand Next { get; } = new RoutedUICommand("Next", "Next", typeof(SpaWindow));
        public static RoutedCommand Last { get; } = new RoutedUICommand("Last", "Last", typeof(SpaWindow));
        public static RoutedCommand Completed { get; } = new RoutedUICommand("Completed", "Completed", typeof(SpaWindow));
        public static RoutedCommand Ignore { get; } = new RoutedUICommand("Ignore", "Ignore", typeof(SpaWindow));
        public static RoutedCommand Cancel { get; } = new RoutedUICommand("Cancel", "Cancel", typeof(SpaWindow));
        public static RoutedCommand Skip { get; } = new RoutedUICommand("Skip", "Skip", typeof(SpaWindow));
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : SpaWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}