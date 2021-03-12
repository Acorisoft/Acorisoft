using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Joins;
using System.Reactive.Linq;
using System.Reactive.PlatformServices;
using System.Reactive.Subjects;
using System.Reactive.Threading;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Emotions
{
    public class EmotionMechanism : IEmotionMechanism
    {
        public EmotionMechanism(CompositeDisposable disposable)
        {
            ListObservable
        }
        

        public IObservable<ICompositionSet> CompositionSet { get; }
    }
}
