using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Acorisoft.Morisa.Core
{
    public interface ISnapshotObject : IUnique, INotifyPropertyChanging, INotifyPropertyChanged
    {
        /// <summary>
        /// 获取或设置文档的唯一标识符。
        /// </summary>
        Guid DocumentId { get; set; }
    }
}
