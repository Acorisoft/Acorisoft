using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    /// <summary>
    /// <see cref="IMapDocument"/> 表示一个地图文档。
    /// </summary>
    public interface IMapDocument
    {
        /// <summary>
        /// 获取或设置当前地图文档的缩略图
        /// </summary>
        public Resource Cover { get; set; }

        /// <summary>
        /// 获取或设置当前地图文档的名字。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置当前地图的地形图层。
        /// </summary>
        public ITerrainLayer Terrain { get; set; }

        /// <summary>
        /// 获取或设置当前地图的地貌图层
        /// </summary>
        public ILandformLayer Landform { get; set; }

        /// <summary>
        /// 获取或设置当前地图的元素图层。
        /// </summary>
        public IElementLayer Elements { get; set; }

        /// <summary>
        /// 当文档发生变化的时候触发该事件。
        /// </summary>
        public event EventHandler Changed;
    }
}
