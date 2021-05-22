using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reactive;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Acorisoft.Extensions.Platforms.Windows;
using Acorisoft.Extensions.Platforms.Windows.Controls;

namespace Acorisoft.Extensions.Platforms.Services
{
    public partial class ViewService : IViewService
    {
        private IDisposable _indicatorBsb,_indicatorBse,_indicatorBsc;
        private Subject<Unit> _busyStateBegin;
        private Subject<Unit> _busyStateEnd;
        private Subject<string> _busyStateChanged;

        private void InitializeBusyState()
        {
            _busyStateBegin = new Subject<Unit>();
            _busyStateChanged = new Subject<string>();
            _busyStateEnd = new Subject<Unit>();
        }

        private void DisposeBusyState()
        {
            _indicatorBsb?.Dispose();
            _indicatorBsc?.Dispose();
            _indicatorBse?.Dispose();
        }
        
        public Task ForceBusyState(ObservableOperation operation)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }
            
            return Task.Run(() =>
            {
                _busyStateBegin.OnNext(Unit.Default);
                _busyStateChanged.OnNext(operation?.Description ?? GetDefaultDescription());
                operation.Run();
                _busyStateEnd.OnNext(Unit.Default);
            });
        }

        private static string GetDefaultDescription()
        {
            return SR.ViewService_ArgumentNull;
        }

        public Task ForceBusyState(IEnumerable<ObservableOperation> operations)
        {
            if (operations is null)
            {
                throw new ArgumentNullException(nameof(operations));
            }
            
            return Task.Run(() =>
            {
                _busyStateBegin.OnNext(Unit.Default);
                foreach (var operation in operations)
                {
                    if (operation is null)
                    {
                        continue;
                    }
                    _busyStateChanged.OnNext(operation?.Description ?? GetDefaultDescription());
                    operation.Run();
                }
                _busyStateEnd.OnNext(Unit.Default);
            });
        }

        public void SetBusyIndicator(IBusyIndicatorCore indicator)
        {
            DisposeBusyState();
            
            var newInstance = indicator ?? throw new ArgumentNullException(nameof(indicator));
            
            //
            //
            _indicatorBsb = newInstance.SubscribeBusyStateBegin(_busyStateBegin);
            _indicatorBsc = newInstance.SubscribeBusyStateChanged(_busyStateChanged); 
            _indicatorBse = newInstance.SubscribeBusyStateEnd(_busyStateEnd);
        }

        public IObservable<string> BusyStateChanged => _busyStateChanged;
        public IObservable<Unit> BusyStateBegin => _busyStateBegin;
        public IObservable<Unit> BusyStateEnd => _busyStateEnd;
    }
}