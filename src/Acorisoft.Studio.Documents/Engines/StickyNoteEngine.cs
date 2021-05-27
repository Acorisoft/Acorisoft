using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Acorisoft.Studio.Documents.Inspirations;
using Acorisoft.Studio.Documents.StickyNote;
using Acorisoft.Studio.ProjectSystem;
using DynamicData;
using LiteDB;

namespace Acorisoft.Studio.Engines
{
    public class StickyNoteEngine : ProjectSystemModule
    {
        //-----------------------------------------------------------------------
        //
        //  Constants
        //
        //-----------------------------------------------------------------------
        public const string CollectionName = "StickyNotes";
        public const string IndexName = "Note_Index";
        
        
        //-----------------------------------------------------------------------
        //
        //  Fields
        //
        //-----------------------------------------------------------------------
        private LiteCollection<StickyNoteIndex> _indexs;
        private LiteCollection<StickyNoteDocument> _documents;
        private readonly SourceList<StickyNoteIndex> _editable;
        private readonly ReadOnlyObservableCollection<StickyNoteIndex> _bindable;
        private bool _isOpen;
        private int _totalIndexCount;
        private int _perPageCount;
        private int _totalPageCount;
        private int _currentPageIndex;
        
        public StickyNoteEngine(ICompositionSetRequestQueue requestQueue) : base(requestQueue)
        {
            //
            // 设置每页内容数量，默认为25个
            _perPageCount = 25;
            _currentPageIndex = 1;
            _editable = new SourceList<StickyNoteIndex>();
            _editable.Connect().Bind(out _bindable).Subscribe();
        }

        #region Override Methods

        protected override void OnCompositionSetOpening(CompositionSetOpenNotification notification)
        {
            _isOpen = true;
            
            //
            //
            _currentPageIndex = 1;
            
            //
            // 获取所有便利贴的索引
            _indexs = notification.MainDatabase.GetCollection<StickyNoteIndex>(IndexName);
            
            //
            // 获取内容
            _documents = notification.MainDatabase.GetCollection<StickyNoteDocument>(CollectionName);
            
            //
            // 获取所有内容的数量
            _totalIndexCount = _indexs.Count();
            
            //
            // 获取所有页面的数量。
            _totalPageCount = (_totalIndexCount + _perPageCount) / _perPageCount;

            //
            // 刷新
            OnDemandPageRefresh(_perPageCount, _currentPageIndex);
            
            
                
            RaiseUpdated(nameof(PageCount));
            RaiseUpdated(nameof(PageIndex));    
            RaiseUpdated(nameof(PerPageCount));
            
        }

        protected override void OnCompositionSetClosing(CompositionSetCloseNotification notification)
        {
            _isOpen = false;
            _indexs = null;
            _documents = null;
            _editable.Clear();
            _currentPageIndex = 1;
        }

        protected override void OnCompositionSetSaving(CompositionSetSaveNotification notification)
        {
            
        }
        
        #endregion

        public async Task<StickyNoteDocument> Open(StickyNoteIndex index)
        {
            if (index == null)
            {
                throw new ArgumentNullException(nameof(index));
            }

            if (index.Id == Guid.Empty)
            {
                throw new InvalidOperationException("无效的操作,无法打开一个无效的文档");
            }

            return null;
        }
        
        protected virtual void OnDemandPageRefresh(int pageCount, int pageIndex)
        {
            //
            // Avoid Exception
            if (!_isOpen)
            {
                return;
            }
            
            //
            // 限制大小
            pageCount = Math.Clamp(pageCount, 1, byte.MaxValue);
            pageIndex = Math.Clamp(pageIndex, 1, ushort.MaxValue);
            
            //
            // 清空所有内容
            _editable.Clear();

            //
            // 获得需要加载的内容
            var enumeration = _indexs.FindAll()
                .Skip(_currentPageIndex * _perPageCount)
                .Take(_perPageCount);
            
            _editable.AddRange(enumeration);
        }
        
        public int PageIndex
        {
            get => _currentPageIndex;
            set
            {
                if (value is < 0 or > 100)
                {
                    throw new IndexOutOfRangeException(nameof(value));
                }
                _currentPageIndex = value;
                OnDemandPageRefresh(_perPageCount,value);
                
                RaiseUpdated();
            }
        }
        
        public int PerPageCount
        {
            get => _perPageCount;
            set
            {
                if (value is < 0 or > 100)
                {
                    throw new IndexOutOfRangeException(nameof(value));
                }
                
                //
                //
                _perPageCount = value;

                //
                //
                OnDemandPageRefresh(value, _currentPageIndex);
                
                RaiseUpdated();
            }
        }

        public int PageCount => _totalPageCount;

        public ReadOnlyObservableCollection<StickyNoteIndex> Collection => _bindable;
    }
}