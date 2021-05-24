using System;
using Acorisoft.Extensions.Platforms.ComponentModel;
using LiteDB;

namespace Acorisoft.Studio.Documents.ProjectSystem
{
    /// <summary>
    /// <see cref="CompositionProject"/> 类型表示一个创作项目。
    /// </summary>
    public class CompositionProject : ObservableObject, ICompositionProject
    {
        private string _name;
        private string _path;
        private Guid _id;

        /// <summary>
        /// 
        /// </summary>
        [BsonIgnore]
        public LiteDatabase Database { get; set; }

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