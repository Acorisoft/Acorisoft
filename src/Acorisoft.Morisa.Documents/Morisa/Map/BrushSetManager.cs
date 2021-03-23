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
    public class BrushSetManager : DataSetManager<BrushSet, BrushSetProperty>, IDataSetManager<BrushSet, BrushSetProperty>
    {
        //
        //
        //
        private readonly BehaviorSubject<Func<IBrush,bool>>         _SelectedBrushFromSelectedBrushFilter;
        private readonly BehaviorSubject<Func<IBrush,bool>>         _SelectedBrushFromBrushGroupFilter;
        private readonly DefferObserver<IBrushGroupTree>            _SelectedBrushFromBrushGroupHandler;
        private readonly DefferObserver<IBrush>                     _SelectedBrushFromSelectedBrushHandler;

        //
        //
        //
        private readonly ReadOnlyObservableCollection<IBrushGroupTree>  _GroupBindableCollection;
        private readonly ReadOnlyObservableCollection<IBrush>           _BrushFromSelectedBrushCollection;
        private readonly ReadOnlyObservableCollection<IBrush>           _BrushFromSelectedGroupCollection;

        //
        //
        //
        private readonly SourceCache<IBrushGroup,Guid>  _GroupEditableCollection;
        private readonly SourceList<IBrush>             _BrushEditableCollection;
        private readonly IDisposable                    _SuggestionDisposable;
        private readonly IDisposable                    _GroupDisposable;
        private readonly IDisposable                    _Disposable;


        public BrushSetManager()
        {
            _SelectedBrushFromSelectedBrushFilter = new BehaviorSubject<Func<IBrush, bool>>(x => false);
            _SelectedBrushFromBrushGroupFilter = new BehaviorSubject<Func<IBrush, bool>>(x => false);
            _SelectedBrushFromBrushGroupHandler = new DefferObserver<IBrushGroupTree>(x =>
            {
                if (x != null)
                {
                    _SelectedBrushFromBrushGroupFilter.OnNext(brush => brush.ParentId == x.Id);
                }
            });

            _SelectedBrushFromSelectedBrushHandler = new DefferObserver<IBrush>(x =>
            {
                if (x != null)
                {
                    _SelectedBrushFromSelectedBrushFilter.OnNext(brush => brush.ParentId == x.ParentId);
                }
            });

            _GroupEditableCollection = new SourceCache<IBrushGroup, Guid>(x => x.Id);
            _BrushEditableCollection = new SourceList<IBrush>();
            _GroupEditableCollection.Connect()
                                    .TransformToTree(x => x.ParentId)
                                    .Transform(x => (IBrushGroupTree)new BrushGroupTree(x))
                                    .TransformMany(x => x.Children, x => x.Id)
                                    .Bind(out _GroupBindableCollection)
                                    .Subscribe(x =>
                                    {
                                        OnGroupChanged(x);
                                    });

            //
            // 获取或设置当前选择的画刷组的推荐画刷
            _SuggestionDisposable = _BrushEditableCollection.Connect()
                                                            .Filter(_SelectedBrushFromSelectedBrushFilter)
                                                            .Bind(out _BrushFromSelectedBrushCollection)
                                                            .Subscribe(x =>
                                                            {
 
                                                            });

            //
            // 获取当前选择的画刷组的关联画刷。
            _GroupDisposable = _BrushEditableCollection.Connect()
                                                       .Filter(_SelectedBrushFromBrushGroupFilter)
                                                       .Bind(out _BrushFromSelectedGroupCollection)
                                                       .DisposeMany()
                                                       .Subscribe(x =>
                                                       {
 
                                                       });

            _Disposable = System.Reactive.Disposables.Disposable.Create(() =>
            {
                _SuggestionDisposable?.Dispose();
                _GroupDisposable?.Dispose();

            });
        }

        protected virtual void OnGroupChanged(IChangeSet<IBrushGroupTree, Guid> set)
        {

        }

        protected override sealed void OnLoad(ILoadContext context)
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

        protected override void InitializeFromDatabase(BrushSet ds)
        {
            Contract.Assert(ds != null);
            Contract.Assert(ds.Database != null);

            base.InitializeFromDatabase(ds);

            //
            // add your code
            _GroupEditableCollection.AddOrUpdate(ds.DB_Group.FindAll());
            _BrushEditableCollection.AddRange(ds.DB_Brush.FindAll());
        }


        protected override sealed bool DetermineDatabaseInitialization(BrushSet set)
        {
            return set.Database.CollectionExists(Constants.BrushCollectionName);
        }

        protected override sealed void InitializeFromCode(BrushSet ds)
        {
            Contract.Assert(ds != null);
            Contract.Assert(ds.Database != null);

            base.InitializeFromCode(ds);

            //
            // add your code
        }

        protected override void OnDisposeUnmanagedCore()
        {
            foreach (var ds in EditableDataSetCollection.Items)
            {
                ds.Dispose();
            }

            _Disposable?.Dispose();
        }

        protected override void OnDisposeManagedCore()
        {
            ProtectedDataSetStream?.Dispose();
            ProtectedIsOpenStream?.Dispose();
            ProtectedPropertyStream?.Dispose();
        }

        protected override sealed BrushSetProperty CreatePropertyCore()
        {
            return new BrushSetProperty
            {

            };
        }

        /// <summary>
        /// 
        /// </summary>
        public IObserver<IBrush> SelectedBrush => _SelectedBrushFromSelectedBrushHandler;

        /// <summary>
        /// 
        /// </summary>
        public IObserver<IBrushGroupTree> SelectedGroup => _SelectedBrushFromBrushGroupHandler;

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyObservableCollection<IBrush> Brushes => _BrushFromSelectedGroupCollection;

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyObservableCollection<IBrushGroupTree> Groups => _GroupBindableCollection;

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyObservableCollection<IBrush> SuggestionBrushes => _BrushFromSelectedBrushCollection;
    }
}
