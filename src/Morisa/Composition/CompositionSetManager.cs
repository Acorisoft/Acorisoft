using Acorisoft.Morisa.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acorisoft.Morisa.Core;
using DynamicData;
using System.Reactive.Disposables;
using Disposable = Acorisoft.Morisa.Core.Disposable;

namespace Acorisoft.Morisa.Composition
{
    /// <summary>
    /// <see cref="CompositionSetManager"/>
    /// </summary>
    public class CompositionSetManager : Disposable, ICompositionSetManager
    {
        private protected readonly ICompositionSetMediator                                  MediatorInstance;
        private protected readonly ReadOnlyObservableCollection<ICompositionSetContext>     OpeningInstance;

        private protected readonly SourceList<ICompositionSetContext>   ContextSource;
        private protected readonly HashSet<ICompositionSetContext>      ContextDistinct;

        private protected CompositionSetContext         ActivatingInstace;
        private readonly CompositeDisposable            _Disposable;

        public CompositionSetManager(ICompositionSetMediator mediator)
        {
            _Disposable = new CompositeDisposable();
            MediatorInstance = mediator ?? throw new ArgumentNullException(nameof(mediator));
            ContextSource = new SourceList<ICompositionSetContext>();
            ContextDistinct = new HashSet<ICompositionSetContext>();

            var disposable =  ContextSource.Connect()
                                           .Filter(x => !ContextDistinct.Contains(x))
                                           .Bind(out OpeningInstance)
                                           .Subscribe(x =>
                                           {
                                               foreach (var change in x)
                                               {
                                                   var item = change.Item.Current;
                                                   switch (change.Reason)
                                                   {
                                                       case ListChangeReason.Add:
                                                           _Disposable.Add(item);
                                                           break;
                                                       case ListChangeReason.Remove:
                                                           _Disposable.Remove(item);
                                                           break;
                                                   }
                                               }
                                           });
            _Disposable.Add(disposable);
        }

        protected override void OnReleaseManageCore()
        {
            base.OnReleaseManageCore();
            _Disposable.Dispose();
            ContextDistinct?.Clear();
            Disposed?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnReleaseUnmanageCore()
        {
            base.OnReleaseUnmanageCore();
            
            var newArray = new ICompositionSetContext[ContextSource.Count];

            OpeningInstance.CopyTo(newArray, 0);

            foreach(var opening in newArray)
            {
                opening.Dispose();
            }

            ContextSource.Clear();
        }

        /// <summary>
        /// 加载操作的后续处理。
        /// </summary>
        /// <param name="context">指定的要加载的上下文。该参数不能为空，并且保证不为空。</param>
        protected virtual void OnLoad(ILoadContext context, CompositionSetContext activatingContext)
        {
            Opened?.Invoke(ActivatingInstace);
        }

        /// <summary>
        /// 使用指定的加载上下文加载一个新的 <see cref="ICompositionSet"/> 类型实例。
        /// </summary>
        /// <param name="context">指定的加载上下文，要求不能为空。</param>
        public void Load(ILoadContext context)
        {
            if (context == null)
            {
                throw new InvalidOperationException(SR.DataSetManager_Load_Context_Null);
            }

            if (string.IsNullOrEmpty(context.FileName))
            {
                throw new InvalidOperationException(SR.DataSetManager_Load_FileName_Null);
            }

            if (!File.Exists(context.FileName))
            {
                throw new InvalidOperationException(SR.DataSetManager_Load_File_NotExists);
            }

            if (!Directory.Exists(context.Directory))
            {
                throw new InvalidOperationException(SR.CompositionSetManager_Load_Directory_NotExists);
            }

            //
            // 创建数据库
            var database = Helper.ToDatabase(context);

            //
            // 创建数据集
            var cs = new CompositionSet
            {
                DatabaseInstance = database,
                ObjectInstance = Helper.ToObject(database),
            };

            //
            // 反序列化属性。
            cs.Property = Helper.ToObject<CompositionSetProperty>(cs);

            //
            // 
            ActivatingInstace = new CompositionSetContext(cs, context);

            //
            // 添加到上下文
            ContextSource.Add(ActivatingInstace);

            //
            // 
            OnLoad(context, ActivatingInstace);
        }

        /// <summary>
        /// 使用指定的保存上下文创建一个新的 <see cref="ICompositionSet"/> 类型实例。
        /// </summary>
        /// <param name="context">指定的保存上下文，要求不能为空。</param>
        public void Generate(ISaveContext<TProperty> context)
        {

        }

        /// <summary>
        /// 关闭当前正在打开的 <see cref="ICompositionSet"/> 类型实例。
        /// </summary>
        public void Close()
        {

        }

        /// <summary>
        /// 切换到指定的创作集上下文。
        /// </summary>
        /// <param name="context">指定要切换的上下文，要求不能为空。</param>
        public void Switch(ICompositionSetContext context)
        {
            if(context == null)
            {
                throw new InvalidOperationException(SR.CompositionSetManager_Switch_Context_Null);
            }

            if (!ContextDistinct.Contains(context) || context is not CompositionSetContext)
            {
                throw new InvalidOperationException(SR.CompositionSetManager_Switch_Context_Invalid);
            }

            ActivatingInstace = (CompositionSetContext)context;

            //
            //
            Switched?.Invoke(context);
        }

        /// <summary>
        /// 获取当前创作集管理器的中介者。
        /// </summary>
        public IActivatingContext<CompositionSet, CompositionSetProperty> Activating => null;

        /// <summary>
        /// 获取当前创作集管理器的中介者。
        /// </summary>
        public ReadOnlyObservableCollection<ICompositionSetContext> Opening => OpeningInstance;

        /// <summary>
        /// 获取当前创作集管理器的中介者。
        /// </summary>
        public ICompositionSetMediator Mediator => MediatorInstance;

        /// <summary>
        /// 当 <see cref="ICompositionSetManager"/> 创作集管理器关闭新的创作集时触发。
        /// </summary>
        public event CompositionClosedHandler Closed;

        /// <summary>
        /// 当 <see cref="ICompositionSetManager"/> 创作集管理器打开新的创作集时触发。
        /// </summary>
        public event CompositionOpenedHandler Opened;

        /// <summary>
        /// 当 <see cref="ICompositionSetManager"/> 创作集管理器切换到新的创作集时触发。
        /// </summary>
        public event CompositionSwitchHandler Switched;

        /// <summary>
        /// 当 <see cref="ICompositionSetManager"/> 创作集管理器释放资源时触发。
        /// </summary>
        public event EventHandler Disposed;
    }
}
