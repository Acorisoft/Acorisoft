using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="LoadContext"/>
    /// </summary>
    public class LoadContext : ILoadContext
    {
        /// <summary>
        /// 获取当前 <see cref="LoadContext"/> 加载上下文所存储的上下文名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取当前 <see cref="LoadContext"/> 加载上下文所存储的上下文文件夹目录。
        /// </summary>
        public string Directory { get; set; }


        /// <summary>
        /// 获取当前 <see cref="LoadContext"/> 加载上下文所存储的上下文文件路径。
        /// </summary>
        public string FileName { get; set; }
    }

    /// <summary>
    /// <see cref="LoadContext{TProperty}"/>
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    public class LoadContext<TProperty> : LoadContext , ILoadContext<TProperty>
    {
        /// <summary>
        /// 获取当前 <see cref="LoadContext{T}"/> 加载上下文所存储的上下文的存储上下文。
        /// </summary>
        public TProperty Context { get; }
    }
}
