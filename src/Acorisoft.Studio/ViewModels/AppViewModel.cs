using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Acorisoft.Extensions.Platforms.Services;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using LiteDB;
using ReactiveUI;

namespace Acorisoft.Studio.ViewModels
{
    public class AppViewModel : AppViewModelBase
    {
        public AppViewModel(IViewService service) : base(service)
        {
            
        }
    }
}