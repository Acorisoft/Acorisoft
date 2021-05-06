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
using ICSharpCode.AvalonEdit;
using Markdig;
using Markdig.Wpf;

namespace Acorisoft.Pandora
{
    /// <summary>
    /// 
    /// </summary>
    public class MarkdownEditor : Control
    {
        public const string RendererName = "PART_Renderer";
        public const string EditorName = "PART_Editor";

        static MarkdownEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MarkdownEditor), new FrameworkPropertyMetadata(typeof(MarkdownEditor)));
        }

        private TextEditor PART_Editor;
        private MarkdownViewer PART_Renderer;
        private bool _isPipelineChangedInInitialize;
        private bool _isDocumentChangedInInitialize;

        public override void OnApplyTemplate()
        {
            PART_Editor = GetTemplateChild(EditorName) as TextEditor;
            PART_Editor.ScrollViewer.ScrollChanged += OnScrollChanged;
            PART_Renderer = GetTemplateChild(RendererName) as MarkdownViewer;

            if (_isDocumentChangedInInitialize)
            {
                PART_Renderer.Markdown = Markdown; 
            }

            if (_isPipelineChangedInInitialize)
            {
                PART_Renderer.Pipeline = Pipeline;
            }

            base.OnApplyTemplate();
        }

        private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            
        }

        private static readonly DependencyPropertyKey DocumentPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Document), typeof(FlowDocument), typeof(MarkdownViewer), new FrameworkPropertyMetadata());

        /// <summary>
        /// Defines the <see cref="Document"/> property.
        /// </summary>
        public static readonly DependencyProperty DocumentProperty = DocumentPropertyKey.DependencyProperty;

        /// <summary>
        /// Defines the <see cref="Markdown"/> property.
        /// </summary>
        public static readonly DependencyProperty MarkdownProperty =
            DependencyProperty.Register(nameof(Markdown), typeof(string), typeof(MarkdownViewer), new FrameworkPropertyMetadata(MarkdownChanged));

        /// <summary>
        /// Defines the <see cref="Markdown"/> property.
        /// </summary>
        public static readonly DependencyProperty PipelineProperty =
            DependencyProperty.Register(nameof(Pipeline), typeof(MarkdownPipeline), typeof(MarkdownViewer), new FrameworkPropertyMetadata(PipelineChanged));

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
            "Mode",
            typeof(EditMode),
            typeof(MarkdownEditor),
            new PropertyMetadata(EditMode.EditAndDisplay));



        public EditMode Mode
        {
            get => (EditMode)GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }

        /// <summary>
        /// Gets the flow document to display.
        /// </summary>
        public FlowDocument? Document
        {
            get { return (FlowDocument)GetValue(DocumentProperty); }
            protected set { SetValue(DocumentPropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets the markdown to display.
        /// </summary>
        public string? Markdown
        {
            get { return (string)GetValue(MarkdownProperty); }
            set { SetValue(MarkdownProperty, value); }
        }

        /// <summary>
        /// Gets or sets the markdown pipeline to use.
        /// </summary>
        public MarkdownPipeline Pipeline
        {
            get { return (MarkdownPipeline)GetValue(PipelineProperty); }
            set { SetValue(PipelineProperty, value); }
        }

        private static void MarkdownChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var editor = (MarkdownEditor)sender;
            if (editor != null)
            {
                if (editor.PART_Renderer != null)
                {
                    editor.PART_Renderer.Markdown = e.NewValue as string;
                }
                else
                {
                    editor._isDocumentChangedInInitialize = true;
                }
            }
        }

        private static void PipelineChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var editor = (MarkdownEditor)sender;
            if(editor != null)
            {
                if (editor.PART_Renderer != null)
                {
                    editor.PART_Renderer.Pipeline = e.NewValue as MarkdownPipeline;
                }
                else
                {
                    editor._isPipelineChangedInInitialize = true;
                }
            }
        }       
    }
}
