using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Acorisoft.Frameworks.Controls;

namespace Acorisoft.FantasyStudio.Documents.Characters
{
    public class GenderItem
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
    }

    /// <summary>
    /// GenderLand.xaml 的交互逻辑
    /// </summary>
    public partial class GenderLand : UserControl
    {
        public GenderLand()
        {
            InitializeComponent();
            PART_Title.Header = Initialize();
            this.DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            switch (ViewModel.Gender)
            {
                case Gender.Female:
                    PART_Female.IsChecked = true;
                    break;
                case Gender.Male:
                    PART_Male.IsChecked = true;
                    break;
                case Gender.Transfemale:
                    PART_Transfemale.IsChecked = true;
                    break;
                case Gender.Transmale:
                    PART_Transmale.IsChecked = true;
                    break;
            }
        }

        protected BasicCharacterDefinition ViewModel => DataContext as BasicCharacterDefinition;

        protected string Initialize()
        {
            var culture = CultureInfo.CurrentCulture;
            var lcid = culture.LCID;
            return lcid switch
            {
                2052 => "性别",
                1028 or 3076 => "性別",
                _ => "Gender",
            };
        }

        private void GenderChanged(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Gender) ((ToggleSelector) sender).Content;
            if (ViewModel is not null)
            {
                ViewModel.Gender = selectedItem;
            }
        }
    }
}