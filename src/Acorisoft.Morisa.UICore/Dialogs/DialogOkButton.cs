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

namespace Acorisoft.Morisa.Dialogs
{

    public class DialogOkButton : Button
    {
        static DialogOkButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogOkButton), new FrameworkPropertyMetadata(typeof(DialogOkButton)));
        }
    }

    public class DialogCancelButton : Button
    {
        static DialogCancelButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogCancelButton), new FrameworkPropertyMetadata(typeof(DialogCancelButton)));
        }
    }

    public class DialogCofirmButton : Button {
        static DialogCofirmButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogCofirmButton), new FrameworkPropertyMetadata(typeof(DialogCofirmButton)));
        }
    }

}
