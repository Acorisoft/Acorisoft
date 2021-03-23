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
using System.IO;
using System.Diagnostics.Contracts;

namespace Acorisoft.Morisa.Map
{
    public class BrushSetManager : DataSetManager<BrushSet, BrushSetProperty>, IDataSetManager<BrushSet, BrushSetProperty>
    {
        private readonly SourceCache<IBrushGroup,Guid> GroupEditableCollection;
        private readonly SourceList<IBrush> BrushEditableCollection;
        private readonly ReadOnlyObservableCollection<IBrushGroupTree> GroupBindableCollection;
        private readonly ReadOnlyObservableCollection<IBrush> SuggestionBrushCollection;
        private readonly ReadOnlyObservableCollection<IBrush> BrushCollection;
        private readonly BehaviorSubject<Func<IBrush,bool>> _SuggestionFilter;
        private readonly BehaviorSubject<Func<IBrush,bool>> _GroupFilter;
        private readonly IDisposable _SuggestionDisposable;
        private readonly IDisposable _GroupDisposable;


        public BrushSetManager()
        {
            _SuggestionFilter = new BehaviorSubject<Func<IBrush, bool>>(x => false);
            _GroupFilter = new BehaviorSubject<Func<IBrush, bool>>(x => false);
            GroupEditableCollection = new SourceCache<IBrushGroup, Guid>(x => x.Id);
            BrushEditableCollection = new SourceList<IBrush>();
            GroupEditableCollection.Connect()
                                   .TransformToTree(x => x.ParentId)
                                   .Transform(x => (IBrushGroupTree)new BrushGroupTree(x))
                                   .TransformMany()
                                   .Bind(out GroupBindableCollection)
                                   .Subscribe(x =>
                                   {
                                       OnGroupChanged(x);
                                   });
            _SuggestionDisposable = BrushEditableCollection.Connect()
                                                           .Filter(_SuggestionFilter)
                                                           .Bind(out BrushCollection)
                                                           .Subscribe(x =>
                                                           {

                                                           });
            _GroupDisposable = BrushEditableCollection.Connect()
                                                      
            
        }

        protected virtual void OnGroupChanged(IChangeSet<IBrushGroupTree,Guid> set)
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
            GroupEditableCollection.AddOrUpdate(ds.DB_Group.FindAll());
            BrushEditableCollection.AddRange(ds.DB_Brush.FindAll());
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
            foreach(var ds in EditableDataSetCollection.Items)
            {
                ds.Dispose();
            }
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

        public IObserver<IBrushGroupTree> SelectedGroup { get; }
        public ReadOnlyObservableCollection<IBrush> Brushes { get; }
        public ReadOnlyObservableCollection<IBrushGroupTree> Groups => GroupBindableCollection;
        public ReadOnlyObservableCollection<IBrush> SuggestionBrushes { get; }
    }
}
