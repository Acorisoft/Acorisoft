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
    [DebuggerDisplay("{SexualOrientation}")]
    public class CharacterAdvancedGenderDefinition : Bindable, ICharacterAdvancedGenderDefinition
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]private SexualCognitive _Cognitive;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]private SexualOrientation _Orientation;


        public int SortingIndex => 1;

        /// <summary>
        /// 获取或设置当前人物设定的性取向。
        /// </summary>
        public SexualOrientation SexualOrientation { get => _Orientation; set => SetValue(ref _Orientation,value); }

        /// <summary>
        /// 获取或设置当前人物设定的性认知。
        /// </summary>
        public SexualCognitive SexualCognitive { get => _Cognitive; set => SetValue(ref _Cognitive, value); }
    }
}
