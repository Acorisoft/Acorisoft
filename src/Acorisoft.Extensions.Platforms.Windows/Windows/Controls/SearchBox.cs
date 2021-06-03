using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Acorisoft.Extensions.Platforms.Windows.Controls.Buttons;

namespace Acorisoft.Extensions.Platforms.Windows.Controls
{
    [TemplatePart(Name = SearchButtonName, Type = typeof(SymbolContentButton))]
    [TemplatePart(Name = ClearButtonName, Type = typeof(SymbolContentButton))]
    [TemplatePart(Name = TextBoxName, Type = typeof(TextBox))]
    public class SearchBox : TextBox
    {
        public const string SearchButtonName = "PART_Search";
        public const string ClearButtonName = "PART_Clear";
        public const string TextBoxName = "PART_Text";
        private SymbolButton PART_Serach;
        private SymbolButton PART_Clear;
        private TextBox PART_Text;

        public SearchBox()
        {
            this.Unloaded += OnUnloaded; 
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            PART_Text.KeyUp -= OnKeyUp;
            PART_Text.TextChanged -= OnTextChanged;
            PART_Serach.Click -= OnSearchClick;
            PART_Clear.Click -= OnClearClick;
        }

        public override void OnApplyTemplate()
        {
            PART_Serach = (SymbolButton) GetTemplateChild(SearchButtonName);
            PART_Clear = (SymbolButton) GetTemplateChild(ClearButtonName);
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
            RaiseEvent(new RoutedEventArgs
            {
                RoutedEvent = TextCompletedEvent
            });
        }
        
        private void OnClearClick(object sender, RoutedEventArgs e)
        {
            SetValue(TextProperty, string.Empty);
            RaiseEvent(new RoutedEventArgs
            {
                RoutedEvent = TextClearEvent
            });
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

        public static readonly RoutedEvent TextClearEvent = EventManager.RegisterRoutedEvent("TextClear",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SearchBox));
        public static readonly RoutedEvent TextCompletedEvent = EventManager.RegisterRoutedEvent("TextCompleted",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SearchBox));
        
        public event  RoutedEventHandler TextCompleted
        {
            add => AddHandler(TextCompletedEvent, value);
            remove => RemoveHandler(TextCompletedEvent, value);
        }
        public event  RoutedEventHandler TextClear
        {
            add => AddHandler(TextClearEvent, value);
            remove => RemoveHandler(TextClearEvent, value);
        }
    }
}