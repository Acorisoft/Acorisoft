using Acorisoft.FantasyStudio.Core;
using Acorisoft.FantasyStudio.Documents.Characters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace FantasyStudio.Frameworks.Documents.Host
{
    public class AppViewModel : Bindable
    {
        public AppViewModel()
        {
            Generations = new ObservableCollection<DefinitionGeneration>
            {
                new DefinitionGeneration
                {
                    Name = "基本设定模组",
                    Type = typeof(BasicCharacterDefinition)
                },
                new DefinitionGeneration
                {
                    Name = "年龄增强模组",
                    Type = typeof(CharacterAdvancedAgeDefinition)
                },new DefinitionGeneration
                {
                    Name = "性别增强模组",
                    Type = typeof(CharacterAdvancedGenderDefinition)
                },new DefinitionGeneration
                {
                    Name = "名字增强模组",
                    Type = typeof(CharacterAdvancedNameDefinition)
                },new DefinitionGeneration
                {
                    Name = "星座模组",
                    Type = typeof(CharacterZodiacDefinition)
                },
            };

            Definitions = new ObservableCollection<ICharacterDefinition>();
        }

        public ObservableCollection<DefinitionGeneration> Generations { get; set; }
        public ObservableCollection<ICharacterDefinition> Definitions { get; set; }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public AppViewModel ViewModel => DataContext as AppViewModel;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new InsertDefinitionWindow();
            if(dialog.ShowDialog() == true && 
                dialog.DialogResult == true && 
                dialog.Result != null &&
                dialog.Result.Type != null)
            {
                var generation = dialog.Result;
                var instance = (ICharacterDefinition)Activator.CreateInstance(dialog.Result.Type);
                ViewModel.Definitions.Add(instance);
            }
        }
    }
}
