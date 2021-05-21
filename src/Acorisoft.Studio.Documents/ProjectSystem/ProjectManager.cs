using System;
using DynamicData;

namespace Acorisoft.Studio.Document.ProjectSystem
{
    public class ProjectManager : IDisposable
    {
        private readonly SourceList<CompositionProject> 
        #region Disposable
        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
        }

        protected virtual void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ProjectManager()
        {
            Dispose(false);
        }
        
        #endregion
    }
}