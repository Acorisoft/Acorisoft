using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using ICSharpCode.AvalonEdit;
using Markdig.Syntax;
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
            MarkdownDocumentPropertyKey = DependencyProperty.RegisterReadOnly(
                "MarkdownDocument", typeof(MarkdownDocument), typeof(MarkdownEditView), new PropertyMetadata(default(MarkdownDocument)));
            FlowDocumentPropertyKey = DependencyProperty.RegisterReadOnly("FlowDocument", typeof(FlowDocument),
                typeof(MarkdownEditView), new PropertyMetadata(default(FlowDocument)));
            MarkdownDocumentProperty = MarkdownDocumentPropertyKey.DependencyProperty;
            FlowDocumentProperty = FlowDocumentPropertyKey.DependencyProperty;
        }
        
        private static void OnMarkdownTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MarkdownEditView view)
            {
                view.PART_Markdown.Text = e.NewValue.ToString();
            }
        }

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode",
            typeof(MarkdownEditMode), typeof(MarkdownEditView), new PropertyMetadata(default(MarkdownEditMode)));

        public static readonly DependencyPropertyKey MarkdownDocumentPropertyKey;
        public static readonly DependencyProperty MarkdownDocumentProperty;

        public static readonly DependencyProperty MarkdownTextProperty = DependencyProperty.Register(
            "MarkdownText", typeof(string), typeof(MarkdownEditView), new PropertyMetadata(default(string), OnMarkdownTextChanged));

        public static readonly DependencyPropertyKey FlowDocumentPropertyKey;
        public static readonly DependencyProperty FlowDocumentProperty ;

        
        private FlowDocumentScrollViewerExtended PART_Document;
        private TextEditor PART_Markdown;
        private ScrollBar PART_HBar;
        private ScrollBar PART_VBar;
        private IDisposable _markdownChangedDisposable;

        private ScrollToAction _scrollToAction;

        public MarkdownEditView()
        {
            this.Loaded += OnLoadedImpl;
            this.Unloaded += OnUnloadedImpl;
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
                PART_Markdown.ScrollViewer.ScrollChanged += OnMarkdownScrollViewerScrollChanged;
            }

            if (PART_Document != null)
            {
                PART_Document.ScrollViewer.ScrollChanged += OnDocumentScrollViewerScrollChanged;
            }

            if (PART_HBar != null && PART_VBar != null && Mode == MarkdownEditMode.Hybrid)
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
                    .Throttle(TimeSpan.FromMilliseconds(50), RxApp.MainThreadScheduler)
                    .Subscribe(x => { OnMarkdownTextChanged(PART_Markdown, (EventArgs) x.EventArgs); });
            }
        }

        #region ScrollTo

        private enum ScrollToAction
        {
            Idle,
            ScrollingMarkdown,
            ScrollingDocument,
        }

        private void OnMarkdownScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (_scrollToAction != ScrollToAction.Idle)
            {
                _scrollToAction = ScrollToAction.Idle;
                return;
            }
            _scrollToAction = ScrollToAction.ScrollingDocument;
            var vRate = (DocumentScrollViewer.ExtentHeight + 1) / (MarkdownScrollViewer.ExtentHeight + 2);
            var hRate = (DocumentScrollViewer.ExtentWidth + 1) / (MarkdownScrollViewer.ExtentWidth + 2);
            DocumentScrollViewer.ScrollToVerticalOffset(DocumentScrollViewer.VerticalOffset + e.VerticalChange * vRate);
            DocumentScrollViewer.ScrollToHorizontalOffset(DocumentScrollViewer.HorizontalOffset + e.HorizontalChange * hRate);
        }
        private void OnDocumentScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (_scrollToAction != ScrollToAction.Idle)
            {
                _scrollToAction = ScrollToAction.Idle;
                return;
            }
            _scrollToAction = ScrollToAction.ScrollingMarkdown;
            var vRate = (MarkdownScrollViewer.ExtentHeight + 1) / (DocumentScrollViewer.ExtentHeight + 2);
            var hRate = (MarkdownScrollViewer.ExtentWidth + 1) / (DocumentScrollViewer.ExtentWidth + 2);
            
            MarkdownScrollViewer.ScrollToVerticalOffset(MarkdownScrollViewer.VerticalOffset + e.VerticalChange * vRate);
            MarkdownScrollViewer.ScrollToHorizontalOffset(MarkdownScrollViewer.HorizontalOffset + e.HorizontalChange * hRate);
        }

        private void OnMarkdownTextChanged(object? sender, EventArgs e)
        {
            if (PART_Document != null)
            {
                var document = Markdown.ToDocument(PART_Markdown.Text);
                MarkdownDocument = document;
                FlowDocument = Markdown.ToFlowDocument(document);
            }

        }

        #endregion

        public FlowDocument FlowDocument
        {
            get => (FlowDocument) GetValue(FlowDocumentProperty);
            private set => SetValue(FlowDocumentPropertyKey, value);
        }
        public string MarkdownText
        {
            get => (string) GetValue(MarkdownTextProperty);
            set => SetValue(MarkdownTextProperty, value);
        }
        public MarkdownDocument MarkdownDocument
        {
            get => (MarkdownDocument) GetValue(MarkdownDocumentProperty);
            private set => SetValue(MarkdownDocumentPropertyKey, value);
        }
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
            get => (MarkdownEditMode) GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }
    }
}