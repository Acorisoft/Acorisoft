namespace Acorisoft.Extensions.Windows.Controls
{
    public interface IEmptyStateView
    {
        object Content { get; set; }
        object EmptyState { get; set; }
        bool IsEmptyState { get; set; }
        bool HasContentState { get; set; }
    }
}