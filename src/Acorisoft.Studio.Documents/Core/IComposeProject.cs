using System;
using Acorisoft.Studio.Resources;

namespace Acorisoft.Studio.Systems
{
    public interface IComposeProject
    {
        /// <summary>
        /// 获取或设置当前创作项目的唯一标识符。
        /// </summary>
        Guid Id { get; set; }
        
        /// <summary>
        /// 获取或设置当前创作集的相册、
        /// </summary>
        ImageResource Album { get; set; }
        string Path { get; set; }
        string Name { get; set; }
    }
}