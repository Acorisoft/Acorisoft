using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Emotions
{
    [Obsolete]
    public interface IEmotionMechanism : IMechanismCore<IEmotionElement>
    {
        void Add(IEmotionElement emotion);
        void Remove(IEmotionElement emotion);
        void Clear();
        void Save();
    }
}
