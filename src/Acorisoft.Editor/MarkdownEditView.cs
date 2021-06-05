using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using Acorisoft.Extensions.Platforms;
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

        //-----------------------------------------------------------------------
        //
        //  Public Methods
        //
        //-----------------------------------------------------------------------
        
        
        static MarkdownEditView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MarkdownEditView), new FrameworkPropertyMetadata(typeof(MarkdownEditView)));
            MarkdownDocumentPropertyKey = DependencyProperty.RegisterReadOnly(
                "MarkdownDocument", typeof(MarkdownDocument), typeof(MarkdownEditView), new PropertyMetadata(default(MarkdownDocument)));
            FlowDocumentPropertyKey = DependencyProperty.RegisterReadOnly("FlowDocument", typeof(FlowDocument),
                typeof(MarkdownEditView), new PropertyMetadata(default(FlowDocument)));
            MarkdownDocumentProperty = MarkdownDocumentPropertyKey.DependencyProperty;
            FlowDocumentProperty = FlowDocumentPropertyKey.DependencyProperty;
        }

        
        //-----------------------------------------------------------------------
        //
        //  Public Methods
        //
        //-----------------------------------------------------------------------
        public static readonly DependencyPropertyKey MarkdownDocumentPropertyKey;
        public static readonly DependencyProperty MarkdownDocumentProperty;
        public static readonly DependencyPropertyKey FlowDocumentPropertyKey;
        public static readonly DependencyProperty FlowDocumentProperty ;
        
        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode",
            typeof(MarkdownEditMode), 
            typeof(MarkdownEditView),
            new PropertyMetadata(default(MarkdownEditMode)));


        public static readonly DependencyProperty MarkdownTextProperty = DependencyProperty.Register("MarkdownText", 
            typeof(string), 
            typeof(MarkdownEditView), 
            new PropertyMetadata(default(string)));


        //-----------------------------------------------------------------------
        //
        //  Public Methods
        //
        //-----------------------------------------------------------------------
        
        private FlowDocumentScrollViewerExtended documentPresentation;
        private TextEditor markdownEditor;
        private IDisposable _markdownChangedDisposable;

        private ScrollToAction _scrollToAction;

        //-----------------------------------------------------------------------
        //
        //  Public Methods
        //
        //-----------------------------------------------------------------------
        public MarkdownEditView()
        {
            this.Loaded += OnLoadedImpl;
            this.Unloaded += OnUnloadedImpl;
        }
        
        //-----------------------------------------------------------------------
        //
        //  Public Methods
        //
        //-----------------------------------------------------------------------
        private void OnUnloadedImpl(object sender, RoutedEventArgs e)
        {
            _markdownChangedDisposable?.Dispose();
        }
        
        private void OnLoadedImpl(object sender, RoutedEventArgs e)
        {
            if (markdownEditor != null)
            {
                markdownEditor.ScrollViewer.ScrollChanged += OnMarkdownScrollViewerScrollChanged;
            }
            
            if (documentPresentation != null)
            {
                documentPresentation.ScrollViewer.ScrollChanged += OnDocumentScrollViewerScrollChanged;
            }
        }
        
        
        public override void OnApplyTemplate()
        {
            markdownEditor = GetTemplateChild(PART_MarkdownName) as TextEditor;
            documentPresentation = GetTemplateChild(PART_DocumentName) as FlowDocumentScrollViewerExtended;
            
            if (markdownEditor != null)
            {
                //
                // 呈现内容会在TextChanged事件触发时每50ms更新。
                _markdownChangedDisposable = Observable.FromEventPattern(
                        handler => markdownEditor.TextChanged += handler,
                        handler => markdownEditor.TextChanged -= handler)
                    .Throttle(TimeSpan.FromMilliseconds(50), RxApp.MainThreadScheduler)
                    .Subscribe(x => { OnMarkdownTextChanged(markdownEditor, (EventArgs) x.EventArgs); });
                
                //
                //
                markdownEditor.Text = (string)GetValue(MarkdownTextProperty);
            }
        }

        
        //-----------------------------------------------------------------------
        //
        //  Public Methods
        //
        //-----------------------------------------------------------------------
        #region Public Methods
        
        /// <summary>
        /// 设置Markdown文本。
        /// </summary>
        /// <param name="markdown">Markdown文本</param>
        public void SetMarkdownText(string markdown)
        {
            if (markdownEditor == null)
            {
                return;
            }
            
            markdownEditor.Text = markdown;
            MarkdownText = markdown;
        }

        /// <summary>
        /// 获取当前行内容
        /// </summary>
        /// <returns>返回当前行内容</returns>
        /// <exception cref="NullReferenceException">访问未初始化的编辑器文档。</exception>
        public string GetCurrentLineText()
        {
            //
            // 当前光标的位置
            var line = markdownEditor?.Document.GetLineByOffset(markdownEditor.CaretOffset);
            
            //
            // 获取当前光标的位置的内容
            return markdownEditor?.Document.GetText(line);
        }

        protected static int LastCharacterOf(string content, char character)
        {
            for (var i = 0; i < content.Length; i++)
            {
                if (content[i] != character)
                {
                    return i;
                }
            }

            return content.Length - 1;
        }

        /// <summary>
        /// 将当前行转化为Markdown格式中的一级大纲内容或者取消格式化
        /// </summary>
        public void ToggleFormatCurrentLineAsMarkdownHeadline1()
        {
            //
            // Avoid Null Reference Object Access
            if (markdownEditor?.Document == null)
            {
                return;
            }
            
            //
            // 当前光标的位置
            var line = markdownEditor?.Document.GetLineByOffset(markdownEditor.CaretOffset);
            
            //
            // 获取当前光标的位置的内容
            var currentLineText = markdownEditor?.Document.GetText(line);


            string currentLineNewString;
            
            if (currentLineText.StartsWith("#"))
            {
                //
                // 寻找到第一个非 # 的单位
                var index = currentLineText.FirstIndexOfDifference('#');

                currentLineNewString = currentLineText.Substring(index, Math.Clamp(currentLineText.Length - index, 0, currentLineText.Length));
            }
            else
            {
                currentLineNewString = "# " + currentLineText;
            }

            markdownEditor?.Document.Replace(line, currentLineNewString.TrimStart());
        }
        
        /// <summary>
        /// 将当前行转化为Markdown格式中的二级大纲内容或者取消格式化
        /// </summary>
        public void ToggleFormatCurrentLineAsMarkdownHeadline2()
        {
            //
            // Avoid Null Reference Object Access
            if (markdownEditor?.Document == null)
            {
                return;
            }
            
            //
            // 当前光标的位置
            var line = markdownEditor?.Document.GetLineByOffset(markdownEditor.CaretOffset);
            
            //
            // 获取当前光标的位置的内容
            var currentLineText = markdownEditor?.Document.GetText(line);


            string currentLineNewString;
            
            if (currentLineText.StartsWith("#"))
            {
                //
                // 寻找到第一个非 # 的单位
                var index = currentLineText.FirstIndexOfDifference('#');
                

                currentLineNewString = currentLineText.Substring(index, Math.Clamp(currentLineText.Length - index, 0, currentLineText.Length));
            }
            else
            {
                currentLineNewString = "## " + currentLineText;
            }

            markdownEditor?.Document.Replace(line, currentLineNewString.TrimStart());
        }
        
        /// <summary>
        /// 将当前行转化为Markdown格式中的三级大纲内容或者取消格式化
        /// </summary>
        public void ToggleFormatCurrentLineAsMarkdownHeadline3()
        {
            //
            // Avoid Null Reference Object Access
            if (markdownEditor?.Document == null)
            {
                return;
            }
            
            //
            // 当前光标的位置
            var line = markdownEditor?.Document.GetLineByOffset(markdownEditor.CaretOffset);
            
            //
            // 获取当前光标的位置的内容
            var currentLineText = markdownEditor?.Document.GetText(line);


            string currentLineNewString;
            
            if (currentLineText.StartsWith("#"))
            {
                //
                // 寻找到第一个非 # 的单位
                var index = currentLineText.FirstIndexOfDifference('#');

                currentLineNewString = currentLineText.Substring(index, Math.Clamp(currentLineText.Length - index, 0, currentLineText.Length));
            }
            else
            {
                currentLineNewString = "### " + currentLineText;
            }

            markdownEditor?.Document.Replace(line, currentLineNewString.TrimStart());
        }
        
        /// <summary>
        /// 将当前行转化为Markdown格式中的四级大纲内容或者取消格式化
        /// </summary>
        public void ToggleFormatCurrentLineAsMarkdownHeadline4()
        {
            //
            // Avoid Null Reference Object Access
            if (markdownEditor?.Document == null)
            {
                return;
            }
            
            //
            // 当前光标的位置
            var line = markdownEditor?.Document.GetLineByOffset(markdownEditor.CaretOffset);
            
            //
            // 获取当前光标的位置的内容
            var currentLineText = markdownEditor?.Document.GetText(line);


            string currentLineNewString;
            
            if (currentLineText.StartsWith("#"))
            {
                //
                // 寻找到第一个非 # 的单位
                var index = currentLineText.FirstIndexOfDifference('#');

                currentLineNewString = currentLineText.Substring(index, Math.Clamp(currentLineText.Length - index, 0, currentLineText.Length));
            }
            else
            {
                currentLineNewString = "#### " + currentLineText;
            }

            markdownEditor?.Document.Replace(line, currentLineNewString.TrimStart());
        }
        
        /// <summary>
        /// 将当前行转化为Markdown格式中的五级大纲内容或者取消格式化
        /// </summary>
        public void ToggleFormatCurrentLineAsMarkdownHeadline5()
        {
            //
            // Avoid Null Reference Object Access
            if (markdownEditor?.Document == null)
            {
                return;
            }
            
            //
            // 当前光标的位置
            var line = markdownEditor?.Document.GetLineByOffset(markdownEditor.CaretOffset);
            
            //
            // 获取当前光标的位置的内容
            var currentLineText = markdownEditor?.Document.GetText(line);


            string currentLineNewString;
            
            if (currentLineText.StartsWith("#"))
            {
                //
                // 寻找到第一个非 # 的单位
                var index = currentLineText.FirstIndexOfDifference('#');

                currentLineNewString = currentLineText.Substring(index, Math.Clamp(currentLineText.Length - index, 0, currentLineText.Length));
            }
            else
            {
                currentLineNewString = "##### " + currentLineText;
            }

            markdownEditor?.Document.Replace(line, currentLineNewString.TrimStart());
        }
        
        /// <summary>
        /// 将当前行转化为Markdown格式中的六级大纲内容或者取消格式化
        /// </summary>
        public void ToggleFormatCurrentLineAsMarkdownHeadline6()
        {
            //
            // Avoid Null Reference Object Access
            if (markdownEditor?.Document == null)
            {
                return;
            }
            
            //
            // 当前光标的位置
            var line = markdownEditor?.Document.GetLineByOffset(markdownEditor.CaretOffset);
            
            //
            // 获取当前光标的位置的内容
            var currentLineText = markdownEditor?.Document.GetText(line);


            string currentLineNewString;
            
            if (currentLineText.StartsWith("#"))
            {
                //
                // 寻找到第一个非 # 的单位
                var index = currentLineText.FirstIndexOfDifference('#');

                currentLineNewString = currentLineText.Substring(index, Math.Clamp(currentLineText.Length - index, 0, currentLineText.Length));
            }
            else
            {
                currentLineNewString = "###### " + currentLineText;
            }

            markdownEditor?.Document.Replace(line, currentLineNewString.TrimStart());
        }
        
        /// <summary>
        /// 将当前行转化为Markdown格式中的七级大纲内容或者取消格式化
        /// </summary>
        public void ToggleFormatCurrentLineAsMarkdownHeadline7()
        {
            //
            // Avoid Null Reference Object Access
            if (markdownEditor?.Document == null)
            {
                return;
            }
            
            //
            // 当前光标的位置
            var line = markdownEditor?.Document.GetLineByOffset(markdownEditor.CaretOffset);
            
            //
            // 获取当前光标的位置的内容
            var currentLineText = markdownEditor?.Document.GetText(line);


            string currentLineNewString;
            
            if (currentLineText.StartsWith("#"))
            {
                //
                // 寻找到第一个非 # 的单位
                var index = currentLineText.FirstIndexOfDifference('#');

                currentLineNewString = currentLineText.Substring(index, Math.Clamp(currentLineText.Length - index, 0, currentLineText.Length));
            }
            else
            {
                currentLineNewString = "####### " + currentLineText;
            }

            markdownEditor?.Document.Replace(line, currentLineNewString.TrimStart());
        }

        /// <summary>
        /// 将当前选择的内容包装为
        /// </summary>
        public void ToggleFormatCurrentLineAsQuote()
        {
            //
            // Avoid Null Reference Object Access
            if (markdownEditor?.Document == null)
            {
                return;
            }
            
            //
            // 当前光标的位置
            var line = markdownEditor?.Document.GetLineByOffset(markdownEditor.CaretOffset);
            
            //
            // 获取当前光标的位置的内容
            var currentLineText = markdownEditor?.Document.GetText(line);


            string currentLineNewString;
            
            if (currentLineText.StartsWith(">"))
            {
                //
                // 寻找到第一个非 # 的单位
                var index = LastCharacterOf(currentLineText, '>');

                currentLineNewString = currentLineText.Substring(index, Math.Clamp(currentLineText.Length - index, 0, currentLineText.Length));
            }
            else
            {
                currentLineNewString = "> " + currentLineText;
            }

            markdownEditor?.Document.Replace(line, currentLineNewString.TrimStart());
        }
        
        /// <summary>
        /// 将当前选择的内容包装为
        /// </summary>
        public void ToggleFormatCurrentLineAsCode()
        {
            //
            // Avoid Null Reference Object Access
            if (markdownEditor?.Document == null)
            {
                return;
            }
            
            //
            // 当前光标的位置
            var line = markdownEditor?.Document.GetLineByOffset(markdownEditor.CaretOffset);
            
            //
            // 获取当前光标的位置的内容
            var currentLineText = markdownEditor?.Document.GetText(line);


            string currentLineNewString;
            
            if (currentLineText.StartsWith("`") && currentLineText.EndsWith('`'))
            {
                //
                // 寻找到第一个非 ` 的单位
                var firstIndex = currentLineText.FirstIndexOfDifference('`');
                var lastIndex = currentLineText.FirstIndexOfDifference('`', true);
                
                currentLineNewString = currentLineText.Substring(firstIndex, Math.Clamp(currentLineText.Length - lastIndex, 0, currentLineText.Length));
            }
            else
            {
                currentLineNewString = $"`{currentLineText}`";
            }

            markdownEditor?.Document.Replace(line, currentLineNewString.TrimStart());
        }


        public void IncreamentIndent()
        {
            //
            // Avoid Null Reference Object Access
            if (markdownEditor?.Document == null)
            {
                return;
            }
            
            //
            // 当前光标的位置
            var line = markdownEditor?.Document.GetLineByOffset(markdownEditor.CaretOffset);
            
            //
            // 获取当前光标的位置的内容
            var currentLineText = markdownEditor?.Document.GetText(line);
            
            var currentLineNewString = $"\t{currentLineText}";

            markdownEditor?.Document.Replace(line, currentLineNewString.TrimStart());
        }
        
        public void DecreamentIndent()
        {
            //
            // Avoid Null Reference Object Access
            if (markdownEditor?.Document == null)
            {
                return;
            }
            
            //
            // 当前光标的位置
            var line = markdownEditor?.Document.GetLineByOffset(markdownEditor.CaretOffset);
            
            //
            // 获取当前光标的位置的内容
            var currentLineText = markdownEditor?.Document.GetText(line);

            if (!currentLineText.StartsWith("\t"))
            {
                return;
            }
            
            //
            // 寻找到第一个非 ` 的单位
            var index = currentLineText.IndexOf('\t');
                
            var currentLineNewString = currentLineText.Substring(index, Math.Clamp(currentLineText.Length - index, 0, currentLineText.Length));
            markdownEditor?.Document.Replace(line, currentLineNewString.TrimStart());

        }
        #endregion

        
        
        
        
        
        
        
        
        
        
        
        
        

        //-----------------------------------------------------------------------
        //
        //  ScrollTo
        //
        //-----------------------------------------------------------------------
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
            if (markdownEditor != null && documentPresentation != null)
            {
                var document = Markdown.ToDocument(markdownEditor.Text);
                MarkdownDocument = document;
                FlowDocument = Markdown.ToFlowDocument(document);
                SetCurrentValue(MarkdownTextProperty,markdownEditor.Text);
            }

        }

        #endregion

        
        //-----------------------------------------------------------------------
        //
        //  Public Methods
        //
        //-----------------------------------------------------------------------
        
        /// <summary>
        /// 
        /// </summary>
        public FlowDocument FlowDocument
        {
            get => (FlowDocument) GetValue(FlowDocumentProperty);
            private set => SetValue(FlowDocumentPropertyKey, value);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string MarkdownText
        {
            get => (string) GetValue(MarkdownTextProperty);
            set => SetValue(MarkdownTextProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public MarkdownDocument MarkdownDocument
        {
            get => (MarkdownDocument) GetValue(MarkdownDocumentProperty);
            private set => SetValue(MarkdownDocumentPropertyKey, value);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public ScrollViewer MarkdownScrollViewer => markdownEditor.ScrollViewer;
        
        /// <summary>
        /// 
        /// </summary>
        public ScrollViewer DocumentScrollViewer => documentPresentation.ScrollViewer;

        /// <summary>
        /// 
        /// </summary>
        public TextEditor Editor => markdownEditor;

        /// <summary>
        /// 
        /// </summary>
        public FlowDocumentScrollViewerExtended Presentation => documentPresentation;

        /// <summary>
        /// 
        /// </summary>
        public MarkdownEditMode Mode
        {
            get => (MarkdownEditMode) GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }
    }
}