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
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using FantasyStudio.MEF.Core;

namespace FantasyStudio.MEF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var catalog = new DirectoryCatalog(Environment.CurrentDirectory);
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
           
            foreach (var model in Submodules)
            {
                Master.Items.Add(model);
            }
        }
        
        [ImportMany(typeof(IModel))]
        public IEnumerable<IModel> Submodules { get; set; }

        private void Master_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Detail.Content = ((ListBox) sender).SelectedItem;
        }
    }
}