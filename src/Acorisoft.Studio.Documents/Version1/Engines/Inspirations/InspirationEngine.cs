using System;
using Acorisoft.Studio.Documents.Inspirations;
using Acorisoft.Studio.ProjectSystem;
using LiteDB;

namespace Acorisoft.Studio.Engines
{
    /// <summary>
    /// <see cref="InspirationEngine"/> 表示灵感集引擎。用于为应用程序提供灵感收集相关的操作。
    /// </summary>
    public class InspirationEngineVersion1 : ProjectSystemModule
    {
        //-----------------------------------------------------------------------
        //
        //  Constants
        //
        //-----------------------------------------------------------------------
        public const string CollectionName = "Inspirations";
        
        
        //-----------------------------------------------------------------------
        //
        //  Fields
        //
        //-----------------------------------------------------------------------
        private readonly ICompositionSetFileManager _fileManager;
        private LiteCollection<InspirationDocument> _documents;
        
        
        public InspirationEngineVersion1(ICompositionSetFileManager fileManager, ICompositionSetRequestQueue requestQueue) : base(requestQueue)
        {
            _fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
        }

        #region Override Methods

        protected override void OnCompositionSetOpening(CompositionSetOpenNotification notification)
        {
            _documents = notification.MainDatabase.GetCollection<InspirationDocument>();
        }

        protected override void OnCompositionSetClosing(CompositionSetCloseNotification notification)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnCompositionSetSaving(CompositionSetSaveNotification notification)
        {
            throw new System.NotImplementedException();
        }
        
        #endregion
    }
}