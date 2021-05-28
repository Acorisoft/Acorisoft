using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Acorisoft.Extensions.Platforms.Windows.Controls
{
    public class PaginationButton : RadioButton
    {
        public static readonly DependencyProperty PageProperty = DependencyProperty.Register(
            "Page", typeof(int), typeof(PaginationButton), new PropertyMetadata(default(int)));

        public int Page
        {
            get => (int) GetValue(PageProperty);
            set => SetValue(PageProperty, value);
        }
    }

    public class Pagination : Control, IPagination
    {
        public const string PanelName = "PART_ItemsPanel";
        public const string FirstButtonName = "PART_First";
        public const string LastButtonName = "PART_Last";
        public const string NextButtonName = "PART_Next";
        public const string PreviousButtonName = "PART_Previous";
        public const string NextPageButtonName = "PART_NextPage";
        public const string LastPageButtonName = "PART_LastPage";
        public const string GotoTextBoxName = "PART_Goto";

        // ReSharper disable once InconsistentNaming
        private StackPanel PART_ItemsPanel;

        // ReSharper disable once InconsistentNaming
        private PaginationButton PART_Next;

        // ReSharper disable once InconsistentNaming
        private PaginationButton PART_NextPage;

        // ReSharper disable once InconsistentNaming
        private PaginationButton PART_Previous;

        // ReSharper disable once InconsistentNaming
        private PaginationButton PART_LastPage;

        // ReSharper disable once InconsistentNaming
        private PaginationButton PART_First;

        // ReSharper disable once InconsistentNaming
        private PaginationButton PART_Last;

        // ReSharper disable once InconsistentNaming
        private TextBox PART_Goto;


        private static void OnPageIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Pagination pagination ||
                e.NewValue is not int value ||
                value is <= 0 or >= int.MaxValue)
            {
                return;
            }

            pagination.OnGeneratePaginationButton();

            if (value == 1)
            {
                if (pagination.PART_Last == null)
                {
                    return;
                }

                pagination.PART_Previous.IsEnabled = false;
                pagination.PART_LastPage.IsEnabled = false;
            }
            else
            {
                if (pagination.PART_Last == null)
                {
                    return;
                }

                pagination.PART_Previous.IsEnabled = true;
                pagination.PART_LastPage.IsEnabled = true;
            }

            if (value == pagination.PageCount)
            {
                if (pagination.PART_Next == null)
                {
                    return;
                }

                pagination.PART_Next.IsEnabled = false;
                pagination.PART_NextPage.IsEnabled = false;
            }
            else
            {
                if (pagination.PART_Last == null)
                {
                    return;
                }

                pagination.PART_Next.IsEnabled = true;
                pagination.PART_NextPage.IsEnabled = true;
            }
        }

        private static void OnPageCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Pagination pagination ||
                e.NewValue is not int value)
            {
                return;
            }

            pagination.OnGeneratePaginationButton();

            if (value < 2)
            {
                if (pagination.PART_Last == null)
                {
                    return;
                }

                pagination.PART_Previous.IsEnabled = false;
                pagination.PART_LastPage.IsEnabled = false;
            }
            else
            {
                if (pagination.PART_Last == null)
                {
                    return;
                }

                pagination.PART_Previous.IsEnabled = true;
                pagination.PART_LastPage.IsEnabled = true;
            }

            if (value == pagination.PageCount || value == 0)
            {
                if (pagination.PART_Next == null)
                {
                    return;
                }

                pagination.PART_Next.IsEnabled = false;
                pagination.PART_NextPage.IsEnabled = false;
            }
            else
            {
                if (pagination.PART_Last == null)
                {
                    return;
                }

                pagination.PART_Next.IsEnabled = true;
                pagination.PART_NextPage.IsEnabled = true;
            }
        }

        public static readonly DependencyProperty PageCountProperty = DependencyProperty.Register(
            "PageCount", typeof(int), typeof(Pagination), new PropertyMetadata(default(int), OnPageCountChanged));

        public static readonly DependencyProperty PageIndexProperty = DependencyProperty.Register(
            "PageIndex", typeof(int), typeof(Pagination), new PropertyMetadata(default(int), OnPageIndexChanged));


        public static readonly DependencyProperty GenerateCountProperty = DependencyProperty.Register(
            "GenerateCount", typeof(int), typeof(Pagination), new PropertyMetadata(default(int)));

        public static readonly DependencyProperty ShowLastButtonProperty = DependencyProperty.Register(
            "ShowLastButton", typeof(bool), typeof(Pagination), new PropertyMetadata(Xaml.False));

        public static readonly DependencyProperty ShowFirstButtonProperty = DependencyProperty.Register(
            "ShowFirstButton", typeof(bool), typeof(Pagination), new PropertyMetadata(Xaml.False));

        public static readonly DependencyProperty ShowGotoButtonProperty = DependencyProperty.Register(
            "ShowGotoButton", typeof(bool), typeof(Pagination), new PropertyMetadata(Xaml.False));

        public static readonly DependencyProperty ShowNextPageButtonProperty = DependencyProperty.Register(
            "ShowNextPageButton", typeof(bool), typeof(Pagination), new PropertyMetadata(Xaml.False));

        public static readonly DependencyProperty ShowLastPageButtonProperty = DependencyProperty.Register(
            "ShowLastPageButton", typeof(bool), typeof(Pagination), new PropertyMetadata(Xaml.False));


        public override void OnApplyTemplate()
        {
            PART_Last = (PaginationButton) GetTemplateChild(LastButtonName);
            PART_LastPage = (PaginationButton) GetTemplateChild(LastPageButtonName);
            PART_First = (PaginationButton) GetTemplateChild(FirstButtonName);
            PART_Next = (PaginationButton) GetTemplateChild(NextButtonName);
            PART_NextPage = (PaginationButton) GetTemplateChild(NextPageButtonName);
            PART_Previous = (PaginationButton) GetTemplateChild(PreviousButtonName);
            PART_ItemsPanel = (StackPanel) GetTemplateChild(PanelName);
            PART_Goto = (TextBox) GetTemplateChild(GotoTextBoxName);

            //
            // 
            PART_Last.Checked += OnLastClick;
            PART_LastPage.Checked += OnLastPageClick;
            PART_Next.Checked += OnNextClick;
            PART_NextPage.Checked += OnNextPageClick;
            PART_First.Checked += OnFirstClick;
            PART_Previous.Checked += OnPreviousClick;
            PART_Goto.KeyDown += OnGotoButtonKeyDown;

            OnGeneratePaginationButton();
        }

        #region EventHandlers

        private void OnFirstClick(object sender, RoutedEventArgs e)
        {
            if (PageCount == 0)
            {
                return;
            }

            PageIndex = 1;
        }

        private void OnLastClick(object sender, RoutedEventArgs e)
        {
            if (PageCount == 0)
            {
                return;
            }

            PageIndex = PageCount;
        }

        private void OnLastPageClick(object sender, RoutedEventArgs e)
        {
            if (PageCount == 0)
            {
                return;
            }

            if (PageIndex == 1)
            {
                return;
            }

            PageIndex -= Math.Clamp(PageIndex - GenerateCount, 0, GenerateCount);
        }

        private void OnNextClick(object sender, RoutedEventArgs e)
        {
            if (PageCount == 0)
            {
                return;
            }

            if (PageIndex == PageCount)
            {
                return;
            }

            PageIndex += 1;
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            if (PageCount == 0)
            {
                return;
            }

            if (PageIndex == 1)
            {
                return;
            }

            PageIndex += Math.Clamp(PageCount - PageIndex, 0, GenerateCount);
        }

        private void OnPreviousClick(object sender, RoutedEventArgs e)
        {
            if (PageCount == 0)
            {
                return;
            }

            if (PageIndex == 0)
            {
                return;
            }

            PageIndex -= 1;
        }

        private void OnGotoButtonKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter && e.SystemKey != Key.Enter)
            {
                return;
            }

            var value = MathExtensions.GetNumber(((TextBox) sender).Text);
            if (value == 0)
            {
                return;
            }

            PageIndex = value;
        }

        #endregion


        public bool ShowLastPageButton
        {
            get => (bool) GetValue(ShowLastPageButtonProperty);
            set => SetValue(ShowLastPageButtonProperty, value);
        }

        public bool ShowNextPageButton
        {
            get => (bool) GetValue(ShowNextPageButtonProperty);
            set => SetValue(ShowNextPageButtonProperty, value);
        }

        public bool ShowGotoButton
        {
            get => (bool) GetValue(ShowGotoButtonProperty);
            set => SetValue(ShowGotoButtonProperty, Xaml.Box(value));
        }

        public bool ShowFirstButton
        {
            get => (bool) GetValue(ShowFirstButtonProperty);
            set => SetValue(ShowFirstButtonProperty, Xaml.Box(value));
        }

        public bool ShowLastButton
        {
            get => (bool) GetValue(ShowLastButtonProperty);
            set => SetValue(ShowLastButtonProperty, Xaml.Box(value));
        }

        public int GenerateCount
        {
            get => (int) GetValue(GenerateCountProperty);
            set => SetValue(GenerateCountProperty, value);
        }

        public int PageIndex
        {
            get => (int) GetValue(PageIndexProperty);
            set => SetValue(PageIndexProperty, value);
        }

        public int PageCount
        {
            get => (int) GetValue(PageCountProperty);
            set => SetValue(PageCountProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnGeneratePaginationButton()
        {
            if (PART_ItemsPanel == null)
            {
                Debug.WriteLine("无法生成分页子项，面板为空");
                return;
            }

            if (PageCount == 0)
            {
                return;
            }

            if (GenerateCount == 0)
            {
                return;
            }

            foreach (PaginationButton button in PART_ItemsPanel.Children)
            {
                button.Click -= OnPaginationButtonClick;
            }

            //
            // 避免生成超出范围的值。
            var actualGenerateCount = Math.Clamp(PageCount - PageIndex + 1, 0, GenerateCount);
            Debug.WriteLine(actualGenerateCount);
            //
            // 清空所有子项
            PART_ItemsPanel.Children.Clear();

            for (int i = 0, n = PageIndex; i < actualGenerateCount; i++, n++)
            {
                var button = new PaginationButton {Page = n};
                button.Click += OnPaginationButtonClick;
                PART_ItemsPanel.Children.Add(button);
            }
        }

        private void OnPaginationButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is PaginationButton button && PageCount > 0 && button.Page < PageCount)
            {
                PageIndex = button.Page;
            }
        }
    }
}