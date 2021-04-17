using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Tags
{
    /// <summary>
    /// <see cref="ITagBridge"/>
    /// </summary>
    public interface ITagBridge
    {
        /// <summary>
        /// 获取或设置当前的 <see cref="ITagBridge"/>实例的名称。
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 获取或设置当前的 <see cref="ITagBridge"/>实例的颜色。
        /// </summary>
        string Color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ITag Source { get; }

        ReadOnlyObservableCollection<ITagBridge> Children { get; }
    }
}
