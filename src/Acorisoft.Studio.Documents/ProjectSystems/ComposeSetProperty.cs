using Acorisoft.Studio.Resources;

namespace Acorisoft.Studio.ProjectSystems
{
    public class ComposeSetProperty : IComposeSetProperty
    {
        public string Name { get; set; }
        public ImageResource Album { get; set; }
        public string Summary { get; set; }
        public RepositoryType RepositoryType { get; set; }
        public string Path { get; set; }
        public string Repository { get; set; }
        public string RepositoryEmailSetting { get; set; }
        public string RepositoryAccountSetting { get; set; }
        public string RepositoryPasswordSetting { get; set; }
    }
}