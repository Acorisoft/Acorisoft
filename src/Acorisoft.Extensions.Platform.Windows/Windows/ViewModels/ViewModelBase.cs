using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Acorisoft.Extensions.Windows.Platforms;
using LiteDB;
using ReactiveUI;

namespace Acorisoft.Extensions.Windows.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject, IViewModel,IDisposable, IRoutableViewModel
    {
        #region IViewModelLifeCycle Interface Members

        
        void IViewModelLifeCycle.Start()
        {
            OnStart();
        }

        protected virtual void OnStart()
        {
            
        }

        void IViewModelLifeCycle.Stop()
        {
            DisposeCore();
        }

        protected virtual void Unsubscribe()
        {
            
        }

        protected virtual void DisposeCore()
        {
            Unsubscribe();
        }

        void IDisposable.Dispose()
        {
            DisposeCore();
        }
        

        #endregion
        
        /// <summary>
        /// 设置指定字段的值并通知更改
        /// </summary>
        /// <typeparam name="T">指定要设置的字段类型。</typeparam>
        /// <param name="source">原始字段。</param>
        /// <param name="value">要设置的值。</param>
        /// <param name="name">设置新值的属性名，缺省。</param>
        protected bool Set<T>(ref T source, T value, [CallerMemberName] string name = "")
        {
            if (EqualityComparer<T>.Default.Equals(source, value))
            {
                return false;
            }

            this.RaisePropertyChanging(name);
            source = value;
            this.RaisePropertyChanged(name);

            return true;
        }


        /// <summary>
        /// 手动推送属性值正在变化的通知。
        /// </summary>
        /// <param name="name">指定属性值正在发生变化的属性名。</param>
        protected void RaiseUpdating(string name)
        {
            this.RaisePropertyChanging(name);
        }

        /// <summary>
        /// 手动推送属性值变化的通知。
        /// </summary>
        /// <param name="name">指定属性值发生变化的属性名。</param>
        protected void RaiseUpdated(string name)
        {
            this.RaisePropertyChanged(name);
        }

        public string Title => UrlPathSegment;
        [BsonIgnore] public virtual string UrlPathSegment => string.Empty;

        [BsonIgnore] public IScreen HostScreen => Platform.ScreenService;
    }
}