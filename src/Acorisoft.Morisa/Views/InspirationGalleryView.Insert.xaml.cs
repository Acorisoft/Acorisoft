﻿using Acorisoft.Morisa.ViewModels;
using ReactiveUI;
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

namespace Acorisoft.Morisa.Views
{
    /// <summary>
    /// InspirationGalleryView.xaml 的交互逻辑
    /// </summary>
    [ViewModel(typeof(InspirationGalleryInsertViewModel))]
    public partial class InspirationGalleryInsertView : ReactiveUserControl<InspirationGalleryInsertViewModel>
    {
        public InspirationGalleryInsertView()
        {
            InitializeComponent();
            this.WhenActivated(d =>
            {
                d(this.WhenAnyValue(x => x.ViewModel).BindTo(this, x => x.DataContext));
            });
        }

        private void OnPreviewQueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {

        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            var data = e.Data.GetData(DataFormats.UnicodeText);

        }
    }
}
