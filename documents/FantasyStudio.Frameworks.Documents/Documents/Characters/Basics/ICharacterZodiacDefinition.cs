using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.FantasyStudio.Documents.Characters
{
    public interface ICharacterZodiacDefinition : ICharacterDefinition
    {
        /// <summary>
        /// 获取或设置当前人物设定的星座。
        /// </summary>
        public Zodiac Zodiac { get; set; }
    }
}
