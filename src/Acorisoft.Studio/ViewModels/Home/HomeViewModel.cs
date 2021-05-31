using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Joins;
using ReactiveUI;
using System.Reactive.Disposables;
using Acorisoft.Extensions.Platforms.Services;
using Acorisoft.Extensions.Platforms.Windows;
using Acorisoft.Extensions.Platforms.Windows.Services;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.ProjectSystem;
using Acorisoft.Studio.ProjectSystems;
using ReactiveUI;

namespace Acorisoft.Studio.ViewModels
{
    public class HomeViewModel : PageViewModelBase
    {
        private readonly IComposeSetSystem _system;
        private readonly ObservableAsPropertyHelper<bool> _isOpen;
        public HomeViewModel(IComposeSetSystem system)
        {
            _system = system;
            _system.IsOpen.ToProperty(this, nameof(IsOpen));
        }

        public bool IsOpen => _isOpen.Value;
    }
}