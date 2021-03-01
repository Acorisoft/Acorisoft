using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Routers
{
    /// <summary>
    /// <see cref="NavigationFilterEventArgs"/> 表示一个导航过滤事件。
    /// </summary>
    public class NavigationFilterEventArgs : EventArgs
    {
        private IRoutableViewModel _result;

        public NavigationFilterEventArgs(IRoutableViewModel oldVM, IRoutableViewModel newVM)
        {
            Old = oldVM;
            New = newVM ?? throw new ArgumentNullException(nameof(newVM));
        }

        /// <summary>
        /// 获取当前过滤器的日志记录工具。
        /// </summary>
        public IFullLogger Logger { get; internal set; }
        
        /// <summary>
        /// 获取当前导航事件的旧视图模型。
        /// </summary>
        public IRoutableViewModel Old { get; }

        /// <summary>
        /// 获取当前导航事件的旧视图模型。
        /// </summary>
        public IRoutableViewModel New { get; }


        /// <summary>
        /// 获取或设置当前导航过滤事件的可导航标识，该属性由管线过滤器设置。
        /// </summary>
        public bool CanNavigate { get; set; }

        /// <summary>
        /// 获取或设置当前导航过滤事件的最终导航结果，该属性由管线过滤器设置。
        /// </summary>
        public IRoutableViewModel Result
        {
            get
            {
                if (CanNavigate)
                {
                    return New;
                }
                else
                {
                    return _result;
                }
            }
            set
            {
                if(CanNavigate)
                {
                    _result = New;
                }
                else{
                    _result = value;
                }
            }
        }
    }
}
