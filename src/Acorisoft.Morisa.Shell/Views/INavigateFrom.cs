using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Views
{
    /// <summary>
    /// <see cref="INavigateFrom"/> 接口用于表示一个抽象的导航接口，用于为视图模型提供参数传递支持。
    /// </summary>
    public interface INavigateFrom
    {
        void NavigateFrom(INavigateContext context);
    }
}
