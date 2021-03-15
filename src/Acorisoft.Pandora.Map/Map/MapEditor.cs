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

        protected class InternalStylusPointObserver : IObserver<StylusPoint>
        {
            public void OnCompleted()
            {
            }

            public void OnError(Exception error)
            {
            }

            public void OnNext(StylusPoint value)
            {
            }

            public ISubject<StylusPoint, IMapSemantic> Output { get; set; }
        }

        private readonly InternalStylusPointObserver _PointObserver;
        private readonly MapInputHandler _InputHandler;

        public MapEditor()
        {
            //
            // Initialize Variables
            _PointObserver = new InternalStylusPointObserver();
            _InputHandler = new MapInputHandler();
            _InputHandler.Subscribe(_PointObserver);

            //
            // Set Variables
            StylusPlugIns.Add(_InputHandler);
        }

        //
        // StylusPoint -> IMapSemantic
        // IMapSemantic -> IMapEditOperation
    }
}
