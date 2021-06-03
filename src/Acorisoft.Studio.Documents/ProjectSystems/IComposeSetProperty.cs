using Acorisoft.Studio.Resources;

namespace Acorisoft.Studio.ProjectSystems
{
    /// <summary>
    /// <see cref="IComposeSetProperty"/> 接口表示一个抽象的创作集属性。
    /// </summary>
    [StoragePlace(StorageClassifier.Metadata)]
    public interface IComposeSetProperty : IDataProperty
    {
        /// <summary>
        /// 获取或设置当前创作集的相册、
        /// </summary>
        ImageResource Album { get; set; }
        
        /// <summary>
        /// 获取或设置当前创作集的摘要、
        /// </summary>
        string Summary { get; set; }
        
        /// <summary>
        /// 获取或设置当前创作集的仓库托管类型
        /// </summary>
        RepositoryType RepositoryType { get; set; }
        
        /// <summary>
        /// 获取或设置当前创作集的路径。
        /// </summary>
        string Path { get; set; }
        
        /// <summary>
        /// 获取或设置当前创作集的仓库托管路径。
        /// </summary>
        string Repository { get; set; }
        
        /// <summary>
        /// 获取或设置当前创作集的仓库的提交邮箱。
        /// </summary>
        string RepositoryEmailSetting { get; set; }
        
        /// <summary>
        /// 获取或设置当前创作集的仓库的提交账户。
        /// </summary>
        string RepositoryAccountSetting { get; set; }
        
        /// <summary>
        /// 获取或设置当前创作集的仓库的提交账户密码。
        /// </summary>
        string RepositoryPasswordSetting { get; set; }
    }
}