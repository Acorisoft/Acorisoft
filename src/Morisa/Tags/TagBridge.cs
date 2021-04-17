using Acorisoft.Morisa.Core;
using DynamicData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Disposables;

namespace Acorisoft.Morisa.Tags
{
    /// <summary>
    /// <see cref="TagBridge"/>
    /// </summary>
    public class TagBridge : Selectable, ITagBridge
    {
        private readonly ITag _Source;
        private readonly Node<ITag, Guid> _Parent;
        private readonly ReadOnlyObservableCollection<ITagBridge> CollectionInstance;

        public TagBridge(Node<ITag, Guid> node, EntityChangedHandler<ITagBridge, Guid> handler)
        {
            _Source = node.Item;
            _Parent = node.Parent.HasValue ? node.Parent.Value : null;
            node.Children
                .Connect()
                .Transform(x => (ITagBridge)new TagBridge(x,handler))
                .Bind(out CollectionInstance)
                .Subscribe(x =>
                {
                    handler?.Invoke(x);
                });
        }

        /// <summary>
        /// 获取或设置当前的 <see cref="ITag"/>实例的唯一标识符。
        /// </summary>
        public Guid Id => Source.Id;

        /// <summary>
        /// 获取或设置当前的 <see cref="ITag"/>实例父级的唯一标识符。
        /// </summary>
        public Guid ParentId => Source.ParentId;

        /// <summary>
        /// 获取或设置当前的 <see cref="ITag"/>实例的名称。
        /// </summary>
        public string Name
        {
            get
            {
                return Source.Name;
            }
            set
            {
                Source.Name = value;
                RaiseUpdated(nameof(Name));
            }
        }


        /// <summary>
        /// 获取或设置当前的 <see cref="ITag"/>实例的父级。
        /// </summary>
        public ITag Parent
        {
            get => _Parent.Item;
        }

        /// <summary>
        /// 获取或设置当前的 <see cref="ITag"/>实例的颜色。
        /// </summary>
        public string Color
        {
            get
            {
                return Source.Color;
            }
            set
            {
                Source.Color = value;
                RaiseUpdated(nameof(Color));
            }
        }

        /// <summary>
        /// 获取或设置当前的 <see cref="ITag"/>实例的源数据。
        /// </summary>
        public ITag Source => _Source;

        /// <summary>
        /// 获取或设置当前的 <see cref="ITag"/>实例的子集。
        /// </summary>
        public ReadOnlyObservableCollection<ITagBridge> Children => CollectionInstance;

        public override string ToString()
        {
            return Name;
        }
    }
}
