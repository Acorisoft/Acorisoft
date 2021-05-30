using System;
using Acorisoft.Studio;

namespace Acorisoft.Studio.ProjectSystem
{
    public interface ICompositionSetProperty : IDataProperty
    {
        /// <summary>
        /// 
        /// </summary>
        Uri Cover { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        string Summary { get; set; }
        
        /// <summary>
        /// 获取或设置当前数据库的版本
        /// </summary>
        DatabaseVersion Version { get; set; }
    }
}