using System.Collections;
using Acorisoft.Extensions.Platforms.Windows.ViewModels;
using Acorisoft.Studio.Documents.Inspirations;

namespace Acorisoft.Studio.ViewModels
{
    public class ConversationViewModel : PageViewModelBase
    {
        protected override void OnParameterReceiving(Hashtable parameters)
        {
            if (parameters is GalleryViewModelParameter<InspirationIndex, InspirationIndexWrapper, InspirationDocument> parameter)
            {
                
            }
        }
    }
}