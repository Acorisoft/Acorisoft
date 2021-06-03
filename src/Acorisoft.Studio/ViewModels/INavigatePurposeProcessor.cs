using System.Collections;

namespace Acorisoft.Studio.ViewModels
{
    public interface INavigatePurposeProcessor
    {
        bool IsAccept(object data);
        void Navigate(Hashtable parameter);
    }
}