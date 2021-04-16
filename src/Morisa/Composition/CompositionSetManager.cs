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
using Acorisoft.Morisa.EventBus;
using Splat;
using PropertyHandler = Acorisoft.Morisa.Core.IDataPropertyHandler<Acorisoft.Morisa.Composition.CompositionSetProperty>;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Acorisoft.Morisa.Composition
{
    /// <summary>
    /// <see cref="CompositionSetManager"/>
    /// </summary>
    public class CompositionSetManager : Disposable, ICompositionSetManager
    {
        private static readonly Callback Empty = ()=>{ };

        //-------------------------------------------------------------------------------------------------
        //
        //  Variables
        //
        //-------------------------------------------------------------------------------------------------
        private protected readonly ICompositionSetMediator                                  MediatorInstance;
        private protected readonly ReadOnlyObservableCollection<ICompositionSetContext>     OpeningInstance;

        private protected readonly SourceList<ICompositionSetContext>   ContextSource;
        private protected readonly BehaviorSubject<IPageRequest>        ContextPager;
        private protected readonly HashSet<ICompositionSetContext>      ContextDistinct;

        private protected CompositionSetContext         ActivatingInstace;
        private readonly CompositeDisposable            _Disposable;
        private readonly IFullLogger                    _Logger;
        private readonly PropertyHandler                _PropertyHandler;

        //-------------------------------------------------------------------------------------------------
        //
        //  Variables
        //
        //-------------------------------------------------------------------------------------------------
        public CompositionSetManager(ICompositionSetMediator mediator, ILogManager logMrg, IDataPropertyManager dpMgr)
        {
            _Disposable = new CompositeDisposable();
            _Logger = logMrg.GetLogger(GetType());
            _PropertyHandler = dpMgr.GetManager<CompositionSetProperty, PropertyHandler>();

            MediatorInstance = mediator ?? throw new ArgumentNullException(nameof(mediator));
            ContextSource = new SourceList<ICompositionSetContext>();
            ContextDistinct = new HashSet<ICompositionSetContext>();
            ContextPager = new BehaviorSubject<IPageRequest>(new PageRequest(1, 25));

            var disposable =  ContextSource.Connect()
                                           .Page(ContextPager)
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
                                                           ContextDistinct.Add(item);
                                                           break;
                                                       case ListChangeReason.Remove:
                                                           _Disposable.Remove(item);
                                                           ContextDistinct.Remove(item);
                                                           break;
                                                   }
                                               }
                                           });
            _Disposable.Add(disposable);
            _Disposable.Add(ContextPager);
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Variables
        //
        //-------------------------------------------------------------------------------------------------

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

            foreach (var opening in newArray)
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
            //
            // 记录
            _Logger.Info(string.Format(SR.CompositionSetManager_Load_Success, activatingContext.Name));
            _Logger.Info(SR.CompositionSetManager_Load_Notification);

            MediatorInstance?.Publish(new CompositionSetOpeningInstruction
            {
                Context = activatingContext
            });

            Opened?.Invoke(ActivatingInstace);
        }

        /// <summary>
        /// 加载操作的后续处理。
        /// </summary>
        /// <param name="context">指定的要加载的上下文。该参数不能为空，并且保证不为空。</param>

        protected virtual void OnLoad(ISaveContext context, CompositionSetContext activatingContext)
        {
            //
            // 记录
            _Logger.Info(string.Format(SR.CompositionSetManager_Load_Success, activatingContext.Name));
            _Logger.Info(SR.CompositionSetManager_Load_Notification);

            MediatorInstance?.Publish(new CompositionSetOpeningInstruction
            {
                Context = activatingContext
            });

            Opened?.Invoke(ActivatingInstace);
        }

        /// <summary>
        /// 关闭操作的后续处理。
        /// </summary>
        /// <param name="context">指定的要关闭的上下文。该参数不能为空，并且保证不为空。</param>
        protected virtual void OnClose(CompositionSetContext activatingContext)
        {
            //
            // 通知组件更新
            MediatorInstance?.Publish(new CompositionSetClosingInstruction());


            Closed?.Invoke(activatingContext);

            //
            // 释放资源
            activatingContext.Dispose();
        }



        //-------------------------------------------------------------------------------------------------
        //
        //  Variables
        //
        //-------------------------------------------------------------------------------------------------

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

            try
            {  //
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
                // 推送
                OnLoad(context, ActivatingInstace);
            }
            catch(Exception ex)
            {
                OpenFailed?.Invoke(this, ex);
            }
        }

        /// <summary>
        /// 使用指定的保存上下文创建一个新的 <see cref="ICompositionSet"/> 类型实例。
        /// </summary>
        /// <param name="context">指定的保存上下文，要求不能为空。</param>
        public void Generate(ISaveContext<CompositionSetProperty> context)
        {
            if (context == null)
            {
                throw new InvalidOperationException(SR.CompositionSetManager_Generate_Context_Null);
            }

            if (context.Property == null)
            {
                throw new InvalidOperationException(SR.CompositionSetManager_Generate_Context_Property_Null);
            }

            if (string.IsNullOrEmpty(context.Name))
            {
                throw new InvalidOperationException(SR.CompositionSetManager_Generate_Context_Name_Empty);
            }

            if (string.IsNullOrEmpty(context.FileName))
            {
                throw new InvalidOperationException(SR.CompositionSetManager_Generate_Context_FileName_Empty);
            }

            if (string.IsNullOrEmpty(context.Directory))
            {
                throw new InvalidOperationException(SR.CompositionSetManager_Generate_Context_Directory_Empty);
            }

            try
            {
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
                // 序列化属性。
                cs.Property = Helper.ToObject<CompositionSetProperty>(cs, context.Property);

                //
                // 处理属性中的资源
                _PropertyHandler?.Handle(context.Property, Empty);

                //
                // 
                ActivatingInstace = new CompositionSetContext(cs, context);

                //
                // 添加到上下文
                ContextSource.Add(ActivatingInstace);

                //
                // 推送
                OnLoad(context, ActivatingInstace);
            }
            catch(Exception ex)
            {
                OpenFailed?.Invoke(this, ex);
            }
        }

        /// <summary>
        /// 关闭当前正在打开的 <see cref="ICompositionSet"/> 类型实例。
        /// </summary>
        public void Close()
        {
            if (ActivatingInstace == null)
            {
                return;
            }

            if (ContextDistinct.Contains(ActivatingInstace))
            {
                //
                // 移除当前上下文
                ContextSource.Remove(ActivatingInstace);

                //
                // 记录
                _Logger.Info(string.Format(SR.CompositionSetManager_Close_Success, ActivatingInstace.Name));
                _Logger.Info(SR.CompositionSetManager_Load_Notification);

                OnClose(ActivatingInstace);
            }
        }

        /// <summary>
        /// 切换到指定的创作集上下文。
        /// </summary>
        /// <param name="context">指定要切换的上下文，要求不能为空。</param>
        public void Switch(ICompositionSetContext context)
        {
            if (context == null)
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
        /// 更新当前活跃的创作集属性。
        /// </summary>
        /// <param name="property">指定要更新的创作集属性，要求不能为空。</param>
        public void Update(CompositionSetProperty property)
        {
            if (property == null)
            {
                return;
            }

            //
            // 异步更新属性。
            _PropertyHandler.Handle(property, () =>
            {
                PropertyChanged?.Invoke(this, new EventArgs());
            });
        }

        /// <summary>
        /// 切换到指定的创作集上下文。
        /// </summary>
        /// <param name="context">指定要切换的上下文，要求不能为空。</param>
        public void Switch(IActivatingContext<CompositionSet, CompositionSetProperty> context)
        {
            if (context == null)
            {
                throw new InvalidOperationException(SR.CompositionSetManager_Switch_Context_Null);
            }

            if (!ContextDistinct.Contains(context) || context is not CompositionSetContext)
            {
                throw new InvalidOperationException(SR.CompositionSetManager_Switch_Context_Invalid);
            }

            if (context is ICompositionSetContext csc)
            {

                ActivatingInstace = (CompositionSetContext)csc;

                //
                //
                Switched?.Invoke(csc);
            }
        }







        //-------------------------------------------------------------------------------------------------
        //
        //  Properties
        //
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// 获取当前创作集管理器的中介者。
        /// </summary>
        public IActivatingContext<CompositionSet, CompositionSetProperty> Activating => ActivatingInstace;

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

        /// <summary>
        /// 当 <see cref="ICompositionSetManager"/> 创作集管理器的活跃创作集属性更新时触发。
        /// </summary>
        public event EventHandler PropertyChanged;

        /// <summary>
        /// 当 <see cref="ICompositionSetManager"/> 创作集管理器的活跃创作集属性加载时触发。
        /// </summary>
        public event ExceptionHandler OpenFailed;
    }
}
