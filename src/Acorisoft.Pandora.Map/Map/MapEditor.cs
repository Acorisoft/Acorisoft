using Acorisoft.Pandora.Inputs;
using Acorisoft.Pandora.Semantic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
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

namespace Acorisoft.Pandora.Map
{
    /// <summary>
    /// <see cref="MapEditor"/> 表示一个地图编辑器
    /// </summary>
    public class MapEditor : Control
    {
        static MapEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MapEditor), new FrameworkPropertyMetadata(typeof(MapEditor)));
        }
    }
}
