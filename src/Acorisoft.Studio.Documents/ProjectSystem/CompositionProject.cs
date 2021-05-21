using System;
using Acorisoft.Extension.ComponentModels;
using LiteDB;

namespace Acorisoft.Studio.Document.ProjectSystem
{
    /// <summary>
    /// <see cref="CompositionProject"/> 表示一个创作项目.
    /// </summary>
    public class CompositionProject : ObservableObject
    {
        private string _name;
        private string _path;
        private string _summary;
        
        /// <summary>
        /// 获取或设置当前项目的唯一标识符。
        /// </summary>
        [BsonId]
        public Guid Id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Name { get => _name; set => SetValue(ref _name,value); }
        
        /// <summary>
        /// 
        /// </summary>
        public string Path  { get => _path; set => SetValue(ref _path,value); }
        
        /// <summary>
        /// 
        /// </summary>
        public string Summary { get => _summary; set => SetValue(ref _summary,value); }
    }
}