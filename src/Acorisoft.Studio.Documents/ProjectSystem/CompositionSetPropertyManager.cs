namespace Acorisoft.Studio.Documents.ProjectSystem
{
    public class CompositionSetPropertyManager : DataPropertyManager<ICompositionSetProperty>, ICompositionSetPropertyManager
    {
        private readonly ICompositionSetFileManager _fileManager;

        public CompositionSetPropertyManager(ICompositionSetFileManager fileManager)
        {
            _fileManager = fileManager;
        }
    }
}