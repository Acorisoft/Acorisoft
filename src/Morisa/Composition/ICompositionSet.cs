using Acorisoft.Morisa.Core;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Composition
{
    /// <summary>
    /// <see cref="ICompositionSet"/> 表示一个抽象的创作集接口，用于为用户提供访问创作集数据功能的支持。
    /// </summary>
    public interface ICompositionSet : IDataSet<CompositionSetProperty>
    {
    }
}
