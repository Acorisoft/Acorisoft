using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.FantasyStudio.Documents.Characters
{
    /// <summary>
    /// <see cref="IBasicCharacterDefinition"/> 表示一个基本设定模组。
    /// </summary>
    public interface IBasicCharacterDefinition : ICharacterDefinition
    {
        /// <summary>
        /// 获取或设置当前人物设定的姓名。
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 获取或者设置当前人物设定的性别。
        /// </summary>
        Gender Gender { get; set; }

        /// <summary>
        /// 获取或者设置当前人物设定的年龄。
        /// </summary>
        int Age { get; set; }

        /// <summary>
        /// 获取或者设置当前人物设定的生日。
        /// </summary>
        DateTime Birthday { get; set; }

        /// <summary>
        /// 获取或者设置当前人物设定的身高。
        /// </summary>
        int Height { get; set; }

        /// <summary>
        /// 获取或者设置当前人物设定的体重。
        /// </summary>
        int Weight { get; set; }
    }
}
