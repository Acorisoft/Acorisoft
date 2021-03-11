using Acorisoft.Morisa.Core;
using Acorisoft.Properties;
using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.IO
{

#pragma warning disable IDE0063

    public class MorisaFileManager : IMorisaFileManager
    {
        public const int Threshold = 16 * 1024 * 1024;
        //-------------------------------------------------------------------------------------------------
        //
        //  Internal Classes
        //
        //-------------------------------------------------------------------------------------------------

        protected class FileManagerProjectReceiver : IObserver<IMorisaProject>
        {
            private readonly MorisaFileManager _Manager;
            private IMorisaProject _Project;

            internal FileManagerProjectReceiver(MorisaFileManager manager)
            {
                _Manager = manager;
            }

            public void OnCompleted()
            {
                if (_Project is MorisaProject mProj)
                {
                    _Manager._Database = mProj.Database ?? throw new InvalidOperationException();

                    foreach (var token in _Manager._Tokens)
                    {
                        token.Cancel();
                    }

                    _Manager._Tokens.Clear();
                }
            }

            public void OnError(Exception error)
            {
            }

            public void OnNext(IMorisaProject value)
            {
                _Project = value ?? throw new InvalidOperationException();
            }
        }

        protected class CompletedStream : Observable<BinaryObject>
        {
            public void Notification(BinaryObject value)
            {
                if (value != null)
                {
                    foreach (var observer in _List)
                    {
                        observer.OnNext(value);
                        observer.OnCompleted();
                    }
                }
            }

            public void Error(Exception ex)
            {
                if (ex != null)
                {
                    foreach (var observer in _List)
                    {
                        observer.OnError(ex);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Variables
        //
        //-------------------------------------------------------------------------------------------------
        private readonly List<CancellationTokenSource>  _Tokens;
        private readonly FileManagerProjectReceiver     _Receiver;
        private readonly CompletedStream                _Completed;
        private ILiteDatabase        _Database;
        private readonly MD5         _MD5;

        //-------------------------------------------------------------------------------------------------
        //
        //  Constructors
        //
        //-------------------------------------------------------------------------------------------------
        public MorisaFileManager()
        {
            _Receiver = new FileManagerProjectReceiver(this);
            _Tokens = new List<CancellationTokenSource>();
            _Completed = new CompletedStream();
            _MD5 = MD5.Create();
        }


        //-------------------------------------------------------------------------------------------------
        //
        //  Public Methods
        //
        //-------------------------------------------------------------------------------------------------

        string ComputeHash(Stream stream)
        {
            var rawArray = _MD5.ComputeHash(stream);
            stream.Position = 0;
            return Convert.ToBase64String(rawArray);
        }

        public void WriteImage(string fileName)
        {
            if (!File.Exists(fileName))
            {
                _Completed.Error(new InvalidOperationException(
                    SR.FileNotFound));
            }

            WriteImageCore(fileName);
            TaskPoolScheduler.Default.Schedule(x => { });
        }

        public void WriteImageCore(string fileName)
        {

            try
            {
                using (var FileStream = new FileStream(fileName , FileMode.Open))
                {
                    if (IgnoreFileDuplicate)
                    {
                        var Id = Guid.NewGuid();
                        var IdStr = Id.ToString("N");
                        _Database.FileStorage.Upload(IdStr , fileName , FileStream);
                    }
                    else
                    {
                        string Md5;

                        if (FileStream.Length > Threshold)
                        {
                            //
                            // 计算哈希值
                            Md5 = ComputeHash(FileStream);

                            if (_Database.FileStorage.Exists(Md5))
                            {
                                var value = new ImageObject
                                {
                                    Id = Md5
                                };
                                _Completed.Notification(value);
                            }
                            else
                            {
                                _Database.FileStorage.Upload(Md5 , fileName , FileStream);
                            }
                        }
                        else
                        {
                            using (var MemStream = new MemoryStream())
                            {

                                //
                                // 复制到内存，避免在低速设备中再读写一次
                                FileStream.CopyTo(MemStream);

                                //
                                // 计算哈希值
                                Md5 = ComputeHash(MemStream);

                                if (_Database.FileStorage.Exists(Md5))
                                {
                                    var value = new ImageObject
                                    {
                                        Id = Md5
                                    };
                                    _Completed.Notification(value);
                                }
                                else
                                {
                                    _Database.FileStorage.Upload(Md5 , fileName , MemStream);
                                }
                            }
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                _Completed.Error(ex);
            }
        }

        //-------------------------------------------------------------------------------------------------
        //
        //  Properties
        //
        //-------------------------------------------------------------------------------------------------
        public bool IgnoreFileDuplicate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IObserver<IMorisaProject> Project => _Receiver;

        /// <summary>
        /// 
        /// </summary>
        public IObservable<BinaryObject> Completed => _Completed;
    }
}
