using Acorisoft.Morisa.Core;
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

namespace Acorisoft.Morisa.Emotions
{
#pragma warning disable IDE0044

    public class EmotionMechanism : MechanismCore, IEmotionMechanism
    {
        //-------------------------------------------------------------------------------------------------
        //
        //  GenereateGuid
        //
        //-------------------------------------------------------------------------------------------------
        public const string MaintainCollectionName = "Emotion";
        //-------------------------------------------------------------------------------------------------
        //
        //  GenereateGuid
        //
        //-------------------------------------------------------------------------------------------------
        private LiteCollection<IEmotionElement>                 _DatabaseCollection;
        private ReadOnlyObservableCollection<IEmotionElement>   _BindableCollection;
        private SourceList<IEmotionElement>                     _EditableCollection;
        //-------------------------------------------------------------------------------------------------
        //
        //  GenereateGuid
        //
        //-------------------------------------------------------------------------------------------------
        public EmotionMechanism() : base()
        {
            _EditableCollection = new SourceList<IEmotionElement>();
            _EditableCollection.Connect()
                               .Bind(out _BindableCollection)
                               .SubscribeOn(TaskPoolScheduler.Default)
                               .Subscribe(x => Save());
        }
        //-------------------------------------------------------------------------------------------------
        //
        //  GenereateGuid
        //
        //-------------------------------------------------------------------------------------------------
        public void Add(IEmotionElement emotion)
        {
            if(emotion == null)
            {
                throw new InvalidOperationException("无效的操作");
            }

            _EditableCollection.Add(emotion);
        }


        public void Remove(IEmotionElement emotion)
        {
            if (emotion == null)
            {
                throw new InvalidOperationException("无效的操作");
            }

            _EditableCollection.Remove(emotion);
        }

        public void Clear()
        {
            _EditableCollection.Clear();
        }


        public void Save()
        {
            _DatabaseCollection.Upsert(_BindableCollection);
        }
        //-------------------------------------------------------------------------------------------------
        //
        //  GenereateGuid
        //
        //-------------------------------------------------------------------------------------------------

        protected override sealed bool DetermineDatabaseInitialization(LiteDatabase database)
        {
            return database.CollectionExists(MaintainCollectionName);
        }

        protected override void OnInitializeEmotionFromDatabase(LiteDatabase database)
        {
            //
            // 从数据库中获取对应的集合
            _DatabaseCollection = database.GetCollection<IEmotionElement>(MaintainCollectionName);
            _EditableCollection.Clear();
            _EditableCollection.AddRange(_DatabaseCollection.FindAll());
        }

        protected override void OnInitializeEmotionWithConstruct(LiteDatabase database)
        {

            //
            // 从数据库中获取对应的集合
            _DatabaseCollection = database.GetCollection<IEmotionElement>(MaintainCollectionName);
            _EditableCollection.Clear();
            _DatabaseCollection.Upsert(_BindableCollection);
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  GenereateGuid
        //
        //-------------------------------------------------------------------------------------------------
        public ReadOnlyObservableCollection<IEmotionElement> Collection => _BindableCollection;
    }
}
