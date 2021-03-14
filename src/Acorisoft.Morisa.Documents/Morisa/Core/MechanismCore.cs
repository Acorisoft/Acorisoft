using Acorisoft.Morisa.Internal;
using DynamicData;
using LiteDB;
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

namespace Acorisoft.Morisa.Core
{
    public abstract class MechanismCore : IMechanismCore
    {
        public const int FirstPage = 1;
        //-------------------------------------------------------------------------------------------------
        //
        //  GenereateGuid
        //
        //-------------------------------------------------------------------------------------------------

        private int _Page;
        private int _PageSize;

        private readonly DelegateRecipient<ICompositionSet> _CompositionSet;
        protected readonly ISubject<IPageRequest> Paginator;

        //-------------------------------------------------------------------------------------------------
        //
        //  GenereateGuid
        //
        //-------------------------------------------------------------------------------------------------
        protected MechanismCore()
        {
            _CompositionSet = new DelegateRecipient<ICompositionSet>(OnCompositionSetChanged);
            _PageSize = 12;
            _Page = FirstPage;
            Paginator = new BehaviorSubject<IPageRequest>(new PageRequest(FirstPage, PageSize));
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  GenereateGuid
        //
        //-------------------------------------------------------------------------------------------------
        protected void OnCompositionSetChanged(ICompositionSet set)
        {
            if (set is CompositionSet csInstance)
            {
                if (DetermineDatabaseInitialization(csInstance.Database))
                {
                    OnInitializeEmotionFromDatabase(csInstance.Database);
                }
                else
                {
                    OnInitializeEmotionWithConstruct(csInstance.Database);
                }

                OnCompositionSetChanged((CompositionSet)set);
            }
        }

        protected virtual void OnCompositionSetChanged(CompositionSet set)
        {

        }

        protected virtual bool DetermineDatabaseInitialization(LiteDatabase database)
        {
            return false;
        }

        protected virtual void OnInitializeEmotionFromDatabase(LiteDatabase database)
        {

        }

        protected virtual void OnInitializeEmotionWithConstruct(LiteDatabase database)
        {

        }

        //-------------------------------------------------------------------------------------------------
        //
        //  GenereateGuid
        //
        //-------------------------------------------------------------------------------------------------
        public IObserver<ICompositionSet> Input => _CompositionSet;

        public int PageSize
        {
            get => _PageSize;
            set
            {
                _PageSize = value;
                Paginator.OnNext(new PageRequest(_Page, _PageSize));
            }
        }

        public int Page
        {
            get => _Page;
            set
            {
                _Page = value;
                Paginator.OnNext(new PageRequest(_Page, _PageSize));
            }
        }
    }
}
