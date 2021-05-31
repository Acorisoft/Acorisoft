using System.Windows;
using System.Windows.Forms;
using Acorisoft.Extensions.Platforms.Windows.Controls.Buttons;
using TextBox = System.Windows.Controls.TextBox;

namespace Acorisoft.Extensions.Platforms.Windows.Controls
{
    [TemplatePart(Name = PART_OpenName, Type = typeof(SymbolButton))]
    [TemplatePart(Name = PART_TextName, Type = typeof(TextBox))]
    public class FolderFormField : FormField
    {
        private const string PART_OpenName = "PART_Open";
        private const string PART_TextName = "PART_Text";
        public static readonly DependencyProperty FolderProperty = DependencyProperty.Register(
            "Folder", typeof(string), typeof(FolderFormField), new PropertyMetadata(default(string)));

        private SymbolButton PART_Open;
        
        public FolderFormField()
        {
            this.Unloaded += OnUnloaded;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (PART_Open != null)
            {
                PART_Open.Click -= OnClick;
            }
        }

        public override void OnApplyTemplate()
        {
            PART_Open = (SymbolButton) GetTemplateChild(PART_OpenName);
            PART_Open.Click += OnClick;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var opendlg = new FolderBrowserDialog();
            if (opendlg.ShowDialog() == DialogResult.OK)
            {
                Folder = opendlg.SelectedPath;
            }
        }

        public string Folder
        {
            get => (string) GetValue(FolderProperty);
            set => SetValue(FolderProperty, value);
        }
    }
}