using System;
using System.IO;
using Acorisoft.Studio.ProjectSystems;
using LiteDB;

namespace Acorisoft.Studio.Resources
{
    [Obsolete]
    public class CoverResource : ImageResource
    {
        public CoverResource()
        {
            Id = Guid.NewGuid();
        }
        
        public sealed override string GetResourceFileName(IComposeSet composeSet)
        {
            if (composeSet != null)
            {
                return Path.Combine(composeSet.GetComposeSetPath(ComposeSetKnownFolder.Image), Id.ToString("N"));
            }
            
            throw new InvalidOperationException("无法打开创作集");
        }

        public sealed override string GetResourceKey()
        {
            return Id.ToString("N");
        }

        /// <summary>
        /// 获取或设置当前封面资源的路径。
        /// </summary>
        [BsonField("p")]
        public Guid Id { get; set; }
    }
}