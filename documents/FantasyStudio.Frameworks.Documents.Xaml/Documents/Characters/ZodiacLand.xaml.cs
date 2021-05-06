using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Acorisoft.Frameworks.Controls;

namespace Acorisoft.FantasyStudio.Documents.Characters
{
    public partial class ZodiacLand : UserControl
    {
        public ZodiacLand()
        {
            InitializeComponent();
            PART_Title.Header = Initialize();
        }      
        private CharacterZodiacDefinition ViewModel => DataContext as CharacterZodiacDefinition;

        private  static string Initialize()
        {
            var culture = CultureInfo.CurrentCulture;
            var lcid = culture.LCID;
            return lcid switch
            {
                2052 => "星座",
                1028 or 3076 => "星座",
                _ => "Zodiac",
            };
        }

        private void ZodiacChanged(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Zodiac) ((ToggleSelector) sender).Content;
            if (ViewModel is not null)
            {
                ViewModel.Zodiac = selectedItem;
            }
        }
    }
}