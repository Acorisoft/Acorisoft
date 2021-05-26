using System;
using System.Diagnostics.Contracts;
using System.Windows.Input;
using Acorisoft.Extensions.Platforms.Windows.Services;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Documents.ProjectSystem;
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
            
            ServiceLocator.ViewService.ManualStartBusyState("正在加载封面");
            var targetName = await ServiceLocator.FileManagerService.UploadImage(opendlg.FileName);
            ServiceLocator.ViewService.ManualEndBusyState();
            CompositionSet.Property.Cover = targetName;
            await ServiceLocator.CompositionSetManager
                                .PropertyManager
                                .SetProperty(CompositionSet.Property);
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
        public ICompositionSet CompositionSet => Parent.CompositionSet;

        public Uri Cover => CompositionSet.Property.Cover;

        public string Name
        {
            get => CompositionSet.Property.Name;
            set
            {
                CompositionSet.Property.Name = value;
                RaiseUpdated();
                ServiceLocator.CompositionSetManager
                              .PropertyManager
                              .SetProperty(CompositionSet.Property);
            }
            
        }
        
        public string Summary
        {
            get => CompositionSet.Property.Summary;
            set
            {
                CompositionSet.Property.Summary = value;
                RaiseUpdated();
                ServiceLocator.CompositionSetManager
                              .PropertyManager
                              .SetProperty(CompositionSet.Property);
            }
        }
    }
}