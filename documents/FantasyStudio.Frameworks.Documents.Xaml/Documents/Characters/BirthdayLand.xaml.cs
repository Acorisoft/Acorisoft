using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Acorisoft.FantasyStudio.Documents.Characters
{
    /// <summary>
    /// BirthdayLand.xaml 的交互逻辑
    /// </summary>
    public partial class BirthdayLand : UserControl
    {
        public BirthdayLand()
        {
            InitializeComponent();
            PART_Title.Header = InitializeTitle();
            PART_Height.Header = InitializeHeightTips();
            PART_Weight.Header = InitializeWeightTips();
        }

        private static string InitializeTitle()
        {
            var culture = CultureInfo.CurrentCulture;
            var lcid = culture.LCID;
            return lcid switch
            {
                2052 => "生日",
                1028 or 3076 => "生日",
                _ => "Birthday",
            };
        }
        private static string InitializeHeightTips()
        {
            var culture = CultureInfo.CurrentCulture;
            var lcid = culture.LCID;
            return lcid switch
            {
                2052 => "身高",
                1028 or 3076 => "身高",
                _ => "Height",
            };
        }
        
        private static string InitializeWeightTips()
        {
            var culture = CultureInfo.CurrentCulture;
            var lcid = culture.LCID;
            return lcid switch
            {
                2052 => "体重",
                1028 or 3076 => "體重",
                _ => "Weight",
            };
        }
    }
}
