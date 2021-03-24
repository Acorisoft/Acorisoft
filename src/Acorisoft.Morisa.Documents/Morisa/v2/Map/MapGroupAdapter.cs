using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.v2.Map
{
    public class MapGroupAdapter : Bindable, IMapGroupAdapter
    {
        public MapGroupAdapter(IMapGroup group)
        {
            Brushes = new ObservableCollection<IMapBrush>();
            Children = new ObservableCollection<IMapGroup>();
            Source = group ?? throw new ArgumentNullException(nameof(group));
        }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<IMapGroup> Children { get; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<IMapBrush> Brushes { get;  }

        /// <summary>
        /// 
        /// </summary>
        public IMapGroup Source { get; }

        /// <summary>
        /// 
        /// </summary>
        public Guid Id
        {
            get => Source.Id;
            set => Source.Id = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid OwnerId
        {
            get => Source.OwnerId;
            set => Source.OwnerId = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get => Source.Name;
            set {
                Source.Name = value;
                RaiseUpdate(nameof(Name));
            }
        }
    }
}
