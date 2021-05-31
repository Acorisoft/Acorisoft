using System;
using System.Diagnostics.Contracts;
using System.Windows.Input;
using Acorisoft.Extensions.Platforms.Windows;
using Acorisoft.Extensions.Platforms.Windows.Services;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.ProjectSystem;
using Microsoft.Win32;
using ReactiveUI;

namespace Acorisoft.Studio.ViewModels
{
    public class HomeContextViewModel : QuickViewModelBase
    {
        public HomeContextViewModel()
        {
            PickCoverCommand = ReactiveCommand.Create(async () => OnPickingCoverOperation());
        }

        async void OnPickingCoverOperation()
        {
            var opendlg = new OpenFileDialog
            {
                Filter = "图片文件|*.bmp;*.png;*.jpg"
            };

            if (opendlg.ShowDialog() != true)
            {
                return;
            }

            using (ViewAware.ForceBusyState("正在加载封面"))
            {
                
            }
        }
        
        protected override void OnStart(IPageViewModel currentViewModel)
        {
            Contract.Assert(currentViewModel is HomeViewModel);
            Ancestor = currentViewModel;
            Parent = Ancestor as HomeViewModel;
        }
        
        public ICommand PickCoverCommand { get; }
        public IPageViewModel Ancestor { get; private set; }
        public HomeViewModel Parent { get; private set; }



    }
}