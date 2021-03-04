using Acorisoft.Morisa.Core;
using Acorisoft.Morisa.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Inspirations
{
    public abstract class InspirationElement : MorisaObject, IInspirationElement
    {
        private protected TagCollection _tagCollection;
        private DateTime _creationTime;
        private DateTime _lastModifiedBy;

        public InspirationElement()
        {
            _tagCollection = new TagCollection(this);
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        /// <summary>
        /// 获取当前灵感元素的标签集合。
        /// </summary>
        public ITagCollection Tags => _tagCollection;

        /// <summary>
        /// 获取或设置创建日期。
        /// </summary>
        public DateTime CreationTime {
            get => _creationTime;
            set => SetValueAndRaiseUpdate(ref _creationTime, value);
        }

        /// <summary>
        /// 获取或设置最后修改日期。
        /// </summary>
        public DateTime LastModifiedBy {
            get => _lastModifiedBy;
            set => SetValueAndRaiseUpdate(ref _lastModifiedBy, value);
        }
    }
}
