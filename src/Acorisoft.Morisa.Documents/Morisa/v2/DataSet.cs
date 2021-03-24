using LiteDB;
using DynamicData;
using DynamicData.Binding;
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
using ExternalCollection = LiteDB.LiteCollection<LiteDB.BsonDocument>;
using Disposable = Acorisoft.Morisa.v2.Core.Disposable;

namespace Acorisoft.Morisa.v2
{
    public abstract class DataSet : Disposable
    {

        protected override void OnDisposeUnmanagedCore()
        {
            Database?.Dispose();
        }

        [BsonIgnore]
        protected internal LiteDatabase Database { get; internal set; }

        [BsonIgnore]
        protected internal ExternalCollection DB_External { get; internal set; }
    }
}
