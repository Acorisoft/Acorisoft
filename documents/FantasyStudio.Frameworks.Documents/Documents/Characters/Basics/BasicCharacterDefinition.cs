using Acorisoft.FantasyStudio.Core;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.FantasyStudio.Documents.Characters
{
    // 姜少凡 {男，14}

    [DebuggerDisplay("{Name} {{{Gender},{Age}}}")]
    public class BasicCharacterDefinition : Bindable, IBasicCharacterDefinition
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]private string _Name;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]private Gender _Gender;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]private int _Age;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]private int _Height;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]private int _Weight;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]private DateTime _Birthday;

        public int SortingIndex => 0;

        /// <summary>
        /// 获取或设置当前人物设定的姓名。
        /// </summary>
        public string Name { get => _Name; set => SetValue(ref _Name, value); }

        /// <summary>
        /// 获取或者设置当前人物设定的性别。
        /// </summary>
        public Gender Gender { get => _Gender; set => SetValue(ref _Gender, value); }

        /// <summary>
        /// 获取或者设置当前人物设定的年龄。
        /// </summary>
        public int Age { get => _Age; set => SetValue(ref _Age, value); }

        /// <summary>
        /// 获取或者设置当前人物设定的生日。
        /// </summary>
        public DateTime Birthday { get => _Birthday; set => SetValue(ref _Birthday, value); }

        /// <summary>
        /// 获取或者设置当前人物设定的身高。
        /// </summary>
        public int Height { get => _Height; set => SetValue(ref _Height, value); }

        /// <summary>
        /// 获取或者设置当前人物设定的体重。
        /// </summary>
        public int Weight { get => _Weight; set => SetValue(ref _Weight, value); }
    }
}
