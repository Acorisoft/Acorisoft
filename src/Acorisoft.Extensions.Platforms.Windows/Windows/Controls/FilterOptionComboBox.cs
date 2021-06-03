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

namespace Acorisoft.Extensions.Platforms.Windows.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class FilterOptionComboBox : ComboBox
    {
        static FilterOptionComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FilterOptionComboBox), new FrameworkPropertyMetadata(typeof(FilterOptionComboBox)));
        }
    }
}
