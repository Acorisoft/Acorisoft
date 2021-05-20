using System;
using System.Windows;
using Acorisoft.Extensions.Windows.Platforms;
using Acorisoft.Extensions.Windows.ViewModels;
using ImTools;
using ReactiveUI;

namespace Acorisoft.Extensions.Windows.Views
{
    public class SpaPage<TViewModel> : ReactiveUserControl<TViewModel> where TViewModel : PageViewModel, IPageViewModel
    {
        protected SpaPage()
        {
            this.Loaded += OnLoadedCore;
            this.Unloaded += OnUnloadedCore;
            this.DataContextChanged+= OnDataContextChanged;
            this.WhenActivated(d => d(this.WhenAnyValue(x => x.ViewModel).Do(OnDataContextHasValue).BindTo(this, x => x.DataContext)));
            DataContextProperty.OverrideMetadata(typeof(InteractiveWindow),new PropertyMetadata(null,OnDataContextChanged));
        }

        #region DataContextChanged
        private void OnDataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is IViewModelLifeCycle oldImpl)
            {
                oldImpl.Stop();
            }

            if (e.NewValue is IViewModelLifeCycle newImpl)
            {
                newImpl.Start();
            }
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
            this.Loaded -= OnLoadedCore;
            this.Unloaded -= OnUnloadedCore;
            this.DataContextChanged -= OnDataContextChanged;
            OnUnloaded(sender, e);
        }

        private void OnLoadedCore(object sender, RoutedEventArgs e)
        {
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