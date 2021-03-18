using Acorisoft.Morisa.Core;
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

namespace Acorisoft.Morisa
{
    public interface IDataSetManager<TDataSet> where TDataSet : DataSet
    {
        void Load();
        void Load(string target);
        void Load(IStoreContext context);
    }
}
