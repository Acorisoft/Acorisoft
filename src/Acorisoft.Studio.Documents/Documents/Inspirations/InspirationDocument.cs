namespace Acorisoft.Studio.Documents.Inspirations
{
    /// <summary>
    /// <see cref="InspirationDocument"/> 表示一个抽象的灵感文档。
    /// </summary>
    public abstract class InspirationDocument : Document
    {
        /// <summary>
        /// 获取或设置灵感的内容类型。
        /// </summary>
        public abstract InspirationType Type { get; }
        
        /// <summary>
        /// 获取或设置灵感的内容摘要。
        /// </summary>
        public string Summary { get; set; }
    }
}