using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace Acorisoft.Morisa.Composition
{
    /// <summary>
    /// <see cref="CompositionSetMediator"/> 类型表示一个创作集中介者。
    /// </summary>
    public class CompositionSetMediator : Mediator, ICompositionSetMediator
    {
        /// <summary>
        /// 初始化一个新的 <see cref="CompositionSetMediator"/> 类型实例。
        /// </summary>
        /// <param name="factory"><see cref="ServiceFactory"/> 委托类型。参数要求不能为空。</param>
        public CompositionSetMediator(ServiceFactory factory) : base(factory)
        {

        }
    }
}
