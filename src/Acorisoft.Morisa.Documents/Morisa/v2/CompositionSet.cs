using Acorisoft.Morisa.Core;
using Acorisoft.Morisa.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    partial class CompositionSet : DataSet<CompositionSetInformation>
    {
        public const string ImageDirectoryName = "Images";
        public const string VideoDirectoryName = "Videos";
        public const string FontsDirectoryName = "Fonts";
        public const string BrushesDirectoryName = "Brushes";

        private protected readonly DelegateObserver<Resource>                   ResourceStream;
        private protected readonly IDisposable                                  ResourceDisposable;


        public CompositionSet()
        {
            ResourceStream = new DelegateObserver<Resource>(ResourceChanged);
            ResourceDisposable = Observable.FromEvent<Resource>(x => ResourceChangedEvent += x, x => ResourceChangedEvent -= x)
                                           .SubscribeOn(ThreadPoolScheduler.Instance)
                                           .Subscribe(x =>
                                           {
                                               OnResourceChanged(x);
                                               OnResourceChangedCore(x);
                                           });
        }


        protected override void OnDisposeCore()
        {
            ResourceDisposable?.Dispose();
            ResourceStream.Dispose();
        }

        internal protected static string EnsureDirectory(string directory)
        {
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }

            return directory;
        }


        protected void ResourceChanged(Resource resource)
        {
            ResourceChangedEvent?.Invoke(resource);
        }

        protected void OnResourceChanged(Resource resource)
        {
            if (resource is InDatabaseResource idr)
            {
                PerformanceInDatabaseResource(idr);
            }
            else if (resource is OutsideResource osr)
            {
                PerformanceOutsideResource(osr);
            }
        }

        protected void PerformanceInDatabaseResource(InDatabaseResource idr)
        {
            if (idr == null)
            {

            }

            if (!File.Exists(idr.FileName))
            {

            }

            idr.Id = Factory.GenerateId();
            try
            {
                Database.FileStorage.Upload(idr.Id, idr.FileName);
            }
            catch
            {
                // TODO: Handle Exception
            }
        }

        protected void PerformanceOutsideResource(OutsideResource osr)
        {
            if (osr == null)
            {

            }

            if (!File.Exists(osr.FileName))
            {

            }

            osr.Id = Factory.GenerateId();
            try
            {
                OnPerformanceOutsideResource(osr);
            }
            catch
            {
                // TODO: Handle Exception
            }
        }

        protected virtual void OnPerformanceOutsideResource(OutsideResource resource)
        {

        }

        protected virtual void OnResourceChangedCore(Resource resource)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public string FileName
        {
            get; internal set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Directory
        {
            get;
            internal set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ImageDirectory
        {
            get
            {
                return EnsureDirectory(Path.Combine(Directory, ImageDirectoryName));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string VideoDirectory
        {
            get
            {
                return EnsureDirectory(Path.Combine(Directory, VideoDirectoryName));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FontsDirectory
        {
            get
            {
                return EnsureDirectory(Path.Combine(Directory, FontsDirectoryName));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string BrushesDirectory
        {
            get
            {
                return EnsureDirectory(Path.Combine(Directory, BrushesDirectory));
            }
        }



        public IObserver<Resource> Resource => ResourceStream;

        /// <summary>
        /// 由事件转为可观测序列。
        /// </summary>
        internal event Action<Resource> ResourceChangedEvent;
    }
}
