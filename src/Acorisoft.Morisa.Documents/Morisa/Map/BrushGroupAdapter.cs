using Acorisoft.Morisa.Core;
using DynamicData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    /// <summary>
    /// <see cref="BrushGroupAdapter"/>
    /// </summary>
    public class BrushGroupAdapter : Bindable, IBrushGroupAdapter
    {
        private string _GroupName;
        private readonly Guid _Id;
        private readonly Guid _ParentId;
        private readonly ReadOnlyObservableCollection<BrushGroupAdapter> _Children;
        private readonly IBrushGroup _Source;

        public BrushGroupAdapter(Node<IBrushGroup,Guid> node, Action<IChangeSet<BrushGroupAdapter, Guid>> changeHandler)
        {
            _Id = node.Key;
            _ParentId = node.Item.ParentId;
            _GroupName = node.Item.Name;
            _Source = node.Item;
            node.Children
                .Connect()
                .Transform(x => (BrushGroupAdapter)new BrushGroupAdapter(x, changeHandler))
                .Bind(out _Children)
                .Subscribe(x =>
                {
                    changeHandler?.Invoke(x);
                });
        }

        public bool IsElement
        {
            get
            {
                return Source.IsElement;
            }
            set
            {
                Source.IsElement = value;
                RaiseUpdate(nameof(IsElement));
            }
        }

        public bool IsLocked
        {
            get => _Source.IsLocked;
            set
            {
                _Source.IsLocked = value;
                RaiseUpdate(nameof(IsLocked));
            }
        }


        /// <summary>
        /// 获取或设置当前画刷分组的唯一标识符。
        /// </summary>
        public Guid Id => _Id;


        /// <summary>
        /// 获取或设置当前画刷分组的父级分组。
        /// </summary>
        public Guid ParentId => _ParentId;

        /// <summary>
        /// 获取或设置当前画刷。
        /// </summary>
        public IBrushGroup Source => _Source;

        /// <summary>
        /// 获取或设置当前画刷的子集。
        /// </summary>
        public ReadOnlyObservableCollection<BrushGroupAdapter> Children => _Children;

        /// <summary>
        /// 获取或设置当前画刷的名称。
        /// </summary>
        public string Name
        {
            get => _GroupName;
            set
            {
                _Source.Name = value;
                Set(ref _GroupName, value);
            }
        }
    }
}
