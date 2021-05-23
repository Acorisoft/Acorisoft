using System;
using Acorisoft.Extension.ComponentModels;
using DynamicData;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    public class ProjectManager : ObservableObject 
    {
        private protected readonly SourceList<CompositionProject> Source;

        public ProjectManager()
        {
            //
            // 这是从数据库中加载项目
            Source = new SourceList<CompositionProject>();
            // Source.Connect()
            //     .Filter(x => x != null)
            //     .Page();
        }
    }
}