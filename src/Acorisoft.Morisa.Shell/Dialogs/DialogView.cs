using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Dialogs
{
    public abstract class DialogView<TViewModel> : ReactiveUserControl<TViewModel> where TViewModel : DialogFunction
    {
        protected readonly CompositeDisposable _Disposable;

        protected DialogView()
        {
            _Disposable = new CompositeDisposable();
            this.WhenActivated(d =>
            {
                d(this.WhenAnyValue(x => x.ViewModel).BindTo(this, x => x.DataContext));
            })
                .DisposeWith(_Disposable);
        }
    }
}
