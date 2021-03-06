using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Views
{
    public class NavigationEventArgs : EventArgs
    {
        private IRoutableViewModel _result;

        public NavigationEventArgs(IRoutableViewModel oldVM, IRoutableViewModel newVM,IFullLogger logger)
        {
            Old = oldVM;
            New = newVM ?? throw new ArgumentNullException(nameof(newVM));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Result = New;
        }

        /// <summary>
        /// 获取当前过滤器的日志记录工具。
        /// </summary>
        public IFullLogger Logger { get; private set; }

        /// <summary>
        /// 获取当前导航事件的旧视图模型。
        /// </summary>
        public IRoutableViewModel Old { get; }

        /// <summary>
        /// 获取当前导航事件的旧视图模型。
        /// </summary>
        public IRoutableViewModel New { get; }

        /// <summary>
        /// 获取或设置当前导航过滤事件的最终导航结果，该属性由管线过滤器设置。
        /// </summary>
        public IRoutableViewModel Result { get; set; }
    }
}
