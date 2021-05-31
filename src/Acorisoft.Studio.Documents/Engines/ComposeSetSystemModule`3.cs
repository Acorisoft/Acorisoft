using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Acorisoft.Studio.Documents;
using Acorisoft.Studio.ProjectSystems;
using DynamicData;
using LiteDB;
using PlatformDisposable = Acorisoft.Extensions.Platforms.Disposable;

namespace Acorisoft.Studio.Engines
{
    public abstract class ComposeSetSystemModule<TIndex, TIndexWrapper, TComposition> : PlatformDisposable,
        IComposeSetSystemModule, IComposeSetSystemModule<TIndex, TIndexWrapper, TComposition>
        where TIndex : DocumentIndex
        where TIndexWrapper : DocumentIndexWrapper<TIndex>
        where TComposition : Document
    {
        //-----------------------------------------------------------------------
        //
        //  Read-Only Fields
        //
        //-----------------------------------------------------------------------
        protected readonly CompositeDisposable Disposable;
        protected readonly BehaviorSubject<bool> IsOpenStream;
        protected readonly BehaviorSubject<int> PerPageItemCountStream;
        protected readonly BehaviorSubject<int> PageIndexStream;
        protected readonly BehaviorSubject<int> PageCountStream;
        protected readonly BehaviorSubject<int> CountStream;
        protected readonly BehaviorSubject<IComparer<TIndexWrapper>> PageSorterStream;
        protected readonly BehaviorSubject<Func<TIndexWrapper, bool>> PageFilterStream;
        protected readonly ReadOnlyObservableCollection<TIndexWrapper> BindableCollection;
        protected readonly SourceList<TIndex> EditableCollection;
        protected readonly Stack<Tuple<int, int>> PageIndexStack;

        //-----------------------------------------------------------------------
        //
        //  Constructors
        //
        //-----------------------------------------------------------------------

        protected ComposeSetSystemModule(Func<TIndex, TIndexWrapper> transformer, IComposeSetRequestQueue requestQueue,
            string indexName, string documentName)
        {
            if (string.IsNullOrEmpty(indexName))
            {
                throw new ArgumentNullException(nameof(indexName));
            }

            if (string.IsNullOrEmpty(documentName))
            {
                throw new ArgumentNullException(nameof(documentName));
            }

            if (transformer == null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            IndexCollectionName = indexName;
            DocumentCollectionName = documentName;
            RequestQueue = requestQueue ?? throw new ArgumentNullException(nameof(requestQueue));
            IsOpenStream = new BehaviorSubject<bool>(false);
            CountStream = new BehaviorSubject<int>(0);
            PerPageItemCountStream = new BehaviorSubject<int>(10);
            PageCountStream = new BehaviorSubject<int>(0);
            PageIndexStream = new BehaviorSubject<int>(0);
            PageSorterStream = new BehaviorSubject<IComparer<TIndexWrapper>>(Comparer<TIndexWrapper>.Default);
            PageFilterStream = new BehaviorSubject<Func<TIndexWrapper, bool>>(NotNullFilter);
            EditableCollection = new SourceList<TIndex>();
            PageIndexStack = new Stack<Tuple<int, int>>();
            PerPageItemCountField = 10;

            var disposable = EditableCollection.Connect()
                .Transform(transformer)
                .Filter(PageFilterStream)
                .Sort(PageSorterStream)
                .Bind(out BindableCollection).Subscribe();
            var disposable1 = PageIndexStream.Subscribe(x => DemandRefreshDataSource());
            var disposable2 = PerPageItemCountStream.Subscribe(x => DemandRefreshDataSource());

            Disposable = new CompositeDisposable
            {
                disposable,
                disposable1,
                disposable2,
                IsOpenStream,
                PerPageItemCountStream,
                PageCountStream,
                PageIndexStream,
                PageSorterStream,
                PageFilterStream,
                CountStream
            };
        }

        protected sealed override void OnDisposeManagedCore()
        {
            if (!Disposable.IsDisposed)
            {
                Disposable.Dispose();
            }
        }

        protected static bool NotNullFilter(TIndexWrapper wrapper) => wrapper != null;

        #region IComposeSetSystemModule Interface Implments

        //-----------------------------------------------------------------------
        //
        //  NewAsync
        //
        //-----------------------------------------------------------------------

        #region NewAsync

        private void NewAsyncImpl(INewItemInfo<TComposition> info)
        {
            //
            // 检测是否已经加载创作集。
            if (!IsOpenField)
            {
                return;
            }

            if (info == null)
            {
                throw new InvalidOperationException(nameof(info));
            }

            if (string.IsNullOrEmpty(info.Name))
            {
                info.Name = SR.ComposeSetSystemModule_EmptyName;
            }

            NewCore(info);
        }

        /// <summary>
        /// 创建索引实例。
        /// </summary>
        /// <returns>返回新创建的索引实例。</returns>
        protected abstract TIndex CreateIndexInstance();

        /// <summary>
        /// 创建文档实例。
        /// </summary>
        /// <returns>返回新创建的文档实例。</returns>
        protected abstract TComposition CreateCompositionInstance();

        /// <summary>
        /// 从文档中抽取索引内容
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="composition">文档</param>
        protected virtual void ExtractIndex(TIndex index, TComposition composition)
        {
        }


        /// <summary>
        /// 在一个异步操作中创建一个新的项目。
        /// </summary>
        /// <param name="info">指定要创建的操作。</param>
        protected virtual void NewCore(INewItemInfo<TComposition> info)
        {
            //
            // 进入创建逻辑
            //
            // 创建实例
            var index = CreateIndexInstance();
            var document = CreateCompositionInstance();

            //
            // 设置同样的唯一标识符。
            index.Id = document.Id = info.Id;
            
            //
            // 设置名称
            document.Name = index.Name = info.Name;

            //
            // 抽取关键字到索引当中
            ExtractIndex(index, document);

            //
            // 插入
            IndexCollection.Insert(index);
            DocumentCollection.Insert(document);

            //
            // 刷新集合
            DemandRefreshDataSource();
        }

        /// <summary>
        /// 在一个异步操作中创建一个新的项目。
        /// </summary>
        /// <param name="info">指定要创建的操作。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task NewAsync(INewItemInfo<TComposition> info)
        {
            return Task.Run(() => NewAsyncImpl(info));
        }

        #endregion

        //-----------------------------------------------------------------------
        //
        //  OpenAsync
        //
        //-----------------------------------------------------------------------

        #region OpenAsync

        private TComposition OpenImpl(TIndex index)
        {
            //
            // 检测是否已经加载创作集。
            if (!IsOpenField)
            {
                throw new InvalidOperationException("无法加载文档,创作集未加载。");
            }

            if (index == null || index.Id == Guid.Empty)
            {
                throw new InvalidOperationException("无法加载文档,错误的索引。");
            }

            return OpenCore(index);
        }

        protected virtual TComposition OpenCore(TIndex index)
        {
            return DocumentCollection.FindById(index.Id);
        }

        /// <summary>
        /// 打开文档
        /// </summary>
        /// <param name="index">要打开的索引。</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task<TComposition> OpenAsync(TIndex index)
        {
            return Task.Run(() => OpenImpl(index));
        }

        #endregion

        //-----------------------------------------------------------------------
        //
        //  FindAsync / ResetAsync
        //
        //-----------------------------------------------------------------------

        #region FindAsync

        private void FindImpl(string keyword)
        {
            if (!IsOpenField)
            {
                return;
            }

            if (string.IsNullOrEmpty(keyword))
            {
                return;
            }

            //
            // 入栈
            var context = new Tuple<int, int>(PageIndexField, PerPageItemCountField);
            PageIndexStack.Push(context);

            FindCore(keyword);
        }

        private void ResetImpl()
        {
            if (!IsOpenField)
            {
                return;
            }

            if (PageIndexStack.Count > 0)
            {
                var (pageIndex, perPageItemCount) = PageIndexStack.Pop();
                PageIndex = pageIndex;
                PerPageItemCount = perPageItemCount;
            }

            ResetCore();
        }

        protected virtual Query ConstructFindExpression(string keyword)
        {
            return Query.Contains("Name", keyword);
        }


        protected void ResetCore()
        {
            //
            // 清空当前页面内容 
            EditableCollection.Clear();


            CollectionEnumerator ??= IndexCollection.FindAll();


            //
            // 重设页面索引位置。
            PageIndex = 1;

            //
            // 刷新内容
            DemandRefreshDataSource();
        }

        protected virtual void FindCore(string keyword)
        {
            //
            //
            CollectionEnumerator = IndexCollection.Find(ConstructFindExpression(keyword));

            //
            // 重设页面索引位置。
            PageIndex = 1;

            //
            // 刷新内容
            DemandRefreshDataSource();
        }

        /// <summary>
        /// 重置搜索
        /// </summary>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task ResetAsync()
        {
            return Task.Run(ResetImpl);
        }


        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task FindAsync(string keyword)
        {
            return Task.Run(() => FindImpl(keyword));
        }

        #endregion


        //-----------------------------------------------------------------------
        //
        //  UpdateAsync
        //
        //-----------------------------------------------------------------------

        #region UpdateAsync

        private void UpdateImpl(TIndexWrapper wrapper, TIndex index, TComposition document)
        {
            if (!IsOpenField)
            {
                return;
            }

            UpdateCore(wrapper, index, document);
        }

        private void UpdateImpl(TComposition document)
        {
            if (!IsOpenField)
            {
                return;
            }

            UpdateCore(document);
        }

        protected virtual void UpdateCore(TIndexWrapper wrapper, TIndex index, TComposition document)
        {
            //
            // 从文档中提取内容
            ExtractIndex(index, document);

            //
            // 往容器中设置新的属性
            wrapper.RaiseUpdated();

            //
            // 更新索引
            IndexCollection.Upsert(index);

            //
            // 更新文档
            DocumentCollection.Upsert(document);
        }

        protected virtual void UpdateCore(TComposition document)
        {
            var index = IndexCollection.FindById(document.Id);

            //
            // 从文档中提取内容
            ExtractIndex(index, document);

            //
            // 刷新
            DemandRefreshDataSource();

            //
            // 更新索引
            IndexCollection.Upsert(index);

            //
            // 更新文档
            DocumentCollection.Upsert(document);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public Task UpdateAsync(TComposition document)
        {
            return Task.Run(() => UpdateImpl(document));
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="wrapper"></param>
        /// <param name="index"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public Task UpdateAsync(TIndexWrapper wrapper, TIndex index, TComposition document)
        {
            return Task.Run(() => UpdateImpl(wrapper, index, document));
        }

        #endregion


        //-----------------------------------------------------------------------
        //
        //  DeleteThisAsync / DeleteThisPageAsync / DeleteAllAsync
        //
        //-----------------------------------------------------------------------

        #region DeleteThisAsync / DeleteThisPageAsync / DeleteAllAsync

        private void DeleteThisImpl(TIndex index)
        {
            if (!IsOpenField)
            {
                return;
            }

            DeleteThisCore(index.Id);
            DemandRefreshDataSource();
        }

        private void DeleteThisImpl(TComposition document)
        {
            if (!IsOpenField)
            {
                return;
            }

            DeleteThisCore(document.Id);
            DemandRefreshDataSource();
        }

        private void DeleteThisPageImpl()
        {
            if (!IsOpenField)
            {
                return;
            }

            DeleteThisPageCore();
            DemandRefreshDataSource();
        }

        private void DeleteAllImpl()
        {
            if (!IsOpenField)
            {
                return;
            }

            DeleteAllCore();
            DemandRefreshDataSource();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        protected virtual void DeleteThisCore(Guid id)
        {
            //
            // 清除当前ID
            IndexCollection.Delete(id);

            //
            // 索引ID与文档ID相同，移除
            DocumentCollection.Delete(id);
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void DeleteThisPageCore()
        {
            var thisPageItems = IndexCollection.FindAll()
                .Skip(Math.Min(PerPageItemCountField * (PageIndexField - 1), CountField))
                .Take(Math.Min(PerPageItemCountField, CountField))
                .ToArray();

            foreach (var item in thisPageItems)
            {
                //
                // 清除当前ID
                IndexCollection.Delete(item.Id);

                //
                // 索引ID与文档ID相同，移除
                DocumentCollection.Delete(item.Id);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void DeleteAllCore()
        {
            //
            // 清空
            EditableCollection.Clear();

            //
            // 清除所有索引
            IndexCollection.Delete(Query.All());

            //
            // 清除所有文档
            DocumentCollection.Delete(Query.All());
        }


        /// <summary>
        /// 删除这个文档
        /// </summary>
        /// <param name="index"></param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>        
        public Task DeleteThisAsync(TIndex index)
        {
            return Task.Run(() => DeleteThisImpl(index));
        }

        /// <summary>
        /// 删除这个文档
        /// </summary>
        /// <param name="document"></param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task DeleteThisAsync(TComposition document)
        {
            return Task.Run(() => DeleteThisImpl(document));
        }

        /// <summary>
        /// 删除这个页面
        /// </summary>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task DeleteThisPageAsync()
        {
            return Task.Run(DeleteThisPageImpl);
        }

        /// <summary>
        /// 删除全部数据
        /// </summary>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task DeleteAllAsync()
        {
            return Task.Run(DeleteAllImpl);
        }

        #endregion

        //-----------------------------------------------------------------------
        //
        //  DemandRefreshDataSource
        //
        //-----------------------------------------------------------------------

        #region DemandRefreshDataSource / OnRefreshDataSource

        /// <summary>
        /// 刷新数据源
        /// </summary>
        private void DemandRefreshDataSource()
        {
            if (IndexCollection == null)
            {
                return;
            }

            if (IndexCollection.Count() == 0)
            {
                PageIndexField = 0;
            }

            if (PerPageItemCountField == 0)
            {
                PerPageItemCountField = 10;
            }

            OnRefreshDataSource(PageIndexField, PerPageItemCountField);
        }

        /// <summary>
        /// 刷新数据源
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="perPageItemCount"></param>
        protected virtual void OnRefreshDataSource(int pageIndex, int perPageItemCount)
        {
            //
            // 保证页面的数量处于正常的范围内
            pageIndex = Math.Clamp(pageIndex, 1, ushort.MaxValue);
            perPageItemCount = Math.Clamp(perPageItemCount, 1, byte.MaxValue);

            //
            // 所有内容的数量
            CountField = IndexCollection.Count();
            CountStream.OnNext(CountField);

            //
            // 所有页面数
            PageCountField = (CountField + PerPageItemCountField - 1) / PerPageItemCountField;
            PageCountStream.OnNext(PageCountField);


            //
            // 清空当前内容 
            EditableCollection.Clear();

            //
            // 判断使用的枚举器，如果为空则使用 FindAll 枚举器，否则使用 Search 枚举器
            CollectionEnumerator ??= IndexCollection.FindAll();

            //
            // 获取内容
            var thisPageEnumeration = CollectionEnumerator
                .Skip(perPageItemCount * (pageIndex - 1))
                .Take(perPageItemCount)
                .ToArray();
            //
            // 添加内容
            EditableCollection.AddRange(thisPageEnumeration);
        }

        #endregion

        #endregion

        /// <summary>
        /// 当创作集打开时处理数据加载任务。
        /// </summary>
        /// <param name="instruction"></param>
        protected virtual void OnComposeSetOpening(ComposeSetOpenInstruction instruction)
        {
            if (instruction?.ComposeSetDatabase == null)
            {
                throw new InvalidOperationException();
            }

            //
            // 获取数据库
            var database = instruction.ComposeSetDatabase.MainDatabase;

            IndexCollection = database.GetCollection<TIndex>(IndexCollectionName);
            DocumentCollection = database.GetCollection<TComposition>(DocumentCollectionName);

            //
            // 初始化页面
            PageIndexField = 1;
            PageIndexStream.OnNext(PageIndexField);

            //
            // 刷新内容
            DemandRefreshDataSource();
        }

        protected virtual void OnComposeSetClosing(ComposeSetCloseInstruction instruction)
        {
            //
            // 清空内容以及程序状态
            DocumentCollection = null;
            IndexCollection = null;
            EditableCollection.Clear();

            //
            // 所有内容的数量
            CountField = 0;
            CountStream.OnNext(CountField);

            //
            // 初始化页面
            PageIndexField = 0;
            PageIndexStream.OnNext(PageIndexField);


            //
            // 所有页面数
            PageCountField = 0;
            PageCountStream.OnNext(PageCountField);

            //
            // 刷新内容
            DemandRefreshDataSource();
        }

        protected virtual void OnComposeSetSaving(ComposeSetSaveInstruction instruction)
        {
        }


        #region INotificationHandler<> Interface Implements

        private void HandleComposeSetOpen(ComposeSetOpenInstruction instruction)
        {
            RequestQueue.Set();
            OnComposeSetOpening(instruction);
            RequestQueue.Unset();
            IsOpenField = true;
        }

        private void HandleComposeSetClose(ComposeSetCloseInstruction instruction)
        {
            RequestQueue.Set();
            OnComposeSetClosing(instruction);
            RequestQueue.Unset();
            IsOpenField = false;
        }

        private void HandleComposeSetSave(ComposeSetSaveInstruction instruction)
        {
            RequestQueue.Set();
            OnComposeSetSaving(instruction);
            RequestQueue.Unset();
        }

        /// <summary>
        /// 处理消息推送
        /// </summary>
        /// <param name="instruction">接收的推送。</param>
        /// <param name="cancellationToken">取消当前任务的Token</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task Handle(ComposeSetOpenInstruction instruction, CancellationToken cancellationToken)
        {
            return Task.Run(() => HandleComposeSetOpen(instruction), cancellationToken);
        }

        /// <summary>
        /// 处理消息推送
        /// </summary>
        /// <param name="instruction">接收的推送。</param>
        /// <param name="cancellationToken">取消当前任务的Token</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task Handle(ComposeSetCloseInstruction instruction, CancellationToken cancellationToken)
        {
            return Task.Run(() => HandleComposeSetClose(instruction), cancellationToken);
        }

        /// <summary>
        /// 处理消息推送
        /// </summary>
        /// <param name="instruction">接收的推送。</param>
        /// <param name="cancellationToken">取消当前任务的Token</param>
        /// <returns>返回此次操作的 <see cref="Task"/> 实例</returns>
        public Task Handle(ComposeSetSaveInstruction instruction, CancellationToken cancellationToken)
        {
            return Task.Run(() => HandleComposeSetSave(instruction), cancellationToken);
        }


        /// <summary>
        /// 获取当前画廊的操作状态
        /// </summary>
        public IObservable<bool> IsOpen => IsOpenStream;

        #endregion


        //-----------------------------------------------------------------------
        //
        //  Protected Properties
        //
        //-----------------------------------------------------------------------


        #region Protected Properties

        protected int CountField { get; private set; }
        protected string IndexCollectionName { get; }
        protected string DocumentCollectionName { get; }
        protected LiteCollection<TIndex> IndexCollection { get; private set; }
        protected LiteCollection<TComposition> DocumentCollection { get; private set; }
        protected IEnumerable<TIndex> CollectionEnumerator { get; private set; }
        protected IComposeSetRequestQueue RequestQueue { get; }
        protected int PerPageItemCountField { get; private set; }
        protected int PageIndexField { get; private set; }
        protected int PageCountField { get; private set; }
        protected bool IsOpenField { get; private set; }
        protected IComparer<TIndexWrapper> SorterField { get; private set; }
        protected Func<TIndexWrapper, bool> FilterField { get; private set; }

        #endregion


        //-----------------------------------------------------------------------
        //
        //  Public Properties
        //
        //-----------------------------------------------------------------------

        #region Properties

        public IObservable<int> Count => CountStream;

        /// <summary>
        /// 获取当前画廊的页面数量
        /// </summary>
        /// <remarks>
        /// <para>这个属性值必须在[1,65536]之间</para>
        /// </remarks>
        public IObservable<int> PageCount => PageCountStream;

        /// <summary>
        /// 获取或设置当前画廊中每个页面中元素的数量。
        /// </summary>
        /// <remarks>
        /// <para>这个属性值必须在[1,255]之间</para>
        /// </remarks>
        public int PerPageItemCount
        {
            get => PerPageItemCountField;
            set
            {
                PerPageItemCountField = value;
                PerPageItemCountStream.OnNext(PerPageItemCountField);
            }
        }

        /// <summary>
        /// 获取或设置当前画廊的页面位置。
        /// </summary>
        /// <remarks>
        /// <para>这个属性值必须在[1,65536]之间</para>
        /// </remarks>
        public int PageIndex
        {
            get => PageIndexField;
            set
            {
                PageIndexField = value;
                PageIndexStream.OnNext(PageIndexField);
            }
        }

        /// <summary>
        /// 获取或设置当前画廊的过滤器。
        /// </summary>
        public Func<TIndexWrapper, bool> Filter
        {
            get => FilterField;
            set
            {
                FilterField = value;
                PageFilterStream.OnNext(FilterField);
            }
        }

        /// <summary>
        /// 获取或设置当前画廊的排序器。
        /// </summary>
        public IComparer<TIndexWrapper> Sorter
        {
            get => SorterField;
            set
            {
                SorterField = value;
                PageSorterStream.OnNext(SorterField);
            }
        }


        /// <summary>
        /// 获取当前 <see cref="IComposeSetSystemModule{TIndex,TIndexWrapper,TComposition}"/> 的可绑定集合。
        /// </summary>
        public ReadOnlyObservableCollection<TIndexWrapper> Collection => BindableCollection;

        #endregion
    }
}