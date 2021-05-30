using System;

namespace Acorisoft.Studio.ProjectSystem
{
    public interface ICompositionProject
    {
        Guid Id { get; set; }
        string Path { get; set; }
        string Name { get; set; }
    }
}