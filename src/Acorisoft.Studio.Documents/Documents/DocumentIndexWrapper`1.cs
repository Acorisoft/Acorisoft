using Acorisoft.Extensions.Platforms.ComponentModel;

namespace Acorisoft.Studio.Documents
{
    public class DocumentIndexWrapper<TIndex> : ObservableObject where TIndex : DocumentIndex
    {
        private bool _isLocked;
        private bool _isSelected;

        public DocumentIndexWrapper(TIndex index)
        {
            Source = index;
        }
        
        protected TIndex Source { get; }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetValue(ref _isSelected, value);
        }

        public bool IsLocked
        {
            get => _isLocked;
            set => SetValue(ref _isLocked, value);
        }
    }
}