﻿using Acorisoft.Morisa.Core;
using Acorisoft.Morisa.v2.Internal;
using System;
using DynamicData;
using DynamicData.Binding;
using LiteDB;
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

namespace Acorisoft.Morisa.v2.Map
{
    public class MapBrushSetFactory : DataSetManager<MapBrushSet, MapBrushSetInformation>, IMapBrushSetFactory
    {
        public const string BrushCollectionName = "Brushes";
        public const string GroupCollectionName = "Groups";

        private readonly IDisposableCollector           _Collector;
        private readonly SourceList<IMapGroup>          _EditableGroupCollection;
        private readonly SourceList<IMapBrush>          _EditableBrushCollection;
        private readonly ReadOnlyObservableCollection<IMapGroupAdapter>  _BindableGroupCollection;
        private readonly ReadOnlyObservableCollection<IMapBrush>         _BindableBrushCollection;
        private readonly SourceCache<IMapGroup,Guid>            _Tree;

        public MapBrushSetFactory(IDisposableCollector collector) : base()
        {
            _Collector = collector;
            _EditableBrushCollection = new SourceList<IMapBrush>();
            _EditableGroupCollection = new SourceList<IMapGroup>();
            _Tree = new SourceCache<IMapGroup, Guid>(x => x.OwnerId);
            _EditableGroupCollection.Connect()
                                    .Transform(x => (IMapGroupAdapter)new MapGroupAdapter(x))
                                    .Bind(out _BindableGroupCollection)
                                    .SubscribeOn(ThreadPoolScheduler.Instance)
                                    .Subscribe(x =>
                                    {
                                        PerformanceGroupChanged(x);
                                    })
                                    .DisposeWith(_Collector.Disposable);

            _EditableBrushCollection.Connect()
                                    .Bind(out _BindableBrushCollection)
                                    .SubscribeOn(ThreadPoolScheduler.Instance)
                                    .Subscribe(x =>
                                    {
                                        PerformanceBrushChanged(x);
                                    })
                                    .DisposeWith(_Collector.Disposable);
            //
            // 收集需要释放的实例。
            _Collector.Collect(ProfileDisposable);
            _Collector.Collect(ResourceDisposable);

        }

        public void PerformanceGroupChanged(IChangeSet<IMapGroupAdapter> x)
        {
            foreach(var changedItem in x)
            {
                switch (changedItem.Reason)
                {
                    case ListChangeReason.Add:
                        DataSet.DB_GroupCollection.Upsert(changedItem.Item.Current);
                        break;
                    default:
                        break;
                }
            }
        }

        public void PerformanceBrushChanged(IChangeSet<IMapBrush> x)
        {

        }

        public void Generate(IGenerateContext<MapBrushSetInformation> context)
        {
            if (context == null)
            {

            }

            if (context.Context == null)
            {

            }

            if (string.IsNullOrEmpty(context.FileName))
            {

            }

            //
            // 创建数据库
            var database = Factory.CreateDatabase(context.FileName);

            //
            //
            SetGenerateContext(context.Context);

            //
            // 提示更新。
            Input.OnNext(new MapBrushSet { Database = database, DB_External = database.GetCollection(ExternalCollectionName) });
        }

        public void Load(IStoreContext context)
        {
            if (context == null)
            {

            }

            if (string.IsNullOrEmpty(context.FileName))
            {

            }
            //
            // 创建数据库
            var database = Factory.CreateDatabase(context.FileName);

            var mbs = new MapBrushSet
            {
                Database = database
            };
            //
            // 提示更新。
            Input.OnNext(mbs);
        }

        protected override sealed void InitializeFromDatabase(MapBrushSet set)
        {
            //
            // 基类操作初始化。
            base.InitializeFromDatabase(set);

            //
            // 获取数据库
            var database = set.Database;

            //
            // 获取集合
            set.DB_BrushCollection = database.GetCollection<IMapBrush>(BrushCollectionName);
            set.DB_GroupCollection = database.GetCollection<IMapGroup>(GroupCollectionName);

            //
            // 获得集合。
            _EditableBrushCollection.AddRange(set.DB_BrushCollection.FindAll());
            _EditableGroupCollection.AddRange(set.DB_GroupCollection.FindAll());

            //
            // 通知视图模型改变已经发生。
            OnLoaded?.Invoke(this, new EventArgs());
        }

        protected override sealed void OnPerformanceOutsideResource(OutsideResource resource)
        {
            throw new NotSupportedException();
        }

        protected override void InitializeFromPattern(MapBrushSet set)
        {

            //
            // 获取数据库
            var database = set.Database;

            //
            //
            base.InitializeFromPattern(set);

            //
            // 获取集合
            set.DB_BrushCollection = database.GetCollection<IMapBrush>(BrushCollectionName);
            set.DB_GroupCollection = database.GetCollection<IMapGroup>(GroupCollectionName);

            //
            // 注册信息。避免集合未创建。
            set.DB_BrushCollection.Upsert(new List<IMapBrush>());
            set.DB_GroupCollection.Upsert(new List<IMapGroup>());

            //
            // 通知视图模型改变已经发生。
            OnLoaded?.Invoke(this, new EventArgs());
        }

        protected override sealed bool DetermineDataSetInitialization(MapBrushSet cs)
        {
            return cs.Database.CollectionExists(BrushCollectionName);
        }

        /// <summary>
        /// 
        /// </summary>
        public SourceList<IMapGroup> MapGroupSource => _EditableGroupCollection;

        /// <summary>
        /// 
        /// </summary>
        public SourceList<IMapBrush> MapBrushSource => _EditableBrushCollection;

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyObservableCollection<IMapGroupAdapter> GroupCollection => _BindableGroupCollection;

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyObservableCollection<IMapBrush> BrushCollection => _BindableBrushCollection;

        /// <summary>
        /// 用于指示视图模型当前已经加载了数据，通知视图模型以重建模型。
        /// </summary>
        public event EventHandler OnLoaded;

    }
}