using Acorisoft.Morisa.Internal;
using DynamicData;
using DynamicData.Binding;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Joins;
using System.Reactive.Linq;
using System.Reactive.PlatformServices;
using System.Reactive.Subjects;
using System.Reactive.Threading;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using DryIoc;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="DataFactory"/> 表示一个数据工厂基类，为数据操作提供基础的支持。
    /// </summary>
    public abstract class DataFactory : IDataFactory
    {
        private protected readonly DelegateObserver<ICompositionSet>            CompositionSet;
        private protected readonly BehaviorSubject<IPageRequest>                PagerStream;   // Page

        protected DataFactory()
        {
            CompositionSet = new DelegateObserver<ICompositionSet>(CompositionSetChanged);
            PagerStream = new BehaviorSubject<IPageRequest>(new PageRequest(1, 25));
        }

        protected void CompositionSetChanged(ICompositionSet set)
        {
            if(set != null)
            {
                if (DetermineDataSetInitialization(set))
                {
                    InitializeFromDatabase(set);
                }
                else
                {
                    InitializeFromPattern(set);
                }

                OnCompositionSetChanged(set);
            }
        }

        /// <summary>
        /// 使用数据库中的数据初始化。
        /// </summary>
        /// <param name="set">指定此初始化操作所需要用到的数据库上下文，要求不能为空。</param>
        protected virtual void InitializeFromDatabase(ICompositionSet set)
        {

        }

        /// <summary>
        /// 数据库未初始化，使用指定的模式来初始化数据库并写入数据。。
        /// </summary>
        /// <param name="set">指定此初始化操作所需要用到的数据库上下文，要求不能为空。</param>
        protected virtual void InitializeFromPattern(ICompositionSet set)
        {

        }

        /// <summary>
        /// 当设定集发生改变时调用该方法。这是一个PostProcess操作。
        /// </summary>
        /// <param name="set">当前的设定集上下文。</param>
        protected virtual void OnCompositionSetChanged(ICompositionSet set)
        {

        }

        /// <summary>
        /// 指示当前的数据库是否初始化。
        /// </summary>
        /// <param name="cs">指定要判断初始化状态的数据库，要求参数不能为。</param>
        protected virtual bool DetermineDataSetInitialization(ICompositionSet cs)
        {
            return false;
        }

        /// <summary>
        /// 获取当前的输入流。当前输入流是一个设定集。
        /// </summary>

        public IObserver<ICompositionSet> Input => CompositionSet;

        /// <summary>
        /// 获取一个页面请求流。
        /// </summary>
        public IObserver<IPageRequest> Pager => PagerStream;
    }
}
