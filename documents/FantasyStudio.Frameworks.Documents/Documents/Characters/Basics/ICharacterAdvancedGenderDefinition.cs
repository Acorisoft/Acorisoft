using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.FantasyStudio.Documents.Characters
{
    public interface ICharacterAdvancedGenderDefinition : ICharacterDefinition
    {
        /// <summary>
        /// 获取或设置当前人物设定的性取向。
        /// </summary>
        SexualOrientation SexualOrientation { get; set; }

        /// <summary>
        /// 获取或设置当前人物设定的性认知。
        /// </summary>
        SexualCognitive SexualCognitive { get; set; }
    }
}
