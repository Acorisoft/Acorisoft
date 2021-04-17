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
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        IReadOnlyCollection<ITag> GetChildren(ITag entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ITag GetParent(ITag entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void AddToRoot(ITag entity);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="parent"></param>
        void AddToChildren(ITag entity, ITag parent);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void RemoveEntityAndChildren(ITag entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool HasChildren(ITag entity);

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
