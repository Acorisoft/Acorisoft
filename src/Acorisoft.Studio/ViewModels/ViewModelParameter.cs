using Acorisoft.Studio.Documents.Inspirations;

namespace Acorisoft.Studio.ViewModels
{
    public static class ViewModelParameter
    {
        public static GalleryViewModelParameter<InspirationIndex, InspirationIndexWrapper, InspirationDocument>
            Parameter(InspirationIndex index, InspirationIndexWrapper wrapper, InspirationDocument document)
        {
            return new GalleryViewModelParameter<InspirationIndex, InspirationIndexWrapper, InspirationDocument>(index,
                wrapper, document);
        }
    }
}