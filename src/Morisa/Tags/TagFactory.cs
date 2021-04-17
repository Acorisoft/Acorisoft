using Acorisoft.Morisa.Composition;
using Acorisoft.Morisa.Core;
using Acorisoft.Morisa.EventBus;
using Acorisoft.Morisa.Properties;
using DynamicData;
using LiteDB;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BindableTagBridgeCollection = System.Collections.ObjectModel.ReadOnlyObservableCollection<Acorisoft.Morisa.Tags.ITagBridge>;

namespace Acorisoft.Morisa.Tags
{
    public class TagFactory : EntitySystem, ITagFactory
    {
        private protected readonly ICompositionSetMediator          MediatorInstance;
        private protected readonly SourceCache<ITag,Guid>           SourceInstance;
        private protected readonly HashSet<string>                  SourceDistinct;
        private protected readonly HashSet<ITag>                    SourceHashset;
        private protected readonly BindableTagBridgeCollection      CollectionInstance;
        private protected readonly IFullLogger                      Logger;
        private protected LiteCollection<ITag>                      CollectionInDatabase;
        private protected readonly DataTrackingState                TrackingState;

        public TagFactory(ICompositionSetMediator mediator, ILogManager logMgr)
        {
            TrackingState = new DataTrackingState();
            MediatorInstance = mediator;
            SourceInstance = new SourceCache<ITag, Guid>(x => x.Id);
            Logger = logMgr.GetLogger<TagFactory>();
            SourceDistinct = new HashSet<string>();
            SourceHashset = new HashSet<ITag>();
            SourceInstance.Connect()
                          .TransformToTree(x => x.ParentId)
                          .Transform(x => (ITagBridge)new TagBridge(x, OnTagChanged))
                          .Bind(out CollectionInstance)
                          .Subscribe(x =>
                          {
                          });

            SourceInstance.Connect()
                          .Subscribe(x => OnTagChanged(x));
        }

        protected virtual void OnTagChanged(IChangeSet<ITagBridge, Guid> changeSets)
        {
            if (TrackingState.IsTracking)
            {
                foreach (var change in changeSets)
                {
                    switch (change.Reason)
                    {
                        case ChangeReason.Refresh:
                            break;
                        case ChangeReason.Add:
                            SourceDistinct.Add(change.Current.Name);
                            SourceHashset.Add(change.Current.Source);
                            CollectionInDatabase.Upsert(change.Current.Source);
                            break;
                        case ChangeReason.Remove:
                            SourceDistinct.Remove(change.Current.Name);
                            SourceHashset.Remove(change.Current.Source);
                            CollectionInDatabase.Delete(change.Current.Source.Id);
                            break;
                        case ChangeReason.Update:
                            if (change.Previous.HasValue)
                            {
                                SourceDistinct.Remove(change.Previous.Value.Name);
                                SourceHashset.Remove(change.Previous.Value.Source);
                                CollectionInDatabase.Delete(change.Current.Source.Id);
                            }
                            SourceDistinct.Add(change.Current.Name);
                            SourceHashset.Add(change.Current.Source);
                            CollectionInDatabase.Upsert(change.Current.Source);
                            break;
                    }
                }

                Changed?.Invoke(this, new EventArgs());
            }
        }

        protected virtual void OnTagChanged(IChangeSet<ITag, Guid> changeSets)
        {
            if (TrackingState.IsTracking)
            {
                foreach (var change in changeSets)
                {
                    switch (change.Reason)
                    {
                        case ChangeReason.Add:
                            SourceDistinct.Add(change.Current.Name);
                            SourceHashset.Add(change.Current);
                            CollectionInDatabase.Upsert(change.Current);
                            break;
                        case ChangeReason.Remove:
                            SourceDistinct.Remove(change.Current.Name);
                            SourceHashset.Remove(change.Current);
                            CollectionInDatabase.Delete(change.Current.Id);
                            break;
                        case ChangeReason.Update:
                            if (change.Previous.HasValue)
                            {
                                SourceDistinct.Remove(change.Previous.Value.Name);
                                SourceHashset.Remove(change.Previous.Value);
                                CollectionInDatabase.Delete(change.Current.Id);
                            }
                            SourceDistinct.Add(change.Current.Name);
                            SourceHashset.Add(change.Current);
                            CollectionInDatabase.Upsert(change.Current);
                            break;
                    }

                    Changed?.Invoke(this, new EventArgs());
                }
            }
        }

        protected override Task OnCompositionSetChanged(CompositionSetOpeningInstruction instruction, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                var context = instruction.Context;
                var activating = context.Activating;

                using (TrackingState.BeforeTracking())
                {

                    //
                    // 读取集合
                    CollectionInDatabase = activating.Database.GetCollection<ITag>(Constants.TagCollection);

                    //
                    // Tracking Information
                    var count = CollectionInDatabase.Count();

                    foreach(var item in CollectionInDatabase.FindAll())
                    {

                        //
                        // 读取数据
                        SourceInstance.AddOrUpdate(item);
                        SourceDistinct.Add(item.Name);
                        SourceHashset.Add(item);
                    }
                }

            }, cancellationToken);
        }

        protected override Task OnCompositionSetClosing(CompositionSetClosingInstruction instruction, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                using (TrackingState.BeforeTracking())
                {
                    SourceDistinct.Clear();
                    SourceHashset.Clear();
                    SourceInstance.Clear();
                }
            }, cancellationToken);
        }

        #region Add / AddToRoot / AddToChildren

        public void Add(ITag entity)
        {
            if (entity == null)
            {
                throw new InvalidOperationException(SR.TagFactory_Add_Entity_Null);
            }

            if (SourceDistinct.Contains(entity.Name))
            {
                throw new InvalidOperationException(string.Format(SR.TagFactory_Add_Entity_Null, entity.Name));
            }

            if (entity.Id == Guid.Empty)
            {
                entity.Id = Helper.ToGuid();
            }

            SourceInstance.AddOrUpdate(entity);
        }

        public void AddToRoot(ITag entity)
        {
            if (entity == null)
            {
                throw new InvalidOperationException(SR.TagFactory_Add_Entity_Null);
            }

            if (SourceDistinct.Contains(entity.Name))
            {
                throw new InvalidOperationException(string.Format(SR.TagFactory_Add_Entity_Null, entity.Name));
            }

            if (entity.Id == Guid.Empty)
            {
                entity.Id = Helper.ToGuid();
            }

            SourceInstance.AddOrUpdate(entity);
        }

        public void AddToChildren(ITag entity, ITag parent)
        {
            if (entity == null)
            {
                throw new InvalidOperationException(SR.TagFactory_Add_Entity_Null);
            }

            if (parent == null)
            {
                throw new InvalidOperationException(SR.TagFactory_Add_Entity_Parent_Null);
            }

            if (SourceDistinct.Contains(entity.Name))
            {
                throw new InvalidOperationException(string.Format(SR.TagFactory_Add_Entity_Null, entity.Name));
            }

            if (!SourceHashset.Contains(parent))
            {
                throw new InvalidOperationException(string.Format(SR.TagFactory_Add_Entity_Parent_NotExists, entity.Name));
            }
            else
            {

                if (entity.Id == Guid.Empty)
                {
                    entity.Id = Helper.ToGuid();
                }

                entity.ParentId = parent.Id;

                //
                // 添加
                SourceInstance.AddOrUpdate(entity);
            }
        }


        #endregion Add / AddToRoot / AddToChildren

        protected IReadOnlyCollection<ITag> GetChildren(ITag entity)
        {
            return SourceInstance.Items.Where(x => x.ParentId == entity.Id && entity.GetHashCode() != x.GetHashCode()).ToArray();
        }

        protected ITag GetParent(ITag entity)
        {
            return SourceInstance.Items.Where(x => x.ParentId == entity.Id).FirstOrDefault();
        }

        public void Clear()
        {
            SourceInstance.Clear();
            SourceDistinct.Clear();
            SourceHashset.Clear();
        }

        public void Demote(ITag entity)
        {
            if (entity == null)
            {
                throw new InvalidOperationException(SR.TagFactory_Update_Entity_Null);
            }

            if (!SourceHashset.Contains(entity))
            {
                throw new InvalidOperationException(SR.TagFactory_Update_Entity_NotExists);
            }

            if (HasChildren(entity))
            {
                var firstChild = SourceInstance.Items.First(x => x.ParentId == entity.Id);

                //
                // changed
                entity.ParentId = firstChild.Id;
                SourceInstance.AddOrUpdate(entity);
            }
        }

        public bool HasChildren(ITag entity)
        {
            return SourceInstance.Items.Any(x => x.ParentId == entity.Id);
        }

        public void Promote(ITag entity)
        {
            if (entity == null)
            {
                throw new InvalidOperationException(SR.TagFactory_Update_Entity_Null);
            }

            if (!SourceHashset.Contains(entity))
            {
                throw new InvalidOperationException(SR.TagFactory_Update_Entity_NotExists);
            }

            if (entity.Id == Guid.Empty)
            {
                throw new InvalidOperationException(SR.TagFactory_Update_Entity_Id_Empty);
            }

            var parent = GetParent(entity);
            entity.ParentId = parent.Id;
            SourceInstance.AddOrUpdate(entity);

        }


        #region Remove / RemoveEntityAndChildren 

        public void Remove(ITag entity)
        {
            if (entity == null)
            {
                throw new InvalidOperationException(SR.TagFactory_Remove_Entity_Null);
            }

            if (SourceHashset.Contains(entity))
            {
                if(entity.Id == Guid.Empty)
                {
                    //
                    // 移除的是根元素
                    var children = GetChildren(entity);

                    //
                    // 将根元素移交到根这里
                    if(children.Count > 0)
                    {
                        foreach(var child in children)
                        {
                            child.ParentId = Guid.Empty;
                            SourceInstance.AddOrUpdate(child);
                        }
                    }
                }
                else
                {
                    var parent = GetParent(entity);
                    var children = GetChildren(entity);

                    foreach(var child in children)
                    {
                        child.ParentId = parent.Id;
                        SourceInstance.AddOrUpdate(child);
                    }
                }

                SourceInstance.Remove(entity);
            }
        }

        public void RemoveEntityAndChildren(ITag entity)
        {
            if (entity == null)
            {
                throw new InvalidOperationException(SR.TagFactory_Remove_Entity_Null);
            }

            if (SourceHashset.Contains(entity))
            {
                //
                // 移除的是根元素
                var children = GetChildren(entity);

                //
                // 将根元素移交到根这里
                if (children.Count > 0)
                {
                    foreach (var child in children)
                    {
                        child.ParentId = Guid.Empty;
                        SourceInstance.Remove(child);
                    }
                }

                SourceInstance.Remove(entity);
            }
        }

        #endregion

        public void Update(ITag entity)
        {
            if (entity == null)
            {
                throw new InvalidOperationException(SR.TagFactory_Update_Entity_Null);
            }

            if (entity.Id == Guid.Empty)
            {
                throw new InvalidOperationException(SR.TagFactory_Update_Entity_Id_Empty);
            }

            SourceInstance.AddOrUpdate(entity);
        }

        public ReadOnlyObservableCollection<ITagBridge> Collection => CollectionInstance;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Changed;
    }
}
