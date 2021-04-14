using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    /// <summary>
    /// <see cref="Disposable"/> 表示一个可释放的对象。
    /// </summary>
    public abstract class Disposable : IDisposable
    {
        private bool _DisposedValue;

        protected virtual void OnReleaseManageCore()
        {

        }

        protected virtual void OnReleaseUnmanageCore()
        {

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_DisposedValue)
            {
                if (disposing)
                {
                    OnReleaseManageCore();
                }

                OnReleaseUnmanageCore();
                _DisposedValue = true;
            }
        }

        public bool IsDisposed => _DisposedValue;

        void IDisposable.Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        ~Disposable()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: false);
        }

    }
}
