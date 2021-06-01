using Acorisoft.Extensions.Platforms.ComponentModel;

namespace Acorisoft.Studio.Documents
{
    public class DocumentIndexWrapperVersion1<TIndex> : ObservableObject where TIndex : DocumentIndexVersion1
    {
        private bool _isLocked;
        private bool _isSelected;

        protected DocumentIndexWrapperVersion1(TIndex index)
        {
            Source = index;
        }

        public virtual void RaiseUpdated()
        {
            //
            // 更新
            RaiseUpdated(nameof(IsSelected));
            RaiseUpdated(nameof(IsLocked));
        }
        
        protected internal TIndex Source { get; private set; }

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