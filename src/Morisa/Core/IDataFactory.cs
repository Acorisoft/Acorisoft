using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    /// <summary>
    /// <see cref="IDataFactory"/> 接口表示一个抽象的数据工厂接口，用于为用户提供数据实体的操作支持。
    /// </summary>
    public interface IDataFactory
    {
        event EventHandler Changed;
    }
}
