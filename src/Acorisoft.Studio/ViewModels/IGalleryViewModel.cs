using System.Windows.Input;

namespace Acorisoft.Studio.ViewModels
{
    public interface IGalleryViewModel<TElement> : IGalleryViewModelDeleter, IGalleryViewModelPaginator
        where TElement : notnull
    {
        ISearchViewModel<TElement> Search { get; }
        ICommand OpenThisCommand { get; }
        ICommand NewCommand { get; }
    }
}