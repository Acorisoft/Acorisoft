using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Disposables;
using Acorisoft.Properties;
using System.Globalization;
using System.IO;
using LiteDB;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="MorisaProjectManager"/>
    /// </summary>
    public class MorisaProjectManager : IMorisaProjectManager, IDisposable
    {

        //-------------------------------------------------------------------------------------------------
        //
        //  Constants
        //
        //-------------------------------------------------------------------------------------------------
        public const string CannotOpenEmptyString                   = "CannotOpenEmptyString";
        public const string CannotOpenWrongFile                     = "CannotOpenWrongFile";
        public const string FileNotFound                            = "FileNotFound";
        public const string ProjectMainDatabaseName                 = "Main.Morisa-Project";
        public const string ProjectMainDatabaseSubffix              = ".Morisa-Project";
        public const string ExternalCollectionName                  = "Externals";
        public const int ProjectMainDatabaseSize                    = 16 * 1024 * 1024;

        //-------------------------------------------------------------------------------------------------
        //
        //  Internal Classes
        //
        //-------------------------------------------------------------------------------------------------
        protected class ProjectInfoStream : Observable<IMorisaProjectInfo>, IObservable<IMorisaProjectInfo>
        {
            public void Notification(IMorisaProjectInfo info)
            {
                if (info != null)
                {
                    foreach (var observer in _List)
                    {
                        observer.OnNext(info);
                        observer.OnCompleted();
                    }
                }
            }

            public void Error(Exception ex)
            {
                if (ex != null)
                {
                    foreach (var observer in _List)
                    {
                        observer.OnError(ex);
                    }
                }
            }
        }

        protected class ProjectStream : Observable<IMorisaProject>, IObservable<IMorisaProject>
        {
            public void Notification(IMorisaProject info)
            {
                if (info != null)
                {
                    foreach (var observer in _List)
                    {
                        observer.OnNext(info);
                        observer.OnCompleted();
                    }
                }
            }
            public void Error(Exception ex)
            {
                if (ex != null)
                {
                    foreach (var observer in _List)
                    {
                        observer.OnError(ex);
                    }
                }
            }
        }


        //-------------------------------------------------------------------------------------------------
        //
        //  Variables
        //
        //-------------------------------------------------------------------------------------------------

        private readonly ProjectInfoStream          _ProjectInfoStream;
        private readonly ProjectStream              _ProjectStream;
        private readonly CompositeDisposable        _Disposable;

        private MorisaProject   _Current;
        private bool            _DisposedValue;

        //-------------------------------------------------------------------------------------------------
        //
        //  Constructors
        //
        //-------------------------------------------------------------------------------------------------

        public MorisaProjectManager()
        {
            _Disposable = new CompositeDisposable();
            _ProjectInfoStream = new ProjectInfoStream();
            _ProjectStream = new ProjectStream();
            _Disposable.Add(_ProjectInfoStream);
            _Disposable.Add(_ProjectStream);
        }


        //-------------------------------------------------------------------------------------------------
        //
        //  Private Methods
        //
        //-------------------------------------------------------------------------------------------------

        void OpenAsDirectoryTarget(string target)
        {
            string dbName;

            //
            // 这个方法将把字符串作为项目目录进行打开。
            if (Directory.Exists(target))
            {
                dbName = Path.Combine(target , ProjectMainDatabaseName);
            }
            else
            {
                //
                //
                var parentDirectory = Directory.GetParent(target);

                //
                //
                if (parentDirectory.Exists)
                {
                    Directory.CreateDirectory(target);
                }

                dbName = Path.Combine(target , ProjectMainDatabaseName);
            }

            //
            // 打开项目
            OpenProjectCore(target , dbName);
        }

        void OpenAsFileTarget(string target)
        {
            string directory;

            if (File.Exists(target))
            {
                directory = Directory.GetParent(target).FullName;

                if (new FileInfo(target).Extension == ProjectMainDatabaseSubffix)
                {
                    //
                    // 项目后缀名正确
                    OpenProjectCore(directory , target);
                }
                else
                {
                    //
                    // 后缀名错误
                    var ex = new InvalidOperationException(
                        SR.ResourceManager.GetString(CannotOpenWrongFile,CultureInfo.CurrentCulture));

                    //
                    // 推送错误
                    _ProjectStream.Error(ex);
                }
            }
            else
            {
                directory = Directory.GetParent(target).FullName;
                var parentDirectory = Directory.GetParent(directory).FullName;

                if (!Directory.Exists(directory) && Directory.Exists(parentDirectory))
                {
                    Directory.CreateDirectory(directory);
                }

                if(!Directory.Exists(directory) && !Directory.Exists(parentDirectory))
                {
                    //
                    // 后缀名错误
                    var ex = new InvalidOperationException(
                        SR.ResourceManager.GetString(CannotOpenWrongFile,CultureInfo.CurrentCulture));

                    //
                    // 推送错误
                    _ProjectStream.Error(ex);
                }

                //
                // 创建项目
                OpenProjectCore(directory , target);
            }
        }

        void OpenProjectCore(string directory , string fileName)
        {
            if (string.IsNullOrEmpty(directory) || string.IsNullOrEmpty(fileName))
            {
                var ex = new InvalidOperationException(
                    SR.ResourceManager.GetString(
                        CannotOpenEmptyString,CultureInfo.CurrentCulture));

                //
                // 推送错误
                _ProjectStream.Error(ex);
            }

            try
            {
                //
                // 打开项目
                var db = new LiteDatabase(new ConnectionString
                {
                    Filename = fileName,
                    InitialSize = ProjectMainDatabaseSize,
                    Connection = ConnectionType.Direct,
                });

                if (_Current != null)
                {
                    _Current.Dispose();
                    _Current = null;
                }

                //
                // 创建项目。
                _Current = new MorisaProject(db);


                if (!_Current.IsNeedInitialize)
                {
                    //
                    // 创建项目信息。
                    var projInfo = new MorisaProjectInfo
                    {
                        Name = _Current.Name,
                        FileName = fileName,
                        Directory = directory
                    };

                    //
                    // 如果不需要初始化则表明改项目是已经存在的
                    // 调用string版本的LoadOrCreateProject 一般是用户指向一个文件链接
                    // 这时候我们需要打开项目并返回项目信息给AppViewModel
                    //
                    // 推送消息
                    _ProjectInfoStream.Notification(projInfo);
                }

                //
                // 推送消息
                _ProjectStream.Notification(_Current);
            }
            catch (Exception ex)
            {

                //
                // 推送错误
                _ProjectStream.Error(ex);
            }
            finally
            {

            }
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Public Methods
        //
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// 加载或创建指定的项目
        /// </summary>
        /// <param name="target">指定要加载或者创建的项目类型。</param>
        public void LoadOrCreateProject(string target)
        {
            //
            // 检测参数是否为空字符串，如果是空字符串则直接抛出异常错误。
            if (string.IsNullOrEmpty(target))
            {
                var ex = new InvalidOperationException(
                    SR.ResourceManager
                    .GetString(
                        CannotOpenEmptyString,CultureInfo.CurrentCulture));
                //
                // 推送错误通知
                _ProjectStream.Error(ex);
            }

            if (Directory.Exists(target))
            {
                //
                // 使用Directory进行判断当前是否为目录
                // [是] 则使用
                // [否] 则使用
                OpenAsDirectoryTarget(target);
            }
            else
            {
                OpenAsFileTarget(target);
            }
        }


        /// <summary>
        /// 加载或创建指定的项目
        /// </summary>
        /// <param name="target">指定要加载或者创建的项目类型。</param>
        public void LoadOrCreateProject(IMorisaProjectInfo target)
        {
            //
            // 检测参数是否为空字符串，如果是空字符串则直接抛出异常错误。
            if (target == null || string.IsNullOrEmpty(target.FileName))
            {
                var ex = new InvalidOperationException(
                    SR.ResourceManager
                    .GetString(
                        CannotOpenEmptyString,CultureInfo.CurrentCulture));
                //
                // 推送错误通知
                _ProjectStream.Error(ex);
            }

            if (File.Exists(target.FileName))
            {
                try
                {
                    //
                    // 打开项目
                    var db = new LiteDatabase(new ConnectionString
                    {
                        Filename = target.FileName,
                        InitialSize = ProjectMainDatabaseSize,
                        Connection = ConnectionType.Direct,
                    });

                    if (_Current != null)
                    {
                        _Current.Dispose();
                        _Current = null;
                    }

                    //
                    // 创建项目。
                    _Current = new MorisaProject(db);

                    //
                    // 推送消息
                    _ProjectStream.Notification(_Current);
                }
                catch (Exception ex)
                {

                    //
                    // 推送错误
                    _ProjectStream.Error(ex);
                }
                finally
                {

                }
            }
            else
            {
                var ex = new FileNotFoundException(
                    SR.ResourceManager
                    .GetString(
                        FileNotFound,CultureInfo.CurrentCulture));
                //
                // 推送错误通知
                _ProjectStream.Error(ex);
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

                if (_Current != null)
                {
                    _Current.Dispose();
                    _Current = null;
                }

                _DisposedValue = true;
            }
        }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Properties
        //
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        public IObservable<IMorisaProjectInfo> ProjectInfo => _ProjectInfoStream;

        /// <summary>
        /// 
        /// </summary>
        public IObservable<IMorisaProject> Project => _ProjectStream;

        // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        ~MorisaProjectManager()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: false);
        }
    }
}
