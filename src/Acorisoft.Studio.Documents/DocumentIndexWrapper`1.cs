namespace Acorisoft.Studio
{
    public class DocumentIndexWrapper<TIndex> : DocumentIndexWrapper
    {
        
        protected DocumentIndexWrapper(TIndex index)
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
        
        public TIndex Source { get; private set; }
    }
}