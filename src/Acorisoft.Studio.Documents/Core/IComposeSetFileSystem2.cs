using System;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;
using Acorisoft.Studio.Resources;

namespace Acorisoft.Studio.Systems
{
    /// <summary>
    /// <see cref="IComposeSetFileSystem2"/> 接口用于表示一个抽象的创作集文件系统接口，它是基础文件系统 <see cref="IComposeSetFileSystem"/> 的升级版。
    /// </summary>
    public interface IComposeSetFileSystem2
    {
        //
        // IComposeSetFileSystem2 相较于 IComposeSetFileSystem 接口，将会改进更多的内容
        //
        // 首先IComposeSetFileSystem2接口，它是IComposeSetFileSystem接口的第二个版本，虽然会兼容IComposeSetFileSystem第一版本的接口
        // 但是只是作为过渡使用。
        // IComposeSetFileSystem2 接口将会提供:
        //
        // 1. 对于 Autosave 提供更完善的支持。

        #region AutoSave API
        

        #endregion
    }
}