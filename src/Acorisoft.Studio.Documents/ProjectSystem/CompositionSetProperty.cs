using System;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    public class CompositionSetProperty : ICompositionSetProperty
    {
        public CompositionSetProperty()
        {
            
        }

        internal CompositionSetProperty(INewProjectInfo newProjectInfo)
        {
            Name = newProjectInfo.Name ?? "未命名";
        }  
        
        public bool IsInitialized { get; set; }
        public string Name { get; set; }
        public Uri Cover { get; set; }
        public string Summary { get; set; }
        public DatabaseVersion Version { get; set; }
    }
}