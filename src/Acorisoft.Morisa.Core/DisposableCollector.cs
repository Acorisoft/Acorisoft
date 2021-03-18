using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public class DisposableCollector : IDisposableCollector
    {
        private readonly CompositeDisposable _Disposable = new CompositeDisposable();
        private bool _DisposedValue;

        public CompositeDisposable Disposable => _Disposable;

        public void Collect(IDisposable disposable)
        {
            if(disposable != null)
            {
                _Disposable.Add(_Disposable);
            }
        }

        public void Uncollect(IDisposable disposable)
        {
            if (disposable != null)
            {
                _Disposable.Remove(_Disposable);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_DisposedValue)
            {
                if (disposing)
                {
                    _Disposable.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并替代终结器
                // TODO: 将大型字段设置为 null
                _DisposedValue = true;
            }
        }

        // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        ~DisposableCollector()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
