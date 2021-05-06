using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Acorisoft.Frameworks.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ToggleSwitch : ToggleButton
    {
        static ToggleSwitch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleSwitch), new FrameworkPropertyMetadata(typeof(ToggleSwitch)));
        }
    }

    public partial class ToggleSelector : RadioButton
    {
        static ToggleSelector()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleSelector), new FrameworkPropertyMetadata(typeof(ToggleSelector)));
        }
    }
}
