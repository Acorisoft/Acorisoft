using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    /// <summary>
    /// <see cref="IMapBrushSet"/> 表示一个画刷集。
    /// </summary>
    public interface IMapBrushSet : IDisposable
    {
        /// <summary>
        /// 获取或设置当前画刷集的分组。
        /// </summary>
        IGroupCollection BrushGroups { get; }

        /// <summary>
        /// 获取或设置当前画刷集的画刷集合。
        /// </summary>
        IMapBrushCollection Brushes { get; }

        /// <summary>
        /// 获取或设置当前画刷集的名字。
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 获取或设置当前画刷集的作者。
        /// </summary>
        string Author { get; set; }

        /// <summary>
        /// 获取或设置当前画刷集的简介。
        /// </summary>
        string Summary { get; set; }

        /// <summary>
        /// 获取或设置当前画刷集的标签。
        /// </summary>
        List<string> Tags { get; set; }

        /// <summary>
        /// 获取或设置当前画刷集的封面。
        /// </summary>
        InDatabaseResource Cover { get; }
    }
}
