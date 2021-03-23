using Acorisoft.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public abstract class DataSetManager<TDataSet> : DataSetManager, IDataSetManager<TDataSet>
        where TDataSet : DataSet, IDataSet
    {
        protected readonly BehaviorSubject<bool> ProtectedIsOpenStream;
        protected readonly Subject<TDataSet> ProtectedDataSetStream;

        protected DataSetManager()
        {
            ProtectedIsOpenStream = new BehaviorSubject<bool>(false);
            ProtectedDataSetStream = new Subject<TDataSet>();
        }  

        /// <summary>
        /// 
        /// </summary>
        public IObservable<TDataSet> DataSetStream => ProtectedDataSetStream;

        /// <summary>
        /// 
        /// </summary>
        public IObservable<bool> IsOpenStream => ProtectedIsOpenStream;

        /// <summary>
        /// 
        /// </summary>
        public TDataSet DataSet { get; private protected set; }

    }
}
