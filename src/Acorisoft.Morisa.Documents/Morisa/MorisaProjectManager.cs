using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using ProjectObserverCollection = System.Collections.Generic.List<System.IObserver<Acorisoft.Morisa.IMorisaProject>>;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="MorisaProjectManager"/> 
    /// </summary>    
    public class MorisaProjectManager : IMorisaProjectManager
    {
        [field: NonSerialized]
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
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public void Load(string fileName)
        {
            ((ISubject<string , IMorisaProject>)this).OnNext(fileName);
            ((ISubject<string , IMorisaProject>)this).OnCompleted();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maybeProjectDirectory"></param>
        protected void LoadProjectCore(string maybeProjectDirectory)
        {
            if (File.Exists(maybeProjectDirectory))
            {
                OpenAsFile(maybeProjectDirectory);
            }
            else
            {
                OpenAsDirectory(maybeProjectDirectory);
            }
        }

        private void OpenAsFile(string fileName)
        {
            var directory = Directory.GetParent(fileName).FullName;

            if(!Directory.Exists(directory))
            {
                //
                // 确保上级目录存在
                var parentDirectory = Directory.GetParent(directory)?.FullName;

                if (Directory.Exists(parentDirectory))
                {
                    //
                    // 如果存在则创建新的目录
                    Directory.CreateDirectory(directory);
                }
                else
                {
                    //
                    // 否则报错
                    var ex = new InvalidOperationException("无法打开一个不存在的目录");

                    foreach (var observer in _observers)
                    {
                        if (observer == null)
                        {
                            _observers.Remove(observer);
                        }
                        else
                        {
                            observer.OnError(ex);
                            observer.OnCompleted();
                        }
                    }

                    throw ex;
                }
            }

            OpenProject(fileName , directory);
        }

        private void OpenAsDirectory(string directory)
        {
            var fileName = Path.Combine(directory , MorisaProject.MorisaProjectMainDBFullName);

            if (!Directory.Exists(directory))
            {
                //
                // 确保上级目录存在
                var parentDirectory = Directory.GetParent(directory)?.FullName;

                if (Directory.Exists(parentDirectory))
                {
                    //
                    // 如果存在则创建新的目录
                    Directory.CreateDirectory(directory);
                }
                else
                {
                    //
                    // 否则报错
                    var ex = new InvalidOperationException("无法打开一个不存在的目录");

                    foreach (var observer in _observers)
                    {
                        if (observer == null)
                        {
                            _observers.Remove(observer);
                        }
                        else
                        {
                            observer.OnError(ex);
                            observer.OnCompleted();
                        }
                    }

                    throw ex;
                }
            }

            OpenProject(fileName , directory);
        }

        private void OpenProject(string fileName,string directory)
        {
            //
            // 初始化项目
            var morisaProject = new MorisaProject(directory , fileName);

            foreach (var observer in _observers)
            {
                if (observer == null)
                {
                    _observers.Remove(observer);
                }
                else
                {
                    observer.OnNext(morisaProject);
                    observer.OnCompleted();
                }
            }
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
            return new CollectionDisposable<IObserver<IMorisaProject>>(_observers , observer);
        }

        void IObserver<string>.OnCompleted()
        {
            if (_isProjectDirectoryChanged && !string.IsNullOrEmpty(_projectDirectory))
            {

                //
                // 打开项目目录
                LoadProjectCore(_projectDirectory);

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
            if (!string.IsNullOrEmpty(value) && _projectDirectory != value)
            {
                _isProjectDirectoryChanged = true;
                _projectDirectory = value;
            }
        }

        /// <summary>
        /// 通知服务提供者有一个观察者想要接收通知。
        /// </summary>
        /// <param name="observer">表示一个接收通知的对象。</param>
        /// <returns>返回一个引用，用于允许服务提供者在发送通知之前停止该观察者接收通知。</returns>
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
