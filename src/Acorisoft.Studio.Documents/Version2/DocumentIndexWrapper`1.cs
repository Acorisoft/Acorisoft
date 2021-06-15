using System;
using System.Collections.Generic;

namespace Acorisoft.Studio
{
    public class DocumentIndexWrapper<TIndex> : DocumentIndexWrapper where TIndex : DocumentIndex
    {
        public int CompareTo(DocumentIndexWrapper<TIndex> y)
        {
            var x = this.Source;
            
            if (x == null || y == null)
            {
                return 0;
            }

            var xTicks = x.CreationTimestamp.Ticks;
            var yTicks = y.CreationTimestamp.Ticks;

            if (xTicks > yTicks)
            {
                return -1;
            }

            return xTicks < yTicks ? 1 : 0;
        }
        protected DocumentIndexWrapper(TIndex index)
        {
            Source = index;
        }

        public virtual void RaiseUpdated()
        {
            //
            // 更新
            RaiseUpdated(nameof(IsSelected));
            RaiseUpdated(nameof(IsLocked));
        }

        public string Name
        {
            get => Source.Name;
            set
            {
                Source.Name = value;
                RaiseUpdated();
            }
        }


        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        public DateTime LastAccessTimestamp
        {
            get => Source.LastAccessTimestamp;
            set
            {
                Source.LastAccessTimestamp = value;
                RaiseUpdated();
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTimestamp
        {
            get => Source.CreationTimestamp;
            set
            {
                Source.CreationTimestamp = value;
                RaiseUpdated();
            }
        }
        
        public TIndex Source { get; private set; }
    }
}