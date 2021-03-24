﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.v2.Core
{
    public abstract class Disposable : IDisposable
    {
        private bool DisposedValue;

        protected virtual void OnDisposeUnmanagedCore()
        {

        }

        protected virtual void OnDisposeCore()
        {

        }

        protected void Dispose(bool disposing)
        {
            if (!DisposedValue)
            {
                if (disposing)
                {
                    OnDisposeCore();
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