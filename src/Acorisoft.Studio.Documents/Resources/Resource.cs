using System;
using Acorisoft.Studio.ProjectSystems;
using LiteDB;

namespace Acorisoft.Studio.Resources
{
    public abstract class Resource
    {
        /// <summary>
        /// 获取一个值，该值用于表示当前资源是否
        /// </summary>
        public ResourceMode Mode { get; set; }
        
        public abstract string GetResourceFileName(IComposeSet composeSet);
        public abstract string GetResourceKey();
    }
}