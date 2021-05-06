using Acorisoft.FantasyStudio.Documents.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.FantasyStudio.Documents
{
    /// <summary>
    /// <see cref="CharacterDocument"/>
    /// </summary>
    public class CharacterDocument : ICharacterDocument
    {
        /// <summary>
        /// 获取或设置当前人物设定文档的名字。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置当前人物设定文档的设定。
        /// </summary>
        public List<ICharacterDefinition> Definitions { get; set; }
    }
}
