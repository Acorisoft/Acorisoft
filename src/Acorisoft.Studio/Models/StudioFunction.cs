using System;
using Acorisoft.Extensions.Platforms.ComponentModel;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.ViewModels;
using Splat;

namespace Acorisoft.Studio.Models
{
    /// <summary>
    /// <see cref="StudioFunction"/> 表示一个功能入口。
    /// </summary>
    public class StudioFunction : ObservableObject
    {
        public virtual IPageViewModel GotoFunction()
        {
            return null;
        }
    }

    /// <summary>
    /// <see cref="InspirationFunction"/> 表示一个灵感集入口。
    /// </summary>
    public sealed class InspirationFunction : StudioFunction
    {
        public override IPageViewModel GotoFunction()
        {
            return Locator.Current.GetService<InspirationGalleryViewModel>();
        }
    }
}