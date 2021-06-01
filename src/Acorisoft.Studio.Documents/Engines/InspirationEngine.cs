using System;
using System.Threading.Tasks;
using Acorisoft.Studio.Documents;
using Acorisoft.Studio.Documents.Inspirations;
using Acorisoft.Studio.ProjectSystems;

namespace Acorisoft.Studio.Engines
{
    [StoragePlace(StorageClassifier.Custom, "Ins")]
    [Purpose(typeof(ConversationInspiration))]
    public class InspirationEngine : ComposeSetSystemModule<InspirationIndex, InspirationIndexWrapper, InspirationDocument>, IInspirationEngine
    {
        private const string DocumentName = "Ins";
        private const string IndexName = "Ins_Index";
        
        public InspirationEngine(IComposeSetRequestQueue requestQueue) : base(Transform, requestQueue,DocumentName,IndexName)
        {
            
        }

        #region Overrides

        

        protected sealed override void NewCore(INewItemInfo<InspirationDocument, InspirationIndex, InspirationDocument> info)
        {
            //
            // 之前的设计中，NewAsync 传递 INewItemInfo<T1,T2,T3> 参数到 NewCore中完成创建实例。
            //
            // 但是当前引擎为一组 Multi-Purpose 引擎，所以要创建的内容实际上在程序本身是不确定的，因此我们需要通过特殊的方式来实现添加。
            //base.NewCore(info);

            //
            // 一定要确保FeedbackValue2 的内容不能为空
            if (info.FeedBackValue2 == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            //
            //
            var index = CreateIndexInstance();
            
            //
            //
            var document = info.FeedBackValue2;
            
            //
            // 设置同样的唯一标识符。
            index.Id = document.Id = info.Id;

            //
            // 设置名称
            document.Name = index.Name = info.Name;

            //
            // 抽取关键字到索引当中
            ExtractIndex(index, document);

            //
            // 返回
            info.FeedBackValue1 = index;
            
            //
            // 插入
            IndexCollection.Insert(index);
            DocumentCollection.Insert(document);

            //
            // 刷新集合
            DemandRefreshDataSource();
        }

        protected sealed override void ExtractIndex(InspirationIndex index, InspirationDocument composition)
        {
            index.Name = string.IsNullOrEmpty(composition.Name)
                ? SR.StickyNoteEngine_EmptyDocumentName
                : composition.Name;
            
            index.Summary = string.IsNullOrEmpty(composition.Summary)
                ? SR.StickyNoteEngine_EmtpySummary
                : composition.Summary.Substring(0, Math.Min(composition.Summary.Length, 100));

            index.Type = composition.Type;
        }

        private static InspirationIndexWrapper Transform(InspirationIndex index)
        {
            return new InspirationIndexWrapper(index);
        }

        protected sealed override InspirationIndex CreateIndexInstance()
        {
            return new InspirationIndex
            {
                LastAccessTimestamp = DateTime.Now,
                CreationTimestamp = DateTime.Now
            };
        }

        protected sealed override InspirationDocument CreateCompositionInstance()
        {
            throw new System.NotImplementedException();
        }
        
        
        #endregion

        #region NewAsync

        public Task NewConversationAsync() => NewAsync(
            new NewItemInfo<InspirationDocument, InspirationIndex, InspirationDocument>
            {
                FeedBackValue2 = new ConversationInspiration()
            });

        #endregion
    }
}