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

    /// <summary>
    /// <see cref="Pagination"/> 表示一个分页控件。
    /// </summary>
    public class Pagination : Control, IPagination
    {
        static Pagination()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pagination),new FrameworkPropertyMetadata(typeof(Pagination)));
            PageIndexProperty = DependencyProperty.Register("PageIndex",
                typeof(int), 
                typeof(Pagination), 
                new PropertyMetadata(default(int),
                    OnPageIndexChanged));

        }
        
        public static readonly DependencyProperty PageIndexProperty ;

        public static readonly DependencyProperty PageItemCountProperty = DependencyProperty.Register(
            "PageItemCount",       
            typeof(int), 
            typeof(Pagination),
            new PropertyMetadata(
                default(int),
                OnPageItemCountChanged,
                OnPageItemCountCoerceChanged));

        public static readonly DependencyProperty PageCountProperty = DependencyProperty.Register("PageCount",
            typeof(int), 
            typeof(Pagination),
            new PropertyMetadata(
                default(int),
                OnPageCountChanged,
                OnPageCountCoerceChanged));

        private static void OnPageCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue is int and (< 0 or > int.MaxValue))
            {
                return;
            }

            //
            //
            var pagination = (Pagination) d;
            
            pagination.PageIndex = 1;
            //
            // 刷新列表
            pagination.GeneratePaginationButton((int) d.GetValue(PageIndexProperty));
            pagination.CheckVisibilityState();
        }
        
        private static void OnPageItemCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue is int and (< 0 or > int.MaxValue))
            {
                return;
            }
            
            
            //
            //
            var pagination = (Pagination) d;
            pagination.PageIndex = 1;
            //
            // 刷新列表
            pagination.GeneratePaginationButton((int) d.GetValue(PageIndexProperty));
            pagination.CheckVisibilityState();
        }
        
        private static void OnPageIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue is int and (< 0 or > int.MaxValue))
            {
                return;
            }
            
            
            //
            //
            var pagination = (Pagination) d;
            
            pagination.CheckVisibilityState();
        }

        private static object OnPageCountCoerceChanged(DependencyObject d,object value)
        {
            return value is int and (< 0 or > int.MaxValue) ? 1 : value;
        }
        
        private static object OnPageItemCountCoerceChanged(DependencyObject d,object value)
        {
            return value is int and (< 0 or > ushort.MaxValue) ? 1 : value;
        }

        private const string PART_HomeName = "PART_Home";
        private const string PART_PreviousName = "PART_Previous";
        private const string PART_NextName = "PART_Next";
        private const string PART_LastName = "PART_Last";
        private const string PART_GotoName = "PART_Goto";
        private const string PART_ItemsPanelName = "PART_ItemsPanel";
        
        private Button PART_Home;
        private Button PART_Previous;
        private Button PART_Next;
        private Button PART_Last;
        private TextBox PART_Goto;
        private StackPanel PART_ItemsPanel;

        public override void OnApplyTemplate()
        {
            PART_Home = (Button) GetTemplateChild(PART_HomeName);
            PART_Previous = (Button) GetTemplateChild(PART_PreviousName);
            PART_Next = (Button) GetTemplateChild(PART_NextName);
            PART_Last = (Button) GetTemplateChild(PART_LastName);
            PART_Goto = (TextBox) GetTemplateChild(PART_GotoName);
            PART_ItemsPanel = (StackPanel)GetTemplateChild(PART_ItemsPanelName);
            PART_Home.Click += NavigateToHome;
            PART_Last.Click += NavigateToLast;
            PART_Next.Click += NextRange;
            PART_Previous.Click += PreviousRange;
            PART_Goto.KeyDown += OnGotoKeyDown;    
            
            base.OnApplyTemplate();
        }

        private void OnGotoKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter && e.SystemKey != Key.Enter)
            {
                return;
            }

            if (!int.TryParse(PART_Goto.Text, out var index))
            {
                return;
            }
            
            if (PageCount == 0)
            {
                return;
            }
            
            //
            // 归一化
            index = Math.Clamp(index, 1, PageCount);
                
            //
            // 判断是否需要重新刷新
            if (index >= PageIndex + PageCount)
            {
                
                //
                // index: 6  maxIndex : 10
                // 
                GeneratePaginationButton(index);
                
                
            }

            for (int i = 0; i < PART_ItemsPanel.Children.Count;i++)
            {
                PaginationButton button = (PaginationButton)PART_ItemsPanel.Children[i];
                if (button.Page == index)
                {
                    button.IsChecked = true;
                }
            }
        }

        protected void NavigateToPage(object sender, RoutedEventArgs e)
        {
            if (sender is not PaginationButton button)
            {
                return;
            }

            if (button.IsChecked != true)
            {
                return;
            }
            
            if (PageCount == 0)
            {
                return;
            }
            
            //
            // 获取并设置指定的页面索引，并保证页面的索引号在[1,PageCount]之间
            PageIndex = Math.Clamp(button.Page, 1, PageCount);
            
            //
            // 判断是否翻页了
            if (PageIndex % PageItemCount == 1 && PageIndex > 1)
            {
                GeneratePaginationButton(PageIndex);
                CheckVisibilityState();
            }
        }

        protected void GeneratePaginationButton(int pageIndex)
        {
            if (PageCount == 0)
            {
                return;
            }

            var count = PageCount;
            
            //
            // analyzed:
            //
            // pageCount is 10, pageItemCount is 5, pageIndex is 6,
            //
            // it will generate 6 7 8 9 10
            //
            // pageCount is 10, pageItemCount is 5, pageIndex is 8,
            //
            // it will generate 8 9 10
            var generateItemCount = Math.Clamp(count - pageIndex + 1, 1 , count);
            
            //
            //
            PART_ItemsPanel.Children.Clear();
            
            //
            // 
            for (int i = pageIndex, n = 0; n < generateItemCount; n++, i++)
            {
                var button = new PaginationButton
                {
                    Page = i
                };

                //
                //
                button.Checked += NavigateToPage;
                
                //
                //
                PART_ItemsPanel.Children.Add(button);

            }
        }
        
        protected void NextRange(object sender, RoutedEventArgs e)
        {
            if (sender is not PaginationButton)
            {
                return;
            }
            if (PageCount == 0)
            {
                return;
            }

            //
            // 获取并设置指定的页面索引，并保证页面的索引号在[1,int.MaxValue]之间
            var index = Math.Clamp(PageIndex + PageItemCount, 1, PageCount);
            
            //
            // 生成按钮
            GeneratePaginationButton(index);
            
            //
            // 设置索引值
            PageIndex = index;
        }

        protected void NavigateToHome(object sender, RoutedEventArgs e)
        {
            if (PageCount == 0)
            {
                return;
            }
            //
            // 生成按钮
            GeneratePaginationButton(1);
            
            //
            // 设置索引值
            PageIndex = 1;
        }
        
        protected void NavigateToLast(object sender, RoutedEventArgs e)
        {
            if (PageCount == 0)
            {
                return;
            }
            //
            // 获取并设置指定的页面索引，并保证页面的索引号在[1,int.MaxValue]之间
            var index = Math.Clamp(PageCount - PageItemCount + 1, 1, PageCount);
            
            //
            // 生成按钮
            GeneratePaginationButton(index);
            
            //
            // 设置索引值
            PageIndex = index;
        }

        protected void PreviousRange(object sender, RoutedEventArgs e)
        {
            if (sender is not PaginationButton)
            {
                return;
            }

            if (PageCount == 0)
            {
                return;
            }
            
            //
            // 获取并设置指定的页面索引，并保证页面的索引号在[1,int.MaxValue]之间
            var index = Math.Clamp(PageIndex - PageItemCount, 1, PageCount);
            
            //
            // 生成按钮
            GeneratePaginationButton(index);
            
            //
            // 设置索引值
            PageIndex = index;
        }

        protected void CheckVisibilityState()
        {
            var index = PageIndex;
            var count = PageCount;
            var itemCount = PageItemCount;

            if (count == 0)
            {
                PART_Home.Visibility=  Visibility.Collapsed;
                PART_Previous.Visibility =  Visibility.Collapsed;
                PART_Last.Visibility =  Visibility.Collapsed;
                PART_Next.Visibility =  Visibility.Collapsed;
                return;
            }
            
            PART_Home.Visibility = index == 1 ? Visibility.Collapsed : Visibility.Visible;
            PART_Previous.Visibility = index == 1? Visibility.Collapsed : Visibility.Visible;
            PART_Last.Visibility = count - index <= itemCount? Visibility.Collapsed : Visibility.Visible;
            PART_Next.Visibility = count - index <= itemCount? Visibility.Collapsed : Visibility.Visible;
        }
        

        public int PageCount
        {
            get => (int) GetValue(PageCountProperty);
            set => SetValue(PageCountProperty, value);
        }
        public int PageItemCount
        {
            get => (int) GetValue(PageItemCountProperty);
            set => SetValue(PageItemCountProperty, value);
        }
        public int PageIndex
        {
            get => (int) GetValue(PageIndexProperty);
            set => SetValue(PageIndexProperty, value);
        }
    }
}