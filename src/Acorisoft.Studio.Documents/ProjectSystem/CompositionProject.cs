using System;
using Acorisoft.Extensions.Platforms.ComponentModel;
using LiteDB;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    /// <summary>
    /// <see cref="CompositionProject"/> 类型表示一个创作项目。
    /// </summary>
    public class CompositionProject : ObservableObject, ICompositionProject, IEquatable<CompositionProject>, IDisposable
    {
        private string _name;
        private string _path;
        private Guid _id;
        public sealed override int GetHashCode()
        {
            return HashCode.Combine(Name, Path);
        }

        public bool Equals(CompositionProject y)
        {
            if (y is not null)
            {
                return y.Name == Name && y.Path == Path;
            }

            return false;
        }
        public sealed override bool Equals(object? obj)
        {
            if (obj is CompositionProject compositionProject)
            {
                return Equals(compositionProject);
            }
            return base.Equals(obj);
        }

        [BsonId]
        public Guid Id
        {
            get => _id;
            set => SetValue(ref _id, value);
        }

        public string Path
        {
            get => _path;
            set => SetValue(ref _path, value);
        }
        
        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }
    }
}