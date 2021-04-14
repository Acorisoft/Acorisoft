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
        /// <summary>
        /// 通知属性值已经发生变更。
        /// </summary>
        /// <param name="name">指定已经发生属性值变更的属性名。</param>
        void RaiseUpdated(string name);


        /// <summary>
        /// 通知属性值正在变更。
        /// </summary>
        /// <param name="name">指定正在变更的属性值属性名。</param>
        void RaiseUpdating(string name);

        /// <summary>
        /// 设置指定的值给指定的字段，并通知属性值已经发生变化。
        /// </summary>
        /// <typeparam name="T">指定要赋值到的字段类型。</typeparam>
        /// <param name="backend">字段。</param>
        /// <param name="value">新值。</param>
        /// <param name="name">属性名。</param>
        void SetValue<T>(ref T backend, T value, string name);
    }
}
