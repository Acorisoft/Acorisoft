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

        private IMapDocument _MapDocument;
        private IDisposable _Disposable;

        public MapViewer()
        {
        }

        protected internal void ReleaseMapDocument()
        {
            _Disposable?.Dispose();
        }

        protected virtual void OnDocumentChanged(object sender, EventArgs e)
        {
            if(_MapDocument != null)
            {
                //
                // 当文档更新的时候使用10ms的采样率进行更新界面,也就是最高100FPS
                _Disposable = Observable.FromEvent(handler => _MapDocument.DocumentChanged += handler , handler => _MapDocument.DocumentChanged -= handler)
                                        .Throttle(TimeSpan.FromMilliseconds(10))
                                        .SubscribeOn(Dispatcher)
                                        .Subscribe(x => InvalidateVisual());
            }
        }

        public IMapDocument Document
        {
            get => (IMapDocument)GetValue(DocumentProperty);
            set => SetValue(DocumentProperty, value);
        }

        public static readonly DependencyProperty DocumentProperty = DependencyProperty.Register(
            "Document",
            typeof(IMapDocument),
            typeof(MapViewer),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender,OnDocumentChanged));


        private static void OnDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mapViewer = (MapViewer)d;

            if (e.OldValue is IMapDocument)
            {
                mapViewer.ReleaseMapDocument();
            }

            if (e.NewValue is IMapDocument newMapDocument)
            {
                mapViewer._MapDocument = newMapDocument;
                mapViewer.OnDocumentChanged(mapViewer, new EventArgs());
            }
        }

    }
}
