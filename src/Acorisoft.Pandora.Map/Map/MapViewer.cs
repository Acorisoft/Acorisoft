using Acorisoft.Morisa.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
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
using System.Reactive.Subjects;

namespace Acorisoft.Pandora.Map
{
    /// <summary>
    /// <see cref="MapViewer"/>
    /// </summary>
    public class MapViewer : Control
    {
        static MapViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MapViewer), new FrameworkPropertyMetadata(typeof(MapViewer)));
        }
    }
}
