using System;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using ICSharpCode.AvalonEdit;
using Markdig.Wpf;
using ReactiveUI;

namespace Acorisoft.Editor
{
    [TemplatePart(Name = PART_MarkdownName, Type = typeof(FlowDocumentScrollViewerExtended))]
    [TemplatePart(Name = PART_DocumentName, Type = typeof(TextEditor))]
    public class MarkdownEditView : Control
    {
        private const string PART_MarkdownName = "PART_Markdown";
        private const string PART_DocumentName = "PART_Document";
        
        static MarkdownEditView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MarkdownEditView), new FrameworkPropertyMetadata(typeof(MarkdownEditView)));
        }

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode", typeof(MarkdownEditMode), typeof(MarkdownEditView), new PropertyMetadata(default(MarkdownEditMode)));

        private FlowDocumentScrollViewerExtended PART_Document;
        private TextEditor PART_Markdown;

        public MarkdownEditView()
        {
            this.Loaded += OnLoadedImpl;
            this.Unloaded += OnUnloadedImpl;
        }

        private void OnUnloadedImpl(object sender, RoutedEventArgs e)
        {
            
        }

        private void OnLoadedImpl(object sender, RoutedEventArgs e)
        {
        }

        public override void OnApplyTemplate()
        {
            PART_Markdown = GetTemplateChild(PART_MarkdownName) as TextEditor;
            PART_Document = GetTemplateChild(PART_DocumentName) as FlowDocumentScrollViewerExtended;
            
            //
            //
            if (PART_Markdown != null)
            {
                var disposable = Observable.FromEventPattern(
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
            }
        }

        public MarkdownEditMode Mode
        {
            get { return (MarkdownEditMode) GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }
    }
}