﻿using System;
using System.Windows;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using ImTools;
using ReactiveUI;

namespace Acorisoft.Extensions.Platforms.Windows.Views
{
    public class QuickPage<TViewModel> : ReactiveUserControl<TViewModel> where TViewModel : class, IRoutableViewModel, IQuickViewModel
    {

        protected QuickPage()
        {
            this.Loaded += OnLoadedImpl;
            this.Unloaded += OnUnloadedCore;
            this.DataContextChanged+= OnDataContextChanged;
            this.WhenActivated(d => d(this.WhenAnyValue(x => x.ViewModel).Do(OnDataContextHasValue).BindTo(this, x => x.DataContext)));
        }

        #region DataContextChanged
        private static void OnDataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        protected virtual void OnDataContextHasValue(IObservable<TViewModel?> stream)
        {
            
        }
        
        protected virtual void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }
        
        #endregion

        #region Loaded & Unloaded


        private void OnUnloadedCore(object sender, RoutedEventArgs e)
        {
            this.Loaded -= OnLoadedImpl;
            this.Unloaded -= OnUnloadedCore;
            this.DataContextChanged -= OnDataContextChanged;
            ViewModel?.Stop();
            OnUnloaded(sender, e);
        }

        private void OnLoadedImpl(object sender, RoutedEventArgs e)
        {
            ViewModel?.Start();
            OnLoaded(sender, e);
        }

        protected virtual void OnUnloaded(object sender, RoutedEventArgs e)
        {
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        #endregion
    }
}