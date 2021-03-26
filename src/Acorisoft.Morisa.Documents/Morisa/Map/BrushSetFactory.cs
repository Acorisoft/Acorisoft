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
using Acorisoft.Properties;
using System.IO;
using System.Diagnostics.Contracts;
using FileMode = System.IO.FileMode;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Png;

namespace Acorisoft.Morisa.Map
{
    public class BrushSetFactory : DataSetFactory<BrushSet, BrushSetProperty>, IBrushSetFactory
    {
        private bool _LoadingState;
        //
        //
        private readonly SourceCache<IBrushGroup,Guid> _GroupSource;
        private readonly SourceList<IBrush>            _BrushSource;

        //
        //
        private readonly ReadOnlyObservableCollection<BrushAdapter>           _BrushCollection;
        private readonly ReadOnlyObservableCollection<BrushGroupAdapter>      _GroupCollection;

        //
        //
        private readonly BehaviorSubject<IPageRequest>              _PagerStream;
        private readonly BehaviorSubject<Func<IBrushAdapter,bool>>  _FilterStream;

        public BrushSetFactory()
        {
            _BrushSource = new SourceList<IBrush>();
            _GroupSource = new SourceCache<IBrushGroup, Guid>(x => x.Id);
            _PagerStream = new BehaviorSubject<IPageRequest>(new PageRequest(1, 50));
            _FilterStream = new BehaviorSubject<Func<IBrushAdapter, bool>>(x => true);

            //
            // 呈现属性的时候需要单独连接一次，否则在监听集合变化操作的时候，一旦设置Filter、Pager都会使得集合发生改变。
            _BrushSource.Connect()
                        .Transform(x => new BrushAdapter(x))
                        .Filter(_FilterStream)
                        .Page(_PagerStream)
                        .Bind(out _BrushCollection)
                        .Subscribe();

            _BrushSource.Connect()
                        .Subscribe(x => OnBrushChanged(x));

            _GroupSource.Connect()
                        .TransformToTree(x => x.ParentId)
                        .Transform(x => new BrushGroupAdapter(x, OnGroupChanged))
                        .Bind(out _GroupCollection)
                        .Subscribe();

            _GroupSource.Connect()
                        .Subscribe(x => OnGroupChanged(x));
        }

        protected virtual void OnBrushChanged(IChangeSet<IBrush> changeSet)
        {
            if (DataSet is null)
            {
                return;
            }

            if (_LoadingState)
            {
                return;
            }

            //
            // capture tree set changed
            foreach (var change in changeSet)
            {

                switch (change.Reason)
                {
                    case ListChangeReason.Add:
                        DataSet.DB_Brush.Insert(change.Item.Current);
                        break;
                    case ListChangeReason.AddRange:
                        DataSet.DB_Brush.Insert(change.Item.Current);
                        break;
                    case ListChangeReason.Clear:
                        DataSet.DB_Brush.Delete(Query.All());
                        break;
                    case ListChangeReason.Remove:
                        DataSet.DB_Brush.Delete(change.Item.Current.Id);
                        break;
                    case ListChangeReason.RemoveRange:
                        
                        break;
                    case ListChangeReason.Replace:
                        
                        break;
                    case ListChangeReason.Moved:
                    default:
                        //
                        // 集合的移动不影响当前树形结构的持久化
                        break;
                }
            }
        }

        protected virtual void OnGroupChanged(IChangeSet<IBrushGroup, Guid> changeSet)
        {
            if (DataSet is null)
            {
                return;
            }

            if (_LoadingState)
            {
                return;
            }

            //
            // capture tree set changed
            foreach (var change in changeSet)
            {

                switch (change.Reason)
                {
                    case ChangeReason.Add:
                        //
                        // 当当前集合发生添加操作时。
                        var targetItem = change.Current;
                        DataSet.DB_Group.Upsert(targetItem);
                        break;
                    case ChangeReason.Refresh:
                        DataSet.DB_Group.Delete(Query.All());
                        break;
                    case ChangeReason.Remove:
                        DataSet.DB_Group.Delete(change.Current.Id);
                        break;
                    case ChangeReason.Update:
                        targetItem = change.Current;
                        DataSet.DB_Group.Upsert(targetItem);
                        break;
                    case ChangeReason.Moved:
                    default:
                        //
                        // 集合的移动不影响当前树形结构的持久化
                        break;
                }
            }
        }

        protected virtual void OnGroupChanged(IChangeSet<BrushGroupAdapter, Guid> changeSet)
        {
            if (DataSet is null)
            {
                return;
            }

            if (_LoadingState)
            {
                return;
            }

            //
            // capture tree set changed
            foreach (var change in changeSet)
            {
  
                switch (change.Reason)
                {
                    case ChangeReason.Add:
                        //
                        // 当当前集合发生添加操作时。
                        var targetItem = change.Current.Source;
                        DataSet.DB_Group.Upsert(targetItem);
                        break;
                    case ChangeReason.Refresh:
                        DataSet.DB_Group.Delete(Query.All());
                        break;
                    case ChangeReason.Remove:
                        DataSet.DB_Group.Delete(change.Current.Id);
                        break;
                    case ChangeReason.Update:
                        targetItem = change.Current.Source;
                        DataSet.DB_Group.Upsert(targetItem);
                        break;
                    case ChangeReason.Moved:
                    default:
                        //
                        // 集合的移动不影响当前树形结构的持久化
                        break;
                }
            }
        }

        protected static FillMode AnalyzeFillMode(int count, int width)
        {
            var rate = count / width;

            if (rate < .25d)
            {
                return FillMode.Thin;
            }
            else if (rate > .25d && rate <= .5d)
            {
                return FillMode.Half;
            }
            else if (rate > .5d && rate < .75d)
            {
                return FillMode.Extra;
            }
            else
            {
                return FillMode.Large;
            }
        }

        protected void AnalyzeFillMode(IGenerateContext<Brush> brush, Image<Rgba32> image, Rgba32 landColor)
        {
            //
            // 打开为图片
            if (image.TryGetSinglePixelSpan(out var colorSpan))
            {
                var count = 0;
                //
                // top edge
                for (int x = 0; x < image.Width; x++)
                {
                    if (landColor == colorSpan[x])
                    {
                        count++;
                    }
                }

                brush.Context.Top = AnalyzeFillMode(count, image.Width);
                count = 0;

                //
                // bottom
                for (int x = image.Height - 1 * image.Width; x < image.Width; x++)
                {
                    if (landColor == colorSpan[x])
                    {
                        count++;
                    }
                }

                //
                // left edge
                brush.Context.Bottom = AnalyzeFillMode(count, image.Width);
                count = 0;

                for (int x = 0; x < image.Height; x += image.Width)
                {
                    if (landColor == colorSpan[x])
                    {
                        count++;
                    }
                }
                //
                // right edge
                brush.Context.Left = AnalyzeFillMode(count, image.Width);
                count = 0;

                for (int x = image.Width - 1; x < image.Height; x += image.Width)
                {
                    if (landColor == colorSpan[x])
                    {
                        count++;
                    }
                }

                brush.Context.Right = AnalyzeFillMode(count, image.Width);
            }
            else
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, nameof(AnalyzeFillMode), SR.InvalidOperation_ExecuteError));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newGroup"></param>
        public void Add(IBrushGroup newGroup)
        {
            if (newGroup is null)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, nameof(newGroup), SR.Parameter_Null));
            }

            if (string.IsNullOrEmpty(newGroup.Name))
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, nameof(IBrushGroup.Name), SR.Parameter_Null));
            }

            if (newGroup.Id == Guid.Empty)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, nameof(IBrushGroup.Name), SR.Parameter_Not_Initialize));
            }

            //
            // 添加到画刷组
            _GroupSource.AddOrUpdate(newGroup);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newGroup"></param>
        /// <param name="parentGroup"></param>
        public void Add(IBrushGroup newGroup, IBrushGroup parentGroup)
        {
            if (newGroup is null)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, nameof(newGroup), SR.Parameter_Null));
            }

            if (string.IsNullOrEmpty(newGroup.Name))
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, newGroup, SR.Parameter_Null));
            }

            if (newGroup.Id == Guid.Empty)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, newGroup, SR.Parameter_Not_Initialize));
            }

            if (parentGroup is null)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, nameof(parentGroup), SR.Parameter_Null));
            }

            if (string.IsNullOrEmpty(parentGroup.Name))
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, parentGroup, SR.Parameter_Null));
            }

            if (parentGroup.Id == Guid.Empty)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, parentGroup, SR.Parameter_Not_Initialize));
            }

            //
            // 设置父级关系
            newGroup.ParentId = parentGroup.Id;

            //
            // 添加到画刷组
            _GroupSource.AddOrUpdate(newGroup);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newGroups"></param>
        public void Add(IEnumerable<IBrushGroup> newGroups)
        {
            if (newGroups == null)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, nameof(newGroups), SR.Parameter_Null));
            }

            foreach (var newGroup in newGroups)
            {
                if (newGroup is null)
                {
                    continue;
                }

                if (string.IsNullOrEmpty(newGroup.Name))
                {
                    continue;
                }

                if (newGroup.Id == Guid.Empty)
                {
                    continue;
                }

                //
                // 添加到画刷组
                _GroupSource.AddOrUpdate(newGroup);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newGroups"></param>
        /// <param name="parentGroup"></param>
        public void Add(IEnumerable<IBrushGroup> newGroups, IBrushGroup parentGroup)
        {
            if (newGroups == null)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, nameof(newGroups), SR.Parameter_Null));
            }

            if (parentGroup is null)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, nameof(parentGroup), SR.Parameter_Null));
            }

            if (string.IsNullOrEmpty(parentGroup.Name))
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, parentGroup, SR.Parameter_Null));
            }

            if (parentGroup.Id == Guid.Empty)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, parentGroup, SR.Parameter_Not_Initialize));
            }

            foreach (var newGroup in newGroups)
            {
                if (newGroup is null)
                {
                    continue;
                }

                if (string.IsNullOrEmpty(newGroup.Name))
                {
                    continue;
                }

                if (newGroup.Id == Guid.Empty)
                {
                    continue;
                }

                //
                // 设置父级关系
                newGroup.ParentId = parentGroup.Id;

                //
                // 添加到画刷组
                _GroupSource.AddOrUpdate(newGroup);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="parentGroup"></param>
        /// <param name="landColor">陆地颜色</param>
        public void Add(IGenerateContext<Brush> brush, IBrushGroup parentGroup, Rgba32 landColor)
        {
            if (brush is null)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, nameof(brush), SR.Parameter_Null));
            }

            if (string.IsNullOrEmpty(brush.FileName))
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, nameof(brush), SR.Parameter_Null));
            }

            if (brush.Context is null)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, nameof(brush), SR.Parameter_Null));
            }


            if (parentGroup is null)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, nameof(parentGroup), SR.Parameter_Null));
            }

            if (string.IsNullOrEmpty(parentGroup.Name))
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, parentGroup, SR.Parameter_Null));
            }

            if (parentGroup.Id == Guid.Empty)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, parentGroup, SR.Parameter_Not_Initialize));
            }

            //
            // 设置父级
            brush.Context.ParentId = parentGroup.Id;

            //
            // 创建Id
            brush.Context.Id = Factory.GenerateGuid();

            try
            {
                //
                // 添加到数据库
                using (var brushStream = new FileStream(brush.FileName, FileMode.Open))
                {
                    using (var memStream = new MemoryStream())
                    {
                        var id = brush.Context.Id.ToString();

                        //
                        // size
                        var image = Image.Load(brushStream);

                        //
                        //
                        image.Mutate(x => x.Resize(80, 80));
                        image.Save(memStream, new PngEncoder());
                        image.Dispose();
                        memStream.Position = 0;

                        //
                        //
                        image = null;
                        DataSet.Database
                               .FileStorage
                               .Upload(id,
                                       id,
                                       memStream);
                        //
                        // 重置文件流的位置
                        memStream.Seek(0, SeekOrigin.Begin);

                        //
                        // 打开为图片
                        using (image = Image.Load(memStream))
                        {
                            AnalyzeFillMode(brush, image.CloneAs<Rgba32>(), landColor);
                        }
                    }
                }

                //
                // 添加
                _BrushSource.Add(brush.Context);
            }
            catch
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brushes"></param>
        /// <param name="parentGroup"></param>
        public void Add(IEnumerable<IGenerateContext<Brush>> brushes, IBrushGroup parentGroup, Rgba32 landColor)
        {
            if (parentGroup is null)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, nameof(parentGroup), SR.Parameter_Null));
            }

            if (string.IsNullOrEmpty(parentGroup.Name))
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, parentGroup, SR.Parameter_Null));
            }

            if (parentGroup.Id == Guid.Empty)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, parentGroup, SR.Parameter_Not_Initialize));
            }

            if (brushes == null)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, nameof(brushes), SR.Parameter_Null));
            }

            foreach (var brush in brushes)
            {
                if (brush is null)
                {
                    continue;
                }

                if (string.IsNullOrEmpty(brush.FileName))
                {
                    continue;
                }

                if (brush.Context is null)
                {
                    continue;
                }
                //
                // 设置父级
                brush.Context.ParentId = parentGroup.Id;

                //
                // 创建Id
                brush.Context.Id = Factory.GenerateGuid(); ;

                using (var brushStream = new FileStream(brush.FileName, FileMode.Open))
                {
                    using (var memStream = new MemoryStream())
                    {
                        var id = brush.Context.Id.ToString();

                        //
                        // size
                        var image = Image.Load(brushStream);

                        //
                        //
                        image.Mutate(x => x.Resize(80, 80));
                        image.Save(memStream, new PngEncoder());
                        image.Dispose();
                        memStream.Position = 0;
                        //
                        //
                        image = null;
                        DataSet.Database
                               .FileStorage
                               .Upload(id,
                                       id,
                                       memStream);
                        //
                        // 重置文件流的位置
                        memStream.Seek(0, SeekOrigin.Begin);

                        //
                        // 打开为图片
                        using (image = Image.Load(memStream))
                        {
                            AnalyzeFillMode(brush, image.CloneAs<Rgba32>(), landColor);
                        }
                    }
                }

                //
                // 
                _BrushSource.Add(brush.Context);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        /// <returns></returns>
        public bool Remove(IBrush brush)
        {
            if (brush is null)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, nameof(brush), SR.Parameter_Null));
            }

            if (brush.Id == Guid.Empty)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, nameof(brush), SR.Parameter_Not_Initialize));
            }

            return _BrushSource.Remove(brush);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brushes"></param>
        /// <returns></returns>
        public bool Remove(IEnumerable<IBrush> brushes)
        {
            if (brushes is null)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, nameof(brushes), SR.Parameter_Null));
            }

            foreach (var brush in brushes)
            {
                _BrushSource.Remove(brush);
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public void Remove(IBrushGroup group)
        {
            if (group is null)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, nameof(group), SR.Parameter_Null));
            }

            if (group.Id == Guid.Empty)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, nameof(group), SR.Parameter_Not_Initialize));
            }

            _GroupSource.Remove(group);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groups"></param>
        /// <returns></returns>
        public void Remove(IEnumerable<IBrushGroup> groups)
        {
            if (groups is null)
            {
                throw new InvalidOperationException(string.Format(SR.InvalidOperation, nameof(groups), SR.Parameter_Null));
            }

            foreach (var group in groups)
            {
                _GroupSource.Remove(group);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void RemoveAllBrushes()
        {
            _BrushSource.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        public void RemoveAllGroups()
        {
            _GroupSource.Clear();
        }

        public Stream GetResource(IBrush resource)
        {
            if (resource == null)
            {
                return null;
            }

            var storage = DataSet.Database.FileStorage;
            var id = resource.Id.ToString();
            if(storage.Exists(id))
            {
                return storage.OpenRead(id);
            }

            return null;
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

                if (DataSet is not null)
                {
                    DataSet.Dispose();
                    DataSet = null;
                }

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


                if (DataSet is not null)
                {
                    DataSet.Dispose();
                    DataSet = null;
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

            _LoadingState = true;
            //_GroupSource.Connect()
            //            .TransformToTree(x => x.ParentId)
            //            .Transform(x => new BrushGroupAdapter(x, OnGroupChanged))
            //            .Bind(out _GroupCollection)
            //            .Subscribe(x =>
            //            {
            //                OnGroupChanged(x);
            //            });

            //_BrushSource.Connect()
            //            .Transform(x => new BrushAdapter(x))
            //            .Sort(_SorterStream)
            //            .Page(_PagerStream)
            //            .Bind(out _BrushCollection)
            //            .Subscribe(x =>
            //            {

            //            });

            _GroupSource.Edit(x =>
            {
                x.Load(ds.DB_Group.FindAll());
            });

            _BrushSource.Edit(x =>
            {
                x.Clear();
                x.AddRange(ds.DB_Brush.FindAll());
            });

            base.InitializeFromDatabase(ds);

            _LoadingState = false;
        }

        protected override bool DetermineDatabaseInitialization(BrushSet set)
        {
            return set.Database.CollectionExists(Constants.BrushCollectionName);
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
        public IObserver<Func<IBrushAdapter,bool>> FilterStream => _FilterStream;

        /// <summary>
        /// 
        /// </summary>
        public IObserver<IPageRequest> PageStream => _PagerStream;

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyObservableCollection<BrushAdapter> Brushes => _BrushCollection;

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyObservableCollection<BrushGroupAdapter> Groups => _GroupCollection;
    }
}
