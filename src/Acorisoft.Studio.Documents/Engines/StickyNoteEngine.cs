using System;
using System.Collections.Generic;
using Acorisoft.Studio.Documents.StickyNotes;
using Acorisoft.Studio.ProjectSystem;
using LiteDB;

namespace Acorisoft.Studio.Engines
{
    public class StickyNoteEngine : DocumentGalleryEngine<StickyNoteIndex, StickyNoteIndexWrapper, StickyNoteDocument>
    {
        public const string CollectionName = "StickyNote";
        public const string IndexesName = "Note_Index";

        public StickyNoteEngine(ICompositionSetRequestQueue requestQueue) : base(Transform, requestQueue,
            CollectionName, IndexesName)
        {
        }

        #region Override Methods

        

        protected override void ExtractIndex(StickyNoteIndex index, StickyNoteDocument document)
        {
            base.ExtractIndex(index, document);
        }

        protected override Query ConstructFindExpression(string keyword)
        {
            return base.ConstructFindExpression(keyword);
        }

        private static StickyNoteIndexWrapper Transform(StickyNoteIndex index)
        {
            return new StickyNoteIndexWrapper(index);
        }

        protected override StickyNoteIndex CreateIndexInstance(INewDocumentInfo<StickyNoteDocument> info)
        {
            return new StickyNoteIndex();
        }

        protected override StickyNoteDocument CreateDocumentInstance(INewDocumentInfo<StickyNoteDocument> info)
        {
            return new StickyNoteDocument();
        }

        protected override BsonDocument SerializeIndex(StickyNoteIndex index)
        {
            throw new NotImplementedException();
        }

        protected override BsonDocument SerializeDocument(StickyNoteDocument document)
        {
            throw new NotImplementedException();
        }

        protected override StickyNoteIndex DeserializeIndex(BsonValue value)
        {
            throw new NotImplementedException();
        }

        protected override StickyNoteDocument DeserializeDocument(BsonValue value)
        {
            throw new NotImplementedException();
        }

        #endregion
        
        public static IComparer<StickyNoteIndexWrapper> Sorter { get; }
    }
}