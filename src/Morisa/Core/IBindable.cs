using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    /// <summary>
    /// <see cref="IBindable"/> 接口表示一个抽象的可绑定对象接口，用于为<see cref="Morisa"/> 提供属性变更通知支持。
    /// </summary>
    public interface IBindable : INotifyPropertyChanged, INotifyPropertyChanging
    {
    }
}
