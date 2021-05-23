using System;
using System.Reactive.Subjects;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;

namespace Acorisoft.Extensions.Platforms.Services
{
    public partial class ViewService
    {
        private Subject<IQuickViewModel> _quickView;
        private Subject<IQuickViewModel> _toolView;
        private Subject<IQuickViewModel> _extraView;
        private Subject<IQuickViewModel> _contextView;
        private Subject<IPageViewModel> _page;

        private void InitializeViewAware()
        {
            _page = new Subject<IPageViewModel>();
            _quickView = new Subject<IQuickViewModel>();
            _toolView = new Subject<IQuickViewModel>();
            _extraView = new Subject<IQuickViewModel>();
            _contextView = new Subject<IQuickViewModel>();
        }
        
        public void NavigateTo(IPageViewModel page)
        {
            if (page is null)
            {
                return;
            }
            
            _page.OnNext(page);
        }

        public void NavigateTo(IQuickViewModel quickView, IQuickViewModel toolView, IQuickViewModel contextView,
            IQuickViewModel extraView)
        {
            if (quickView is not null)
            {
                _quickView.OnNext(quickView);
            }
            
            if (toolView is not null)
            {
                _toolView.OnNext(toolView);
            }
            
            if (contextView is not null)
            {
                _contextView.OnNext(contextView);
            }
            
            if (extraView is not null)
            {
                _extraView.OnNext(extraView);
            }
        }

        public IObservable<IPageViewModel> Page => _page;
        public IObservable<IQuickViewModel> QuickView => _quickView;

        public IObservable<IQuickViewModel> ToolView => _toolView;

        public IObservable<IQuickViewModel> ContextView => _contextView;

        public IObservable<IQuickViewModel> ExtraView => _extraView;
    }
}