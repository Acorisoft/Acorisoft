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


        /// <summary>
        /// 
        /// </summary>
        public Guid Id => _Id;

        /// <summary>
        /// 
        /// </summary>
        public Guid ParentId => _ParentId;

        /// <summary>
        /// 
        /// </summary>
        public IBrushGroup Source => _Source;

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyObservableCollection<BrushGroupAdapter> Children => _Children;

        /// <summary>
        /// 
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
