using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Tags
{

    /// <summary>
    /// <see cref="Tag"/> 类型表示一个标签实例。
    /// </summary>
    public class Tag : ITag, IEquatable<Tag>
    {
        /// <summary>
        /// 比较与指定对象之间的值等价性。
        /// </summary>
        /// <param name="y">指定要比较的对象。</param>
        /// <returns>如果指定的对象值等价性一致，则返回<see cref="true"/>否则返回<see cref="false"/></returns>
        public bool Equals([AllowNull]Tag x)
        {
            if(x == null)
            {
                return false;
            }

            return x.Name == Name;
        }

        /// <summary>
        /// 获取或设置当前的 <see cref="ITag"/>实例的唯一标识符。
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置当前的 <see cref="ITag"/>实例父级的唯一标识符。
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 获取或设置当前的 <see cref="ITag"/>实例的名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置当前的 <see cref="ITag"/>实例的颜色。
        /// </summary>
        public string Color { get; set; }


        /// <summary>
        /// 比较与指定对象之间的值等价性。
        /// </summary>
        /// <param name="y">指定要比较的对象。</param>
        /// <returns>如果指定的对象值等价性一致，则返回<see cref="true"/>否则返回<see cref="false"/></returns>
        public override sealed bool Equals(object obj)
        {
            if (obj is Tag x)
            {
                return Equals(x);
            }

            return base.Equals(obj);
        }

        public override sealed int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Name?.GetHashCode() ?? 17);
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
