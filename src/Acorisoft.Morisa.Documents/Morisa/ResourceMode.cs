namespace Acorisoft.Morisa
{
    /// <summary>
    /// <see cref="ResourceMode"/> 枚举用于枚举当前数据集的资源存储模式。
    /// </summary>
    public enum ResourceMode
    {
        /// <summary>
        /// 默认模式，资源默认在数据集之外存储（数据集所在的目录）
        /// </summary>
        Default,

        /// <summary>
        /// 内存储模式，资源默认存储于数据集之中
        /// </summary>
        Inside,

        /// <summary>
        /// 外存储模式，资源默认存储在数据集之外，
        /// </summary>
        Outside,


    }
}