using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Acorisoft.Studio.Documents;
using Acorisoft.Studio.ProjectSystem;
using DynamicData;
using LiteDB;

namespace Acorisoft.Studio.Engines
{
    /// <summary>
    /// <see cref="DocumentGalleryEngine{TIndex,TIndexWrapper,TDocument}"/> 表示一个文档画廊引擎。用于为应用程序提供画廊功能支持。
    /// </summary>
    public abstract class DocumentGalleryEngine<TIndex, TIndexWrapper, TDocument> : ProjectSystemModule,
        IDocumentGalleryEngine<TIndex, TIndexWrapper, TDocument>, IDisposable
        where TIndex : DocumentIndex
        where TIndexWrapper : DocumentIndexWrapper<TIndex>
        where TDocument : Document
    {
        //-----------------------------------------------------------------------
        //
        //  Read-Only Fields
        //
        //-----------------------------------------------------------------------
        private protected readonly SourceList<TIndex> EditableCollection;
        private protected readonly ReadOnlyObservableCollection<TIndexWrapper> BindableCollection;
        private protected readonly ISubject<Func<TIndexWrapper, bool>> FilterStream;
        private protected readonly ISubject<IComparer<TIndexWrapper>> SorterStream;
        private protected readonly CompositeDisposable Disposable;
        private protected readonly ISubject<int> PageCountStream;
        private protected readonly ISubject<int> PerPageCountStream;
        private protected readonly ISubject<int> PageIndexStream;
        private protected readonly ISubject<bool> IsOpenStream;
        private readonly string _documentCollectionName;
        private readonly string _indexesCollectionName;

        //
        // LitieDB 4.1.4 do not support transaction
        // private protected DatabaseTransaction Transaction;

        //-----------------------------------------------------------------------
        //
        //  Fields
        //
        //-----------------------------------------------------------------------
        private protected int _PerPageCount;
        private protected int _PageCount;
        private protected int _PageIndex;
        private protected int _TotalItemCount;
        private protected bool _IsOpen;
        private protected LiteCollection<TIndex> IndexCollection;
        private protected LiteCollection<TDocument> DocumentCollection;
        private protected Func<TIndexWrapper, bool> _Filter;
        private protected IComparer<TIndexWrapper> _Sorter;
        private protected IEnumerable<TIndex> Enumerator;

        //-----------------------------------------------------------------------
        //
        //  Constructor
        //
        //-----------------------------------------------------------------------
        protected DocumentGalleryEngine(Func<TIndex, TIndexWrapper> transformer,
            ICompositionSetRequestQueue requestQueue, string collectionName, string indexName) : base(requestQueue)
        {
            if (transformer == null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            //
            // 一个可释放资源组，将会收集在 DocumentGalleryEngine 作用域下创建的所有资源
            Disposable = new CompositeDisposable();

            //
            // 排序器流。
            SorterStream = new BehaviorSubject<IComparer<TIndexWrapper>>(Comparer<TIndexWrapper>.Default);

            //
            // 过滤器流
            FilterStream = new BehaviorSubject<Func<TIndexWrapper, bool>>(x => x != null);

            //
            // 
            PageCountStream = new BehaviorSubject<int>(0);
            PerPageCountStream = new BehaviorSubject<int>(10);
            PageIndexStream = new BehaviorSubject<int>(1);
            IsOpenStream = new BehaviorSubject<bool>(false);

            //
            // 可编辑集合
            EditableCollection = new SourceList<TIndex>();
            var disposable1 = EditableCollection.Connect()
                .Transform(transformer)
                .Filter(FilterStream)
                .Sort(SorterStream)
                .Bind(out BindableCollection)
                .Subscribe();


            var disposable2 = PerPageCountStream.Subscribe(OnPageInformationChanged);
            var disposable3 = PageIndexStream.Subscribe(OnPageInformationChanged);

            _documentCollectionName = collectionName;
            _indexesCollectionName = indexName;

            Disposable.Add(disposable1);
            Disposable.Add(disposable2);
            Disposable.Add(disposable3);
            Disposable.Add(EditableCollection);
            Disposable.Add((BehaviorSubject<Func<TIndexWrapper, bool>>) FilterStream);
            Disposable.Add((BehaviorSubject<IComparer<TIndexWrapper>>) SorterStream);
            Disposable.Add((BehaviorSubject<bool>) IsOpenStream);
            Disposable.Add((BehaviorSubject<int>) PageCountStream);
            Disposable.Add((BehaviorSubject<int>) PerPageCountStream);
            Disposable.Add((BehaviorSubject<int>) PageIndexStream);
        }

        //-----------------------------------------------------------------------
        //
        //  Override Methods
        //
        //-----------------------------------------------------------------------

        #region Override Methods

        protected override void OnCompositionSetOpening(CompositionSetOpenNotification notification)
        {
            //
            // 跳过无效的通知
            if (!Helper.ValidateOpenNotification(notification))
            {
                return;
            }

            //
            //
            DocumentCollection = notification.MainDatabase.GetCollection<TDocument>(_documentCollectionName);

            //
            //
            IndexCollection = notification.MainDatabase.GetCollection<TIndex>(_indexesCollectionName);

            //
            // 所有内容的数量
            _TotalItemCount = IndexCollection.Count();

            //
            // 所有页面数
            _PageCount = (_TotalItemCount + _PerPageCount) / _PerPageCount;
            PageCountStream.OnNext(_PageCount);

            //
            // 初始化页面
            _PageIndex = 1;
            PageIndexStream.OnNext(_PageIndex);

            //
            // 加载内容
            DemandRefreshDataSource(_PerPageCount, _PageIndex);

            //
            // 提示已经打开项目
            _IsOpen = true;
            IsOpenStream.OnNext(_IsOpen);
        }

        protected override void OnCompositionSetClosing(CompositionSetCloseNotification notification)
        {
            //
            // 清空内容以及程序状态
            DocumentCollection = null;
            IndexCollection = null;
            EditableCollection.Clear();
            _PageIndex = 1;
            _PageCount = 0;

            //
            // 提示已经关闭项目
            _IsOpen = false;
            IsOpenStream.OnNext(_IsOpen);
        }

        protected override void OnCompositionSetSaving(CompositionSetSaveNotification notification)
        {
        }

        #endregion


        //-----------------------------------------------------------------------
        //
        //  Private Methods
        //
        //-----------------------------------------------------------------------

        private void DemandRefreshDataSourceImpl(int perPageCount, int pageIndex)
        {
            if (!_IsOpen)
            {
                return;
            }
            DemandRefreshDataSource(perPageCount, pageIndex);
        }
        
        private void DeleteThisImpl(TIndex index)
        {
            if (!_IsOpen)
            {
                return;
            }
            DeleteThisCore(index.Id);
        }

        private void DeleteThisImpl(TDocument document)
        {
            if (!_IsOpen)
            {
                return;
            }
            DeleteThisCore(document.Id);
        }

        private void DeleteThisPageImpl()
        {
            if (!_IsOpen)
            {
                return;
            }
            DeleteThisPageCore();
        }

        private void DeleteAllImpl()
        {
            if (!_IsOpen)
            {
                return;
            }
            DeleteAllCore();
        }
        
        
        private void UpdateImpl(TIndexWrapper wrapper, TIndex index, TDocument document)
        {
            if (!_IsOpen)
            {
                return;
            }
            UpdateCore(wrapper, index, document);
        }
        
        private void UpdateImpl(TDocument document)
        {
            if (!_IsOpen)
            {
                return;
            }
            UpdateCore(document);
        }

        private TDocument OpenImpl(TIndexWrapper wrapper)
        {
            if (!_IsOpen)
            {
                return default(TDocument);
            }
            return OpenImpl(wrapper.Source);
        }
        
        private TDocument OpenImpl(TIndex index)
        {
            if (!_IsOpen)
            {
                return default(TDocument);
            }
            return OpenCore(index);
        }

        private void NewImpl(INewDocumentInfo<TDocument> info)
        {
            if (!_IsOpen)
            {
                return;
            }

            NewCore(info);
        }

        private void FindImpl(string keyword)
        {
            if (!_IsOpen)
            {
                return;
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                return;
            }

            //
            //
            Enumerator = IndexCollection.Find(ConstructFindExpression(keyword));
            
            //
            // 
            DemandRefreshDataSourceImpl(_PerPageCount, 1);
        }
        
        
        protected void ResetFindImpl()
        {
            if (!_IsOpen)
            {
                return;
            }

            //
            // 清空当前页面内容 
            EditableCollection.Clear();

            
            Enumerator ??= IndexCollection.FindAll();


            DemandRefreshDataSourceImpl(_PerPageCount, _PageIndex);
        }
        
        //-----------------------------------------------------------------------
        //
        //  Protected Virtual Methods
        //
        //-----------------------------------------------------------------------






        protected abstract TIndex CreateIndexInstance(INewDocumentInfo<TDocument> info);
        protected abstract TDocument CreateDocumentInstance(INewDocumentInfo<TDocument> info);
        protected abstract BsonDocument SerializeIndex(TIndex index);
        protected abstract BsonDocument SerializeDocument(TDocument document);
        protected abstract TIndex DeserializeIndex(BsonValue value);
        protected abstract TDocument DeserializeDocument(BsonValue value);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        protected virtual void OnPageInformationChanged(int value)
        {
            DemandRefreshDataSourceImpl(_PerPageCount, _PageIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="perPageCount"></param>
        /// <param name="pageIndex"></param>
        protected virtual void DemandRefreshDataSource(int perPageCount, int pageIndex)
        {
            Enumerator ??= IndexCollection.FindAll();
            
            //
            // 清空集合
            EditableCollection.Clear();

            //
            // 获取内容
            var thisPageEnumeration = Enumerator
                .Skip(perPageCount * pageIndex)
                .Take(perPageCount)
                .ToArray();

            //
            // 添加内容
            EditableCollection.AddRange(thisPageEnumeration);
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
                .Skip(_PerPageCount * _PageIndex)
                .Take(_PerPageCount)
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
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="document"></param>
        protected virtual void ExtractIndex(TIndex index, TDocument document)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wrapper"></param>
        /// <param name="index"></param>
        /// <param name="document"></param>
        protected virtual void UpdateCore(TIndexWrapper wrapper, TIndex index, TDocument document)
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
        
        protected virtual void UpdateCore(TDocument document)
        {
            var index = IndexCollection.FindById(document.Id);
            
            //
            // 从文档中提取内容
            ExtractIndex(index, document);

            //
            // 刷新
            DemandRefreshDataSourceImpl(_PerPageCount, _PageIndex);

            //
            // 更新索引
            IndexCollection.Upsert(index);
            
            //
            // 更新文档
            DocumentCollection.Upsert(document);
        }

        protected virtual TDocument OpenCore(TIndex index)
        {
            return DocumentCollection.FindById(index.Id);
        }
        
        
        protected virtual void NewCore(INewDocumentInfo<TDocument> info)
        {
            //
            // 创建实例
            var index = CreateIndexInstance(info);
            var document = CreateDocumentInstance(info);

            //
            // 设置同样的唯一标识符。
            index.Id = document.Id = Guid.NewGuid();
            
            //
            // 抽取关键字到索引当中
            ExtractIndex(index, document);

            //
            // 插入
            IndexCollection.Insert(index);
            DocumentCollection.Insert(document);
        }

        protected virtual Query ConstructFindExpression(string keyword)
        {
            return Query.Contains("Name", keyword);
        }
        
        
        
        

        //-----------------------------------------------------------------------
        //
        //  Public Methods
        //
        //-----------------------------------------------------------------------
        
        
        
        
        
        
        public void Dispose()
        {
            //
            // 清空内容以及程序状态
            DocumentCollection = null;
            IndexCollection = null;
            EditableCollection.Clear();
            _PageIndex = 1;
            _PageCount = 0;

            //
            // 提示已经关闭项目
            _IsOpen = false;
            IsOpenStream.OnNext(_IsOpen);
            Disposable?.Dispose();
        }

        public Task ResetFindAsync()
        {
            return Task.Run(ResetFindImpl);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task FindAsync(string keyword)
        {
            return Task.Run(() => FindImpl(keyword));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task NewAsync(INewDocumentInfo<TDocument> info)
        {
            return Task.Run(() => NewImpl(info));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<TDocument> OpenAsync(TIndexWrapper index)
        {
            return Task.Run(() => OpenImpl(index));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<TDocument> OpenAsync(TIndex index)
        {
            return Task.Run(() => OpenImpl(index));
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public Task UpdateAsync(TDocument document)
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
        public Task UpdateAsync(TIndexWrapper wrapper, TIndex index, TDocument document)
        {
            return Task.Run(() => UpdateImpl(wrapper, index, document));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Task DeleteThisAsync(TIndex index)
        {
            return Task.Run(() => DeleteThisImpl(index));
        }

        /// <summary>
        /// 删除这个文档
        /// </summary>
        /// <param name="document"></param>
        public Task DeleteThisAsync(TDocument document)
        {
            return Task.Run(() => DeleteThisImpl(document));
        }

        /// <summary>
        /// 删除这个页面
        /// </summary>
        public Task DeleteThisPageAsync()
        {
            return Task.Run(DeleteThisPageImpl);
        }

        /// <summary>
        /// 删除全部数据
        /// </summary>
        public Task DeleteAllAsync()
        {
            return Task.Run(DeleteAllImpl);
        }

        /// <summary>
        /// 获取当前画廊的操作状态
        /// </summary>
        public IObservable<bool> IsOpen => IsOpenStream;

        /// <summary>
        /// 获取或设置当前画廊的排序器。
        /// </summary>
        public IObservable<int> PageCount => PageCountStream;

        /// <summary>
        /// 获取或设置当前画廊的排序器。
        /// </summary>
        public int PerPageCount
        {
            get => _PerPageCount;
            set
            {
                _PerPageCount = value;
                PerPageCountStream.OnNext(value);
            }
        }

        /// <summary>
        /// 获取或设置当前画廊的排序器。
        /// </summary>
        public int PageIndex
        {
            get => _PageIndex;
            set
            {
                _PageIndex = value;
                PageIndexStream.OnNext(value);
            }
        }

        /// <summary>
        /// 获取或设置当前画廊的排序器。
        /// </summary>
        public Func<TIndexWrapper, bool> Filter
        {
            get => _Filter;
            set
            {
                _Filter = value;
                FilterStream.OnNext(value);
            }
        }

        /// <summary>
        /// 获取或设置当前画廊的排序器。
        /// </summary>
        public IComparer<TIndexWrapper> Sorter
        {
            get => _Sorter;
            set
            {
                _Sorter = value;
                SorterStream.OnNext(value);
            }
        }

        /// <summary>
        /// 获取或设置当前画廊的排序器。
        /// </summary>
        public ReadOnlyObservableCollection<TIndexWrapper> Collection => BindableCollection;
    }
}