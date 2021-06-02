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
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MarkdownEditView), new FrameworkPropertyMetadata(typeof(MarkdownEditView)));
        }

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode", typeof(MarkdownEditMode), typeof(MarkdownEditView), new PropertyMetadata(default(MarkdownEditMode)));

        private FlowDocumentScrollViewerExtended PART_Document;
        private TextEditor PART_Markdown;
        private ScrollBar PART_HBar;
        private ScrollBar PART_VBar;
        private IDisposable _markdownChangedDisposable;

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
        }

        public override void OnApplyTemplate()
        {
            PART_Markdown = GetTemplateChild(PART_MarkdownName) as TextEditor;
            PART_Document = GetTemplateChild(PART_DocumentName) as FlowDocumentScrollViewerExtended;
            PART_HBar = GetTemplateChild(PART_HBarName) as ScrollBar;
            PART_VBar = GetTemplateChild(PART_VBarName) as ScrollBar;
            
            
            //
            //
            if (PART_Markdown != null)
            {
                //
                // 呈现内容会在TextChanged事件触发时每400ms更新。
                _markdownChangedDisposable = Observable.FromEventPattern(
                        handler => PART_Markdown.TextChanged += handler,
                        handler => PART_Markdown.TextChanged -= handler)
                    .Throttle(TimeSpan.FromMilliseconds(400), RxApp.MainThreadScheduler)
                    .Subscribe(x =>
                    {
                        OnMarkdownTextChanged(PART_Markdown, (EventArgs)x.EventArgs);
                    });
                
            }
        }
        

        private void OnMarkdownTextChanged(object? sender, EventArgs e)
        {
            if (PART_Document != null)
            {
                var markdownDocument = Markdown.ToDocument(PART_Markdown.Text);
                var flowDocument = Markdown.ToFlowDocument(markdownDocument);
                
                PART_Document.Document = flowDocument;
                PART_Document.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                PART_Document.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                PART_Markdown.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                PART_Markdown.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                
                //
                //
                
                // PART_HBar.Maximum = Math.Max(PART_Document.ho)
            }
        }

        public MarkdownEditMode Mode
        {
            get { return (MarkdownEditMode) GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }
    }
}