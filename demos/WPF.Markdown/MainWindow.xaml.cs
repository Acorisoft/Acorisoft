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
using System.Xaml;
using Markdig.Wpf;
using XamlReader = System.Windows.Markup.XamlReader;
using System.Reflection;
using Markdig;
using System.IO;
using System.Diagnostics;
using Neo.Markdig.Xaml;

namespace WPF.Markdown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            Editor.TextChanged += Editor_TextChanged;
        }

        private void Editor_TextChanged(object sender, EventArgs e)
        {
            TransformWithMarkdigWPF(Editor.Text);
            TransformWithNeo(Editor.Text);
        }

        private void Rx_TextChanged(object sender, TextChangedEventArgs e)
        {
        }


        private void TransformWithNeo(string markdown)
        {
            Counter(() =>
            {

                Viewer.Markdown = markdown;
                return "Neo.Markdig.Xaml";
            });
        }

        public static void Counter(Func<string> action)
        {
            var dog = new Stopwatch();
            dog.Start();
            var name = action?.Invoke();
            dog.Stop();Debug.WriteLine($"{name} cost : {dog.ElapsedMilliseconds}ms");
        }

        private void TransformWithMarkdigWPF(string markdown)
        {
            Counter(() =>
            {
                var xaml = Markdig.Wpf.Markdown.ToFlowDocument(markdown, BuildPipeline());
                Viewer1.Document = xaml;
                return "Markdig.Wpf";
            });
        }

        private static MarkdownPipeline BuildPipeline()
        {
            return new MarkdownPipelineBuilder()
                .UseSupportedExtensions()
                .Build();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void OpenHyperlink(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Process.Start(e.Parameter.ToString());
        }

        class MyXamlSchemaContext : XamlSchemaContext
        {
            public override bool TryGetCompatibleXamlNamespace(string xamlNamespace, out string compatibleNamespace)
            {
                if (xamlNamespace.Equals("clr-namespace:Markdig.Wpf", StringComparison.Ordinal))
                {
                    compatibleNamespace = $"clr-namespace:Markdig.Wpf;assembly={Assembly.GetAssembly(typeof(Markdig.Wpf.Styles)).FullName}";
                    return true;
                }
                return base.TryGetCompatibleXamlNamespace(xamlNamespace, out compatibleNamespace);
            }
        }
    }
}

