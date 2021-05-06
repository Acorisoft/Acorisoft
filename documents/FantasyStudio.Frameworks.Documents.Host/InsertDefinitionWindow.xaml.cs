using Acorisoft.FantasyStudio.Documents.Characters;
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
using System.Windows.Shapes;

namespace FantasyStudio.Frameworks.Documents.Host
{
    /// <summary>
    /// InsertDefinitionWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InsertDefinitionWindow : Window
    {
        public InsertDefinitionWindow()
        {
            InitializeComponent();
        }

        public DefinitionGeneration Result { get; private set; }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Result != null)
            {
                DialogResult = true;
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Result != null)
            {
                DialogResult = false;
                this.Close();
            }
        }

        private void ListBox_Selected(object sender, SelectionChangedEventArgs e)
        {
            var list = (ListBox)sender;
            Result = list.SelectedItem as DefinitionGeneration;
        }
    }
}
