using Acorisoft.FantasyStudio.Core;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Acorisoft.FantasyStudio.Documents.Characters
{
    [DebuggerDisplay("{{{RealName}}}")]
    public class CharacterAdvancedNameDefinition : Bindable, ICharacterAdvancedNameDefinition
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]private string _Real;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]private string _Code;

        public int SortingIndex => 1;
        /// <summary>
        /// 获取或设置当前人物设定的真实姓名。
        /// </summary>
        public string RealName { get => _Real; set => SetValue(ref _Real, value); }

        /// <summary>
        /// 获取或设置当前人物设定的行动代号。
        /// </summary>
        public string CodeName { get => _Code; set => SetValue(ref _Code, value); }

        /// <summary>
        /// 获取或设置当前人物设定的昵称。
        /// </summary>
        public ObservableCollection<string> NickNames { get; set; }

        /// <summary>
        /// 获取或设置当前人物设定的曾用名。
        /// </summary>
        public ObservableCollection<string> FormerNames { get; set; }
    }
}
