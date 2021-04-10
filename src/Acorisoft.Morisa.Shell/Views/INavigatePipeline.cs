namespace Acorisoft.Views
{
    public interface INavigatePipeline
    {
        void OnNext(INavigateContext context);
    }
}