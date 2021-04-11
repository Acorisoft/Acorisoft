using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.ViewModels
{
    /// <summary>
    /// <see cref="IAppViewModel"/>
    /// </summary>
    public interface IAppViewModel : IScreen, IViewModel, IViewManager
    {
        /// <summary>
        /// 获取当前正在活跃的视图模型。
        /// </summary>
        IViewModel ViewModel { get; }

        /// <summary>
        /// 获取当前上一个的视图模型。
        /// </summary>
        IViewModel LastViewModel { get; }
    }
}
