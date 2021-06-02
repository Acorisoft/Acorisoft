using System;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Documents.Inspirations;

namespace Acorisoft.Studio.ViewModels
{
    public class InspirationClipboardTextViewModel : DialogViewModelBase, INewInspirationInfo
    {
        public InspirationClipboardTextViewModel(string sourceText)
        {
            Text = sourceText;
            Item = new StickyNoteInspiration
            {
                Name = DateTime.Now.ToShortDateString(),
                Summary = DateTime.Now.ToShortDateString()
            };
            
            Id = Guid.NewGuid();
        }

        public sealed override bool VerifyAccess()
        {
            return !string.IsNullOrEmpty(Text);
        }
        
        public sealed override bool Accept<T>()
        {
            return typeof(T) == typeof(InspirationClipboardTextViewModel) || typeof(T) == typeof(INewInspirationInfo);
        }

        public sealed override object GetResult() => (INewInspirationInfo) this;

        public string Text { get; }
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
        public InspirationDocument Item { get; set; }
        public string Path { get; set; }
        public InspirationIndex FeedBackValue1 { get; set; }
    }
}