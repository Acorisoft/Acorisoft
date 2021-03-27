using Acorisoft.Morisa.Core;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Map
{
    /// <summary>
    /// <see cref="BrushGroup"/> 表示一个画刷分组。
    /// </summary>
    public class BrushGroup : Bindable, IBrushGroup
    {
        /// <summary>
        /// 获取或设置当前画刷分组的唯一标识符。
        /// </summary>
        [BsonId]
        public Guid Id { get; set; }

        [BsonField("l")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置当前画刷分组的父级分组。
        /// </summary>
        [BsonField("p")]
        public Guid ParentId { get; set; }

        /// <summary>
        /// 获取或设置当前画刷的名称。
        /// </summary>
        [BsonField("n")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonField("ie")]
        public bool IsElement { get; set; }
    }
}
