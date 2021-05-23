using System;
using System.Reactive.Subjects;
using Acorisoft.Extensions.Platforms.Windows.Controls;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;

namespace Acorisoft.Extensions.Platforms.Services
{
    public partial class ViewService
    {
        private IDisposable _toastDisposable;
        private Subject<IToastViewModel> _toastChanged;

        private void InitializeToast()
        {
            _toastChanged = new Subject<IToastViewModel>();
        }

        private void DisposeToast()
        {
            _toastDisposable?.Dispose();
        }

        public void Toast(string content)
        {
            Toast(content, null, TimeSpan.FromMilliseconds(1500));
        }
        
        public void Toast(string content, object icon)
        {
            Toast(content, icon, TimeSpan.FromMilliseconds(1500));
        }

        public void Toast(string content, object icon, TimeSpan duration)
        {
            var viewModel = new ToastViewModel(duration)
            {
                Icon = icon,
                Title = content
            };

            _toastChanged.OnNext(viewModel);
        }

        public void SetToast(IToastHostCore toastHostCore)
        {
            DisposeToast();

            _toastDisposable = toastHostCore.SubscribeToastPushing(_toastChanged);
        }

        public IObservable<IToastViewModel> ToastChanged => _toastChanged;
    }
}