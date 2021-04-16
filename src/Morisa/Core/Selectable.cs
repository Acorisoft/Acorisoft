using LiteDB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    /// <summary>
    /// <see cref="Selectable"/> 类型表示一个可被观察的可选择对象。用于提供对象的选择功能。
    /// </summary>
    public abstract class Selectable : Bindable, ISelectable
    {
        [BsonIgnore]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _IsSelected,_CanSelected;

        /// <summary>
        /// 获取或设置一个值，该值表示当前对象是否被选择。
        /// </summary>
        public bool IsSelected { get => _IsSelected; set => SetValue(ref _IsSelected, value); }

        /// <summary>
        /// 获取或设置一个值，该值表示当前对象是否能选择。
        /// </summary>
        public bool CanSelected { get => _CanSelected; set => SetValue(ref _CanSelected, value); }
    }
}
