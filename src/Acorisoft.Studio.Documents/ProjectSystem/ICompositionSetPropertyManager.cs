using System.Threading.Tasks;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    public interface ICompositionSetPropertyManager : IDataPropertyManager<ICompositionSetProperty>
    {
        Task<ICompositionSetProperty> SetProperty(ICompositionSetProperty property);
    }
}