using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public class CompositionSetFactory : ICompositionSetFactory, ICompositionSetFactoryImpl
    {
        private IDisposableCollector        _Disposables;
        private ICompositionSetContext      _Context;

        public CompositionSetFactory(IDisposableCollector collector)
        {
            _Disposables = collector ?? throw new ArgumentNullException(nameof(collector));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Load(IGenerateContext<ICompositionSetInformation> context)
        {
            throw new NotImplementedException();
        }

        public void Load(IStoreContext context)
        {
            throw new NotImplementedException();
        }

        public void Load(string target)
        {
            throw new NotImplementedException();
        }
        public IObservable<ICompositionSetContext> CompositionSetStream => throw new NotImplementedException();

        public IObservable<ICompositionSetInformation> InformationStream => throw new NotImplementedException();

    }
}
