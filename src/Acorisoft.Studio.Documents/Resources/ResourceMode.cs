namespace Acorisoft.Studio.Documents.Resources
{
    public enum ResourceMode
    {
        
        /// <summary>
        /// 存储于文件夹中。
        /// </summary>
        Outside,
        
        /// <summary>
        /// 存储于数据库中，一般用于不可变的内容
        /// </summary>
        Inside,
    }
}