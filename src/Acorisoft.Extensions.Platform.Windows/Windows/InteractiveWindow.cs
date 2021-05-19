using System;
using System.Windows;
using Acorisoft.Extensions.Windows.Platforms;
using ReactiveUI;
using Splat;

namespace Acorisoft.Extensions.Windows
{
    public abstract class InteractiveWindow : Window
    {
        protected InteractiveWindow()
        {
            this.Loaded += OnLoadedCore;
            this.Unloaded += OnUnloadedCore;
        }

        #region Loaded & Unloaded

        private void OnUnloadedCore(object sender, RoutedEventArgs e)
        {
            Platform.ViewService.Navigating -= OnNavigating;
            OnUnloaded(sender, e);
        }

        private void OnLoadedCore(object sender, RoutedEventArgs e)
        {
            Platform.ViewService.Navigating += OnNavigating;
            OnLoaded(sender, e);
        }

        protected virtual void OnUnloaded(object sender, RoutedEventArgs e)
        {
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        #endregion
        
        #region ViewService Impl

        protected virtual void OnNavigating(object sender, NavigateToViewEventArgs e)
        {
        }

        #endregion

    }
}