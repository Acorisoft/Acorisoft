using Acorisoft.Morisa.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    interface ICompositionSetFactoryImpl
    {
        /// <summary>
        /// 用于为 IDataSetFactory 提供设定集变更订阅。
        /// </summary>
        IObservable<ICompositionSetContext> CompositionSetStream { get; }

        /// <summary>
        /// 用于为订阅设定集信息变化。
        /// </summary>
        IObservable<ICompositionSetInformation> InformationStream { get; }
    }

    /// <summary>
    /// <see cref="ICompositionSetFactory"/> 接口用于定义一个设定集工厂，用于实现对设定集的访问。
    /// </summary>
    public interface ICompositionSetFactory : IDisposable
    {
        void Load(IGenerateContext<ICompositionSetInformation> context);
        void Load(IStoreContext context);
        void Load(string target);
    }
}
