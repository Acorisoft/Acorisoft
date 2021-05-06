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

namespace Acorisoft.Frameworks.Controls
{
    /// <summary>
    /// <see cref="ToggleTextEditor"/> 表示一个仅有启用编辑模式才可编辑的文本编辑框控件。
    /// </summary>
    public class ToggleTextEditor : TextBox
    {
        static ToggleTextEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleTextEditor), new FrameworkPropertyMetadata(typeof(ToggleTextEditor)));
            IsEditModePropertyKey = DependencyProperty.RegisterReadOnly(
            "IsEditMode",
            typeof(bool),
            typeof(ToggleTextEditor),
            new PropertyMetadata(BooleanBoxes.Boxes(false)));
            IsEditModeProperty = IsEditModePropertyKey.DependencyProperty;
        }

        public const string PART_IconButtonName = "PART_IconButton";
        public const string PART_EditName = "PART_Edit";

        private Border  PART_IconButton;
        private TextBox PART_Edit;
        private bool _IsClicked;

        public ToggleTextEditor()
        {
            this.Unloaded += OnUnloaded;
        }

        protected virtual void OnUnloaded(object sender, RoutedEventArgs e)
        {
            PART_IconButton.MouseDown -= OnMouseDown;
            PART_IconButton.MouseUp -= OnMouseUp;
        }

        public override void OnApplyTemplate()
        {
            PART_IconButton = (Border)GetTemplateChild(PART_IconButtonName);
            PART_IconButton.MouseDown += OnMouseDown;
            PART_IconButton.MouseUp += OnMouseUp;
            PART_Edit = (TextBox)GetTemplateChild(PART_EditName);
            PART_Edit.KeyUp += OnKeyUp;
            base.OnApplyTemplate();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter || e.SystemKey == Key.Enter)
            {
                IsEditMode = false;
                _IsClicked = false;
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var changed = !IsEditMode;

            if (_IsClicked)
            {
                IsEditMode = changed;

                if (changed)
                {
                    PART_Edit.Focus();
                }
            }

            _IsClicked = false;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _IsClicked = true;
        }

        public object EditModeIcon
        {
            get => (object)GetValue(EditModeIconProperty);
            set => SetValue(EditModeIconProperty, value);
        }

        public object LockModeIcon
        {
            get => (object)GetValue(LockModeIconProperty);
            set => SetValue(LockModeIconProperty, value);
        }


        public bool IsEditMode
        {
            get => (bool)GetValue(IsEditModeProperty);
            private set => SetValue(IsEditModePropertyKey, value);
        }

        public static readonly DependencyPropertyKey IsEditModePropertyKey;
        public static readonly DependencyProperty IsEditModeProperty;


        public static readonly DependencyProperty LockModeIconProperty = DependencyProperty.Register(
            "LockModeIcon",
            typeof(object),
            typeof(ToggleTextEditor),
            new PropertyMetadata(null));

        public static readonly DependencyProperty EditModeIconProperty = DependencyProperty.Register(
            "EditModeIcon",
            typeof(object),
            typeof(ToggleTextEditor),
            new PropertyMetadata(null));

    }
}
