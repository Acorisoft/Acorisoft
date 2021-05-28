using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Acorisoft.Extensions.Platforms.Windows.Controls.Buttons;

namespace Acorisoft.Extensions.Platforms.Windows.Controls
{
    [TemplatePart(Name = SearchButtonName, Type = typeof(HighlightSymbolContentButton))]
    [TemplatePart(Name = ClearButtonName, Type = typeof(HighlightSymbolContentButton))]
    [TemplatePart(Name = TextBoxName, Type = typeof(TextBox))]
    public class SearchBox : TextBox
    {
        public const string SearchButtonName = "PART_Search";
        public const string ClearButtonName = "PART_Clear";
        public const string TextBoxName = "PART_Text";
        private HighlightSymbolButton PART_Serach;
        private HighlightSymbolButton PART_Clear;
        private TextBox PART_Text;


        public override void OnApplyTemplate()
        {
            PART_Serach = (HighlightSymbolButton) GetTemplateChild(SearchButtonName);
            PART_Clear = (HighlightSymbolButton) GetTemplateChild(ClearButtonName);
            PART_Text = (TextBox) GetTemplateChild(TextBoxName);
            PART_Text.KeyUp += OnKeyUp;
            PART_Text.TextChanged += OnTextChanged;
            PART_Serach.Click += OnSearchClick;
            PART_Clear.Click += OnClearClick;
            base.OnApplyTemplate();
        }
        private void OnSearchClick(object sender, RoutedEventArgs e)
        {
            SetValue(TextProperty, PART_Text.Text);
        }
        private void OnClearClick(object sender, RoutedEventArgs e)
        {
            SetValue(TextProperty, string.Empty);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            PART_Clear.Visibility = string.IsNullOrEmpty(PART_Text.Text) ? Visibility.Collapsed : Visibility.Visible;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter || e.SystemKey != Key.Enter)
            {
                return;
            }
            
            SetValue(TextProperty, PART_Text.Text);
        }
    }
}