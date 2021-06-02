using System;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using ICSharpCode.AvalonEdit;
using Markdig.Wpf;
using ReactiveUI;

namespace Acorisoft.Editor
{
    [TemplatePart(Name = PART_MarkdownName, Type = typeof(FlowDocumentScrollViewerExtended))]
    [TemplatePart(Name = PART_DocumentName, Type = typeof(TextEditor))]
    [TemplatePart(Name = PART_HBarName, Type = typeof(ScrollBar))]
    [TemplatePart(Name = PART_VBarName, Type = typeof(ScrollBar))]
    public class MarkdownEditView : Control
    {
        private const string PART_MarkdownName = "PART_Markdown";
        private const string PART_DocumentName = "PART_Document";
        private const string PART_HBarName = "PART_HBar";
        private const string PART_VBarName = "PART_VBar";

        static MarkdownEditView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MarkdownEditView),
                new FrameworkPropertyMetadata(typeof(MarkdownEditView)));
        }

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode",
            typeof(MarkdownEditMode), typeof(MarkdownEditView), new PropertyMetadata(default(MarkdownEditMode)));

        private FlowDocumentScrollViewerExtended PART_Document;
        private TextEditor PART_Markdown;
        private ScrollBar PART_HBar;
        private ScrollBar PART_VBar;
        private IDisposable _markdownChangedDisposable;

        private double _docHeight;
        private double _docWidth;
        private double _mdHeight;
        private double _mdWidth;

        public MarkdownEditView()
        {
            this.SizeChanged += OnSizeChanged;
            this.Loaded += OnLoadedImpl;
            this.Unloaded += OnUnloadedImpl;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var mdScrollViewer = MarkdownScrollViewer;
            var docScrollViewer = DocumentScrollViewer;
            _docHeight = docScrollViewer.ViewportHeight +
                         docScrollViewer.ExtentHeight;
            _docWidth = docScrollViewer.ViewportWidth +
                        docScrollViewer.ExtentWidth;
            _mdHeight = mdScrollViewer.ViewportHeight +
                        mdScrollViewer.ExtentHeight;
            _mdWidth = mdScrollViewer.ViewportWidth +
                       mdScrollViewer.ExtentWidth;

            var docVRate = _docHeight / (docScrollViewer.ViewportHeight + 1);
            var docHRate = _docWidth / (docScrollViewer.ViewportWidth + 1);

            PART_HBar.Maximum = Math.Clamp(docHRate, 1, ushort.MaxValue);
            PART_HBar.Minimum = 0.0;
            PART_HBar.ViewportSize = 1;

            PART_VBar.Maximum = Math.Clamp(docVRate, 1, ushort.MaxValue);
            PART_VBar.Minimum = 0.0;
            PART_VBar.ViewportSize = 1;

            mdScrollViewer.ScrollToVerticalOffset(PART_VBar.Value * _mdHeight);
            docScrollViewer.ScrollToVerticalOffset(PART_VBar.Value * _docHeight);
            mdScrollViewer.ScrollToHorizontalOffset(PART_HBar.Value * _mdWidth);
            docScrollViewer.ScrollToHorizontalOffset(PART_HBar.Value * _docWidth);
        }

        private void OnUnloadedImpl(object sender, RoutedEventArgs e)
        {
            _markdownChangedDisposable?.Dispose();
        }

        private void OnLoadedImpl(object sender, RoutedEventArgs e)
        {
            //
            //
            if (PART_Markdown != null)
            {
                PART_Markdown.ScrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
            }

            if (PART_Document != null)
            {
                PART_Document.ScrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
            }

            if (PART_HBar != null)
            {
                PART_HBar.ValueChanged += OnHorizontalScrolling;
            }

            if (PART_VBar != null)
            {
                PART_VBar.ValueChanged += VerticalScrolling;
            }

            if (PART_HBar != null && PART_VBar != null)
            {
                PART_Document.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                PART_Document.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                PART_Markdown.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                PART_Markdown.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            }
        }

        public override void OnApplyTemplate()
        {
            PART_Markdown = GetTemplateChild(PART_MarkdownName) as TextEditor;
            PART_Document = GetTemplateChild(PART_DocumentName) as FlowDocumentScrollViewerExtended;
            PART_HBar = GetTemplateChild(PART_HBarName) as ScrollBar;
            PART_VBar = GetTemplateChild(PART_VBarName) as ScrollBar;
            if (PART_Markdown != null)
            {
                //
                // 呈现内容会在TextChanged事件触发时每400ms更新。
                _markdownChangedDisposable = Observable.FromEventPattern(
                        handler => PART_Markdown.TextChanged += handler,
                        handler => PART_Markdown.TextChanged -= handler)
                    .Throttle(TimeSpan.FromMilliseconds(400), RxApp.MainThreadScheduler)
                    .Subscribe(x => { OnMarkdownTextChanged(PART_Markdown, (EventArgs) x.EventArgs); });
            }
        }

        #region ScrollTo

        private void ForceMarkdownScrollTo()
        {
            var h = PART_HBar.Value;
            var v = PART_VBar.Value;
            // PART_Markdown.ScrollViewer.HorizontalOffset = 
        }


        private void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var mdScrollViewer = MarkdownScrollViewer;
            var docScrollViewer = DocumentScrollViewer;
            mdScrollViewer.ScrollToVerticalOffset(PART_VBar.Value * _mdHeight);
            docScrollViewer.ScrollToVerticalOffset(PART_VBar.Value * _docHeight);
            mdScrollViewer.ScrollToHorizontalOffset(PART_HBar.Value * _mdWidth);
            docScrollViewer.ScrollToHorizontalOffset(PART_HBar.Value * _docWidth);
        }


        private void VerticalScrolling(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var mdScrollViewer = MarkdownScrollViewer;
            var docScrollViewer = DocumentScrollViewer;
            mdScrollViewer.ScrollToVerticalOffset(PART_VBar.Value * _mdHeight);
            docScrollViewer.ScrollToVerticalOffset(PART_VBar.Value * _docHeight);
        }

        private void OnHorizontalScrolling(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var mdScrollViewer = MarkdownScrollViewer;
            var docScrollViewer = DocumentScrollViewer;
            mdScrollViewer.ScrollToHorizontalOffset(PART_HBar.Value * _mdWidth);
            docScrollViewer.ScrollToHorizontalOffset(PART_HBar.Value * _docWidth);
        }


        private void OnMarkdownTextChanged(object? sender, EventArgs e)
        {
            if (PART_Document != null)
            {
                var markdownDocument = Markdown.ToDocument(PART_Markdown.Text);
                var flowDocument = Markdown.ToFlowDocument(markdownDocument);

                PART_Document.Document = flowDocument;
            }

            // if (PART_Document != null && PART_Markdown != null)
            // {
            //     //
            //     //
            //     var mdScrollViewer = PART_Markdown.ScrollViewer;
            //     var docScrollViewer = PART_Document.ScrollViewer;
            //     //
            //     // var mdActualWidth = mdScrollViewer.ExtentWidth + mdScrollViewer.ViewportWidth;
            //     // var mdActualHeight = mdScrollViewer.ExtentHeight + mdScrollViewer.ViewportHeight;
            //     //
            //     // var docActualWidth = docScrollViewer.ExtentWidth + docScrollViewer.ViewportWidth;
            //     // var docActualHeight = docScrollViewer.ExtentHeight + docScrollViewer.ViewportHeight;
            //     //
            //     // var minWidth = Math.Min(mdActualWidth, docActualWidth);
            //     // var minHeight = Math.Min(mdActualHeight, docActualHeight);
            //     // var maxWidth = Math.Max(mdActualWidth, docActualWidth);
            //     // var maxHeight = Math.Max(mdActualHeight, docActualHeight);
            //     // _horizontalScaleRate = Math.Min(mdActualWidth, docActualWidth) / Math.Max(mdActualWidth, docActualWidth);
            //     // _vertialScaleRate = Math.Min(mdActualHeight, docActualHeight) / Math.Max(mdActualHeight, docActualHeight);
            //     //
            //     // _horizontalScaleRate = minWidth / maxWidth;
            //     // _vertialScaleRate = minHeight / maxHeight;
            //     //
            //     // //
            //     // // 设置最大值
            //     // PART_HBar.Maximum = maxWidth;
            //     // PART_HBar.ViewportSize = 1;
            //     //
            //     // PART_VBar.Maximum = maxHeight;
            //     // PART_VBar.ViewportSize = 1;
            //
            //     _docHeight = docScrollViewer.ViewportHeight +
            //                  docScrollViewer.ExtentHeight + 1;
            //     _docWidth = docScrollViewer.ViewportWidth +
            //                 docScrollViewer.ExtentWidth + 1;
            //     _mdHeight = mdScrollViewer.ViewportHeight +
            //                  mdScrollViewer.ExtentHeight + 1;
            //     _mdWidth = mdScrollViewer.ViewportWidth +
            //                 mdScrollViewer.ExtentWidth + 1;
            //     
            //     var docVRate = docScrollViewer.ViewportHeight / _docHeight;
            //     var docHRate = docScrollViewer.ViewportWidth / _docWidth;
            //     
            //     PART_HBar.Maximum = docHRate;
            //     PART_HBar.Minimum = 0.0;
            //     
            //     PART_VBar.Maximum = docVRate;
            //     PART_VBar.Minimum = 0.0;
            // }

            var mdScrollViewer = MarkdownScrollViewer;
            var docScrollViewer = DocumentScrollViewer;
            _docHeight = docScrollViewer.ViewportHeight +
                         docScrollViewer.ExtentHeight + 1;
            _docWidth = docScrollViewer.ViewportWidth +
                        docScrollViewer.ExtentWidth + 1;
            _mdHeight = mdScrollViewer.ViewportHeight +
                        mdScrollViewer.ExtentHeight + 1;
            _mdWidth = mdScrollViewer.ViewportWidth +
                       mdScrollViewer.ExtentWidth + 1;

            var docVRate = docScrollViewer.ViewportHeight / _docHeight;
            var docHRate = docScrollViewer.ViewportWidth / _docWidth;

            PART_HBar.Maximum = docHRate;
            PART_HBar.Minimum = 0.0;
            PART_HBar.ViewportSize = 1;

            PART_VBar.Maximum = docVRate;
            PART_VBar.Minimum = 0.0;
            PART_VBar.ViewportSize = 1;

            docScrollViewer.ScrollToVerticalOffset(PART_VBar.Value * _docHeight - docScrollViewer.ViewportHeight);
            docScrollViewer.ScrollToHorizontalOffset(PART_HBar.Value * _docWidth - docScrollViewer.ViewportWidth);
            mdScrollViewer.ScrollToVerticalOffset(PART_VBar.Value * _mdHeight - mdScrollViewer.ViewportHeight);
            mdScrollViewer.ScrollToHorizontalOffset(PART_HBar.Value * _mdWidth - mdScrollViewer.ViewportWidth);
        }

        #endregion

        public ScrollViewer MarkdownScrollViewer => PART_Markdown.ScrollViewer;
        public ScrollViewer DocumentScrollViewer => PART_Document.ScrollViewer;

        /// <summary>
        /// 
        /// </summary>
        public TextEditor Editor => PART_Markdown;

        /// <summary>
        /// 
        /// </summary>
        public FlowDocumentScrollViewerExtended Presentation => PART_Document;

        public MarkdownEditMode Mode
        {
            get { return (MarkdownEditMode) GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }
    }
}