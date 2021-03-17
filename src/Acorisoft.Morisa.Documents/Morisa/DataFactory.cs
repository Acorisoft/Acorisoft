using Acorisoft.Morisa.Internal;
using DynamicData;
using DynamicData.Binding;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ReactiveUI;
using Splat;
using DryIoc;

namespace Acorisoft.Morisa
{
    public abstract class DataFactory : IDataFactory
    {
        private protected readonly DelegateRecipient<ICompositionSet>           CompositionSet;
        private protected readonly BehaviorSubject<IPageRequest>                PagerStream;   // Page

        protected DataFactory()
        {
            CompositionSet = new DelegateRecipient<ICompositionSet>(CompositionSetChanged);
            PagerStream = new BehaviorSubject<IPageRequest>(new PageRequest(1, 25));
        }

        protected void CompositionSetChanged(ICompositionSet set)
        {
            if(set != null)
            {
                if (DetermineDataSetInitialization(set))
                {
                    InitializeFromDatabase(set);
                }
                else
                {
                    InitializeFromPattern(set);
                }

                OnCompositionSetChanged(set);
            }
        }

        protected virtual void InitializeFromDatabase(ICompositionSet set)
        {

        }
        protected virtual void InitializeFromPattern(ICompositionSet set)
        {

        }

        protected virtual void OnCompositionSetChanged(ICompositionSet set)
        {

        }
        protected virtual bool DetermineDataSetInitialization(ICompositionSet cs)
        {
            return false;
        }

        public IObserver<ICompositionSet> Input => CompositionSet;
        public IObserver<IPageRequest> Pager => PagerStream;
    }
}
