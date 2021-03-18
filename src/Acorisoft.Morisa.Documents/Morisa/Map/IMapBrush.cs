using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    /// <summary>
    /// <see cref=""/> 表示连接处的内容占比。
    /// </summary>
    public enum FillMode
    {
        Thin,
        Half,
        Extra,
        Large
    }

    public interface IMapBrush
    {
        Guid Id { get; set; }
        Guid? OwnerId { get; set; }
        Resource Brush { get; set; }
        FillMode Left { get; set; }
        FillMode Right { get; set; }
        FillMode Top { get; set; }
        FillMode Bottom { get; set; }
    }
}
