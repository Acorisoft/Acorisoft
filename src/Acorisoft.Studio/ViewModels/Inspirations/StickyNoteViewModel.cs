using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive.Disposables;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Acorisoft.Extensions.Platforms.Windows;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Documents.Inspirations;
using Acorisoft.Studio.Resources;
using Acorisoft.Studio.Documents.StickyNotes;
using Acorisoft.Studio.Engines;
using Acorisoft.Studio.ProjectSystems;
using Acorisoft.Studio.Properties;
using ReactiveUI;

namespace Acorisoft.Studio.ViewModels
{
    public class StickyNoteViewModel : PageViewModelBase
    {
        private StickyNoteInspiration _document;
        private InspirationIndex _index;
        private InspirationIndexWrapper _wrapper;
        private readonly IInspirationEngine _engine;
        private readonly IComposeSetSystem _system;
        public StickyNoteViewModel(IInspirationEngine engine, IComposeSetSystem system)
        {
            _engine = engine;
            _system = system;
            Albums = new ObservableCollection<ImageSource>();
            SaveCommand = ReactiveCommand.Create(OnSave);
        }

        protected async void OnSave()
        {
            await _engine.UpdateAsync(_wrapper, _index, _document);
        }

        protected override void OnParameterReceiving(Hashtable parameters)
        {
            if (parameters is not GalleryViewModelParameter<InspirationIndex, InspirationIndexWrapper, InspirationDocument> parameter)
            {
                Toast("未打开文件");
                return;
            }
            
            _document = parameter.Document as StickyNoteInspiration;

            if (_document == null)
            {
                Toast("打开错误");
                return;
            }
            
            _index = parameter.Index;
            _wrapper = parameter.Wrapper;

            if (_document.Album is AlbumResource album)
            {
                Albums.Clear();
                foreach (var stream in _system.Open(album))
                {
                    var ms = new MemoryStream((int) stream.Length);
                    stream.CopyTo(ms);
                    ms.Seek(0, SeekOrigin.Begin);

                    //
                    // 创建图片
                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = ms;
                    bitmapImage.EndInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnDemand;
                    bitmapImage.Freeze();
                    Albums.Add( bitmapImage);
                }
            }
            
            RaiseUpdated(nameof(Document));
            RaiseUpdated(nameof(Album));
            RaiseUpdated(nameof(Name));
            RaiseUpdated(nameof(Content));
        }
        
        public ICommand SaveCommand { get; }
        
        public ObservableCollection<ImageSource> Albums { get; }

        public string Name
        {
            get => _document.Name;
            set
            {
                
                _document.Name = value;
                RaiseUpdated();
            }
        }

        public ImageResource Album
        {
            get => _document.Album;
            set
            {
                _document.Album = value;
                RaiseUpdated();
            }
        }

        public string Content
        {
            get => _document.Content;
            set
            {
                _document.Content = value;
                _document.Summary = _document.Content.Substring(0, Math.Min(_document.Content.Length, 100));
                RaiseUpdated();
            }
        }

        public StickyNoteInspiration Document
        {
            get => _document;
        }
    }
}