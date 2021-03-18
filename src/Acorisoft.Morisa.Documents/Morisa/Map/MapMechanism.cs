using Acorisoft.Morisa.Core;
using Acorisoft.Morisa.Emotions;
using Acorisoft.Morisa.Persistants;
using Acorisoft.Morisa.Internal;
using DynamicData;
using DynamicData.Binding;
using LiteDB;
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
using ReactiveUI;
using Splat;
using System.Windows;
using DryIoc;

using Setting = Acorisoft.Morisa.Map.MapBrushSet.Setting;

namespace Acorisoft.Morisa.Map
{
    public class MapMechanism : IMapMechanism, IDisposable
    {
        private MapBrushSet _MapBrushSet;
        private IDisposable _SettingDisposable;
        private IDisposable _BrushDisposable;
        private IDisposable _GroupDisposable;
        private IDisposable _CoverDisposable;

        private readonly IDisposable            _BrushSetDisposable;
        private SourceList<IMapBrush>           _EditableMapBrushCollection;
        private SourceList<IMapGroup>           _EditableMapGroupCollection;

        private ReadOnlyObservableCollection<IMapBrush>         _BindableMapBrushCollection;
        private ReadOnlyObservableCollection<MapGroupAdapter>   _BindableMapGroupCollection;


        public MapMechanism()
        {
            _BrushSetDisposable = Observable.FromEvent<MapBrushSet>(x => BrushSetChanged += x, x => BrushSetChanged -= x)
                                            .ObserveOn(ImmediateScheduler.Instance)
                                            .Where(x => x != null)
                                            .Subscribe(x =>
                                            {
                                                _CoverDisposable?.Dispose();
                                                _BrushDisposable?.Dispose();
                                                _GroupDisposable?.Dispose();
                                                _SettingDisposable?.Dispose();
                                                _CoverDisposable = Observable.FromEvent<InDatabaseResource>(handler => CoverChanged += handler, handler => CoverChanged -= handler)
                                                                             .ObserveOn(ThreadPoolScheduler.Instance)
                                                                             .Subscribe(x => UpdateCoverCore(x));
                                                _SettingDisposable = Observable.FromEvent<Setting>(handler => SettingChanged += handler, handler => SettingChanged -= handler)
                                                                               .ObserveOn(ThreadPoolScheduler.Instance)
                                                                               .Subscribe(x => UpdateSettingCore(x));
                                                BrushSet = x;
                                              });
            _EditableMapBrushCollection = new SourceList<IMapBrush>();
            _EditableMapGroupCollection = new SourceList<IMapGroup>();
            _EditableMapBrushCollection.Connect()
                                       .Bind(out _BindableMapBrushCollection)
                                       .ObserveOn(ThreadPoolScheduler.Instance)
                                       .Subscribe(x => { });
            //_EditableMapGroupCollection.Connect()
            //                           .Select(x => new MapGroupAdapter(x))
            //                           .Bind(out _BindableMapGroupCollection)
            //                           .ObserveOn(ThreadPoolScheduler.Instance)
            //                           .Subscribe(x => { });

        }

        public void AddBrush(string fileName)
        {

        }

        public void AddBatch()
        {

        }

        public void ClearBrush()
        {

        }

        public void Load(string fileName, IMapBrushSetInfo info)
        {
            if (info == null)
            {
                throw new InvalidOperationException();
            }

            if (!string.IsNullOrEmpty(info.Name))
            {
                throw new InvalidOperationException();
            }

            BrushSet = InitializeCore(new MapBrushSet(new LiteDatabase(new ConnectionString
            {
                Filename = fileName,
                InitialSize = 4 * 1024 * 1024,
                Journal = false
            })), info);
        }

        public void Load(string fileName)
        {
            BrushSet = InitializeCore(new MapBrushSet(new LiteDatabase(new ConnectionString
            {
                Filename = fileName,
                InitialSize = 4 * 1024 * 1024,
                Journal = false
            })), null);
        }

        protected static MapBrushSet InitializeCore(MapBrushSet mbs, IMapBrushSetInfo info)
        {

            //
            // 初始化集合。
            mbs.DB_BrushCollection = mbs.Database.GetCollection<IMapBrush>(MapBrushSet.MaintainBrushCollectionName);
            mbs.DB_ExternalCollection = mbs.Database.GetCollection(MapBrushSet.MaintainExternalCollectionName);
            mbs.DB_GroupCollection = mbs.Database.GetCollection<IMapGroup>(MapBrushSet.MaintainGroupCollectionName);

            if (info != null)
            {
                mbs.Properties = new Setting
                {
                    Summary = info.Summary,
                    Author = info.Author,
                    Cover = info.Cover,
                    Name = info.Name,
                    Tags = info.Tags
                };

                if (info.Cover is InDatabaseResource idr)
                {
                    if (string.IsNullOrEmpty(idr.FileName))
                    {
                        // TODO:
                        throw new InvalidOperationException();
                    }

                    if (mbs.Properties != null && mbs.Properties.Cover != null)
                    {
                        mbs.Database
                           .FileStorage
                           .Delete(mbs.Properties.Cover.Id);
                    }

                    try
                    {
                        idr.Id = Factory.GenereateGuid();

                        mbs.Database
                           .FileStorage
                           .Upload(idr.Id,
                                   idr.FileName);
                    }
                    catch (Exception ex)
                    {

                    }
                }

            }
            return mbs;
        }

        protected void UpdateSettingCore(Setting setting)
        {
            if (setting == null)
            {
                return;
            }

            if (_MapBrushSet == null)
            {

            }

            if (_MapBrushSet is MapBrushSet mbs)
            {
                mbs.DB_ExternalCollection.Upsert(
                    MapBrushSet.MaintainSettingName,
                    BsonMapper.Global.ToDocument(setting));
            }
        }

        protected virtual void UpdateCoverCore(Resource resource)
        {
            if (resource is InDatabaseResource idr && _MapBrushSet is MapBrushSet mbs)
            {
                if (string.IsNullOrEmpty(idr.FileName))
                {
                    // TODO:
                    throw new InvalidOperationException();
                }

                if (mbs.Properties != null && mbs.Properties.Cover != null)
                {
                    mbs.Database
                       .FileStorage
                       .Delete(mbs.Properties.Cover.Id);
                }

                try
                {
                    idr.Id = Factory.GenereateGuid();

                    mbs.Database
                       .FileStorage
                       .Upload(idr.Id,
                               idr.FileName);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public void Dispose()
        {
            _BrushSetDisposable.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        public MapBrushSet BrushSet
        {
            get => _MapBrushSet;
            private set
            {
                _MapBrushSet?.Dispose();
                _MapBrushSet = value;
                BrushSetChanged?.Invoke(_MapBrushSet);
            }
        }

        /// <summary>
        /// 获取或设置当前画刷集的名字。
        /// </summary>
        public string Name
        {
            get => _MapBrushSet.Name;
            set
            {
                _MapBrushSet.Properties.Name = value;
                SettingChanged?.Invoke(_MapBrushSet.Properties);
            }
        }

        /// <summary>
        /// 获取或设置当前画刷集的作者。
        /// </summary>
        public string Author
        {
            get => _MapBrushSet.Author;
            set
            {
                _MapBrushSet.Properties.Author = value;
                SettingChanged?.Invoke(_MapBrushSet.Properties);
            }
        }

        /// <summary>
        /// 获取或设置当前画刷集的简介。
        /// </summary>
        public string Summary
        {
            get => _MapBrushSet.Summary;
            set
            {
                _MapBrushSet.Properties.Summary = value;
                SettingChanged?.Invoke(_MapBrushSet.Properties);
            }
        }

        /// <summary>
        /// 获取或设置当前画刷集的标签。
        /// </summary>
        public List<string> Tags
        {
            get => _MapBrushSet.Tags;
            set
            {
                _MapBrushSet.Properties.Tags = value;
                SettingChanged?.Invoke(_MapBrushSet.Properties);
            }
        }

        /// <summary>
        /// 获取或设置当前画刷集的封面。
        /// </summary>
        public InDatabaseResource Cover
        {
            get => _MapBrushSet.Cover;
            set
            {
                _MapBrushSet.Properties.Cover = value;
                SettingChanged?.Invoke(_MapBrushSet.Properties);
                CoverChanged?.Invoke(value);
            }
        }

        public ReadOnlyObservableCollection<IMapBrush> Brushes => _BindableMapBrushCollection;
        public ReadOnlyObservableCollection<MapGroupAdapter> Groups => _BindableMapGroupCollection;

        protected event Action<IMapGroup> GroupChanged;
        protected event Action<IMapBrush> BrushChanged;
        protected event Action<InDatabaseResource> CoverChanged;
        protected event Action<Setting> SettingChanged;
        public event Action<MapBrushSet> BrushSetChanged;
    }
}
