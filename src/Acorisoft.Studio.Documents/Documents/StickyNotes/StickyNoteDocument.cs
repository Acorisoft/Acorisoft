namespace Acorisoft.Studio.Documents.StickyNotes
{
    /// <summary>
    /// <see cref="StickyNoteDocument"/> 类型表示一个便签文档。
    /// </summary>
    public class StickyNoteDocument : Acorisoft.Studio.Document
    {
        
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