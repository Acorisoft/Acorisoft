using DryIoc;
using Splat.DryIoc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using System.Diagnostics.CodeAnalysis;

namespace Acorisoft.Morisa.Core
{
    /// <summary>
    /// <see cref="ApplicationEnvironment"/> 类型用于表示一个应用程序环境变量。主要保存着应用程序环境常量。
    /// </summary>
    public class ApplicationEnvironment : IApplicationEnvironment
    {
        private readonly IContainer _container;
        public ApplicationEnvironment()
        {
            // 使用最终可释放的瞬时对象规则初始化一个IOC容器。
            _container = new Container(Rules.Default.WithTrackingDisposableTransients());

            //
            // 启用指定的IOC依赖处理器
            _container.UseDryIocDependencyResolver();
        }

        [SuppressMessage("Usage", "CA1816:Dispose 方法应调用 SuppressFinalize", Justification = "<挂起>")]
        public void Dispose()
        {
            if (!_container.IsDisposed)
            {
                //
                // 释放容器
                _container.Dispose();
            }
        }

        /// <summary>
        /// 获取当前应用环境中的容器
        /// </summary>
        public IContainer Container => _container;

        #region Directory Methods And Properties

        public static string GetDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        #region Directory Properties

        /// <DirectoryStructure>
        /// \Morisa
        /// \Morisa\Bin
        /// \Morisa\Logs
        /// \Morisa\Data
        /// \Morisa\Patches
        /// </DirectoryStructure>

        /// <summary>
        /// 获取当前应用程序的应用目录。
        /// </summary>
        /// <remarks>
        /// <para>可运行程序目录与应用目录不是同一个位置，应用目录是应用程序包括其其他数据所在的目录的位置。</para>
        /// <para>一般为: %AppData%\Acorisoft\Morisa</para>
        /// </remarks>
        public static string ApplicationDirectory {
            get {
                return Directory.GetParent(Environment.CurrentDirectory)?.FullName;
            }
        }

        /// <summary>
        /// 获取当前应用程序的可运行程序目录。
        /// </summary>
        /// <remarks>
        /// <para>可运行程序目录与应用目录不是同一个位置，应用目录是应用程序包括其其他数据所在的目录的位置。</para>
        /// <para>一般为: %AppData%\Acorisoft\Morisa\Bin</para>
        /// </remarks>
        public static string BinaryFileDirectory {
            get {
                return Path.Combine(ApplicationDirectory, "Bin");
            }
        }

        /// <summary>
        /// 获取当前应用程序的日志目录。
        /// </summary>
        /// <remarks>
        /// <para>一般为: %AppData%\Acorisoft\Morisa\Logs</para>
        /// </remarks>
        public static string LogFileDirectory {
            get {
                return Path.Combine(ApplicationDirectory, "Logs");
            }
        }

        /// <summary>
        /// 获取当前应用程序的应用数据目录。
        /// </summary>
        /// <remarks>
        /// <para>一般为: %AppData%\Acorisoft\Morisa\Data</para>
        /// </remarks>
        public static string ApplicationDatabaseDirectory {
            get {
                return Path.Combine(ApplicationDirectory, "Data");
            }
        }

        /// <summary>
        /// 获取当前应用程序的升级包目录。
        /// </summary>
        /// <remarks>
        /// <para>一般为: %AppData%\Acorisoft\Morisa\Patches</para>
        /// </remarks>
        public static string PatchFileDirectory {
            get {
                return Path.Combine(ApplicationDirectory, "Patches");
            }
        }

        #endregion Directory Properties

        #endregion Directory Methods And Properties

        public CultureInfo Culture {
            get;
            set;
        }

        public event EventHandler CultureChanged;
    }
}
