using Acorisoft.Studio.Documents.Engines;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    public class CompositionSetFileManager : ProjectSystemHandler, ICompositionSetFileManager
    {
        public CompositionSetFileManager(ICompositionSetRequestQueue requestQueue) : base(requestQueue)
        {
        }

        protected override void OnCompositionSetOpening(CompositionSetOpenNotification notification)
        {
        }

        protected override void OnCompositionSetClosing(CompositionSetCloseNotification notification)
        {
        }
    }
}