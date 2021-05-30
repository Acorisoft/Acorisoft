using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        //-----------------------------------------------------------------------
        //
        //  Sorter Classes And Static Sorter Instance
        //
        //-----------------------------------------------------------------------
        
        #region Sorter

        //-----------------------------------------------------------------------
        //
        //  排序工具专门用于为数据引擎提供排序支持，排序工具主要分为升序排序（Ascending）和降序排序（Descending）
        //
        //  1) 升序， 从小到大排列
        //  2) 降序, 从大到小排列
        //
        //  *) Compare 方法, x > y = 1  x < y = -1 x == y = 1  
        //
        //-----------------------------------------------------------------------

        #region Ascending Sorter
        
        public class AscendingByCreatedTimeSorter : IComparer<StickyNoteDocument>
        {
            public int Compare(StickyNoteDocument? x, StickyNoteDocument? y)
            {
                if (x == null || y == null)
                {
                    return 0;
                }

                var xTicks = x.CreatedBy.Ticks;
                var yTicks = y.CreatedBy.Ticks;

                if (xTicks > yTicks)
                {
                    return 1;
                }

                return xTicks < yTicks ? -1 : 0;
            }
        }
        
        public class AscendingByLastAccessTimeSorter : IComparer<StickyNoteDocument>
        {
            public int Compare(StickyNoteDocument? x, StickyNoteDocument? y)
            {
                if (x == null || y == null)
                {
                    return 0;
                }

                var xTicks = x.LastAccessBy.Ticks;
                var yTicks = y.LastAccessBy.Ticks;

                if (xTicks > yTicks)
                {
                    return 1;
                }

                return xTicks < yTicks ? -1 : 0;
            }
        }

        public class AscendingByNameSorter : IComparer<StickyNoteDocument>
        {
            public int Compare(StickyNoteDocument? x, StickyNoteDocument? y)
            {
                if (x == null || y == null)
                {
                    return 0;
                }

                var xName = x.Name;
                var yName = y.Name;

                return string.Compare(xName, yName, StringComparison.Ordinal);
            }
        }

        #endregion
        
        #region Descending Sorter
        
        public class DescendingByCreatedTimeSorter : IComparer<StickyNoteDocument>
        {
            public int Compare(StickyNoteDocument? x, StickyNoteDocument? y)
            {
                if (x == null || y == null)
                {
                    return 0;
                }

                var xTicks = x.CreatedBy.Ticks;
                var yTicks = y.CreatedBy.Ticks;

                if (xTicks > yTicks)
                {
                    return -1;
                }

                return xTicks < yTicks ? 1 : 0;
            }
        }
        
        public class DescendingByLastAccessTimeSorter : IComparer<StickyNoteDocument>
        {
            public int Compare(StickyNoteDocument? x, StickyNoteDocument? y)
            {
                if (x == null || y == null)
                {
                    return 0;
                }

                var xTicks = x.LastAccessBy.Ticks;
                var yTicks = y.LastAccessBy.Ticks;

                if (xTicks > yTicks)
                {
                    return -1;
                }

                return xTicks < yTicks ? 1 : 0;
            }
        }
        
        public class DescendingByNameSorter : IComparer<StickyNoteDocument>
        {
            public int Compare(StickyNoteDocument? x, StickyNoteDocument? y)
            {
                if (x == null || y == null)
                {
                    return 0;
                }

                var xName = x.Name;
                var yName = y.Name;

                return string.Compare(yName, xName, StringComparison.Ordinal);
            }
        }
        #endregion

        public static IComparer<StickyNoteDocument> AscendingByCreateBy { get; } = new AscendingByCreatedTimeSorter();
        public static IComparer<StickyNoteDocument> AscendingByName { get; } = new AscendingByNameSorter();
        public static IComparer<StickyNoteDocument> AscendingByLastAccessBy { get; } = new AscendingByLastAccessTimeSorter();
        public static IComparer<StickyNoteDocument> DescendingByCreateBy { get; } = new DescendingByCreatedTimeSorter();
        public static IComparer<StickyNoteDocument> DescendingByName { get; } = new DescendingByNameSorter();
        public static IComparer<StickyNoteDocument> DescendingByLastAccessBy { get; } = new DescendingByLastAccessTimeSorter();
        #endregion
    }
}