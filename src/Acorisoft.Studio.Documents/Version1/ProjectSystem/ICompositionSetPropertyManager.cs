using System;
using System.Threading.Tasks;

namespace Acorisoft.Studio.ProjectSystem
{
    public interface ICompositionSetPropertyManager : IDataPropertyManager
    {
        ICompositionSetProperty GetProperty();
        Task<ICompositionSetProperty> SetProperty(ICompositionSetProperty property);
        
        IObservable<ICompositionSetProperty> Property { get; }
    }
}