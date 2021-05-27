namespace Acorisoft.Studio.Documents.StickyNote
{
    /// <summary>
    /// <see cref="StickyNoteDocument"/> 类型表示一个便签文档。
    /// </summary>
    public class StickyNoteDocument : Document
    {
        /// <summary>
        /// 获取或设置当前 <see cref="StickyNoteDocument"/> 的名称。
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 获取或设置当前 <see cref="StickyNoteDocument"/> 的纯文本内容。
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// 获取或设置当前 <see cref="StickyNoteDocument"/> 的原始数据。
        /// </summary>
        public string RawData { get; set; }
    }
}