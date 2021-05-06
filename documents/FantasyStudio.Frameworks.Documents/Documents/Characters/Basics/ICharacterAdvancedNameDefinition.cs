using System.Collections.ObjectModel;

namespace Acorisoft.FantasyStudio.Documents.Characters
{
    public interface ICharacterAdvancedNameDefinition : ICharacterDefinition
    {
        /// <summary>
        /// 获取或设置当前人物设定的真实姓名。
        /// </summary>
        string RealName { get; set; }

        /// <summary>
        /// 获取或设置当前人物设定的行动代号。
        /// </summary>
        string CodeName { get; set; }
        /// <summary>
        /// 获取或设置当前人物设定的昵称。
        /// </summary>
        ObservableCollection<string> NickNames { get; set; }

        /// <summary>
        /// 获取或设置当前人物设定的曾用名。
        /// </summary>
        ObservableCollection<string> FormerNames { get; set; }
    }
}
