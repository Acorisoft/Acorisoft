using LiteDB;
using DynamicData;
using DynamicData.Binding;
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
using Acorisoft.Morisa.Core;
using Acorisoft.Morisa.Reactive;
using System.IO;
using System.Diagnostics.Contracts;

namespace Acorisoft.Morisa.Map
{
    public class BrushSetFactory : DataSetFactory<BrushSet, BrushSetProperty>, IBrushSetFactory
    {
        //
        //
        private readonly SourceCache<IBrushGroup,Guid> _GroupSource;
        private readonly SourceList<IBrush>            _BrushSource;

        //
        //
        private readonly ReadOnlyObservableCollection<IBrushAdapter>           _BrushCollection;
        private readonly ReadOnlyObservableCollection<IBrushGroupAdapter>      _GroupCollection;

        //
        //
        private readonly IObservable<IPageRequest>              _PagerStream;
        private readonly IObservable<IComparer<IBrushAdapter>>  _SorterStream;
        private readonly IObservable<Func<IBrushAdapter,bool>>  _FilterStream;

        public BrushSetFactory()
        {
            _BrushSource = new SourceList<IBrush>();
            _GroupSource = new SourceCache<IBrushGroup, Guid>(x => x.Id);

            _BrushSource.Connect()
                        .Transform(x => (IBrushAdapter)(new BrushAdapter(x)))
                        .Filter(_FilterStream)
                        .Sort(_SorterStream)
                        .Page(_PagerStream)
                        .DisposeMany()
                        .Bind(out _BrushCollection)
                        .Subscribe(x =>
                        {

                        });

            _GroupSource.Connect()
                        .TransformToTree(x => x.ParentId)
                        .Transform(x => (IBrushGroupAdapter)new BrushGroupAdapter(x))
                        .DisposeMany()
                        .Bind(out _GroupCollection)
                        .Subscribe(x =>
                        {

                        });
        }

        public void Add(IBrushGroup newGroup)
        {

        }

        public void Add(IBrushGroup newGroup, IBrushGroup parentGroup)
        {

        }
        public void Add(IEnumerable<IBrushGroup> newGroup, IBrushGroup parentGroup)
        {

        }

        public void Add(IBrush brush, IBrushGroup parentGroup)
        {

        }

        public void Add(IEnumerable<IBrush> brush, IBrushGroup parentGroup)
        {

        }

        public bool Remove(IBrush brush)
        {

        }

        public bool Remove(IEnumerable<IBrush> brushes)
        {

        }

        public bool Remove(IBrushGroup group)
        {

        }

        public bool Remove(IEnumerable<IBrushGroup> groups)
        {

        }

        public void RemoveAllBrushes()
        {

        }

        public void RemoveAllGroups()
        {

        }

        protected override BrushSetProperty CreatePropertyCore()
        {
            return new BrushSetProperty
            {

            };
        }

        protected override void OnLoad(ILoadContext context)
        {
            try
            {
                //
                // 在相对安全的上下文环境中打开数据集。

                var bs = new BrushSet
                {
                    Database = Helper.GetDatabase(context)
                };

                //
                // 初始化数据集。
                bs.DB_External = bs.Database.GetCollection(Constants.ExternalCollectionName);
                bs.DB_Brush = bs.Database.GetCollection<IBrush>(Constants.BrushCollectionName);
                bs.DB_Group = bs.Database.GetCollection<IBrushGroup>(Constants.GroupCollectionName);

                //
                // 调用基类的
                OnDataSetChanged(DataSet, bs);
            }
            catch
            {

            }
        }

        protected override void OnLoad(ISaveContext<BrushSetProperty> context)
        {
            try
            {
                //
                // 在相对安全的上下文环境中打开数据集。

                var bs = new BrushSet
                {
                    Database = Helper.GetDatabase(context)
                };

                //
                // 初始化数据集。
                bs.DB_External = bs.Database.GetCollection(Constants.ExternalCollectionName);
                bs.DB_Brush = bs.Database.GetCollection<IBrush>(Constants.BrushCollectionName);
                bs.DB_Group = bs.Database.GetCollection<IBrushGroup>(Constants.GroupCollectionName);

                //
                // 将当前属性保存于指定的位置
                Singleton(bs, context.Property);

                //
                // 将当前属性的封面存储于制定的位置。
                if (context.Property.Cover is not null)
                {
                    ProtectedResourceHandler.OnNext(context.Property.Cover);
                }

                //
                // 调用基类的
                OnDataSetChanged(DataSet, bs);
            }
            catch
            {

            }
        }

        protected override void InitializeFromDatabase(BrushSet ds)
        {
            Contract.Assert(ds != null);
            Contract.Assert(ds.Database != null);

            base.InitializeFromDatabase(ds);

        }

        protected override bool DetermineDatabaseInitialization(BrushSet set)
        {
            return set.Database.CollectionExists(Constants.ExternalCollectionName);
        }

        protected override void InitializeFromCode(BrushSet ds)
        {
            Contract.Assert(ds != null);
            Contract.Assert(ds.Database != null);

            base.InitializeFromCode(ds);

            //
            // add your code
        }

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyObservableCollection<IBrushAdapter> Brushes => _BrushCollection;

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyObservableCollection<IBrushGroupAdapter> Groups => _GroupCollection;
    }
}
