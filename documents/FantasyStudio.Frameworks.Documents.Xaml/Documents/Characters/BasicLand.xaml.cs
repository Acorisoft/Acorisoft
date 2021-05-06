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
    /// BasicLand.xaml 的交互逻辑
    /// </summary>
    public partial class BasicLand : UserControl
    {
        public BasicLand()
        {
            InitializeComponent();
            PART_Title.Header = Initialize();
        }

        protected static string Initialize()
        {
            var culture = CultureInfo.CurrentCulture;
            var lcid = culture.LCID;
            return lcid switch
            {
                2052 => "基本设定",
                1028 or 3076 => "基本設定",
                _ => "Basic Definitions",
            };
        }
    }
}
