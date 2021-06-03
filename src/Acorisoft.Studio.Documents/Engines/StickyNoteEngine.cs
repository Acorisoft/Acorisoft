using System;
using System.Collections.Generic;
using Acorisoft.Studio.Documents;
using Acorisoft.Studio.Documents.Inspirations;
using Acorisoft.Studio.Documents.StickyNotes;
using Acorisoft.Studio.ProjectSystems;

namespace Acorisoft.Studio.Engines
{
    [Obsolete]
    public class StickyNoteEngine : ComposeSetSystemModule<InspirationIndex, InspirationIndexWrapper, StickyNoteInspiration> , IStickyNoteEngine
    {
        private const string CollectionName = "StickyNote";
        private const string IndexesName = "Note_Index";

        public StickyNoteEngine(IComposeSetRequestQueue requestQueue) : base(Transform, requestQueue, CollectionName, IndexesName)
        {
        }

        protected override InspirationIndex CreateIndexInstance()
        {
            return new InspirationIndex
            {
                LastAccessTimestamp = DateTime.Now,
                CreationTimestamp = DateTime.Now
            };
        }

        protected override void ExtractIndex(InspirationIndex index, StickyNoteInspiration composition)
        {
            index.Name = string.IsNullOrEmpty(composition.Name)
                ? SR.StickyNoteEngine_EmptyDocumentName
                : composition.Name;
            
            index.Summary = string.IsNullOrEmpty(composition.Content)
                ? SR.StickyNoteEngine_EmtpySummary
                : composition.Content.Substring(0, Math.Min(composition.Content.Length, 100));
        }

        protected override StickyNoteInspiration CreateCompositionInstance()
        {
            return new StickyNoteInspiration
            {
                LastAccessTimestamp = DateTime.Now,
                CreationTimestamp = DateTime.Now
            };
        }

        private static InspirationIndexWrapper Transform(InspirationIndex index)
        {
            return new InspirationIndexWrapper(index);
        }

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

        public class AscendingByCreatedTimeSorter : IComparer<StickyNoteIndexWrapper>
        {
            public int Compare(StickyNoteIndexWrapper? x, StickyNoteIndexWrapper? y)
            {
                if (x == null || y == null)
                {
                    return 0;
                }

                var xTicks = x.Source.CreationTimestamp.Ticks;
                var yTicks = y.Source.CreationTimestamp.Ticks;

                if (xTicks > yTicks)
                {
                    return 1;
                }

                return xTicks < yTicks ? -1 : 0;
            }
        }

        public class AscendingByLastAccessTimeSorter : IComparer<StickyNoteIndexWrapper>
        {
            public int Compare(StickyNoteIndexWrapper? x, StickyNoteIndexWrapper? y)
            {
                if (x == null || y == null)
                {
                    return 0;
                }

                var xTicks = x.Source.LastAccessTimestamp.Ticks;
                var yTicks = y.Source.LastAccessTimestamp.Ticks;

                if (xTicks > yTicks)
                {
                    return 1;
                }

                return xTicks < yTicks ? -1 : 0;
            }
        }

        public class AscendingByNameSorter : IComparer<StickyNoteIndexWrapper>
        {
            public int Compare(StickyNoteIndexWrapper? x, StickyNoteIndexWrapper? y)
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

        public class DescendingByCreatedTimeSorter : IComparer<StickyNoteIndexWrapper>
        {
            public int Compare(StickyNoteIndexWrapper? x, StickyNoteIndexWrapper? y)
            {
                if (x == null || y == null)
                {
                    return 0;
                }

                var xTicks = x.Source.CreationTimestamp.Ticks;
                var yTicks = y.Source.CreationTimestamp.Ticks;

                if (xTicks > yTicks)
                {
                    return -1;
                }

                return xTicks < yTicks ? 1 : 0;
            }
        }

        public class DescendingByLastAccessTimeSorter : IComparer<StickyNoteIndexWrapper>
        {
            public int Compare(StickyNoteIndexWrapper? x, StickyNoteIndexWrapper? y)
            {
                if (x == null || y == null)
                {
                    return 0;
                }

                var xTicks = x.Source.LastAccessTimestamp.Ticks;
                var yTicks = y.Source.LastAccessTimestamp.Ticks;

                if (xTicks > yTicks)
                {
                    return -1;
                }

                return xTicks < yTicks ? 1 : 0;
            }
        }

        public class DescendingByNameSorter : IComparer<StickyNoteIndexWrapper>
        {
            public int Compare(StickyNoteIndexWrapper? x, StickyNoteIndexWrapper? y)
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

        public static IComparer<StickyNoteIndexWrapper> Normal { get; } = Comparer<StickyNoteIndexWrapper>.Default;
        public static IComparer<StickyNoteIndexWrapper> AscendingByCreationTimestamp { get; } = new AscendingByCreatedTimeSorter();
        public static IComparer<StickyNoteIndexWrapper> AscendingByName { get; } = new AscendingByNameSorter();

        public static IComparer<StickyNoteIndexWrapper> AscendingByLastAccessTimestamp { get; } =
            new AscendingByLastAccessTimeSorter();

        public static IComparer<StickyNoteIndexWrapper> DescendingByCreationTimestamp { get; } = new DescendingByCreatedTimeSorter();
        public static IComparer<StickyNoteIndexWrapper> DescendingByName { get; } = new DescendingByNameSorter();

        public static IComparer<StickyNoteIndexWrapper> DescendingByLastAccessTimestamp { get; } =
            new DescendingByLastAccessTimeSorter();

        #endregion
    }
}