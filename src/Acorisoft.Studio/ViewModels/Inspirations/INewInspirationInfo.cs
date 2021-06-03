using Acorisoft.Studio.Documents.Inspirations;

namespace Acorisoft.Studio.ViewModels
{
    public interface INewInspirationInfo : INewItemInfo<InspirationDocument, InspirationIndex>
    {
    }
    
    public interface INewStickyInfo: INewItemInfo<InspirationDocument, InspirationIndex>
    {
        StickyNoteInspiration StickyNote { get; }
    }
}