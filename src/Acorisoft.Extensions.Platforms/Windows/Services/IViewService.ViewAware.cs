using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;

namespace Acorisoft.Extensions.Platforms.Windows.Services
{
    public partial class ViewService
    {
        private Subject<IQuickViewModel> _quickView;
        private Subject<IQuickViewModel> _toolView;
        private Subject<IQuickViewModel> _extraView;
        private Subject<IQuickViewModel> _contextView;
        private Subject<IPageViewModel> _page;
        private Stack<IPageViewModel> _pageLastStack;
        private IPageViewModel _current;

        private void InitializeViewAware()
        {
            _pageLastStack = new Stack<IPageViewModel>();
            _page = new Subject<IPageViewModel>();
            _quickView = new Subject<IQuickViewModel>();
            _toolView = new Subject<IQuickViewModel>();
            _extraView = new Subject<IQuickViewModel>();
            _contextView = new Subject<IQuickViewModel>();
        }
        
        public void NavigateTo(IPageViewModel page)
        {
            OnNavigateTo(page, false);
        }

        private void OnNavigateTo(IPageViewModel page, bool isGoBack)
        {
            if (page is null)
            {
                return;
            }

            if (!isGoBack && _current != null)
            {
                
                //
                // 页面栈
                _pageLastStack.Push(_current);
            }
            _page.OnNext(page);
            _current = page;
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

        public bool CanGoBack()
        {
            return _pageLastStack.Count > 0;
        }

        public void GoBack()
        {
            if (CanGoBack())
            {
                OnNavigateTo(_pageLastStack.Pop(), true);
            }
        }

        public IObservable<IPageViewModel> Page => _page;
        public IObservable<IQuickViewModel> QuickView => _quickView;

        public IObservable<IQuickViewModel> ToolView => _toolView;

        public IObservable<IQuickViewModel> ContextView => _contextView;

        public IObservable<IQuickViewModel> ExtraView => _extraView;
    }
}