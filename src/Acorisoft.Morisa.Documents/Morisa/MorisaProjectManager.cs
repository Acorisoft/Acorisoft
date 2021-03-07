using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using ProjectObserverCollection = System.Collections.Generic.List<System.IObserver<Acorisoft.Morisa.IMorisaProject>>;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="MorisaProjectManager"/> 
    /// </summary>
    public class MorisaProjectManager : IMorisaProjectManager, ISubject<string, IMorisaProject>
    {
        private readonly ProjectObserverCollection _observers;

        [field: NonSerialized]
        private string _projectDirectory;

        [field: NonSerialized]
        private bool _isProjectDirectoryChanged;

        public MorisaProjectManager()
        {
            _observers = new ProjectObserverCollection();
        }

        /// <summary>
        /// 通知服务提供者有一个观察者想要接收通知。
        /// </summary>
        /// <param name="observer">表示一个接收通知的对象。</param>
        /// <returns>返回一个引用，用于允许服务提供者在发送通知之前停止该观察者接收通知。</returns>
        public IDisposable Subscribe(IObserver<IMorisaProject> observer)
        {
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observer));
            }

            //
            // 添加到项目变化观察者集合。
            _observers.Add(observer);

            //
            // 返回集合释放器
            return new CollectionDisposable<IObserver<IMorisaProject>>(_observers, observer);
        }

        void IObserver<string>.OnCompleted()
        {
            if(_isProjectDirectoryChanged && !string.IsNullOrEmpty(_projectDirectory))
            {
                //
                // 重新设置为false
                _isProjectDirectoryChanged = false;
            }
        }

        void IObserver<string>.OnError(Exception error)
        {
            
        }

        void IObserver<string>.OnNext(string value)
        {
            if(!string.IsNullOrEmpty(value) && _projectDirectory != value)
            {
                _isProjectDirectoryChanged = true;
                _projectDirectory = value;
            }
        }

        IDisposable IObservable<IMorisaProject>.Subscribe(IObserver<IMorisaProject> observer)
        {
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observer));
            }
            return Subscribe(observer);
        }
    }
}
