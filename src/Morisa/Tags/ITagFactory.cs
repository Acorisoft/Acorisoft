using Acorisoft.Morisa.Composition;
using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Tags
{
    /// <summary>
    /// <see cref="ITagFactory"/>
    /// </summary>
    public interface ITagFactory : IEntitySystem, IDataFactory<ITag>
    {
        /// <summary>
        /// 升级
        /// </summary>
        /// <param name="tag"></param>
        void Promote(ITag tag);

        /// <summary>
        /// 降级
        /// </summary>
        /// <param name="tag"></param>
        void Demote(ITag tag);

        /// <summary>
        /// 
        /// </summary>
        ReadOnlyObservableCollection<ITagBridge> Collection { get; }
    }
}
