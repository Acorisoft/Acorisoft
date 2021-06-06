using System;
using LiteDB;

namespace Acorisoft.Studio.Systems
{
    /// <summary>
    /// <see cref="IComposeSet"/> 接口表示一个抽象的创作集接口。
    /// </summary>
    public interface IComposeSet : IDisposable
    {
        /// <summary>
        /// 获取当前创作集的路径。
        /// </summary>
        string Path { get; }

        /// <summary>
        /// 获取指定已知目录的路径
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        string GetComposeSetPath(ComposeSetKnownFolder folder);
        
        /// <summary>
        /// 获取当前创作集的属性。
        /// </summary>
        IComposeSetProperty Property { get; set; }
        
        /// <summary>
        /// 获取清单列表。
        /// </summary>
        public string AutoSaveManifestFileName { get; }
        
    }

    interface IComposeSetDatabase
    {
        LiteDatabase MainDatabase { get; }
    }
}