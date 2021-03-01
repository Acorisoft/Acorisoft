using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Acorisoft.Morisa.Panels
{
    /// <summary>
    /// 瀑布流布局，等列宽,原作者地址：https://blog.csdn.net/xubright/article/details/89881847
    /// </summary>
    public class WaterfallPanel : Panel
    {
        public WaterfallPanel()
        {
        }

        /// <summary>
        /// 列数
        /// </summary>
        public int ColumnCount {
            get { return (int)this.GetValue(ColumnCountProperty); }
            set { this.SetValue(ColumnCountProperty, value); }
        }

        public static readonly DependencyProperty ColumnCountProperty = DependencyProperty.Register("ColumnCount", typeof(int), typeof(WaterfallPanel), new PropertyMetadata(PropertyChanged));

        public static void PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender == null || e.NewValue == e.OldValue)
                return;
            sender.SetValue(ColumnCountProperty, e.NewValue);

        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (Children.Count <= 0)
                return new Size(0, 0);

            #region 测量值
            //Canvase、ScrollView等会给出无穷宽高
            Size childrenAvailableSize = new Size(double.IsPositiveInfinity(availableSize.Width) ? double.PositiveInfinity : availableSize.Width / ColumnCount, double.PositiveInfinity);
            Size[] childrenDesiredSizes = new Size[Children.Count];

            double[] columnH = new double[ColumnCount];
            double[] columnW = new double[ColumnCount];//列最宽元素的宽度
            for (int i = 0; i < Children.Count; i++)
            {

                int index = 0;//那一列目前高度最低
                for (int j = 1; j < ColumnCount; j++)
                {
                    if (columnH[j] < columnH[index])
                        index = j;
                }
                Children[i].Measure(childrenAvailableSize);
                childrenDesiredSizes[i] = Children[i].DesiredSize;
                columnH[index] += childrenDesiredSizes[i].Height;
                if (columnW[index] < childrenDesiredSizes[i].Width)
                    columnW[index] = childrenDesiredSizes[i].Width;
            }
            #endregion 测量值

            //返回尺寸
            Size resultSize = new Size()
            {
                Height = columnH.Max(),
                Width = columnW.Max() * ColumnCount//等宽
            };

            resultSize.Width = double.IsPositiveInfinity(availableSize.Width) ? resultSize.Width : availableSize.Width;

            resultSize.Height = double.IsPositiveInfinity(availableSize.Height) ? resultSize.Height : availableSize.Height;

            return resultSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Children.Count <= 0)
                return new Size(0, 0);

            #region 测量值
            Size childrenFinalSize = new Size(finalSize.Width / ColumnCount, finalSize.Height);

            double[] columnH = new double[ColumnCount];
            for (int i = 0; i < Children.Count; i++)
            {
                int index = 0;//那一列目前高度最低
                for (int j = 1; j < ColumnCount; j++)
                {
                    if (columnH[j] < columnH[index])
                        index = j;
                }
                Children[i].Arrange(new Rect(childrenFinalSize.Width * index, columnH[index], childrenFinalSize.Width, Children[i].DesiredSize.Height));
                 columnH[index] += Children[i].DesiredSize.Height;
            }
            #endregion 测量值

            return finalSize;
        }
    }
}
