using System.Windows.Input;

namespace Acorisoft.Studio.ViewModels
{
    public interface IGalleryViewModelDeleter
    {
        /// <summary>
        /// 
        /// </summary>
        ICommand DeleteThisCommand { get; }
        
        /// <summary>
        /// 
        /// </summary>
        ICommand DeleteThisPageCommand { get; }
        
        /// <summary>
        /// 
        /// </summary>
        ICommand DeleteAllCommand { get; }
    }
}