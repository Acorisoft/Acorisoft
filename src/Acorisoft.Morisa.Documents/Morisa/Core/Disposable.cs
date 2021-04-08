using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Core
{
    /// <summary>
    /// <see cref="Disposable"/> 类型实例，用于为 <see cref="Morisa"/> 提供对象可释放支持。
    /// </summary>
    public abstract class Disposable : IDisposable
    {
        private bool DisposedValue;

        protected virtual void OnDisposeUnmanagedCore()
        {

        }

        protected virtual void OnDisposeManagedCore()
        {

        }

        protected void Dispose(bool disposing)
        {
            if (!DisposedValue)
            {
                if (disposing)
                {
                    OnDisposeManagedCore();
                }

                OnDisposeUnmanagedCore();
                DisposedValue = true;
            }
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
