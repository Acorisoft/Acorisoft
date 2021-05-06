using Markdig;
using Markdig.Wpf;
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
using Markdown = Markdig.Wpf.Markdown;

namespace Pandora.Host
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Editor_TextChanged(object sender, EventArgs e)
        {
            //    var pipeline = new MarkdownPipelineBuilder().Build();
            //    var markdown = MarkdownXaml.ToMarkdownDocument(Editor.Text,pipeline);
            //    var document = MarkdownXaml.ToFlowDocument(markdown, pipeline);
            Renderer.Markdown = Editor.Text;
        }
    }
}
