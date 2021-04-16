using Acorisoft.Morisa.Composition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="CompositionOpenedHandler"/> 委托类型用于表示创作集加载时的处理程序。
    /// </summary>
    /// <param name="csc">指定当前操作的创作集上下文。</param>
    public delegate void CompositionOpenedHandler(ICompositionSetContext csc);

    /// <summary>
    /// <see cref="CompositionClosedHandler"/> 委托类型用于表示创作集关闭时的处理程序。
    /// </summary>
    /// <param name="csc">指定当前操作的创作集上下文。</param>
    public delegate void CompositionClosedHandler(ICompositionSetContext csc);

    /// <summary>
    /// <see cref="CompositionSwitchHandler"/> 委托类型用于表示创作集切换时的处理程序。
    /// </summary>
    /// <param name="csc">指定当前操作的创作集上下文。</param>
    public delegate void CompositionSwitchHandler(ICompositionSetContext csc);

    /// <summary>
    /// 
    /// </summary>
    public delegate void Callback();
}
