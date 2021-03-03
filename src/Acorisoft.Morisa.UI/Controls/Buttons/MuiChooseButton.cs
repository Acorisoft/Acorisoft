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

namespace Acorisoft.Morisa.Controls.Buttons
{
    /// <summary>
    /// 选择控件
    /// </summary>
    public class MuiChooseButton : MuiRadioButton
    {
        static MuiChooseButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MuiChooseButton), new FrameworkPropertyMetadata(typeof(MuiChooseButton)));
        }
    }
}
