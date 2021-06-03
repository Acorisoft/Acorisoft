using System.Collections;
using Acorisoft.Extensions.Platforms.Windows;
using Acorisoft.Studio.Documents.Inspirations;
using Acorisoft.Studio.Documents.StickyNotes;

namespace Acorisoft.Studio.ViewModels
{
    public sealed class GalleryViewModelParameter<TIndex, TWrapper, TComposition> : Hashtable
        where TIndex : DocumentIndex
        where TWrapper : DocumentIndexWrapper<TIndex>
        where TComposition : Document
    {
        public GalleryViewModelParameter(TIndex index, TWrapper wrapper, TComposition document)
        {
            Add(ViewAware.Arg1, document);
            Add(ViewAware.Arg2, index);
            Add(ViewAware.Arg3, wrapper);
        }

        public TComposition Document => base[ViewAware.Arg1] as TComposition;
        public TIndex Index => base[ViewAware.Arg2] as TIndex;
        public TWrapper Wrapper => base[ViewAware.Arg3] as TWrapper;
    }
}