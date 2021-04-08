using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Threading;
namespace Acorisoft.Morisa.Documents
{
    public class Class1
    {
        static Class1()
        {
            IDataSetFactory factory = null;
            factory.ExceptionStream
                   .ObserveOn(CurrentThreadScheduler.Instance)
                   .SubscribeOn(CurrentThreadScheduler.Instance)
                   .Subscribe(x =>
                   {
                       
                   });
        }
    }
}
