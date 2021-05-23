using System;
using Acorisoft.Studio.ViewModels;
using ImTools;
using Splat;

namespace Acorisoft.Studio
{
    public static class ViewModelLocator
    {
        private static readonly Lazy<AppViewModel> LazyInstance = new Lazy<AppViewModel>(() => Locator.Current.GetService<AppViewModel>());

        public static AppViewModel AppViewModel => LazyInstance.Value;
    }
}