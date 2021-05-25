using System.Threading.Tasks;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    public class CompositionSetPropertyManager : DataPropertyManager<ICompositionSetProperty>, ICompositionSetPropertyManager
    {
        private readonly ICompositionSetFileManager _fileManager;

        public CompositionSetPropertyManager(ICompositionSetFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public async Task<ICompositionSetProperty> SetProperty(ICompositionSetProperty property)
        {
            //
            // TODO: File Operation
            if (property.Cover is not null)
            {
            }
            
            base.SetProperty(property);
            return property;
        }
    }
}