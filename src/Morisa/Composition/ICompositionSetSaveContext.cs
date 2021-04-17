using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.Composition
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICompositionSetSaveContext : ISaveContext<CompositionSetProperty>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    [DebuggerDisplay("{Name} - {FileName}")]
    public class CompositionSetSaveContext : ICompositionSetSaveContext
    {
        private readonly CompositionSetProperty _Property;

        public CompositionSetSaveContext()
        {
            _Property = new CompositionSetProperty();
        }

        /// <summary>
        /// 获取当前 <see cref="CompositionSetSaveContext"/> 存储上下文所存储的上下文名称。
        /// </summary>
        public string Name {
            get => _Property.Name;
            set => _Property.Name = value;
        }

        /// <summary>
        /// 获取当前 <see cref="CompositionSetSaveContext"/> 存储上下文所存储的作者名称。
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 获取当前 <see cref="CompositionSetSaveContext"/> 存储上下文所存储的摘要。
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 获取当前 <see cref="CompositionSetSaveContext"/> 存储上下文所存储的主题。
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// 获取当前 <see cref="CompositionSetSaveContext"/> 加载上下文所存储的上下文文件夹目录。
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// 获取当前 <see cref="CompositionSetSaveContext"/> 加载上下文所存储的上下文文件路径。
        /// </summary>
        public string FileName { get; set; }


        /// <summary>
        /// 获取当前 <see cref="ISaveContext{T}"/> 加载上下文所存储的上下文文件路径。
        /// </summary>
        public CompositionSetProperty Property => _Property;

        public override string ToString()
        {
            return $"{FileName}";
        }
    }
}
