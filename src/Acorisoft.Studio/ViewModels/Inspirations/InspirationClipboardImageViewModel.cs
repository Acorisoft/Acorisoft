using System;
using System.Windows.Media;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Documents.Inspirations;

namespace Acorisoft.Studio.ViewModels
{
    public class InspirationClipboardImageViewModel: DialogViewModelBase, INewStickyInfo
    {
        public InspirationClipboardImageViewModel(ImageSource source)
        {
            Image = source;
            StickyNote = new StickyNoteInspiration
            {
                Name = DateTime.Now.ToShortDateString(),
                Summary = DateTime.Now.ToShortDateString(),
                LastAccessTimestamp = DateTime.Now,
                CreationTimestamp = DateTime.Now
            };
            Id = Guid.NewGuid();
        }

        public sealed override bool VerifyAccess()
        {
            return true;
        }
        
        public sealed override bool Accept<T>()
        {
            return typeof(T) == typeof(InspirationClipboardTextViewModel) || typeof(T) == typeof(INewStickyInfo);
        }

        public sealed override object GetResult() => (INewStickyInfo) this;

        public ImageSource Image { get; }
        public Guid Id { get; set; }

        public string Name
        {
            get => Item.Name;
            set
            {
                Item.Name = value;
                RaiseUpdated(nameof(Name));
            }
        }
        public InspirationDocument Item
        {
            get => StickyNote;
            set
            {
                
            }
        }
        public StickyNoteInspiration StickyNote { get; set; }
        public string Path { get; set; }
        public InspirationIndex FeedBackValue1 { get; set; }
    }

}