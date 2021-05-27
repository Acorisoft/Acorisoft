using System.Collections;
using Acorisoft.Extensions.Platforms.Windows;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Documents.StickyNote;

namespace Acorisoft.Studio.ViewModels
{
    public class StickyNoteViewModel : PageViewModelBase
    {
        public StickyNoteViewModel()
        {
            
        }

        protected override void OnParameterReceiving(Hashtable parameters)
        {
            if (parameters.ContainsKey(ViewAware.Arg1))
            {
                Parameter(parameters[ViewAware.Arg1] as StickyNoteDocument);
            }
        }

        protected void Parameter(StickyNoteDocument document)
        {
            
        }
    }
}