using Acorisoft.Morisa.Core;
using Acorisoft.Morisa.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DataSetFactory : Disposable, IDataSetFactory
    {
        protected readonly BehaviorSubject<bool> ProtectedIsOpenStream;
        protected readonly DefferObserver<Resource> ProtectedResourceHandler;
        protected readonly Subject<Resource> ProtectedResourceStream;

        protected DataSetFactory()
        {
            ProtectedIsOpenStream = new BehaviorSubject<bool>(false);
            ProtectedResourceHandler = new DefferObserver<Resource>(x => ResourceChanged(x));
            ProtectedResourceStream = new Subject<Resource>();
        }

        protected void ResourceChanged(Resource x)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public IObservable<bool> IsOpen => ProtectedIsOpenStream;

        /// <summary>
        /// 
        /// </summary>
        public IObservable<Resource> Completed => ProtectedResourceStream;

        /// <summary>
        /// 
        /// </summary>
        public IObserver<Resource> Resource => ProtectedResourceHandler;
    }
}
