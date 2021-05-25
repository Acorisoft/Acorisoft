using Acorisoft.Studio.Documents.Engines;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    public class CompositionSetFileManager : ProjectSystemHandler, ICompositionSetFileManager
    {
        private ICompositionSet _composition;
        public CompositionSetFileManager(ICompositionSetRequestQueue requestQueue) : base(requestQueue)
        {
        }

        protected override void OnCompositionSetOpening(CompositionSetOpenNotification notification)
        {
            _composition = notification.CompositionSet;
        }

        protected override void OnCompositionSetClosing(CompositionSetCloseNotification notification)
        {
            _composition = null;
        }
    }
}