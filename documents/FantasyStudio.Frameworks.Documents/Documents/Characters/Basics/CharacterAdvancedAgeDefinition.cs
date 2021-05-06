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
    // Real: {32},Pub: {21}
    [DebuggerDisplay("Real: {RealAge},Pub: {PublicAge}")]
    public class CharacterAdvancedAgeDefinition : Bindable, ICharacterAdvancedAgeDefinition
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]private DateTime _Birth;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]private string _Place;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]private int _Real;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]private int _Public;


        public int SortingIndex => 1;

        /// <summary>
        /// 获取或者设置当前人物设定的生日。
        /// </summary>
        public DateTime Birthday { get => _Birth; set => SetValue(ref _Birth, value); }

        /// <summary>
        /// 获取或者设置当前人物设定的出生地。
        /// </summary>
        public string Birthplace { get => _Place; set => SetValue(ref _Place, value); }

        /// <summary>
        /// 获取或者设置当前人物设定的真实年龄。
        /// </summary>
        public int RealAge { get => _Real; set => SetValue(ref _Real, value); }

        /// <summary>
        /// 获取或者设置当前人物设定的对外公开的年龄。
        /// </summary>
        public int PublicAge { get => _Public; set => SetValue(ref _Public, value); }
    }
}
