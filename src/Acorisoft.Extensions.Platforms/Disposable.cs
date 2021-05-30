using System;

namespace Acorisoft.Extensions.Platforms
{
    public abstract class Disposable : IDisposable
    {
        private bool _disposedValue;

        protected virtual void OnDisposeManagedCore()
        {
            
        }

        protected virtual void OnDisposeUnmanagedCore()
        {
            
        }
        
        protected void Dispose(bool disposing)
        {
            if (_disposedValue)
            {
                return;
            }
            
            if (disposing)
            {
                OnDisposeManagedCore();
            }

            OnDisposeUnmanagedCore();
            _disposedValue = true;
        }

        // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        ~Disposable()
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