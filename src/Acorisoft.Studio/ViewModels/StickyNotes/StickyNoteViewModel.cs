using System;
using System.Collections;
using System.Reactive.Disposables;
using System.Windows.Input;
using Acorisoft.Extensions.Platforms.Windows;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Documents.StickyNotes;
using Acorisoft.Studio.Engines;
using Acorisoft.Studio.ProjectSystems;
using ReactiveUI;

namespace Acorisoft.Studio.ViewModels
{
    public class StickyNoteViewModel : PageViewModelBase
    {
        private bool _isOpen;
        private bool _isChanged;
        private StickyNoteDocument _document;
        private StickyNoteIndex _index;
        private StickyNoteIndexWrapper _wrapper;
        private readonly StickyNoteEngine _engine;
        private readonly IComposeSetSystem _css;

        public StickyNoteViewModel(IComposeSetSystem css, StickyNoteEngine engine)
        {
            _css = css;
            _engine = engine ?? throw new ArgumentNullException(nameof(engine));
            SaveCommand = ReactiveCommand.Create(OnSave);
        }

        protected async void OnSave()
        {
            await _engine.UpdateAsync(_wrapper, _index, _document);
            ViewAware.Toast("保存成功");
        }

        protected override void OnParameterReceiving(Hashtable parameters)
        {
            if (parameters is GalleryViewModelParameter<StickyNoteIndex, StickyNoteIndexWrapper, StickyNoteDocument>
                parameter)
            {
                Parameter(parameter.Document, parameter.Index, parameter.Wrapper);
            }
        }

        private void Parameter(StickyNoteDocument document, StickyNoteIndex index, StickyNoteIndexWrapper wrapper)
        {
            _isOpen = document is not null;
            _document = document;
            _wrapper = wrapper;
            _index = index;
            RaiseUpdated(nameof(IsOpen));
            RaiseUpdated(nameof(IsOpen));
        }

        public string Name
        {
            get => _document.Name;
            set
            {
                _document.Name = value;
                _index.Name = value;
                _isChanged = true;
                RaiseUpdated(nameof(Name));
                RaiseUpdated(nameof(IsChanged));
            }
        }

        public ICommand SaveCommand { get; }
        public bool IsChanged => _isChanged;
        public bool IsOpen => _isOpen;
    }
}