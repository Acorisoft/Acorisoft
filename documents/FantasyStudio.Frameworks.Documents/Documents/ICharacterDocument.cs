using Acorisoft.FantasyStudio.Documents.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Acorisoft.FantasyStudio.Documents
{
    public interface ICharacterDocument
    {
        /// <summary>
        /// 获取或设置当前人物设定文档的名称。
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 获取或设置当前人物设定文档的设定。
        /// </summary>
        List<ICharacterDefinition> Definitions { get; set; }
    }
}
