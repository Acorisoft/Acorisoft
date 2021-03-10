
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject, IRoutableViewModel
    {
        private IScreen _screen;

        /// <summary>
        /// 设置指定字段的值并通知更改
        /// </summary>
        /// <typeparam name="T">指定要设置的字段类型。</typeparam>
        /// <param name="source">原始字段。</param>
        /// <param name="value">要设置的值。</param>
        /// <param name="name">设置新值的属性名，缺省。</param>
        protected bool Set<T>(ref T source , T value , [CallerMemberName] string name = "")
        {
            if (!EqualityComparer<T>.Default.Equals(source , value))
            {
                this.RaisePropertyChanging(name);
                source = value;
                this.RaisePropertyChanged(name);

                return true;
            }

            return false;
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

        public virtual string UrlPathSegment => string.Empty;

        public IScreen HostScreen {
            get {
                if (_screen == null)
                {
                    _screen = Locator.Current.GetService<IScreen>();
                }
                return _screen;
            }
        }
    }
}