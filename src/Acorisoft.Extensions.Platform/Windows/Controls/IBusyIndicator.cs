namespace Acorisoft.Extensions.Windows.Controls
{
    public interface IBusyIndicator
    {
        bool IsBusy { get; set; }
        string Description { get; set; }
    }
}