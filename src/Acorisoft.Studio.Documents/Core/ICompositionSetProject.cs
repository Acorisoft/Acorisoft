using System;

namespace Acorisoft.Studio.Core
{
    public interface ICompositionSetProject
    {
        /// <summary>
        /// 获取或设置当前
        /// </summary>
        Guid Id { get; set; }
        string Path { get; set; }
        string Name { get; set; }
    }
}