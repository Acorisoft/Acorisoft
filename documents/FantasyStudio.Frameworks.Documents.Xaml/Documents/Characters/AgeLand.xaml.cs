using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
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
    /// AgeLand.xaml 的交互逻辑
    /// </summary>
    public partial class AgeLand : UserControl
    {
        public AgeLand()
        {
            InitializeComponent();
            PART_Title.Header = Initialize();
        }

        protected string Initialize()
        {
            var culture = CultureInfo.CurrentCulture;
            var lcid = culture.LCID;
            return lcid switch
            {
                2052 => "年龄",
                1028 or 3076 => "年齡",
                _ => "Age",
            };
        }
    }
}
