using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Extensions.Platforms.Windows.Controls
{
    /// <summary>
    /// <see cref="IFolderField"/> 表示一个文件夹字段控件。
    /// </summary>
    public interface IFolderField
    {
        string Directory { get; set; }
    }
}
